using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito
{
  public class AvitoSeller : WebClientBot, IPrepareContent
  {
    public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var result = GetSeller();
      if (result != null)
      {
        return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(PartsPage.Seller, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Seller, null);
      }
    }
    public string GetSeller()
    {
      string seller = null;
      var res = Doc.DocumentNode.SelectSingleNode("//div[@id='seller']//strong");
      if (res != null)
      {
        seller = res.InnerText.Trim();
      }
      return seller;
    }

  }
}
