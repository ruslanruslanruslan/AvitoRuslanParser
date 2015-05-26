using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AvitoRuslanParser;

namespace AvitoRuslanParser.EbayParser
{
  class EbayLoadImage
  {
    //public Func<string> GetidImage = (() => MySqlDB2.ResourceID());
    //public Func<string> GetidImageList = (() => MySqlDB2.ResourceListID());

    private IHttpWeb web;
    private string PathToFolder;
    private MySqlDB mySqlDB;
    private string ftpUsername;
    private string ftpPassword;
    private ImageLoader imageLoader;

    public EbayLoadImage(IHttpWeb httpweb, string pathFolder, MySqlDB _mySqlDB, string _ftpUsername, string _ftpPassword)
    {
      PathToFolder = pathFolder;
      web = httpweb;
      mySqlDB = _mySqlDB;
      ftpUsername = _ftpUsername;
      ftpPassword = _ftpPassword;
      imageLoader = new ImageLoader(PathToFolder, ftpUsername, ftpPassword);
    }

    public EbayImageParsedCountHelper LoadImages(IEnumerable<string> LinksImages)
    {
      EbayImageParsedCountHelper helper = new EbayImageParsedCountHelper();
      if (LinksImages != null)
      {
        helper.CountParsed = LinksImages.Count();
        foreach (var item in LinksImages)
        {
          string guid = mySqlDB.ResourceID();
          string guid2 = mySqlDB.ResourceListIDEbay();
          try
          {
            if (imageLoader.LoadImage(item, web, guid, guid2))
            {
              mySqlDB.InsertItemResource(guid, frmMain.URLLink);
              mySqlDB.InsertassGrabberEbayResourceList(guid2, guid);
              helper.CountDownloaded++;
              helper.ErrorList.Add("LoadImage success: " + item, false);
            }
          }
          catch (Exception ex)
          {
            helper.ErrorList.Add("LoadImage error: " + item + ": " + ex.Message, true);
          }
        }
      }
      return helper;
    }
  }

  class EbayImageParsedCountHelper
  {
    private int countParsed = 0;
    public int CountParsed
    {
      get { return countParsed; }
      set { countParsed = value; }
    }

    private int countDownloaded = 0;
    public int CountDownloaded
    {
      get { return countDownloaded; }
      set { countDownloaded = value; }
    }

    private Dictionary<string, bool> errorList = new Dictionary<string,bool>();
    public Dictionary<string, bool> ErrorList
    {
      get { return errorList; }
      set { errorList = value; }
    }
  }

}
