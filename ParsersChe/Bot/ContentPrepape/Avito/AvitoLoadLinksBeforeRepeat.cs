using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoLoadLinksBeforeRepeat : AvitoLoadLinksFromSection
  {
    private int limitLinks;
    private Func<int, bool> IsNewAd;
    public Action<string> AddData { get; set; }
    private int currentCountRepeat;
    public AvitoLoadLinksBeforeRepeat(IHttpWeb httpWeb, int limitLink, Func<int, bool> IsNewAd)
      : base(httpWeb)
    {
      this.limitLinks = limitLink;
      this.IsNewAd = IsNewAd;
    }
    public override void LoadLinkFromPage()
    {

      var units = Doc.DocumentNode.SelectNodes("//a[@class='photo-wrapper']");

      foreach (var item in units)
      {


        string resultRef;
        string href = item.GetAttributeValue("href", "");
        if (!string.IsNullOrEmpty(href))
        {
          if (Links == null) { Links = new List<string>(); }

          resultRef = avitoHost + href;

          int idAd = GetIdAd(resultRef);
          bool isNew = IsNewAd(idAd);
          if (!isNew) { currentCountRepeat++; }
          if (currentCountRepeat >= limitLinks)
          {
            IsNextPage = false;
            break;
          }
          if (isNew)
          {
            Links.Add(resultRef);
            if (AddData != null)
            {
              AddData(resultRef);
            }
          }
          if (IncLink != null)
          {
            IncLink();
          }
        }

      }
    }

    private int GetIdAd(string url)
    {
      string res = InfoPage.GetDatafromText(url, "\\d+$");
      int resInt = Convert.ToInt32(res);
      return resInt;
    }
  }
}
