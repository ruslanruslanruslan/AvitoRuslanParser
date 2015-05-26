using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoLoadLinksSetCount : AvitoLoadLinksFromSection
  {
    private int limitLinks;
    public AvitoLoadLinksSetCount(IHttpWeb httpWeb, int limitLink)
      : base(httpWeb)
    {
      this.limitLinks = limitLink;
    }
    public override void LoadLinkFromPage()
    {
      var units = Doc.DocumentNode.SelectNodes("//a[@class='second-link']");
      foreach (var item in units)
      {
        string resultRef;
        string href = item.GetAttributeValue("href", "");
        if (!string.IsNullOrEmpty(href))
        {
          if (Links == null) { Links = new List<string>(); }
          resultRef = avitoHost + href;
          Links.Add(resultRef);
          if (Links.Count >= limitLinks)
          {
            IsNextPage = false;
            break;
          }
        }

      }

    }
  }
}
