using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape
{
  public interface IPrepareContent
  {
    KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlDocument doc);
  }
}
