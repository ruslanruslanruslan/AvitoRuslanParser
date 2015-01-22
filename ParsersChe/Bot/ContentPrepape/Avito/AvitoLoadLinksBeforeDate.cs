using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoLoadLinksBeforeDate : AvitoLoadLinksFromSection
  {
    private DateTime date;
    private int wrongDateCount;
    private int maxWrongDate;
    private Dictionary<string, int> month = new Dictionary<string, int>();
    public AvitoLoadLinksBeforeDate(IHttpWeb httpWeb, DateTime date, int maxWrongDate)

      : base(httpWeb)
    {
      this.maxWrongDate = maxWrongDate;
      this.date = date;
      LoadMonth();
    }
    public override void LoadLinkFromPage()
    {
      var mainUnits = Doc.DocumentNode.SelectNodes("//div[@class='t_i']/div");
      foreach (var item in mainUnits)
      {
        if (wrongDateCount > maxWrongDate)
        {
          IsNextPage = false;
          break;
        }
        var dateUnit = item.SelectSingleNode("div[@class='t_i_date']");
        if (dateUnit != null)
        {
          string textDate = dateUnit.FirstChild.InnerText.Trim();
          DateTime dateAd = GetDateFromAllText(textDate);
          if (date <= dateAd)
          {
            var linkNode = item.SelectSingleNode("div/h3/a[@class='second-link']");
            string resultRef;
            string href = linkNode.GetAttributeValue("href", "");
            if (!string.IsNullOrEmpty(href))
            {
              if (Links == null) { Links = new List<string>(); }
              resultRef = avitoHost + href;
              Links.Add(resultRef);
            }
            wrongDateCount = 0;
          }
          else
          {
            wrongDateCount++;
          }

        }
      }
    }

    public DateTime GetDateFromAllText(string textDate)
    {
      DateTime date = new DateTime();
      switch (textDate)
      {
        case "Вчера": date = DateTime.Today.AddDays(-1); break;
        case "Сегодня": date = DateTime.Today; break;
        default: date = PrepareDateFromText(textDate); break;
      }
      return date;
    }
    protected DateTime PrepareDateFromText(string dateText)
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


  }
}
