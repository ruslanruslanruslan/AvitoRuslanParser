using ParsersChe.Bot.ActionOverPage.EnumsPartPage;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class AvitoAutoDrive : AvitoAutoParams
  {
    protected override string GetParams()
    {
      partsPage = PartsPage.Drive;
      return HelpFulAvitoAutoParamsLoad.GetParamFromParentNodes(Doc, new string[,]{ 
             {"задний привод","задний"},
             {"передний привод","передний"},
             {"полный привод","полный"}
          });
    }
  }
}
