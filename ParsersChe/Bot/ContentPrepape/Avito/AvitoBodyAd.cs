using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito
{
  public class AvitoBodyAd : WebClientBot, IPrepareContent
  {
    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      var result = GetBodyText();
      if (result != null)
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Body, new List<string> { result });
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(PartsPage.Body, null);
    }

    private string GetBodyText()
    {
      var sb = new StringBuilder();
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

    public PartsPage PatsPage { get; set; }
  }
}
