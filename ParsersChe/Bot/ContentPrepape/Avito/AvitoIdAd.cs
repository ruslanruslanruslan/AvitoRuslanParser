using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoIdAd : WebClientBot, IPrepareContent
  {
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      var res = InfoPage.GetDatafromText(url, "\\d+$");
      var part = PartsPage.Id;
      if (!string.IsNullOrEmpty(res))
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, new List<string> { res });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
    }
  }
}
