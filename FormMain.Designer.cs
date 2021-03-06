﻿namespace AutoScreenCapture
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButtonOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItemShowSystemTrayIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlScreens = new System.Windows.Forms.TabControl();
            this.contextMenuStripScreenshotPreview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBoxSlides = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorAbout = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemShowInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHideInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorInterface = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemStartCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStopCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorCapture = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSlideshow = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFirstSlide = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreviousSlide = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPlaySlideshow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextSlide = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLastSlide = new System.Windows.Forms.ToolStripButton();
            this.tabControlModules = new System.Windows.Forms.TabControl();
            this.tabPageScreens = new System.Windows.Forms.TabPage();
            this.tabPageRegions = new System.Windows.Forms.TabPage();
            this.tabPageDelay = new System.Windows.Forms.TabPage();
            this.groupBoxCaptureDelay = new System.Windows.Forms.GroupBox();
            this.labelLimit = new System.Windows.Forms.Label();
            this.checkBoxInitialScreenshot = new System.Windows.Forms.CheckBox();
            this.numericUpDownCaptureLimit = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCaptureLimit = new System.Windows.Forms.CheckBox();
            this.labelMillisecondsInterval = new System.Windows.Forms.Label();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelSecondsInterval = new System.Windows.Forms.Label();
            this.labelMinutesInterval = new System.Windows.Forms.Label();
            this.labelHoursInterval = new System.Windows.Forms.Label();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
            this.checkBoxScheduleOnTheseDays = new System.Windows.Forms.CheckBox();
            this.checkBoxFriday = new System.Windows.Forms.CheckBox();
            this.checkBoxThursday = new System.Windows.Forms.CheckBox();
            this.checkBoxWednesday = new System.Windows.Forms.CheckBox();
            this.checkBoxTuesday = new System.Windows.Forms.CheckBox();
            this.checkBoxMonday = new System.Windows.Forms.CheckBox();
            this.checkBoxSunday = new System.Windows.Forms.CheckBox();
            this.checkBoxSaturday = new System.Windows.Forms.CheckBox();
            this.dateTimePickerScheduleStopAt = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerScheduleStartAt = new System.Windows.Forms.DateTimePicker();
            this.checkBoxScheduleStopAt = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleStartAt = new System.Windows.Forms.CheckBox();
            this.tabPageSlideshow = new System.Windows.Forms.TabPage();
            this.groupBoxSlideshowDelay = new System.Windows.Forms.GroupBox();
            this.labelSlideshowDelayMilliseconds = new System.Windows.Forms.Label();
            this.numericUpDownSlideshowDelayMilliseconds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSlideSkip = new System.Windows.Forms.NumericUpDown();
            this.labelSlideshowDelaySeconds = new System.Windows.Forms.Label();
            this.labelSlideshowDelayMinutes = new System.Windows.Forms.Label();
            this.checkBoxSlideSkip = new System.Windows.Forms.CheckBox();
            this.labelSlideshowDelayHours = new System.Windows.Forms.Label();
            this.numericUpDownSlideshowDelaySeconds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSlideshowDelayMinutes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSlideshowDelayHours = new System.Windows.Forms.NumericUpDown();
            this.tabPageEditors = new System.Windows.Forms.TabPage();
            this.tabPageTriggers = new System.Windows.Forms.TabPage();
            this.tabPageSecurity = new System.Windows.Forms.TabPage();
            this.groupBoxSecurity = new System.Windows.Forms.GroupBox();
            this.checkBoxPassphraseLock = new System.Windows.Forms.CheckBox();
            this.buttonClearPassphrase = new System.Windows.Forms.Button();
            this.labelPasswordDescription = new System.Windows.Forms.Label();
            this.buttonSetPassphrase = new System.Windows.Forms.Button();
            this.textBoxPassphrase = new System.Windows.Forms.TextBox();
            this.toolStripScreenCapture = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStartCapture = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStopCapture = new System.Windows.Forms.ToolStripButton();
            this.timerScheduledCaptureStart = new System.Windows.Forms.Timer(this.components);
            this.timerScheduledCaptureStop = new System.Windows.Forms.Timer(this.components);
            this.timerScreenCapture = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.contextMenuStripSystemTrayIcon.SuspendLayout();
            this.toolStripSlideshow.SuspendLayout();
            this.tabControlModules.SuspendLayout();
            this.tabPageDelay.SuspendLayout();
            this.groupBoxCaptureDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.tabPageSchedule.SuspendLayout();
            this.groupBoxSchedule.SuspendLayout();
            this.tabPageSlideshow.SuspendLayout();
            this.groupBoxSlideshowDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMilliseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelaySeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayHours)).BeginInit();
            this.tabPageSecurity.SuspendLayout();
            this.groupBoxSecurity.SuspendLayout();
            this.toolStripScreenCapture.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(0, 1);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ShowWeekNumbers = true;
            this.monthCalendar.TabIndex = 0;
            this.monthCalendar.TabStop = false;
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.DateSelected_monthCalendar);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonOptions});
            this.statusStrip.Location = new System.Drawing.Point(0, 466);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(851, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripDropDownButtonOptions
            // 
            this.toolStripDropDownButtonOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShowSystemTrayIcon});
            this.toolStripDropDownButtonOptions.Image = global::AutoScreenCapture.Properties.Resources.options;
            this.toolStripDropDownButtonOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonOptions.Name = "toolStripDropDownButtonOptions";
            this.toolStripDropDownButtonOptions.Size = new System.Drawing.Size(78, 20);
            this.toolStripDropDownButtonOptions.Text = "Options";
            // 
            // toolStripMenuItemShowSystemTrayIcon
            // 
            this.toolStripMenuItemShowSystemTrayIcon.CheckOnClick = true;
            this.toolStripMenuItemShowSystemTrayIcon.Name = "toolStripMenuItemShowSystemTrayIcon";
            this.toolStripMenuItemShowSystemTrayIcon.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItemShowSystemTrayIcon.Text = "Show the system tray icon";
            this.toolStripMenuItemShowSystemTrayIcon.CheckedChanged += new System.EventHandler(this.CheckedChanged_toolStripMenuItemShowSystemTrayIcon);
            this.toolStripMenuItemShowSystemTrayIcon.Click += new System.EventHandler(this.SaveSettings);
            // 
            // tabControlScreens
            // 
            this.tabControlScreens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlScreens.Location = new System.Drawing.Point(251, 1);
            this.tabControlScreens.Name = "tabControlScreens";
            this.tabControlScreens.SelectedIndex = 0;
            this.tabControlScreens.Size = new System.Drawing.Size(600, 407);
            this.tabControlScreens.TabIndex = 4;
            this.tabControlScreens.TabStop = false;
            this.tabControlScreens.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged_tabControlScreens);
            // 
            // contextMenuStripScreenshotPreview
            // 
            this.contextMenuStripScreenshotPreview.Name = "contextMenuStripScreenshotPreview";
            this.contextMenuStripScreenshotPreview.Size = new System.Drawing.Size(61, 4);
            // 
            // listBoxSlides
            // 
            this.listBoxSlides.FormattingEnabled = true;
            this.listBoxSlides.IntegralHeight = false;
            this.listBoxSlides.Location = new System.Drawing.Point(6, 3);
            this.listBoxSlides.MaximumSize = new System.Drawing.Size(204, 123);
            this.listBoxSlides.MinimumSize = new System.Drawing.Size(204, 123);
            this.listBoxSlides.Name = "listBoxSlides";
            this.listBoxSlides.ScrollAlwaysVisible = true;
            this.listBoxSlides.Size = new System.Drawing.Size(204, 123);
            this.listBoxSlides.Sorted = true;
            this.listBoxSlides.TabIndex = 6;
            this.listBoxSlides.TabStop = false;
            this.listBoxSlides.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged_listBoxSlides);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTrayIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseMove);
            // 
            // contextMenuStripSystemTrayIcon
            // 
            this.contextMenuStripSystemTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout,
            this.toolStripSeparatorAbout,
            this.toolStripMenuItemShowInterface,
            this.toolStripMenuItemHideInterface,
            this.toolStripSeparatorInterface,
            this.toolStripMenuItemStartCapture,
            this.toolStripMenuItemStopCapture,
            this.toolStripSeparatorCapture,
            this.toolStripMenuItemExit});
            this.contextMenuStripSystemTrayIcon.Name = "contextMenuStrip";
            this.contextMenuStripSystemTrayIcon.Size = new System.Drawing.Size(153, 154);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemAbout.Text = "About ...";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.Click_toolStripMenuItemAbout);
            // 
            // toolStripSeparatorAbout
            // 
            this.toolStripSeparatorAbout.Name = "toolStripSeparatorAbout";
            this.toolStripSeparatorAbout.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItemShowInterface
            // 
            this.toolStripMenuItemShowInterface.Enabled = false;
            this.toolStripMenuItemShowInterface.Name = "toolStripMenuItemShowInterface";
            this.toolStripMenuItemShowInterface.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemShowInterface.Text = "Show Interface";
            this.toolStripMenuItemShowInterface.Click += new System.EventHandler(this.Click_toolStripMenuItemShowInterface);
            // 
            // toolStripMenuItemHideInterface
            // 
            this.toolStripMenuItemHideInterface.Enabled = false;
            this.toolStripMenuItemHideInterface.Name = "toolStripMenuItemHideInterface";
            this.toolStripMenuItemHideInterface.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemHideInterface.Text = "Hide Interface";
            this.toolStripMenuItemHideInterface.Click += new System.EventHandler(this.Click_toolStripMenuItemHideInterface);
            // 
            // toolStripSeparatorInterface
            // 
            this.toolStripSeparatorInterface.Name = "toolStripSeparatorInterface";
            this.toolStripSeparatorInterface.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItemStartCapture
            // 
            this.toolStripMenuItemStartCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemStartCapture.Name = "toolStripMenuItemStartCapture";
            this.toolStripMenuItemStartCapture.ShowShortcutKeys = false;
            this.toolStripMenuItemStartCapture.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemStartCapture.Text = "Start Capture";
            // 
            // toolStripMenuItemStopCapture
            // 
            this.toolStripMenuItemStopCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemStopCapture.Enabled = false;
            this.toolStripMenuItemStopCapture.Name = "toolStripMenuItemStopCapture";
            this.toolStripMenuItemStopCapture.ShowShortcutKeys = false;
            this.toolStripMenuItemStopCapture.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemStopCapture.Text = "Stop Capture";
            this.toolStripMenuItemStopCapture.Click += new System.EventHandler(this.Click_toolStripMenuItemStopScreenCapture);
            // 
            // toolStripSeparatorCapture
            // 
            this.toolStripSeparatorCapture.Name = "toolStripSeparatorCapture";
            this.toolStripSeparatorCapture.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.ShowShortcutKeys = false;
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.Click_toolStripMenuItemExit);
            // 
            // toolStripSlideshow
            // 
            this.toolStripSlideshow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStripSlideshow.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripSlideshow.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripSlideshow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFirstSlide,
            this.toolStripButtonPreviousSlide,
            this.toolStripButtonPlaySlideshow,
            this.toolStripButtonNextSlide,
            this.toolStripButtonLastSlide});
            this.toolStripSlideshow.Location = new System.Drawing.Point(4, 441);
            this.toolStripSlideshow.Name = "toolStripSlideshow";
            this.toolStripSlideshow.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripSlideshow.Size = new System.Drawing.Size(118, 25);
            this.toolStripSlideshow.TabIndex = 11;
            // 
            // toolStripButtonFirstSlide
            // 
            this.toolStripButtonFirstSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFirstSlide.Enabled = false;
            this.toolStripButtonFirstSlide.Image = global::AutoScreenCapture.Properties.Resources.player_start;
            this.toolStripButtonFirstSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirstSlide.Name = "toolStripButtonFirstSlide";
            this.toolStripButtonFirstSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFirstSlide.Click += new System.EventHandler(this.Click_toolStripButtonFirstSlide);
            // 
            // toolStripButtonPreviousSlide
            // 
            this.toolStripButtonPreviousSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreviousSlide.Enabled = false;
            this.toolStripButtonPreviousSlide.Image = global::AutoScreenCapture.Properties.Resources.player_rew;
            this.toolStripButtonPreviousSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviousSlide.Name = "toolStripButtonPreviousSlide";
            this.toolStripButtonPreviousSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPreviousSlide.Click += new System.EventHandler(this.Click_toolStripButtonPreviousSlide);
            // 
            // toolStripButtonPlaySlideshow
            // 
            this.toolStripButtonPlaySlideshow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlaySlideshow.Enabled = false;
            this.toolStripButtonPlaySlideshow.Image = global::AutoScreenCapture.Properties.Resources.player_play;
            this.toolStripButtonPlaySlideshow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlaySlideshow.Name = "toolStripButtonPlaySlideshow";
            this.toolStripButtonPlaySlideshow.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPlaySlideshow.Click += new System.EventHandler(this.Click_toolStripButtonPlaySlideshow);
            // 
            // toolStripButtonNextSlide
            // 
            this.toolStripButtonNextSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextSlide.Enabled = false;
            this.toolStripButtonNextSlide.Image = global::AutoScreenCapture.Properties.Resources.player_fwd;
            this.toolStripButtonNextSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextSlide.Name = "toolStripButtonNextSlide";
            this.toolStripButtonNextSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNextSlide.Click += new System.EventHandler(this.Click_toolStripButtonNextSlide);
            // 
            // toolStripButtonLastSlide
            // 
            this.toolStripButtonLastSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLastSlide.Enabled = false;
            this.toolStripButtonLastSlide.Image = global::AutoScreenCapture.Properties.Resources.player_end;
            this.toolStripButtonLastSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLastSlide.Name = "toolStripButtonLastSlide";
            this.toolStripButtonLastSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLastSlide.Click += new System.EventHandler(this.Click_toolStripButtonLastSlide);
            // 
            // tabControlModules
            // 
            this.tabControlModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlModules.Controls.Add(this.tabPageScreens);
            this.tabControlModules.Controls.Add(this.tabPageRegions);
            this.tabControlModules.Controls.Add(this.tabPageDelay);
            this.tabControlModules.Controls.Add(this.tabPageSchedule);
            this.tabControlModules.Controls.Add(this.tabPageSlideshow);
            this.tabControlModules.Controls.Add(this.tabPageEditors);
            this.tabControlModules.Controls.Add(this.tabPageTriggers);
            this.tabControlModules.Controls.Add(this.tabPageSecurity);
            this.tabControlModules.Location = new System.Drawing.Point(0, 164);
            this.tabControlModules.Multiline = true;
            this.tabControlModules.Name = "tabControlModules";
            this.tabControlModules.SelectedIndex = 0;
            this.tabControlModules.Size = new System.Drawing.Size(249, 244);
            this.tabControlModules.TabIndex = 12;
            this.tabControlModules.TabStop = false;
            // 
            // tabPageScreens
            // 
            this.tabPageScreens.Location = new System.Drawing.Point(4, 40);
            this.tabPageScreens.Name = "tabPageScreens";
            this.tabPageScreens.Size = new System.Drawing.Size(241, 200);
            this.tabPageScreens.TabIndex = 5;
            this.tabPageScreens.Text = "Screens";
            this.tabPageScreens.UseVisualStyleBackColor = true;
            // 
            // tabPageRegions
            // 
            this.tabPageRegions.Location = new System.Drawing.Point(4, 40);
            this.tabPageRegions.Name = "tabPageRegions";
            this.tabPageRegions.Size = new System.Drawing.Size(241, 200);
            this.tabPageRegions.TabIndex = 4;
            this.tabPageRegions.Text = "Regions";
            this.tabPageRegions.UseVisualStyleBackColor = true;
            // 
            // tabPageDelay
            // 
            this.tabPageDelay.AutoScroll = true;
            this.tabPageDelay.Controls.Add(this.groupBoxCaptureDelay);
            this.tabPageDelay.Location = new System.Drawing.Point(4, 40);
            this.tabPageDelay.Name = "tabPageDelay";
            this.tabPageDelay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDelay.Size = new System.Drawing.Size(241, 200);
            this.tabPageDelay.TabIndex = 0;
            this.tabPageDelay.Text = "Delay";
            this.tabPageDelay.UseVisualStyleBackColor = true;
            // 
            // groupBoxCaptureDelay
            // 
            this.groupBoxCaptureDelay.Controls.Add(this.labelLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxInitialScreenshot);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelHoursInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxCaptureDelay.Location = new System.Drawing.Point(6, 5);
            this.groupBoxCaptureDelay.Name = "groupBoxCaptureDelay";
            this.groupBoxCaptureDelay.Size = new System.Drawing.Size(205, 122);
            this.groupBoxCaptureDelay.TabIndex = 14;
            this.groupBoxCaptureDelay.TabStop = false;
            this.groupBoxCaptureDelay.Text = "Take screenshots every ...";
            // 
            // labelLimit
            // 
            this.labelLimit.AutoSize = true;
            this.labelLimit.Location = new System.Drawing.Point(171, 47);
            this.labelLimit.Name = "labelLimit";
            this.labelLimit.Size = new System.Drawing.Size(24, 13);
            this.labelLimit.TabIndex = 26;
            this.labelLimit.Text = "limit";
            // 
            // checkBoxInitialScreenshot
            // 
            this.checkBoxInitialScreenshot.AutoSize = true;
            this.checkBoxInitialScreenshot.Location = new System.Drawing.Point(110, 20);
            this.checkBoxInitialScreenshot.Name = "checkBoxInitialScreenshot";
            this.checkBoxInitialScreenshot.Size = new System.Drawing.Size(90, 17);
            this.checkBoxInitialScreenshot.TabIndex = 18;
            this.checkBoxInitialScreenshot.TabStop = false;
            this.checkBoxInitialScreenshot.Text = "Initial Capture";
            this.checkBoxInitialScreenshot.UseVisualStyleBackColor = true;
            this.checkBoxInitialScreenshot.Click += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownCaptureLimit
            // 
            this.numericUpDownCaptureLimit.Location = new System.Drawing.Point(127, 45);
            this.numericUpDownCaptureLimit.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownCaptureLimit.Name = "numericUpDownCaptureLimit";
            this.numericUpDownCaptureLimit.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownCaptureLimit.TabIndex = 8;
            this.numericUpDownCaptureLimit.TabStop = false;
            this.numericUpDownCaptureLimit.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxCaptureLimit
            // 
            this.checkBoxCaptureLimit.AutoSize = true;
            this.checkBoxCaptureLimit.Location = new System.Drawing.Point(110, 47);
            this.checkBoxCaptureLimit.Name = "checkBoxCaptureLimit";
            this.checkBoxCaptureLimit.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCaptureLimit.TabIndex = 16;
            this.checkBoxCaptureLimit.TabStop = false;
            this.checkBoxCaptureLimit.UseVisualStyleBackColor = true;
            this.checkBoxCaptureLimit.CheckedChanged += new System.EventHandler(this.CheckedChanged_checkBoxCaptureLimit);
            this.checkBoxCaptureLimit.Click += new System.EventHandler(this.SaveSettings);
            // 
            // labelMillisecondsInterval
            // 
            this.labelMillisecondsInterval.AutoSize = true;
            this.labelMillisecondsInterval.Location = new System.Drawing.Point(54, 99);
            this.labelMillisecondsInterval.Name = "labelMillisecondsInterval";
            this.labelMillisecondsInterval.Size = new System.Drawing.Size(63, 13);
            this.labelMillisecondsInterval.TabIndex = 7;
            this.labelMillisecondsInterval.Text = "milliseconds";
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(6, 97);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 6;
            this.numericUpDownMillisecondsInterval.TabStop = false;
            this.numericUpDownMillisecondsInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelSecondsInterval
            // 
            this.labelSecondsInterval.AutoSize = true;
            this.labelSecondsInterval.Location = new System.Drawing.Point(54, 73);
            this.labelSecondsInterval.Name = "labelSecondsInterval";
            this.labelSecondsInterval.Size = new System.Drawing.Size(47, 13);
            this.labelSecondsInterval.TabIndex = 5;
            this.labelSecondsInterval.Text = "seconds";
            // 
            // labelMinutesInterval
            // 
            this.labelMinutesInterval.AutoSize = true;
            this.labelMinutesInterval.Location = new System.Drawing.Point(54, 47);
            this.labelMinutesInterval.Name = "labelMinutesInterval";
            this.labelMinutesInterval.Size = new System.Drawing.Size(43, 13);
            this.labelMinutesInterval.TabIndex = 4;
            this.labelMinutesInterval.Text = "minutes";
            // 
            // labelHoursInterval
            // 
            this.labelHoursInterval.AutoSize = true;
            this.labelHoursInterval.Location = new System.Drawing.Point(54, 21);
            this.labelHoursInterval.Name = "labelHoursInterval";
            this.labelHoursInterval.Size = new System.Drawing.Size(33, 13);
            this.labelHoursInterval.TabIndex = 3;
            this.labelHoursInterval.Text = "hours";
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(6, 71);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSecondsInterval.TabIndex = 2;
            this.numericUpDownSecondsInterval.TabStop = false;
            this.numericUpDownSecondsInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(6, 45);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMinutesInterval.TabIndex = 1;
            this.numericUpDownMinutesInterval.TabStop = false;
            this.numericUpDownMinutesInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(6, 19);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 0;
            this.numericUpDownHoursInterval.TabStop = false;
            this.numericUpDownHoursInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.Controls.Add(this.groupBoxSchedule);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 40);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Size = new System.Drawing.Size(241, 200);
            this.tabPageSchedule.TabIndex = 6;
            this.tabPageSchedule.Text = "Schedule";
            this.tabPageSchedule.UseVisualStyleBackColor = true;
            // 
            // groupBoxSchedule
            // 
            this.groupBoxSchedule.Controls.Add(this.checkBoxScheduleOnTheseDays);
            this.groupBoxSchedule.Controls.Add(this.checkBoxFriday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxThursday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxWednesday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxTuesday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxMonday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxSunday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxSaturday);
            this.groupBoxSchedule.Controls.Add(this.dateTimePickerScheduleStopAt);
            this.groupBoxSchedule.Controls.Add(this.dateTimePickerScheduleStartAt);
            this.groupBoxSchedule.Controls.Add(this.checkBoxScheduleStopAt);
            this.groupBoxSchedule.Controls.Add(this.checkBoxScheduleStartAt);
            this.groupBoxSchedule.Location = new System.Drawing.Point(7, 6);
            this.groupBoxSchedule.Name = "groupBoxSchedule";
            this.groupBoxSchedule.Size = new System.Drawing.Size(205, 108);
            this.groupBoxSchedule.TabIndex = 22;
            this.groupBoxSchedule.TabStop = false;
            this.groupBoxSchedule.Text = "Schedule";
            // 
            // checkBoxScheduleOnTheseDays
            // 
            this.checkBoxScheduleOnTheseDays.AutoSize = true;
            this.checkBoxScheduleOnTheseDays.Location = new System.Drawing.Point(6, 63);
            this.checkBoxScheduleOnTheseDays.Name = "checkBoxScheduleOnTheseDays";
            this.checkBoxScheduleOnTheseDays.Size = new System.Drawing.Size(119, 17);
            this.checkBoxScheduleOnTheseDays.TabIndex = 11;
            this.checkBoxScheduleOnTheseDays.TabStop = false;
            this.checkBoxScheduleOnTheseDays.Text = "Only on these days:";
            this.checkBoxScheduleOnTheseDays.UseVisualStyleBackColor = true;
            // 
            // checkBoxFriday
            // 
            this.checkBoxFriday.AutoSize = true;
            this.checkBoxFriday.Enabled = false;
            this.checkBoxFriday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFriday.Location = new System.Drawing.Point(165, 86);
            this.checkBoxFriday.Name = "checkBoxFriday";
            this.checkBoxFriday.Size = new System.Drawing.Size(33, 16);
            this.checkBoxFriday.TabIndex = 10;
            this.checkBoxFriday.TabStop = false;
            this.checkBoxFriday.Text = "Fr";
            this.checkBoxFriday.UseVisualStyleBackColor = true;
            // 
            // checkBoxThursday
            // 
            this.checkBoxThursday.AutoSize = true;
            this.checkBoxThursday.Enabled = false;
            this.checkBoxThursday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxThursday.Location = new System.Drawing.Point(130, 86);
            this.checkBoxThursday.Name = "checkBoxThursday";
            this.checkBoxThursday.Size = new System.Drawing.Size(34, 16);
            this.checkBoxThursday.TabIndex = 9;
            this.checkBoxThursday.TabStop = false;
            this.checkBoxThursday.Text = "Th";
            this.checkBoxThursday.UseVisualStyleBackColor = true;
            // 
            // checkBoxWednesday
            // 
            this.checkBoxWednesday.AutoSize = true;
            this.checkBoxWednesday.Enabled = false;
            this.checkBoxWednesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxWednesday.Location = new System.Drawing.Point(92, 86);
            this.checkBoxWednesday.Name = "checkBoxWednesday";
            this.checkBoxWednesday.Size = new System.Drawing.Size(38, 16);
            this.checkBoxWednesday.TabIndex = 8;
            this.checkBoxWednesday.TabStop = false;
            this.checkBoxWednesday.Text = "We";
            this.checkBoxWednesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxTuesday
            // 
            this.checkBoxTuesday.AutoSize = true;
            this.checkBoxTuesday.Enabled = false;
            this.checkBoxTuesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTuesday.Location = new System.Drawing.Point(58, 86);
            this.checkBoxTuesday.Name = "checkBoxTuesday";
            this.checkBoxTuesday.Size = new System.Drawing.Size(34, 16);
            this.checkBoxTuesday.TabIndex = 7;
            this.checkBoxTuesday.TabStop = false;
            this.checkBoxTuesday.Text = "Tu";
            this.checkBoxTuesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxMonday
            // 
            this.checkBoxMonday.AutoSize = true;
            this.checkBoxMonday.Enabled = false;
            this.checkBoxMonday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMonday.Location = new System.Drawing.Point(20, 86);
            this.checkBoxMonday.Name = "checkBoxMonday";
            this.checkBoxMonday.Size = new System.Drawing.Size(38, 16);
            this.checkBoxMonday.TabIndex = 6;
            this.checkBoxMonday.TabStop = false;
            this.checkBoxMonday.Text = "Mo";
            this.checkBoxMonday.UseVisualStyleBackColor = true;
            // 
            // checkBoxSunday
            // 
            this.checkBoxSunday.AutoSize = true;
            this.checkBoxSunday.Enabled = false;
            this.checkBoxSunday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSunday.Location = new System.Drawing.Point(165, 64);
            this.checkBoxSunday.Name = "checkBoxSunday";
            this.checkBoxSunday.Size = new System.Drawing.Size(35, 16);
            this.checkBoxSunday.TabIndex = 5;
            this.checkBoxSunday.TabStop = false;
            this.checkBoxSunday.Text = "Su";
            this.checkBoxSunday.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaturday
            // 
            this.checkBoxSaturday.AutoSize = true;
            this.checkBoxSaturday.Enabled = false;
            this.checkBoxSaturday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSaturday.Location = new System.Drawing.Point(130, 64);
            this.checkBoxSaturday.Name = "checkBoxSaturday";
            this.checkBoxSaturday.Size = new System.Drawing.Size(35, 16);
            this.checkBoxSaturday.TabIndex = 4;
            this.checkBoxSaturday.TabStop = false;
            this.checkBoxSaturday.Text = "Sa";
            this.checkBoxSaturday.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerScheduleStopAt
            // 
            this.dateTimePickerScheduleStopAt.CustomFormat = "HH:mm";
            this.dateTimePickerScheduleStopAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerScheduleStopAt.Location = new System.Drawing.Point(142, 39);
            this.dateTimePickerScheduleStopAt.Name = "dateTimePickerScheduleStopAt";
            this.dateTimePickerScheduleStopAt.ShowUpDown = true;
            this.dateTimePickerScheduleStopAt.Size = new System.Drawing.Size(56, 20);
            this.dateTimePickerScheduleStopAt.TabIndex = 3;
            this.dateTimePickerScheduleStopAt.TabStop = false;
            // 
            // dateTimePickerScheduleStartAt
            // 
            this.dateTimePickerScheduleStartAt.CustomFormat = "HH:mm";
            this.dateTimePickerScheduleStartAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerScheduleStartAt.Location = new System.Drawing.Point(142, 16);
            this.dateTimePickerScheduleStartAt.Name = "dateTimePickerScheduleStartAt";
            this.dateTimePickerScheduleStartAt.ShowUpDown = true;
            this.dateTimePickerScheduleStartAt.Size = new System.Drawing.Size(56, 20);
            this.dateTimePickerScheduleStartAt.TabIndex = 2;
            this.dateTimePickerScheduleStartAt.TabStop = false;
            // 
            // checkBoxScheduleStopAt
            // 
            this.checkBoxScheduleStopAt.AutoSize = true;
            this.checkBoxScheduleStopAt.Location = new System.Drawing.Point(6, 41);
            this.checkBoxScheduleStopAt.Name = "checkBoxScheduleStopAt";
            this.checkBoxScheduleStopAt.Size = new System.Drawing.Size(99, 17);
            this.checkBoxScheduleStopAt.TabIndex = 1;
            this.checkBoxScheduleStopAt.TabStop = false;
            this.checkBoxScheduleStopAt.Text = "Stop capture at";
            this.checkBoxScheduleStopAt.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleStartAt
            // 
            this.checkBoxScheduleStartAt.AutoSize = true;
            this.checkBoxScheduleStartAt.Location = new System.Drawing.Point(6, 18);
            this.checkBoxScheduleStartAt.Name = "checkBoxScheduleStartAt";
            this.checkBoxScheduleStartAt.Size = new System.Drawing.Size(99, 17);
            this.checkBoxScheduleStartAt.TabIndex = 0;
            this.checkBoxScheduleStartAt.TabStop = false;
            this.checkBoxScheduleStartAt.Text = "Start capture at";
            this.checkBoxScheduleStartAt.UseVisualStyleBackColor = true;
            // 
            // tabPageSlideshow
            // 
            this.tabPageSlideshow.AutoScroll = true;
            this.tabPageSlideshow.Controls.Add(this.listBoxSlides);
            this.tabPageSlideshow.Controls.Add(this.groupBoxSlideshowDelay);
            this.tabPageSlideshow.Location = new System.Drawing.Point(4, 40);
            this.tabPageSlideshow.Name = "tabPageSlideshow";
            this.tabPageSlideshow.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSlideshow.Size = new System.Drawing.Size(241, 200);
            this.tabPageSlideshow.TabIndex = 1;
            this.tabPageSlideshow.Text = "Slideshow";
            this.tabPageSlideshow.UseVisualStyleBackColor = true;
            // 
            // groupBoxSlideshowDelay
            // 
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelayMilliseconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelayMilliseconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideSkip);
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelaySeconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelayMinutes);
            this.groupBoxSlideshowDelay.Controls.Add(this.checkBoxSlideSkip);
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelayHours);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelaySeconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelayMinutes);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelayHours);
            this.groupBoxSlideshowDelay.Location = new System.Drawing.Point(6, 132);
            this.groupBoxSlideshowDelay.Name = "groupBoxSlideshowDelay";
            this.groupBoxSlideshowDelay.Size = new System.Drawing.Size(204, 124);
            this.groupBoxSlideshowDelay.TabIndex = 12;
            this.groupBoxSlideshowDelay.TabStop = false;
            this.groupBoxSlideshowDelay.Text = "Show the next slide in ...";
            // 
            // labelSlideshowDelayMilliseconds
            // 
            this.labelSlideshowDelayMilliseconds.AutoSize = true;
            this.labelSlideshowDelayMilliseconds.Location = new System.Drawing.Point(54, 99);
            this.labelSlideshowDelayMilliseconds.Name = "labelSlideshowDelayMilliseconds";
            this.labelSlideshowDelayMilliseconds.Size = new System.Drawing.Size(63, 13);
            this.labelSlideshowDelayMilliseconds.TabIndex = 7;
            this.labelSlideshowDelayMilliseconds.Text = "milliseconds";
            // 
            // numericUpDownSlideshowDelayMilliseconds
            // 
            this.numericUpDownSlideshowDelayMilliseconds.Location = new System.Drawing.Point(6, 97);
            this.numericUpDownSlideshowDelayMilliseconds.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelayMilliseconds.Name = "numericUpDownSlideshowDelayMilliseconds";
            this.numericUpDownSlideshowDelayMilliseconds.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelayMilliseconds.TabIndex = 6;
            this.numericUpDownSlideshowDelayMilliseconds.TabStop = false;
            this.numericUpDownSlideshowDelayMilliseconds.ValueChanged += new System.EventHandler(this.ValueChanged_numericUpDownSlideshowDelay);
            this.numericUpDownSlideshowDelayMilliseconds.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownSlideSkip
            // 
            this.numericUpDownSlideSkip.Location = new System.Drawing.Point(156, 19);
            this.numericUpDownSlideSkip.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownSlideSkip.Name = "numericUpDownSlideSkip";
            this.numericUpDownSlideSkip.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideSkip.TabIndex = 8;
            this.numericUpDownSlideSkip.TabStop = false;
            this.numericUpDownSlideSkip.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelSlideshowDelaySeconds
            // 
            this.labelSlideshowDelaySeconds.AutoSize = true;
            this.labelSlideshowDelaySeconds.Location = new System.Drawing.Point(54, 73);
            this.labelSlideshowDelaySeconds.Name = "labelSlideshowDelaySeconds";
            this.labelSlideshowDelaySeconds.Size = new System.Drawing.Size(47, 13);
            this.labelSlideshowDelaySeconds.TabIndex = 5;
            this.labelSlideshowDelaySeconds.Text = "seconds";
            // 
            // labelSlideshowDelayMinutes
            // 
            this.labelSlideshowDelayMinutes.AutoSize = true;
            this.labelSlideshowDelayMinutes.Location = new System.Drawing.Point(54, 47);
            this.labelSlideshowDelayMinutes.Name = "labelSlideshowDelayMinutes";
            this.labelSlideshowDelayMinutes.Size = new System.Drawing.Size(43, 13);
            this.labelSlideshowDelayMinutes.TabIndex = 4;
            this.labelSlideshowDelayMinutes.Text = "minutes";
            // 
            // checkBoxSlideSkip
            // 
            this.checkBoxSlideSkip.AutoSize = true;
            this.checkBoxSlideSkip.Location = new System.Drawing.Point(113, 20);
            this.checkBoxSlideSkip.Name = "checkBoxSlideSkip";
            this.checkBoxSlideSkip.Size = new System.Drawing.Size(47, 17);
            this.checkBoxSlideSkip.TabIndex = 13;
            this.checkBoxSlideSkip.TabStop = false;
            this.checkBoxSlideSkip.Text = "Skip";
            this.checkBoxSlideSkip.UseVisualStyleBackColor = true;
            this.checkBoxSlideSkip.Click += new System.EventHandler(this.SaveSettings);
            // 
            // labelSlideshowDelayHours
            // 
            this.labelSlideshowDelayHours.AutoSize = true;
            this.labelSlideshowDelayHours.Location = new System.Drawing.Point(54, 21);
            this.labelSlideshowDelayHours.Name = "labelSlideshowDelayHours";
            this.labelSlideshowDelayHours.Size = new System.Drawing.Size(33, 13);
            this.labelSlideshowDelayHours.TabIndex = 3;
            this.labelSlideshowDelayHours.Text = "hours";
            // 
            // numericUpDownSlideshowDelaySeconds
            // 
            this.numericUpDownSlideshowDelaySeconds.Location = new System.Drawing.Point(6, 71);
            this.numericUpDownSlideshowDelaySeconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelaySeconds.Name = "numericUpDownSlideshowDelaySeconds";
            this.numericUpDownSlideshowDelaySeconds.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelaySeconds.TabIndex = 2;
            this.numericUpDownSlideshowDelaySeconds.TabStop = false;
            this.numericUpDownSlideshowDelaySeconds.ValueChanged += new System.EventHandler(this.ValueChanged_numericUpDownSlideshowDelay);
            this.numericUpDownSlideshowDelaySeconds.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownSlideshowDelayMinutes
            // 
            this.numericUpDownSlideshowDelayMinutes.Location = new System.Drawing.Point(6, 45);
            this.numericUpDownSlideshowDelayMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelayMinutes.Name = "numericUpDownSlideshowDelayMinutes";
            this.numericUpDownSlideshowDelayMinutes.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelayMinutes.TabIndex = 1;
            this.numericUpDownSlideshowDelayMinutes.TabStop = false;
            this.numericUpDownSlideshowDelayMinutes.ValueChanged += new System.EventHandler(this.ValueChanged_numericUpDownSlideshowDelay);
            this.numericUpDownSlideshowDelayMinutes.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownSlideshowDelayHours
            // 
            this.numericUpDownSlideshowDelayHours.Location = new System.Drawing.Point(6, 19);
            this.numericUpDownSlideshowDelayHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelayHours.Name = "numericUpDownSlideshowDelayHours";
            this.numericUpDownSlideshowDelayHours.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelayHours.TabIndex = 0;
            this.numericUpDownSlideshowDelayHours.TabStop = false;
            this.numericUpDownSlideshowDelayHours.ValueChanged += new System.EventHandler(this.ValueChanged_numericUpDownSlideshowDelay);
            this.numericUpDownSlideshowDelayHours.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // tabPageEditors
            // 
            this.tabPageEditors.AutoScroll = true;
            this.tabPageEditors.Location = new System.Drawing.Point(4, 40);
            this.tabPageEditors.Name = "tabPageEditors";
            this.tabPageEditors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEditors.Size = new System.Drawing.Size(241, 200);
            this.tabPageEditors.TabIndex = 2;
            this.tabPageEditors.Text = "Editors";
            this.tabPageEditors.UseVisualStyleBackColor = true;
            // 
            // tabPageTriggers
            // 
            this.tabPageTriggers.AutoScroll = true;
            this.tabPageTriggers.Location = new System.Drawing.Point(4, 40);
            this.tabPageTriggers.Name = "tabPageTriggers";
            this.tabPageTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTriggers.Size = new System.Drawing.Size(241, 200);
            this.tabPageTriggers.TabIndex = 3;
            this.tabPageTriggers.Text = "Triggers";
            this.tabPageTriggers.UseVisualStyleBackColor = true;
            // 
            // tabPageSecurity
            // 
            this.tabPageSecurity.Controls.Add(this.groupBoxSecurity);
            this.tabPageSecurity.Location = new System.Drawing.Point(4, 40);
            this.tabPageSecurity.Name = "tabPageSecurity";
            this.tabPageSecurity.Size = new System.Drawing.Size(241, 200);
            this.tabPageSecurity.TabIndex = 7;
            this.tabPageSecurity.Text = "Security";
            this.tabPageSecurity.UseVisualStyleBackColor = true;
            // 
            // groupBoxSecurity
            // 
            this.groupBoxSecurity.Controls.Add(this.checkBoxPassphraseLock);
            this.groupBoxSecurity.Controls.Add(this.buttonClearPassphrase);
            this.groupBoxSecurity.Controls.Add(this.labelPasswordDescription);
            this.groupBoxSecurity.Controls.Add(this.buttonSetPassphrase);
            this.groupBoxSecurity.Controls.Add(this.textBoxPassphrase);
            this.groupBoxSecurity.Location = new System.Drawing.Point(6, 6);
            this.groupBoxSecurity.Name = "groupBoxSecurity";
            this.groupBoxSecurity.Size = new System.Drawing.Size(205, 135);
            this.groupBoxSecurity.TabIndex = 23;
            this.groupBoxSecurity.TabStop = false;
            this.groupBoxSecurity.Text = "Security";
            // 
            // checkBoxPassphraseLock
            // 
            this.checkBoxPassphraseLock.AutoSize = true;
            this.checkBoxPassphraseLock.Location = new System.Drawing.Point(6, 111);
            this.checkBoxPassphraseLock.Name = "checkBoxPassphraseLock";
            this.checkBoxPassphraseLock.Size = new System.Drawing.Size(50, 17);
            this.checkBoxPassphraseLock.TabIndex = 4;
            this.checkBoxPassphraseLock.TabStop = false;
            this.checkBoxPassphraseLock.Text = "Lock";
            this.checkBoxPassphraseLock.UseVisualStyleBackColor = true;
            this.checkBoxPassphraseLock.CheckedChanged += new System.EventHandler(this.CheckedChanged_checkBoxPassphraseLock);
            // 
            // buttonClearPassphrase
            // 
            this.buttonClearPassphrase.Location = new System.Drawing.Point(141, 107);
            this.buttonClearPassphrase.Name = "buttonClearPassphrase";
            this.buttonClearPassphrase.Size = new System.Drawing.Size(52, 23);
            this.buttonClearPassphrase.TabIndex = 3;
            this.buttonClearPassphrase.TabStop = false;
            this.buttonClearPassphrase.Text = "Clear";
            this.buttonClearPassphrase.UseVisualStyleBackColor = true;
            this.buttonClearPassphrase.Click += new System.EventHandler(this.Click_buttonClearPassphrase);
            // 
            // labelPasswordDescription
            // 
            this.labelPasswordDescription.Location = new System.Drawing.Point(7, 21);
            this.labelPasswordDescription.Name = "labelPasswordDescription";
            this.labelPasswordDescription.Size = new System.Drawing.Size(186, 55);
            this.labelPasswordDescription.TabIndex = 2;
            this.labelPasswordDescription.Text = "This passphrase will be required whenever screen capturing is stopped, this inter" +
    "face is shown, or the application is exiting.";
            // 
            // buttonSetPassphrase
            // 
            this.buttonSetPassphrase.Enabled = false;
            this.buttonSetPassphrase.Location = new System.Drawing.Point(83, 107);
            this.buttonSetPassphrase.Name = "buttonSetPassphrase";
            this.buttonSetPassphrase.Size = new System.Drawing.Size(52, 23);
            this.buttonSetPassphrase.TabIndex = 1;
            this.buttonSetPassphrase.TabStop = false;
            this.buttonSetPassphrase.Text = "Set";
            this.buttonSetPassphrase.UseVisualStyleBackColor = true;
            this.buttonSetPassphrase.Click += new System.EventHandler(this.Click_buttonSetPassphrase);
            // 
            // textBoxPassphrase
            // 
            this.textBoxPassphrase.Location = new System.Drawing.Point(6, 82);
            this.textBoxPassphrase.MaxLength = 30;
            this.textBoxPassphrase.Name = "textBoxPassphrase";
            this.textBoxPassphrase.Size = new System.Drawing.Size(187, 20);
            this.textBoxPassphrase.TabIndex = 0;
            this.textBoxPassphrase.TabStop = false;
            this.textBoxPassphrase.TextChanged += new System.EventHandler(this.TextChanged_textBoxPassphrase);
            // 
            // toolStripScreenCapture
            // 
            this.toolStripScreenCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStripScreenCapture.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripScreenCapture.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripScreenCapture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStartCapture,
            this.toolStripButtonStopCapture});
            this.toolStripScreenCapture.Location = new System.Drawing.Point(20, 411);
            this.toolStripScreenCapture.Name = "toolStripScreenCapture";
            this.toolStripScreenCapture.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripScreenCapture.ShowItemToolTips = false;
            this.toolStripScreenCapture.Size = new System.Drawing.Size(195, 25);
            this.toolStripScreenCapture.TabIndex = 20;
            // 
            // toolStripButtonStartCapture
            // 
            this.toolStripButtonStartCapture.Enabled = false;
            this.toolStripButtonStartCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.toolStripButtonStartCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStartCapture.Name = "toolStripButtonStartCapture";
            this.toolStripButtonStartCapture.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonStartCapture.Text = "Start Capture";
            this.toolStripButtonStartCapture.Click += new System.EventHandler(this.Click_toolStripMenuItemStartScreenCapture);
            // 
            // toolStripButtonStopCapture
            // 
            this.toolStripButtonStopCapture.Enabled = false;
            this.toolStripButtonStopCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripButtonStopCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStopCapture.Name = "toolStripButtonStopCapture";
            this.toolStripButtonStopCapture.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonStopCapture.Text = "Stop Capture";
            this.toolStripButtonStopCapture.Click += new System.EventHandler(this.Click_toolStripMenuItemStopScreenCapture);
            // 
            // timerScheduledCaptureStart
            // 
            this.timerScheduledCaptureStart.Enabled = true;
            this.timerScheduledCaptureStart.Interval = 60000;
            this.timerScheduledCaptureStart.Tick += new System.EventHandler(this.Tick_timerScheduledCaptureStart);
            // 
            // timerScheduledCaptureStop
            // 
            this.timerScheduledCaptureStop.Enabled = true;
            this.timerScheduledCaptureStop.Interval = 60000;
            this.timerScheduledCaptureStop.Tick += new System.EventHandler(this.Tick_timerScheduledCaptureStop);
            // 
            // timerScreenCapture
            // 
            this.timerScreenCapture.Tick += new System.EventHandler(this.Tick_timerScreenCapture);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 488);
            this.Controls.Add(this.toolStripScreenCapture);
            this.Controls.Add(this.tabControlModules);
            this.Controls.Add(this.tabControlScreens);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.toolStripSlideshow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(768, 437);
            this.Name = "FormMain";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormViewer_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStripSystemTrayIcon.ResumeLayout(false);
            this.toolStripSlideshow.ResumeLayout(false);
            this.toolStripSlideshow.PerformLayout();
            this.tabControlModules.ResumeLayout(false);
            this.tabPageDelay.ResumeLayout(false);
            this.groupBoxCaptureDelay.ResumeLayout(false);
            this.groupBoxCaptureDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            this.tabPageSchedule.ResumeLayout(false);
            this.groupBoxSchedule.ResumeLayout(false);
            this.groupBoxSchedule.PerformLayout();
            this.tabPageSlideshow.ResumeLayout(false);
            this.groupBoxSlideshowDelay.ResumeLayout(false);
            this.groupBoxSlideshowDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMilliseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelaySeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayHours)).EndInit();
            this.tabPageSecurity.ResumeLayout(false);
            this.groupBoxSecurity.ResumeLayout(false);
            this.groupBoxSecurity.PerformLayout();
            this.toolStripScreenCapture.ResumeLayout(false);
            this.toolStripScreenCapture.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControlScreens;
        private System.Windows.Forms.ListBox listBoxSlides;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStopCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripScreenshotPreview;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowInterface;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorInterface;
        private System.Windows.Forms.ToolStrip toolStripSlideshow;
        private System.Windows.Forms.ToolStripButton toolStripButtonFirstSlide;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviousSlide;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlaySlideshow;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextSlide;
        private System.Windows.Forms.ToolStripButton toolStripButtonLastSlide;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHideInterface;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorCapture;
        private System.Windows.Forms.TabControl tabControlModules;
        private System.Windows.Forms.TabPage tabPageDelay;
        private System.Windows.Forms.TabPage tabPageSlideshow;
        private System.Windows.Forms.GroupBox groupBoxCaptureDelay;
        private System.Windows.Forms.Label labelMillisecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
        private System.Windows.Forms.Label labelSecondsInterval;
        private System.Windows.Forms.Label labelMinutesInterval;
        private System.Windows.Forms.Label labelHoursInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.CheckBox checkBoxCaptureLimit;
        private System.Windows.Forms.CheckBox checkBoxInitialScreenshot;
        private System.Windows.Forms.GroupBox groupBoxSlideshowDelay;
        private System.Windows.Forms.Label labelSlideshowDelayMilliseconds;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelayMilliseconds;
        private System.Windows.Forms.Label labelSlideshowDelaySeconds;
        private System.Windows.Forms.Label labelSlideshowDelayMinutes;
        private System.Windows.Forms.Label labelSlideshowDelayHours;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelaySeconds;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelayMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelayHours;
        private System.Windows.Forms.CheckBox checkBoxSlideSkip;
        private System.Windows.Forms.ToolStrip toolStripScreenCapture;
        private System.Windows.Forms.NumericUpDown numericUpDownCaptureLimit;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideSkip;
        private System.Windows.Forms.Label labelLimit;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonOptions;
        private System.Windows.Forms.Timer timerScheduledCaptureStart;
        private System.Windows.Forms.Timer timerScheduledCaptureStop;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAbout;
        private System.Windows.Forms.Timer timerScreenCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowSystemTrayIcon;
        private System.Windows.Forms.TabPage tabPageEditors;
        private System.Windows.Forms.TabPage tabPageTriggers;
        private System.Windows.Forms.TabPage tabPageRegions;
        private System.Windows.Forms.TabPage tabPageScreens;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPageSecurity;
        private System.Windows.Forms.ToolStripButton toolStripButtonStartCapture;
        private System.Windows.Forms.ToolStripButton toolStripButtonStopCapture;
        private System.Windows.Forms.GroupBox groupBoxSchedule;
        private System.Windows.Forms.CheckBox checkBoxScheduleOnTheseDays;
        private System.Windows.Forms.CheckBox checkBoxFriday;
        private System.Windows.Forms.CheckBox checkBoxThursday;
        private System.Windows.Forms.CheckBox checkBoxWednesday;
        private System.Windows.Forms.CheckBox checkBoxTuesday;
        private System.Windows.Forms.CheckBox checkBoxMonday;
        private System.Windows.Forms.CheckBox checkBoxSunday;
        private System.Windows.Forms.CheckBox checkBoxSaturday;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStopAt;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStartAt;
        private System.Windows.Forms.CheckBox checkBoxScheduleStopAt;
        private System.Windows.Forms.CheckBox checkBoxScheduleStartAt;
        private System.Windows.Forms.GroupBox groupBoxSecurity;
        private System.Windows.Forms.CheckBox checkBoxPassphraseLock;
        private System.Windows.Forms.Button buttonClearPassphrase;
        private System.Windows.Forms.Label labelPasswordDescription;
        private System.Windows.Forms.Button buttonSetPassphrase;
        private System.Windows.Forms.TextBox textBoxPassphrase;
    }
}