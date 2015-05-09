using ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito;
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

namespace ParsersChe.Bot.ContentPrepape.Avito
{

  public class AvitoLoadImageCutWatermark : AvitoLoadImages
  {
    public Func<string> GetidImage;
    public AvitoLoadImageCutWatermark(IHttpWeb httpweb, string pathFolder, Func<string> GetidImage)
      : base(httpweb, pathFolder)
    {
      this.GetidImage = GetidImage;
    }

    protected override void LoadImages()
    {
      if (LinksImages != null)
        foreach (var item in LinksImages)
        {
          HttpWebRequest req = WebCl.GetHttpWebReq(item);
          HttpWebResponse resp = WebCl.GetHttpWebResp(req);
          if (resp != null)
          {
            if (ResultDown == null) { ResultDown = new List<string>(); }
            var wcpr = WebCl as WebClProxy;

            Stream imageStream = wcpr.GetStream(resp);
            if (imageStream != null)
            {
              string guid = GetidImage();
              var image = Image.FromStream(imageStream);
              ReseizeSave(image, image.Size, "", guid);
            }
          }
        }


    }

    public void ReseizeSave(Image image, Size size, string prefix, string guid)
    {
      var litleImage = ResizeImage(image, size);
      string path = PathToFolder + "\\" + guid + "_" + prefix + ".jpg";
      ResultDown.Add(path);
      litleImage.Save(path, ImageFormat.Jpeg);
    }

    protected Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
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

      Image newImage2 = new Bitmap(newWidth - minW, newHeight - minH);
      using (Graphics graphicsHandle = Graphics.FromImage(newImage2))
      {
        graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphicsHandle.DrawImage(newImage, new Rectangle(0, 0, newWidth - minW, newHeight - minH), 0, 0, newWidth - minW, newHeight - minH, GraphicsUnit.Pixel);
      }

      return newImage2;
    }

  }
}
