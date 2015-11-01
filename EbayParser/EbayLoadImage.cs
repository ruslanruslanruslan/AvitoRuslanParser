using ParsersChe.HelpFull;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public ImageParsedCountHelper LoadImages(IEnumerable<string> LinksImages)
    {
      var helper = new ImageParsedCountHelper();
      if (LinksImages != null)
      {
        helper.CountParsed = LinksImages.Count();
        foreach (var item in LinksImages)
        {
          var guid = mySqlDB.ResourceID();          
          try
          {
            var dirName = string.Empty;
            if (guid.Length > 3)
              dirName = guid.Substring(0, 3);
            else
              dirName = guid;
            if (imageLoader.LoadImage(item, web, guid, helper.ResourceId, dirName))
            {
              mySqlDB.InsertItemResource(guid, frmMain.URLLink, dirName);
              helper.Resources.Add(guid);
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
}
