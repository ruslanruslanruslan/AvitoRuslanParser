using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser.Decorators
{
    public class CookieDecorator:WebClDecoratorBase
    {
        private CookieContainer cookieContainerCl;
        
        public CookieDecorator(IHttpWeb component):base(component)
        {
            cookieContainerCl = new CookieContainer();
        }

        public override System.Net.HttpWebRequest GetHttpWebReq(string url)
        {
            var baseResult=base.GetHttpWebReq(url);
            baseResult.CookieContainer = cookieContainerCl;
            return baseResult;
        }
    }
}
