using ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito;
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
using MySql.Data.MySqlClient;
using AvitoRuslanParser;


namespace AvitoRuslanParser.AvitoParser
{

  public class AvitoLoadImageDeferentSize : AvitoLoadImages
  {
    private MySqlDB mySqlDB;
    private string ftpUsername;
    private string ftpPassword;
    private ImageLoader imageLoader;
    public string GetidImage()
    {
      return mySqlDB.ResourceID();
    }
    public string GetidImageList()
    {
      return mySqlDB.ResourceListIDAvito();
    }
    public AvitoLoadImageDeferentSize(IHttpWeb httpweb, string pathFolder, MySqlDB _mySqlDB, string _ftpUsername, string _ftpPassword)
      : base(httpweb, pathFolder)
    {
      mySqlDB = _mySqlDB;
      ftpUsername = _ftpUsername;
      ftpPassword = _ftpPassword;
      imageLoader = new ImageLoader(PathToFolder, ftpUsername, ftpPassword);
    }

    protected override void LoadImages()
    {
      if (LinksImages != null)
      {
        LinksImages = LinksImages.Distinct().ToList();
        foreach (var item in LinksImages)
        {
          string guid = GetidImage();
          string guid2 = GetidImageList();
          if (imageLoader.LoadImage(item, WebCl, guid, guid2))
          {
            mySqlDB.InsertItemResource(guid, frmMain.URLLink);
            mySqlDB.InsertassGrabberAvitoResourceList(guid2, guid);
          }
        }
      }
    }
  }
}
