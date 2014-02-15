using AutoRuParser.Bots;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser
{
    public class WebCl : IHttpWeb
    {
        private string url;
        public virtual HttpWebRequest GetHttpWebReq(string url)
        {
            this.url = url;
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
            return httpWebRequest;
        }

        public HttpWebResponse GetHttpWebResp(HttpWebRequest webReq)
        {
            int countTry = 20;
            int count404Eror = 0;
            bool repeat = true;

            HttpWebResponse res = null;
            while (repeat && countTry > 0 && count404Eror < 3)
                try
                {
                    res = (HttpWebResponse)webReq.GetResponse();
                    repeat = false;

                }
                catch (WebException wex)
                {
                    res = null;
                    countTry--;
                    int code = 0;
                    if (wex.Response != null)
                    {
                        code = (int)((HttpWebResponse)wex.Response).StatusCode;
                    }
                    if (code == 404 || code == 302)
                    {
                        count404Eror++;
                    }
                    Log(wex, "GetHttpWebResp");
                }
                catch (Exception ex)
                {
                    res = null;
                    countTry--;
                    int code = 0;
                    webReq = GetHttpWebReq(url);
                }
            if (res != null && (int)res.StatusCode == 302)
            {
                res.Close();
                res = null;
            }
            return res;
        }

        public string GetContent(HttpWebResponse webResp)
        {
            return GetContent(webResp, Encoding.GetEncoding("windows-1251"));
        }
        public string GetContent(HttpWebResponse webResp, Encoding encoding)
        {
            string content=string.Empty;
            try
            {
                using (var responseStream = webResp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, encoding))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
            }
            return content;
        }


        public bool DownloadImage(HttpWebResponse webResp, string fileName)
        {
            bool result = false;
            int countTry = 3;
            bool repeat = true;
            while (repeat && countTry > 0 && webResp != null)
            {
                // if the remote file was found, download it
                try
                {
                    using (Stream inputStream = webResp.GetResponseStream())
                    using (Stream outputStream = File.OpenWrite(fileName))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        do
                        {
                            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                            outputStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                    result = true;
                    repeat = false;
                }
                catch (WebException exWeb)
                {

                }
            }
            return result;
        }


        public void WritePostLine(ref HttpWebRequest req, string postString)
        {
            byte[] ByteArr = System.Text.Encoding.UTF8.GetBytes(postString);
            req.ContentLength = ByteArr.Length;
            req.GetRequestStream().Write(ByteArr, 0, ByteArr.Length);
        }

        private void Log(WebException exWeb, string nameMethod)
        {
            try
            {
                File.AppendAllText("logWEB.txt", exWeb.Message + Environment.NewLine);
                File.AppendAllText("logWEB.txt", "++" + Environment.NewLine);

                File.AppendAllText("logWEB.txt", exWeb.Status.ToString() + Environment.NewLine);
                File.AppendAllText("logWEB.txt", "++" + Environment.NewLine);
                File.AppendAllText("logWEB.txt", nameMethod + Environment.NewLine);
                File.AppendAllText("logWEB.txt", "------" + Environment.NewLine);
            }
            catch (Exception)
            {
                
               
            }
               
        }
    }
}
