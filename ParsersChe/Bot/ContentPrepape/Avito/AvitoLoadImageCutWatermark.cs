using ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito;
using ParsersChe.WebClientParser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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
          var req = WebCl.GetHttpWebReq(item);
          var resp = WebCl.GetHttpWebResp(req);
          if (resp != null)
          {
            if (ResultDown == null) { ResultDown = new List<string>(); }
            var wcpr = WebCl as WebClProxy;

            var imageStream = wcpr.GetStream(resp);
            if (imageStream != null)
            {
              var guid = GetidImage();
              var image = Image.FromStream(imageStream);
              ReseizeSave(image, image.Size, "", guid);
            }
          }
        }
    }

    public void ReseizeSave(Image image, Size size, string prefix, string guid)
    {
      var litleImage = ResizeImage(image, size);
      var path = PathToFolder + "\\" + guid + "_" + prefix + ".jpg";
      ResultDown.Add(path);
      litleImage.Save(path, ImageFormat.Jpeg);
    }

    protected Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
    {
      int newWidth;
      int newHeight;
      var minW = 35;
      var minH = 30;
      if (preserveAspectRatio)
      {
        var originalWidth = image.Width;
        var originalHeight = image.Height;
        var percentWidth = size.Width / (float)originalWidth;
        var percentHeight = size.Height / (float)originalHeight;
        var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
        newWidth = (int)(originalWidth * percent);
        newHeight = (int)(originalHeight * percent);
      }
      else
      {
        newWidth = size.Width;
        newHeight = size.Height;
      }
      minW = (int)(minW * (newWidth / (float)image.Width));
      minH = (int)(minH * (newHeight / (float)image.Height));


      var newImage = new Bitmap(newWidth, newHeight);
      using (var graphicsHandle = Graphics.FromImage(newImage))
      {
        graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
      }

      var newImage2 = new Bitmap(newWidth - minW, newHeight - minH);
      using (var graphicsHandle = Graphics.FromImage(newImage2))
      {
        graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphicsHandle.DrawImage(newImage, new Rectangle(0, 0, newWidth - minW, newHeight - minH), 0, 0, newWidth - minW, newHeight - minH, GraphicsUnit.Pixel);
      }

      return newImage2;
    }

  }
}
