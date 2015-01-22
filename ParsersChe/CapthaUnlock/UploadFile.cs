using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AntigateUnravel
{
  public class UploadFile
  {
    public UploadFile()
    {
      ContentType = "image/png";
    }
    public string Name { get; set; }
    public string Filename { get; set; }
    public string ContentType { get; set; }
    public Stream Stream { get; set; }
  }
}
