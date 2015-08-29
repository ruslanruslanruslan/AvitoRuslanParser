using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Collections.Generic;

namespace ParsersChe.ResultPrepare
{
  public abstract class ResultPreparer
  {
    public IEnumerable<Dictionary<PartsPage, IEnumerable<string>>> Data { protected get; set; }
    public string PreparedData { get; set; }
    public ResultPreparer()
    {

    }
    public ResultPreparer(IEnumerable<Dictionary<PartsPage, IEnumerable<string>>> data)
    {
      Data = data;
    }

    public abstract void PrepareData();

  }
}
