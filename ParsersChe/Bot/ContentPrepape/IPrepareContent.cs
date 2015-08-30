using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepare
{
  public interface IPrepareContent
  {
    KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlDocument doc);
  }
}
