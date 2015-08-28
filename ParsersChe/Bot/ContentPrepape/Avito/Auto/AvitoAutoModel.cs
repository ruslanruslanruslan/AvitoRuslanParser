using ParsersChe.Bot.ActionOverPage.EnumsPartPage;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class AvitoAutoModel : AvitoAutoParams
  {
    protected override string GetParams()
    {
      partsPage = PartsPage.Model;
      return HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Модель");
    }
  }
}
