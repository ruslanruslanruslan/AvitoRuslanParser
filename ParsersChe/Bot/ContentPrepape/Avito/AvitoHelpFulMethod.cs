using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
    class AvitoHelpFulMethod
    {
        public static string GetFirstParam(HtmlDocument Doc)
        {
            string result = null;
            var res = Doc.DocumentNode.SelectNodes("//dd[@class='item-params c-1']/a/strong");
            if (res != null && res.Count > 0)
            {
                result = res[0].InnerText.Trim();
            }
            return result;
        }

        public static string GetParamFromNodes(HtmlDocument Doc, string marker)
        {
            string textRes = null;
            var ress = Doc.DocumentNode.SelectNodes("//dd[@class='item-params c-1']/a[@title]");
            if (ress != null)
            {
                var res = ress
                       .Where(x =>
                       {
                           bool isMaker = false;
                           string title = x.GetAttributeValue("title", "none");
                           if (!string.IsNullOrEmpty(title) && !title.Equals("none"))
                           {
                               isMaker = Regex.IsMatch(title, marker);
                           }
                           return isMaker;
                       });
                if (res != null && res.Count<HtmlNode>() > 0)
                {
                    var resNode = res.First<HtmlNode>();
                    textRes = resNode.InnerText.Trim(); ;
                }
            }
            return textRes;
        }
    }
}
