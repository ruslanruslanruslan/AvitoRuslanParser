using AutoRuParser.Bots;
using ParsersChe.WebClientParser.Proxy;
using System;
using System.IO;
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
    public virtual HttpWebRequest GetHttpWebReq(string url)
    {
      this.url = url;
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
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
    protected virtual HttpWebRequest GetHttpWebReqNewProxy(string url)
    {
      this.url = url;
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = ProxyCollectionSingl.Instance.NewProxy;
      httpWebRequest.Timeout = 8000;
      httpWebRequest.KeepAlive = true;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      return httpWebRequest;
    }

    public HttpWebResponse GetHttpWebResp(HttpWebRequest webReq)
    {
      var countTry = 20;
      var count404Eror = 0;
      var repeat = true;

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
          var code = 0;
          if (wex.Response != null)
            code = (int)((HttpWebResponse)wex.Response).StatusCode;
          if (code == 404 || code == 302)
            count404Eror++;
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
        res = null;
      return res;
    }

    public string GetContent(HttpWebResponse webResp, Encoding encoding)
    {
      string content = null;
      var countTry = 3;
      var repeat = true;
      while (repeat && countTry > 0 && webResp != null)
        try
        {
          var responseStream = webResp.GetResponseStream();
          responseStream.ReadTimeout = 8000;
          using (var sr = new StreamReader(responseStream, encoding))
          {
            content = sr.ReadToEnd();
            webResp = null;
            if (content.Equals("обновите страницу, пожалуйста"))
              throw new WebException("ParsersChe error: Proxy no Russian");
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
      var countTry = 20;
      var count404Eror = 0;
      var repeat = true;

      while (repeat && countTry > 0 && count404Eror < 3)
      {
        try
        {
          var ByteArr = Encoding.UTF8.GetBytes(postString);
          req.ContentLength = ByteArr.Length;
          req.GetRequestStream().Write(ByteArr, 0, ByteArr.Length);
          repeat = false;
        }
        catch (WebException wex)
        {
          countTry--;
          var code = 0;
          if (wex.Response != null)
            code = (int)((HttpWebResponse)wex.Response).StatusCode;
          if (code == 404 || code == 302)
            count404Eror++;
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
      var countTry = 3;
      var repeat = true;
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
      var result = false;
      var countTry = 3;
      var repeat = true;
      while (repeat && countTry > 0 && response != null)
      {
        // if the remote file was found, download it
        try
        {
          using (var inputStream = response.GetResponseStream())
          using (var outputStream = File.OpenWrite(fileName))
          {
            var buffer = new byte[4096];
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
