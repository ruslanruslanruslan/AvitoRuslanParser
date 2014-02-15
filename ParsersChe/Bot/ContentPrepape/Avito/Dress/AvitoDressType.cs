using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Dress
{
  public  class AvitoDressType : WebClientBot, IPrepareContent
    {
        public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
        {
            string result = AvitoHelpFulMethod.GetParamFromNodes(doc, "Вид одежды");
            if (result != null)
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.DressType, new List<string> { result });
            }
            else
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.DressType, null);
            }
        }
    }
}
