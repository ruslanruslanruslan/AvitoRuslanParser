using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
  public class AvitoAutoTransmission : AvitoAutoParams
  {
    protected override string GetParams()
    {
      partsPage = PartsPage.Transmission;
      string res = HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Коробка передач");
      string result = null;
      if (res != null)
        switch (res)
        {
          case "МТ": result = "механическая"; break;
          case "АТ": result = "автоматическая"; break;
        }
      return result;
    }
  }
}
