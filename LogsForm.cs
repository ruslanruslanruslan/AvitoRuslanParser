using AvitoRuslanParser;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ParsersChe.WebClientParser.Proxy;
using System.Threading;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using NLog;
using System.Threading.Tasks;

namespace LogsForms
{
    public partial class LogsForm : Form
    {

        private static   Logger logger = LogManager.GetCurrentClassLogger();
        int sleepSec=-1;
        private int countParsed=0;
        private int countInseted=0;
        public static string URLLink;

        public LogsForm()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

        }

        public void IncParsed() 
        {
            countParsed++;
         //   labelParsed.Text = countParsed.ToString();
        }
        public void incInseted() 
        {
            countInseted++;
           // labelInserted.Text = countInseted.ToString();
        } 

        public void SetZeroCounters() 
        {
            countInseted = 0;
            countParsed = 0;
         //   labelParsed.Text = "0";
          //  labelInserted.Text = "0";
        }

        public void AddLog(string msg) 
        {
            richTextBox1.AppendText( DateTime.Now.ToShortTimeString() + " | " + msg + Environment.NewLine);
        }
        public void AddLogStatistic(string category, int countPrepared,int countInserted) 
        {
            richTextBox2.AppendText(category + " | count prepared: " + countPrepared.ToString() + " count inserted: " + countInserted.ToString() + Environment.NewLine);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += DateTime.Now.ToString()+Environment.NewLine;
        }

     //Методот по события нажатия кнопки
        

        
    }
}
