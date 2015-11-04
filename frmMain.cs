using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AvitoRuslanParser.EbayParser;
using AvitoRuslanParser.Helpfuls;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using Extensions;
using ParsersChe.HelpFull;

namespace AvitoRuslanParser
{
  public partial class frmMain : Form
  {
    private int countParsed = 0;
    private int countInserted = 0;
    public static string URLLink;
    private MySqlDB mySqlDB;
    private object thislock = new object();

    private ParsingState AvitoState = new ParsingState();
    private ParsingState EbayState = new ParsingState();

    public frmMain()
    {
      InitializeComponent();
      CheckForIllegalCrossThreadCalls = true;
      mySqlDB = new MySqlDB(Properties.Default.MySqlServerUsername, Properties.Default.MySqlServerPassword,
        Properties.Default.MySqlServerAddress, Properties.Default.MySqlServerPort, Properties.Default.MySqlServerDatabase);
    }

    public void IncParsed()
    {
      countParsed++;
      labelParsed.SetPropertyThreadSafe(() => labelParsed.Text, countParsed.ToString());
    }
    public void incInserted()
    {
      countInserted++;
      labelInserted.SetPropertyThreadSafe(() => labelInserted.Text, countInserted.ToString());
    }

    public void SetZeroCounters()
    {
      countInserted = 0;
      countParsed = 0;
      labelParsed.SetPropertyThreadSafe(() => labelParsed.Text, "0");
      labelInserted.SetPropertyThreadSafe(() => labelInserted.Text, "0");
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      LoadField();
      try
      {
        foreach (var str in mySqlDB.GetCategories())
          cbCategories.Items.Add(str);
      }
      catch (Exception)
      {
        var frm = new frmSettings(true);
        var result = frm.ShowDialog();
        if (result == DialogResult.Cancel)
          Application.Exit();
        if (result == DialogResult.Abort)
        {
          LinkAdtextBox.Enabled = false;
          btnEnter.Enabled = false;
          cbCategories.Enabled = false;
          btnReset.Enabled = false;
          btnParsingAvitoStart.Enabled = false;
          btnParsingEbayStart.Enabled = false;
          btnParsingAvitoEbayStart.Enabled = false;
          btnParsingAvitoPause.Enabled = false;
          btnParsingEbayPause.Enabled = false;
          btnParsingAvitoEbayPause.Enabled = false;
          btnParsingAvitoStop.Enabled = false;
          btnParsingEbayStop.Enabled = false;
          btnParsingAvitoEbayStop.Enabled = false;
        }
      }
    }

    private void DisableControls(Control ctrls)
    {
      foreach (Control ctrl in ctrls.Controls)
        DisableControls(ctrl);
      ctrls.Enabled = false;
    }

