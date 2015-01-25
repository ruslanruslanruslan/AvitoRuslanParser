using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace AntigateUnravel
{
  #region readKey
  public class UserInfo2
  {

    public string AntigeitKey { get; set; }
  }

  public class UserInfoReader2
  {
    private static UserInfo2 userInfo;
    private static object obj = new object();
    public static UserInfo2 Info
    {
      get
      {
        if (userInfo == null)
        {
          userInfo = ReadUserInfo("info.txt");
        }
        return userInfo;
      }
    }
    public static UserInfo2 ReadUserInfo(string path)
    {
      string text;
      lock (obj)
      {
        using (TextReader reader = new StreamReader(path))
        {
          text = reader.ReadToEnd();
        }
      }
      UserInfo2 user = new UserInfo2();

      user.AntigeitKey = GetValueForName(text, "antigate");
      return user;
    }

    private static string GetValueForName(string text, string nameField)
    {
      string result = InfoPage.GetDatafromText(text, "" + nameField + "=\"(.+?)\"", 1);
      return result;
    }
  }
  #endregion
  public class Antigate
  {
    private string key = "f44a3dc0fd2c48748831fbad8bb1010f";// set personal key

    public Antigate()
    {
      key = UserInfoReader2.Info.AntigeitKey;
    }
    public string Key
    {
      get { return key; }
      set { key = value; }
    }

    private string indexCaptha;
    public void UploadCaptha(Stream streamImage)
    {
      Uploading up = new Uploading();
      byte[] res;
      using (var stream = streamImage)
      {
        var files = new[] 
                {
                    new UploadFile
                    {
                        Name = "file",
                        Filename = "captcha",
                        ContentType = "image/jpeg",
                        Stream = stream
                    }
                };

        var values = new NameValueCollection();
        values.Add("method", "post");
        values.Add("key", key);
        res = up.UploadFiles("http://antigate.com/in.php", files, values, "");
      }

      string resText = ByteArrayToString(res);
      indexCaptha = InfoPage.GetDatafromText(resText, "\\d+");
    }
    public void UploadCaptha(string dumpPath)
    {
      Uploading up = new Uploading();
      byte[] res;
      using (var stream = File.Open(dumpPath, FileMode.Open))
      {
        var files = new[] 
                {
                    new UploadFile
                    {
                        Name = "file",
                        Filename = Path.GetFileName(dumpPath),
                        ContentType = "image/jpeg",
                        Stream = stream
                    }
                };

        var values = new NameValueCollection();
        values.Add("method", "post");
        values.Add("key", key);
        res = up.UploadFiles("http://antigate.com/in.php", files, values, "");
      }

      string resText = ByteArrayToString(res);
      indexCaptha = InfoPage.GetDatafromText(resText, "\\d+");
    }
    public string SendResponses()
    {
      string result = "";
      string res = "";
      bool isNotReady = true;
      while (isNotReady)
      {
        string url = "http://antigate.com/res.php?key=" + key + "&action=get&id=" + indexCaptha;
        res = InfoPage.GetPage(url);
        if (!res.Equals("CAPCHA_NOT_READY"))
        {
          isNotReady = false;
        }
        else
        {
          Thread.Sleep(2000);
        }

      }
      result = InfoPage.GetDatafromText(res, "OK\\|(.+)", 1);
      return result;

    }


    private string ByteArrayToString(byte[] input)
    {
      UTF8Encoding enc = new UTF8Encoding();
      string str = enc.GetString(input);
      return str;
    }
  }
}
