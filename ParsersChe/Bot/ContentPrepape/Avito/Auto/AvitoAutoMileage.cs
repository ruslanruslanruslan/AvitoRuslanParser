using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
    public class AvitoAutoMileage:AvitoAutoParams
    {
        protected override string GetParams()
        {
            partsPage = PartsPage.Mileage;
            return HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Пробег");
        }
    }
}
