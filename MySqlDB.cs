using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Windows.Forms;

namespace AvitoRuslanParser
{
  public  class MySqlDB
    {
        //conectionString!!!!!!!!!!!!!!!
      public static readonly string connectionString = "server=localhost;user=root;database=playandbay_test;port=3306;password=Galka91;";
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
                string sql = "SELECT ifnull(max(pk_i_id),0)+1 FROM playandbay.oc_t_item;";
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
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return resultStr;
        }

        public static IList<string[]> LoadSectionsLink() 
        {
            IList<string[]> links = new List<string[]>();
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "SELECT search_url, category_name FROM playandbay_test.fct_categories_search where search_url is not null";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var link=reader.GetString(0);
                    var category_name = reader.GetString(1);
                    links.Add(new string[] { link, category_name });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return links;
        }

        public static int CountAd { get; set; }
        public static bool IsNewAd(int id) 
        {
            CountAd++;
            bool resultValue = true;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "SELECT count(*) FROM playandbay_test.fct_grabber_avito where avito_id=@Id";
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
                MessageBox.Show(ex.Message);

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
                string sql = "SELECT ifnull(max(pk_i_id),0)+1 FROM playandbay_test.oc_t_item_resource;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    resultStr=r.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

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
                string sql = "select ifnull(v,0)+1 from (select max(pk_i_id) v from playandbay_test.oc_t_item) t";
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
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return resultStr;
        }
        public static string ExecuteProc()
        {
            string resultStr = string.Empty;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                //тело запроса!!!!!!!!!!
                string sql = "use playandbay_test; call ribr_test();";
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
                MessageBox.Show("!"+ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return resultStr;
        }
        //Метод вставки данных в базу!!!!!!!!
        public static void InsertFctAvitoGrabber(Dictionary<PartsPage,IEnumerable<string>> list,string index,string url,string section) 
        {
            if (list != null && list.Count>0) 
            {
                MySqlConnection conn = null;
                try
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    //Тело запроса!!!!!!!
                    string sql = @" insert into playandbay_test.fct_grabber_avito (id_resource_list, avito_id,url, title, tel_num, author, price, city, avito_section, user_section, description)
                                    Values(@index,@idAvito,@url,@title,@phone,@seller,@price,@city,@subcategory,@section,@desc)";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Prepare();
                    //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
                    // и тд
                    string cost = "null";
                    if (list[PartsPage.Cost] != null) 
                    {
                        cost = list[PartsPage.Cost].First<string>();
                    }
                    string phone = "null";
                    if (list[PartsPage.Phone] != null)
                    {
                        phone = list[PartsPage.Phone].First<string>();
                    }
                    cmd.Parameters.AddWithValue("@index", index);
                    cmd.Parameters.AddWithValue("idAvito", list[PartsPage.Id].First<string>());
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.Parameters.AddWithValue("@title", list[PartsPage.Title].First<string>());
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@seller", list[PartsPage.Seller].First<string>());
                    cmd.Parameters.AddWithValue("@price", cost);
                    cmd.Parameters.AddWithValue("@city", list[PartsPage.City].First<string>());
                    cmd.Parameters.AddWithValue("@subcategory", list[PartsPage.SubCategory].First<string>());
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@desc", list[PartsPage.Body].First<string>());
                    int result = cmd.ExecuteNonQuery();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
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
                    string sql = @" insert into playandbay_test.oc_t_item_resource
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
                    MessageBox.Show(ex.Message);
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
                    string sql = @" insert into playandbay_test.ass_grabber_avito_resource_list
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
                    MessageBox.Show(ex.Message);
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
                string sql = @" delete from playandbay_test.ass_grabber_avito_resource_list 
                                where id_resource_list in (select id_resource_list from playandbay.fct_grabber_avito 
						                                   where transformated = 0);
                                    delete from playandbay.fct_grabber_avito 
                                    where transformated = 0;";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }          
        }
    }
}
