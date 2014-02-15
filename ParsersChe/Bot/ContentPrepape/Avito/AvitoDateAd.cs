using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
    public class AvitoDateAd : WebClientBot, IPrepareContent
    {
        private DateTime date;
        private Dictionary<string, int> month = new Dictionary<string, int>();

        public string LoadDateText()
        {
            var dateNode = Doc.DocumentNode.SelectSingleNode("//div[@class='item_subtitle']");
            if (dateNode == null) 
            {
                dateNode = Doc.DocumentNode.SelectSingleNode("//div[@class='item_subtitle item_subtitle-empty']");
            }
            if (dateNode == null) 
            {
                dateNode = Doc.DocumentNode.SelectSingleNode("//div[@class='item_subtitle item-without-images item_subtitle-empty']");
            }
            LoadMonth();
            string textAll = dateNode.FirstChild.InnerText.Trim();
            string textDate = Regex.Match(textAll, "Размещено (.+?) в ").Groups[1].Value;
            DateTime date = GetDateFromAllText(textDate);
            return date.ToLongDateString();

        }

        public DateTime GetDateFromAllText(string textDate)
        {
            DateTime date = new DateTime();
            switch (textDate)
            {
                case "вчера": date = DateTime.Today.AddDays(-1); break;
                case "сегодня": date = DateTime.Today; break;
                default: date = PrepareDateFromText(textDate); break;
            }
            return date;
        }
        private DateTime PrepareDateFromText(string dateText)
        {
            string numDay = InfoPage.GetDatafromText(dateText, "\\d+");
            string monthName = InfoPage.GetDatafromText(dateText, "[а-я]+");
            int day = Convert.ToInt32(numDay);
            int month = this.month[monthName];
            return new DateTime(DateTime.Today.Year, month, day);

        }
        private void LoadMonth()
        {
            month.Add("янв", 1);
            month.Add("фев", 2);
            month.Add("мар", 3);
            month.Add("апр", 4);
            month.Add("май", 5);
            month.Add("июн", 6);
            month.Add("июл", 7);
            month.Add("авг", 8);
            month.Add("сен", 9);
            month.Add("окт", 10);
            month.Add("нояб", 11);
            month.Add("дек", 12);
        }

        public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
        {
            Doc = doc;
            Url = url;
            PartsPage part = PartsPage.Date;
            string result = LoadDateText();
            if (result != null)
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(part, new List<string> { result });
            }
            else
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
            }
        }


    }
}
