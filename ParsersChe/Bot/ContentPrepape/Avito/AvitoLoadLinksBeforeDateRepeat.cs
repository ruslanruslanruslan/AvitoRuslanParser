using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoLoadLinksBeforeDateRepeat : AvitoLoadLinksFromSection
  {
    private int limitLinks;
    private Func<int, bool> IsNewAd;
    private int currentCountRepeat;
    public Action Sleep { get; set; }
    private DateTime date;
    private int wrongDateCount;
    private int maxWrongDate;
    private Dictionary<string, int> month = new Dictionary<string, int>();

    public AvitoLoadLinksBeforeDateRepeat(IHttpWeb httpWeb, int limitLink, Func<int, bool> IsNewAd, DateTime date, int maxWrongDate)
      : base(httpWeb)
    {
      limitLinks = limitLink;
      this.IsNewAd = IsNewAd;
      this.maxWrongDate = maxWrongDate;
      this.date = date;
      LoadMonth();
    }

    public override void LoadLinkWithAllPage()
    {
      LoadLinkFromPage();
      while (IsNextPage)
      {
        if (Sleep != null) Sleep();
        Console.WriteLine("Task " + Task.CurrentId.ToString() + " page: " + NumberPage.ToString());
        PrepareUrl();
        var url = Url;
        var req = HttpWeb.GetHttpWebReq(url);
        req.AllowAutoRedirect = false;

        var resp = HttpWeb.GetHttpWebResp(req);
        if (resp != null)
        {
          Content = HttpWeb.GetContent(resp, Encoding.UTF8);
          if (!string.IsNullOrEmpty(Content))
          {
            Doc.LoadHtml(Content);
            LoadLinkFromPage();
          }
          else
            IsNextPage = false;
        }
        else
          IsNextPage = false;
      }
    }

    public override void LoadLinkFromPage()
    {
      var units = Doc.DocumentNode.SelectNodes("//div[@class='b-catalog-table']/div");
      foreach (var item in units)
      {
        if (wrongDateCount > maxWrongDate)
        {
          IsNextPage = false;
          break;
        }
        string resultRef;
        var linkNode = item.SelectSingleNode("div/h3/a[@class='second-link']");
        if (linkNode == null) continue;
        var href = linkNode.GetAttributeValue("href", "");
        var dateUnit = item.SelectSingleNode("div[@class='date']");
        if (!string.IsNullOrEmpty(href) && dateUnit != null)
        {
          if (Links == null) { Links = new List<string>(); }
          resultRef = avitoHost + href;
          var idAd = GetIdAd(resultRef);
          var isNew = IsNewAd(idAd);

          var textDate = dateUnit.FirstChild.InnerText.Trim();
          var dateAd = GetDateFromAllText(textDate);

          if (!isNew) { currentCountRepeat++; }
          if (currentCountRepeat >= limitLinks)
          {
            IsNextPage = false;
            break;
          }
          if (isNew)
          {
            if (date <= dateAd)
            {
              Links.Add(resultRef);
              wrongDateCount = 0;
            }
            else
              wrongDateCount++;
          }
        }

      }
    }

    private int GetIdAd(string url)
    {
      var res = InfoPage.GetDatafromText(url, "\\d+$");
      return Convert.ToInt32(res);
    }

    public DateTime GetDateFromAllText(string textDate)
    {
      var date = new DateTime();
      switch (textDate)
      {
        case "Вчера": date = DateTime.Today.AddDays(-1); break;
        case "Сегодня": date = DateTime.Today; break;
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
  }
}
