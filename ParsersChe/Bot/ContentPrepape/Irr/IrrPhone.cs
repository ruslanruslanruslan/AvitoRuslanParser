using HtmlAgilityPack;
using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Irr
{
    class IrrPhone:IPrepareContent
    {
         #region Fields
        private string content;
        private string phonePageContent;
        private string url;
        private HtmlDocument doc;
        private string pKey;
        private IHttpWeb httpWeb;
        #endregion
        #region Constructors
        public IrrPhone(IHttpWeb httpWeb)
        {
            this.httpWeb = httpWeb;
        }
        public IrrPhone()
        {
            httpWeb = new WebCl();
        }
        #endregion
        public IHttpWeb HttpWeb
        {
            get { return httpWeb; }
        }

        public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlDocument doc)
        {
            this.content = content;
            this.url = url;
            this.doc = doc;
            PartsPage typeResult = PartsPage.Phone;
            return new KeyValuePair<PartsPage, IEnumerable<string>>(typeResult, null);
        }
    }
}
