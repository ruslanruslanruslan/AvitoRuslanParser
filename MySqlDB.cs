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
  public class MySqlDB
  {
    private string m_Server;
    private int m_Port;
    private string m_Database;
    private string m_Login;
    private string m_Password;
    private MySqlConnection m_mySqlConnection;

    public MySqlDB(string _Login, string _Password, string _Server, int _Port, string _Database)
    {
      Login = _Login;
      Password = _Password;
      Server = _Server;
      Port = _Port;
      Database = _Database;
    }

    public string Server
    {
      get { return m_Server; }
      set { m_Server = value; }
    }
    public int Port
    {
      get { return m_Port; }
      set { m_Port = value; }
    }
    public string Database
    {
      get { return m_Database; }
      set { m_Database = value; }
    }
    public string Login
    {
      get { return m_Login; }
      set { m_Login = value; }
    }
    public string Password
    {
      get { return m_Password; }
      set { m_Password = value; }
    }
    public string ConnectionString
    {
      get
      {
        return "server=" + Server + ";port=" + Convert.ToString(Port) +
          ";database=" + Database + ";user=" + Login + ";password=" + Password + ";";
      }
    }

    public MySqlConnection mySqlConnection
    {
      get
      {
        if (m_mySqlConnection == null)
        {
          m_mySqlConnection = new MySqlConnection(ConnectionString);
        }
        if (m_mySqlConnection.State == System.Data.ConnectionState.Broken || m_mySqlConnection.State == System.Data.ConnectionState.Closed)
        {
          try
          {
            m_mySqlConnection.Open();
          }
          catch (Exception ex)
          {
            throw new Exception("Невозможно соединиться с сервером базы данных", ex);
          }
        }
        return m_mySqlConnection;
      }
    }

    //conectionString!!!!!!!!!!!!!!!
    //private static readonly string connectionString = "server=playandbay.com;user=remoteviewer;database=playandbay_test;port=3306;password=Pauza123;";
    //Метод получения ID картинки префикс!!!!!!!!!
    public string ItemID()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "SELECT ifnull(max(pk_i_id),0)+1 FROM oc_t_item;";
      string resultStr = string.Empty;
      try
      {
        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
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
      return resultStr;
    }

    public IList<string[]> LoadSectionsLink()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "SELECT search_url, category_name FROM fct_categories_search where search_url is not null";
      IList<string[]> links = new List<string[]>();
      string resultStr = string.Empty;
      MySqlDataReader reader = null;
      try
      {
        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          var link = reader.GetString(0);
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
        if (reader != null)
        {
          reader.Close();
        }
      }
      return links;
    }
    public IList<string> GetCategories()
    {
      MySqlCommand command = new MySqlCommand(); ;
      const string commandString = @"select concat(d1.s_name, "" | "", coalesce(d2.s_name,""-""), "" | "", coalesce(d3.s_name,""-""), "" | "", coalesce(d4.s_name,""-"")) s_name,
                                         coalesce(c4.pk_i_id, c3.pk_i_id, c2.pk_i_id, c1.pk_i_id) id
                                  from oc_t_category c1
                                  left join oc_t_category c2 on c1.pk_i_id=c2.fk_i_parent_id
                                  left join oc_t_category c3 on c2.pk_i_id=c3.fk_i_parent_id
                                  left join oc_t_category c4 on c3.pk_i_id=c4.fk_i_parent_id
                                  left join oc_t_category_description d1 on c1.pk_i_id=d1.fk_i_category_id
                                  left join oc_t_category_description d2 on c2.pk_i_id=d2.fk_i_category_id
                                  left join oc_t_category_description d3 on c3.pk_i_id=d3.fk_i_category_id
                                  left join oc_t_category_description d4 on c4.pk_i_id=d4.fk_i_category_id
                                  where d1.fk_c_locale_code = ""ru_RU"" 
                                        and (d2.fk_c_locale_code = ""ru_RU"" or d2.fk_c_locale_code is null)
                                        and (d3.fk_c_locale_code = ""ru_RU"" or d3.fk_c_locale_code is null)
                                        and (d4.fk_c_locale_code = ""ru_RU"" or d4.fk_c_locale_code is null)
                                        and c1.fk_i_parent_id is null
                                  order by d1.s_name, d2.s_name, d3.s_name, d4.s_name;";
      command.CommandText = commandString;
      command.Connection = mySqlConnection;
      MySqlDataReader reader = null;
      IList<string> results = new List<string>();
      try
      {
        reader = command.ExecuteReader();
        while (reader.Read())
        {
          results.Add(Convert.ToString(reader["s_name"]));
        }
        reader.Close();
      }
      catch (MySqlException ex)
      {
        Console.WriteLine("Error: \r\n{0}", ex.ToString());
      }
      finally
      {
        if (reader != null)
        {
          reader.Close();
        }
      }
      return results;
    }

    public int CountAd { get; set; }
    public bool IsNewAd(int id)
    {
      //тело запроса!!!!!!!!!!
      const string sql = "SELECT count(*) FROM fct_grabber_avito where avito_id=@Id";
      CountAd++;
      bool resultValue = true;
      try
      {

        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Connection = mySqlConnection;
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
      return resultValue;
    }
    public string ResourceID()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "SELECT ifnull(max(pk_i_id),0)+1 FROM oc_t_item_resource;";
      string resultStr = string.Empty;
      try
      {
        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
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
      return resultStr;
    }
    public string ResourceListID()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "select max(ifnull(v,0))+1 from (select max(pk_i_id) v from oc_t_item union select max(id_resource_list) from fct_grabber_avito where transformated <> 0) t";
      string resultStr = string.Empty;
      try
      {

        //select ifnull(AUTO_INCREMENT,0) FROM information_schema.tables WHERE table_name = 'oc_t_item'"
        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
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
      return resultStr;
    }
    public string ExecuteProc()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call ribr_test();";
      string resultStr = string.Empty;
      try
      {

        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
        object result = cmd.ExecuteScalar();
        if (result != null)
        {
          int r = Convert.ToInt32(result);
          resultStr = r.ToString();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("!" + ex.Message);

      }
      return resultStr;
    }
    //Метод вставки данных в базу!!!!!!!!
    public void InsertFctAvitoGrabber(Dictionary<PartsPage, IEnumerable<string>> list, string index, string url, string section)
    {
      //Тело запроса!!!!!!!
      const string sql = @" insert into fct_grabber_avito (id_resource_list, avito_id,url, title, tel_num, author, price, city, avito_section, user_section, description)
                                    Values(@index,@idAvito,@url,@title,@phone,@seller,@price,@city,@subcategory,@section,@desc)";
      if (list != null && list.Count > 0)
      {
        try
        {
          MySqlCommand cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
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
      }
    }
    public void InsertItemResource(string resourceid, string url)
    {
      if (resourceid != null)
      {
        //Тело запроса!!!!!!!
        const string sql = @" insert into oc_t_item_resource
                                    select @resourceid, 1, null, ""jpg"", ""image/jpeg"", ""oc-content/uploads/""";
        try
        {
          MySqlCommand cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
          cmd.CommandText = sql;
          cmd.Prepare();
          cmd.Parameters.AddWithValue("@resourceid", resourceid);

          int result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
      }
    }
    public void InsertassGrabberAvitoResourceList(string index1, string index2)
    {
      //Тело запроса!!!!!!!
      const string sql = @" insert into ass_grabber_avito_resource_list
                    Values(@index1,@index2)";
      if (index1 != null)
      {
        try
        {
          MySqlCommand cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
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
      }

    }
    public void DeleteUnTransformated()
    {
      //Тело запроса!!!!!!!
      const string sql = @" delete from ass_grabber_avito_resource_list 
                                where id_resource_list in (select id_resource_list from fct_grabber_avito 
						                                   where transformated = 0);
                                    delete from fct_grabber_avito 
                                    where transformated = 0;";
      try
      {
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = mySqlConnection;
        cmd.CommandText = sql;
        cmd.Prepare();
        int result = cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
  }
}
