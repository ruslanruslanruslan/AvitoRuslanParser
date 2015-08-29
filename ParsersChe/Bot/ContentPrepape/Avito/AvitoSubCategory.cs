using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoSubCategory : WebClientBot, IPrepareContent
  {
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var parts = PartsPage.SubCategory;
      var result = GetSubCategory();
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(parts, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(parts, null);
    }

    public string GetSubCategory()
    {
      var category = string.Empty;

      var resColl = Doc.DocumentNode.SelectNodes("//div[@class='breadcrumbs-links']/a");
      if (resColl != null && resColl.Count > 2)
      {
        category = resColl[2].InnerHtml.Trim();
        return category;
      }
      resColl = Doc.DocumentNode.SelectNodes("//div[@class='b-catalog-breadcrumbs']/a");
      if (resColl != null)
      {
        foreach (var res in resColl)
        {
          if (category != string.Empty)
            category += "/";
          category += res.InnerHtml.Trim();
        }
      }
      return category;
    }
  }
}
