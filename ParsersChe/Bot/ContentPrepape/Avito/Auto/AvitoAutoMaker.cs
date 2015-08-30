using ParsersChe.Bot.ActionOverPage.EnumsPartPage;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class AvitoAutoMaker : AvitoAutoParams
  {
    protected override string GetParams()
    {
      partsPage = PartsPage.Maker;
      return HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Марка");
    }
  }
}
