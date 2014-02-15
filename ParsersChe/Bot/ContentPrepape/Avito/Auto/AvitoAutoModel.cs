using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
    public class AvitoAutoModel:AvitoAutoParams
    {
        protected override string GetParams()
        {
            partsPage = PartsPage.Model;
            return HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Модель");
        }
    }
}
