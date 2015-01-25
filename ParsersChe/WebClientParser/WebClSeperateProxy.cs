using AutoRuParser.Bots;
using ParsersChe.WebClientParser.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser
{
  public class WebClSeperateProxy : WebClProxy
  {
    protected ProxyCollSeparate proxyColl;
    public WebClSeperateProxy(ProxyCollSeparate proxyColl)
    {
      this.proxyColl = proxyColl;
    }
    public override System.Net.HttpWebRequest GetHttpWebReq(string url)
    {
      this.Url = url;
      HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = proxyColl.Proxy;
      httpWebRequest.Timeout = 12000;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      httpWebRequest.KeepAlive = true;
      return httpWebRequest;
    }
    protected override System.Net.HttpWebRequest GetHttpWebReqNewProxy(string url)
    {
      this.Url = url;
      HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = proxyColl.NewProxy;
      httpWebRequest.Timeout = 8000;
      httpWebRequest.KeepAlive = true;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      return httpWebRequest;
    }
  }
}
