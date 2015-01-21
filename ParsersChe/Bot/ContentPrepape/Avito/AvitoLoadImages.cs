using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito
{
  public class AvitoLoadImages : WebClientBot, IPrepareContent
  {
    public string PathToFolder { get; private set; }
    private IList<string> linksImages;

    public IList<string> LinksImages
    {
      get { return linksImages; }
      protected set { linksImages = value; }
    }
    private static readonly string http = "http:";
    private IList<string> resultDown;

    protected IList<string> ResultDown
    {
      get { return resultDown; }
      set { resultDown = value; }
    }
    public AvitoLoadImages(IHttpWeb httpweb, string pathFolder)
    {
      this.WebCl = httpweb;
      PathToFolder = pathFolder.TrimEnd('/', '\\');
      if ((pathFolder.ToLower()).StartsWith("ftp://") == false)
      {
        bool isEsist = Directory.Exists(pathFolder);
        {
          if (!isEsist)
            throw new FileNotFoundException("Path no exists");
        }
      }
    }
    public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Url = url;
      Doc = doc;
      // CreateFolderId();
      LoadUrlsImage();
      LoadImages();
      var result = resultDown;
      PartsPage part = PartsPage.Image;
      return new KeyValuePair<PartsPage, IEnumerable<string>>(part, result);
    }

    private void LoadUrlsImage()
    {
      LoadSingleImage();
      //if (linksImages == null)
        LoadMoreImages();
    }
    private void CreateFolderId()
    {
      string id = InfoPage.GetDatafromText(Url, "\\d+$");
      if (!string.IsNullOrEmpty(id))
      {
        string path = PathToFolder + "\\" + id;
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        PathToFolder = path + "\\";
      }
    }

    protected virtual void LoadImages()
    {
      if (linksImages != null)
      {
        linksImages = linksImages.Distinct().ToList();
        foreach (var item in linksImages)
        {
          bool result;
          HttpWebRequest req = WebCl.GetHttpWebReq(item);
          HttpWebResponse resp = WebCl.GetHttpWebResp(req);
          if (resp != null)
          {
            if (resultDown == null) { resultDown = new List<string>(); }
            string path = PathToFolder + "\\" + "Base_" + Guid.NewGuid().ToString() + ".jpg";

            result = WebCl.DownloadImage(resp, path);
            if (result)
            {
              resultDown.Add(path);
            }
          }
        }
      }
    }
    #region private helpful methods
    private void LoadSingleImage()
    {
      var coll = Doc.DocumentNode.SelectSingleNode("//td[@class='big-picture only-one']/img");
      if (coll == null) coll = Doc.DocumentNode.SelectSingleNode("//td[@class='big-picture more-than-one']/img");
      if (coll != null)
      {
        string imglink = coll.GetAttributeValue("src", "none");
        if (!string.IsNullOrEmpty(imglink) && !imglink.Equals("none"))
        {
          if (linksImages == null) { linksImages = new List<string>(); }
          linksImages.Add(http + imglink);
        }
      }
    }

    private void LoadMoreImages()
    {
      var colls = Doc.DocumentNode.SelectNodes("//div[@class='items']/div/a");
      if (colls != null)
      {
        foreach (var item in colls)
        {
          string imglink = item.GetAttributeValue("href", "nonde");
          if (!string.IsNullOrEmpty(imglink) && !imglink.Equals("none"))
          {
            if (linksImages == null) { linksImages = new List<string>(); }
            linksImages.Add(http + imglink);
          }
        }
      }
    }
    #endregion
  }
}
