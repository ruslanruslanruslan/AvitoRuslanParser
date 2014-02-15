using AutoRuParser.Bots;
using ParsersChe.WebClientParser.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser
{
    public class WebClAlwaysNewProxy:IHttpWeb
    {
        private string url;
        
        public virtual  System.Net.HttpWebRequest GetHttpWebReq(string url)
        {
            this.url = url;
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
            httpWebRequest.Proxy = ProxyCollectionSingl.Instance.NewProxy;
            httpWebRequest.Timeout = 8000;
            return httpWebRequest;
        }

        public virtual System.Net.HttpWebResponse GetHttpWebResp(System.Net.HttpWebRequest webReq)
        {
            int countTry = 20;
            bool repeat=true;
            HttpWebResponse res = null;
            while(repeat && countTry>0)
            try
            {
               
                 res = (HttpWebResponse)webReq.GetResponse();
                repeat = false;
            }
            catch (WebException wex) 
            {
                countTry--;
                File.AppendAllText("log.txt", wex.Message+Environment.NewLine);
                File.AppendAllText("log.txt", "++" + Environment.NewLine);
                File.AppendAllText("log.txt", wex.Status.ToString() + Environment.NewLine);
                File.AppendAllText("log.txt", "++" + Environment.NewLine);
                File.AppendAllText("log.txt", "GetHttpWebResp" + Environment.NewLine);
                File.AppendAllText("log.txt", "------" + Environment.NewLine);
                webReq = GetHttpWebReq(url);
            }

            return res;
        }


        public virtual string GetContent(System.Net.HttpWebResponse webResp, Encoding encoding)
        {
            string content=null;
            int countTry = 3;
            bool repeat=true;
            while (repeat && countTry > 0)
            try
            {
                var responseStream = webResp.GetResponseStream();
                responseStream.ReadTimeout = 8000;
                using (StreamReader sr = new StreamReader(responseStream, encoding))
                {
                    content = sr.ReadToEnd();
                    repeat = false;
                }
            }
            catch (WebException exWeb) 
            {
                countTry--;
                File.AppendAllText("log.txt", exWeb.Message + Environment.NewLine);
                File.AppendAllText("log.txt", "++" + Environment.NewLine);
                File.AppendAllText("log.txt", exWeb.Status.ToString() + Environment.NewLine);
                File.AppendAllText("log.txt", "++" + Environment.NewLine);
                File.AppendAllText("log.txt", "GetContent" + Environment.NewLine);
                File.AppendAllText("log.txt", "------" + Environment.NewLine);
              var   webReq = GetHttpWebReq(url);
              webResp = GetHttpWebResp(webReq);
            }
            return content;
        }


        public string GetContent(HttpWebResponse webResp)
        {
            return GetContent(webResp, Encoding.GetEncoding("windows-1251"));
        }


        public bool DownloadImage(HttpWebResponse webResp,string fileName)
        {
            throw new NotImplementedException();
        }


        public void WritePostLine(ref HttpWebRequest req, string postString)
        {
            throw new NotImplementedException();
        }
    }
}
