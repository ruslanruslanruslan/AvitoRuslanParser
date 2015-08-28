using System.Net;

namespace ParsersChe.WebClientParser.Proxy.CheckerProxy
{
  interface ICheckerProxy
  {
    bool IsWorkProxy(IWebProxy proxy);
  }
}
