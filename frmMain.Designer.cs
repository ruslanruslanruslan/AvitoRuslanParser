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
      this.btnEnter = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.LinkAdtextBox = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.btnParsingAvito = new System.Windows.Forms.Button();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.labelParsed = new System.Windows.Forms.Label();
      this.labelInserted = new System.Windows.Forms.Label();
      this.buttonParsingEbay = new System.Windows.Forms.Button();
      this.buttonParsingAvitoEbay = new System.Windows.Forms.Button();
      this.btnSettings = new System.Windows.Forms.Button();
      this.cbCategories = new System.Windows.Forms.ComboBox();
      this.Label2 = new System.Windows.Forms.Label();
      this.btnReset = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rtbLog = new System.Windows.Forms.RichTextBox();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.rtbLogStatistics = new System.Windows.Forms.RichTextBox();
      this.groupBox1.SuspendLayout();
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
      // btnParsingAvito
      // 
      this.btnParsingAvito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnParsingAvito.Location = new System.Drawing.Point(748, 307);
      this.btnParsingAvito.Name = "btnParsingAvito";
      this.btnParsingAvito.Size = new System.Drawing.Size(184, 23);
      this.btnParsingAvito.TabIndex = 12;
      this.btnParsingAvito.Text = "Start Parsing Avito";
      this.btnParsingAvito.UseVisualStyleBackColor = true;
      this.btnParsingAvito.Click += new System.EventHandler(this.btnParsingAvito_Click);
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
      // buttonParsingEbay
      // 
      this.buttonParsingEbay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonParsingEbay.Location = new System.Drawing.Point(748, 343);
      this.buttonParsingEbay.Name = "buttonParsingEbay";
      this.buttonParsingEbay.Size = new System.Drawing.Size(184, 23);
      this.buttonParsingEbay.TabIndex = 18;
      this.buttonParsingEbay.Text = "Start Parsing Ebay";
      this.buttonParsingEbay.UseVisualStyleBackColor = true;
      this.buttonParsingEbay.Click += new System.EventHandler(this.buttonParsingEbay_Click);
      // 
      // buttonParsingAvitoEbay
      // 
      this.buttonParsingAvitoEbay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonParsingAvitoEbay.Location = new System.Drawing.Point(748, 378);
      this.buttonParsingAvitoEbay.Name = "buttonParsingAvitoEbay";
      this.buttonParsingAvitoEbay.Size = new System.Drawing.Size(184, 23);
      this.buttonParsingAvitoEbay.TabIndex = 19;
      this.buttonParsingAvitoEbay.Text = "Start Parsing Avito+Ebay";
      this.buttonParsingAvitoEbay.UseVisualStyleBackColor = true;
      this.buttonParsingAvitoEbay.Click += new System.EventHandler(this.buttonParsingAvitoEbay_Click);
      // 
      // btnSettings
      // 
      this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSettings.Location = new System.Drawing.Point(558, 378);
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
      this.groupBox1.Size = new System.Drawing.Size(927, 238);
      this.groupBox1.TabIndex = 26;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Logs";
      // 
      // rtbLog
      // 
      this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbLog.Location = new System.Drawing.Point(3, 16);
      this.rtbLog.Name = "rtbLog";
      this.rtbLog.ReadOnly = true;
      this.rtbLog.Size = new System.Drawing.Size(921, 120);
      this.rtbLog.TabIndex = 0;
      this.rtbLog.Text = "";
      this.rtbLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLog_LinkClicked);
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter1.Location = new System.Drawing.Point(3, 136);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(921, 3);
      this.splitter1.TabIndex = 1;
      this.splitter1.TabStop = false;
      // 
      // rtbLogStatistics
      // 
      this.rtbLogStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.rtbLogStatistics.Location = new System.Drawing.Point(3, 139);
      this.rtbLogStatistics.Name = "rtbLogStatistics";
      this.rtbLogStatistics.ReadOnly = true;
      this.rtbLogStatistics.Size = new System.Drawing.Size(921, 96);
      this.rtbLogStatistics.TabIndex = 2;
      this.rtbLogStatistics.Text = "";
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(944, 411);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btnReset);
      this.Controls.Add(this.Label2);
      this.Controls.Add(this.cbCategories);
      this.Controls.Add(this.btnSettings);
      this.Controls.Add(this.buttonParsingAvitoEbay);
      this.Controls.Add(this.buttonParsingEbay);
      this.Controls.Add(this.labelInserted);
      this.Controls.Add(this.labelParsed);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.btnParsingAvito);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.LinkAdtextBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnEnter);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Parser";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_Closing);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox LinkAdtextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnParsingAvito;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelParsed;
        private System.Windows.Forms.Label labelInserted;
        private System.Windows.Forms.Button buttonParsingEbay;
        private System.Windows.Forms.Button buttonParsingAvitoEbay;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ComboBox cbCategories;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox rtbLogStatistics;
    }
}

