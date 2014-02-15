using ParsersChe.WebClientParser.Proxy.CheckerProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser.Proxy
{
    sealed public class ProxyCollectionSingl:IDisposable
    {
        private ProxyCollection proxyColl=null;
        private static volatile ProxyCollectionSingl instance;
        private static object syncRoot = new Object();
        public IWebProxy NewProxy { get { return instance.proxyColl.NewProxy; } }
        public IWebProxy Proxy { get { return instance.proxyColl.Proxy; } }

        public static string ProxyUser { get; set; }
        public static string ProxyPass { get; set; }
        private static string proxyPath = "proxy.txt";

        public static string ProxyPath
        {
            get { return proxyPath; }
            set { proxyPath = value; }
        }

        private ProxyCollectionSingl()
        {
            if (!string.IsNullOrEmpty(ProxyUser) && !string.IsNullOrEmpty(ProxyPass) && !string.IsNullOrEmpty(proxyPath))
            {
                //ProxyCollUp proxyCollup = new ProxyCollUp(ProxyUser, ProxyPass, proxyPath, new AvitoCheckerProxy());
                //proxyCollup.MinCountProxy = 800;
                //proxyCollup.TimeCheckCrashProxy = 60000;
                //proxyColl = proxyCollup;
                proxyColl = new ProxyQueueTxt(ProxyUser, ProxyPass, proxyPath);
            }
            else if (!string.IsNullOrEmpty(proxyPath)) 
            {
                proxyColl = new ProxyQueueTxt( proxyPath);
            }
          
            proxyColl.ReadProxy();
            
        }
        public static ProxyCollectionSingl Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ProxyCollectionSingl();
                    }
                }

                return instance;
            }

        }


        public void Dispose()
        {
            instance.proxyColl.Dispose();
            instance = null;

        }

        public void Wait() 
        {
            var opColl = instance.proxyColl as ProxyCollUp;
                if (opColl!=null)
                {
                    opColl.Wait();
                }
        }
    }
}
