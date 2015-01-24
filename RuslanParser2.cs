using ParsersChe.Bot.ActionOverPage;
using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.Bot.ContentPrepape.Avito;
using ParsersChe.WebClientParser;
using ParsersChe.WebClientParser.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvitoRuslanParser.AvitoParser;

namespace AvitoRuslanParser
{
  class RuslanParser2
  {
    private string pathImages2 = "images";
    private MySqlDB mySqlDB;
    private string ftpUsername;
    private string ftpPassword;
    public string PathImages2
    {
      get { return pathImages2; }
      set { pathImages2 = value; }
    }

    public RuslanParser2(string user, string pass, string pathToProxy, MySqlDB _mySqlDB, string _ftpUsername, string _ftpPassword)
    {
      mySqlDB = _mySqlDB;
      ftpUsername = _ftpUsername;
      ftpPassword = _ftpPassword;
    }

    public Dictionary<PartsPage, IEnumerable<string>> Run(string link)
    {
      Dictionary<PartsPage, IEnumerable<string>> result = null;
      IHttpWeb webCl = new WebCl();
      string url = link;
      try
      {
        ParserPage parser = new SimpleParserPage
              (url, new List<IPrepareContent> {
                    new AvitoLoadImageDeferentSize(webCl,pathImages2, mySqlDB, ftpUsername, ftpPassword)
                    { 
                    }
                           }, webCl
              );
        parser.RunActions();
        //MySqlDB.InsertItemResource(MySqlDB.ResourceID(), link);
        // ProxyCollectionSingl.Instance.Dispose();
        result = parser.ResultsParsing;
      }
      catch (Exception)
      {
        //  System.Windows.Forms.MessageBox.Show("Test");
      }
      return result;
    }
  }
}
