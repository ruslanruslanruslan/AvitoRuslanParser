using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using ParsersChe.WebClientParser;

namespace ParsersChe.Bot
{
    abstract public class WebClientBot:IDisposable
    {
        protected int CodeResponse { get; set; }
        protected string Content { get; set; }
        protected IHttpWeb WebCl { get; set; }
        public string Url { get; protected set; }
        protected HtmlDocument Doc { get; set; }

        public void WriteSerizialiationCookie() 
        {
            //string path = PathToCookie;
            //if (this.cookieContainers!=null)
            //InfoPage.WriteSerObjectToFile(path, this.cookieContainers);
        }

        public void ReadSerizialiationCookie() 
        {
            //string path = PathToCookie;
            //this.cookieContainers = new CookieContainer();
           
          // this.cookieContainers = InfoPage.ReadSerObjectToFile(path) as CookieContainer;
        }

        public void Dispose()
        {
          //  WriteSerizialiationCookie();
        }
    }
}
