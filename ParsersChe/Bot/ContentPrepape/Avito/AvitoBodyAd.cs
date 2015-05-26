using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito
{
  public class AvitoBodyAd : WebClientBot, IPrepareContent
  {
    public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      string result = GetBodyText();
      if (result != null)
      {
        return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(PartsPage.Body, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(PartsPage.Body, null);
      }
    }

    private string GetBodyText()
    {
      StringBuilder sb = new StringBuilder();
      var resNode = Doc.DocumentNode.SelectNodes("//div[@id='desc_text']/p");
      if (resNode != null)
      {
        foreach (var item in resNode)
        {
          sb.Append(item.InnerText.Trim());
          sb.Append(Environment.NewLine);
        }

      }
      return sb.ToString();
    }

    public EnumsPartPage.PartsPage PatsPage { get; set; }
  }
}
