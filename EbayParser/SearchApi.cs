using eBay.Services;
using eBay.Services.Finding;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AvitoRuslanParser.EbayParser
{
    class SearchApi
    {
        private const string itemInfoApi = "http://open.api.ebay.com/shopping?callname=GetMultipleItems&responseencoding=XML&appid=Artsiom97-3905-4c09-9e4e-4144ac444e6&siteid=0&version=525&ItemID={0}&IncludeSelector=Description,ItemSpecifics,Details";
        private const string itemInfoDesc = "http://open.api.ebay.com/shopping?callname=GetMultipleItems&responseencoding=XML&appid=Artsiom97-3905-4c09-9e4e-4144ac444e6&siteid=0&version=525&ItemID={0}&IncludeSelector=TextDescription";


        public string Keywords { get; set; }
        public int CategoryId { get; set; }
        public int PerPage { get; set; }

        private string section;

        public string Section
        {
            set
            {
                GetDataFromLink(value);
                section = value;
            }
        }

        private void GetDataFromLink(string link) 
        {
            string pattern1 = "_nkw=(.+?)&";
            string patternt2 = "_nkw=(.+)";
            string patternKeyWord = null;

            patternKeyWord = Regex.IsMatch(link, pattern1) ? pattern1 : patternt2;
            Keywords = Regex.Match(link, patternKeyWord).Groups[1].Value;

            string patternCategory = null;
            pattern1 = "_sacat=(\\d+)";
            patternt2 = "/(\\d+)/";
            patternCategory = Regex.IsMatch(link, pattern1) ? pattern1 : patternt2;
            string category = Regex.Match(link, patternCategory).Groups[1].Value;
            CategoryId = Convert.ToInt32(category);

        }

        public IEnumerable<long> SearchLinks()
        {
            IList<long> list = new List<long>();
            try
            {
                //181419645692
                ClientConfig config = new ClientConfig();
                config.EndPointAddress = "http://svcs.ebay.com/services/search/FindingService/v1";
                config.ApplicationId = "Artsiom97-3905-4c09-9e4e-4144ac444e6";

                FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

                FindItemsAdvancedRequest request = new FindItemsAdvancedRequest();
                // Set request parameters
                request.keywords = Keywords;
                request.categoryId = new string[] { CategoryId.ToString() };
                PaginationInput pi = new PaginationInput();
                pi.entriesPerPage = PerPage;

                pi.entriesPerPageSpecified = true;
                request.paginationInput = pi;
                // Call the service
                FindItemsAdvancedResponse response = client.findItemsAdvanced(request);
                // Show output
                SearchItem[] items = response.searchResult.item;

                for (int i = 0; i < items.Length; i++)
                {
                    list.Add(Convert.ToInt64(items[i].itemId));
                }
            }
            catch (Exception ex)
            {
                // Handle exception if any
                Console.WriteLine(ex);
            }
            return list;

        }

        public static GetMultipleItemsResponse ParseItems(IEnumerable<long> idsAd)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in idsAd)
            {
                sb.Append(item.ToString());
                sb.Append(",");
            }
            string linkApi = string.Format(itemInfoApi, sb.ToString());
            string linkApiDesc = string.Format(itemInfoDesc, sb.ToString());
            string content = null;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                content = wc.DownloadString(linkApi);
            }
            string contentDesc = null;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                contentDesc = wc.DownloadString(linkApiDesc);
            }

            XmlSerializer ser = new XmlSerializer(typeof(GetMultipleItemsResponse));
            GetMultipleItemsResponse items;

            using (StringReader reader = new StringReader(content))
            {
                items = (GetMultipleItemsResponse)ser.Deserialize(reader);
            }

            GetMultipleItemsResponse itemDesc = null;
            using (StringReader reader = new StringReader(contentDesc))
            {
                itemDesc = (GetMultipleItemsResponse)ser.Deserialize(reader);
            }

            if (items != null && items.Item != null && items.Item.Count() > 0)
            {
                for (int i = 0; i < items.Item.Count(); i++)
                {
                    for (int j = 0; j < items.Item[i].PictureURL.Length; j++)
                    {
                        string original = items.Item[i].PictureURL[j];
                        string pattern = "_\\d+\\.";
                        string newV= "_57.";
                        string newsrc=Regex.Replace(original, pattern,newV);
                        items.Item[i].PictureURL[j] = newsrc;
                    }
                }

                GetImageFromDescription(items, itemDesc);
            }
            return items;

        }

        public static void GetImageFromDescription(GetMultipleItemsResponse items, GetMultipleItemsResponse itemsDesc)
        {
            if (items != null && items.Item != null && items.Item.Count() > 0)
            {
                for (int i = 0; i < items.Item.Count(); i++)
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(items.Item[i].Description);

                    string desc = HttpUtility.HtmlDecode(GetText(doc.DocumentNode).Replace("&nbsp;",""));

                    desc = Regex.Replace(desc, "<[\\s|\\S]+?>", string.Empty);
                    desc = Regex.Replace(desc,"\\t","");
                    string paatern="(\r\n){2,}";
                    string paatern2 = "(\r\n ){2,}";
                    string pattern3=".+\\{[\\s|\\S]+?\\}";
                    string  trueDesc=Regex.Replace(desc,pattern3,string.Empty);

                    trueDesc=Regex.Replace(trueDesc,"<[\\s|\\S]+?>",string.Empty);
                    trueDesc = Regex.Replace(trueDesc, "( ){2,}", " ");
                    trueDesc = Regex.Replace(trueDesc, "(\n){2,}", "\n");
                    trueDesc = Regex.Replace(trueDesc, "( \n){2,}", "\n");


                    trueDesc = Regex.Replace(trueDesc, paatern, "\r\n");
                    trueDesc = Regex.Replace(trueDesc, paatern2, "\r\n ");
                    trueDesc = Regex.Replace(trueDesc, "( \r\n){2,}", "\r\n ");
                    trueDesc = Regex.Replace(trueDesc, "(\n){2,}", "\n");

                     items.Item[i].Description = trueDesc.Trim();
                        items.Item[i].PictureURL = NewArrayImg(items.Item[i].PictureURL, doc);
                }
            }
        }

        public static string GetText(HtmlNode doc)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in doc.ChildNodes)
            {
                if (item.Name != "script")
                {
                    string text = null;
                    if (item.HasChildNodes)
                    {
                       text=  GetText(item);
                    }
                    else
                    {
                        text = item.InnerText;
                    }
                    sb.AppendLine(text);
                }
            }
            return sb.ToString();
        }

        public static string[] NewArrayImg(string[] oldArray, HtmlDocument doc)
        {
            var checkArray = oldArray.Select(x => Regex.Replace(x, "\\$_.+", ""));
            var newlist = new List<string>();
            var imgnode = doc.DocumentNode.SelectNodes("//img");
            if (imgnode != null)
            {
                foreach (var item in imgnode)
                {
                    string src = item.GetAttributeValue("src", "");
                    bool isNewImg = !checkArray.Contains(Regex.Replace(src, "\\$_.+", ""));
                    if (isNewImg)
                    {
                        var bytes = DownImg(src);
                        if (bytes!=null)
                        {
                            using (Stream stream = new MemoryStream(bytes))
                            {
                                var bmp = Image.FromStream(stream);
                                int sMore;
                                int sLess;
                                int temp;
                                sMore = bmp.Size.Height;
                                sLess = bmp.Size.Width;
                                if (sMore < sLess) { temp = sMore; sMore = sLess; sLess = temp; }
                                if (sMore > 639 && sLess > 479)
                                {
                                    newlist.Add(src);
                                }
                            }
                        }
                    }
                }
            }
            newlist.AddRange(oldArray);
            return newlist.ToArray();
        }
        private static byte[] DownImg(string src) 
        {
            byte[] bytes = null;
            try
            {
                WebClient wc = new WebClient();
                 bytes = wc.DownloadData(src);
            }
            catch (Exception)
            {
                
            }
         return bytes;
        }


        public static void SaveImage()
        {

        }
    }
}
