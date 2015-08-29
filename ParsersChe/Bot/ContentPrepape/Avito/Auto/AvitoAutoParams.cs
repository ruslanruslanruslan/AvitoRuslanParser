using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  abstract public class AvitoAutoParams : WebClientBot, IPrepareContent
  {
    protected PartsPage partsPage;
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var result = GetParams();
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(partsPage, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(partsPage, null);
    }

    protected abstract string GetParams();
  }
}
