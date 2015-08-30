using ParsersChe.Bot.ActionOverPage.EnumsPartPage;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class AvitoAutoWheel : AvitoAutoParams
  {
    protected override string GetParams()
    {
      partsPage = PartsPage.Wheel;
      return HelpFulAvitoAutoParamsLoad.GetParamFromParentNodes(Doc, new string[,] {
           {"левый руль","левый"},
            {"правый руль","правый"}
            });
    }
  }
}
