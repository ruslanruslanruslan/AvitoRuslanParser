using AntigateUnravel;
using AutoRuParser.Bots;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ParsersChe.Bot.ActionOverPage.ContentPrepape
{
    public class AvitoSendMessageToOwner : WebClientBot, IPrepareContent
    {
        private static readonly string linkOnCaptcha = "http://www.avito.ru/captcha?1274071207028";
        private string captchaKey;
        private static readonly string avitoHost = "http://www.avito.ru/";

        private EmailUnit email;

        public EmailUnit Email
        {
            get { return email; }
            set { email = value; }
        }

        public AvitoSendMessageToOwner(IHttpWeb webCl, EmailUnit email)
            : this(webCl)
        {
            email.Message += Environment.NewLine + Guid.NewGuid().ToString();
            this.email = email;
        }
        public AvitoSendMessageToOwner(IHttpWeb webCl)
        {
            this.WebCl = webCl;
        }
        public KeyValuePair<EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
        {
            this.Content = content;
            this.Url = url;
            this.Doc = doc;
            string res = null;
            int count = 3;
            if (IsPermessionEmail())
            {
                while (string.IsNullOrEmpty(res) || (res.Equals("error") && count > 0))
                {
                    LoadCaptcha();
                    res = SendMessage();
                    count--;
                }
            }
            else { res = "No Permission"; }
            PartsPage typeResult = PartsPage.MessageSend;
            if (res != null)
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(typeResult, new List<string>()
            {
                res
            });
            }
            else
            {
                return new KeyValuePair<PartsPage, IEnumerable<string>>(typeResult, null);
            }
        }

        private bool IsPermessionEmail() 
        {
            var res = Doc.DocumentNode.SelectSingleNode("//span[@id='write2author']");
            if (res != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        private void LoadCaptcha()
        {
            int count = 3;
            while (string.IsNullOrEmpty(captchaKey) && count > 0)
            {
                var req = WebCl.GetHttpWebReq(linkOnCaptcha);
                req.Referer = Url;
                var resp = WebCl.GetHttpWebResp(req);
                if (resp != null)
                {
                    Stream streamImg = resp.GetResponseStream();
                    Antigate antigate = new Antigate();
                    antigate.UploadCaptha(streamImg);
                    captchaKey = antigate.SendResponses();
                }
                count--;
            }
        }

        private string SendMessage()
        {
            if (string.IsNullOrEmpty(captchaKey)) { return "captchaKey is empty"; }
            string result = null;
            string urlPrepare = Url.Replace(avitoHost, "").Replace('/', '_');
            string urlSendMessage = avitoHost + "items/write/" + urlPrepare;
            #region postLineCreate
            StringBuilder postLine = new StringBuilder();
            postLine.Append("name=");
            postLine.Append(HttpUtility.UrlEncode(email.Name));
            postLine.Append("&");
            postLine.Append("email=");
            postLine.Append(HttpUtility.UrlEncode(email.Email));
            postLine.Append("&");
            postLine.Append("comment=");
            postLine.Append(HttpUtility.UrlEncode(email.Message));
            postLine.Append("&");
            postLine.Append("captcha=");
            postLine.Append(captchaKey); ;
            postLine.Append("&subscribe-position=2");
            #endregion
            string postString = postLine.ToString();

            HttpWebRequest req = WebCl.GetHttpWebReq(urlSendMessage);
            req.AllowAutoRedirect = true;
            
            req.Referer = Url;
            req.Method = "POST";
            req.Accept = HttpHeaders.AcceptSlash.Value;
            req.ContentType = HttpHeaders.ContentTypeUrlEncoded.Value + "; charset=UTF-8";
            req.Headers["X-Requested-With"] = HttpHeaders.XRequestedWithDef.Value;
            WebCl.WritePostLine(ref req,postString);
            HttpWebResponse res = WebCl.GetHttpWebResp(req);
            if (res != null)
            {
                result = WebCl.GetContent(res, Encoding.UTF8);
                if (!result.Equals("done")) { result = "error"; }
            }
            return result;

        }
    }
}
