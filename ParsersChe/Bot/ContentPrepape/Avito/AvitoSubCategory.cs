using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
    public class AvitoSubCategory:WebClientBot,IPrepareContent
    {
        public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
        {
            Doc = doc;
            PartsPage parts = PartsPage.SubCategory;
            var result = GetSubCategory();
            if (result != null)
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(parts, new List<string> { result });
            }
            else
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(parts, null);
            }
        }

        public string GetSubCategory() 
        {
            string category = null;
            var resColl = Doc.DocumentNode.SelectNodes("//div[@class='breadcrumbs-links']/a");
            if (resColl != null && resColl.Count > 2)
            {
                category = resColl[2].InnerHtml.Trim();
            }
            return category;
        }
    }
}
