﻿namespace AvitoRuslanParser
{
  partial class frmSettings
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabMain = new System.Windows.Forms.TabPage();
      this.edtSleepSecAfterPublication = new System.Windows.Forms.TextBox();
      this.label15 = new System.Windows.Forms.Label();
      this.cbPublishParsedData = new System.Windows.Forms.CheckBox();
      this.btnBrowserSMSSpamerPath = new System.Windows.Forms.Button();
      this.edtSMSSPamerPath = new System.Windows.Forms.TextBox();
      this.cbRunSMSSpamer = new System.Windows.Forms.CheckBox();
      this.edtSleepAfterParse = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.btnSaveImagePathBrowse = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.edtSaveImagePath = new System.Windows.Forms.TextBox();
      this.tabMySql = new System.Windows.Forms.TabPage();
      this.label14 = new System.Windows.Forms.Label();
      this.edtMySqlServerPassword = new System.Windows.Forms.TextBox();
      this.edtMySqlServerUsername = new System.Windows.Forms.TextBox();
      this.edtMySqlServerDatabase = new System.Windows.Forms.TextBox();
      this.edtMySqlServerPort = new System.Windows.Forms.TextBox();
      this.edtMySqlServerAddress = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.tabFTP = new System.Windows.Forms.TabPage();
      this.edtFtpFolder = new System.Windows.Forms.TextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.edtFtpPassword = new System.Windows.Forms.TextBox();
      this.edtFtpUsername = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.tabProxy = new System.Windows.Forms.TabPage();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.edtPassword = new System.Windows.Forms.TextBox();
      this.edtUsername = new System.Windows.Forms.TextBox();
      this.edtPathToProxyFile = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnProxyFileBrowse = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.dlgBrowser = new System.Windows.Forms.FolderBrowserDialog();
      this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
      this.cbInfiniteParsing = new System.Windows.Forms.CheckBox();
      this.tabControl1.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabMySql.SuspendLayout();
      this.tabFTP.SuspendLayout();
      this.tabProxy.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabMain);
      this.tabControl1.Controls.Add(this.tabMySql);
      this.tabControl1.Controls.Add(this.tabFTP);
      this.tabControl1.Controls.Add(this.tabProxy);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(560, 261);
      this.tabControl1.TabIndex = 0;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.cbInfiniteParsing);
      this.tabMain.Controls.Add(this.edtSleepSecAfterPublication);
      this.tabMain.Controls.Add(this.label15);
      this.tabMain.Controls.Add(this.cbPublishParsedData);
      this.tabMain.Controls.Add(this.btnBrowserSMSSpamerPath);
      this.tabMain.Controls.Add(this.edtSMSSPamerPath);
      this.tabMain.Controls.Add(this.cbRunSMSSpamer);
      this.tabMain.Controls.Add(this.edtSleepAfterParse);
      this.tabMain.Controls.Add(this.label3);
      this.tabMain.Controls.Add(this.btnSaveImagePathBrowse);
      this.tabMain.Controls.Add(this.label1);
      this.tabMain.Controls.Add(this.edtSaveImagePath);
      this.tabMain.Location = new System.Drawing.Point(4, 22);
      this.tabMain.Name = "tabMain";
      this.tabMain.Padding = new System.Windows.Forms.Padding(3);
      this.tabMain.Size = new System.Drawing.Size(552, 235);
      this.tabMain.TabIndex = 0;
      this.tabMain.Text = "Main";
      this.tabMain.UseVisualStyleBackColor = true;
      // 
      // edtSleepSecAfterPublication
      // 
      this.edtSleepSecAfterPublication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtSleepSecAfterPublication.Location = new System.Drawing.Point(172, 71);
      this.edtSleepSecAfterPublication.Name = "edtSleepSecAfterPublication";
      this.edtSleepSecAfterPublication.Size = new System.Drawing.Size(296, 20);
      this.edtSleepSecAfterPublication.TabIndex = 10;
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(8, 74);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(138, 13);
      this.label15.TabIndex = 9;
      this.label15.Text = "Sleep after publication (sec)";
      // 
      // cbPublishParsedData
      // 
      this.cbPublishParsedData.AutoSize = true;
      this.cbPublishParsedData.Checked = true;
      this.cbPublishParsedData.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbPublishParsedData.Location = new System.Drawing.Point(11, 182);
      this.cbPublishParsedData.Name = "cbPublishParsedData";
      this.cbPublishParsedData.Size = new System.Drawing.Size(119, 17);
      this.cbPublishParsedData.TabIndex = 8;
      this.cbPublishParsedData.Text = "Publish parsed data";
      this.cbPublishParsedData.UseVisualStyleBackColor = true;
      this.cbPublishParsedData.Visible = false;
      // 
      // btnBrowserSMSSpamerPath
      // 
      this.btnBrowserSMSSpamerPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBrowserSMSSpamerPath.Location = new System.Drawing.Point(474, 96);
      this.btnBrowserSMSSpamerPath.Name = "btnBrowserSMSSpamerPath";
      this.btnBrowserSMSSpamerPath.Size = new System.Drawing.Size(75, 23);
      this.btnBrowserSMSSpamerPath.TabIndex = 7;
      this.btnBrowserSMSSpamerPath.Text = "Browse";
      this.btnBrowserSMSSpamerPath.UseVisualStyleBackColor = true;
      this.btnBrowserSMSSpamerPath.Click += new System.EventHandler(this.btnBrowserSMSSpamerPath_Click);
      // 
      // edtSMSSPamerPath
      // 
      this.edtSMSSPamerPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtSMSSPamerPath.Location = new System.Drawing.Point(172, 98);
      this.edtSMSSPamerPath.Name = "edtSMSSPamerPath";
      this.edtSMSSPamerPath.Size = new System.Drawing.Size(296, 20);
      this.edtSMSSPamerPath.TabIndex = 6;
      // 
      // cbRunSMSSpamer
      // 
      this.cbRunSMSSpamer.AutoSize = true;
      this.cbRunSMSSpamer.Location = new System.Drawing.Point(11, 101);
      this.cbRunSMSSpamer.Name = "cbRunSMSSpamer";
      this.cbRunSMSSpamer.Size = new System.Drawing.Size(108, 17);
      this.cbRunSMSSpamer.TabIndex = 5;
      this.cbRunSMSSpamer.Text = "Run SMSSpamer";
      this.cbRunSMSSpamer.UseVisualStyleBackColor = true;
      // 
      // edtSleepAfterParse
      // 
      this.edtSleepAfterParse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtSleepAfterParse.Location = new System.Drawing.Point(172, 45);
      this.edtSleepAfterParse.Name = "edtSleepAfterParse";
      this.edtSleepAfterParse.Size = new System.Drawing.Size(296, 20);
      this.edtSleepAfterParse.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(8, 48);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(113, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Sleep after parse (sec)";
      // 
      // btnSaveImagePathBrowse
      // 
      this.btnSaveImagePathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveImagePathBrowse.Location = new System.Drawing.Point(474, 11);
      this.btnSaveImagePathBrowse.Name = "btnSaveImagePathBrowse";
      this.btnSaveImagePathBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnSaveImagePathBrowse.TabIndex = 2;
      this.btnSaveImagePathBrowse.Text = "Browse";
      this.btnSaveImagePathBrowse.UseVisualStyleBackColor = true;
      this.btnSaveImagePathBrowse.Click += new System.EventHandler(this.btnSaveImagePathBrowse_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(103, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Path to save images";
      // 
      // edtSaveImagePath
      // 
      this.edtSaveImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtSaveImagePath.Location = new System.Drawing.Point(172, 13);
      this.edtSaveImagePath.Name = "edtSaveImagePath";
      this.edtSaveImagePath.Size = new System.Drawing.Size(296, 20);
      this.edtSaveImagePath.TabIndex = 0;
      // 
      // tabMySql
      // 
      this.tabMySql.Controls.Add(this.label14);
      this.tabMySql.Controls.Add(this.edtMySqlServerPassword);
      this.tabMySql.Controls.Add(this.edtMySqlServerUsername);
      this.tabMySql.Controls.Add(this.edtMySqlServerDatabase);
      this.tabMySql.Controls.Add(this.edtMySqlServerPort);
      this.tabMySql.Controls.Add(this.edtMySqlServerAddress);
      this.tabMySql.Controls.Add(this.label10);
      this.tabMySql.Controls.Add(this.label9);
      this.tabMySql.Controls.Add(this.label8);
      this.tabMySql.Controls.Add(this.label7);
      this.tabMySql.Controls.Add(this.label6);
      this.tabMySql.Location = new System.Drawing.Point(4, 22);
      this.tabMySql.Name = "tabMySql";
      this.tabMySql.Padding = new System.Windows.Forms.Padding(3);
      this.tabMySql.Size = new System.Drawing.Size(552, 235);
      this.tabMySql.TabIndex = 1;
      this.tabMySql.Text = "MySql";
      this.tabMySql.UseVisualStyleBackColor = true;
      // 
      // label14
      // 
      this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label14.Location = new System.Drawing.Point(8, 175);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(541, 23);
      this.label14.TabIndex = 10;
      this.label14.Text = "Need program restart to apply database changes";
      this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // edtMySqlServerPassword
      // 
      this.edtMySqlServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerPassword.Location = new System.Drawing.Point(62, 114);
      this.edtMySqlServerPassword.Name = "edtMySqlServerPassword";
      this.edtMySqlServerPassword.PasswordChar = '*';
      this.edtMySqlServerPassword.Size = new System.Drawing.Size(487, 20);
      this.edtMySqlServerPassword.TabIndex = 9;
      // 
      // edtMySqlServerUsername
      // 
      this.edtMySqlServerUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerUsername.Location = new System.Drawing.Point(62, 88);
      this.edtMySqlServerUsername.Name = "edtMySqlServerUsername";
      this.edtMySqlServerUsername.Size = new System.Drawing.Size(487, 20);
      this.edtMySqlServerUsername.TabIndex = 8;
      // 
      // edtMySqlServerDatabase
      // 
      this.edtMySqlServerDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerDatabase.Location = new System.Drawing.Point(62, 62);
      this.edtMySqlServerDatabase.Name = "edtMySqlServerDatabase";
      this.edtMySqlServerDatabase.Size = new System.Drawing.Size(487, 20);
      this.edtMySqlServerDatabase.TabIndex = 7;
      // 
      // edtMySqlServerPort
      // 
      this.edtMySqlServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerPort.Location = new System.Drawing.Point(62, 36);
      this.edtMySqlServerPort.Name = "edtMySqlServerPort";
      this.edtMySqlServerPort.Size = new System.Drawing.Size(487, 20);
      this.edtMySqlServerPort.TabIndex = 6;
      // 
      // edtMySqlServerAddress
      // 
      this.edtMySqlServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerAddress.Location = new System.Drawing.Point(62, 10);
      this.edtMySqlServerAddress.Name = "edtMySqlServerAddress";
      this.edtMySqlServerAddress.Size = new System.Drawing.Size(487, 20);
      this.edtMySqlServerAddress.TabIndex = 5;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(8, 117);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(53, 13);
      this.label10.TabIndex = 4;
      this.label10.Text = "Password";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(8, 91);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(29, 13);
      this.label9.TabIndex = 3;
      this.label9.Text = "User";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(8, 65);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(53, 13);
      this.label8.TabIndex = 2;
      this.label8.Text = "Database";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(8, 39);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(26, 13);
      this.label7.TabIndex = 1;
      this.label7.Text = "Port";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 13);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(38, 13);
      this.label6.TabIndex = 0;
      this.label6.Text = "Server";
      // 
      // tabFTP
      // 
      this.tabFTP.Controls.Add(this.edtFtpFolder);
      this.tabFTP.Controls.Add(this.label13);
      this.tabFTP.Controls.Add(this.edtFtpPassword);
      this.tabFTP.Controls.Add(this.edtFtpUsername);
      this.tabFTP.Controls.Add(this.label12);
      this.tabFTP.Controls.Add(this.label11);
      this.tabFTP.Location = new System.Drawing.Point(4, 22);
      this.tabFTP.Name = "tabFTP";
      this.tabFTP.Padding = new System.Windows.Forms.Padding(3);
      this.tabFTP.Size = new System.Drawing.Size(552, 235);
      this.tabFTP.TabIndex = 2;
      this.tabFTP.Text = "FTP";
      this.tabFTP.UseVisualStyleBackColor = true;
      // 
      // edtFtpFolder
      // 
      this.edtFtpFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtFtpFolder.Location = new System.Drawing.Point(69, 13);
      this.edtFtpFolder.Name = "edtFtpFolder";
      this.edtFtpFolder.Size = new System.Drawing.Size(475, 20);
      this.edtFtpFolder.TabIndex = 5;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(8, 16);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(56, 13);
      this.label13.TabIndex = 4;
      this.label13.Text = "FTP folder";
      // 
      // edtFtpPassword
      // 
      this.edtFtpPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtFtpPassword.Location = new System.Drawing.Point(69, 69);
      this.edtFtpPassword.Name = "edtFtpPassword";
      this.edtFtpPassword.PasswordChar = '*';
      this.edtFtpPassword.Size = new System.Drawing.Size(475, 20);
      this.edtFtpPassword.TabIndex = 3;
      // 
      // edtFtpUsername
      // 
      this.edtFtpUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtFtpUsername.Location = new System.Drawing.Point(69, 39);
      this.edtFtpUsername.Name = "edtFtpUsername";
      this.edtFtpUsername.Size = new System.Drawing.Size(475, 20);
      this.edtFtpUsername.TabIndex = 2;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(8, 72);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(53, 13);
      this.label12.TabIndex = 1;
      this.label12.Text = "Password";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(8, 42);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(55, 13);
      this.label11.TabIndex = 0;
      this.label11.Text = "Username";
      // 
      // tabProxy
      // 
      this.tabProxy.Controls.Add(this.label5);
      this.tabProxy.Controls.Add(this.label4);
      this.tabProxy.Controls.Add(this.edtPassword);
      this.tabProxy.Controls.Add(this.edtUsername);
      this.tabProxy.Controls.Add(this.edtPathToProxyFile);
      this.tabProxy.Controls.Add(this.label2);
      this.tabProxy.Controls.Add(this.btnProxyFileBrowse);
      this.tabProxy.Location = new System.Drawing.Point(4, 22);
      this.tabProxy.Name = "tabProxy";
      this.tabProxy.Padding = new System.Windows.Forms.Padding(3);
      this.tabProxy.Size = new System.Drawing.Size(552, 235);
      this.tabProxy.TabIndex = 3;
      this.tabProxy.Text = "Proxy";
      this.tabProxy.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(8, 67);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(53, 13);
      this.label5.TabIndex = 6;
      this.label5.Text = "Password";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(8, 41);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(55, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "Username";
      // 
      // edtPassword
      // 
      this.edtPassword.Location = new System.Drawing.Point(90, 64);
      this.edtPassword.Name = "edtPassword";
      this.edtPassword.Size = new System.Drawing.Size(378, 20);
      this.edtPassword.TabIndex = 4;
      // 
      // edtUsername
      // 
      this.edtUsername.Location = new System.Drawing.Point(90, 38);
      this.edtUsername.Name = "edtUsername";
      this.edtUsername.Size = new System.Drawing.Size(378, 20);
      this.edtUsername.TabIndex = 3;
      // 
      // edtPathToProxyFile
      // 
      this.edtPathToProxyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtPathToProxyFile.Location = new System.Drawing.Point(90, 12);
      this.edtPathToProxyFile.Name = "edtPathToProxyFile";
      this.edtPathToProxyFile.Size = new System.Drawing.Size(378, 20);
      this.edtPathToProxyFile.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(8, 15);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(85, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Path to proxy file";
      // 
      // btnProxyFileBrowse
      // 
      this.btnProxyFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnProxyFileBrowse.Location = new System.Drawing.Point(474, 10);
      this.btnProxyFileBrowse.Name = "btnProxyFileBrowse";
      this.btnProxyFileBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnProxyFileBrowse.TabIndex = 0;
      this.btnProxyFileBrowse.Text = "Browse";
      this.btnProxyFileBrowse.UseVisualStyleBackColor = true;
      this.btnProxyFileBrowse.Click += new System.EventHandler(this.btnProxyFileBrowse_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnSave);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 227);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(560, 34);
      this.panel1.TabIndex = 1;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(478, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnSave.Location = new System.Drawing.Point(397, 4);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // cbInfiniteParsing
      // 
      this.cbInfiniteParsing.AutoSize = true;
      this.cbInfiniteParsing.Location = new System.Drawing.Point(11, 124);
      this.cbInfiniteParsing.Name = "cbInfiniteParsing";
      this.cbInfiniteParsing.Size = new System.Drawing.Size(92, 17);
      this.cbInfiniteParsing.TabIndex = 11;
      this.cbInfiniteParsing.Text = "InfiniteParsing";
      this.cbInfiniteParsing.UseVisualStyleBackColor = true;
      // 
      // frmSettings
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(560, 261);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.tabControl1);
      this.Name = "frmSettings";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Settings";
      this.Load += new System.EventHandler(this.frmSettings_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabMain.PerformLayout();
      this.tabMySql.ResumeLayout(false);
      this.tabMySql.PerformLayout();
      this.tabFTP.ResumeLayout(false);
      this.tabFTP.PerformLayout();
      this.tabProxy.ResumeLayout(false);
      this.tabProxy.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabMain;
    private System.Windows.Forms.TabPage tabMySql;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TabPage tabFTP;
    private System.Windows.Forms.Button btnSaveImagePathBrowse;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox edtSaveImagePath;
    private System.Windows.Forms.FolderBrowserDialog dlgBrowser;
    private System.Windows.Forms.TabPage tabProxy;
    private System.Windows.Forms.TextBox edtPathToProxyFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnProxyFileBrowse;
    private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    private System.Windows.Forms.TextBox edtSleepAfterParse;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox edtPassword;
    private System.Windows.Forms.TextBox edtUsername;
    private System.Windows.Forms.TextBox edtMySqlServerPassword;
    private System.Windows.Forms.TextBox edtMySqlServerUsername;
    private System.Windows.Forms.TextBox edtMySqlServerDatabase;
    private System.Windows.Forms.TextBox edtMySqlServerPort;
    private System.Windows.Forms.TextBox edtMySqlServerAddress;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox edtFtpPassword;
    private System.Windows.Forms.TextBox edtFtpUsername;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox edtFtpFolder;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.CheckBox cbRunSMSSpamer;
    private System.Windows.Forms.TextBox edtSMSSPamerPath;
    private System.Windows.Forms.Button btnBrowserSMSSpamerPath;
    private System.Windows.Forms.CheckBox cbPublishParsedData;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox edtSleepSecAfterPublication;
    private System.Windows.Forms.CheckBox cbInfiniteParsing;
  }
}