using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace 個人專題Chefless_Food__Decentralization
{
    public partial class Form註冊帳號 : UIForm
    {
        public Form註冊帳號()
        {
            InitializeComponent();
        }

        private void Form註冊帳號_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new
            SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "chefless";
            scsb.IntegratedSecurity = true;//windows整合驗證
            scsb.Encrypt = false;
            GlobalVar.strDBConnectionString = scsb.ConnectionString;

        }

        private void btn註冊帳號_送出_Click(object sender, EventArgs e)
        {
            // 獲取輸入的資料
            string 帳號 = txt註冊帳號_Email.Text.Trim();
            string 密碼 = txt註冊帳號_密碼.Text.Trim();
            string 姓名 = txt註冊帳號_姓名.Text.Trim();
            string 手機號碼 = txt註冊帳號_手機號碼.Text.Trim();

            // 檢查所有欄位是否已填寫
            if (string.IsNullOrEmpty(帳號) || string.IsNullOrEmpty(密碼) || string.IsNullOrEmpty(姓名) || string.IsNullOrEmpty(手機號碼))
            {
                string message = "所有欄位都必須填寫！";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                return; // 提前結束方法，不繼續執行下面的代碼
            }

            // 檢查密碼是否相同
            string 密碼確認 = txt註冊帳號_密碼確認.Text.Trim();
            if (密碼 != 密碼確認)
            {
                //MessageBox.Show("密碼和確認密碼不相符");

                string message = "密碼和確認密碼不相符";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }

                // 清空密碼和確認密碼輸入框
                txt註冊帳號_密碼.Clear();
                txt註冊帳號_密碼確認.Clear();

                // 將光標移到txt註冊帳號_密碼
                txt註冊帳號_密碼.Focus();
            }
            else
            {
                // 密碼確認相符，執行 SQL 命令，將資料插入到 persons 資料表中
                using (SqlConnection connection = new SqlConnection(GlobalVar.strDBConnectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO persons (email, 密碼, 姓名, 手機號碼) VALUES (@Email, @Password, @Name, @PhoneNumber)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", 帳號);
                        command.Parameters.AddWithValue("@Password", 密碼);
                        command.Parameters.AddWithValue("@Name", 姓名);
                        command.Parameters.AddWithValue("@PhoneNumber", 手機號碼);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            string message = "註冊成功！";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("註冊成功！");
                            // 清空輸入框
                            txt註冊帳號_Email.Clear();
                            txt註冊帳號_密碼.Clear();
                            txt註冊帳號_密碼確認.Clear();
                            txt註冊帳號_姓名.Clear();
                            txt註冊帳號_手機號碼.Clear();
                        }
                        else
                        {
                            string message = "註冊失敗！";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("註冊失敗！");
                        }
                    }
                }
            }
        }


        private void btn註冊帳號_取消_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}