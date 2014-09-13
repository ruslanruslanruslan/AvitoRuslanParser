using AvitoRuslanParser.EbayParser;
using AvitoRuslanParser.Helpfuls;
using MySql.Data.MySqlClient;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvitoRuslanParser
{
    class MySqlDB2
    {
        private const string hostAvito = "www.avito.ru";
        private const string hostEbay = "www.ebay.com";
        private const string namedb = "playandbay";
        //conectionString!!!!!!!!!!!!!!!
        public static readonly string connectionString = "server=localhost;user=root;database=playandbay;port=3306;password=Galka91;";
        //Метод получения ID картинки префикс!!!!!!!!!
        public static string ItemID()
        {
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "SELECT ifnull(max(pk_i_id),0)+1 FROM " + namedb + ".oc_t_item;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    resultStr = r.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
            return resultStr;
        }

        public static IList<SectionItem> LoadSectionLinkEbay()
        {
            var allLinks = LoadSectionsLink();
            var newlist = allLinks.Where(x => x.site == SectionItem.Site.Ebay);
            return newlist.ToList();
        }
        
        /// <summary>
        /// Returns searching categories for both sites Avito and Ebay
        /// </summary>
        /// <returns></returns>
        public static IList<SectionItem> LoadSectionsLink()
        {
            IList<SectionItem> links = new List<SectionItem>();
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!//set order by ordering
                string sql = "SELECT search_url, category_name FROM " + namedb + ".fct_categories_search where search_url is not null order by ordering";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var link = reader.GetString(0);
                    var category_name = reader.GetString(1);
                    SectionItem.Site siteCurrent = SectionItem.Site.UnTyped;
                    if (link != "NULL" && link != null && link != string.Empty)
                    {
                        //if (link == null || link == string.Empty) System.Diagnostics.Debugger.Break();
                        var uri = new Uri(link);

                        if (uri.Host == "www.avito.ru") siteCurrent = SectionItem.Site.Avito;
                        else siteCurrent = SectionItem.Site.Ebay;
                        var section = new SectionItem { Link = link, site = siteCurrent, CategoryName = category_name };
                        links.Add(section);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
            return links;
        }

        public static IList<long> LoadAuctionLink()
        {
            IList<long> links = new List<long>();
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!//set order by ordering
                string sql = "SELECT ebay_id FROM " + namedb + ".fct_grabber_ebay where is_auction=1 and price is null";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt64(0);
                    links.Add(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
            return links;
        }

        public static void UpdateAuction(GetMultipleItemsResponse list)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                foreach (var item in list.Item)
                {
                    bool is_auction = item.TimeLeft == "PT0S";
                    if (!is_auction) break;
                    //Тело запроса!!!!!!!

                    string sql = @" update " + namedb + @".fct_grabber_ebay  set price=@price where ebay_id=@id ";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Prepare();
                    //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
                    // и тд

                    cmd.Parameters.AddWithValue("@price", item.CurrentPrice.Value);
                    cmd.Parameters.AddWithValue("@id", item.ItemID);
                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            
        }
        public static int CountAd { get; set; }
        public static bool IsNewAdAvito(int id)
        {
            CountAd++;
            bool resultValue = true;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "SELECT count(*) FROM " + namedb + ".fct_grabber_avito where avito_id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Id", id);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    if (r > 0)
                    {
                        resultValue = false;
                    }
                    else
                    {
                        resultValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
            return resultValue;
        }
        public static bool IsNewAdEbay(long id)
        {
            CountAd++;
            bool resultValue = true;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "SELECT count(*) FROM " + namedb + ".fct_grabber_ebay where ebay_id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Id", id);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    if (r > 0)
                    {
                        resultValue = false;
                    }
                    else
                    {
                        resultValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
            return resultValue;
        }
        public static string ResourceID()
        {
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "SELECT ifnull(max(pk_i_id),0)+1 FROM "+namedb+".oc_t_item_resource;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    resultStr = r.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
            return resultStr;
        }

        public static string ResourceListID()
        {
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "select max(ifnull(v,0)) from (select max(pk_i_id) v from " + namedb + ".oc_t_item union select max(id_resource_list) from " + namedb + ".fct_grabber_ebay) t";

               // string sql = "select ifnull(v,0)+1 from (select max(pk_i_id) v from "+namedb+".oc_t_item) t";
                //select ifnull(AUTO_INCREMENT,0) FROM information_schema.tables WHERE table_name = 'oc_t_item'"
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    resultStr = r.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return resultStr;
        }

        public static string ResourceListID(MySqlConnection conn)
        {
            string resultStr = string.Empty;
            try
            {
                //тело запроса!!!!!!!!!!
             string sql=   "select max(ifnull(v,0))+1 from (select max(pk_i_id) v from " + namedb + ".oc_t_item union select max(id_resource_list) from " + namedb + ".fct_grabber_ebay) t";
           //     string sql = "select ifnull(v,0)+1 from (select max(pk_i_id) v from " + namedb + ".oc_t_item) t";
                //select ifnull(AUTO_INCREMENT,0) FROM information_schema.tables WHERE table_name = 'oc_t_item'"
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    resultStr = r.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            
            return resultStr;
        }

     //   public static string ExecuteProcAvito() { return ExecuteProc("use " + namedb + "; call ribr_test();"); }

       // public static string ExecuteProcEbay(MySqlConnection conn) { return ExecuteProc(conn,"use " + namedb + "; call ribr_test2();"); }

        public static string ExecuteProc2()
        {
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "use "+namedb+"; call ribr_test2(null);";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    resultStr = r.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("!" + ex.ToString());

            }
            finally
            {
                conn.Close();
            }
            return resultStr;
        }


        private static string ExecuteProc(MySqlConnection conn,string body)
        {
            string resultStr = string.Empty;
            try
            {
             
                //тело запроса!!!!!!!!!!
                string sql = body;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    resultStr = r.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("!" + ex.ToString());

            }
          
            return resultStr;
        }
        //Метод вставки данных в базу!!!!!!!!
        public static void InsertFctEbayGrabber(GetMultipleItemsResponse list, string section)
        {
            if (list != null && list.Item!=null && list.Item.Length > 0)
            {
                MySqlConnection conn = null;
                try
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    //Тело запроса!!!!!!!
                    foreach (var item in list.Item)
                    {
                        InserEbay(item, section, conn);
                   //     ExecuteProcEbay(conn);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static void InsertFctEbayGrabberOneItem(GetMultipleItemsResponseItem item, string section)
        {
            if (item != null)
            {
                MySqlConnection conn = null;
                try
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    InserEbay(item, section, conn);
                    }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static bool InserEbay(GetMultipleItemsResponseItem item, string section,MySqlConnection conn)
        {
            string index = ResourceListID(conn);
            int trans = 0;
            bool is_auction = item.MinimumToBid != null;
            if (is_auction) trans = 4;
            string sql = @" insert into " + namedb + @".fct_grabber_ebay (id_resource_list, ebay_id,url, title, author, price, city,country, ebay_section, user_section, description,curr_code,is_auction,bid,transformated)
                                    Values(@index,@idEbay,@url,@title,@seller,@price,@city,@country,@subcategory,@section,@desc,@currency,@is_auction,@bid,@transformated)";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Prepare();
            //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
            // и тд

            decimal? price=null;
            decimal? bid=null;
            if (is_auction)
            {
                bid = item.CurrentPrice.Value;
            }
            else { price = item.CurrentPrice.Value; }

            cmd.Parameters.AddWithValue("@index", index);
            cmd.Parameters.AddWithValue("idEbay", item.ItemID);
            cmd.Parameters.AddWithValue("@url", item.ViewItemURLForNaturalSearch);
            cmd.Parameters.AddWithValue("@title", item.Title);
            cmd.Parameters.AddWithValue("@seller", item.Seller.UserID);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@city", item.Location);
            cmd.Parameters.AddWithValue("@country", item.Country);

            cmd.Parameters.AddWithValue("@subcategory", item.PrimaryCategoryName);
            cmd.Parameters.AddWithValue("@section", section);
            cmd.Parameters.AddWithValue("@desc", item.Description);
            cmd.Parameters.AddWithValue("@currency", item.CurrentPrice.currencyID);
            cmd.Parameters.AddWithValue("@is_auction", is_auction);
            cmd.Parameters.AddWithValue("@transformated", trans);
            cmd.Parameters.AddWithValue("@bid", bid);

            int result = cmd.ExecuteNonQuery();
            return is_auction;
        }
        public static void InserEbayGrabber() { }
        public static void InsertItemResource(string resourceid, string url)
        {
            if (resourceid != null)
            {
                MySqlConnection conn = null;
                try
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    //Тело запроса!!!!!!!
                    string sql = @" insert into " + namedb + @".oc_t_item_resource
                                    select @resourceid, 1, null, ""jpg"", ""image/jpeg"", ""oc-content/uploads/""";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@resourceid", resourceid);

                    int result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static void InsertassGrabberAvitoResourceList(string index1, string index2)
        {
            if (index1 != null)
            {
                MySqlConnection conn = null;
                try
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    //Тело запроса!!!!!!!
                    string sql = @" insert into " + namedb + @".ass_grabber_avito_resource_list
                    Values(@index1,@index2)";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Prepare();
                    //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
                    // и тд
                    cmd.Parameters.AddWithValue("@index1", index1);
                    cmd.Parameters.AddWithValue("@index2", index2);
                    int result = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public static void InsertassGrabberEbayResourceList(string index1, string index2)
        {
            if (index1 != null)
            {
                MySqlConnection conn = null;
                try
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    //Тело запроса!!!!!!!
                    string sql = @" insert into " + namedb + @".ass_grabber_ebay_resource_list
                    Values(@index1,@index2)";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Prepare();
                    //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
                    // и тд
                    cmd.Parameters.AddWithValue("@index1", index1);
                    cmd.Parameters.AddWithValue("@index2", index2);
                    int result = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public static void DeleteUnTransformated()
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //Тело запроса!!!!!!!
                string sql = @" delete from " + namedb + @".ass_grabber_avito_resource_list 
                                where id_resource_list in (select id_resource_list from fct_grabber_avito 
						                                   where transformated = 0);
                                    delete from fct_grabber_avito 
                                    where transformated = 0;";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
