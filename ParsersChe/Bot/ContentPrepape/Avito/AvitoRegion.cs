using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoRegion : WebClientBot, IPrepareContent
  {
    public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      PartsPage part = PartsPage.Region;
      string result = GetLocal();
      if (result != null)
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
      }
    }

    private string GetLocal()
    {
      string result = null;
      var res = Doc.DocumentNode.SelectNodes("//a[@class='c-1']");
      if (res != null && res.Count > 1)
      {
        result = res[0].InnerText.Trim();
      }
      return result;
    }
  }
}
