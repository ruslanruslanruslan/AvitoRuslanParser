using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito
{
    public class AvitoCost : WebClientBot, IPrepareContent
    {
        public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
        {
            Doc = doc;
            var result = GetCost();
            if (result != null)
            {
                return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(PartsPage.Cost, new List<string> { result });

            }
            else
            {
                return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(PartsPage.Cost, null);

            }
        }
        private string GetCost()
        {
            string cost = null;
            var res = Doc.DocumentNode.SelectSingleNode("//span[@class='p_i_price t-item-price']/span");
            if (res != null)
            {
                cost = res.InnerText.Replace("&nbsp;", "");
                cost = cost.Replace(" ", "").Replace("руб.","");
                cost = InfoPage.GetDatafromText(cost, "\\d+");
            }
            return cost;
        }
    }
}
