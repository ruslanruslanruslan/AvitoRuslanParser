using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Dress
{
  public  class AvitoDressSubject : WebClientBot, IPrepareContent
    {
        public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
        {
            string result = AvitoHelpFulMethod.GetParamFromNodes(doc, "Предмет одежды");
            if (result != null)
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.DressSubject, new List<string> { result });
            }
            else
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.DressSubject, null);
            }
        }
    }
}
