using ParsersChe.Bot.ContentPrepape.Avito.Auto;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParsersChe.Bot.ContentPrepape.Avito.Auto
{
    public class AvitoAutoMaker : AvitoAutoParams
    {
        protected override string GetParams()
        {
            partsPage = PartsPage.Maker;
            return HelpFulAvitoAutoParamsLoad.GetParamFromNodes(Doc,"Марка");
        }
    }
}
