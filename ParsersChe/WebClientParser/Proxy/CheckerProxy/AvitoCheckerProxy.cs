namespace ParsersChe.WebClientParser.Proxy.CheckerProxy
{
  class AvitoCheckerProxy : ICheckerProxy
  {
    private readonly string url = "http://www.avito.ru/s/a/i/0.gif";
    public bool IsWorkProxy(System.Net.IWebProxy proxy)
    {
      var result = true;
      IHttpWeb web = new WebCl();
      var req = web.GetHttpWebReq(url);
      req.Timeout = 8000;
      req.Proxy = proxy;
      try
      {
        var res = web.GetHttpWebResp(req);
        if ((int)res.StatusCode != 200)
          result = false;
      }
      catch
      {
        result = false;
      }
      return result;
    }
  }
}
