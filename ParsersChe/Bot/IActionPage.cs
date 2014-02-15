using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage
{
    public interface IActionPage
    {
        void RunActions();
        Dictionary<PartsPage, IEnumerable<string>> ResultsParsing { get; set; }

    }
}
