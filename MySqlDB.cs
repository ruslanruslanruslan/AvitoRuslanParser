using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System.Windows.Forms;
using AvitoRuslanParser.Helpfuls;
using AvitoRuslanParser.EbayParser;

namespace AvitoRuslanParser
{
  public class MySqlDB
  {
    private const string hostAvito = "www.avito.ru";
    private const string hostEbay = "www.ebay.com";
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

    public void Close()
    {
      if (mySqlConnection != null)
      {
        mySqlConnection.Close();
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

    public int CountAd { get; set; }

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
    public string ResourceListIDAvito()
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
    public string ExecuteProcAvito(string id)
    {
      //тело запроса!!!!!!!!!!
      //const string sql = "call ribr_test();";
      const string sql = "call sp_map_grabber_avito(@id)";
      string resultStr = string.Empty;
      try
      {
        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Prepare();
        cmd.Parameters.AddWithValue("@id", id);
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
          cmd.Parameters.AddWithValue("@idAvito", list[PartsPage.Id].First<string>());
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
    // From MySqlDB2
    public IList<SectionItem> LoadSectionLinkEbay()
    {
      var allLinks = LoadSectionsLinkEx();
      var newlist = allLinks.Where(x => x.site == SectionItem.Site.Ebay);
      return newlist.ToList();
    }
    /// <summary>
    /// Returns searching categories for both sites Avito and Ebay
    /// </summary>
    /// <returns></returns>
    public IList<SectionItem> LoadSectionsLinkEx()
    {
      //тело запроса!!!!!!!!!!//set order by ordering
      const string sql = "SELECT search_url, category_name FROM fct_categories_search where search_url is not null order by ordering";
      IList<SectionItem> links = new List<SectionItem>();
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
          SectionItem.Site siteCurrent = SectionItem.Site.UnTyped;
          if (link != "NULL" && link != null && link != string.Empty)
          {
            //if (link == null || link == string.Empty) System.Diagnostics.Debugger.Break();
            var uri = new Uri(link);

            if (uri.Host == hostAvito) siteCurrent = SectionItem.Site.Avito;
            else if (uri.Host == hostEbay) siteCurrent = SectionItem.Site.Ebay;
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
        if (reader != null)
        {
          reader.Close();
        }
      }
      return links;
    }
    public IList<long> LoadAuctionLink()
    {
      //тело запроса!!!!!!!!!!//set order by ordering
      const string sql = "SELECT ebay_id FROM fct_grabber_ebay where is_auction=1 and price is null";
      IList<long> links = new List<long>();
      string resultStr = string.Empty;
      MySqlDataReader reader = null;
      try
      {
        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
        reader = cmd.ExecuteReader();
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
        if (reader != null)
        {
          reader.Close();
        }
      }
      return links;
    }
    public void UpdateAuction(GetMultipleItemsResponse list)
    {
      //Тело запроса!!!!!!!
      const string sql = @"update fct_grabber_ebay set price=@price where ebay_id=@id";
      try
      {
        foreach (var item in list.Item)
        {
          bool is_auction = item.TimeLeft == "PT0S";
          if (!is_auction) break;

          MySqlCommand cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
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
    }
    public bool IsNewAdAvito(int id)
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
        MessageBox.Show(ex.ToString());

      }
      return resultValue;
    }
    public bool IsNewAdEbay(long id)
    {
      //тело запроса!!!!!!!!!!
      const string sql = "SELECT count(*) FROM fct_grabber_ebay where ebay_id=@Id";
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
        MessageBox.Show(ex.ToString());

      }
      return resultValue;
    }
    public string ResourceListIDEbay()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "select max(ifnull(v,0)) from (select max(pk_i_id) v from oc_t_item union select max(id_resource_list) from fct_grabber_ebay) t";
      string resultStr = string.Empty;
      try
      {
        // string sql = "select ifnull(v,0)+1 from (select max(pk_i_id) v from "+namedb+".oc_t_item) t";
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
        MessageBox.Show(ex.ToString());
      }
      return resultStr;
    }
    public string ExecuteProcEBay(string id)
    {
      //тело запроса!!!!!!!!!!
      //const string sql = "call ribr_test2(null);";
      const string sql = "call sp_map_grabber_ebay(@id)";
      string resultStr = string.Empty;
      try
      {
        MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Prepare();
        cmd.Parameters.AddWithValue("@id", id);
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
    public void InsertFctEbayGrabber(GetMultipleItemsResponse list, string section)
    {
      if (list != null && list.Item != null && list.Item.Length > 0)
      {
        try
        {
          foreach (var item in list.Item)
          {
            InsertEbay(item, section);
            //     ExecuteProcEbay(conn);
          }

        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.ToString());
        }
      }
    }
    public void InsertFctEbayGrabberOneItem(GetMultipleItemsResponseItem item, string section)
    {
      if (item != null)
      {
        try
        {
          InsertEbay(item, section);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.ToString());
        }
      }
    }
    public bool InsertEbay(GetMultipleItemsResponseItem item, string section)
    {
      const string sql = @" insert into fct_grabber_ebay (id_resource_list, ebay_id,url, title, author, price, city,country, ebay_section, user_section, description,curr_code,is_auction,bid,transformated)
                                    Values(@index,@idEbay,@url,@title,@seller,@price,@city,@country,@subcategory,@section,@desc,@currency,@is_auction,@bid,@transformated)";
      string index = ResourceListIDEbay();
      int trans = 0;
      bool is_auction = item.MinimumToBid != null;
      if (is_auction) trans = 4;
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = mySqlConnection;
      cmd.CommandText = sql;
      cmd.Prepare();
      //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
      // и тд

      decimal? price = null;
      decimal? bid = null;
      if (is_auction)
      {
        bid = item.CurrentPrice.Value;
      }
      else { price = item.CurrentPrice.Value; }

      cmd.Parameters.AddWithValue("@index", index);
      cmd.Parameters.AddWithValue("@idEbay", item.ItemID);
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
    public void InsertassGrabberEbayResourceList(string index1, string index2)
    {
      if (index1 != null)
      {
        try
        {
          //Тело запроса!!!!!!!
          string sql = @" insert into ass_grabber_ebay_resource_list
                    Values(@index1,@index2)";
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
          MessageBox.Show(ex.ToString());
        }
      }
    }
  }
}
