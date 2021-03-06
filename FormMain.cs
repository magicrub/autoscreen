﻿//-----------------------------------------------------------------------
// <copyright file="FormMain.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        private FormEditor formEditor = new FormEditor();
        private FormTrigger formTrigger = new FormTrigger();
        private FormRegion formRegion = new FormRegion();
        private FormScreen formScreen = new FormScreen();
        private FormEnterPassphrase formEnterPassphrase = new FormEnterPassphrase();

        private ImageFormatCollection _imageFormatCollection;

        /// <summary>
        /// Threads for background operations.
        /// </summary>
        private BackgroundWorker runSlideSearchThread = null;

        private BackgroundWorker runDateSearchThread = null;

        private BackgroundWorker runSaveSettings = null;

        /// <summary>
        /// Delegates for the threads.
        /// </summary>
        private delegate void RunSlideSearchDelegate(DoWorkEventArgs e);

        private delegate void RunDateSearchDelegate(DoWorkEventArgs e);

        private delegate void SaveSettingsDelegate(DoWorkEventArgs e);

        /// <summary>
        /// Default settings used by the command line parser.
        /// </summary>
        private const int CAPTURE_LIMIT_MIN = 0;

        private const int CAPTURE_LIMIT_MAX = 9999;
        private const int CAPTURE_DELAY_DEFAULT_IN_MINUTES = 1;

        /// <summary>
        /// The various regular expressions used in the parsing of the command line arguments.
        /// </summary>
        private const string REGEX_COMMAND_LINE_INITIAL = "^-initial$";

        private const string REGEX_COMMAND_LINE_LIMIT = @"^-limit=(?<Limit>\d{1,7})$";

        private const string REGEX_COMMAND_LINE_STOPAT =
            @"^-stopat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        private const string REGEX_COMMAND_LINE_STARTAT =
            @"^-startat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        private const string REGEX_COMMAND_LINE_DELAY =
            @"^-delay=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})\.(?<Milliseconds>\d{3})$";

        private const string REGEX_COMMAND_LINE_LOCK = "^-lock$";
        private const string REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON = "^-hideSystemTrayIcon$";

        /// <summary>
        /// Constructor for the main form. Arguments from the command line can be passed to it.
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        public FormMain(string[] args)
        {
            InitializeComponent();

            if (!Directory.Exists(FileSystem.ApplicationFolder))
            {
                Directory.CreateDirectory(FileSystem.ApplicationFolder);
            }

            Settings.Initialize();

            Log.Enabled = Convert.ToBoolean(Settings.Application.GetByKey("DebugMode", defaultValue: true).Value);

            Log.Write("Starting application.");

            LoadSettings();

            Text = (string)Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value;

            if (args.Length > 0)
            {
                ParseCommandLineArguments(args);
            }
        }

        /// <summary>
        /// When this form loads we'll need to delete slides and then search for dates and slides.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeThreads();

            SearchDates();

            RunTriggersOfConditionType(TriggerConditionType.ApplicationStartup);
        }

        private void InitializeThreads()
        {
            runDateSearchThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runDateSearchThread.DoWork += new DoWorkEventHandler(DoWork_runDateSearchThread);

            runSlideSearchThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runSlideSearchThread.DoWork += new DoWorkEventHandler(DoWork_runSlideSearchThread);
        }

        /// <summary>
        /// Loads the user's saved settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                Log.Write("Loading settings.");

                Settings.User.Load();

                Log.Write("Settings loaded.");

                Log.Write("Initializing image format collection.");
                _imageFormatCollection = new ImageFormatCollection();

                Log.Write("Initializing slideshow module.");

                Slideshow.Initialize();
                Slideshow.OnPlaying += new EventHandler(Slideshow_Playing);

                Log.Write("Initializing editor collection.");

                formEditor.EditorCollection.Load();

                Log.Write("Loaded " + formEditor.EditorCollection.Count + " editors.");

                Log.Write("Initializing trigger collection.");

                formTrigger.TriggerCollection.Load();

                Log.Write("Loaded " + formTrigger.TriggerCollection.Count + " triggers.");

                Log.Write("Initializing region collection.");

                formRegion.RegionCollection.Load(_imageFormatCollection);

                Log.Write("Loaded " + formRegion.RegionCollection.Count + " regions.");

                Log.Write("Initializing screen collection");

                formScreen.ScreenCollection.Load(_imageFormatCollection);

                Log.Write("Loaded " + formScreen.ScreenCollection.Count + " screens.");

                Log.Write("Building screenshot preview context menu.");

                BuildScreenshotPreviewContextualMenu();

                Log.Write(
                    "Loading screenshots into the screenshot collection to generate a history of what was captured.");

                ScreenshotCollection.Load(_imageFormatCollection);

                int screenshotDelay =
                    Convert.ToInt32(Settings.User.GetByKey("ScreenshotDelay", defaultValue: 60000).Value);
                int slideshowDelay =
                    Convert.ToInt32(Settings.User.GetByKey("SlideshowDelay", defaultValue: 1000).Value);

                decimal screenshotDelayHours =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenshotDelay)).Hours);
                decimal screenshotDelayMinutes =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenshotDelay)).Minutes);
                decimal screenshotDelaySeconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenshotDelay)).Seconds);
                decimal screenshotDelayMilliseconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenshotDelay)).Milliseconds);

                decimal slideshowDelayHours =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Hours);
                decimal slideshowDelayMinutes =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Minutes);
                decimal slideshowDelaySeconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Seconds);
                decimal slideshowDelayMilliseconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Milliseconds);

                numericUpDownHoursInterval.Value = screenshotDelayHours;
                numericUpDownMinutesInterval.Value = screenshotDelayMinutes;
                numericUpDownSecondsInterval.Value = screenshotDelaySeconds;
                numericUpDownMillisecondsInterval.Value = screenshotDelayMilliseconds;

                numericUpDownSlideshowDelayHours.Value = slideshowDelayHours;
                numericUpDownSlideshowDelayMinutes.Value = slideshowDelayMinutes;
                numericUpDownSlideshowDelaySeconds.Value = slideshowDelaySeconds;
                numericUpDownSlideshowDelayMilliseconds.Value = slideshowDelayMilliseconds;

                numericUpDownSlideSkip.Value =
                    Convert.ToInt32(Settings.User.GetByKey("SlideSkip", defaultValue: 10).Value);
                numericUpDownCaptureLimit.Value =
                    Convert.ToInt32(Settings.User.GetByKey("CaptureLimit", defaultValue: 0).Value);

                checkBoxSlideSkip.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("SlideSkipCheck", defaultValue: false).Value);
                checkBoxCaptureLimit.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureLimitCheck", defaultValue: false).Value);
                checkBoxInitialScreenshot.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("TakeInitialScreenshotCheck", defaultValue: false).Value);

                checkBoxPassphraseLock.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("LockScreenCaptureSession", defaultValue: false).Value);

                textBoxPassphrase.Text =
                    Settings.User.GetByKey("Passphrase", defaultValue: string.Empty).Value.ToString();

                if (textBoxPassphrase.Text.Length > 0)
                {
                    textBoxPassphrase.ReadOnly = true;
                    buttonSetPassphrase.Enabled = false;
                    checkBoxPassphraseLock.Enabled = true;
                }
                else
                {
                    checkBoxPassphraseLock.Checked = false;
                    checkBoxPassphraseLock.Enabled = false;
                }

                toolStripMenuItemShowSystemTrayIcon.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("ShowSystemTrayIcon", defaultValue: true).Value);

                checkBoxScheduleStopAt.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureStopAtCheck", defaultValue: false).Value);
                checkBoxScheduleStartAt.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureStartAtCheck", defaultValue: false).Value);

                checkBoxSaturday.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnSaturdayCheck", defaultValue: false).Value);
                checkBoxSunday.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnSundayCheck", defaultValue: false).Value);
                checkBoxMonday.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnMondayCheck", defaultValue: false).Value);
                checkBoxTuesday.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnTuesdayCheck", defaultValue: false).Value);
                checkBoxWednesday.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnWednesdayCheck", defaultValue: false).Value);
                checkBoxThursday.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnThursdayCheck", defaultValue: false).Value);
                checkBoxFriday.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnFridayCheck", defaultValue: false).Value);

                checkBoxScheduleOnTheseDays.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnTheseDaysCheck", defaultValue: false).Value);

                dateTimePickerScheduleStopAt.Value = DateTime.Parse(Settings.User.GetByKey("CaptureStopAtValue",
                        defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0))
                    .Value
                    .ToString());
                dateTimePickerScheduleStartAt.Value = DateTime.Parse(Settings.User.GetByKey("CaptureStartAtValue",
                        defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0))
                    .Value
                    .ToString());

                EnableStartCapture();

                CaptureLimitCheck();
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::LoadSettings", ex);
            }
        }

        /// <summary>
        /// Saves the user's current settings so we can load them at a later time when the user executes the application.
        /// </summary>
        private void SaveSettings()
        {
            if (runSaveSettings == null)
            {
                runSaveSettings = new BackgroundWorker
                {
                    WorkerReportsProgress = false,
                    WorkerSupportsCancellation = true
                };
                runSaveSettings.DoWork += new DoWorkEventHandler(DoWork_runSaveSettingsThread);
            }
            else
            {
                if (!runSaveSettings.IsBusy)
                {
                    runSaveSettings.RunWorkerAsync();
                }
            }
        }

        /// <summary>
        /// When this form is closing we can either exit the application or just close this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                // Hide the system tray icon.
                notifyIcon.Visible = false;

                HideInterface();

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runSlideSearchThread != null && runSlideSearchThread.IsBusy)
                {
                    runSlideSearchThread.CancelAsync();
                }

                formEditor.EditorCollection.Save();

                ScreenshotCollection.Save();

                formTrigger.TriggerCollection.Save();

                formRegion.RegionCollection.Save();

                formScreen.ScreenCollection.Save();

                // Exit.
                Environment.Exit(0);
            }
            else
            {
                SaveSettings();

                RunTriggersOfConditionType(TriggerConditionType.InterfaceClosing);

                // If there isn't a Trigger for "InterfaceClosing" that performs an action
                // then make sure we cancel this event so that nothing happens. We want the user
                // to use a Trigger, and decide what they want to do, when closing the interface window.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Searches for dates. They should be in the format yyyy-mm-dd.
        /// </summary>
        private void SearchDates()
        {
            ClearScreenPreview();
            DisableToolStripButtons();

            monthCalendar.BoldedDates = null;

            if (runDateSearchThread != null && !runDateSearchThread.IsBusy)
            {
                runDateSearchThread.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Searches for slides.
        /// </summary>
        private void SearchSlides()
        {
            listBoxSlides.BeginUpdate();

            ClearScreenPreview();
            DisableToolStripButtons();

            if (runSlideSearchThread != null && !runSlideSearchThread.IsBusy)
            {
                runSlideSearchThread.RunWorkerAsync();
            }

            listBoxSlides.EndUpdate();

            // It's very important here to disable all the slideshow controls if there were slides found.
            if (listBoxSlides.Items.Count == 0)
            {
                toolStripButtonFirstSlide.Enabled = false;
                toolStripButtonPreviousSlide.Enabled = false;
                toolStripButtonPlaySlideshow.Enabled = false;
                toolStripButtonNextSlide.Enabled = false;
                toolStripButtonLastSlide.Enabled = false;
            }
        }

        /// <summary>
        /// This thread is responsible for finding slides.
        /// </summary>
        /// <param name="e"></param>
        private void RunSlideSearch(DoWorkEventArgs e)
        {
            if (listBoxSlides.InvokeRequired)
            {
                listBoxSlides.Invoke(new RunSlideSearchDelegate(RunSlideSearch), new object[] { e });
            }
            else
            {
                ArrayList slides =
                    ScreenshotCollection.GetSlidesByDate(monthCalendar.SelectionStart.ToString(MacroParser.DateFormat));

                if (slides != null && slides.Count > 0)
                {
                    foreach (string slide in slides)
                    {
                        listBoxSlides.Items.Add(slide);
                    }
                }

                if (listBoxSlides.Items.Count > 0)
                {
                    monthCalendar.Enabled = true;

                    listBoxSlides.SelectedIndex = listBoxSlides.Items.Count - 1;

                    toolStripButtonNextSlide.Enabled = true;
                    toolStripButtonLastSlide.Enabled = true;

                    EnablePlaySlideshow();
                }
            }
        }

        /// <summary>
        /// This thread is responsible for figuring out what days screenshots were taken.
        /// </summary>
        /// <param name="e"></param>
        private void RunDateSearch(DoWorkEventArgs e)
        {
            if (monthCalendar.InvokeRequired)
            {
                monthCalendar.Invoke(new RunDateSearchDelegate(RunDateSearch), new object[] { e });
            }
            else
            {
                ArrayList dates = ScreenshotCollection.GetDates();

                DateTime[] boldedDates = new DateTime[dates.Count];

                for (int i = 0; i < dates.Count; i++)
                {
                    boldedDates.SetValue(ConvertDateStringToDateTime(dates[i].ToString()), i);
                }

                monthCalendar.BoldedDates = boldedDates;
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        /// <param name="e"></param>
        private void SaveSettings(DoWorkEventArgs e)
        {
            try
            {
                if (listBoxSlides.InvokeRequired)
                {
                    listBoxSlides.Invoke(new SaveSettingsDelegate(SaveSettings), new object[] { e });
                }
                else
                {
                    Log.Write("Saving settings.");

                    Settings.User.GetByKey("SlideSkip", defaultValue: 10).Value = numericUpDownSlideSkip.Value;
                    Settings.User.GetByKey("CaptureLimit", defaultValue: 0).Value = numericUpDownCaptureLimit.Value;
                    Settings.User.GetByKey("ScreenshotDelay", defaultValue: 60000).Value = GetCaptureDelay();
                    Settings.User.GetByKey("SlideshowDelay", defaultValue: 1000).Value = GetSlideshowDelay();
                    Settings.User.GetByKey("SlideSkipCheck", defaultValue: false).Value = checkBoxSlideSkip.Checked;
                    Settings.User.GetByKey("CaptureLimitCheck", defaultValue: false).Value =
                        checkBoxCaptureLimit.Checked;
                    Settings.User.GetByKey("TakeInitialScreenshotCheck", defaultValue: false).Value =
                        checkBoxInitialScreenshot.Checked;
                    Settings.User.GetByKey("ShowSystemTrayIcon", defaultValue: true).Value =
                        toolStripMenuItemShowSystemTrayIcon.Checked;
                    Settings.User.GetByKey("CaptureStopAtCheck", defaultValue: false).Value =
                        checkBoxScheduleStopAt.Checked;
                    Settings.User.GetByKey("CaptureStartAtCheck", defaultValue: false).Value =
                        checkBoxScheduleStartAt.Checked;
                    Settings.User.GetByKey("CaptureOnSundayCheck", defaultValue: false).Value = checkBoxSunday.Checked;
                    Settings.User.GetByKey("CaptureOnMondayCheck", defaultValue: false).Value = checkBoxMonday.Checked;
                    Settings.User.GetByKey("CaptureOnTuesdayCheck", defaultValue: false).Value =
                        checkBoxTuesday.Checked;
                    Settings.User.GetByKey("CaptureOnWednesdayCheck", defaultValue: false).Value =
                        checkBoxWednesday.Checked;
                    Settings.User.GetByKey("CaptureOnThursdayCheck", defaultValue: false).Value =
                        checkBoxThursday.Checked;
                    Settings.User.GetByKey("CaptureOnFridayCheck", defaultValue: false).Value = checkBoxFriday.Checked;
                    Settings.User.GetByKey("CaptureOnSaturdayCheck", defaultValue: false).Value =
                        checkBoxSaturday.Checked;
                    Settings.User.GetByKey("CaptureOnTheseDaysCheck", defaultValue: false).Value =
                        checkBoxScheduleOnTheseDays.Checked;
                    Settings.User.GetByKey("CaptureStopAtValue",
                            defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0,
                                0))
                        .Value = dateTimePickerScheduleStopAt.Value;
                    Settings.User.GetByKey("CaptureStartAtValue",
                            defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0,
                                0))
                        .Value = dateTimePickerScheduleStartAt.Value;
                    Settings.User.GetByKey("LockScreenCaptureSession", defaultValue: false).Value =
                        checkBoxPassphraseLock.Checked;
                    Settings.User.GetByKey("Passphrase", defaultValue: string.Empty).Value = textBoxPassphrase.Text;
                    Settings.User.GetByKey("Schedule", defaultValue: false).Value = timerScheduledCaptureStart.Enabled;

                    Settings.User.Save();

                    Log.Write("Settings saved.");
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::RunSaveApplicationSettings", ex);
            }
        }

        /// <summary>
        /// Converts the string representation of a date into a DateTime object. Used by the RunDateSearch thread so we can set bolded dates in the calendar.
        /// </summary>
        /// <param name="date">A string representation of a date (such as "2019-02-06").</param>
        /// <returns>A DateTime object based on the provided date string.</returns>
        private DateTime ConvertDateStringToDateTime(string date)
        {
            return new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(5, 2)),
                Convert.ToInt32(date.Substring(8, 2)));
        }

        /// <summary>
        /// Shows the interface.
        /// </summary>
        private void ShowInterface()
        {
            Log.Write("Showing interface.");

            SearchDates();
            SearchSlides();

            if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
            {
                formEnterPassphrase.ShowDialog(this);
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.LockScreenCaptureSession)
            {
                checkBoxPassphraseLock.Checked = false;
                Settings.User.GetByKey("LockScreenCaptureSession", defaultValue: false).Value = false;
                SaveSettings();

                Opacity = 100;
                toolStripMenuItemShowInterface.Enabled = false;
                toolStripMenuItemHideInterface.Enabled = true;

                Show();

                Visible = true;
                ShowInTaskbar = true;

                // If the window is mimimized then show it when the user wants to open the window.
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }

                RunTriggersOfConditionType(TriggerConditionType.InterfaceShowing);

                Focus();
            }
        }

        /// <summary>
        /// Hides the interface.
        /// </summary>
        private void HideInterface()
        {
            SaveSettings();

            Log.Write("Hiding interface.");

            // Pause the slideshow if you find it still playing.
            if (Slideshow.Playing)
            {
                PauseSlideshow();
            }

            Opacity = 0;
            toolStripMenuItemShowInterface.Enabled = true;
            toolStripMenuItemHideInterface.Enabled = false;

            Hide();
            Visible = false;
            ShowInTaskbar = false;

            RunTriggersOfConditionType(TriggerConditionType.InterfaceHiding);
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            if (timerScreenCapture.Enabled)
            {
                Log.Write("Stopping screen capture.");

                if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
                {
                    formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!ScreenCapture.LockScreenCaptureSession)
                {
                    checkBoxPassphraseLock.Checked = false;
                    Settings.User.GetByKey("LockScreenCaptureSession", defaultValue: false).Value = false;
                    SaveSettings();

                    ScreenCapture.Count = 0;
                    timerScreenCapture.Enabled = false;

                    ScreenCapture.Running = false;

                    DisableStopCapture();
                    EnableStartCapture();

                    SearchDates();
                    SearchSlides();

                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStopped);
                }
            }
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        private void PlaySlideshow()
        {
            int slideshowDelay = GetSlideshowDelay();

            DisableControls();

            if (listBoxSlides.Items.Count > 0 && slideshowDelay > 0)
            {
                if (Slideshow.Index == Slideshow.Count - 1)
                {
                    Slideshow.First();
                    listBoxSlides.SelectedIndex = Slideshow.Index;
                }

                toolStripButtonPlaySlideshow.Image = Properties.Resources.player_pause;

                Slideshow.Interval = slideshowDelay;
                Slideshow.SlideSkipCheck = checkBoxSlideSkip.Checked;
                Slideshow.SlideSkip = (int)numericUpDownSlideSkip.Value;

                Slideshow.Play();
            }
        }

        /// <summary>
        /// Pauses the slideshow.
        /// </summary>
        private void PauseSlideshow()
        {
            EnableControls();

            if (listBoxSlides.Items.Count > 0)
            {
                toolStripButtonPlaySlideshow.Image = Properties.Resources.player_play;

                Slideshow.Stop();
            }
        }

        /// <summary>
        /// Stops the slideshow.
        /// </summary>
        private void StopSlideshow()
        {
            EnableControls();

            toolStripButtonPlaySlideshow.Image = Properties.Resources.player_play;

            Slideshow.Stop();
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        private void StartScreenCapture()
        {
            if (!timerScreenCapture.Enabled)
            {
                SaveSettings();

                // Stop the slideshow if it's currently playing.
                if (Slideshow.Playing)
                {
                    Slideshow.Stop();
                }

                // Stop the date search thread if it's busy.
                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                // Stop the slide search thread if it's busy.
                if (runSlideSearchThread != null && runSlideSearchThread.IsBusy)
                {
                    runSlideSearchThread.CancelAsync();
                }

                DisableStartCapture();
                EnableStopScreenCapture();

                // Setup the properties for the screen capture class.
                ScreenCapture.Delay = GetCaptureDelay();
                ScreenCapture.Limit = checkBoxCaptureLimit.Checked ? (int)numericUpDownCaptureLimit.Value : 0;

                if (checkBoxPassphraseLock.Checked)
                {
                    ScreenCapture.LockScreenCaptureSession = true;
                }
                else
                {
                    ScreenCapture.LockScreenCaptureSession = false;
                }

                ScreenCapture.Running = true;

                ScreenCapture.DateTimeStartCapture = DateTime.Now;

                RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStarted);

                if (checkBoxInitialScreenshot.Checked)
                {
                    Log.Write("Taking initial screenshots.");

                    TakeScreenshot();
                }

                // Start taking screenshots.
                Log.Write("Starting screen capture.");

                timerScreenCapture.Interval = GetCaptureDelay();
                timerScreenCapture.Enabled = true;
            }
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the appropriate image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_listBoxSlides(object sender, EventArgs e)
        {
            Slideshow.Index = listBoxSlides.SelectedIndex;
            Slideshow.Count = listBoxSlides.Items.Count;

            if (Slideshow.Index == (Slideshow.Count - 1))
            {
                StopSlideshow();
            }

            if (Slideshow.Index == 0)
            {
                toolStripButtonFirstSlide.Enabled = false;
                toolStripButtonPreviousSlide.Enabled = false;
            }
            else
            {
                toolStripButtonFirstSlide.Enabled = true;
                toolStripButtonPreviousSlide.Enabled = true;
            }

            if ((Slideshow.Count - 1) <= Slideshow.Index)
            {
                toolStripButtonNextSlide.Enabled = false;
                toolStripButtonLastSlide.Enabled = false;
            }
            else
            {
                toolStripButtonNextSlide.Enabled = true;
                toolStripButtonLastSlide.Enabled = true;
            }

            if (Slideshow.Index >= 0 && Slideshow.Index <= (Slideshow.Count - 1))
            {
                Slideshow.SelectedSlide = listBoxSlides.Items[Slideshow.Index].ToString();

                foreach (TabPage tabPage in tabControlScreens.TabPages)
                {
                    Control[] controls = tabPage.Controls.Find(tabPage.Name, false);

                    if (controls != null && controls.Length == 1)
                    {
                        PictureBox pictureBox = (PictureBox)controls[0];

                        Screenshot selectedScreenshot = ScreenshotCollection.GetBySlide(tabPage.Name, listBoxSlides.Text);
                        pictureBox.Image = ScreenCapture.GetImageByPath(selectedScreenshot.Path);
                    }
                }
            }
        }

        /// <summary>
        /// Converts the given hours, minutes, seconds, and milliseconds into an aggregate milliseconds value.
        /// </summary>
        /// <param name="hours">The number of hours to be converted.</param>
        /// <param name="minutes">The number of minutes to be converted.</param>
        /// <param name="seconds">The number of seconds to be converted.</param>
        /// <param name="milliseconds">The number of milliseconds to be converted.</param>
        /// <returns></returns>
        private int ConvertIntoMilliseconds(int hours, int minutes, int seconds, int milliseconds)
        {
            return 1000 * (hours * 3600 + minutes * 60 + seconds) + milliseconds;
        }

        /// <summary>
        /// Returns the screen capture delay. This value will be used as the screen capture timer's interval property.
        /// </summary>
        /// <returns></returns>
        private int GetCaptureDelay()
        {
            return ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value,
                (int)numericUpDownMillisecondsInterval.Value);
        }

        /// <summary>
        /// Returns the slideshow delay. This value will be used as the slideshow timer's interval property.
        /// </summary>
        /// <returns></returns>
        private int GetSlideshowDelay()
        {
            return ConvertIntoMilliseconds((int)numericUpDownSlideshowDelayHours.Value,
                (int)numericUpDownSlideshowDelayMinutes.Value, (int)numericUpDownSlideshowDelaySeconds.Value,
                (int)numericUpDownSlideshowDelayMilliseconds.Value);
        }

        /// <summary>
        /// Disables the appropriate controls when playing the slideshow.
        /// </summary>
        private void DisableControls()
        {
            monthCalendar.Enabled = false;

            numericUpDownSlideshowDelayHours.Enabled = false;
            numericUpDownSlideshowDelayMinutes.Enabled = false;
            numericUpDownSlideshowDelaySeconds.Enabled = false;
            numericUpDownSlideshowDelayMilliseconds.Enabled = false;

            numericUpDownSlideSkip.Enabled = false;
            checkBoxSlideSkip.Enabled = false;

            toolStripButtonStartCapture.Enabled = false;
            toolStripMenuItemStartCapture.Enabled = false;
        }

        /// <summary>
        /// Enables the appropriate controls when the slideshow is paused or stopped.
        /// </summary>
        private void EnableControls()
        {
            monthCalendar.Enabled = true;

            numericUpDownSlideshowDelayHours.Enabled = true;
            numericUpDownSlideshowDelayMinutes.Enabled = true;
            numericUpDownSlideshowDelaySeconds.Enabled = true;
            numericUpDownSlideshowDelayMilliseconds.Enabled = true;

            numericUpDownSlideSkip.Enabled = true;
            checkBoxSlideSkip.Enabled = true;

            if (!timerScreenCapture.Enabled)
            {
                toolStripButtonStartCapture.Enabled = true;
                toolStripMenuItemStartCapture.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the screenshot images in the interface when searching for dates and slides.
        /// </summary>
        private void ClearScreenPreview()
        {
            Slideshow.Clear();
            listBoxSlides.Items.Clear();
        }

        /// <summary>
        /// Disables the tool strip buttons when searching for dates and slides.
        /// </summary>
        private void DisableToolStripButtons()
        {
            toolStripButtonFirstSlide.Enabled = false;
            toolStripButtonPreviousSlide.Enabled = false;
            toolStripButtonPlaySlideshow.Enabled = false;
            toolStripButtonNextSlide.Enabled = false;
            toolStripButtonLastSlide.Enabled = false;
        }

        /// <summary>
        /// Shows the slideshow.
        /// </summary>
        private void ShowSlideshow()
        {
            if (Slideshow.Playing)
            {
                Slideshow.Stop();
            }

            tabControlModules.SelectedTab = tabControlModules.TabPages["tabPageSlideshow"];
        }

        /// <summary>
        /// Shows the slideshow when a date on the calendar has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSelected_monthCalendar(object sender, DateRangeEventArgs e)
        {
            ShowSlideshow();
        }

        /// <summary>
        /// Shows the first slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonFirstSlide(object sender, EventArgs e)
        {
            Slideshow.First();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the previous slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonPreviousSlide(object sender, EventArgs e)
        {
            Slideshow.Previous();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonPlaySlideshow(object sender, EventArgs e)
        {
            if (Slideshow.Playing)
            {
                PauseSlideshow();
            }
            else
            {
                PlaySlideshow();
            }
        }

        /// <summary>
        /// Shows the next slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonNextSlide(object sender, EventArgs e)
        {
            Slideshow.Next();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the last slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonLastSlide(object sender, EventArgs e)
        {
            Slideshow.Last();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemStartScreenCapture(object sender, EventArgs e)
        {
            StartScreenCapture();
        }

        /// <summary>
        /// Stops the currently running screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemStopScreenCapture(object sender, EventArgs e)
        {
            StopScreenCapture();
        }

        /// <summary>
        /// Exits the application from the system tray icon menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemExit(object sender, EventArgs e)
        {
            ExitApplication();
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private void ExitApplication()
        {
            Log.Write("Exiting application.");

            if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
            {
                formEnterPassphrase.ShowDialog(this);
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.LockScreenCaptureSession)
            {
                RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                checkBoxPassphraseLock.Checked = false;
                Settings.User.GetByKey("LockScreenCaptureSession", defaultValue: false).Value = false;
                SaveSettings();

                // Hide the system tray icon.
                notifyIcon.Visible = false;

                HideInterface();

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runSlideSearchThread != null && runSlideSearchThread.IsBusy)
                {
                    runSlideSearchThread.CancelAsync();
                }

                formEditor.EditorCollection.Save();

                ScreenshotCollection.Save();

                formTrigger.TriggerCollection.Save();

                formRegion.RegionCollection.Save();

                formScreen.ScreenCollection.Save();

                // Exit.
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Runs the slide search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runSlideSearchThread(object sender, DoWorkEventArgs e)
        {
            RunSlideSearch(e);
        }

        /// <summary>
        /// Runs the date search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runDateSearchThread(object sender, DoWorkEventArgs e)
        {
            RunDateSearch(e);
        }

        /// <summary>
        /// Runs the save settings thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runSaveSettingsThread(object sender, DoWorkEventArgs e)
        {
            SaveSettings(e);
        }

        /// <summary>
        /// Updates the list of screenshots with the current slideshow index when the slideshow is playing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slideshow_Playing(object sender, EventArgs e)
        {
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Figures out if the "Play Slideshow" control should be enabled or disabled.
        /// </summary>
        private void EnablePlaySlideshow()
        {
            if (GetSlideshowDelay() > 0 && listBoxSlides.Items.Count > 0)
            {
                toolStripButtonPlaySlideshow.Enabled = true;
            }
            else
            {
                toolStripButtonPlaySlideshow.Enabled = false;
            }
        }

        /// <summary>
        /// Figures out if the "Start Capture" controls should be enabled or disabled.
        /// </summary>
        private void EnableStartCapture()
        {
            if (GetCaptureDelay() > 0)
            {
                toolStripButtonStartCapture.Enabled = true;
                toolStripMenuItemStartCapture.Enabled = true;

                numericUpDownHoursInterval.Enabled = true;
                checkBoxInitialScreenshot.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                checkBoxCaptureLimit.Enabled = true;
                numericUpDownCaptureLimit.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;

                checkBoxScheduleStartAt.Enabled = true;
                checkBoxScheduleStopAt.Enabled = true;
                checkBoxScheduleOnTheseDays.Enabled = true;
                checkBoxSunday.Enabled = true;
                checkBoxMonday.Enabled = true;
                checkBoxTuesday.Enabled = true;
                checkBoxWednesday.Enabled = true;
                checkBoxThursday.Enabled = true;
                checkBoxFriday.Enabled = true;
                checkBoxSaturday.Enabled = true;
                dateTimePickerScheduleStartAt.Enabled = true;
                dateTimePickerScheduleStopAt.Enabled = true;
            }
            else
            {
                DisableStartCapture();
            }
        }

        /// <summary>
        /// Enables the "Stop Capture" controls.
        /// </summary>
        private void EnableStopScreenCapture()
        {
            toolStripButtonStopCapture.Enabled = true;
            toolStripMenuItemStopCapture.Enabled = true;

            numericUpDownHoursInterval.Enabled = false;
            checkBoxInitialScreenshot.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            checkBoxCaptureLimit.Enabled = false;
            numericUpDownCaptureLimit.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;

            checkBoxScheduleStartAt.Enabled = false;
            checkBoxScheduleStopAt.Enabled = false;
            checkBoxScheduleOnTheseDays.Enabled = false;
            checkBoxSunday.Enabled = false;
            checkBoxMonday.Enabled = false;
            checkBoxTuesday.Enabled = false;
            checkBoxWednesday.Enabled = false;
            checkBoxThursday.Enabled = false;
            checkBoxFriday.Enabled = false;
            checkBoxSaturday.Enabled = false;
            dateTimePickerScheduleStartAt.Enabled = false;
            dateTimePickerScheduleStopAt.Enabled = false;
        }

        /// <summary>
        /// Disables the "Stop Capture" controls.
        /// </summary>
        private void DisableStopCapture()
        {
            toolStripButtonStopCapture.Enabled = false;
            toolStripMenuItemStopCapture.Enabled = false;
        }

        /// <summary>
        /// Disables the "Start Capture" controls.
        /// </summary>
        private void DisableStartCapture()
        {
            toolStripButtonStartCapture.Enabled = false;
            toolStripMenuItemStartCapture.Enabled = false;
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_toolStripButtonPlaySlideshow(object sender, EventArgs e)
        {
            PlaySlideshow();
        }

        /// <summary>
        /// Shows the interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemShowInterface(object sender, EventArgs e)
        {
            ShowInterface();
        }

        /// <summary>
        /// Hides the interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemHideInterface(object sender, EventArgs e)
        {
            HideInterface();
        }

        /// <summary>
        /// Shows the "About" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemAbout(object sender, EventArgs e)
        {
            MessageBox.Show(
                Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value + " " +
                Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value +
                " (\"Dalek\")\nDeveloped by Gavin Kendall (2008 - 2019)", "About", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Opens Windows Explorer to show the location of the selected screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemShowScreenshotLocation(object sender, EventArgs e)
        {
            if (listBoxSlides.SelectedIndex > -1)
            {
                Screenshot selectedScreenshot = ScreenshotCollection.GetBySlide(tabControlScreens.SelectedTab.Name, Slideshow.SelectedSlide);

                if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.Path) &&
                    File.Exists(selectedScreenshot.Path))
                {
                    Process.Start(FileSystem.FileManager, "/select,\"" + selectedScreenshot.Path + "\"");
                }
            }
        }

        /// <summary>
        /// Parses the command line and processes the commands the user has chosen from the command line.
        /// </summary>
        /// <param name="args"></param>
        private void ParseCommandLineArguments(string[] args)
        {
            try
            {
                Log.Write("Parsing command line arguments.");

                #region Default Values for Command Line Arguments/Options

                checkBoxInitialScreenshot.Checked = false;

                checkBoxCaptureLimit.Checked = false;
                numericUpDownCaptureLimit.Value = CAPTURE_LIMIT_MIN;

                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = CAPTURE_DELAY_DEFAULT_IN_MINUTES;
                numericUpDownSecondsInterval.Value = 0;
                numericUpDownMillisecondsInterval.Value = 0;

                checkBoxScheduleStopAt.Checked = false;
                checkBoxScheduleStartAt.Checked = false;
                checkBoxScheduleOnTheseDays.Checked = false;

                toolStripMenuItemShowSystemTrayIcon.Checked = true;

                #endregion Default Values for Command Line Arguments/Options

                Regex rgxCommandLineLock = new Regex(REGEX_COMMAND_LINE_LOCK);
                Regex rgxCommandLineLimit = new Regex(REGEX_COMMAND_LINE_LIMIT);
                Regex rgxCommandLineInitial = new Regex(REGEX_COMMAND_LINE_INITIAL);
                Regex rgxCommandLineCaptureDelay = new Regex(REGEX_COMMAND_LINE_DELAY);
                Regex rgxCommandLineScheduleStopAt = new Regex(REGEX_COMMAND_LINE_STOPAT);
                Regex rgxCommandLineScheduleStartAt = new Regex(REGEX_COMMAND_LINE_STARTAT);
                Regex rgxCommandLineHideSystemTrayIcon = new Regex(REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON);

                #region Command Line Argument Parsing

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] != null)
                    {
                        Log.Write("Parsing command line argument at index " + i + " --> " + args[i]);
                    }

                    if (rgxCommandLineInitial.IsMatch(args[i]))
                    {
                        checkBoxInitialScreenshot.Checked = true;
                    }

                    if (rgxCommandLineLimit.IsMatch(args[i]))
                    {
                        int cmdLimit = Convert.ToInt32(rgxCommandLineLimit.Match(args[i]).Groups["Limit"].Value);

                        if (cmdLimit >= CAPTURE_LIMIT_MIN && cmdLimit <= CAPTURE_LIMIT_MAX)
                        {
                            numericUpDownCaptureLimit.Value = cmdLimit;
                            checkBoxCaptureLimit.Checked = true;
                        }
                    }

                    if (rgxCommandLineCaptureDelay.IsMatch(args[i]))
                    {
                        int hours = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Hours"].Value);
                        int minutes =
                            Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Minutes"].Value);
                        int seconds =
                            Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Seconds"].Value);
                        int milliseconds =
                            Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Milliseconds"].Value);

                        numericUpDownHoursInterval.Value = hours;
                        numericUpDownMinutesInterval.Value = minutes;
                        numericUpDownSecondsInterval.Value = seconds;
                        numericUpDownMillisecondsInterval.Value = milliseconds;
                    }

                    if (rgxCommandLineScheduleStartAt.IsMatch(args[i]))
                    {
                        int hours = Convert.ToInt32(rgxCommandLineScheduleStartAt.Match(args[i]).Groups["Hours"].Value);
                        int minutes =
                            Convert.ToInt32(rgxCommandLineScheduleStartAt.Match(args[i]).Groups["Minutes"].Value);
                        int seconds =
                            Convert.ToInt32(rgxCommandLineScheduleStartAt.Match(args[i]).Groups["Seconds"].Value);

                        dateTimePickerScheduleStartAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                            DateTime.Now.Day, hours, minutes, seconds);

                        checkBoxScheduleStartAt.Checked = true;
                    }

                    if (rgxCommandLineScheduleStopAt.IsMatch(args[i]))
                    {
                        int hours = Convert.ToInt32(rgxCommandLineScheduleStopAt.Match(args[i]).Groups["Hours"].Value);
                        int minutes =
                            Convert.ToInt32(rgxCommandLineScheduleStopAt.Match(args[i]).Groups["Minutes"].Value);
                        int seconds =
                            Convert.ToInt32(rgxCommandLineScheduleStopAt.Match(args[i]).Groups["Seconds"].Value);

                        dateTimePickerScheduleStopAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                            DateTime.Now.Day, hours, minutes, seconds);

                        checkBoxScheduleStopAt.Checked = true;
                    }

                    if (rgxCommandLineLock.IsMatch(args[i]) && textBoxPassphrase.Text.Length > 0)
                    {
                        checkBoxPassphraseLock.Checked = true;
                    }

                    if (rgxCommandLineHideSystemTrayIcon.IsMatch(args[i]))
                    {
                        toolStripMenuItemShowSystemTrayIcon.Checked = false;
                    }
                }

                #endregion Command Line Argument Parsing

                ScreenCapture.RunningFromCommandLine = true;

                InitializeThreads();

                StartScreenCapture();
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::ParseCommandLine", ex);
            }
        }

        /// <summary>
        /// Builds the sub-menus for the contextual menu that appears when the user right-clicks on the selected screenshot.
        /// </summary>
        private void BuildScreenshotPreviewContextualMenu()
        {
            contextMenuStripScreenshotPreview.Items.Clear();

            ToolStripMenuItem showScreenshotLocationToolStripItem = new ToolStripMenuItem("Show Screenshot Location");
            showScreenshotLocationToolStripItem.Click +=
                new EventHandler(Click_toolStripMenuItemShowScreenshotLocation);

            ToolStripMenuItem addNewEditorToolStripItem = new ToolStripMenuItem("Add New Editor ...");
            addNewEditorToolStripItem.Click += new EventHandler(Click_addEditorToolStripMenuItem);

            contextMenuStripScreenshotPreview.Items.Add(showScreenshotLocationToolStripItem);
            contextMenuStripScreenshotPreview.Items.Add(new ToolStripSeparator());
            contextMenuStripScreenshotPreview.Items.Add(addNewEditorToolStripItem);

            BuildEditorsList();
            BuildTriggersList();
            BuildRegionsList();
            BuildScreensList();
        }

        private void BuildEditorsList()
        {
            int xPosEditor = 5;
            int yPosEditor = 3;

            const int EDITOR_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int X_POS_EDITOR_ICON = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_IMAGE_WIDTH = 20;
            const int SMALL_IMAGE_HEIGHT = 20;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_EDITOR_TEXTBOX = 48;
            const int X_POS_EDITOR_BUTTON = 178;
            const int EDITOR_TEXTBOX_WIDTH = 125;
            const int Y_POS_EDITOR_INCREMENT = 23;
            const int EDITOR_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageEditors.Controls.Clear();

            // The button for adding a new Editor.
            Button buttonAddNewEditor = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosEditor, yPosEditor),
                Text = "Add New Editor ...",
                TabStop = false
            };
            buttonAddNewEditor.Click += new EventHandler(Click_addEditorToolStripMenuItem);
            tabPageEditors.Controls.Add(buttonAddNewEditor);

            // Move down and then add the "Remove Selected Editors" button.
            yPosEditor += 27;

            Button buttonRemoveSelectedEditors = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosEditor, yPosEditor),
                Text = "Remove Selected Editors",
                TabStop = false
            };
            buttonRemoveSelectedEditors.Click += new EventHandler(Click_removeSelectedEditors);
            tabPageEditors.Controls.Add(buttonRemoveSelectedEditors);

            // Move down a bit so we can start populating the Editors tab page with a list of Editors.
            yPosEditor += 28;

            foreach (Editor editor in formEditor.EditorCollection)
            {
                if (editor != null && File.Exists(editor.Application))
                {
                    // ****************** EDITORS LIST IN CONTEXTUAL MENU *************************
                    // Add the Editor to the screenshot preview contextual menu and to each "Edit"
                    // button at the top of the individual preview image.

                    contextMenuStripScreenshotPreview.Items.Add(editor.Name,
                        Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    // ****************************************************************************

                    // ****************** EDITORS LIST IN EDITORS TAB PAGE ************************
                    // Add the Editor to the list of Editors in the Editors tab page.

                    // Add a checkbox so that the user has the ability to remove the selected Editor.
                    CheckBox checkboxEditor = new CheckBox
                    {
                        Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                        Location = new Point(xPosEditor, yPosEditor),
                        Tag = editor,
                        TabStop = false
                    };
                    tabPageEditors.Controls.Add(checkboxEditor);

                    // Add an image showing the application icon of the Editor.
                    PictureBox pictureBoxEditor = new PictureBox
                    {
                        Size = new Size(SMALL_IMAGE_WIDTH, SMALL_IMAGE_HEIGHT),
                        Location = new Point(xPosEditor + X_POS_EDITOR_ICON, yPosEditor),
                        Image = Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    tabPageEditors.Controls.Add(pictureBoxEditor);

                    // Add a read-only text box showing the application name of the Editor.
                    TextBox textBoxEditor = new TextBox
                    {
                        Width = EDITOR_TEXTBOX_WIDTH,
                        Height = EDITOR_HEIGHT,
                        MaxLength = EDITOR_TEXTBOX_MAX_LENGTH,
                        Location = new Point(xPosEditor + X_POS_EDITOR_TEXTBOX, yPosEditor),
                        Text = editor.Name,
                        ReadOnly = true,
                        TabStop = false
                    };
                    tabPageEditors.Controls.Add(textBoxEditor);

                    // Add a button so that the user can change the Editor.
                    Button buttonChangeEditor = new Button
                    {
                        Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                        Location = new Point(xPosEditor + X_POS_EDITOR_BUTTON, yPosEditor),
                        Text = EDIT_BUTTON_TEXT,
                        Tag = editor,
                        TabStop = false
                    };
                    buttonChangeEditor.Click += new EventHandler(Click_buttonChangeEditor);
                    tabPageEditors.Controls.Add(buttonChangeEditor);

                    // Move down the Editors tab page so we're ready to loop around again and add the next Editor to it.
                    yPosEditor += Y_POS_EDITOR_INCREMENT;
                    // ****************************************************************************
                }
            }
        }

        private void BuildTriggersList()
        {
            int xPosTrigger = 5;
            int yPosTrigger = 3;

            const int TRIGGER_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_TRIGGER_TEXTBOX = 20;
            const int X_POS_TRIGGER_BUTTON = 178;
            const int TRIGGER_TEXTBOX_WIDTH = 153;
            const int Y_POS_TRIGGER_INCREMENT = 23;
            const int TRIGGER_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageTriggers.Controls.Clear();

            // The button for adding a new Trigger.
            Button buttonAddNewTrigger = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosTrigger, yPosTrigger),
                Text = "Add New Trigger ...",
                TabStop = false
            };
            buttonAddNewTrigger.Click += new EventHandler(Click_addTrigger);
            tabPageTriggers.Controls.Add(buttonAddNewTrigger);

            // Move down and then add the "Remove Selected Triggers" button.
            yPosTrigger += 27;

            Button buttonRemoveSelectedTriggers = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosTrigger, yPosTrigger),
                Text = "Remove Selected Triggers",
                TabStop = false
            };
            buttonRemoveSelectedTriggers.Click += new EventHandler(Click_removeSelectedTriggers);
            tabPageTriggers.Controls.Add(buttonRemoveSelectedTriggers);

            // Move down a bit so we can start populating the Triggers tab page with a list of Triggers.
            yPosTrigger += 28;

            foreach (Trigger trigger in formTrigger.TriggerCollection)
            {
                // Add a checkbox so that the user has the ability to remove the selected Trigger.
                CheckBox checkboxTrigger = new CheckBox
                {
                    Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                    Location = new Point(xPosTrigger, yPosTrigger),
                    Tag = trigger,
                    TabStop = false
                };
                tabPageTriggers.Controls.Add(checkboxTrigger);

                // Add a read-only text box showing the name of the Trigger.
                TextBox textBoxTrigger = new TextBox
                {
                    Width = TRIGGER_TEXTBOX_WIDTH,
                    Height = TRIGGER_HEIGHT,
                    MaxLength = TRIGGER_TEXTBOX_MAX_LENGTH,
                    Location = new Point(xPosTrigger + X_POS_TRIGGER_TEXTBOX, yPosTrigger),
                    Text = trigger.Name,
                    ReadOnly = true,
                    TabStop = false
                };
                tabPageTriggers.Controls.Add(textBoxTrigger);

                // Add a button so that the user can change the Trigger.
                Button buttonChangeTrigger = new Button
                {
                    Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                    Location = new Point(xPosTrigger + X_POS_TRIGGER_BUTTON, yPosTrigger),
                    Text = EDIT_BUTTON_TEXT,
                    Tag = trigger,
                    TabStop = false
                };
                buttonChangeTrigger.Click += new EventHandler(Click_buttonChangeTrigger);
                tabPageTriggers.Controls.Add(buttonChangeTrigger);

                // Move down the Triggers tab page so we're ready to loop around again and add the next Trigger to it.
                yPosTrigger += Y_POS_TRIGGER_INCREMENT;
            }
        }

        private void BuildScreensList()
        {
            int xPosScreen = 5;
            int yPosScreen = 3;

            const int SCREEN_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_SCREEN_TEXTBOX = 20;
            const int X_POS_SCREEN_BUTTON = 178;
            const int SCREEN_TEXTBOX_WIDTH = 153;
            const int Y_POS_SCREEN_INCREMENT = 23;
            const int SCREEN_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageScreens.Controls.Clear();

            // The button for adding a new Screen.
            Button buttonAddNewScreen = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosScreen, yPosScreen),
                Text = "Add New Screen ...",
                TabStop = false
            };
            buttonAddNewScreen.Click += new EventHandler(Click_addScreen);
            tabPageScreens.Controls.Add(buttonAddNewScreen);

            // Move down and then add the "Remove Selected Screens" button.
            yPosScreen += 27;

            Button buttonRemoveSelectedScreens = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosScreen, yPosScreen),
                Text = "Remove Selected Screens",
                TabStop = false
            };
            buttonRemoveSelectedScreens.Click += new EventHandler(Click_removeSelectedScreens);
            tabPageScreens.Controls.Add(buttonRemoveSelectedScreens);

            // Move down a bit so we can start populating the Screens tab page with a list of Screens.
            yPosScreen += 28;

            // Clear out the controls of the "Screens" tab control.
            tabControlScreens.Controls.Clear();

            foreach (Screen screen in formScreen.ScreenCollection)
            {
                // Add a checkbox so that the user has the ability to remove the selected Screen.
                CheckBox checkboxScreen = new CheckBox
                {
                    Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                    Location = new Point(xPosScreen, yPosScreen),
                    Tag = screen,
                    TabStop = false
                };
                tabPageScreens.Controls.Add(checkboxScreen);

                // Add a read-only text box showing the name of the Screen.
                TextBox textBoxScreen = new TextBox
                {
                    Width = SCREEN_TEXTBOX_WIDTH,
                    Height = SCREEN_HEIGHT,
                    MaxLength = SCREEN_TEXTBOX_MAX_LENGTH,
                    Location = new Point(xPosScreen + X_POS_SCREEN_TEXTBOX, yPosScreen),
                    Text = screen.Name,
                    ReadOnly = true,
                    TabStop = false
                };
                tabPageScreens.Controls.Add(textBoxScreen);

                // Add a button so that the user can change the Screen.
                Button buttonChangeScreen = new Button
                {
                    Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                    Location = new Point(xPosScreen + X_POS_SCREEN_BUTTON, yPosScreen),
                    Text = EDIT_BUTTON_TEXT,
                    Tag = screen,
                    TabStop = false
                };
                buttonChangeScreen.Click += new EventHandler(Click_buttonChangeScreen);
                tabPageScreens.Controls.Add(buttonChangeScreen);

                // Setup the "Screen" tab page for the current screen with all of its associated controls.

                ToolStripSplitButton toolStripSplitButtonScreen = new ToolStripSplitButton
                {
                    Text = "Edit",
                    Image = Resources.edit
                };

                toolStripSplitButtonScreen.DropDown.Items.Add("Add New Editor ...", null,
                    Click_addEditorToolStripMenuItem);
                foreach (Editor editor in formEditor.EditorCollection)
                {
                    if (editor != null && File.Exists(editor.Application))
                    {
                        toolStripSplitButtonScreen.DropDown.Items.Add(editor.Name,
                            Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    }
                }

                ToolStrip toolStripScreen = new ToolStrip
                {
                    GripStyle = ToolStripGripStyle.Hidden
                };

                toolStripScreen.Items.Add(toolStripSplitButtonScreen);

                PictureBox pictureBoxScreen = new PictureBox
                {
                    Name = screen.Name,
                    BackColor = Color.Black,
                    Location = new Point(4, 29),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ContextMenuStrip = contextMenuStripScreenshotPreview,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left,
                };

                TabPage tabPageScreen = new TabPage
                {
                    Name = screen.Name,
                    Text = screen.Name
                };

                tabPageScreen.Controls.Add(toolStripScreen);
                tabPageScreen.Controls.Add(pictureBoxScreen);

                pictureBoxScreen.Width = (tabPageScreen.Width - 10);
                pictureBoxScreen.Height = (tabPageScreen.Height - 30);

                tabControlScreens.Controls.Add(tabPageScreen);

                // Move down the Screens tab page so we're ready to loop around again and add the next Screen to it.
                yPosScreen += Y_POS_SCREEN_INCREMENT;
            }
        }

        private void BuildRegionsList()
        {
            int xPosRegion = 5;
            int yPosRegion = 3;

            const int REGION_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_REGION_TEXTBOX = 20;
            const int X_POS_REGION_BUTTON = 178;
            const int REGION_TEXTBOX_WIDTH = 153;
            const int Y_POS_REGION_INCREMENT = 23;
            const int REGION_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageRegions.Controls.Clear();

            // The button for adding a new Region.
            Button buttonAddNewRegion = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosRegion, yPosRegion),
                Text = "Add New Region ...",
                TabStop = false
            };
            buttonAddNewRegion.Click += new EventHandler(Click_addRegion);
            tabPageRegions.Controls.Add(buttonAddNewRegion);

            // Move down and then add the "Remove Selected Regions" button.
            yPosRegion += 27;

            Button buttonRemoveSelectedRegions = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosRegion, yPosRegion),
                Text = "Remove Selected Regions",
                TabStop = false
            };
            buttonRemoveSelectedRegions.Click += new EventHandler(Click_removeSelectedRegions);
            tabPageRegions.Controls.Add(buttonRemoveSelectedRegions);

            // Move down a bit so we can start populating the Regions tab page with a list of Regions.
            yPosRegion += 28;

            foreach (Region region in formRegion.RegionCollection)
            {
                // Add a checkbox so that the user has the ability to remove the selected Region.
                CheckBox checkboxRegion = new CheckBox
                {
                    Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                    Location = new Point(xPosRegion, yPosRegion),
                    Tag = region,
                    TabStop = false
                };
                tabPageRegions.Controls.Add(checkboxRegion);

                // Add a read-only text box showing the name of the Region.
                TextBox textBoxRegion = new TextBox
                {
                    Width = REGION_TEXTBOX_WIDTH,
                    Height = REGION_HEIGHT,
                    MaxLength = REGION_TEXTBOX_MAX_LENGTH,
                    Location = new Point(xPosRegion + X_POS_REGION_TEXTBOX, yPosRegion),
                    Text = region.Name,
                    ReadOnly = true,
                    TabStop = false
                };
                tabPageRegions.Controls.Add(textBoxRegion);

                // Add a button so that the user can change the Region.
                Button buttonChangeRegion = new Button
                {
                    Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                    Location = new Point(xPosRegion + X_POS_REGION_BUTTON, yPosRegion),
                    Text = EDIT_BUTTON_TEXT,
                    Tag = region,
                    TabStop = false
                };
                buttonChangeRegion.Click += new EventHandler(Click_buttonChangeRegion);
                tabPageRegions.Controls.Add(buttonChangeRegion);

                // Move down the Regions tab page so we're ready to loop around again and add the next Region to it.
                yPosRegion += Y_POS_REGION_INCREMENT;
            }
        }

        #region Click Event Handlers

        #region Editor

        /// <summary>
        /// Shows the "Add Editor" window to enable the user to add a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addEditorToolStripMenuItem(object sender, EventArgs e)
        {
            formEditor.EditorObject = null;

            formEditor.ShowDialog(this);

            if (formEditor.DialogResult == DialogResult.OK)
            {
                BuildScreenshotPreviewContextualMenu();

                formEditor.EditorCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Editors from the Editors tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedEditors(object sender, EventArgs e)
        {
            int countBeforeRemoval = formEditor.EditorCollection.Count;

            foreach (Control control in tabPageEditors.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Editor editor = formEditor.EditorCollection.Get((Editor)checkBox.Tag);
                        formEditor.EditorCollection.Remove(editor);
                    }
                }
            }

            if (countBeforeRemoval > formEditor.EditorCollection.Count)
            {
                BuildScreenshotPreviewContextualMenu();

                formEditor.EditorCollection.Save();
            }
        }

        /// <summary>
        /// Runs the chosen image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_runEditor(object sender, EventArgs e)
        {
            if (listBoxSlides.SelectedIndex > -1)
            {
                Editor editor = formEditor.EditorCollection.GetByName(sender.ToString());
                RunEditor(editor);
            }
        }

        /// <summary>
        /// Shows the "Change Editor" window to enable the user to edit a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeEditor(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formEditor.EditorObject = (Editor)buttonSelected.Tag;

                formEditor.ShowDialog(this);

                if (formEditor.DialogResult == DialogResult.OK)
                {
                    BuildScreenshotPreviewContextualMenu();

                    formEditor.EditorCollection.Save();
                }
            }
        }

        /// <summary>
        /// Executes a chosen image editor from the interface.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        private void RunEditor(Editor editor)
        {
            if (editor != null)
            {
                RunEditor(editor, ScreenshotCollection.GetBySlide(tabControlScreens.SelectedTab.Name, Slideshow.SelectedSlide));
            }
        }

        /// <summary>
        /// Executes a chosen image editor from a Trigger.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        private void RunEditor(Editor editor, TriggerActionType triggerActionType)
        {
            if (editor != null && triggerActionType == TriggerActionType.RunEditor && ScreenCapture.Running)
            {
                RunEditor(editor,
                    ScreenshotCollection.GetBySlide(tabControlScreens.SelectedTab.Name, ScreenshotCollection.GetByIndex(ScreenshotCollection.Count - 1)
                        .Slide));
            }
        }

        /// <summary>
        /// Runs the editor using the specified screenshot.
        /// </summary>
        /// <param name="editor">The editor to use.</param>
        /// <param name="screenshot">The screenshot to use.</param>
        private void RunEditor(Editor editor, Screenshot screenshot)
        {
            // Execute the chosen image editor. If the %screenshot% argument happens to be included
            // then we'll use that argument as the screenshot file path when executing the image editor.
            if (editor != null && (screenshot != null && !string.IsNullOrEmpty(screenshot.Path) &&
                                   File.Exists(screenshot.Path)))
            {
                Process.Start(editor.Application,
                    editor.Arguments.Replace("%screenshot%", "\"" + screenshot.Path + "\""));
            }
        }

        #endregion Editor

        #region Trigger

        /// <summary>
        /// Shows the "Add Trigger" window to enable the user to add a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addTrigger(object sender, EventArgs e)
        {
            formTrigger.TriggerObject = null;

            formTrigger.EditorCollection = formEditor.EditorCollection;

            formTrigger.ShowDialog(this);

            if (formTrigger.DialogResult == DialogResult.OK)
            {
                BuildScreenshotPreviewContextualMenu();

                formTrigger.TriggerCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Triggers from the Triggers tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedTriggers(object sender, EventArgs e)
        {
            int countBeforeRemoval = formTrigger.TriggerCollection.Count;

            foreach (Control control in tabPageTriggers.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Trigger trigger = formTrigger.TriggerCollection.Get((Trigger)checkBox.Tag);
                        formTrigger.TriggerCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > formTrigger.TriggerCollection.Count)
            {
                BuildScreenshotPreviewContextualMenu();

                formTrigger.TriggerCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Trigger" window to enable the user to edit a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeTrigger(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formTrigger.TriggerObject = (Trigger)buttonSelected.Tag;

                formTrigger.EditorCollection = formEditor.EditorCollection;

                formTrigger.ShowDialog(this);

                if (formTrigger.DialogResult == DialogResult.OK)
                {
                    BuildScreenshotPreviewContextualMenu();

                    formTrigger.TriggerCollection.Save();
                }
            }
        }

        #endregion Trigger

        #region Region

        /// <summary>
        /// Shows the "Add Region" window to enable the user to add a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addRegion(object sender, EventArgs e)
        {
            formRegion.RegionObject = null;
            formRegion.ImageFormatCollection = _imageFormatCollection;

            formRegion.ShowDialog(this);

            if (formRegion.DialogResult == DialogResult.OK)
            {
                BuildScreenshotPreviewContextualMenu();

                formRegion.RegionCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Regions from the Regions tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedRegions(object sender, EventArgs e)
        {
            int countBeforeRemoval = formRegion.RegionCollection.Count;

            foreach (Control control in tabPageRegions.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Region region = formRegion.RegionCollection.Get((Region)checkBox.Tag);
                        formRegion.RegionCollection.Remove(region);
                    }
                }
            }

            if (countBeforeRemoval > formRegion.RegionCollection.Count)
            {
                BuildScreenshotPreviewContextualMenu();

                formRegion.RegionCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Region" window to enable the user to edit a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeRegion(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formRegion.RegionObject = (Region)buttonSelected.Tag;
                formRegion.ImageFormatCollection = _imageFormatCollection;

                formRegion.ShowDialog(this);

                if (formRegion.DialogResult == DialogResult.OK)
                {
                    BuildScreenshotPreviewContextualMenu();

                    formRegion.RegionCollection.Save();
                }
            }
        }

        #endregion Region

        #region Screen

        /// <summary>
        /// Shows the "Add Screen" window to enable the user to add a chosen Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addScreen(object sender, EventArgs e)
        {
            formScreen.ScreenObject = null;
            formScreen.ImageFormatCollection = _imageFormatCollection;

            formScreen.ShowDialog(this);

            if (formScreen.DialogResult == DialogResult.OK)
            {
                BuildScreenshotPreviewContextualMenu();

                formScreen.ScreenCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Screens from the Screens tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedScreens(object sender, EventArgs e)
        {
            int countBeforeRemoval = formScreen.ScreenCollection.Count;

            foreach (Control control in tabPageScreens.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Screen screen = formScreen.ScreenCollection.Get((Screen)checkBox.Tag);
                        formScreen.ScreenCollection.Remove(screen);
                    }
                }
            }

            if (countBeforeRemoval > formScreen.ScreenCollection.Count)
            {
                BuildScreenshotPreviewContextualMenu();

                formScreen.ScreenCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Screen" window to enable the user to edit a chosen Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeScreen(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formScreen.ScreenObject = (Screen)buttonSelected.Tag;
                formScreen.ImageFormatCollection = _imageFormatCollection;

                formScreen.ShowDialog(this);

                if (formScreen.DialogResult == DialogResult.OK)
                {
                    BuildScreenshotPreviewContextualMenu();

                    formScreen.ScreenCollection.Save();
                }
            }
        }

        #endregion Screen

        #region Passphrase

        /// <summary>
        /// Sets the passphrase chosen by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonSetPassphrase(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                Settings.User.GetByKey("Passphrase", defaultValue: string.Empty).Value = textBoxPassphrase.Text;
                SaveSettings();

                textBoxPassphrase.ReadOnly = true;
                buttonSetPassphrase.Enabled = false;

                checkBoxPassphraseLock.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the passphrase chosen by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonClearPassphrase(object sender, EventArgs e)
        {
            textBoxPassphrase.Clear();
            textBoxPassphrase.ReadOnly = false;

            checkBoxPassphraseLock.Enabled = false;
            checkBoxPassphraseLock.Checked = false;

            Settings.User.GetByKey("LockScreenCaptureSession", defaultValue: false).Value = false;
            Settings.User.GetByKey("Passphrase", defaultValue: string.Empty).Value = string.Empty;
            SaveSettings();

            textBoxPassphrase.Focus();
        }

        #endregion Passphrase

        #endregion Click Event Handlers

        /// <summary>
        /// Determines which screen tab is selected (All Screens, Screen 1, Screen 2, Screen 3, Screen 4, or Active Window).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_tabControlScreens(object sender, EventArgs e)
        {
            Slideshow.SelectedScreen = (tabControlScreens.SelectedIndex + 1);
        }

        /// <summary>
        /// The timer for taking screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScreenCapture(object sender, EventArgs e)
        {
            if (!timerScreenCapture.Enabled)
            {
                StopScreenCapture();
            }

            if (ScreenCapture.Limit >= ScreenCapture.CAPTURE_LIMIT_MIN &&
                ScreenCapture.Limit <= ScreenCapture.CAPTURE_LIMIT_MAX)
            {
                if (ScreenCapture.Count < ScreenCapture.Limit)
                {
                    TakeScreenshot();
                }

                if (ScreenCapture.Count == ScreenCapture.Limit)
                {
                    RunTriggersOfConditionType(TriggerConditionType.LimitReached);
                }
            }
            else
            {
                TakeScreenshot();
            }
        }

        /// <summary>
        /// Takes a screenshot of each available region and screen.
        /// </summary>
        private void TakeScreenshot()
        {
            int count = 0;

            ScreenCapture.DateTimePreviousScreenshot = DateTime.Now;

            ScreenCapture.Count++;

            RunTriggersOfConditionType(TriggerConditionType.ScreenshotTaken);

            RunRegionCaptures();

            RunScreenCaptures();
        }

        /// <summary>
        /// Checks the capture limit when the checkbox is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_checkBoxCaptureLimit(object sender, EventArgs e)
        {
            CaptureLimitCheck();
        }

        /// <summary>
        /// Checks the capture limit.
        /// </summary>
        private void CaptureLimitCheck()
        {
            if (checkBoxCaptureLimit.Checked)
            {
                numericUpDownCaptureLimit.Enabled = true;

                ScreenCapture.Count = 0;
                ScreenCapture.Limit = (int)numericUpDownCaptureLimit.Value;
            }
            else
            {
                numericUpDownCaptureLimit.Enabled = false;
            }
        }

        /// <summary>
        /// Enables the checkboxes for the days that could be selected when setting up a scheduled screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_checkBoxScheduleOnTheseDays(object sender, EventArgs e)
        {
            if (checkBoxScheduleOnTheseDays.Checked)
            {
                checkBoxSaturday.Enabled = true;
                checkBoxSunday.Enabled = true;
                checkBoxMonday.Enabled = true;
                checkBoxTuesday.Enabled = true;
                checkBoxWednesday.Enabled = true;
                checkBoxThursday.Enabled = true;
                checkBoxFriday.Enabled = true;
            }
            else
            {
                checkBoxSaturday.Enabled = false;
                checkBoxSunday.Enabled = false;
                checkBoxMonday.Enabled = false;
                checkBoxTuesday.Enabled = false;
                checkBoxWednesday.Enabled = false;
                checkBoxThursday.Enabled = false;
                checkBoxFriday.Enabled = false;
            }
        }

        /// <summary>
        /// The timer used for starting scheduled screen capture sessions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCaptureStart(object sender, EventArgs e)
        {
            if (checkBoxScheduleStartAt.Checked)
            {
                if (checkBoxScheduleOnTheseDays.Checked)
                {
                    if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
                        ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
                         (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute)))
                    {
                        StartScreenCapture();
                    }
                }
                else
                {
                    if ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute))
                    {
                        StartScreenCapture();
                    }
                }
            }
        }

        /// <summary>
        /// The timer used for stopping scheduled screen capture sessions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCaptureStop(object sender, EventArgs e)
        {
            if (checkBoxScheduleStopAt.Checked)
            {
                if (checkBoxScheduleOnTheseDays.Checked)
                {
                    if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
                        ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
                         (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute)))
                    {
                        StopScreenCapture();
                    }
                }
                else
                {
                    if ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute))
                    {
                        StopScreenCapture();
                    }
                }
            }
        }

        /// <summary>
        /// Enables the "Play Slideshow" control if it should be enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueChanged_numericUpDownSlideshowDelay(object sender, EventArgs e)
        {
            EnablePlaySlideshow();
        }

        /// Show or hide the system tray icon depending on the option selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_toolStripMenuItemShowSystemTrayIcon(object sender, EventArgs e)
        {
            notifyIcon.Visible = toolStripMenuItemShowSystemTrayIcon.Checked;
        }

        /// <summary>
        /// Determine if we need to lock the screen capture session or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_checkBoxPassphraseLock(object sender, EventArgs e)
        {
            if (checkBoxPassphraseLock.Checked)
            {
                ScreenCapture.LockScreenCaptureSession = true;
            }
            else
            {
                ScreenCapture.LockScreenCaptureSession = false;
            }
        }

        /// <summary>
        /// Determines when we enable the "Set" button or disable the "Lock" checkbox (and "Set" button) for passphrase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextChanged_textBoxPassphrase(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                buttonSetPassphrase.Enabled = true;
            }
            else
            {
                checkBoxPassphraseLock.Enabled = false;
                checkBoxPassphraseLock.Checked = false;

                buttonSetPassphrase.Enabled = false;
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            foreach (Trigger trigger in formTrigger.TriggerCollection)
            {
                if (trigger.ConditionType == conditionType)
                {
                    // These actions need to directly correspond with the TriggerActionType class.
                    switch (trigger.ActionType)
                    {
                        case TriggerActionType.ExitApplication:
                            ExitApplication();
                            break;

                        case TriggerActionType.HideInterface:
                            HideInterface();
                            break;

                        case TriggerActionType.PlaySlideshow:
                            PlaySlideshow();
                            break;

                        case TriggerActionType.RunEditor:
                            Editor editor = formEditor.EditorCollection.GetByName(trigger.Editor);
                            RunEditor(editor, TriggerActionType.RunEditor);
                            break;

                        case TriggerActionType.ShowInterface:
                            ShowInterface();
                            break;

                        case TriggerActionType.StartScreenCapture:
                            StartScreenCapture();
                            break;

                        case TriggerActionType.StopScreenCapture:
                            StopScreenCapture();
                            break;
                    }
                }
            }
        }

        private void RunRegionCaptures()
        {
            foreach (Region region in formRegion.RegionCollection)
            {
                ScreenCapture.TakeScreenshot(
                    name: region.Name,
                    path: region.Folder + MacroParser.ParseTags(region.Name, region.Macro, region.Format),
                    format: region.Format,
                    jpegQuality: region.JpegQuality,
                    resolutionRatio: region.ResolutionRatio,
                    mouse: region.Mouse,
                    x: region.X,
                    y: region.Y,
                    width: region.Width,
                    height: region.Height,
                    addToScreenshotCollection: false // Since this is a region capture we won't be including it in the slideshow so don't add it to the screenshot collection
                );
            }
        }

        private void RunScreenCaptures()
        {
            foreach (Screen screen in formScreen.ScreenCollection)
            {
                ScreenCapture.TakeScreenshot(
                    name: screen.Name,
                    path: screen.Folder + MacroParser.ParseTags(screen.Name, screen.Macro, screen.Format),
                    format: screen.Format,
                    jpegQuality: screen.JpegQuality,
                    resolutionRatio: screen.ResolutionRatio,
                    mouse: screen.Mouse,
                    x: formScreen.ScreenDictionary[screen.Component].Bounds.X,
                    y: formScreen.ScreenDictionary[screen.Component].Bounds.Y,
                    width: formScreen.ScreenDictionary[screen.Component].Bounds.Width,
                    height: formScreen.ScreenDictionary[screen.Component].Bounds.Height,
                    addToScreenshotCollection: true
                );
            }
        }

        /// <summary>
        /// Displays the remaining time for when the next screenshot will be taken
        /// when the mouse pointer moves over the system tray icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (ScreenCapture.Running)
            {
                int remainingHours = ScreenCapture.TimeRemainingForNextScreenshot.Hours;
                int remainingMinutes = ScreenCapture.TimeRemainingForNextScreenshot.Minutes;
                int remainingSeconds = ScreenCapture.TimeRemainingForNextScreenshot.Seconds;
                int remainingMilliseconds = ScreenCapture.TimeRemainingForNextScreenshot.Milliseconds;

                string remainingHoursStr = (remainingHours > 0
                    ? remainingHours.ToString() + " hour" + (remainingHours > 1 ? "s" : string.Empty) + ", "
                    : string.Empty);
                string remainingMinutesStr = (remainingMinutes > 0
                    ? remainingMinutes.ToString() + " minute" + (remainingMinutes > 1 ? "s" : string.Empty) + ", "
                    : string.Empty);

                string remainingTimeStr = string.Empty;

                if (remainingSeconds < 1)
                {
                    remainingTimeStr = "0." + remainingMilliseconds.ToString() + " milliseconds";
                }
                else
                {
                    remainingTimeStr = remainingHoursStr + remainingMinutesStr + remainingSeconds.ToString() +
                                       " second" + (remainingSeconds > 1 ? "s" : string.Empty) + " at " +
                                       ScreenCapture.DateTimeNextScreenshot.ToLongTimeString();
                }

                notifyIcon.Text = "Next capture in " + remainingTimeStr;
            }
            else
            {
                notifyIcon.Text = Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value +
                                  " (" + Settings.Application
                                      .GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value + ")";
            }
        }
    }
}