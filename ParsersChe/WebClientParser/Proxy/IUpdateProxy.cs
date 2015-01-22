using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.WebClientParser.Proxy
{
  public interface IUpdateProxy
  {
    void CheckCrahsList();
    void RemoveProxyToCrahsList();
  }
}
