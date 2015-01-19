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
    public frmSettings()
    {
      InitializeComponent();
    }

    private void frmSettings_Load(object sender, EventArgs e)
    {
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
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      Properties.Default.PathToImg = edtSaveImagePath.Text;
      Properties.Default.PathToProxy = edtPathToProxyFile.Text;
      Properties.Default.SleepSec = Convert.ToInt32(edtSleep.Text);
      Properties.Default.User = edtUsername.Text;
      Properties.Default.Password = edtPassword.Text;
      Properties.Default.MySqlServerAddress = edtMySqlServerAddress.Text;
      Properties.Default.MySqlServerPort = Convert.ToInt32(edtMySqlServerPort.Text);
      Properties.Default.MySqlServerDatabase = edtMySqlServerDatabase.Text;
      Properties.Default.MySqlServerUsername = edtMySqlServerUsername.Text;
      Properties.Default.MySqlServerPassword = edtMySqlServerPassword.Text;
      Properties.Default.FtpUsername = edtFtpUsername.Text;
      Properties.Default.FtpPassword = edtFtpPassword.Text;

      Properties.Default.Save();
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
  }
}
