using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoAgency : WebClientBot, IPrepareContent
  {
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var result = IsAgency().ToString();
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Agency, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Agency, null);
    }

    public bool IsAgency()
    {
      var IsAgency = false;

      var nodeTag = Doc.DocumentNode.SelectSingleNode("//div[@id='seller']/span");
      if (nodeTag != null)
      {
        var inputText = nodeTag.InnerText.Trim();
        switch (inputText)
        {
          case "(агентство)": IsAgency = true; break;
          case "(частное лицо)": IsAgency = false; break;
          default: IsAgency = false; break;
        }
      }

      return IsAgency;
    }
  }
}
