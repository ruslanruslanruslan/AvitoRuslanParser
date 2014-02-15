using ParsersChe.Bot.ActionOverPage;
using ParsersChe.Bot.ActionOverPage.ContentPrepape;
using ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.Bot.ContentPrepape.Avito;
using ParsersChe.WebClientParser;
using ParsersChe.WebClientParser.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvitoRuslanParser
{
    class RuslanParser
    {
        private string pathImages="images";

        public string PathImages
        {
            get { return pathImages; }
            set { pathImages= value; }
        }
        private Func<string> loadGuid = (() => Guid.NewGuid().ToString());

        public Func<string> LoadGuid
        {
            get { return loadGuid; }
            set { loadGuid = value; }
        }
        

        public RuslanParser(string user,string pass,string pathToProxy)
        {
            ProxyCollectionSingl.ProxyPass = pass;
            ProxyCollectionSingl.ProxyUser = user;
            ProxyCollectionSingl.ProxyPass = pathToProxy;
        }

        public Dictionary<PartsPage,IEnumerable<string>> Run(string link) 
        {
            IHttpWeb webCl = new WebClProxy();
            string url = link;
            ParserPage parser = new SimpleParserPage
              (url, new List<IPrepareContent> {
                    new AvitoPhones(webCl),
                    new AvitoCity(),
                    new AvitoSeller(),
                    new AvitoTitle(),
                    new AvitoCost(),
                    new AvitoBodyAd(),
                    new AvitoSubCategory(),
                    new AvitoLoadImageDeferentSize(webCl,pathImages)
                    { 
                        GetidImage=loadGuid
                    }
                           }, webCl
              );
            parser.RunActions();
            ProxyCollectionSingl.Instance.Dispose();
            var result = parser.ResultsParsing;
            return result;
        }
    }
}
