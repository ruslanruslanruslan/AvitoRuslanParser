using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsersChe.Bot.ContentPrepape.Irr
{
  public class IrrLoadLinks : WebClientBot, IPrepareContent
  {
    private IHttpWeb httpWeb;
    private string er = "?er=2";

    public IHttpWeb HttpWeb
    {
      get { return httpWeb; }
      set { httpWeb = value; }
    }
    private int numberPage = 1;
    public Action IncLink { get; set; }
    public Action<string> AddData { get; set; }
    public Func<int, bool> IsNewAd { get; set; }
    public int NumberPage
    {
      get { return numberPage; }
      set { numberPage = value; }
    }
    private IList<string> links;

    public IList<string> Links
    {
      get { return links; }
      set { links = value; }
    }
    private bool isNextPage = true;

    public bool IsNextPage
    {
      get { return isNextPage; }
      set { isNextPage = value; }
    }

    public IrrLoadLinks(IHttpWeb httpWeb)
    {
      this.httpWeb = httpWeb;
    }
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Content = content;
      Url = url;
      Doc = doc;
      LoadLinkWithAllPage();
      return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.LinkOnAd, links);
    }

    public virtual void LoadLinkWithAllPage()
    {
      LoadLinkFromPage();
      while (isNextPage)
      {
        Console.WriteLine("Task " + Task.CurrentId.ToString() + " page: " + numberPage.ToString());
        PrepareUrl();
        string url = Url;
        var req = httpWeb.GetHttpWebReq(url);
        req.AllowAutoRedirect = false;

        var resp = httpWeb.GetHttpWebResp(req);
        if (resp != null)
        {
          Content = httpWeb.GetContent(resp, Encoding.UTF8);
          if (!string.IsNullOrEmpty(Content))
          {
            Doc.LoadHtml(Content);
            LoadLinkFromPage();
          }
          else
            isNextPage = false;
        }
        else
          isNextPage = false;
      }
    }

    public virtual void LoadLinkFromPage()
    {
      var units = Doc.DocumentNode.SelectNodes("//a[@class='add_title']");
      foreach (var item in units)
      {
        string resultRef;
        var href = item.GetAttributeValue("href", "");
        if (!string.IsNullOrEmpty(href))
        {
          if (links == null) { links = new List<string>(); }
          resultRef = href;
          var codeAd = GetIdAd(resultRef);
          if (IsNewAd(codeAd))
          {
            links.Add(resultRef);
            AddData(resultRef);
          }
          if (IncLink != null)
            IncLink();
        }
      }
    }

    private int GetIdAd(string url)
    {
      url = url.Replace(".html", "");
      var res = InfoPage.GetDatafromText(url, "\\d+$");
      return Convert.ToInt32(res);
    }

    protected virtual void PrepareUrl()
    {
      numberPage++;
      var isPageParam = Regex.IsMatch(Url, "page\\d+");

      if (isPageParam)
        Url = Regex.Replace(Url, "page\\d+", "page" + numberPage);
      else
      {
        Url.Replace(er, "");
        Url += Url + "page" + numberPage + "/" + er;
      }

    }
  }
}
