using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito
{
  public class AvitoTitle : WebClientBot, IPrepareContent
  {
    public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      string title = GetTitle();
      if (title != null)
      {
        return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(
             PartsPage.Title,
             new List<string> { title });
      }
      else
      {
        return new KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>>(
           PartsPage.Title,
          null);
      }
    }

    private string GetTitle()
    {
      string title = null;
      var titleNode = Doc.DocumentNode.SelectSingleNode("//h1[@class='item_title item_title-large']");
      if (titleNode != null)
      {
        title = titleNode.InnerText.Trim();
      }
      else
      {
        titleNode = Doc.DocumentNode.SelectSingleNode("//h1[@class='item_title item_title-medium']");
        if (titleNode != null)
        {
          title = titleNode.InnerText.Trim();
        }
        else
        {
          titleNode = Doc.DocumentNode.SelectSingleNode("//h1[@class='item_title item_title-small']");
          if (titleNode != null)
          {
            title = titleNode.InnerText.Trim();
          }
          else
          {
            titleNode = Doc.DocumentNode.SelectSingleNode("//h1[@itemprop='name']");
            if (titleNode != null)
            {
              title = titleNode.InnerText.Trim();
            }
          }
        }
      }
      return title;
    }


  }
}
