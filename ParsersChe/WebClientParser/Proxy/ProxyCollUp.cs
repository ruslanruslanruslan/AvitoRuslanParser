using ParsersChe.WebClientParser.Proxy.CheckerProxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ParsersChe.WebClientParser.Proxy
{
    class ProxyCollUp : ProxyCollection
    {
        private Task checkerProxyTask;
        private string  pathToCrashList="crashproxy.txt";
        private long countProxy;
        protected long CountProxy
        {
            get {
                lock (this)
                {
                    return countProxy;  
                }
            }
            set 
            {
                lock (this) 
                {
                    countProxy = value;
                }
            }
        }
        private long minCountProxy = 100;
        public long MinCountProxy
        {
            get { return minCountProxy; }
            set { minCountProxy = value; }
        }
        private Timer timer;
        private int timeCheckCrashProxy=600000;
        public int TimeCheckCrashProxy {
            get
            {
                return timeCheckCrashProxy;
            }
            set
            {
                if (value != null)
                {
                    timer.Interval = value;
                    timeCheckCrashProxy = value;
                }
            }
        }

        public string  PathToCrashList
        {
            get { return pathToCrashList; }
            set { pathToCrashList=value; }
        }
        private ICheckerProxy checkerProxy;

        public ICheckerProxy CheckerProxy
        {
            get { return checkerProxy; }
            set { checkerProxy = value; }
        }

        public ProxyCollUp(string path, ICheckerProxy checkerProxy)
        :base(path)
        {
           this.checkerProxy = checkerProxy;
           timer= new Timer(timeCheckCrashProxy);
           timer.AutoReset = true;
           timer.Elapsed += timer_Elapsed;
        }

        public ProxyCollUp(string login, string pass, string path, ICheckerProxy checkerProxy)
        :base(login,pass,path)
        {
            this.checkerProxy = checkerProxy;
            timer = new Timer(timeCheckCrashProxy);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = true;

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
             if (checkerProxyTask == null)
                 checkerProxyTask = Task.Factory.StartNew(() => CheckerCrashList());
             else if (checkerProxyTask.IsCompleted)
             {
                 checkerProxyTask = Task.Factory.StartNew(() => CheckerCrashList());
             }
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
             if (CountProxy <= minCountProxy) 
             {
              System.Threading.Thread.Sleep(60000);
             }
             lock (this)
             {
                 string resultUrl;
                 queue.TryDequeue(out resultUrl);
                 WriteCurrentProxyToCrachList();
                 var proxy = new WebProxy(resultUrl);
                 if (credential != null)
                     proxy.Credentials = credential;
                 Proxy = proxy;
                 return proxy;
             }
         }
         public override void ReadProxy()
         {
             var lines = File.ReadAllLines(path);
             var proxy = File.ReadAllLines(pathToCrashList);
           lines=  lines.Except(proxy).ToArray<string>();
             countProxy = lines.Length;
             if (lines != null)
             {
                 queue = new ConcurrentQueue<string>(lines);
                 UpdateNewProxy(lines[0]);
             }
         }
        
         private void WriteCurrentProxyToCrachList()
         {
             lock (this)
             {
                 using (TextWriter tw = new StreamWriter(pathToCrashList, true))
                 {
                     WebProxy wb=Proxy as WebProxy;
                   string url=  wb.Address.Authority;
                   tw.WriteLine(url);
                 }
                 CountProxy--;
                 if (!timer.Enabled) { timer.Enabled = true; }
             }
         }

         private void CheckerCrashList()
         {
             var proxy = File.ReadAllLines(pathToCrashList);
             proxy.Reverse<string>();
             if (proxy != null)
             {
                 List<string> listBad = new List<string>();
                 foreach (var item in proxy)
                 {
                     WebProxy wp = new WebProxy(new Uri("http://"+item));
                     if (credential != null) wp.Credentials = credential;
                     bool res = checkerProxy.IsWorkProxy(wp);
                     if (res)
                     {
                         queue.Enqueue(item);
                     }
                     else 
                     {
                         listBad.Add(item);
                     }
                 }
                 File.WriteAllLines(pathToCrashList, listBad);
             }
         }

         public override void WriteProxy()
         {
             File.WriteAllLines(path, queue);
         }
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
         {
             CheckerCrashList();
         }

        public void Wait() 
        {
            if (checkerProxyTask != null) 
            {
                checkerProxyTask.Wait();
            }
        }



        public override void ReadProxy(IList<string> proxyList)
        {
            throw new NotImplementedException();
        }
    }
}
