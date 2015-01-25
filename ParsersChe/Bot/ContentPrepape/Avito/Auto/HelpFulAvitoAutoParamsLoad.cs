using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class HelpFulAvitoAutoParamsLoad
  {
    public static string GetParamFromNodes(HtmlDocument Doc, string marker)
    {
      string textRes = null;
      var ress = Doc.DocumentNode.SelectNodes("//dd[@class='item-params c-1']/div/a[@title]");
      if (ress != null)
      {
        var res = ress
               .Where(x =>
               {
                 bool isMaker = false;
                 string title = x.GetAttributeValue("title", "none");
                 if (!string.IsNullOrEmpty(title) && !title.Equals("none"))
                 {
                   isMaker = Regex.IsMatch(title, marker);
                 }
                 return isMaker;
               });
        if (res != null && res.Count<HtmlNode>() > 0)
        {
          var resNode = res.First<HtmlNode>();
          textRes = resNode.InnerText.Trim(); ;
        }
      }
      return textRes;
    }

    private static bool IsMarkerMatch(HtmlDocument Doc, string marker)
    {
      bool result = false;
      var ress = Doc.DocumentNode.SelectNodes("//dd[@class='item-params c-1']/div");
      if (ress != null)
      {
        var res = ress.Where(x =>
        {
          string content = x.InnerText;
          bool resultL = Regex.IsMatch(content, marker);
          return resultL;
        });
        if (res != null && res.Count<HtmlNode>() > 0)
        {
          result = true;
        }
      }
      return result;
    }

    public static string GetParamFromParentNodes(HtmlDocument Doc, string[,] markers)
    {
      string result = null;
      for (int i = 0; i < markers.Length / 2; i++)
      {
        bool isMacth = IsMarkerMatch(Doc, markers[i, 0]);
        if (isMacth)
        {
          result = markers[i, 1];
          break;
        }
      }
      return result;
    }

  }

}
