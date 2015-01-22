using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsersChe.WebClientParser
{
  public interface IHttpWeb
  {
    HttpWebRequest GetHttpWebReq(string url);
    HttpWebResponse GetHttpWebResp(HttpWebRequest webReq);
    string GetContent(HttpWebResponse webResp);
    string GetContent(HttpWebResponse webResp, Encoding encoding);
    bool DownloadImage(HttpWebResponse webResp, string fileName);
    void WritePostLine(ref HttpWebRequest req, string postString);
  }
}
