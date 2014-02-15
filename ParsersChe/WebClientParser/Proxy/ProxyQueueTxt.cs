using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
namespace ParsersChe.WebClientParser.Proxy
{
    public class ProxyQueueTxt:ProxyCollection
    {
        public ProxyQueueTxt(string path)
        :base(path)
        {
        }
        private static object obj = new object();
        public ProxyQueueTxt(string login, string pass, string path)
        :base(login,pass,path)
        {

        }
        private ConcurrentQueue<string> queue;

        public override IWebProxy NewProxy
        {
            get
            {
                return UpdateNewProxy();
            }

        }

        private void UpdateNewProxy(string url) 
        {
            lock (this)
            {
                var proxy = new WebProxy(url);
                if (credential != null)
                    proxy.Credentials = credential;
                Proxy = proxy;
            }
        }

        private IWebProxy UpdateNewProxy() 
        {
            lock (this)
            {
                string resultUrl;
                queue.TryDequeue(out resultUrl);
                queue.Enqueue(resultUrl);
                var proxy = new WebProxy(resultUrl);
                if (credential != null)
                    proxy.Credentials = credential;
                Proxy = proxy;
                return proxy;
            }
        }
        public override void ReadProxy()
        {
           var lines= File.ReadAllLines(path);
           if (lines != null) 
           {
               queue = new ConcurrentQueue<string>(lines);
               UpdateNewProxy(lines[0]);
           }
        }

        public override void ReadProxy(IList<string> proxyList)
        {
            var lines = proxyList;
            if (lines != null)
            {
                queue = new ConcurrentQueue<string>(lines);
                UpdateNewProxy(lines[0]);
            }
        }

        public override void WriteProxy()
        {
            lock (obj)
            {
                File.WriteAllLines(path, queue); 
            }
        }
    }
}
