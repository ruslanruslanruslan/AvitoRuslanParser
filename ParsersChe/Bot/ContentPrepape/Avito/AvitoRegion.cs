using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoRegion : WebClientBot, IPrepareContent
  {
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var part = PartsPage.Region;
      var result = GetLocal();
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(part, null);
    }

    private string GetLocal()
    {
      string result = null;
      var res = Doc.DocumentNode.SelectNodes("//a[@class='c-1']");
      if (res != null && res.Count > 1)
        result = res[0].InnerText.Trim();
      return result;
    }
  }
}
