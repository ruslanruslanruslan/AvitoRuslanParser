using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoIdAd : WebClientBot, IPrepareContent
  {

    public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      string res = InfoPage.GetDatafromText(url, "\\d+$");
      var part = PartsPage.Id;
      if (!string.IsNullOrEmpty(res))
      {
        return new KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>>(part, new List<string> { res });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
      }

    }
  }
}
