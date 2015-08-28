using AutoRuParser.Bots;
using ParsersChe.WebClientParser.Proxy;
using System.Net;

namespace ParsersChe.WebClientParser
{
  public class WebClSepNewProxy : WebClSeperateProxy
  {
    public WebClSepNewProxy(ProxyCollSeparate proxyColl)
      : base(proxyColl)
    {
    }

    public override HttpWebRequest GetHttpWebReq(string url)
    {
      Url = url;
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = proxyColl.NewProxy;
      httpWebRequest.Timeout = 12000;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      httpWebRequest.KeepAlive = true;
      return httpWebRequest;
    }
  }
}
