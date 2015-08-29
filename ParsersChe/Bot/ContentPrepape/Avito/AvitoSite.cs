using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito
{
  public class AvitoSite : WebClientBot, IPrepareContent
  {
    private static readonly string siteName = "www.avito.ru";
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Site, new List<string> { siteName });
    }
  }
}
