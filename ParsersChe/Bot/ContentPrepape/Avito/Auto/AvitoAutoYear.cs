using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
    public class AvitoAutoYear:AvitoAutoParams
    {
        protected override string GetParams()
        {
            partsPage = PartsPage.Year;
            var result = HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc, "Год выпуска");
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Replace(" г.", "");
            }
            return result;
        }
    }
}
