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
  public class WebClProxy : IHttpWeb
  {
    private string url;

    protected string Url
    {
      get { return url; }
      set { url = value; }
    }
    private object obj = new object();
    public virtual System.Net.HttpWebRequest GetHttpWebReq(string url)
    {
      this.url = url;
      HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = ProxyCollectionSingl.Instance.Proxy;
      httpWebRequest.Timeout = 12000;
      httpWebRequest.KeepAlive = true;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      return httpWebRequest;
    }

    public void UpProxy()
    {
      var p = ProxyCollectionSingl.Instance.NewProxy;
    }
    protected virtual System.Net.HttpWebRequest GetHttpWebReqNewProxy(string url)
    {
      this.url = url;
      HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = ProxyCollectionSingl.Instance.NewProxy;
      httpWebRequest.Timeout = 8000;
      httpWebRequest.KeepAlive = true;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      return httpWebRequest;
    }

    public System.Net.HttpWebResponse GetHttpWebResp(System.Net.HttpWebRequest webReq)
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
          webReq = GetHttpWebReqNewProxy(url);
        }
        catch
        {
          res = null;
          countTry--;
          webReq = GetHttpWebReqNewProxy(url);
        }
      if (res != null && (int)res.StatusCode == 302)
      {
        res = null;
      }
      return res;
    }


    public string GetContent(System.Net.HttpWebResponse webResp, Encoding encoding)
    {
      string content = null;
      int countTry = 3;
      bool repeat = true;
      while (repeat && countTry > 0 && webResp != null)
        try
        {
          var responseStream = webResp.GetResponseStream();
          responseStream.ReadTimeout = 8000;
          using (StreamReader sr = new StreamReader(responseStream, encoding))
          {
            content = sr.ReadToEnd();
            webResp.Close();
            if (content.Equals("обновите страницу, пожалуйста")) throw new WebException("Proxy no Russian");
            repeat = false;
          }
        }
        catch (WebException exWeb)
        {
          countTry--;
          content = null;
          Log(exWeb, "GetContent");
          var webReq = GetHttpWebReqNewProxy(url);
          if (webReq != null)
            webResp = GetHttpWebResp(webReq);
        }

      return content;
    }


    public string GetContent(HttpWebResponse webResp)
    {
      return GetContent(webResp, Encoding.UTF8);
    }

    private void Log(WebException exWeb, string nameMethod)
    {

      lock (obj)
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


    public void WritePostLine(ref HttpWebRequest req, string postString)
    {
      int countTry = 20;
      int count404Eror = 0;
      bool repeat = true;

      while (repeat && countTry > 0 && count404Eror < 3)
      {
        try
        {
          byte[] ByteArr = System.Text.Encoding.UTF8.GetBytes(postString);
          req.ContentLength = ByteArr.Length;
          req.GetRequestStream().Write(ByteArr, 0, ByteArr.Length);
          repeat = false;
        }
        catch (WebException wex)
        {
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
          var req2 = GetHttpWebReqNewProxy(url);
          req2.Method = req.Method;
          req2.Referer = req.Referer;
          req2.Accept = req.Accept;
          req2.ContentType = req.ContentType;
          req2.Headers["X-Requested-With"] = req.Headers["X-Requested-With"];
          req2.AllowAutoRedirect = req.AllowAutoRedirect;
          req = req2;
        }
        catch
        {
          countTry--;
          req = GetHttpWebReqNewProxy(url);
        }
      }
    }
    public Stream GetStream(HttpWebResponse response)
    {
      int countTry = 3;
      bool repeat = true;
      Stream str = null;
      while (repeat && countTry > 0 && response != null)
      {
        // if the remote file was found, download it
        try
        {
          str = response.GetResponseStream();
          repeat = false;
        }
        catch (WebException exWeb)
        {
          countTry--;
          Log(exWeb, "DownloadImage");
          var webReq = GetHttpWebReqNewProxy(url);
          if (webReq != null)
            response = GetHttpWebResp(webReq);
        }
      }
      return str;

    }

    public bool DownloadImage(HttpWebResponse response, string fileName)
    {
      bool result = false;
      int countTry = 3;
      bool repeat = true;
      while (repeat && countTry > 0 && response != null)
      {
        // if the remote file was found, download it
        try
        {
          using (Stream inputStream = response.GetResponseStream())
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
          countTry--;

          Log(exWeb, "DownloadImage");
          var webReq = GetHttpWebReqNewProxy(url);
          if (webReq != null)
            response = GetHttpWebResp(webReq);
        }
        catch
        {
          countTry--;
          var webReq = GetHttpWebReqNewProxy(url);
          if (webReq != null)
            response = GetHttpWebResp(webReq);
        }
      }
      return result;

    }
  }
}
