using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoCity : WebClientBot, IPrepareContent
  {
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var part = PartsPage.City;
      var result = GetLocal();
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
    }

    public string GetLocal()
    {
      string result = null;
      var res = Doc.DocumentNode.SelectNodes("//span[@class='pseudo-link']"); // c-1
      if (res == null)
        res = Doc.DocumentNode.SelectNodes("//span[@class='pseudo-link icon-link']"); 
      if (res != null)
      {
        if (res.Count > 1)
          result = res[1].InnerText.Trim();
        else if (res.Count == 1)
          result = res[0].InnerText.Trim();
      }
      return result;
    }
  }
}
