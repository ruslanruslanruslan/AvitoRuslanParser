using AutoRuParser.Bots;
using ParsersChe.WebClientParser.Proxy;
using System.Net;

namespace ParsersChe.WebClientParser
{
  public class WebClSeperateProxy : WebClProxy
  {
    protected ProxyCollSeparate proxyColl;
    public WebClSeperateProxy(ProxyCollSeparate proxyColl)
    {
      this.proxyColl = proxyColl;
    }
    public override HttpWebRequest GetHttpWebReq(string url)
    {
      Url = url;
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = proxyColl.Proxy;
      httpWebRequest.Timeout = 12000;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      httpWebRequest.KeepAlive = true;
      return httpWebRequest;
    }
    protected override HttpWebRequest GetHttpWebReqNewProxy(string url)
    {
      Url = url;
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = proxyColl.NewProxy;
      httpWebRequest.Timeout = 8000;
      httpWebRequest.KeepAlive = true;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      return httpWebRequest;
    }
  }
}
