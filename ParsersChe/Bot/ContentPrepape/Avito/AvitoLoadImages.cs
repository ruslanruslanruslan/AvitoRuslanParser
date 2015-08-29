using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito
{
  public class AvitoLoadImages : WebClientBot, IPrepareContent
  {
    public string PathToFolder { get; private set; }
    protected IList<string> linksImages;

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

    protected ImageParsedCountHelper imageParsedCountHelper = null;
    public ImageParsedCountHelper ImageParsedCount
    {
      get { return imageParsedCountHelper; }
      set { imageParsedCountHelper = value; }
    }

    public AvitoLoadImages(IHttpWeb httpweb, string pathFolder, ImageParsedCountHelper imageCount = null)
    {
      imageParsedCountHelper = imageCount;
      WebCl = httpweb;
      PathToFolder = pathFolder.TrimEnd('/', '\\');
      if ((pathFolder.ToLower()).StartsWith("ftp://") == false)
      {
        var isEsist = Directory.Exists(pathFolder);
        {
          if (!isEsist)
            throw new FileNotFoundException("LoadImage error: Path no exists '" + pathFolder + "'");
        }
      }
    }
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Url = url;
      Doc = doc;
      LoadUrlsImage();
      LoadImages();
      return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Image, resultDown);
    }

    private void LoadUrlsImage()
    {
      LoadSingleImage();
      LoadMoreImages();
    }
    private void CreateFolderId()
    {
      var id = InfoPage.GetDatafromText(Url, "\\d+$");
      if (!string.IsNullOrEmpty(id))
      {
        var path = PathToFolder + "\\" + id;
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
        if (imageParsedCountHelper != null)
          imageParsedCountHelper.CountParsed = linksImages.Count;
        foreach (var item in linksImages)
        {
          try
          {
            bool result;
            var req = WebCl.GetHttpWebReq(item);
            var resp = WebCl.GetHttpWebResp(req);
            if (resp != null)
            {
              if (resultDown == null)
                resultDown = new List<string>();
              var path = PathToFolder + "\\" + "Base_" + Guid.NewGuid().ToString() + ".jpg";

              result = WebCl.DownloadImage(resp, path);
              if (result)
              {
                resultDown.Add(path);
                imageParsedCountHelper.CountDownloaded++;
                imageParsedCountHelper.ErrorList.Add("LoadImage success: " + item, false);
              }
            }
          }
          catch (Exception ex)
          {
            imageParsedCountHelper.ErrorList.Add("LoadImage error: " + item + ": " + ex.Message, true);
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
        var imglink = coll.GetAttributeValue("src", "none");
        if (!string.IsNullOrEmpty(imglink) && !imglink.Equals("none"))
        {
          if (linksImages == null)
            linksImages = new List<string>();
          linksImages.Add(http + imglink);
        }
      }
    }

    private void LoadMoreImages()
    {
      var colls = Doc.DocumentNode.SelectNodes("//div[@class='items']/div/a");
      if (colls == null) colls = Doc.DocumentNode.SelectNodes("//meta[@property='og:image']");
      if (colls != null)
      {
        foreach (var item in colls)
        {
          var imglink = item.GetAttributeValue("content", "nonde");
          if (!string.IsNullOrEmpty(imglink) && !imglink.Equals("none"))
          {
            if (linksImages == null) { linksImages = new List<string>(); }
            if (imglink.StartsWith(http))
              linksImages.Add(imglink);
            else
              linksImages.Add(http + imglink);
          }
        }
      }
    }
    #endregion
  }

  public class ImageParsedCountHelper
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
