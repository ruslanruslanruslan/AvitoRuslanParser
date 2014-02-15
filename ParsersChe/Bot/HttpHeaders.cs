using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRuParser.Bots
{
    public sealed  class  HttpHeaders
    {
        public static readonly HttpHeaders UserAgentIE10 = new HttpHeaders("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        public static readonly HttpHeaders AcceptXhtml = new HttpHeaders("Accept", "text/html, application/xhtml+xml, */*");
        public static readonly HttpHeaders AcceptSlash = new HttpHeaders("Accept", "*/*");
      
        public static readonly HttpHeaders AcceptTextHtml = new HttpHeaders("Accept","text/html, */*; q=0.01");
        public static readonly HttpHeaders ContentTypeUrlEncoded = new HttpHeaders("Content-type","application/x-www-form-urlencoded");
        public static readonly HttpHeaders XRequestedWithDef = new HttpHeaders("X-Requested-With", "XMLHttpRequest");

        private HttpHeaders(string name,string value)
        {
            Value = value;
            Name = name;
        }

        public string Value { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return Value;
        }


    }
 

    
    
}
