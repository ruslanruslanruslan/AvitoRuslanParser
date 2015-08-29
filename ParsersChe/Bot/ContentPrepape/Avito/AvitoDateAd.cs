using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoDateAd : WebClientBot, IPrepareContent
  {
    private Dictionary<string, int> month = new Dictionary<string, int>();

    public string LoadDateText()
    {
      var dateNode = Doc.DocumentNode.SelectSingleNode("//div[@class='item_subtitle']");
      if (dateNode == null)
        dateNode = Doc.DocumentNode.SelectSingleNode("//div[@class='item_subtitle item_subtitle-empty']");
      if (dateNode == null)
        dateNode = Doc.DocumentNode.SelectSingleNode("//div[@class='item_subtitle item-without-images item_subtitle-empty']");
      LoadMonth();
      var textAll = dateNode.FirstChild.InnerText.Trim();
      var textDate = Regex.Match(textAll, "Размещено (.+?) в ").Groups[1].Value;
      var date = GetDateFromAllText(textDate);
      return date.ToLongDateString();
    }

    public DateTime GetDateFromAllText(string textDate)
    {
      var date = new DateTime();
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
      var numDay = InfoPage.GetDatafromText(dateText, "\\d+");
      var monthName = InfoPage.GetDatafromText(dateText, "[а-я]+");
      var day = Convert.ToInt32(numDay);
      var month = this.month[monthName];
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

    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      Url = url;
      var part = PartsPage.Date;
      var result = LoadDateText();
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
    }
  }
}
