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
using AvitoRuslanParser.EbayParser;
using AvitoRuslanParser.Helpfuls;
using ParsersChe.WebClientParser;
using System.Text.RegularExpressions;

namespace AvitoRuslanParser
{
  public partial class frmMain : Form
  {

    private static Logger logger = LogManager.GetCurrentClassLogger();
    int sleepSec = -1;
    private int countParsed = 0;
    private int countInserted = 0;
    public static string URLLink;
    private MySqlDB mySqlDB;

    public frmMain()
    {
      InitializeComponent();
      System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
      mySqlDB = new MySqlDB(Properties.Default.MySqlServerUsername, Properties.Default.MySqlServerPassword,
        Properties.Default.MySqlServerAddress, Properties.Default.MySqlServerPort, Properties.Default.MySqlServerDatabase);
    }

    public void IncParsed()
    {
      countParsed++;
      labelParsed.Text = countParsed.ToString();
    }
    public void incInserted()
    {
      countInserted++;
      labelInserted.Text = countInserted.ToString();
    }

    public void SetZeroCounters()
    {
      countInserted = 0;
      countParsed = 0;
      labelParsed.Text = "0";
      labelInserted.Text = "0";
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      LoadField();
      try
      {
        foreach (string str in mySqlDB.GetCategories())
        {
          cbCategories.Items.Add(str);
        }
      }
      catch
      {
        frmSettings frm = new frmSettings();
        frm.ShowDialog();
        Application.Restart();
      }
    }

    private void frmMain_Closing(object sender, FormClosingEventArgs e)
    {
      SaveField();
      if (mySqlDB != null)
      {
        mySqlDB.Close();
      }
    }
    //Методот по события нажатия кнопки

    private void OneLinkEbay()
    {
      try
      {
        long id = Convert.ToInt64(Regex.Match(URLLink, "\\d{7,}").Value);
        var imgParser = new EbayLoadImage(new WebCl(), Properties.Default.PathToImg, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword);
        var parsedItems = SearchApi.ParseItems(new long[] { id });


        mySqlDB.InsertFctEbayGrabber(parsedItems, cbCategories.Text);
        bool isAuction = true;

        if (parsedItems != null && parsedItems.Item != null && parsedItems.Item.Count() > 0)
        {
          imgParser.LoadImages(parsedItems.Item[0].PictureURL);
          isAuction = parsedItems.Item[0].BuyItNowAvailable != null;

        }

        if (!isAuction)
          mySqlDB.ExecuteProcEBay(mySqlDB.ResourceListIDEbay());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }

    }
    private void OneLinkAvito()
    {
      try
      {
        label6.Text = "Start";
        //Ссылка на обьявление
        mySqlDB.DeleteUnTransformated();
        //Создаем класс и вводим параметры 
        var Parser = new RuslanParser(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB);
        Parser.PathImages = Properties.Default.PathToImg;
        //вот тут мы вызывем запрос на Id к базе
        //Parser.LoadGuid = (() => MySqlDB.SeletGuid());
        //Parser.LoadGuid = (() => "1532");
        //тут мы присваем результат переменной result
        var result = Parser.Run(URLLink);
        // тут мы передаем в метод вставки данные
        //result, index ID, ссылку на обьявления
        //id я не вставлял так как непонятно было и неподходило под структуру бд
        string idResourceList = mySqlDB.ResourceListIDAvito();
        mySqlDB.InsertFctAvitoGrabber(result, idResourceList, URLLink, cbCategories.Text);
        //MessageBox.Show(MySqlDB.PictureID());
        //MessageBox.Show(MySqlDB.PictureListID());
        var Parser2 = new RuslanParser2(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword);
        Parser2.PathImages2 = Properties.Default.PathToImg;
        var result2 = Parser2.Run(URLLink);
        mySqlDB.ExecuteProcAvito(idResourceList);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error" + Environment.NewLine + ex.ToString());
      }
      label6.Text = "Finish";
    }
    private void btnEnter_Click(object sender, EventArgs e)
    {//Проверку на пустые знаяения полей
      if (!string.IsNullOrEmpty(LinkAdtextBox.Text) /*&& !string.IsNullOrEmpty(Properties.Default.User)
          && !string.IsNullOrEmpty(Properties.Default.Password) && !string.IsNullOrEmpty(Properties.Default.PathToProxy)*/
          && !string.IsNullOrEmpty(Properties.Default.PathToImg))
      {
        URLLink = LinkAdtextBox.Text;
        var uri = new Uri(URLLink);
        if (uri.Host == "www.avito.ru") { OneLinkAvito(); }
        else if (uri.Host == "www.ebay.com") { OneLinkEbay(); }
      }
    }

