using HtmlAgilityPack;
using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Irr
{
  class IrrPhone : IPrepareContent
  {
    #region Fields
    private string content;
    private string url;
    private HtmlDocument doc;
    private IHttpWeb httpWeb;
    #endregion
    #region Constructors
    public IrrPhone(IHttpWeb httpWeb)
    {
      this.httpWeb = httpWeb;
    }
    public IrrPhone()
    {
      httpWeb = new WebCl();
    }
    #endregion
    public IHttpWeb HttpWeb
    {
      get { return httpWeb; }
    }

    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlDocument doc)
    {
      this.content = content;
      this.url = url;
      this.doc = doc;
      return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Phone, null);
    }
  }
}
