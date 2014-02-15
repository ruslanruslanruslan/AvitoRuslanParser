using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ParsersChe.ResultPrepare
{
    public class XmlLeonidData : ResultPreparer
    {
        private XDocument xml;
        private string separate = string.Empty;
        public XmlLeonidData(IEnumerable<Dictionary<PartsPage, IEnumerable<string>>> data)
            : base(data)
        {

        }
        public XmlLeonidData()
            : base()
        {

        }
        public override void PrepareData()
        {
            xml = new XDocument(new XElement("main"));
            foreach (var item in Data)
            {
                separate = string.Empty;
                string maker = GetSingleDateByKey(PartsPage.Maker,item);
                string model = GetSingleDateByKey(PartsPage.Model, item);
                string city = GetSingleDateByKey(PartsPage.City, item);
                string key_words = string.Format("Продам {0} {1} в городе {2}", maker, model, city);
                XElement el = new XElement("advert");
                ATX("name_adv", PartsPage.Title,el,item);
                ATX("email", "avto-tim@list.ru",el);
                ATX("site", "www.avito.ru",el);
                ATX("time", "Два месяца",el);
                ATX("key_words", key_words, el);
                ATX("text", PartsPage.Body, el, item);
                ATX("cost", PartsPage.Cost, el, item);
                ATX("currency", PartsPage.Currency, el, item);
                ATX("phone", PartsPage.Phone, el, item);
                ATX("name_user", PartsPage.Seller, el, item);
                ATX("type", "Предложение, продаю",el);
                #region createParams
                XElement elParam = new XElement("addparam");
                ATAP("Марка автомобиля", maker, elParam);
                ATAP("Модель", model, elParam);
                ATAP("Год выпуска", PartsPage.Year, elParam,item);
                ATAPMileage("Пробег (км)", PartsPage.Mileage, elParam, item);
                ATAP("Руль", PartsPage.Wheel, elParam,item);
                ATAP("Тип сделки", "Куплю", elParam);
                ATAP("Привод", PartsPage.Drive, elParam,item);
                ATAP("Коробка передач", PartsPage.Transmission, elParam,item);
                ATAP("Тип двигателя", PartsPage.TypeEngine, elParam,item);
                ATAP("Тип кузова", PartsPage.AutoBodyType, elParam,item);
                el.Add(elParam);
                #endregion
                ATX("category", PartsPage.Category,el,item);
                ATX("podcategory", PartsPage.SubCategory,el,item);
                ATX("country", "Россия",el);
                ATX("region", PartsPage.Region, el, item);
                ATX("city", city,el);
                ATXImg("image", PartsPage.Image, el, item);
                xml.Root.Add(el);
            }
            PreparedData = xml.ToString();
   
        }
        private void ATXImg(string name, PartsPage part, XElement el, Dictionary<PartsPage, IEnumerable<string>> dict)
        {
            StringBuilder sb = new StringBuilder();
            var node = dict[part];
            if (node != null)
            {
                string separate = string.Empty;
                foreach (string item in node)
                {
                    sb.Append(separate);
                    string nameFile = Path.GetFileName(item);
                    sb.Append(nameFile);
                    if (separate.Equals(string.Empty))
                        separate = ";";
                }
            }
            el.Add(new XElement(name, sb.ToString()));
        }
        private void ATX(string name, string value,XElement el)
        {
            el.Add(new XElement(name, value));
        }
        private void ATX(string name, PartsPage part,XElement el,Dictionary<PartsPage, IEnumerable<string>> dict)
        {
            StringBuilder sb = new StringBuilder();
            var node = dict[part];
            if (node != null)
            {
                string separate = string.Empty;
                foreach (string item in node)
                {
                    sb.Append(separate);
                    sb.Append(item);
                    if (separate.Equals(string.Empty))
                        separate = ";";
                }
            }
            el.Add(new XElement(name, sb.ToString()));
        }

        private void ATAP(string name, PartsPage part, XElement paramNode, Dictionary<PartsPage, IEnumerable<string>> dict)
        {
            string value = null;
            var node = dict[part];
            if (node != null && node.Count<string>() > 0)
            {
                value = node.First<string>();
            }
            if (value == null) value = "Не указан";
            paramNode.Value += this.separate + name + " = " + value;
            separate = ";";
        }
        private void ATAP(string name, string value, XElement paramNode) 
        {
            paramNode.Value += this.separate + name + " = " + value;
            separate = ";";
        }

        private void ATAPMileage(string name, PartsPage part, XElement paramNode, Dictionary<PartsPage, IEnumerable<string>> dict) 
        {
            string value = null;
            var node = dict[part];
           
            if (node != null && node.Count<string>() > 0)
            {
                value = node.First<string>();
            }
            if (value != null)
            {
                value = value.Replace(" ", "");
                value = InfoPage.GetDatafromText(value, "\\d+");
            }
            else 
            {
                value = "Не указан";
            }
            paramNode.Value += this.separate + name + " = " + value;

            separate = ";";
        }
        private string GetSingleDateByKey(PartsPage part, Dictionary<PartsPage, IEnumerable<string>> dict)
        {
            string value = string.Empty;
            var node = dict[part];
            if (node != null && node.Count<string>() > 0)
            {
                value = node.First<string>();
            }
            return value;
        }
    }
}
