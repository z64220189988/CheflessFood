using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace 個人專題Chefless_Food__Decentralization
{
    public partial class Form使用者登入 : UIForm
    {

        public Form使用者登入()
        {
            InitializeComponent();
        }

        private void Form使用者登入_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "chefless";
            scsb.IntegratedSecurity = true;
            GlobalVar.strDBConnectionString = scsb.ConnectionString;

            string username = Properties.Settings.Default.Username;
            string password = Properties.Settings.Default.Password;
            // 检查并显示设置的值，帮助调试
            Console.WriteLine($"Loaded Username: {username}");
            Console.WriteLine($"Loaded Password: {password}");

            // 從設定中讀取使用者名稱和密碼
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Username) && !string.IsNullOrEmpty(Properties.Settings.Default.Password))
            {
                txt使用者登入_帳號.Text = Properties.Settings.Default.Username;
                txt使用者登入_密碼.Text = Properties.Settings.Default.Password;
                chk使用者登入_記住我.Checked = true;
            }
        }

        private void btn使用者登入_登入_Click(object sender, EventArgs e)
        {
            GlobalVar.使用者帳號 = txt使用者登入_帳號.Text.Trim();
            GlobalVar.使用者密碼 = txt使用者登入_密碼.Text.Trim();

            if ((txt使用者登入_帳號.Text != "") && (txt使用者登入_密碼.Text != ""))
            {
                SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
                con.Open();
                string strSQL = "select * from Persons where email = @SearchEmail and 密碼 = @SearchPassword;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchEmail", GlobalVar.使用者帳號);
                cmd.Parameters.AddWithValue("@SearchPassword", GlobalVar.使用者密碼);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    GlobalVar.is登入成功 = true;
                    GlobalVar.使用者名稱 = reader["姓名"].ToString();
                    GlobalVar.使用者帳號 = (string)reader["email"];
                    GlobalVar.使用者權限 = (int)reader["權限"];
                    GlobalVar.使用者ID = Convert.ToInt32(reader["id"]); // 讀取使用者ID

                    if (chk使用者登入_記住我.Checked)
                    {
                        Properties.Settings.Default.Username = GlobalVar.使用者帳號;
                        Properties.Settings.Default.Password = GlobalVar.使用者密碼;
                    }
                    else
                    {
                        Properties.Settings.Default.Username = string.Empty;
                        Properties.Settings.Default.Password = string.Empty;
                    }
                    Properties.Settings.Default.Save();

                    Console.WriteLine($"使用者ID:{GlobalVar.使用者ID}");

                    // 根據權限判斷身分組

                    if (GlobalVar.使用者權限 >= 1000)
                    {
                        GlobalVar.身分組 = "客戶";
                        string message = $"登入成功\r\n登入姓名:{GlobalVar.使用者名稱}\r\n請至顧客專區";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show($"登入成功\n登入姓名:{GlobalVar.使用者名稱}\n請至顧客專區");

                        this.Close();
                    }
                    else if (GlobalVar.使用者權限 >= 100)
                    {
                        GlobalVar.身分組 = "店員";
                    }
                    else if (GlobalVar.使用者權限 >= 10)
                    {
                        GlobalVar.身分組 = "店長";
                    }
                    else if (GlobalVar.使用者權限 >= 1)
                    {
                        GlobalVar.身分組 = "老闆";
                    }

                    if (GlobalVar.使用者權限 < 1000)
                    {

                        //MessageBox.Show($"登入成功\n登入姓名:{GlobalVar.使用者名稱}\n身分組: {GlobalVar.身分組}");
                        // 這裡可以加入跳轉到相應管理界面的代碼
                        string message = $"登入成功！\r\n登入姓名:{GlobalVar.使用者名稱}\r\n身分組: {GlobalVar.身分組}";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                    }
                    this.Close();
                    reader.Close();
                    con.Close();
                }
                else
                {
                    GlobalVar.is登入成功 = false;
                    //MessageBox.Show("登入失敗!");
                    string message = "登入失敗!";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                }
                reader.Close();
                con.Close();
            }
            else
            {
                //MessageBox.Show("輸入資料有誤");
                string message = "\r\n輸入資料有誤";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }

            }

        }

        private void btn使用者登入_取消_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt使用者登入_帳號_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt使用者登入_密碼_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn使用者登入_註冊_Click(object sender, EventArgs e)
        {
            Form註冊帳號 註冊 = new Form註冊帳號();
            註冊.ShowDialog();
        }

        private void txt使用者登入_帳號_MouseClick(object sender, EventArgs e)
        {
            txt使用者登入_帳號.Clear();
        }

        private void txt使用者登入_密碼_MouseClick(object sender, EventArgs e)
        {
            txt使用者登入_密碼.Clear();
        }

        private void chk使用者登入_記住我_CheckedChanged(object sender, EventArgs e)
        {
            if (chk使用者登入_記住我.Checked)
            {
                Properties.Settings.Default.Username = txt使用者登入_帳號.Text;
                Properties.Settings.Default.Password = txt使用者登入_密碼.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
                Properties.Settings.Default.Save();
            }
        }
    }
}