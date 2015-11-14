﻿using ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito;
using ParsersChe.HelpFull;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Linq;


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
    public AvitoLoadImageDeferentSize(IHttpWeb httpweb, string pathFolder, MySqlDB _mySqlDB, string _ftpUsername, string _ftpPassword, ImageParsedCountHelper imageParsed)
      : base(httpweb, pathFolder, imageParsed)
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
        if (imageParsedCountHelper != null)
          imageParsedCountHelper.CountParsed = linksImages.Count;
        foreach (var item in LinksImages)
        {
          try
          {
            var guid = GetidImage();
            var dirName = string.Empty;
            if (guid.Length > 3)
              dirName = guid.Substring(0, 3);
            else
              dirName = guid;

            if (imageLoader.LoadImage(item, WebCl, guid, imageParsedCountHelper.ResourceId, dirName))
            {
              mySqlDB.InsertItemResource(guid, frmMain.URLLink, dirName);
              imageParsedCountHelper.Resources.Add(guid);
              imageParsedCountHelper.CountDownloaded++;
              imageParsedCountHelper.ErrorList.Add("LoadImage success: " + item, false);
            }
          }
          catch (Exception ex)
          {
            imageParsedCountHelper.ErrorList.Add("LoadImage error: " + item + ": " + ex.Message, true);
          }
        }
      }
    }
  }

}
