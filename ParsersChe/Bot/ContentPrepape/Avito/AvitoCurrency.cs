using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito
{
  public class AvitoCurrency : WebClientBot, IPrepareContent
  {
    public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var result = GetCurrency();
      if (result != null)
      {
        return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(PartsPage.Currency, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Currency, null);
      }
    }
    private string GetCurrency()
    {
      string cur = null;
      var ress = Doc.DocumentNode.SelectSingleNode("//span[@class='p_i_price']/strong");
      if (ress != null)
      {
        var res = ress.ChildNodes["#text"];
        if (res != null)
        {
          cur = res.InnerText.Trim();
        }
      }
      return cur;
    }

  }
}
