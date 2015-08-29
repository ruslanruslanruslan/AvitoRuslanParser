using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepare
{
  public class AvitoPhones : IPrepareContent
  {
    #region Fields
    private string content;
    private string phonePageContent;
    private string url;
    private HtmlDocument doc;
    private string pKey;
    private string urlPhoneImage;
    private IHttpWeb httpWeb;
    #endregion
    #region Constructors
    public AvitoPhones(IHttpWeb httpWeb)
    {
      this.httpWeb = httpWeb;
    }
    public AvitoPhones()
    {
      httpWeb = new WebCl();
    }
    #endregion
    public IHttpWeb HttpWeb
    {
      get { return httpWeb; }
    }

    public KeyValuePair<PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlDocument doc)
    {
      this.content = content;
      this.url = url;
      this.doc = doc;

      var av = LoadPkey();
      if (av)
        LoadPhone();
      var typeResult = PartsPage.Phone;
      if (phonePageContent != null)
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(typeResult, new List<string>()
            {
                phonePageContent
            });
      }
      else
        return new KeyValuePair<PartsPage, IEnumerable<string>>(typeResult, null);
    }
    #region Private Methods
    private bool LoadPkey()
    {
      string text;
      var unit = doc.DocumentNode.SelectSingleNode("//div[@class='item']");
      if (unit == null) { unit = doc.DocumentNode.SelectSingleNode("//div[@class='item item-rabota']"); }
      if (unit == null) { unit = doc.DocumentNode.SelectSingleNode("//div[@class='item item-layout-cols']"); }
      if (unit == null)
      {
        string mobileURL;
        var id = InfoPage.GetDatafromText(url, "\\d{7,}");
        mobileURL = url.Replace("www.avito.ru", "m.avito.ru");
        mobileURL = mobileURL.Replace("_" + id, "._" + id);
        var req = HttpWeb.GetHttpWebReq(mobileURL);
        req.AllowAutoRedirect = true;
        var res = HttpWeb.GetHttpWebResp(req);
        if (res != null)
        {
          var Content = HttpWeb.GetContent(res, Encoding.UTF8);
          var docMobile = new HtmlDocument();
          docMobile.LoadHtml(Content);
          unit = docMobile.DocumentNode.SelectSingleNode("//a[@class='person-action button button-solid button-blue button-large action-show-number action-link link ']");
        }
      }
      if (unit != null)
      {
        text = unit.InnerText;
        var item_phone = InfoPage.GetDatafromText(text, "item_phone = *?\'(.+?)\'", 1);
        if (item_phone == null || item_phone == string.Empty)
        {
          foreach (var attr in unit.Attributes)
          {
            if (attr.Name == "href")
            {
              urlPhoneImage = attr.Value;
              return true;
            }
          }
        }
        if (item_phone == null || item_phone == string.Empty)
          return false;
        var lastChar = url.Last();
        var isDigit = char.IsDigit(lastChar);
        bool oddornot;
        if (isDigit && Convert.ToInt32(lastChar.ToString()) % 2 != 0)
          oddornot = false;
        else
          oddornot = true;
        var mc = Regex.Matches(item_phone, "[0-9a-f]+");
        var macthesArray = new string[mc.Count];
        for (var i1 = 0; i1 < mc.Count; i1++)
          macthesArray[i1] = mc[i1].Value;

        string pre;
        if (oddornot)
          pre = string.Join<string>(string.Empty, macthesArray.Reverse());
        else
          pre = string.Join<string>(string.Empty, macthesArray);
        var i = 1;
        var sb = new StringBuilder();
        foreach (var item in pre)
        {
          if (i == 1)
            sb.Append(item);
          if (i == 3)
            i = 0;
          i++;
        }
        pKey = sb.ToString();
        return true;
      }
      else
        return false;
    }
    private void LoadPhone()
    {
      string urlPhone;
      if (urlPhoneImage == null || urlPhoneImage == string.Empty)
      {
        var id = InfoPage.GetDatafromText(url, "\\d{7,}");
        url = url.Replace("www.avito.ru", "m.avito.ru");
        url = url.Replace("_" + id, "._" + id);
        urlPhone = string.Format("{0}/phone/{1}?async", url, pKey);
      }
      else
      {
        var append = "http://";
        if ((url.ToLower()).StartsWith("http://"))
          append = "http://";
        else if ((url.ToLower()).StartsWith("https://"))
          append = "https://";
        urlPhone = append + "m.avito.ru" + urlPhoneImage + "?async";
      }

      var req = httpWeb.GetHttpWebReq(urlPhone);
      req.AllowAutoRedirect = true;
      req.Referer = url;
      var res = httpWeb.GetHttpWebResp(req);
      if (res != null)
      {
        phonePageContent = HttpWeb.GetContent(res);
        phonePageContent = phonePageContent.Split('"')[3].ToString();
      }
      else
        phonePageContent = null;
    }
    #endregion
  }
}
