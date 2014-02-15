using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser.Proxy.CheckerProxy
{
    interface ICheckerProxy
    {
        bool IsWorkProxy(IWebProxy proxy);
    }
}
