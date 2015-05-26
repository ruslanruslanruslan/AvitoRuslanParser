using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Dress
{
  public class AvitoDressSize : WebClientBot, IPrepareContent
  {
    public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      string result = AvitoHelpFulMethod.GetParamFromNodes(doc, "Размер");
      if (result != null)
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Size, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Size, null);
      }
    }
  }
}
