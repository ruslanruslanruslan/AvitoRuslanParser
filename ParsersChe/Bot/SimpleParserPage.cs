using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.WebClientParser;
using System.Collections.Generic;
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
      var req = HttpWeb.GetHttpWebReq(url);
      req.AllowAutoRedirect = true;
      var res = HttpWeb.GetHttpWebResp(req);
      if (res != null)
        Content = HttpWeb.GetContent(res, Encoding.UTF8);
    }

    public override void LoadPage()
    {
      LoadPage(PageUrl);
    }


  }
}
