using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoAgency : WebClientBot, IPrepareContent
  {
    public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      string result = IsAgency().ToString();
      if (result != null)
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Agency, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Agency, null);
      }
    }

    public bool IsAgency()
    {
      bool IsAgency = false;

      var nodeTag = Doc.DocumentNode.SelectSingleNode("//div[@id='seller']/span");
      if (nodeTag != null)
      {
        string inputText = nodeTag.InnerText.Trim();
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
