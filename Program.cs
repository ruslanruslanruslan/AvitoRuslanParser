using AvitoRuslanParser.EbayParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;
using System.Security;
using AvitoRuslanParser;

namespace AvitoRuslanParser
{
  class Program
  {
    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    [HandleProcessCorruptedStateExceptions]
    [SecurityCritical]
    static void Main()
    {
      try
      {
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new frmMain());
      }
      catch(Exception ex)
      {
        MessageBox.Show("Exception:" + Environment.NewLine + ex.Message + Environment.NewLine + "Stack trace:" + Environment.NewLine + ex.StackTrace);
      }
    }

    static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs ex)
    {
      MessageBox.Show("Exception:" + Environment.NewLine + ex.ExceptionObject.ToString());
    }


  }
}
