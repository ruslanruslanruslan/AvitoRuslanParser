using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ActionOverPage
{
  public interface IActionPage
  {
    void RunActions();
    Dictionary<PartsPage, IEnumerable<string>> ResultsParsing { get; set; }
  }
}
