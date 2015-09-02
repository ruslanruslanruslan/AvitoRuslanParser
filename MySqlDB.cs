using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using AvitoRuslanParser.Helpfuls;
using AvitoRuslanParser.EbayParser;

namespace AvitoRuslanParser
{
  public class MySqlDB : IDisposable
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

    ~MySqlDB()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      Close();
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
      if (m_mySqlConnection != null)
      {
        m_mySqlConnection.Close();
      }
    }

    private MySqlConnection Connect()
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
          throw new Exception("MySql error: Невозможно соединиться с сервером базы данных: " + ex.Message, ex);
        }
      }
      return m_mySqlConnection;
    }

    public MySqlConnection mySqlConnection
    {
      get
      {
        return Connect();
      }
    }

    public int CountAd { get; set; }

    public IList<string[]> LoadSectionsLink()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call sp_get_SearchUrlsList();";
      IList<string[]> links = new List<string[]>();
      var resultStr = string.Empty;
      MySqlDataReader reader = null;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
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
        throw new Exception ("MySql error: [" + sql + "]: " + ex.Message, ex);
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
      var command = new MySqlCommand(); ;
      const string commandString = "call sp_get_CategoriesList();";
      command.CommandText = commandString;
      command.Connection = mySqlConnection;
      MySqlDataReader reader = null;
      IList<string> results = new List<string>();
      try
      {
        reader = command.ExecuteReader();
        while (reader.Read())
          results.Add(Convert.ToString(reader["s_name"]));
      }
      catch (MySqlException ex)
      {
        throw new Exception("MySql error: [" + commandString + "]: " + ex.Message, ex);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
      return results;
    }

    public string ResourceID()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call sp_get_NextResourceId();";
      var resultStr = string.Empty;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        var result = cmd.ExecuteScalar();
        if (result != null)
        {
          var r = Convert.ToInt32(result);
          resultStr = r.ToString();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "]: " + ex.Message, ex);
      }
      return resultStr;
    }
    public string ResourceListIDAvito()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call sp_get_avito_NextItemId();";
      var resultStr = string.Empty;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        var result = cmd.ExecuteScalar();
        if (result != null)
        {
          var r = Convert.ToInt32(result);
          resultStr = r.ToString();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "]: " + ex.Message, ex);
      }
      return resultStr;
    }
    public string ExecuteProcAvito(string id)
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call sp_map_grabber_avito(@id)";
      var resultStr = string.Empty;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Prepare();
        cmd.Parameters.AddWithValue("@id", id);
        var result = cmd.ExecuteScalar();
        if (result != null)
          resultStr = result.ToString();
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [id = " + id + "]: " + ex.Message, ex);
      }
      return resultStr;
    }
    //Метод вставки данных в базу!!!!!!!!
    public void InsertFctAvitoGrabber(Dictionary<PartsPage, IEnumerable<string>> list, string index, string url, string section)
    {
      //Тело запроса!!!!!!!
      const string sql = @"insert into fct_grabber_avito (id_resource_list, avito_id,url, title, tel_num, author, price, city, avito_section, user_section, description)
                                    Values(@index,@idAvito,@url,@title,@phone,@seller,@price,@city,@subcategory,@section,@desc)";
      var cost = "null";
      var phone = "null";
      if (list != null && list.Count > 0)
      {
        try
        {
          var cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
          cmd.CommandText = sql;
          cmd.Prepare();
          //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
          // и тд
          if (list[PartsPage.Cost] != null)
            cost = list[PartsPage.Cost].First();
          if (list[PartsPage.Phone] != null)
            phone = list[PartsPage.Phone].First();
          cmd.Parameters.AddWithValue("@index", index);
          cmd.Parameters.AddWithValue("@idAvito", list[PartsPage.Id].First());
          cmd.Parameters.AddWithValue("@url", url);
          cmd.Parameters.AddWithValue("@title", list[PartsPage.Title].First());
          cmd.Parameters.AddWithValue("@phone", phone);
          cmd.Parameters.AddWithValue("@seller", list[PartsPage.Seller].First());
          cmd.Parameters.AddWithValue("@price", cost);
          cmd.Parameters.AddWithValue("@city", list[PartsPage.City].First());
          cmd.Parameters.AddWithValue("@subcategory", list[PartsPage.SubCategory].First());
          cmd.Parameters.AddWithValue("@section", section);
          cmd.Parameters.AddWithValue("@desc", list[PartsPage.Body].First());
          var result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          throw new Exception("MySql error: [" + sql + "] [index = " + index + "] [idAvito = " + list[PartsPage.Id].First() + "] [url = " + url +
            "] [title = " + list[PartsPage.Title].First() + "] [phone = " + phone + "] [seller = " + list[PartsPage.Seller].First() + 
            "] [price = " + cost + "] [city = " + list[PartsPage.City].First() + "] [subcategory = " + list[PartsPage.SubCategory].First() +
            "] [section = " + section + "] [desc = " + list[PartsPage.Body].First() + " ]: " + ex.Message, ex);
        }
      }
    }
    public void InsertItemResource(string resourceid, string url, string directory)
    {
      if (resourceid != null)
      {
        //Тело запроса!!!!!!!
        const string fixedDirectory = @"oc-content/uploads/";
        const string sql = @" insert into oc_t_item_resource
                                    select @resourceid, 1, null, ""jpg"", ""image/jpeg"", @directory";
        try
        {
          var cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
          cmd.CommandText = sql;
          cmd.Prepare();
          cmd.Parameters.AddWithValue("@resourceid", resourceid);
          if (directory.EndsWith("\\"))
            directory.TrimEnd('\\');
          if (!directory.EndsWith("/"))
            directory += "/";
          cmd.Parameters.AddWithValue("@directory", fixedDirectory + directory);

          var result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          throw new Exception("MySql error: [" + sql + "] [resourceid = " + resourceid + "]: " + ex.Message, ex);
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
          var cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
          cmd.CommandText = sql;
          cmd.Prepare();
          //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
          // и тд
          cmd.Parameters.AddWithValue("@index1", index1);
          cmd.Parameters.AddWithValue("@index2", index2);
          var result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          throw new Exception("MySql error: [" + sql + "] [index1 = ]" + index1 + "] [index2 = ]" + index2 +  "]: " + ex.Message, ex);
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
        var cmd = new MySqlCommand();
        cmd.Connection = mySqlConnection;
        cmd.CommandText = sql;
        cmd.Prepare();
        var result = cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "]: " + ex.Message, ex);
      }
    }
    // From MySqlDB2
    public IList<SectionItem> LoadSectionLinkEbay()
    {
      try
      {
        var allLinks = LoadSectionsLinkEx();
        var newlist = allLinks.Where(x => x.site == SectionItem.Site.Ebay);
        return newlist.ToList();
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: " + ex.Message, ex);
      }
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
      var resultStr = string.Empty;
      MySqlDataReader reader = null;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          var link = reader.GetString(0);
          var category_name = reader.GetString(1);
          var siteCurrent = SectionItem.Site.UnTyped;
          if (link != "NULL" && link != null && link != string.Empty)
          {
            //if (link == null || link == string.Empty) System.Diagnostics.Debugger.Break();
            var uri = new Uri(link);

            if (uri.Host == hostAvito)
              siteCurrent = SectionItem.Site.Avito;
            else if (uri.Host == hostEbay)
              siteCurrent = SectionItem.Site.Ebay;
            var section = new SectionItem { Link = link, site = siteCurrent, CategoryName = category_name };
            links.Add(section);
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "]: " + ex.Message, ex);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
      return links;
    }
    public IList<long> LoadAuctionLink()
    {
      //тело запроса!!!!!!!!!!//set order by ordering
      const string sql = "call sp_get_ebay_AuctionsListForCheck();";
      IList<long> links = new List<long>();
      var resultStr = string.Empty;
      MySqlDataReader reader = null;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          var id = reader.GetInt64(0);
          links.Add(id);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "]: " + ex.Message, ex);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
      return links;
    }

    public string GetEBayIDResourceListByEBayID(string ebay_id)
    {
      const string sql = "select id_resource_list from fct_grabber_ebay where ebay_id = @ebay_id;";
      var resultStr = string.Empty;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Connection = mySqlConnection;
        cmd.CommandText = sql;
        cmd.Prepare();

        cmd.Parameters.AddWithValue("@ebay_id", ebay_id);
        var result = cmd.ExecuteScalar();
        if (result != null)
        {
          var r = Convert.ToInt32(result);
          resultStr = r.ToString();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] " + ex.Message, ex);
      }
      return resultStr;
    }

    public int UpdateAuction(GetMultipleItemsResponse list, out string TimeLeft)
    {
      //Тело запроса!!!!!!!
      const string sql = @"update fct_grabber_ebay set price=@price where ebay_id=@id";
      decimal price = 0;
      ulong id = 0;
      var published = 0;
      TimeLeft = "ERROR";
      try
      {
        foreach (var item in list.Item)
        {
          TimeLeft = item.TimeLeft;
          var is_auction = item.TimeLeft == "PT0S";
          if (!is_auction)
            break;

          var cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
          cmd.CommandText = sql;
          cmd.Prepare();
          //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
          // и тд
          price = item.CurrentPrice.Value;
          id = item.ItemID;
          cmd.Parameters.AddWithValue("@price", price);
          cmd.Parameters.AddWithValue("@id", id);
          int result = cmd.ExecuteNonQuery();

          if (Properties.Default.PublishParsedData)
          {
            ExecuteProcEBay(GetEBayIDResourceListByEBayID(Convert.ToString(id)));
            published = 1;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [price = " + price + "] [id = " + id + "]: " + ex.Message, ex);
      }
      return published;
    }
    public bool IsNewAdAvito(int id)
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call sp_check_avito_IsNewAd(@Id);";
      CountAd++;
      var resultValue = true;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Connection = mySqlConnection;
        cmd.CommandText = sql;
        cmd.Prepare();

        cmd.Parameters.AddWithValue("@Id", id);
        var result = cmd.ExecuteScalar();
        if (result != null)
        {
          var r = Convert.ToInt32(result);
          result = (r != 0);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [id = " + id + "]: " + ex.Message, ex);
      }
      return resultValue;
    }
    public bool IsNewAdEbay(long id)
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call sp_check_ebay_IsNewAd(@Id);";
      CountAd++;
      var resultValue = true;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Connection = mySqlConnection;
        cmd.CommandText = sql;
        cmd.Prepare();

        cmd.Parameters.AddWithValue("@Id", id);
        var result = cmd.ExecuteScalar();
        if (result != null)
        {
          int r = Convert.ToInt32(result);
          resultValue = (r != 0);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [Id = " + id + "]: " + ex.Message, ex);
      }
      return resultValue;
    }
    public string ResourceListIDEbay()
    {
      //тело запроса!!!!!!!!!!
      const string sql = "select max(ifnull(v,0)) from (select max(pk_i_id) v from oc_t_item union select max(id_resource_list) from fct_grabber_ebay) t";
      var resultStr = string.Empty;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        var result = cmd.ExecuteScalar();
        if (result != null)
        {
          var r = Convert.ToInt32(result);
          resultStr = r.ToString();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "]: " + ex.Message, ex);
      }
      return resultStr;
    }
    public string ExecuteProcEBay(string id)
    {
      //тело запроса!!!!!!!!!!
      const string sql = "call sp_map_grabber_ebay(@id)";
      var resultStr = string.Empty;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Prepare();
        cmd.Parameters.AddWithValue("@id", id);
        var result = cmd.ExecuteScalar();
        if (result != null)
          resultStr = result.ToString();
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [id = " + id + "]: " + ex.Message, ex);
      }
      return resultStr;
    }
    public string InsertSMSSpamerData(string id)
    {
      const string sql = "call sp_fill_smsspamer_data(@id)";
      var resultStr = string.Empty;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Prepare();
        cmd.Parameters.AddWithValue("@id", id);
        var result = cmd.ExecuteScalar();
        if (result != null)
          resultStr = result.ToString();
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [id = " + id + "]: " + ex.Message, ex);
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
            InsertEbay(item, section);
        }
        catch (Exception ex)
        {
          throw new Exception("MySql error: " + ex.Message, ex);
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
          throw new Exception("MySql error: " + ex.Message, ex);
        }
      }
    }
    public bool InsertEbay(GetMultipleItemsResponseItem item, string section)
    {
      const string sql = @" insert into fct_grabber_ebay (id_resource_list, ebay_id,url, title, author, price, city,country, ebay_section, user_section, description,curr_code,is_auction,bid,transformated)
                                    Values(@index,@idEbay,@url,@title,@seller,@price,@city,@country,@subcategory,@section,@desc,@currency,@is_auction,@bid,@transformated)";
      var index = "null";
      decimal? price = null;
      decimal? bid = null;
      var trans = 0;
      var is_auction = false;
      try
      {
        index = Convert.ToString(Convert.ToInt32(ResourceListIDEbay()) + 1);
        is_auction = item.MinimumToBid != null;
        if (is_auction)
          trans = 4;
        var cmd = new MySqlCommand();
        cmd.Connection = mySqlConnection;
        cmd.CommandText = sql;
        cmd.Prepare();
        //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
        // и тд
        
        if (is_auction)
          bid = item.CurrentPrice.Value;
        else
          price = item.CurrentPrice.Value;

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

        var result = cmd.ExecuteNonQuery();
        return is_auction;
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [index = " + index + "] [idEbay = " + item.ItemID + "] [url = " + item.ViewItemURLForNaturalSearch +
          "] [title = " + item.Title + "] [seller = " + item.Seller.UserID + "] [price = " + price + "] [city = " + item.Location +
          "] [country = " + item.Country + "] [subcategory = " + item.PrimaryCategoryName + "] [section = " + section + "] [desc = " + item.Description +
          "] [currency = " + item.CurrentPrice.currencyID + "] [is_auction = " + is_auction + "] [transformated = " + trans + "] [bid = " + bid +
          "]: " + ex.Message, ex);
      }
    }
    public void InsertassGrabberEbayResourceList(string index1, string index2)
    {
      //Тело запроса!!!!!!!
      const string sql = @" insert into ass_grabber_ebay_resource_list
                    Values(@index1,@index2)";
      if (index1 != null)
      {
        try
        {
          var cmd = new MySqlCommand();
          cmd.Connection = mySqlConnection;
          cmd.CommandText = sql;
          cmd.Prepare();
          //Параметры для вставки типа в теле @phone тут мы  list[PartsPage.Phone].First<string>()
          // и тд
          cmd.Parameters.AddWithValue("@index1", index1);
          cmd.Parameters.AddWithValue("@index2", index2);
          var result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          throw new Exception("MySql error: [" + sql + "] [index1 = " + index1 + "] [index2 = " + index2 + "]: " + ex.Message, ex);
        }
      }
    }
  }
}
