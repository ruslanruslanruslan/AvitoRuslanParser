using System.IO;

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
