using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser.Proxy
{
  abstract public class ProxyCollection : IDisposable
  {
    public abstract IWebProxy NewProxy { get; }
    public IWebProxy Proxy { get; protected set; }
    protected NetworkCredential credential;
    protected IWebProxy webProxy;
    protected string path;

    public ProxyCollection(string login, string password, string path)
      : this(path)
    {
      if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
        credential = new NetworkCredential(login, password);

    }
    public ProxyCollection(string path)
    {
      this.path = path;
    }


    public abstract void ReadProxy();
    public abstract void ReadProxy(IList<string> proxyList);

    public abstract void WriteProxy();

    public void Dispose()
    {
      this.WriteProxy();
    }
  }
}
