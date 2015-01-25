using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito
{
  public class AvitoSite : WebClientBot, IPrepareContent
  {
    private static readonly string siteName = "www.avito.ru";
    public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(PartsPage.Site, new List<string> { siteName });
    }
  }
}
