using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.WebClientParser.Decorators
{
    public class WebClDecoratorBase:IHttpWeb
    {
        protected readonly IHttpWeb _component;

        public WebClDecoratorBase(IHttpWeb _component)
        {
            this._component = _component;
        }

        public virtual System.Net.HttpWebRequest GetHttpWebReq(string url)
        {
            return _component.GetHttpWebReq(url);
        }

        public virtual System.Net.HttpWebResponse GetHttpWebResp(System.Net.HttpWebRequest webReq)
        {
            return _component.GetHttpWebResp(webReq);
        }

        public virtual string GetContent(System.Net.HttpWebResponse webResp)
        {
            return _component.GetContent(webResp);
        }

        public virtual string GetContent(System.Net.HttpWebResponse webResp, Encoding encoding)
        {
            return _component.GetContent(webResp, encoding);
        }


        public bool DownloadImage(System.Net.HttpWebResponse webResp, string fileName)
        {
            return _component.DownloadImage(webResp, fileName);
        }


        public void WritePostLine(ref System.Net.HttpWebRequest req, string postString)
        {
            _component.WritePostLine(ref req, postString);
        }
    }
}
