using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage
{
  public class SimpleParserPage : ParserPage
  {
    public SimpleParserPage(IEnumerable<IPrepareContent> contentPreparers, IHttpWeb webClient)
      : base(contentPreparers, webClient)
    {

    }
    public SimpleParserPage(string url, IEnumerable<IPrepareContent> contentPreparers, IHttpWeb webClient)
      : base(contentPreparers, webClient, url)
    {
    }

    public override void LoadPage(string url)
    {
      this.Url = url;
      HttpWebRequest req = HttpWeb.GetHttpWebReq(url);
      req.AllowAutoRedirect = true;
      HttpWebResponse res = HttpWeb.GetHttpWebResp(req);
      if (res != null)
      {
        Content = HttpWeb.GetContent(res, Encoding.UTF8);
      }
    }


  }
}