    private void frmMain_Closing(object sender, FormClosingEventArgs e)
    {
      if (!AvitoState.Stopped || !EbayState.Stopped)
      {
        if (MessageBox.Show("Parsing is still running. Are you sure you want to cancel it?", "Close application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
          Text += " [Closing...]";
          DisableControls(this);
          if (!AvitoState.Stopped)
            AvitoState.SetStopping();
          if (!EbayState.Stopped)
            EbayState.SetStopping();
          AvitoState.SetNeedAppClose();
          EbayState.SetNeedAppClose();
        }
        e.Cancel = true;
        return;
      }
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
        var id = Convert.ToInt64(Regex.Match(URLLink, "\\d{7,}").Value);
        var imgParser = new EbayLoadImage(new WebCl(), Properties.Default.PathToImg, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword);
        var parsedItems = SearchApi.ParseItems(new long[] { id });


        mySqlDB.InsertFctEbayGrabber(parsedItems, cbCategories.Text);
        var isAuction = true;

        ImageParsedCountHelper imageCount = null;

        if (parsedItems != null && parsedItems.Item != null && parsedItems.Item.Count() > 0)
        {
          imageCount = imgParser.LoadImages(parsedItems.Item[0].PictureURL);
          AddLog("Parser: " + imageCount.CountParsed + " images parsed", LogMessageColor.Information());
          AddLog("Parser: " + imageCount.CountDownloaded + " images downloaded", LogMessageColor.Information());
          foreach (var error in imageCount.ErrorList)
            AddLog(error.Key, error.Value == true ? LogMessageColor.Error() : LogMessageColor.Success());
          isAuction = (parsedItems.Item[0].TimeLeft != null && parsedItems.Item[0].ListingType != "FixedPriceItem");
        }

        var idResourceList = mySqlDB.ResourceListIDEbay();
        imageCount.ResourceId = idResourceList;
        if (imageCount != null)
        {
          foreach (var im in imageCount.Resources)
            mySqlDB.InsertassGrabberEbayResourceList(idResourceList, im);
          mySqlDB.UpdateFctEbayGrabberPhotoCount(idResourceList, imageCount.Resources.Count);
        }

        if (!isAuction)
          mySqlDB.ExecuteProcEBay(mySqlDB.ResourceListIDEbay());
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }

    }
    private void OneLinkAvito()
    {
      try
      {
        label6.SetPropertyThreadSafe(() => label6.Text, "Start");
        //Ссылка на обьявление
        mySqlDB.PrepareAvitoEnvironment();
        //Создаем класс и вводим параметры 
        var Parser = new RuslanParser(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB);
        Parser.PathImages = Properties.Default.PathToImg;
        //вот тут мы вызывем запрос на Id к базе
        //Parser.LoadGuid = (() => MySqlDB.SeletGuid());
        //Parser.LoadGuid = (() => "1532");
        //тут мы присваем результат переменной result
        Dictionary<PartsPage, IEnumerable<string>> result = null;
        try
        {
          result = Parser.Run(URLLink);
        }
        catch (Exception ex)
        {
          AddLog("Parser: " + ex.Message, LogMessageColor.Error());
        }
        // тут мы передаем в метод вставки данные
        //result, index ID, ссылку на обьявления
        //id я не вставлял так как непонятно было и неподходило под структуру бд
        if (result != null)
        {
          var idResourceList = mySqlDB.ResourceListIDAvito();
          mySqlDB.InsertFctAvitoGrabber(result, idResourceList, URLLink, cbCategories.Text);
          var imageCount = new ImageParsedCountHelper();
          imageCount.ResourceId = idResourceList;
          var Parser2 = new RuslanParser2(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword, imageCount);
          Parser2.PathImages2 = Properties.Default.PathToImg;
          var result2 = Parser2.Run(URLLink);
          AddLog("Parser: " + imageCount.CountParsed + " images parsed", LogMessageColor.Information());
          AddLog("Parser: " + imageCount.CountDownloaded + " images downloaded", LogMessageColor.Information());
          foreach (var error in imageCount.ErrorList)
          {
            AddLog(error.Key, error.Value == true ? LogMessageColor.Error() : LogMessageColor.Success());
          }

          foreach (var im in imageCount.Resources)
            mySqlDB.InsertassGrabberAvitoResourceList(idResourceList, im);

          mySqlDB.UpdateFctAvitoGrabberPhotoCount(idResourceList, imageCount.Resources.Count);

          AddLog("Parser: Loading images end", LogMessageColor.Information());
          AddLog("Parser: Begin publishing ad", LogMessageColor.Information());
          mySqlDB.ExecuteProcAvito(idResourceList);
          AddLog("Parser: End publishing ad", LogMessageColor.Information());
        }
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }
      label6.SetPropertyThreadSafe(() => label6.Text, "Finish");
    }
    private void btnEnter_Click(object sender, EventArgs e)
    {//Проверку на пустые знаяения полей
      if (!string.IsNullOrEmpty(LinkAdtextBox.Text) /*&& !string.IsNullOrEmpty(Properties.Default.User)
          && !string.IsNullOrEmpty(Properties.Default.Password) && !string.IsNullOrEmpty(Properties.Default.PathToProxy)*/
          && !string.IsNullOrEmpty(Properties.Default.PathToImg))
      {
        URLLink = LinkAdtextBox.Text;
        var uri = new Uri(URLLink);
        if (uri.Host == "www.avito.ru")
          OneLinkAvito();
        else if (uri.Host == "www.ebay.com")
          OneLinkEbay();
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
          label6.SetPropertyThreadSafe(() => label6.Text, "Start");

          while (AvitoState.Pausing || AvitoState.Paused)
          {
            if (AvitoState.Pausing)
            {
              AddLog(string.Empty, LogMessageColor.Information());
              AddLog("Paused", LogMessageColor.Information());
              AddLog(string.Empty, LogMessageColor.Information());
            }
            AvitoState.SetPaused();
            if (EbayState.Pausing || EbayState.Paused)
            {
              btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
              btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
              btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);
            }
            else
            {
              btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, true);
              btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
              btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, true);
            }
            Thread.Sleep(1000);
          }

          if (AvitoState.Stopping || AvitoState.Stopped)
          {
            label6.SetPropertyThreadSafe(() => label6.Text, "Finish");
            return;
          }

          //Ссылка на обьявление
          //  URLLink = LinkAdtextBox.Text;
          //Создаем класс и вводим параметры 
          var Parser = new RuslanParser(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB);
          var imageCount = new ImageParsedCountHelper();
          var Parser2 = new RuslanParser2(Properties.Default.User, Properties.Default.Password, Properties.Default.PathToProxy, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword, imageCount);

          Parser.PathImages = Properties.Default.PathToImg;

