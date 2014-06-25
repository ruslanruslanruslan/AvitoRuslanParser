using ParsersChe.Bot.ActionOverPage.ContentPrepape.Avito;
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
using MySql.Data.MySqlClient;
using AvitoRuslanParser;
using WindowsFormsApplication1;


namespace ParsersChe.Bot.ContentPrepape.Avito
{

 public   class AvitoLoadImageDeferentSize : AvitoLoadImages
    {
     public Func<string> GetidImage;
     public Func<string> GetidImageList;
     
     public AvitoLoadImageDeferentSize(IHttpWeb httpweb, string pathFolder) : base(httpweb, pathFolder)
        {
        
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
                         var wcpr=WebCl;

                       Stream imageStream=  resp.GetResponseStream();
                       if (imageStream!=null)
                        {
                            string guid = GetidImage();
                            string guid2 = GetidImageList();
                            MySqlDB.InsertItemResource(guid, Form1.URLLink);
                            MySqlDB.InsertassGrabberAvitoResourceList(guid2, guid);
                                
                            var image = Image.FromStream(imageStream);
                            ReseizeSave(image, image.Size, "_original", guid);
                            ReseizeSave(image, new Size(295, 190), "_preview", guid);
                            ReseizeSave(image, new Size(80, 80), "_thumbnail", guid);
                            ReseizeSave(image, new Size(1, 1), "", guid);
                        }
                    }
                }

        }

        public void ReseizeSave(Image image, Size size,string prefix,string guid) 
        {
            var litleImage = ResizeImage(image, size);
            string path = PathToFolder + "\\" + guid+prefix +".jpg";
            ResultDown.Add(path);
            litleImage.Save(path,ImageFormat.Jpeg);
        }

        protected  Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            int minW=35;
            int minH=30;
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
            Image newImage2 = DrawFilledRectangle(size.Width,size.Height);

            int xMove = (size.Width - (newWidth ))/2;
            int yMove = (size.Height - (newHeight ))/2;
            using (Graphics graphicsHandle = Graphics.FromImage(newImage2))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(newImage, new Rectangle(xMove, yMove, newWidth , newHeight ), 0, 0, newWidth - minW*2, newHeight - minH, GraphicsUnit.Pixel);
            }

            return newImage2;
        }
        private Bitmap DrawFilledRectangle(int x, int y)
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
