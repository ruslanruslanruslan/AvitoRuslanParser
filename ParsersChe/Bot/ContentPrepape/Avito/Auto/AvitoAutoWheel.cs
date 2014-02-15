using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
    public class AvitoAutoWheel:AvitoAutoParams
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
