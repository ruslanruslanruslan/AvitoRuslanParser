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
    class RuslanParser2
    {
        private string pathImages2 = "images";

        public string PathImages2
        {
            get { return pathImages2; }
            set { pathImages2 = value; }
        }
        private Func<string> loadGuid = (() => MySqlDB.ResourceID());
        private Func<string> loadGuid2 = (() => MySqlDB.ResourceListID());

        public Func<string> LoadGuid
        {
            get { return loadGuid; }
            set { loadGuid = value; }
        }

        public Func<string> LoadGuid2
        {
            get { return loadGuid2; }
            set { loadGuid2 = value; }
        }


        public RuslanParser2(string user, string pass, string pathToProxy)
        {
           
        }

        public Dictionary<PartsPage, IEnumerable<string>> Run(string link)
        {
            Dictionary<PartsPage, IEnumerable<string>> result=null;
            IHttpWeb webCl = new WebCl();
            string url = link;
            try
            {
                ParserPage parser = new SimpleParserPage
                      (url, new List<IPrepareContent> {
                    new AvitoLoadImageDeferentSize(webCl,pathImages2)
                    { 
                        GetidImage=loadGuid,
                        GetidImageList=loadGuid2
                    }
                           }, webCl
                      );
                parser.RunActions();
                //MySqlDB.InsertItemResource(MySqlDB.ResourceID(), link);
                // ProxyCollectionSingl.Instance.Dispose();
                result = parser.ResultsParsing;
            }
            catch (Exception)
            {
              //  System.Windows.Forms.MessageBox.Show("Test");
            }
            return result;
        }
    }
}