    private void LoadSection(string[] linkSection)
    {
      /*if (!string.IsNullOrEmpty(LinkAdtextBox.Text) && !string.IsNullOrEmpty(userNametextBox.Text)
         && !string.IsNullOrEmpty(PasswordtextBox.Text) && !string.IsNullOrEmpty(pathToProxytextBox.Text)
         && !string.IsNullOrEmpty(pathToImgtextBox.Text))*/
      {
        try
        {
          label6.Text = "Start";

          //Ссылка на обьявление
          //  URLLink = LinkAdtextBox.Text;
          //Создаем класс и вводим параметры 
          var Parser = new RuslanParser(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB);
          var Parser2 = new RuslanParser2(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword);

          Parser.PathImages = Properties.Default.PathToImg;

          var linksAds = Parser.LoadLinks(linkSection[0]);
          AddLog(linkSection[1]);
          AddLog("Count new ad: " + linksAds.Count().ToString());
          int i = 0;
          int countPre = 0;
          int countIns = 0;
          foreach (var item in linksAds)
          {
            AddLog("start parse link: " + item);
            i++;
            if (i == 25)
            {
              i = -1;
            }
            URLLink = item;
            mySqlDB.DeleteUnTransformated();
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

            AddLog(sb.ToString());
            IncParsed();
            countPre++;
            if (result[PartsPage.Cost] != null)
            {
              AddLog("preparing ad to insert to db");
              string idResourceList = mySqlDB.ResourceListIDAvito();
              mySqlDB.InsertFctAvitoGrabber(result, idResourceList, item, linkSection[1]);
              AddLog("ad inserted");
              incInserted();

              Parser2.PathImages2 = Properties.Default.PathToImg;

              var result2 = Parser2.Run(item);
              mySqlDB.ExecuteProcAvito(idResourceList);
              countIns++;

            }

            if (sleepSec == -1) sleepSec = Properties.Default.SleepSec;
            AddLog("sleep on. " + sleepSec + " sec");
            Thread.Sleep(sleepSec * 1000);
            AddLog("sleep off" + Environment.NewLine + Environment.NewLine);
          }
          AddLogStatistic(linkSection[1], mySqlDB.CountAd, countIns);

        }
        catch (Exception ex)
        {
          AddLog(ex.ToString());
          MessageBox.Show("Error" + Environment.NewLine + ex.ToString());
        }
        label6.Text = "Finish";
        // logger.Info("fsnish");
      }
    }
    #region fieldSaveLoad
    private void SaveField()
    {
      Properties.Default.LinkOnAd = LinkAdtextBox.Text;
      Properties.Default.Save();

    }
    private void LoadField()
    {
      LinkAdtextBox.Text = Properties.Default.LinkOnAd;
    }
    #endregion

