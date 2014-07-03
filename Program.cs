using AvitoRuslanParser.EbayParser;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace AvitoRuslanParser
{
    class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
         //   SearchApi.ParseItems(new long[] { 201117381586, 191228445949, 141318265370 });
           // Logger logger = LogManager.GetCurrentClassLogger();
           // logger.Info("hello");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        
    }
}
