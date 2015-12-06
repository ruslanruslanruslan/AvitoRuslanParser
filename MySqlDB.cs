using System;
using System.Collections.Generic;
using System.Drawing;
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

    public delegate void DataBaseLog(string msg, Color color);

    public DataBaseLog dbLog = null;

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

    private MySqlCommand Prepare(string query, Dictionary<string, object> parameters = null)
    {
      var cmd = new MySqlCommand(query, mySqlConnection);
      if (parameters != null)
      {
        cmd.Prepare();
        foreach (var param in parameters)
          cmd.Parameters.AddWithValue(param.Key, param.Value);
      }
      return cmd;
    }

    private string PrepareErrorMessage(string query, Dictionary<string, object> parameters, Exception ex)
    {
      var str = "MySql error: [" + query + "]";
      if (parameters != null)
        foreach (var param in parameters)
          str += " [" + param.Key + " = " + param.Value + "]";
      str += ": " + ex.Message;
      return str;
    }

    private object ExecuteScalar(string query, Dictionary<string, object> parameters = null)
    {
      try
      {
        var result = Prepare(query, parameters).ExecuteScalar();
        LogQuery(query, parameters, result);
        return result;
      }
      catch (Exception ex)
      {
        throw new Exception(PrepareErrorMessage(query, parameters, ex), ex);
      }
    }

    private List<Dictionary<string, string>> ExecuteReader(string query, Dictionary<string, object> parameters = null)
    {
      MySqlDataReader result = null;
      try
      {
        result = Prepare(query, parameters).ExecuteReader();
        var list = new List<Dictionary<string, string>>();
        while (result.Read())
        {
          var res = new Dictionary<string, string>();
          for (var i = 0; i < result.FieldCount; ++i)
            res.Add(result.GetName(i), result.GetString(i));
          list.Add(res);
        }
        LogReader(query, parameters, list);
        return list;
      }
      catch (Exception ex)
      {
        throw new Exception(PrepareErrorMessage(query, parameters, ex), ex);
      }
      finally
      {
        if (result != null)
          result.Close();
      }
    }

    private int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
    {
      try
      {
        var result = Prepare(query, parameters).ExecuteNonQuery();
        LogQuery(query, parameters, result);
        return result;
      }
      catch (Exception ex)
      {
        throw new Exception(PrepareErrorMessage(query, parameters, ex), ex);
      }
    }

    private string PrepareLog(string query, Dictionary<string, object> parameters)
    {
      var str = query;
      str += Environment.NewLine;
      if (parameters != null)
        foreach (var param in parameters)
          str += param.Key + ": " + param.Value + Environment.NewLine;
      return str;
    }

    private void LogQuery(string query, Dictionary<string, object> parameters, object result)
    {
      if (dbLog == null)
        return;

      var str = PrepareLog(query, parameters);
      str += "Result: " + result;

      dbLog(str, LogMessageColor.DataBase());
    }

    private void LogReader(string query, Dictionary<string, object> parameters, List<Dictionary<string, string>> result)
    {
      if (dbLog == null)
        return;

      var str = PrepareLog(query, parameters);
      str += "Results: " + Environment.NewLine + "++-----++" + Environment.NewLine;
      foreach (var row in result)
      {
        foreach (var val in row)
          str += val.Key + ": " + val.Value + Environment.NewLine;
        str += "++-----++" + Environment.NewLine;
      }

      dbLog(str, LogMessageColor.DataBase());
    }

    public int CountAd { get; set; }

    public IList<string[]> LoadSectionsLink()
    {
      const string sql = "call sp_get_SearchUrlsList();";
      IList<string[]> links = new List<string[]>();
      var reader = ExecuteReader(sql);
      foreach (var row in reader)
      {
        var link = row.Values.ElementAt(0);
        var category_name = row.Values.ElementAt(1);
        links.Add(new string[] { link, category_name });
      }
      return links;
    }
    public IList<string> GetCategories()
    {
      const string sql = "call sp_get_CategoriesList();";
      IList<string> results = new List<string>();
      var reader = ExecuteReader(sql);
      foreach (var row in reader)
        results.Add(row["s_name"]);
      return results;
    }

    public string ResourceID()
    {
      const string sql = "select fn_get_NextResourceId();";
      var resultStr = string.Empty;
      var result = ExecuteScalar(sql);
      if (result != null)
      {
        var r = Convert.ToInt32(result);
        resultStr = r.ToString();
      }
      return resultStr;
    }
    public string ResourceListIDAvito()
    {
      const string sql = "call sp_get_avito_NextItemId();";
      var resultStr = string.Empty;
      var result = ExecuteScalar(sql);
      if (result != null)
      {
        var r = Convert.ToInt32(result);
        resultStr = r.ToString();
      }
      return resultStr;
    }
    public string ExecuteProcAvito(string id)
    {
      const string sql = "call sp_map_grabber_avito(@id)";
      var resultStr = string.Empty;
      var parameters = new Dictionary<string, object>();
      parameters.Add("@id", id);
      var result = ExecuteScalar(sql, parameters);
      if (result != null)
        resultStr = result.ToString();
      return resultStr;
    }

    public void InsertFctAvitoGrabber(Dictionary<PartsPage, IEnumerable<string>> list, string index, string url, string section)
    {
      const string sql = @"insert into fct_grabber_avito (id_resource_list, avito_id,url, title, tel_num, author, price, city, avito_section, user_section, description)
                                    Values(@index,@idAvito,@url,@title,@phone,@seller,@price,@city,@subcategory,@section,@desc)";
      var cost = "null";
      var phone = "null";
      if (list != null && list.Count > 0)
      {
        if (list[PartsPage.Cost] != null)
          cost = list[PartsPage.Cost].First();
        if (list[PartsPage.Phone] != null)
          phone = list[PartsPage.Phone].First();
        var parameters = new Dictionary<string, object>();

        parameters.Add("@index", index);
        parameters.Add("@idAvito", list[PartsPage.Id].First());
        parameters.Add("@url", url);
        parameters.Add("@title", list[PartsPage.Title].First());
        parameters.Add("@phone", phone);
        parameters.Add("@seller", list[PartsPage.Seller].First());
        parameters.Add("@price", cost);
        parameters.Add("@city", list[PartsPage.City].First());
        parameters.Add("@subcategory", list[PartsPage.SubCategory].First());
        parameters.Add("@section", section);
        parameters.Add("@desc", list[PartsPage.Body].First());
        ExecuteNonQuery(sql, parameters);
      }
    }
    public void UpdateFctAvitoGrabberPhotoCount(string index, int photo_cnt)
    {
      const string sql = @"update fct_grabber_avito set photo_cnt=@photo_cnt where id_resource_list=@index";
      var parameters = new Dictionary<string, object>();
      parameters.Add("@photo_cnt", photo_cnt);
      parameters.Add("@index", index);
      ExecuteNonQuery(sql, parameters);
    }
    public void InsertItemResource(string resourceid, string url, string directory)
    {
      if (resourceid != null)
      {
        const string fixedDirectory = @"oc-content/uploads/";
        const string sql = @" insert into oc_t_item_resource
                                    select @resourceid, 1, null, ""jpg"", ""image/jpeg"", @directory";
        var parameters = new Dictionary<string, object>();
        parameters.Add("@resourceid", resourceid);
        if (directory.EndsWith("\\"))
          directory.TrimEnd('\\');
        if (!directory.EndsWith("/"))
          directory += "/";
        parameters.Add("@directory", fixedDirectory + directory);

        ExecuteNonQuery(sql, parameters);
      }
    }
    public void InsertassGrabberAvitoResourceList(string index1, string index2)
    {
      const string sql = @" insert into ass_grabber_avito_resource_list (id_resource_list, id_resource)
                    Values(@index1,@index2)";
      if (index1 != null)
      {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@index1", index1);
        parameters.Add("@index2", index2);
        ExecuteNonQuery(sql, parameters);
      }
    }
    public void PrepareAvitoEnvironment()
    {
      const string sql = @"call sp_prepare_avito_environment;";
      ExecuteNonQuery(sql);
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
    public IList<SectionItem> LoadSectionsLinkEx()
    {
      const string sql = "SELECT search_url, category_name FROM fct_categories_search where search_url is not null order by ordering";
      IList<SectionItem> links = new List<SectionItem>();
      var resultStr = string.Empty;

      var reader = ExecuteReader(sql);
      foreach (var row in reader)
      {
        var link = row.Values.ElementAt(0);
        var category_name = row.Values.ElementAt(1);
        var siteCurrent = SectionItem.Site.UnTyped;
        if (link.ToUpper() != "NULL" && link != null && link != string.Empty)
        {
          var uri = new Uri(link);

          if (uri.Host == hostAvito)
            siteCurrent = SectionItem.Site.Avito;
          else if (uri.Host == hostEbay)
            siteCurrent = SectionItem.Site.Ebay;
          var section = new SectionItem { Link = link, site = siteCurrent, CategoryName = category_name };
          links.Add(section);
        }
      }
      return links;
    }
    public IList<long> LoadAuctionLink()
    {
      const string sql = "call sp_get_ebay_AuctionsListForCheck();";
      IList<long> links = new List<long>();
      var resultStr = string.Empty;
      var reader = ExecuteReader(sql);
      foreach (var row in reader)
      {
        var id = Convert.ToInt64(row.Values.ElementAt(0));
        links.Add(id);
      }
      return links;
    }

    public string GetEBayIDResourceListByEBayID(string ebay_id)
    {
      const string sql = "select id_resource_list from fct_grabber_ebay where ebay_id = @ebay_id;";
      var resultStr = string.Empty;
      var parameters = new Dictionary<string, object>();
      parameters.Add("@ebay_id", ebay_id);
      var result = ExecuteScalar(sql, parameters);
      if (result != null)
      {
        var r = Convert.ToInt32(result);
        resultStr = r.ToString();
      }

      return resultStr;
    }

    public int UpdateAuction(GetMultipleItemsResponse list, out string TimeLeft)
    {
      const string sql = @"update fct_grabber_ebay set price=@price where ebay_id=@id";
      decimal price = 0;
      ulong id = 0;
      var published = 0;
      TimeLeft = "ERROR";
      foreach (var item in list.Item)
      {
        TimeLeft = item.TimeLeft;
        var is_auction = item.TimeLeft == "PT0S";
        if (!is_auction)
          break;

        price = item.CurrentPrice.Value;
        id = item.ItemID;
        var parameters = new Dictionary<string, object>();
        parameters.Add("@price", price);
        parameters.Add("@id", id);
        ExecuteNonQuery(sql, parameters);

        if (Properties.Default.PublishParsedData)
        {
          ExecuteProcEBay(GetEBayIDResourceListByEBayID(Convert.ToString(id)));
          published = 1;
        }
      }
      return published;
    }
    public bool IsNewAdAvito(int id)
    {
      const string sql = "call sp_check_avito_IsNewAd(@Id);";
      CountAd++;
      var resultValue = true;
      var parameters = new Dictionary<string, object>();
      parameters.Add("@Id", id);
      var result = ExecuteScalar(sql, parameters);
      if (result != null)
      {
        var r = Convert.ToInt32(result);
        resultValue = (r != 0);
      }
      return resultValue;
    }
    public bool IsNewAdEbay(long id)
    {
      const string sql = "call sp_check_ebay_IsNewAd(@Id);";
      CountAd++;
      var resultValue = true;
      var parameters = new Dictionary<string, object>();
      parameters.Add("@Id", id);
      var result = ExecuteScalar(sql, parameters);
      if (result != null)
      {
        int r = Convert.ToInt32(result);
        resultValue = (r != 0);
      }
      return resultValue;
    }
    public string ResourceListIDEbay()
    {
      const string sql = "select max(ifnull(v,0)) from (select max(pk_i_id) v from oc_t_item union select max(id_resource_list) from fct_grabber_ebay) t";
      var resultStr = string.Empty;
      var result = ExecuteScalar(sql);
      if (result != null)
      {
        var r = Convert.ToInt32(result);
        resultStr = r.ToString();
      }
      return resultStr;
    }
    public string ExecuteProcEBay(string id)
    {
      const string sql = "call sp_map_grabber_ebay(@id)";
      var resultStr = string.Empty;
      var parameters = new Dictionary<string, object>();
      parameters.Add("@id", id);
      var result = ExecuteScalar(sql, parameters);
      if (result != null)
        resultStr = result.ToString();
      return resultStr;
    }
    public string InsertSMSSpamerData(string id)
    {
      const string sql = "call sp_fill_smsspamer_data(@id)";
      var resultStr = string.Empty;
      var parameters = new Dictionary<string, object>();
      parameters.Add("@id", id);
      var result = ExecuteScalar(sql, parameters);
      if (result != null)
        resultStr = result.ToString();
      return resultStr;
    }

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
      index = Convert.ToString(Convert.ToInt32(ResourceListIDEbay()) + 1);
      is_auction = item.MinimumToBid != null;
      if (is_auction)
        trans = 4;

      var parameters = new Dictionary<string, object>();

      if (is_auction)
        bid = item.CurrentPrice.Value;
      else
        price = item.CurrentPrice.Value;

      parameters.Add("@index", index);
      parameters.Add("@idEbay", item.ItemID);
      parameters.Add("@url", item.ViewItemURLForNaturalSearch);
      parameters.Add("@title", item.Title);
      parameters.Add("@seller", item.Seller.UserID);
      parameters.Add("@price", price);
      parameters.Add("@city", item.Location);
      parameters.Add("@country", item.Country);

      parameters.Add("@subcategory", item.PrimaryCategoryName);
      parameters.Add("@section", section);
      parameters.Add("@desc", item.Description);
      parameters.Add("@currency", item.CurrentPrice.currencyID);
      parameters.Add("@is_auction", is_auction);
      parameters.Add("@transformated", trans);
      parameters.Add("@bid", bid);

      ExecuteNonQuery(sql, parameters);
      return is_auction;
    }
    public void UpdateFctEbayGrabberPhotoCount(string index, int photo_cnt)
    {
      const string sql = @"update fct_grabber_ebay set photo_cnt=@photo_cnt where id_resource_list=@index";
      var parameters = new Dictionary<string, object>();
      parameters.Add("@photo_cnt", photo_cnt);
      parameters.Add("@index", index);
      ExecuteNonQuery(sql, parameters);
    }
    public void InsertassGrabberEbayResourceList(string index1, string index2)
    {
      const string sql = @" insert into ass_grabber_ebay_resource_list
                    Values(@index1,@index2)";
      if (index1 != null)
      {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@index1", index1);
        parameters.Add("@index2", index2);
        ExecuteNonQuery(sql, parameters);
      }
    }
  }
}
