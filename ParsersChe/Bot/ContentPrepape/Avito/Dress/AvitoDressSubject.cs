using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Avito.Dress
{
  public class AvitoDressSubject : WebClientBot, IPrepareContent
  {
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      var result = AvitoHelpFulMethod.GetParamFromNodes(doc, "Предмет одежды");
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.DressSubject, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.DressSubject, null);
    }
  }
}
