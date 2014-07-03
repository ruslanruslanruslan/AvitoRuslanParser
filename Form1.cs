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
using LogsForms;
using AvitoRuslanParser.EbayParser;
using AvitoRuslanParser.Helpfuls;
using ParsersChe.WebClientParser;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        int sleepSec = -1;
        private int countParsed = 0;
        private int countInseted = 0;
        public static string URLLink;
        private LogsForm logsForm;

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

        }

        public void IncParsed()
        {
            countParsed++;
            labelParsed.Text = countParsed.ToString();
        }
        public void incInseted()
        {
            countInseted++;
            labelInserted.Text = countInseted.ToString();
        }

        public void SetZeroCounters()
        {
            countInseted = 0;
            countParsed = 0;
            labelParsed.Text = "0";
            labelInserted.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadField();
            {
                MySqlCommand command = new MySqlCommand(); ;
                string connectionString, commandString;
                connectionString = "server=localhost;user=root;database=playandbay;port=3306;password=Galka91;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                commandString = @"select concat(d1.s_name, "" | "", coalesce(d2.s_name,""-""), "" | "", coalesce(d3.s_name,""-""), "" | "", coalesce(d4.s_name,""-"")) s_name,
                                         coalesce(c4.pk_i_id, c3.pk_i_id, c2.pk_i_id, c1.pk_i_id) id
                                  from playandbay.oc_t_category c1
                                  left join playandbay.oc_t_category c2 on c1.pk_i_id=c2.fk_i_parent_id
                                  left join playandbay.oc_t_category c3 on c2.pk_i_id=c3.fk_i_parent_id
                                  left join playandbay.oc_t_category c4 on c3.pk_i_id=c4.fk_i_parent_id
                                  left join playandbay.oc_t_category_description d1 on c1.pk_i_id=d1.fk_i_category_id
                                  left join playandbay.oc_t_category_description d2 on c2.pk_i_id=d2.fk_i_category_id
                                  left join playandbay.oc_t_category_description d3 on c3.pk_i_id=d3.fk_i_category_id
                                  left join playandbay.oc_t_category_description d4 on c4.pk_i_id=d4.fk_i_category_id
                                  where d1.fk_c_locale_code = ""ru_RU"" 
                                        and (d2.fk_c_locale_code = ""ru_RU"" or d2.fk_c_locale_code is null)
                                        and (d3.fk_c_locale_code = ""ru_RU"" or d3.fk_c_locale_code is null)
                                        and (d4.fk_c_locale_code = ""ru_RU"" or d4.fk_c_locale_code is null)
                                        and c1.fk_i_parent_id is null
                                  order by d1.s_name, d2.s_name, d3.s_name, d4.s_name;";
                command.CommandText = commandString;
                command.Connection = connection;
                MySqlDataReader reader;
                try
                {
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["s_name"]);
                    }
                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: \r\n{0}", ex.ToString());
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            SaveField();
        }
        //Методот по события нажатия кнопки

        private void OneLinkEbay()
        {
            long id = Convert.ToInt64(Regex.Match(URLLink, "\\d{7,}").Value);
            var imgParser = new EbayLoadImage(new WebCl(), pathToImgtextBox.Text);
            var parsedItems = SearchApi.ParseItems(new long[] { id });


            MySqlDB2.InsertFctEbayGrabber(parsedItems, listBox1.Text);
            bool isAuction = true;

            if (parsedItems != null && parsedItems.Item != null && parsedItems.Item.Count() > 0)
            {
                imgParser.LoadImages(parsedItems.Item[0].PictureURL);
                isAuction = parsedItems.Item[0].BuyItNowAvailable != null;

            }

            if (!isAuction)
                MySqlDB2.ExecuteProc2();

        }
        private void OneLinkAvito()
        {
            try
            {
                label6.Text = "Start";
                //Ссылка на обьявление
                MySqlDB.DeleteUnTransformated();
                //Создаем класс и вводим параметры 
                var Parser = new RuslanParser(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);
                Parser.PathImages = pathToImgtextBox.Text;
                //вот тут мы вызывем запрос на Id к базе
                //Parser.LoadGuid = (() => MySqlDB.SeletGuid());
                //Parser.LoadGuid = (() => "1532");
                //тут мы присваем результат переменной result
                var result = Parser.Run(URLLink);
                // тут мы передаем в метод вставки данные
                //result, index ID, ссылку на обьявления
                //id я не вставлял так как непонятно было и неподходило под структуру бд
                MySqlDB.InsertFctAvitoGrabber(result, MySqlDB.ResourceListID(), URLLink, listBox1.Text);
                //MessageBox.Show(MySqlDB.PictureID());
                //MessageBox.Show(MySqlDB.PictureListID());
                var Parser2 = new RuslanParser2(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);
                Parser2.PathImages2 = pathToImgtextBox.Text;
                var result2 = Parser2.Run(URLLink);
                MySqlDB.ExecuteProc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            label6.Text = "Finish";
        }
        private void button1_Click(object sender, EventArgs e)
        {//Проверку на пустые знаяения полей
            if (!string.IsNullOrEmpty(LinkAdtextBox.Text) && !string.IsNullOrEmpty(userNametextBox.Text)
                && !string.IsNullOrEmpty(PasswordtextBox.Text) && !string.IsNullOrEmpty(pathToProxytextBox.Text)
                && !string.IsNullOrEmpty(pathToImgtextBox.Text))
            {
                URLLink = LinkAdtextBox.Text;
                var uri = new Uri(URLLink);
                if (uri.Host == "www.avito.ru") { OneLinkAvito(); }
                else if (uri.Host == "www.ebay.com") { OneLinkEbay(); }
            }
        }

        private void LoadSection(string[] linkSection)
        {
            if (!string.IsNullOrEmpty(LinkAdtextBox.Text) && !string.IsNullOrEmpty(userNametextBox.Text)
               && !string.IsNullOrEmpty(PasswordtextBox.Text) && !string.IsNullOrEmpty(pathToProxytextBox.Text)
               && !string.IsNullOrEmpty(pathToImgtextBox.Text))
            {
                try
                {
                    label6.Text = "Start";

                    //Ссылка на обьявление
                    //  URLLink = LinkAdtextBox.Text;
                    //Создаем класс и вводим параметры 
                    var Parser = new RuslanParser(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);
                    var Parser2 = new RuslanParser2(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);

                    Parser.PathImages = pathToImgtextBox.Text;

                    var linksAds = Parser.LoadLinks(linkSection[0]);
                    logsForm.AddLog(linkSection[1]);
                    logsForm.AddLog("Count new ad: " + linksAds.Count().ToString());
                    int i = 0;
                    int countPre = 0;
                    int countIns = 0;
                    foreach (var item in linksAds)
                    {
                        logsForm.AddLog("start parse link: " + item);
                        i++;
                        if (i == 25)
                        {
                            i = -1;
                        }
                        URLLink = item;
                        //  MySqlDB.DeleteUnTransformated();
                        var result = Parser.Run(item);
                        //  result["Phone"] = result["Phone"].ToString().Split('"')[3];
                        //
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine();
                        foreach (var element in result)
                        {
                            if (element.Key != PartsPage.Body)
                            {
                                sb.Append(element.Key + " - ");
                                if (element.Value != null)
                                    foreach (var t in element.Value)
                                    {
                                        sb.Append(t + " |");
                                    }
                                sb.Append(Environment.NewLine);
                            }
                        }

                        logsForm.AddLog(sb.ToString());
                        IncParsed();
                        countPre++;
                        if (result[PartsPage.Cost] != null)
                        {
                            logsForm.AddLog("preparing ad to insert to db");
                            MySqlDB.InsertFctAvitoGrabber(result, MySqlDB.ResourceListID(), item, linkSection[1]);
                            logsForm.AddLog("ad inserted");
                            incInseted();

                            Parser2.PathImages2 = pathToImgtextBox.Text;

                            var result2 = Parser2.Run(item);
                            MySqlDB.ExecuteProc();
                            countIns++;

                        }

                        if (sleepSec == -1) sleepSec = Convert.ToInt32(textBoxSleep.Text);
                        logsForm.AddLog("sleep on. " + sleepSec + " sec");
                        Thread.Sleep(sleepSec * 1000);
                        logsForm.AddLog("sleep off" + Environment.NewLine + Environment.NewLine);
                    }
                    logsForm.AddLogStatistic(linkSection[1], MySqlDB.CountAd, countIns);

                }
                catch (Exception ex)
                {
                    logsForm.AddLog(ex.ToString());
                    MessageBox.Show("Error" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                }
                label6.Text = "Finish";
                // logger.Info("fsnish");
            }
        }
        #region fieldSaveLoad
        private void SaveField()
        {
            Properties.Default.User = userNametextBox.Text;
            Properties.Default.Password = PasswordtextBox.Text;
            Properties.Default.LinkOnAd = LinkAdtextBox.Text;
            Properties.Default.PathToImg = pathToImgtextBox.Text;
            Properties.Default.PathToProxy = pathToProxytextBox.Text;
            Properties.Default.Save();

        }
        private void LoadField()
        {
            userNametextBox.Text = Properties.Default.User;
            PasswordtextBox.Text = Properties.Default.Password;
            LinkAdtextBox.Text = Properties.Default.LinkOnAd;
            pathToImgtextBox.Text = Properties.Default.PathToImg;
            pathToProxytextBox.Text = Properties.Default.PathToProxy;
        }
        #endregion

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            logsForm = new LogsForm();
            logsForm.Visible = true;
            SetZeroCounters();
            var links = MySqlDB.LoadSectionsLink();
            Task.Factory.StartNew(() =>
            {
                button2.Enabled = false;
                foreach (var item in links)
                {
                    logsForm.AddLog("start next sections");
                    LoadSection(item);
                    //    Thread.Sleep(25 * 1000);
                }
                ProxyCollectionSingl.Instance.Dispose();
                button2.Enabled = true;
            });

        }

        private void textBoxSleep_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadSectionEbay(SectionItem sectionItem)
        {
            int cryticalCount = 8;
            var searchApi = new SearchApi();
            searchApi.PerPage = 100;
            searchApi.Section = sectionItem.Link;

            logsForm.AddLog(sectionItem.CategoryName);

            var ids = searchApi.SearchLinks();
            logsForm.AddLog("Count new ad: " + ids.Count().ToString());

            IList<long> newIds = new List<long>();
            int countCurrentRepeat = 0;

            foreach (var item in ids)
            {
                if (countCurrentRepeat > cryticalCount) break;
                if (MySqlDB2.IsNewAdEbay(item))
                {
                    newIds.Add(item);
                }
                else
                {
                    countCurrentRepeat++;
                }
            }
            //       if (newIds.Count == 0) return;

            var partsIdsCollection = Helpful.Partition<long>(newIds, 1);
            logsForm.AddLog("Prepared  insert to db");

            var imgParser = new EbayLoadImage(new WebCl(), pathToImgtextBox.Text);

            foreach (var item in partsIdsCollection)
            {
                var parsedItems = SearchApi.ParseItems(item);
                foreach (var unit in parsedItems.Item)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(unit.ViewItemURLForNaturalSearch);
                    sb.AppendLine(unit.Title);
                    sb.AppendLine("cost: " + unit.CurrentPrice.Value.ToString());
                    sb.AppendLine("country: " + unit.Country);
                    sb.AppendLine("city: " + unit.Location);
                    sb.AppendLine("author: " + unit.Seller.UserID);
                    sb.AppendLine("ebay section: " + unit.PrimaryCategoryName);
                    logsForm.AddLog(sb.ToString());
                }

                logsForm.AddLog("preparing ad to insert to db");
                MySqlDB2.InsertFctEbayGrabber(parsedItems, sectionItem.CategoryName);
                bool isAuction = true;
                if (parsedItems != null && parsedItems.Item != null && parsedItems.Item.Count() > 0)
                {
                    imgParser.LoadImages(parsedItems.Item[0].PictureURL);
                    isAuction = parsedItems.Item[0].TimeLeft != null;
                }
                 // if (!isAuction)
                MySqlDB2.ExecuteProc2();
                logsForm.AddLog("ad inserted" + Environment.NewLine);
            }

            logsForm.AddLog("Inserted to db");
            logsForm.AddLog("Inserted: " + newIds.Count().ToString());
        }

        private void LoadSectionEbayAvito(SectionItem sectionItem)
        {
            if (sectionItem.site == SectionItem.Site.Avito)
            {
                LoadSection(new string[] { sectionItem.Link, sectionItem.CategoryName });
            }
            else
            {
                LoadSectionEbay(sectionItem);
            }
        }
        private void buttonParsingEbay_Click(object sender, EventArgs e)
        {
            logsForm = new LogsForm();
            logsForm.Visible = true;
            SetZeroCounters();
            var links = MySqlDB2.LoadSectionLinkEbay();
            Task.Factory.StartNew(() =>
            {
                buttonParsingEbay.Enabled = false;

                if (true)
                {
                    logsForm.AddLog("start update auctions");
                    var auctionlinks = MySqlDB2.LoadAuctionLink();
                    foreach (long item in auctionlinks)
                    {
                        logsForm.AddLog("update auction: " + item.ToString());
                        var parsedItems = SearchApi.ParseItems(new long[] { item });
                        MySqlDB2.UpdateAuction(parsedItems);
                    }
                    logsForm.AddLog("finish update auctions" + Environment.NewLine);
                }

                foreach (var item in links)
                {
                    logsForm.AddLog("start next sections");
                    LoadSectionEbay(item);
                    //    Thread.Sleep(25 * 1000);
                }
                buttonParsingEbay.Enabled = true;
            });
        }

        private void buttonParsingAvitoEbay_Click(object sender, EventArgs e)
        {
            logsForm = new LogsForm();
            logsForm.Visible = true;
            SetZeroCounters();
            var links = MySqlDB2.LoadSectionsLink();
            Task.Factory.StartNew(() =>
            {
                buttonParsingAvitoEbay.Enabled = false;

                if (true)
                {
                    logsForm.AddLog("start update auctions");
                    var auctionlinks = MySqlDB2.LoadAuctionLink();
                    foreach (long item in auctionlinks)
                    {
                        logsForm.AddLog("update auction: " + item.ToString());
                        var parsedItems = SearchApi.ParseItems(new long[] { item });
                        MySqlDB2.UpdateAuction(parsedItems);
                    }
                    logsForm.AddLog("finish update auctions" + Environment.NewLine);
                }
                foreach (var item in links)
                {
                    logsForm.AddLog("start next sections");
                    logsForm.AddLog(item.site.ToString());
                    LoadSectionEbayAvito(item);
                    //    Thread.Sleep(25 * 1000);
                }
                ProxyCollectionSingl.Instance.Dispose();
                buttonParsingAvitoEbay.Enabled = true;
            });
        }
    }
}
