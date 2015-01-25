using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape
{
  public class AvitoLoadLinksFromSection : WebClientBot, IPrepareContent
  {
    private IHttpWeb httpWeb;

    public IHttpWeb HttpWeb
    {
      get { return httpWeb; }
      set { httpWeb = value; }
    }
    private int numberPage = 1;
    public Action IncLink { get; set; }
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
    public AvitoLoadLinksFromSection(IHttpWeb httpWeb)
    {
      this.httpWeb = httpWeb;
    }
    protected static readonly string avitoHost = "http://www.avito.ru";
    public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Content = content;
      Url = url;
      Doc = doc;


      LoadLinkWithAllPage();
      var result = links;
      PartsPage part = PartsPage.LinkOnAd;
      return new KeyValuePair<PartsPage, IEnumerable<string>>(part, result);
    }

    public virtual void LoadLinkWithAllPage()
    {
      LoadLinkFromPage();
      while (isNextPage)
      {
        Console.WriteLine("Task " + Task.CurrentId.ToString() + " page: " + numberPage.ToString());
        PrepareUrl();
        string url = Url;
        HttpWebRequest req = httpWeb.GetHttpWebReq(url);
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
          {
            isNextPage = false;
          }
        }
        else
        {
          isNextPage = false;
        }
      }
    }

    public virtual void LoadLinkFromPage()
    {
      var units = Doc.DocumentNode.SelectNodes("//a[@class='second-link']");
      foreach (var item in units)
      {
        string resultRef;
        string href = item.GetAttributeValue("href", "");
        if (!string.IsNullOrEmpty(href))
        {
          if (links == null) { links = new List<string>(); }
          resultRef = avitoHost + href;
          links.Add(resultRef);
          if (IncLink != null)
          {
            IncLink();
          }
        }

      }
    }

    protected void PrepareUrl()
    {
      numberPage++;
      bool isPageParam = Regex.IsMatch(Url, "p=\\d+");

      if (isPageParam)
      {
        Url = Regex.Replace(Url, "p=\\d+", "p=" + numberPage);
      }
      else
      {
        string seperate = "&";
        bool isParam = Regex.IsMatch(Url, "\\?");
        if (!isParam) { seperate = "?"; }
        Url += seperate + "p=" + numberPage;
      }

    }
  }
}
