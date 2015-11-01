using System.Collections.Generic;

namespace ParsersChe.HelpFull
{
  public class ImageParsedCountHelper
  {
    private int countParsed = 0;
    public int CountParsed
    {
      get { return countParsed; }
      set { countParsed = value; }
    }

    private int countDownloaded = 0;
    public int CountDownloaded
    {
      get { return countDownloaded; }
      set { countDownloaded = value; }
    }

    private Dictionary<string, bool> errorList = new Dictionary<string, bool>();
    public Dictionary<string, bool> ErrorList
    {
      get { return errorList; }
      set { errorList = value; }
    }

    private List<string> resources = new List<string>();
    public List<string> Resources
    {
      get { return resources; }
      set { resources = value; }
    }

    private string resourceId = string.Empty;
    public string ResourceId
    {
      get { return resourceId; }
      set { resourceId = value; }
    }
  }
}
