using AutoRuParser.Bots;
using ParsersChe.WebClientParser.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser
{
  public class WebClSepNewProxy : WebClSeperateProxy
  {
    public WebClSepNewProxy(ProxyCollSeparate proxyColl)
      : base(proxyColl)
    {
    }

    public override System.Net.HttpWebRequest GetHttpWebReq(string url)
    {
      this.Url = url;
      HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
      httpWebRequest.UserAgent = HttpHeaders.UserAgentIE10.Value;
      httpWebRequest.Proxy = proxyColl.NewProxy;
      httpWebRequest.Timeout = 12000;
      httpWebRequest.ServicePoint.ConnectionLimit = 200;
      httpWebRequest.KeepAlive = true;
      return httpWebRequest;
    }
  }
}
