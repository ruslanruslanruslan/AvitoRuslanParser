using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AvitoRuslanParser;

namespace AvitoRuslanParser
{
  class EbayLoadImage
  {
    //public Func<string> GetidImage = (() => MySqlDB2.ResourceID());
    //public Func<string> GetidImageList = (() => MySqlDB2.ResourceListID());

    private IHttpWeb web;
    private string PathToFolder;
    private MySqlDB mySqlDB;
    private string ftpUsername;
    private string ftpPassword;

    public EbayLoadImage(IHttpWeb httpweb, string pathFolder, MySqlDB _mySqlDB, string _ftpUsername, string _ftpPassword)
    {
      PathToFolder = pathFolder;
      web = httpweb;
      mySqlDB = _mySqlDB;
      ftpUsername = _ftpUsername;
      ftpPassword = _ftpPassword;
    }


    public void LoadImages(IEnumerable<string> LinksImages)
    {
      // IList
      if (LinksImages != null)
        foreach (var item in LinksImages)
        {
          HttpWebRequest req = web.GetHttpWebReq(item);
          HttpWebResponse resp = web.GetHttpWebResp(req);
          if (resp != null)
          {
            //  if (ResultDown == null) { ResultDown = new List<string>(); }
            var wcpr = web;

            Stream imageStream = resp.GetResponseStream();
            if (imageStream != null)
            {
              string guid = mySqlDB.ResourceID();
              string guid2 = mySqlDB.ResourceListIDEbay();
              mySqlDB.InsertItemResource(guid, frmMain.URLLink);
              mySqlDB.InsertassGrabberEbayResourceList(guid2, guid);

              var image = Image.FromStream(imageStream);
              ResizeAndSave(image, image.Size, "_original", guid);
              ResizeAndSave(image, new Size(295, 190), "_preview", guid);
              ResizeAndSave(image, new Size(80, 80), "_thumbnail", guid);
              //   ReseizeSave(image, new Size(1, 1), "", guid);
            }
          }
        }
    }


    public void ResizeAndSave(Image image, Size size, string prefix, string guid)
    {
      var litleImage = ResizeImage(image, size);
      string path;
      string filename = guid + prefix + ".jpg";
      if ((PathToFolder.ToLower()).StartsWith("ftp://"))
      {
        path = Path.GetTempPath();
      }
      else
      {
        path = PathToFolder + "\\";
      }
      path += filename;
      //ResultDown.Add(path);
      litleImage.Save(path, ImageFormat.Jpeg);
      if ((PathToFolder.ToLower()).StartsWith("ftp://"))
      {
        FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create(PathToFolder + "\\" + filename);
        ftpClient.Credentials = new System.Net.NetworkCredential(ftpUsername, ftpPassword);
        ftpClient.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
        ftpClient.UseBinary = true;
        ftpClient.KeepAlive = true;
        System.IO.FileInfo fi = new System.IO.FileInfo(path);
        ftpClient.ContentLength = fi.Length;
        byte[] buffer = new byte[4097];
        int bytes = 0;
        int total_bytes = (int)fi.Length;
        System.IO.FileStream fs = fi.OpenRead();
        System.IO.Stream rs = ftpClient.GetRequestStream();
        while (total_bytes > 0)
        {
          bytes = fs.Read(buffer, 0, buffer.Length);
          rs.Write(buffer, 0, bytes);
          total_bytes = total_bytes - bytes;
        }
        //fs.Flush();
        fs.Close();
        rs.Close();
        FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
        var value = uploadResponse.StatusDescription;
        uploadResponse.Close();
        if (File.Exists(path))
        {
          File.Delete(path);
        }
      }
    }

    protected static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
    {
      int newWidth;
      int newHeight;
      int minW = 35;
      int minH = 30;
      if (preserveAspectRatio)
      {
        int originalWidth = image.Width;
        int originalHeight = image.Height;
        float percentWidth = (float)size.Width / (float)originalWidth;
        float percentHeight = (float)size.Height / (float)originalHeight;
        float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
        newWidth = (int)(originalWidth * percent);
        newHeight = (int)(originalHeight * percent);
      }
      else
      {
        newWidth = size.Width;
        newHeight = size.Height;
      }
      minW = (int)(minW * (float)((float)newWidth / (float)image.Width));
      minH = (int)(minH * (float)((float)newHeight / (float)image.Height));


      Image newImage = new Bitmap(newWidth, newHeight);
      using (Graphics graphicsHandle = Graphics.FromImage(newImage))
      {
        graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);

      }

      //Image newImage2 = new Bitmap(newWidth - minW, newHeight - minH);
      Image newImage2 = DrawFilledRectangle(size.Width, size.Height);

      int xMove = (size.Width - (newWidth)) / 2;
      int yMove = (size.Height - (newHeight)) / 2;
      using (Graphics graphicsHandle = Graphics.FromImage(newImage2))
      {
        graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphicsHandle.DrawImage(newImage, new Rectangle(xMove, yMove, newWidth, newHeight), 0, 0, newWidth - minW * 2, newHeight - minH, GraphicsUnit.Pixel);
      }

      return newImage2;
    }
    private static Bitmap DrawFilledRectangle(int x, int y)
    {
      Bitmap bmp = new Bitmap(x, y);
      using (Graphics graph = Graphics.FromImage(bmp))
      {
        Rectangle ImageSize = new Rectangle(0, 0, x, y);
        graph.FillRectangle(Brushes.White, ImageSize);
      }
      return bmp;
    }
  }
}
