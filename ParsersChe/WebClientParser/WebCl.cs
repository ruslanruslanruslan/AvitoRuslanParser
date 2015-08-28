using AutoRuParser.Bots;
using System;
using System.IO;
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
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
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
        }
        catch
        {
          res = null;
          countTry--;
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
      var content = string.Empty;
      Stream responseStream = null;
      try
      {
        try
        {
          responseStream = webResp.GetResponseStream();
          using (var sr = new StreamReader(responseStream, encoding))
          {
            responseStream = null;
            content = sr.ReadToEnd();
          }
        }
        finally
        {
          if (responseStream != null)
            responseStream.Dispose();
        }
      }
      catch (Exception)
      {
      }
      return content;
    }


    public bool DownloadImage(HttpWebResponse webResp, string fileName)
    {
      var result = false;
      var countTry = 3;
      var repeat = true;
      while (repeat && countTry > 0 && webResp != null)
      {
        // if the remote file was found, download it
        try
        {
          using (var inputStream = webResp.GetResponseStream())
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
        catch
        {

        }
      }
      return result;
    }


    public void WritePostLine(ref HttpWebRequest req, string postString)
    {
      var ByteArr = Encoding.UTF8.GetBytes(postString);
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
