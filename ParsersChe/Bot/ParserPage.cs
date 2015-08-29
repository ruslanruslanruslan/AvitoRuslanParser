using ParsersChe.WebClientParser;
using System.Collections.Generic;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using HtmlAgilityPack;
using System;

namespace ParsersChe.Bot.ActionOverPage
{
  public abstract class ParserPage : WebClientBot, IActionPage, ILoadContent
  {
    private IHttpWeb httpWeb;
    public Dictionary<PartsPage, IEnumerable<string>> ResultsParsing { get; set; }
    private IEnumerable<IPrepareContent> contentPreparers;
    private string pageUrl;

    public IEnumerable<IPrepareContent> ContentPreparersAction
    {
      get { return contentPreparers; }
      set { contentPreparers = value; }
    }
    protected IHttpWeb HttpWeb
    {
      get { return httpWeb; }
      set { httpWeb = value; }
    }
    protected string PageUrl
    {
      get { return pageUrl; }
      set { pageUrl = value; }
    }

    public ParserPage(IEnumerable<IPrepareContent> contentPreparers, IHttpWeb httWeb)
    {
      this.contentPreparers = contentPreparers;
      httpWeb = httWeb;
    }
    public ParserPage(IEnumerable<IPrepareContent> contentPreparers, IHttpWeb httWeb, string url)
      : this(contentPreparers, httWeb)
    {
      PageUrl = url;
    }

    public virtual void RunActions()
    {
      if (Content != null)
      {
        ResultsParsing = new Dictionary<PartsPage, IEnumerable<string>>();
        var doc = new HtmlDocument();
        doc.LoadHtml(Content);
        // Check if page is blocked
        //string result = null;
        var res = doc.DocumentNode.SelectNodes("//title"); // c-1
        if (res != null && res.Count >= 1 && res[0].InnerText.Contains("Объявление отклонено администрацией сайта"))
          throw new Exception(res[0].InnerText);

        foreach (var item in contentPreparers)
        {
          var result = item.RunActions(Content, Url, doc);
          if (result.Key != PartsPage.Empty)
            ResultsParsing.Add(result.Key, result.Value);
        }
      }
    }

    public abstract void LoadPage(string url);
    public abstract void LoadPage();

  }
}