    private void btnParsingAvito_Click(object sender, EventArgs e)
    {
      SetZeroCounters();
      var links = mySqlDB.LoadSectionsLink();
      Task.Factory.StartNew(() =>
      {
        btnParsingAvito.Enabled = false;
        foreach (var item in links)
        {
          AddLog("start next sections");
          LoadSection(item);
          //    Thread.Sleep(25 * 1000);
        }

        //ProxyCollectionSingl.Instance.Dispose();
        btnParsingAvito.Enabled = true;
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

      AddLog(sectionItem.CategoryName);

      var ids = searchApi.SearchLinks();
      AddLog("Count new ad: " + ids.Count().ToString());

      IList<long> newIds = new List<long>();
      int countCurrentRepeat = 0;

      foreach (var item in ids)
      {
        if (countCurrentRepeat > cryticalCount) break;
        if (mySqlDB.IsNewAdEbay(item))
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
      AddLog("Prepared insert to db");

      var imgParser = new EbayLoadImage(new WebCl(), Properties.Default.PathToImg, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword);

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
          AddLog(sb.ToString());
        }

        AddLog("preparing ad to insert to db");
        try
        {
          mySqlDB.InsertFctEbayGrabber(parsedItems, sectionItem.CategoryName);
        }
        catch { }
        bool isAuction = true;
        if (parsedItems != null && parsedItems.Item != null && parsedItems.Item.Count() > 0)
        {
          imgParser.LoadImages(parsedItems.Item[0].PictureURL);
          isAuction = parsedItems.Item[0].TimeLeft != null;
        }
        // if (!isAuction)
        mySqlDB.ExecuteProcEBay(mySqlDB.ResourceListIDEbay());
        AddLog("ad inserted" + Environment.NewLine);

        if (sleepSec == -1) sleepSec = Properties.Default.SleepSec;
        AddLog("sleep on. " + sleepSec + " sec");
        Thread.Sleep(sleepSec * 1000);
        AddLog("sleep off" + Environment.NewLine + Environment.NewLine);
      }

      AddLog("Inserted to db");
      AddLog("Inserted: " + newIds.Count().ToString());
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
      SetZeroCounters();
      var links = mySqlDB.LoadSectionLinkEbay();
      Task.Factory.StartNew(() =>
      {
        buttonParsingEbay.Enabled = false;

        if (true)
        {
          AddLog("start update auctions");
          var auctionlinks = mySqlDB.LoadAuctionLink();
          foreach (long item in auctionlinks)
          {            
            var parsedItems = SearchApi.ParseItems(new long[] { item });
            AddLog("update auction: " + item.ToString() + "\t" + parsedItems.Ack);
            if (parsedItems.Ack == "Success")
            {
              mySqlDB.UpdateAuction(parsedItems);
            }
          }
          AddLog("finish update auctions" + Environment.NewLine);
        }

        foreach (var item in links)
        {
          AddLog("start next sections");
          LoadSectionEbay(item);
          //    Thread.Sleep(25 * 1000);
        }
        buttonParsingEbay.Enabled = true;
      });
    }

    private void buttonParsingAvitoEbay_Click(object sender, EventArgs e)
    {
      SetZeroCounters();
      var links = mySqlDB.LoadSectionsLinkEx();
      Task.Factory.StartNew(() =>
      {
        buttonParsingAvitoEbay.Enabled = false;

        if (true)
        {
          AddLog("start update auctions");
          var auctionlinks = mySqlDB.LoadAuctionLink();
          foreach (long item in auctionlinks)
          {
            var parsedItems = SearchApi.ParseItems(new long[] { item });
            AddLog("update auction: " + item.ToString() + "\t" + parsedItems.Ack);
            if (parsedItems.Ack == "Success")
            {
              mySqlDB.UpdateAuction(parsedItems);
            }
          }
          AddLog("finish update auctions" + Environment.NewLine);
        }
        foreach (var item in links)
        {
          AddLog("start next sections");
          AddLog(item.site.ToString());
          LoadSectionEbayAvito(item);
          //    Thread.Sleep(25 * 1000);
        }
        ProxyCollectionSingl.Instance.Dispose();
        buttonParsingAvitoEbay.Enabled = true;
      });
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
      frmSettings frm = new frmSettings();
      frm.ShowDialog();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      cbCategories.SelectedIndex = -1;
    }

    private void AddLog(string msg)
    {
      rtbLog.AppendText(DateTime.Now.ToShortTimeString() + " | " + msg + Environment.NewLine);
    }
    private void AddLogStatistic(string category, int countPrepared, int countInserted)
    {
      rtbLogStatistics.AppendText(category + " | count prepared: " + countPrepared.ToString() + " count inserted: " + countInserted.ToString() + Environment.NewLine);
    }
  }
}
