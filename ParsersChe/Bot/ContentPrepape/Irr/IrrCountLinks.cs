using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Irr
{
  class IrrCountLinks : WebClientBot, IPrepareContent
  {
    public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      PartsPage part = PartsPage.CountLink;
      string result = GetData();
      if (result != null)
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
      }
    }

    public string GetData()
    {
      string result = null;
      var res = Doc.DocumentNode.SelectSingleNode("//span[@id='finded_ads']");
      if (res != null)
      {
        result = res.InnerText.Trim().Replace(",", "").Replace(" ", "").Replace("&nbsp;", "").Replace("(", "").Replace(")", "");

      }
      return result;
    }

  }
}
