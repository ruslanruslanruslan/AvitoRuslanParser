using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
