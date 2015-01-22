using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser.Proxy.CheckerProxy
{
  class AvitoCheckerProxy : ICheckerProxy
  {
    private readonly string url = "http://www.avito.ru/s/a/i/0.gif";
    public bool IsWorkProxy(System.Net.IWebProxy proxy)
    {
      bool result = true;
      IHttpWeb web = new WebCl();
      HttpWebRequest req = web.GetHttpWebReq(url);
      req.Timeout = 8000;
      req.Proxy = proxy;
      try
      {
        HttpWebResponse res = web.GetHttpWebResp(req);
        if ((int)res.StatusCode != 200)
        {
          result = false;
        }
      }
      catch
      {
        result = false;
      }
      return result;
    }
  }
}
