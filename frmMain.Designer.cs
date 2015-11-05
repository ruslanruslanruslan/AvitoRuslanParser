namespace AvitoRuslanParser
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (mySqlDB != null)
              mySqlDB.Dispose();
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.btnEnter = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.LinkAdtextBox = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.labelParsed = new System.Windows.Forms.Label();
      this.labelInserted = new System.Windows.Forms.Label();
      this.btnSettings = new System.Windows.Forms.Button();
      this.cbCategories = new System.Windows.Forms.ComboBox();
      this.Label2 = new System.Windows.Forms.Label();
      this.btnReset = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rtbLog = new System.Windows.Forms.RichTextBox();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.rtbLogStatistics = new System.Windows.Forms.RichTextBox();
      this.imlMain = new System.Windows.Forms.ImageList(this.components);
      this.gbAvito = new System.Windows.Forms.GroupBox();
      this.btnParsingAvitoStop = new System.Windows.Forms.Button();
      this.btnParsingAvitoPause = new System.Windows.Forms.Button();
      this.btnParsingAvitoStart = new System.Windows.Forms.Button();
      this.gbEbay = new System.Windows.Forms.GroupBox();
      this.btnParsingEbayStop = new System.Windows.Forms.Button();
      this.btnParsingEbayPause = new System.Windows.Forms.Button();
      this.btnParsingEbayStart = new System.Windows.Forms.Button();
      this.gbAvitoEbay = new System.Windows.Forms.GroupBox();
      this.btnParsingAvitoEbayStop = new System.Windows.Forms.Button();
      this.btnParsingAvitoEbayPause = new System.Windows.Forms.Button();
      this.btnParsingAvitoEbayStart = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.gbAvito.SuspendLayout();
      this.gbEbay.SuspendLayout();
      this.gbAvitoEbay.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnEnter
      // 
      this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnEnter.Location = new System.Drawing.Point(857, 8);
      this.btnEnter.Name = "btnEnter";
      this.btnEnter.Size = new System.Drawing.Size(75, 23);
      this.btnEnter.TabIndex = 0;
      this.btnEnter.Text = "Enter";
      this.btnEnter.UseVisualStyleBackColor = true;
      this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(2, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(72, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Link to advert";
      // 
      // LinkAdtextBox
      // 
      this.LinkAdtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.LinkAdtextBox.Location = new System.Drawing.Point(80, 10);
      this.LinkAdtextBox.Name = "LinkAdtextBox";
      this.LinkAdtextBox.Size = new System.Drawing.Size(771, 20);
      this.LinkAdtextBox.TabIndex = 2;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(252, 208);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(0, 13);
      this.label6.TabIndex = 8;
      // 
      // label9
      // 
      this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(12, 364);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(92, 13);
      this.label9.TabIndex = 14;
      this.label9.Text = "Count Parsed ad: ";
      // 
      // label10
      // 
      this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(12, 387);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(97, 13);
      this.label10.TabIndex = 15;
      this.label10.Text = "Count Inserted ad: ";
      // 
      // labelParsed
      // 
      this.labelParsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.labelParsed.AutoSize = true;
      this.labelParsed.Location = new System.Drawing.Point(133, 364);
      this.labelParsed.Name = "labelParsed";
      this.labelParsed.Size = new System.Drawing.Size(16, 13);
      this.labelParsed.TabIndex = 16;
      this.labelParsed.Text = "...";
      // 
      // labelInserted
      // 
      this.labelInserted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.labelInserted.AutoSize = true;
      this.labelInserted.Location = new System.Drawing.Point(133, 386);
      this.labelInserted.Name = "labelInserted";
      this.labelInserted.Size = new System.Drawing.Size(16, 13);
      this.labelInserted.TabIndex = 17;
      this.labelInserted.Text = "...";
      // 
      // btnSettings
      // 
      this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSettings.Location = new System.Drawing.Point(627, 377);
      this.btnSettings.Name = "btnSettings";
      this.btnSettings.Size = new System.Drawing.Size(184, 23);
      this.btnSettings.TabIndex = 20;
      this.btnSettings.Text = "Settings";
      this.btnSettings.UseVisualStyleBackColor = true;
      this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
      // 
      // cbCategories
      // 
      this.cbCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbCategories.FormattingEnabled = true;
      this.cbCategories.Location = new System.Drawing.Point(80, 36);
      this.cbCategories.Name = "cbCategories";
      this.cbCategories.Size = new System.Drawing.Size(771, 21);
      this.cbCategories.TabIndex = 21;
      // 
      // Label2
      // 
      this.Label2.AutoSize = true;
      this.Label2.Location = new System.Drawing.Point(2, 39);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(49, 13);
      this.Label2.TabIndex = 22;
      this.Label2.Text = "Category";
      // 
      // btnReset
      // 
      this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnReset.Location = new System.Drawing.Point(857, 36);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new System.Drawing.Size(75, 23);
      this.btnReset.TabIndex = 23;
      this.btnReset.Text = "Reset";
      this.btnReset.UseVisualStyleBackColor = true;
      this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.rtbLog);
      this.groupBox1.Controls.Add(this.splitter1);
      this.groupBox1.Controls.Add(this.rtbLogStatistics);
      this.groupBox1.Location = new System.Drawing.Point(5, 63);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(927, 224);
      this.groupBox1.TabIndex = 26;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Logs";
      // 
      // rtbLog
      // 
      this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.rtbLog.Location = new System.Drawing.Point(3, 16);
      this.rtbLog.Name = "rtbLog";
      this.rtbLog.ReadOnly = true;
      this.rtbLog.Size = new System.Drawing.Size(921, 106);
      this.rtbLog.TabIndex = 0;
      this.rtbLog.Text = "";
      this.rtbLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLog_LinkClicked);
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter1.Location = new System.Drawing.Point(3, 122);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(921, 3);
      this.splitter1.TabIndex = 1;
      this.splitter1.TabStop = false;
      // 
      // rtbLogStatistics
      // 
      this.rtbLogStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.rtbLogStatistics.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.rtbLogStatistics.Location = new System.Drawing.Point(3, 125);
      this.rtbLogStatistics.Name = "rtbLogStatistics";
      this.rtbLogStatistics.ReadOnly = true;
      this.rtbLogStatistics.Size = new System.Drawing.Size(921, 96);
      this.rtbLogStatistics.TabIndex = 2;
      this.rtbLogStatistics.Text = "";
      // 
      // imlMain
      // 
      this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
      this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
      this.imlMain.Images.SetKeyName(0, "start.png");
      this.imlMain.Images.SetKeyName(1, "pause.png");
      this.imlMain.Images.SetKeyName(2, "stop.png");
      // 
      // gbAvito
      // 
      this.gbAvito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.gbAvito.Controls.Add(this.btnParsingAvitoStop);
      this.gbAvito.Controls.Add(this.btnParsingAvitoPause);
      this.gbAvito.Controls.Add(this.btnParsingAvitoStart);
      this.gbAvito.Location = new System.Drawing.Point(817, 293);
      this.gbAvito.Name = "gbAvito";
      this.gbAvito.Size = new System.Drawing.Size(115, 33);
      this.gbAvito.TabIndex = 27;
      this.gbAvito.TabStop = false;
      this.gbAvito.Text = "Avito";
      // 
      // btnParsingAvitoStop
      // 
      this.btnParsingAvitoStop.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingAvitoStop.Enabled = false;
      this.btnParsingAvitoStop.ImageKey = "stop.png";
      this.btnParsingAvitoStop.ImageList = this.imlMain;
      this.btnParsingAvitoStop.Location = new System.Drawing.Point(88, 11);
      this.btnParsingAvitoStop.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingAvitoStop.Name = "btnParsingAvitoStop";
      this.btnParsingAvitoStop.Size = new System.Drawing.Size(24, 23);
      this.btnParsingAvitoStop.TabIndex = 14;
      this.btnParsingAvitoStop.UseVisualStyleBackColor = true;
      this.btnParsingAvitoStop.Click += new System.EventHandler(this.btnParsingAvitoStop_Click);
      // 
      // btnParsingAvitoPause
      // 
      this.btnParsingAvitoPause.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingAvitoPause.Enabled = false;
      this.btnParsingAvitoPause.ImageKey = "pause.png";
      this.btnParsingAvitoPause.ImageList = this.imlMain;
      this.btnParsingAvitoPause.Location = new System.Drawing.Point(64, 11);
      this.btnParsingAvitoPause.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingAvitoPause.Name = "btnParsingAvitoPause";
      this.btnParsingAvitoPause.Size = new System.Drawing.Size(24, 23);
      this.btnParsingAvitoPause.TabIndex = 13;
      this.btnParsingAvitoPause.UseVisualStyleBackColor = true;
      this.btnParsingAvitoPause.Click += new System.EventHandler(this.btnParsingAvitoPause_Click);
      // 
      // btnParsingAvitoStart
      // 
      this.btnParsingAvitoStart.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingAvitoStart.ImageKey = "start.png";
      this.btnParsingAvitoStart.ImageList = this.imlMain;
      this.btnParsingAvitoStart.Location = new System.Drawing.Point(40, 11);
      this.btnParsingAvitoStart.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingAvitoStart.Name = "btnParsingAvitoStart";
      this.btnParsingAvitoStart.Size = new System.Drawing.Size(24, 23);
      this.btnParsingAvitoStart.TabIndex = 12;
      this.btnParsingAvitoStart.UseVisualStyleBackColor = true;
      this.btnParsingAvitoStart.Click += new System.EventHandler(this.btnParsingAvito_Click);
      // 
      // gbEbay
      // 
      this.gbEbay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.gbEbay.Controls.Add(this.btnParsingEbayStop);
      this.gbEbay.Controls.Add(this.btnParsingEbayPause);
      this.gbEbay.Controls.Add(this.btnParsingEbayStart);
      this.gbEbay.Location = new System.Drawing.Point(817, 329);
      this.gbEbay.Name = "gbEbay";
      this.gbEbay.Size = new System.Drawing.Size(115, 33);
      this.gbEbay.TabIndex = 28;
      this.gbEbay.TabStop = false;
      this.gbEbay.Text = "Ebay";
      // 
      // btnParsingEbayStop
      // 
      this.btnParsingEbayStop.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingEbayStop.Enabled = false;
      this.btnParsingEbayStop.ImageKey = "stop.png";
      this.btnParsingEbayStop.ImageList = this.imlMain;
      this.btnParsingEbayStop.Location = new System.Drawing.Point(88, 11);
      this.btnParsingEbayStop.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingEbayStop.Name = "btnParsingEbayStop";
      this.btnParsingEbayStop.Size = new System.Drawing.Size(24, 23);
      this.btnParsingEbayStop.TabIndex = 14;
      this.btnParsingEbayStop.UseVisualStyleBackColor = true;
      this.btnParsingEbayStop.Click += new System.EventHandler(this.btnParsingEbayStop_Click);
      // 
      // btnParsingEbayPause
      // 
      this.btnParsingEbayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingEbayPause.Enabled = false;
      this.btnParsingEbayPause.ImageKey = "pause.png";
      this.btnParsingEbayPause.ImageList = this.imlMain;
      this.btnParsingEbayPause.Location = new System.Drawing.Point(64, 11);
      this.btnParsingEbayPause.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingEbayPause.Name = "btnParsingEbayPause";
      this.btnParsingEbayPause.Size = new System.Drawing.Size(24, 23);
      this.btnParsingEbayPause.TabIndex = 13;
      this.btnParsingEbayPause.UseVisualStyleBackColor = true;
      this.btnParsingEbayPause.Click += new System.EventHandler(this.btnParsingEbayPause_Click);
      // 
      // btnParsingEbayStart
      // 
      this.btnParsingEbayStart.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingEbayStart.ImageKey = "start.png";
      this.btnParsingEbayStart.ImageList = this.imlMain;
      this.btnParsingEbayStart.Location = new System.Drawing.Point(40, 11);
      this.btnParsingEbayStart.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingEbayStart.Name = "btnParsingEbayStart";
      this.btnParsingEbayStart.Size = new System.Drawing.Size(24, 23);
      this.btnParsingEbayStart.TabIndex = 12;
      this.btnParsingEbayStart.UseVisualStyleBackColor = true;
      this.btnParsingEbayStart.Click += new System.EventHandler(this.buttonParsingEbay_Click);
      // 
      // gbAvitoEbay
      // 
      this.gbAvitoEbay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.gbAvitoEbay.Controls.Add(this.btnParsingAvitoEbayStop);
      this.gbAvitoEbay.Controls.Add(this.btnParsingAvitoEbayPause);
      this.gbAvitoEbay.Controls.Add(this.btnParsingAvitoEbayStart);
      this.gbAvitoEbay.Location = new System.Drawing.Point(817, 368);
      this.gbAvitoEbay.Name = "gbAvitoEbay";
      this.gbAvitoEbay.Size = new System.Drawing.Size(115, 33);
      this.gbAvitoEbay.TabIndex = 28;
      this.gbAvitoEbay.TabStop = false;
      this.gbAvitoEbay.Text = "Avito + Ebay";
      // 
      // btnParsingAvitoEbayStop
      // 
      this.btnParsingAvitoEbayStop.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingAvitoEbayStop.Enabled = false;
      this.btnParsingAvitoEbayStop.ImageKey = "stop.png";
      this.btnParsingAvitoEbayStop.ImageList = this.imlMain;
      this.btnParsingAvitoEbayStop.Location = new System.Drawing.Point(88, 11);
      this.btnParsingAvitoEbayStop.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingAvitoEbayStop.Name = "btnParsingAvitoEbayStop";
      this.btnParsingAvitoEbayStop.Size = new System.Drawing.Size(24, 23);
      this.btnParsingAvitoEbayStop.TabIndex = 14;
      this.btnParsingAvitoEbayStop.UseVisualStyleBackColor = true;
      this.btnParsingAvitoEbayStop.Click += new System.EventHandler(this.btnParsingAvitoEbayStop_Click);
      // 
      // btnParsingAvitoEbayPause
      // 
      this.btnParsingAvitoEbayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingAvitoEbayPause.Enabled = false;
      this.btnParsingAvitoEbayPause.ImageKey = "pause.png";
      this.btnParsingAvitoEbayPause.ImageList = this.imlMain;
      this.btnParsingAvitoEbayPause.Location = new System.Drawing.Point(64, 11);
      this.btnParsingAvitoEbayPause.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingAvitoEbayPause.Name = "btnParsingAvitoEbayPause";
      this.btnParsingAvitoEbayPause.Size = new System.Drawing.Size(24, 23);
      this.btnParsingAvitoEbayPause.TabIndex = 13;
      this.btnParsingAvitoEbayPause.UseVisualStyleBackColor = true;
      this.btnParsingAvitoEbayPause.Click += new System.EventHandler(this.btnParsingAvitoEbayPause_Click);
      // 
      // btnParsingAvitoEbayStart
      // 
      this.btnParsingAvitoEbayStart.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnParsingAvitoEbayStart.ImageKey = "start.png";
      this.btnParsingAvitoEbayStart.ImageList = this.imlMain;
      this.btnParsingAvitoEbayStart.Location = new System.Drawing.Point(40, 11);
      this.btnParsingAvitoEbayStart.Margin = new System.Windows.Forms.Padding(0);
      this.btnParsingAvitoEbayStart.Name = "btnParsingAvitoEbayStart";
      this.btnParsingAvitoEbayStart.Size = new System.Drawing.Size(24, 23);
      this.btnParsingAvitoEbayStart.TabIndex = 12;
      this.btnParsingAvitoEbayStart.UseVisualStyleBackColor = true;
      this.btnParsingAvitoEbayStart.Click += new System.EventHandler(this.buttonParsingAvitoEbay_Click);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(944, 411);
      this.Controls.Add(this.gbAvitoEbay);
      this.Controls.Add(this.gbEbay);
      this.Controls.Add(this.gbAvito);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btnReset);
      this.Controls.Add(this.Label2);
      this.Controls.Add(this.cbCategories);
      this.Controls.Add(this.btnSettings);
      this.Controls.Add(this.labelInserted);
      this.Controls.Add(this.labelParsed);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.LinkAdtextBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnEnter);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Parser";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_Closing);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.groupBox1.ResumeLayout(false);
      this.gbAvito.ResumeLayout(false);
      this.gbEbay.ResumeLayout(false);
      this.gbAvitoEbay.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox LinkAdtextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelParsed;
        private System.Windows.Forms.Label labelInserted;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ComboBox cbCategories;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox rtbLogStatistics;
    private System.Windows.Forms.ImageList imlMain;
    private System.Windows.Forms.GroupBox gbAvito;
    private System.Windows.Forms.Button btnParsingAvitoStop;
    private System.Windows.Forms.Button btnParsingAvitoPause;
    private System.Windows.Forms.Button btnParsingAvitoStart;
    private System.Windows.Forms.GroupBox gbEbay;
    private System.Windows.Forms.Button btnParsingEbayStop;
    private System.Windows.Forms.Button btnParsingEbayPause;
    private System.Windows.Forms.Button btnParsingEbayStart;
    private System.Windows.Forms.GroupBox gbAvitoEbay;
    private System.Windows.Forms.Button btnParsingAvitoEbayStop;
    private System.Windows.Forms.Button btnParsingAvitoEbayPause;
    private System.Windows.Forms.Button btnParsingAvitoEbayStart;
  }
}

