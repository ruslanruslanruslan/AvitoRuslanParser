using ParsersChe.Bot;
using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParsersChe.Bot.ActionOverPage;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  abstract public class AvitoAutoParams : WebClientBot, IPrepareContent
  {
    protected PartsPage partsPage;
    public KeyValuePair<ParsersChe.Bot.ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      string result = GetParams();
      if (result != null)
      {
        return new KeyValuePair<ParsersChe.Bot.ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>>(partsPage, new List<string> { result });

      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(partsPage, null);
      }
    }

    protected abstract string GetParams();
  }
}