          var linksAds = Parser.LoadLinks(linkSection[0]);
          if (linksAds == null)
          {
            AddLog("Parser: Couldn't retrieve ad links from given section " + linkSection[1], LogMessageColor.Error());
            return;
          }
          AddLog("Parser: " + linkSection[1], LogMessageColor.Information());
          AddLog("Parser: Count new ad: " + linksAds.Count().ToString(), LogMessageColor.Information());
          var i = 0;
          var countPre = 0;
          var countIns = 0;

          while (AvitoState.Pausing || AvitoState.Paused)
          {
            if (AvitoState.Pausing)
            {
              AddLog(string.Empty, LogMessageColor.Information());
              AddLog("Paused", LogMessageColor.Information());
              AddLog(string.Empty, LogMessageColor.Information());
            }
            AvitoState.SetPaused();
            if (EbayState.Pausing || EbayState.Paused)
            {
              btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
              btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
              btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);
            }
            else
            {
              btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, true);
              btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
              btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, true);
            }
            Thread.Sleep(1000);
          }

          if (AvitoState.Stopping || AvitoState.Stopped)
          {
            label6.SetPropertyThreadSafe(() => label6.Text, "Finish");
            return;
          }

          foreach (var item in linksAds)
          {
            while (AvitoState.Pausing || AvitoState.Paused)
            {
              if (AvitoState.Pausing)
              {
                AddLog(string.Empty, LogMessageColor.Information());
                AddLog("Paused", LogMessageColor.Information());
                AddLog(string.Empty, LogMessageColor.Information());
              }
              AvitoState.SetPaused();
              if (EbayState.Pausing || EbayState.Paused)
              {
                btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
                btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
                btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);
              }
              else
              {
                btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, true);
                btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
                btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, true);
              }
              Thread.Sleep(1000);
            }

            if (AvitoState.Stopping || AvitoState.Stopped)
              break;

            try
            {
              imageCount.ErrorList.Clear();
              imageCount.Resources.Clear();
              imageCount.CountParsed = 0;
              imageCount.CountDownloaded = 0;
              imageCount.ResourceId = string.Empty;
              AddLog("Parser: start parse link: " + item, LogMessageColor.Information());
              i++;
              if (i == 25)
                i = -1;
              URLLink = item;
              mySqlDB.PrepareAvitoEnvironment();
              Dictionary<PartsPage, IEnumerable<string>> result = null;
              try
              {
                result = Parser.Run(item);
              }
              catch (Exception ex)
              {
                AddLog("Parser: " + ex.Message, LogMessageColor.Error());
                continue;
              }
              var sb = new StringBuilder();
              sb.AppendLine();
              foreach (var element in result)
              {
                if (element.Key != PartsPage.Body)
                {
                  sb.Append(string.Format("{0,-15} - ", element.Key));
                  if (element.Value != null)
                    foreach (var t in element.Value)
                    {
                      if (element.Value.Count() > 1)
                        sb.Append(t + " |");
                      else
                        sb.Append(t);
                    }
                  sb.Append(Environment.NewLine);
                }
              }
              AddLog("Parser: " + sb.ToString().TrimEnd(Environment.NewLine.ToCharArray()), LogMessageColor.Information());
              IncParsed();
              countPre++;

              AddLog("Parser: sleep after parse on. " + Properties.Default.SleepAfterParseSec + " sec", LogMessageColor.Information());
              Thread.Sleep(Properties.Default.SleepAfterParseSec * 1000);
              AddLog("Parser: sleep after parse off", LogMessageColor.Information());

              AddLog("Parser: preparing ad to insert to db", LogMessageColor.Information());
              var idResourceList = mySqlDB.ResourceListIDAvito();
              mySqlDB.InsertFctAvitoGrabber(result, idResourceList, item, linkSection[1]);
              AddLog("Parser: ad inserted", LogMessageColor.Information());
              incInserted();
              imageCount.ResourceId = idResourceList;
              if (Properties.Default.PublishParsedData)
              {
                AddLog("Parser: Begin loading images", LogMessageColor.Information());
                Parser2.PathImages2 = Properties.Default.PathToImg;

                var result2 = Parser2.Run(item);
                AddLog("Parser: " + imageCount.CountParsed + " images parsed", LogMessageColor.Information());
                AddLog("Parser: " + imageCount.CountDownloaded + " images downloaded", LogMessageColor.Information());

                foreach (var error in imageCount.ErrorList)
                  AddLog(error.Key, error.Value == true ? LogMessageColor.Error() : LogMessageColor.Success());

                foreach (var im in imageCount.Resources)
                  mySqlDB.InsertassGrabberAvitoResourceList(idResourceList, im);

                mySqlDB.UpdateFctAvitoGrabberPhotoCount(idResourceList, imageCount.Resources.Count);

                AddLog("Parser: Loading images end", LogMessageColor.Information());
                AddLog("Parser: Begin publishing ad", LogMessageColor.Information());
                mySqlDB.ExecuteProcAvito(idResourceList);
                AddLog("Parser: End publishing ad", LogMessageColor.Information());

                AddLog("Parser: sleep after publication on. " + Properties.Default.SleepSecAfterPublicationSec + " sec", LogMessageColor.Information());
                Thread.Sleep(Properties.Default.SleepSecAfterPublicationSec * 1000);
                AddLog("Parser: sleep after publication off", LogMessageColor.Information());
              }
              else
              {
                AddLog("Parser: Saving data to SMSSpamer", LogMessageColor.Information());
                mySqlDB.InsertSMSSpamerData(idResourceList);
                AddLog("Parser: Data saved successfully", LogMessageColor.Information());
              }
              countIns++;
            }
            catch (Exception ex)
            {
              AddLog("Parser: " + ex.Message, LogMessageColor.Error());
            }
            AddLog(string.Empty, LogMessageColor.Information());
          }
          AddLogStatistic(linkSection[1], mySqlDB.CountAd, countIns);
        }
        catch (Exception ex)
        {
          AddLog("Parser: " + ex.Message, LogMessageColor.Error());
        }
        label6.SetPropertyThreadSafe(() => label6.Text, "Finish");
      }
    }
    private void SaveField()
    {
      Properties.Default.LinkOnAd = LinkAdtextBox.Text;
      Properties.Default.Save();
    }
    private void LoadField()
    {
      LinkAdtextBox.Text = Properties.Default.LinkOnAd;
    }

    private void btnParsingAvito_Click(object sender, EventArgs e)
    {
      if (AvitoState.Pausing || AvitoState.Paused)
      {
        AvitoState.SetRunning();
        btnParsingAvitoStart.Enabled = false;
        btnParsingAvitoPause.Enabled = true;
        btnParsingAvitoStop.Enabled = true;
        return;
      }
      StartSMSSpamer();
      if (!CheckPublishingOn())
        return;
      try
      {
        Task.Factory.StartNew(() =>
        {
          btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, false);
          btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
          btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, false);

          btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, false);
          btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, true);
          btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, true);

          btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, false);
          btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
          btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, false);

          SetZeroCounters();

          AvitoState.SetRunning();

          var links = mySqlDB.LoadSectionsLink();
          foreach (var item in links)
          {
            while (AvitoState.Pausing || AvitoState.Paused)
            {
              if (AvitoState.Pausing)
              {
                AddLog(string.Empty, LogMessageColor.Information());
                AddLog("Paused", LogMessageColor.Information());
                AddLog(string.Empty, LogMessageColor.Information());
              }
              AvitoState.SetPaused();
              btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, true);
              btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
              btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, true);
              Thread.Sleep(1000);
            }
            if (AvitoState.Stopping || AvitoState.Stopped)
              break;
            try
            {
              var uri = new Uri(item[0]);
              if (uri.Host == "www.avito.ru")
              {
                AddLog("Parser: start next section", LogMessageColor.Information());
                LoadSection(item);
                if (AvitoState.Stopping || AvitoState.Stopped)
                  break;
              }
            }
            catch (Exception ex)
            {
              AddLog("Parser: " + ex.Message, LogMessageColor.Error());
            }
          }

          AvitoState.SetStopped();

          //ProxyCollectionSingl.Instance.Dispose();
          btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
          btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
          btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, false);

          btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, true);
          btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
          btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, false);

          btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
          btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
          btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, false);

          if (AvitoState.NeedAppClose)
            Invoke((MethodInvoker)delegate
            {
              Close();
            });
        });
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }
    }

    private void LoadSectionEbay(SectionItem sectionItem)
    {
      while (EbayState.Pausing || EbayState.Paused)
      {
        if (EbayState.Pausing)
        {
          AddLog(string.Empty, LogMessageColor.Information());
          AddLog("Paused", LogMessageColor.Information());
          AddLog(string.Empty, LogMessageColor.Information());
        }
        EbayState.SetPaused();
        if (AvitoState.Pausing || AvitoState.Paused)
        {
          btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
          btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
          btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);
        }
        else
        {
          btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
          btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
          btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, true);
        }
        Thread.Sleep(1000);
      }

      if (EbayState.Stopping || EbayState.Stopped)
        return;

      var cryticalCount = 8;
      var searchApi = new SearchApi();
      searchApi.PerPage = 100;
      searchApi.Section = sectionItem.Link;

      AddLog("Parser: " + sectionItem.CategoryName, LogMessageColor.Information());

      try
      {
        var ids = searchApi.SearchLinks();
        AddLog("Parser: Count new ad: " + ids.Count().ToString(), LogMessageColor.Information());
        IList<long> newIds = new List<long>();
        var countCurrentRepeat = 0;

        foreach (var item in ids)
        {
          if (countCurrentRepeat > cryticalCount)
            break;
          if (mySqlDB.IsNewAdEbay(item))
            newIds.Add(item);
          else
            countCurrentRepeat++;
        }

        var partsIdsCollection = Helpful.Partition(newIds, 1);
        AddLog("Parser: Prepared insert to db", LogMessageColor.Information());

        var imgParser = new EbayLoadImage(new WebCl(), Properties.Default.PathToImg, mySqlDB, Properties.Default.FtpUsername, Properties.Default.FtpPassword);

        foreach (var item in partsIdsCollection)
        {
          while (EbayState.Pausing || EbayState.Paused)
          {
            if (EbayState.Pausing)
            {
              AddLog(string.Empty, LogMessageColor.Information());
              AddLog("Paused", LogMessageColor.Information());
              AddLog(string.Empty, LogMessageColor.Information());
            }
            EbayState.SetPaused();
            if (AvitoState.Pausing || AvitoState.Paused)
            {
              btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
              btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
              btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);
            }
            else
            {
              btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
              btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
              btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, true);
            }
            Thread.Sleep(1000);
          }

          if (EbayState.Stopping || EbayState.Stopped)
            break;

          AddLog("Parser: Begin parsing ad", LogMessageColor.Information());
          var parsedItems = SearchApi.ParseItems(item);
          foreach (var unit in parsedItems.Item)
          {
            AddLog(string.Format("{0,-15} - {1}", "url: ", unit.ViewItemURLForNaturalSearch), LogMessageColor.Information());
            AddLog(string.Format("{0,-15} - {1}", "title: ", unit.Title), LogMessageColor.Information());
            AddLog(string.Format("{0,-15} - {1}", "cost: ", unit.CurrentPrice.Value.ToString()), LogMessageColor.Information());
            AddLog(string.Format("{0,-15} - {1}", "country: ", unit.Country), LogMessageColor.Information());
            AddLog(string.Format("{0,-15} - {1}", "city: ", unit.Location), LogMessageColor.Information());
            AddLog(string.Format("{0,-15} - {1}", "author: ", unit.Seller.UserID), LogMessageColor.Information());
            AddLog(string.Format("{0,-15} - {1}", "ebay section: ", unit.PrimaryCategoryName), LogMessageColor.Information());
          }
          var isAuction = true;
          ImageParsedCountHelper imageCount = null;
          if (parsedItems != null && parsedItems.Item != null && parsedItems.Item.Count() > 0)
          {
            if (Properties.Default.PublishParsedData)
            {
              AddLog("Parser: Begin loading images", LogMessageColor.Information());
              try
              {
                imageCount = imgParser.LoadImages(parsedItems.Item[0].PictureURL);
                AddLog("Parser: " + imageCount.CountParsed + " images parsed", LogMessageColor.Information());
                AddLog("Parser: " + imageCount.CountDownloaded + " images downloaded", LogMessageColor.Information());
                foreach (var error in imageCount.ErrorList)
                  AddLog(error.Key, error.Value == true ? LogMessageColor.Error() : LogMessageColor.Success());
              }
              catch (Exception ex)
              {
                AddLog("Parser: " + ex.Message, LogMessageColor.Error());
              }
              AddLog("Parser: End loading images", LogMessageColor.Information());
            }
            isAuction = (parsedItems.Item[0].TimeLeft != null && parsedItems.Item[0].ListingType != "FixedPriceItem");
            if (isAuction)
              AddLog("Parser: It is auction", LogMessageColor.Information());
            else
              AddLog("Parser: It is advertisement", LogMessageColor.Information());
          }
          AddLog("Parser: End parsing ad", LogMessageColor.Information());
          IncParsed();

          AddLog("Parser: sleep after parse on. " + Properties.Default.SleepAfterParseSec + " sec", LogMessageColor.Information());
          Thread.Sleep(Properties.Default.SleepAfterParseSec * 1000);
          AddLog("Parser: sleep after parse off", LogMessageColor.Information());

          AddLog("Parser: preparing ad to insert to db", LogMessageColor.Information());
          var idResourceList = mySqlDB.ResourceListIDEbay();
          imageCount.ResourceId = idResourceList;
          try
          {
            mySqlDB.InsertFctEbayGrabber(parsedItems, sectionItem.CategoryName);
            if (imageCount != null)
            {
              foreach (var im in imageCount.Resources)
                mySqlDB.InsertassGrabberEbayResourceList(idResourceList, im);
              mySqlDB.UpdateFctEbayGrabberPhotoCount(idResourceList, imageCount.Resources.Count);
            }
            if (Properties.Default.PublishParsedData && !isAuction)
            {
              AddLog("Parser: Begin publishing ad", LogMessageColor.Information());
              mySqlDB.ExecuteProcEBay(idResourceList);
              AddLog("Parser: End publishing ad", LogMessageColor.Information());

              AddLog("Parser: sleep after publication on. " + Properties.Default.SleepSecAfterPublicationSec + " sec", LogMessageColor.Information());
              Thread.Sleep(Properties.Default.SleepSecAfterPublicationSec * 1000);
              AddLog("Parser: sleep after publication off", LogMessageColor.Information());
            }
            AddLog("Parser: ad inserted", LogMessageColor.Information());
            incInserted();
          }
          catch (Exception ex)
          {
            AddLog("Parser: " + ex.Message, LogMessageColor.Error());
          }
          AddLog(string.Empty, LogMessageColor.Information());
        }

        AddLog("Parser: End pasring section " + sectionItem.CategoryName, LogMessageColor.Information());
        AddLog("Parser: Inserted: " + newIds.Count().ToString(), LogMessageColor.Information());
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }
    }

    private void LoadSectionEbayAvito(SectionItem sectionItem)
    {
      try
      {
        if (sectionItem.site == SectionItem.Site.Avito)
          LoadSection(new string[] { sectionItem.Link, sectionItem.CategoryName });
        else
          LoadSectionEbay(sectionItem);
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }
    }

    private void EbayUpdateAuctions()
    {
      try
      {
        AddLog("Parser: start update auctions", LogMessageColor.Information());
        var auctionlinks = mySqlDB.LoadAuctionLink();
        foreach (long item in auctionlinks)
        {
          while (EbayState.Pausing || EbayState.Paused)
          {
            if (EbayState.Pausing)
            {
              AddLog(string.Empty, LogMessageColor.Information());
              AddLog("Paused", LogMessageColor.Information());
              AddLog(string.Empty, LogMessageColor.Information());
            }
            EbayState.SetPaused();
            if (AvitoState.Pausing || AvitoState.Paused)
            {
              btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
              btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
              btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);
            }
            else
            {
              btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
              btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
              btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, true);
            }
            Thread.Sleep(1000);
          }

          if (EbayState.Stopping || EbayState.Stopped)
            break;

          try
          {
            AddLog("Parser: updating auction: " + item.ToString() + "...", LogMessageColor.Information());
            var parsedItems = SearchApi.ParseItems(new long[] { item });
            if (parsedItems.Ack == "Success")
            {
              AddLog("Parser: update auction: " + item.ToString() + "\t" + parsedItems.Ack, LogMessageColor.Information());

              AddLog("Parser: sleep after parse on. " + Properties.Default.SleepAfterParseSec + " sec", LogMessageColor.Information());
              Thread.Sleep(Properties.Default.SleepAfterParseSec * 1000);
              AddLog("Parser: sleep after parse off", LogMessageColor.Information());

              string timeLeft;
              if (mySqlDB.UpdateAuction(parsedItems, out timeLeft) == 1)
              {
                AddLog("Parser: auction " + item.ToString() + " published", LogMessageColor.Information());

                AddLog("Parser: sleep after publication on. " + Properties.Default.SleepSecAfterPublicationSec + " sec", LogMessageColor.Information());
                Thread.Sleep(Properties.Default.SleepSecAfterPublicationSec * 1000);
                AddLog("Parser: sleep after publication off", LogMessageColor.Information());
              }
              else
              {
                AddLog("Parser: auction " + item.ToString() + " not published: Auction time left: " + timeLeft, LogMessageColor.Information());
              }
            }
            else
            {
              var err = string.Empty;
              if (parsedItems.Errors != null)
              {
                if (parsedItems.Errors.LongMessage.Length > 0)
                  err = parsedItems.Errors.LongMessage;
                else
                  err = parsedItems.Errors.ShortMessage;
              }
              AddLog("Parser: update auction: " + item.ToString() + "\t" + parsedItems.Ack + ":\t" + err, LogMessageColor.Error());

              AddLog("Parser: sleep after parse on. " + Properties.Default.SleepAfterParseSec + " sec", LogMessageColor.Information());
              Thread.Sleep(Properties.Default.SleepAfterParseSec * 1000);
              AddLog("Parser: sleep after parse off", LogMessageColor.Information());
            }
          }
          catch (Exception ex)
          {
            AddLog("Parser: " + ex.Message, LogMessageColor.Error());
          }
        }
        AddLog("Parser: finish update auctions" + Environment.NewLine, LogMessageColor.Information());
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }
    }

    private void buttonParsingEbay_Click(object sender, EventArgs e)
    {
      if (EbayState.Pausing || EbayState.Paused)
      {
        EbayState.SetRunning();
        btnParsingEbayStart.Enabled = false;
        btnParsingEbayPause.Enabled = true;
        btnParsingEbayStop.Enabled = true;
        return;
      }
      if (!CheckPublishingOn())
        return;
      try
      {
        Task.Factory.StartNew(() =>
        {
          btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, false);
          btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, true);
          btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, true);

          btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, false);
          btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
          btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, false);

          btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, false);
          btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
          btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, false);

          SetZeroCounters();

          EbayState.SetRunning();

          EbayUpdateAuctions();

          while (EbayState.Pausing || EbayState.Paused)
          {
            if (EbayState.Pausing)
            {
              AddLog(string.Empty, LogMessageColor.Information());
              AddLog("Paused", LogMessageColor.Information());
              AddLog(string.Empty, LogMessageColor.Information());
            }
            EbayState.SetPaused();
            btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
            btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
            btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, true);
            Thread.Sleep(1000);
          }
          if (!EbayState.Stopping && !EbayState.Stopped)
          {
            var links = mySqlDB.LoadSectionLinkEbay();

            foreach (var item in links)
            {
              while (EbayState.Pausing || EbayState.Paused)
              {
                if (EbayState.Pausing)
                {
                  AddLog(string.Empty, LogMessageColor.Information());
                  AddLog("Paused", LogMessageColor.Information());
                  AddLog(string.Empty, LogMessageColor.Information());
                }
                EbayState.SetPaused();
                btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
                btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
                btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, true);
                Thread.Sleep(1000);
              }
              if (EbayState.Stopping || EbayState.Stopped)
                break;
              try
              {
                AddLog("Parser: start next section", LogMessageColor.Information());
                LoadSectionEbay(item);
              }
              catch (Exception ex)
              {
                AddLog("Parser: " + ex.Message, LogMessageColor.Error());
              }
            }
          }
          EbayState.SetStopped();

          btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
          btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
          btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, false);

          btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, true);
          btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
          btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, false);

          btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
          btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
          btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, false);

          if (EbayState.NeedAppClose)
            Invoke((MethodInvoker)delegate
            {
              Close();
            });
        });
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }
    }

    private void buttonParsingAvitoEbay_Click(object sender, EventArgs e)
    {
      if (AvitoState.Pausing || AvitoState.Paused || EbayState.Pausing || EbayState.Paused)
      {
        AvitoState.SetRunning();
        EbayState.SetRunning();
        btnParsingAvitoEbayStart.Enabled = false;
        btnParsingAvitoEbayPause.Enabled = true;
        btnParsingAvitoEbayStop.Enabled = true;
        return;
      }
      StartSMSSpamer();
      if (!CheckPublishingOn())
        return;
      try
      {
        Task.Factory.StartNew(() =>
        {
          btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, false);
          btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
          btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, false);

          btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, false);
          btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
          btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, false);

          btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, false);
          btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, true);
          btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);

          SetZeroCounters();

          AvitoState.SetRunning();
          EbayState.SetRunning();

          EbayUpdateAuctions();

          while (AvitoState.Pausing || AvitoState.Paused || EbayState.Pausing || EbayState.Paused)
          {
            if (AvitoState.Pausing || EbayState.Pausing)
            {
              AddLog(string.Empty, LogMessageColor.Information());
              AddLog("Paused", LogMessageColor.Information());
              AddLog(string.Empty, LogMessageColor.Information());
            }
            AvitoState.SetPaused();
            EbayState.SetPaused();
            btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
            btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
            btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, true);
            Thread.Sleep(1000);
          }
          if (!AvitoState.Stopping && !AvitoState.Stopped && !EbayState.Stopping && !EbayState.Stopped)
          {
            var links = mySqlDB.LoadSectionsLinkEx();

            foreach (var item in links)
            {
              try
              {
                AddLog("Parser: start next section", LogMessageColor.Information());
                AddLog("Parser: " + item.site.ToString(), LogMessageColor.Information());
                LoadSectionEbayAvito(item);
              }
              catch (Exception ex)
              {
                AddLog("Parser: " + ex.Message, LogMessageColor.Error());
              }
            }
          }
          //ProxyCollectionSingl.Instance.Dispose();

          AvitoState.SetStopped();
          EbayState.SetStopped();

          btnParsingEbayStart.SetPropertyThreadSafe(() => btnParsingEbayStart.Enabled, true);
          btnParsingEbayPause.SetPropertyThreadSafe(() => btnParsingEbayPause.Enabled, false);
          btnParsingEbayStop.SetPropertyThreadSafe(() => btnParsingEbayStop.Enabled, false);

          btnParsingAvitoStart.SetPropertyThreadSafe(() => btnParsingAvitoStart.Enabled, true);
          btnParsingAvitoPause.SetPropertyThreadSafe(() => btnParsingAvitoPause.Enabled, false);
          btnParsingAvitoStop.SetPropertyThreadSafe(() => btnParsingAvitoStop.Enabled, false);

          btnParsingAvitoEbayStart.SetPropertyThreadSafe(() => btnParsingAvitoEbayStart.Enabled, true);
          btnParsingAvitoEbayPause.SetPropertyThreadSafe(() => btnParsingAvitoEbayPause.Enabled, false);
          btnParsingAvitoEbayStop.SetPropertyThreadSafe(() => btnParsingAvitoEbayStop.Enabled, false);

          if (AvitoState.NeedAppClose || EbayState.NeedAppClose)
            Invoke((MethodInvoker)delegate
            {
              Close();
            });
        });
      }
      catch (Exception ex)
      {
        AddLog("Parser: " + ex.Message, LogMessageColor.Error());
      }
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
      var frm = new frmSettings();
      frm.ShowDialog();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      cbCategories.SelectedIndex = -1;
    }

    private void AddLog(string msg, Color msgColor)
    {
      if (rtbLog.InvokeRequired)
      {
        rtbLog.Invoke(new MethodInvoker(() => AddLog(msg, msgColor)));
      }
      else
      {
        try
        {
          lock (thislock)
          {
            var start = rtbLog.Text.Length - 1;
            if (start < 0)
              start = 0;
            if (msg == string.Empty)
              rtbLog.AppendText(Environment.NewLine);
            else
              rtbLog.AppendText(DateTime.Now.ToLongTimeString() + " | " + msg + Environment.NewLine);
            rtbLog.Select(start, rtbLog.Text.Length - start + 1);
            rtbLog.SelectionColor = msgColor;
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error adding log", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }
    private void AddLogStatistic(string category, int countPrepared, int countInserted)
    {
      if (rtbLogStatistics.InvokeRequired)
      {
        rtbLogStatistics.Invoke(new MethodInvoker(() => AddLogStatistic(category, countPrepared, countInserted)));
      }
      else
      {
        try
        {
          lock (thislock)
          {
            rtbLogStatistics.AppendText(DateTime.Now.ToLongTimeString() + " | " + category + " | count prepared: " + countPrepared.ToString() + " count inserted: " + countInserted.ToString() + Environment.NewLine);
            rtbLogStatistics.SelectionStart = rtbLogStatistics.Text.Length;
            rtbLogStatistics.ScrollToCaret();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error adding log statistics", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void rtbLog_LinkClicked(object sender, LinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(e.LinkText);
    }

    private void StartSMSSpamer()
    {
      if (Properties.Default.RunSMSSpamer && Properties.Default.SMSSpamerPath.Length > 0)
      {
        Task.Factory.StartNew(() =>
        {
          string arguments = "--send-sms-from-db -server " + Properties.Default.MySqlServerAddress + " -database " +
            Properties.Default.MySqlServerDatabase + " -login " + Properties.Default.MySqlServerUsername + " -password " +
            Properties.Default.MySqlServerPassword + " -port " + Properties.Default.MySqlServerPort;
          System.Diagnostics.Process.Start(Properties.Default.SMSSpamerPath, arguments);
        }
        );
      }
    }

    private bool CheckPublishingOn()
    {
      if (!Properties.Default.PublishParsedData)
        return MessageBox.Show("Pusblishing is off. Are you sure you want to proceed?", "Publishing is off", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
      else
        return true;
    }

    private void btnParsingAvitoPause_Click(object sender, EventArgs e)
    {
      AvitoState.SetPausing();
      btnParsingAvitoStart.Enabled = false;
      btnParsingAvitoPause.Enabled = false;
      btnParsingAvitoStop.Enabled = false;
    }

    private void btnParsingAvitoStop_Click(object sender, EventArgs e)
    {
      AvitoState.SetStopping();
      btnParsingAvitoStart.Enabled = false;
      btnParsingAvitoPause.Enabled = false;
      btnParsingAvitoStop.Enabled = false;
    }

    private void btnParsingEbayPause_Click(object sender, EventArgs e)
    {
      EbayState.SetPausing();
      btnParsingEbayStart.Enabled = false;
      btnParsingEbayPause.Enabled = false;
      btnParsingEbayStop.Enabled = false;
    }

    private void btnParsingEbayStop_Click(object sender, EventArgs e)
    {
      EbayState.SetStopping();
      btnParsingEbayStart.Enabled = false;
      btnParsingEbayPause.Enabled = false;
      btnParsingEbayStop.Enabled = false;
    }

    private void btnParsingAvitoEbayPause_Click(object sender, EventArgs e)
    {
      AvitoState.SetPausing();
      EbayState.SetPausing();
      btnParsingAvitoEbayStart.Enabled = false;
      btnParsingAvitoEbayPause.Enabled = false;
      btnParsingAvitoEbayStop.Enabled = false;
    }

    private void btnParsingAvitoEbayStop_Click(object sender, EventArgs e)
    {
      AvitoState.SetStopping();
      EbayState.SetStopping();
      btnParsingAvitoEbayStart.Enabled = false;
      btnParsingAvitoEbayPause.Enabled = false;
      btnParsingAvitoEbayStop.Enabled = false;
    }
  }
}
