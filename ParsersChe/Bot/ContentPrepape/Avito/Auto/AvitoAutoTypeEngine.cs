using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
    public class AvitoAutoTypeEngine:AvitoAutoParams
    {
        protected override string GetParams()
        {
            partsPage = PartsPage.TypeEngine;
            return HelpFulAvitoAutoParamsLoad.GetParamFromParentNodes(Doc, new string[,]{
               {"бензин","бензин"},
               {"дизель","дизель"}
           });
        }
    }
}
