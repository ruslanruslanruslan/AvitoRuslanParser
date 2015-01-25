using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Animals
{
  public class AvitoAnimalsBreed : WebClientBot, IPrepareContent
  {
    public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var result = AvitoHelpFulMethod.GetFirstParam(Doc);
      if (result != null)
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.AnimalBreed, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.AnimalBreed, null);
      }
    }
  }
}
