using ParsersChe.Bot.ActionOverPage.EnumsPartPage;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class AvitoAutoMileage : AvitoAutoParams
  {
    protected override string GetParams()
    {
      partsPage = PartsPage.Mileage;
      return HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Пробег");
    }
  }
}
