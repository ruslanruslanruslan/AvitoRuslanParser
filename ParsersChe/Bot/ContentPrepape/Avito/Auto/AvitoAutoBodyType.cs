using ParsersChe.Bot.ActionOverPage.EnumsPartPage;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class AvitoAutoBodyType : AvitoAutoParams
  {
    protected override string GetParams()
    {
      partsPage = PartsPage.AutoBodyType;
      return HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Тип кузова");
    }
  }
}
