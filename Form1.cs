using AvitoRuslanParser;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ParsersChe.WebClientParser.Proxy;
using System.Threading;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public static string URLLink;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadField();
            {
                MySqlCommand command = new MySqlCommand(); ;
                string connectionString, commandString;
                connectionString = "server=localhost;user=root;database=playandbay;port=3306;password=Galka91;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                commandString = @"select concat(d1.s_name, "" | "", coalesce(d2.s_name,""-""), "" | "", coalesce(d3.s_name,""-""), "" | "", coalesce(d4.s_name,""-"")) s_name,
                                         coalesce(c4.pk_i_id, c3.pk_i_id, c2.pk_i_id, c1.pk_i_id) id
                                  from playandbay.oc_t_category c1
                                  left join playandbay.oc_t_category c2 on c1.pk_i_id=c2.fk_i_parent_id
                                  left join playandbay.oc_t_category c3 on c2.pk_i_id=c3.fk_i_parent_id
                                  left join playandbay.oc_t_category c4 on c3.pk_i_id=c4.fk_i_parent_id
                                  left join playandbay.oc_t_category_description d1 on c1.pk_i_id=d1.fk_i_category_id
                                  left join playandbay.oc_t_category_description d2 on c2.pk_i_id=d2.fk_i_category_id
                                  left join playandbay.oc_t_category_description d3 on c3.pk_i_id=d3.fk_i_category_id
                                  left join playandbay.oc_t_category_description d4 on c4.pk_i_id=d4.fk_i_category_id
                                  where d1.fk_c_locale_code = ""ru_RU"" 
                                        and (d2.fk_c_locale_code = ""ru_RU"" or d2.fk_c_locale_code is null)
                                        and (d3.fk_c_locale_code = ""ru_RU"" or d3.fk_c_locale_code is null)
                                        and (d4.fk_c_locale_code = ""ru_RU"" or d4.fk_c_locale_code is null)
                                        and c1.fk_i_parent_id is null
                                  order by d1.s_name, d2.s_name, d3.s_name, d4.s_name;";
                command.CommandText = commandString;
                command.Connection = connection;
                MySqlDataReader reader;
                try
                {
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["s_name"]);
                    }
                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: \r\n{0}", ex.ToString());
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            SaveField();
        }
     //Методот по события нажатия кнопки
        private void button1_Click(object sender, EventArgs e)
        {//Проверку на пустые знаяения полей
            if (!string.IsNullOrEmpty(LinkAdtextBox.Text) && !string.IsNullOrEmpty(userNametextBox.Text)
                && !string.IsNullOrEmpty(PasswordtextBox.Text) && !string.IsNullOrEmpty(pathToProxytextBox.Text)
                && !string.IsNullOrEmpty(pathToImgtextBox.Text))
            {
                try
                {
                    label6.Text = "Start";
                    //Ссылка на обьявление
                    URLLink = LinkAdtextBox.Text;
                    MySqlDB.DeleteUnTransformated();
                    //Создаем класс и вводим параметры 
                    var Parser = new RuslanParser(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);
                    Parser.PathImages = pathToImgtextBox.Text;
                    //вот тут мы вызывем запрос на Id к базе
                    //Parser.LoadGuid = (() => MySqlDB.SeletGuid());
                    //Parser.LoadGuid = (() => "1532");
                    //тут мы присваем результат переменной result
                    var result = Parser.Run(URLLink);
                    // тут мы передаем в метод вставки данные
                    //result, index ID, ссылку на обьявления
                    //id я не вставлял так как непонятно было и неподходило под структуру бд
                    MySqlDB.InsertFctAvitoGrabber(result, MySqlDB.ResourceListID(), URLLink, listBox1.Text);
                    //MessageBox.Show(MySqlDB.PictureID());
                    //MessageBox.Show(MySqlDB.PictureListID());
                    var Parser2 = new RuslanParser2(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);
                    Parser2.PathImages2 = pathToImgtextBox.Text;
                    var result2 = Parser2.Run(URLLink);
                    MySqlDB.ExecuteProc();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                }
                label6.Text = "Finish";
            }
        }

        private void LoadSection(string[] linkSection)
        {
            if (!string.IsNullOrEmpty(LinkAdtextBox.Text) && !string.IsNullOrEmpty(userNametextBox.Text)
               && !string.IsNullOrEmpty(PasswordtextBox.Text) && !string.IsNullOrEmpty(pathToProxytextBox.Text)
               && !string.IsNullOrEmpty(pathToImgtextBox.Text))
            {
                try
                {
                    label6.Text = "Start";
                    
                    //Ссылка на обьявление
                  //  URLLink = LinkAdtextBox.Text;
                    //Создаем класс и вводим параметры 
                    var Parser = new RuslanParser(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);
                    var Parser2 = new RuslanParser2(userNametextBox.Text, PasswordtextBox.Text, pathToProxytextBox.Text);

                    Parser.PathImages = pathToImgtextBox.Text;

                    var linksAds = Parser.LoadLinks(linkSection[0]);
                int i=0;
                    foreach (var item in linksAds)
                    {
                        i++;
                        if (i == 25) 
                        {
                            i = -1;
                        }
                        URLLink = item;
                      //  MySqlDB.DeleteUnTransformated();
                        var result = Parser.Run(item);

                        if (result[PartsPage.Cost] != null)
                        {
                            MySqlDB.InsertFctAvitoGrabber(result, MySqlDB.ResourceListID(), item, linkSection[1]);


                            Parser2.PathImages2 = pathToImgtextBox.Text;

                            var result2 = Parser2.Run(item);
                            MySqlDB.ExecuteProc();

                        }
                        Thread.Sleep(25 * 1000);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                }
                label6.Text = "Finish";
            }
        }
        #region fieldSaveLoad
        private void SaveField()
        {
            Properties.Default.User = userNametextBox.Text;
            Properties.Default.Password = PasswordtextBox.Text;
            Properties.Default.LinkOnAd = LinkAdtextBox.Text;
            Properties.Default.PathToImg = pathToImgtextBox.Text;
            Properties.Default.PathToProxy = pathToProxytextBox.Text;
            Properties.Default.Save();

        }
        private void LoadField()
        {
            userNametextBox.Text = Properties.Default.User;
            PasswordtextBox.Text = Properties.Default.Password;
            LinkAdtextBox.Text = Properties.Default.LinkOnAd;
            pathToImgtextBox.Text = Properties.Default.PathToImg;
            pathToProxytextBox.Text = Properties.Default.PathToProxy;
        }
        #endregion

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var links = MySqlDB.LoadSectionsLink();
            foreach (var item in links)
            {
                LoadSection(item);
            //    Thread.Sleep(25 * 1000);
            }
            ProxyCollectionSingl.Instance.Dispose();
        }
    }
}
