using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvitoRuslanParser
{
  public partial class frmSettings : Form
  {
    private bool ForceRestart;
    public frmSettings(bool bForceRestart = false)
    {
      InitializeComponent();
      ForceRestart = bForceRestart;
    }

    private void frmSettings_Load(object sender, EventArgs e)
    {
      if (Properties.Default.PathToImg.StartsWith("ftp://"))
        edtFtpFolder.Text = Properties.Default.PathToImg;
      else
        edtSaveImagePath.Text = Properties.Default.PathToImg;
      edtPathToProxyFile.Text = Properties.Default.PathToProxy;
      edtSleep.Text = Properties.Default.SleepSec.ToString();
      edtUsername.Text = Properties.Default.User;
      edtPassword.Text = Properties.Default.Password;
      edtMySqlServerAddress.Text = Properties.Default.MySqlServerAddress;
      edtMySqlServerPort.Text = Properties.Default.MySqlServerPort.ToString();
      edtMySqlServerDatabase.Text = Properties.Default.MySqlServerDatabase;
      edtMySqlServerUsername.Text = Properties.Default.MySqlServerUsername;
      edtMySqlServerPassword.Text = Properties.Default.MySqlServerPassword;
      edtFtpUsername.Text = Properties.Default.FtpUsername;
      edtFtpPassword.Text = Properties.Default.FtpPassword;
      cbRunSMSSpamer.Checked = Properties.Default.RunSMSSpamer;
      edtSMSSPamerPath.Text = Properties.Default.SMSSpamerPath;
      cbPublishParsedData.Checked = Properties.Default.PublishParsedData;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (edtFtpFolder.Text.Length > 0 && edtSaveImagePath.Text.Length > 0)
      {
        MessageBox.Show("You need to fill only one path: 'Path to save images' or 'FTP folder'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        DialogResult = System.Windows.Forms.DialogResult.None;
        return;
      }
      if (edtSaveImagePath.Text.Length > 0)
        Properties.Default.PathToImg = edtSaveImagePath.Text;
      else if (edtFtpFolder.Text.Length > 0)
      {
        if (edtFtpUsername.Text.Length == 0 || edtFtpPassword.Text.Length == 0)
        {
          MessageBox.Show("Incomplete FTP settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          DialogResult = System.Windows.Forms.DialogResult.None;
          return;
        }
        else
          Properties.Default.PathToImg = edtFtpFolder.Text;
      }

      if (cbRunSMSSpamer.Checked && edtSMSSPamerPath.Text.Length == 0)
      {
        MessageBox.Show("SMSSPamer path is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        DialogResult = System.Windows.Forms.DialogResult.None;
        return;
      }
      else
      {
        Properties.Default.SMSSpamerPath = edtSMSSPamerPath.Text;
        Properties.Default.RunSMSSpamer = cbRunSMSSpamer.Checked;
      }

      Properties.Default.PathToProxy = edtPathToProxyFile.Text;
      Properties.Default.SleepSec = Convert.ToInt32(edtSleep.Text);
      Properties.Default.User = edtUsername.Text;
      Properties.Default.Password = edtPassword.Text;
      if (edtMySqlServerAddress.Text.Length == 0 || edtMySqlServerDatabase.Text.Length == 0 || edtMySqlServerPort.Text.Length == 0 ||
        edtMySqlServerUsername.Text.Length == 0 || edtMySqlServerPassword.Text.Length == 0)
      {
        MessageBox.Show("Incomplete MySql settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        DialogResult = System.Windows.Forms.DialogResult.None;
        return;
      }
      bool bNeedRestart = false;
      if (Properties.Default.MySqlServerAddress != edtMySqlServerAddress.Text || Properties.Default.MySqlServerPort != Convert.ToInt32(edtMySqlServerPort.Text) ||
          Properties.Default.MySqlServerDatabase != edtMySqlServerDatabase.Text || Properties.Default.MySqlServerUsername != edtMySqlServerUsername.Text ||
          Properties.Default.MySqlServerPassword != edtMySqlServerPassword.Text)
      {
        bNeedRestart = true;
      }
      Properties.Default.MySqlServerAddress = edtMySqlServerAddress.Text;
      Properties.Default.MySqlServerPort = Convert.ToInt32(edtMySqlServerPort.Text);
      Properties.Default.MySqlServerDatabase = edtMySqlServerDatabase.Text;
      Properties.Default.MySqlServerUsername = edtMySqlServerUsername.Text;
      Properties.Default.MySqlServerPassword = edtMySqlServerPassword.Text;
      Properties.Default.FtpUsername = edtFtpUsername.Text;
      Properties.Default.FtpPassword = edtFtpPassword.Text;

      Properties.Default.PublishParsedData = cbPublishParsedData.Checked;

      Properties.Default.Save();

      // Test MySql connection

      MySqlDB db = new MySqlDB(Properties.Default.MySqlServerUsername, Properties.Default.MySqlServerPassword, Properties.Default.MySqlServerAddress, Properties.Default.MySqlServerPort, Properties.Default.MySqlServerDatabase);
      try
      {
        var r = db.mySqlConnection;
      }
      catch (Exception)
      {
        MessageBox.Show("Невозможно установить соединение с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        DialogResult = DialogResult.Abort;
        bNeedRestart = false;
        ForceRestart = false;
      }
      db.Close();

      if (bNeedRestart || ForceRestart)
      {
        MessageBox.Show("Для внесения изменения программа будет перезапущена", "Перезапуск", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Application.Restart();
      }
    }

    private void btnSaveImagePathBrowse_Click(object sender, EventArgs e)
    {
      if (edtSaveImagePath.Text.Length > 0 && edtSaveImagePath.Text.StartsWith("ftp://") == false)
      {
        dlgBrowser.SelectedPath = edtSaveImagePath.Text;
      }
      dlgBrowser.ShowNewFolderButton = true;
      if (dlgBrowser.ShowDialog() == DialogResult.OK)
      {
        edtSaveImagePath.Text = dlgBrowser.SelectedPath;
      }
    }

    private void btnProxyFileBrowse_Click(object sender, EventArgs e)
    {
      if (edtPathToProxyFile.Text.Length > 0)
      {
        dlgOpenFile.FileName = edtPathToProxyFile.Text;
      }
      if (dlgOpenFile.ShowDialog() == DialogResult.OK)
      {
        edtPathToProxyFile.Text = dlgOpenFile.FileName;
      }
    }

    private void btnBrowserSMSSpamerPath_Click(object sender, EventArgs e)
    {
      dlgOpenFile.Filter = "SMSSpamer Application|SMSSpamer.exe";
      if (dlgOpenFile.ShowDialog() == DialogResult.OK)
      {
        edtSMSSPamerPath.Text = dlgOpenFile.FileName;
      }
    }
  }
}
