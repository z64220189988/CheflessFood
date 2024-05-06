using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace 個人專題Chefless_Food__Decentralization
{
    public partial class Form二手商品詳情 : UIForm
    {
        private List<int> 商品ID列表;
        private int 當前商品索引;
        private HOME homeForm;
        private bool is新商品 = false; // 新增的成員變量用於標識是否為新商品
        private string 上傳圖片檔名 = "預設照片.png";

        //無參數的構造函數，用於創建新商品
        public Form二手商品詳情()
        {
            InitializeComponent();
            初始化為新商品();
            Set使用者ID();
            btn二手商品詳情_發佈.Visible = false;
            btn二手商品詳情_上一筆.Visible = false;
            btn二手商品詳情_下一筆.Visible = false;
        }

        //有參數的構造函數，用於引用另一個Form的參數

        public Form二手商品詳情(List<int> 二手商品ID列表,int 當前索引,HOME home)
        {
            InitializeComponent();
            this.商品ID列表 = 二手商品ID列表;
            this.當前商品索引 = 當前索引;
            this.homeForm = home;

            // 窗體加載時設置商品信息
            this.Load += (sender, e) => Set二手商品信息(商品ID列表[當前商品索引]);
        }

        //-------------------------------------------------------------------

        private void 初始化為新商品()
        {
            is新商品 = true;
            btn二手商品詳情_上傳圖片.Visible = true;
            

            // 設置pictureBox為"上傳圖片.png"
            string uploadImagePath = System.IO.Path.Combine(GlobalVar.image_dir, "上傳圖片.png");
            if (System.IO.File.Exists(uploadImagePath))
            {
                pictureBox二手商品詳情.Image = Image.FromFile(uploadImagePath);
            }

            // 將所有控件設置為可編輯
            txt二手商品詳情_姓名.ReadOnly = false;
            txt二手商品詳情_商品名稱.ReadOnly = false;
            txt二手商品詳情_商品金額.ReadOnly = false;
            txt二手商品詳情_聯絡資訊.ReadOnly = false;
            txt二手商品詳情_商品描述.ReadOnly = false;

            cbox二手商品詳情_交易模式.ReadOnly = false;
            cbox二手商品詳情_區域.ReadOnly = false;
            cbox二手商品詳情_商品新舊程度.ReadOnly = false;
            cbox二手商品詳情_交易方式.ReadOnly = false;
            cbox二手商品詳情_是否接受議價.ReadOnly = false;

            btn二手商品詳情_刪除此商品.Visible = false;
            btn二手商品詳情_修改商品內容.Visible = false;

            // 清除所有控件的值
            txt二手商品詳情_姓名.Text = "";
            txt二手商品詳情_商品名稱.Text = "";
            txt二手商品詳情_商品金額.Text = "";
            txt二手商品詳情_聯絡資訊.Text = "";
            txt二手商品詳情_商品描述.Text = "";

            cbox二手商品詳情_交易模式.SelectedIndex = 0;
            cbox二手商品詳情_區域.SelectedIndex = 0;
            cbox二手商品詳情_商品新舊程度.SelectedIndex = 0;
            cbox二手商品詳情_交易方式.SelectedIndex = 0;
            cbox二手商品詳情_是否接受議價.SelectedIndex = 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 檢查是否按下了左鍵或右鍵
            if (keyData == Keys.Left)
            {
                btn二手商品詳情_上一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }
            else if (keyData == Keys.Right)
            {
                btn二手商品詳情_下一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }

            return base.ProcessCmdKey(ref msg, keyData); // 調用基類的處理器
        }

        public void Set二手商品信息(int 二手商品ID)
        {
           
            btn二手商品詳情_上傳圖片.Visible = false;
            // 假設 connectionString 是您的數據庫連接字符串
            string connectionString = GlobalVar.strDBConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM secondhand WHERE shid = @二手商品ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@二手商品ID", 二手商品ID);
                Console.WriteLine($"正在加載商品ID: {二手商品ID}");


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        txt二手商品詳情_姓名.Text = reader["shname"].ToString();
                        txt二手商品詳情_商品名稱.Text = reader["shpname"].ToString();
                        txt二手商品詳情_商品金額.Text = reader["shprice"].ToString();
                        txt二手商品詳情_聯絡資訊.Text = reader["shcontact"].ToString();
                        txt二手商品詳情_商品描述.Text = reader["shdesc"].ToString();

                        cbox二手商品詳情_交易模式.Text = reader["shbuyorsale"].ToString();
                        cbox二手商品詳情_區域.Text = reader["shrigion"].ToString();
                        cbox二手商品詳情_商品新舊程度.Text = reader["shnew"].ToString();
                        cbox二手商品詳情_交易方式.Text = reader["shtradeway"].ToString();
                        cbox二手商品詳情_是否接受議價.Text = reader["shnego"].ToString();

                        txt二手商品詳情_姓名.ReadOnly = true;
                        txt二手商品詳情_商品名稱.ReadOnly = true;
                        txt二手商品詳情_商品金額.ReadOnly = true;
                        txt二手商品詳情_聯絡資訊.ReadOnly = true;
                        txt二手商品詳情_商品描述.ReadOnly = true;

                        cbox二手商品詳情_交易模式.ReadOnly = true;
                        cbox二手商品詳情_區域.ReadOnly = true;
                        cbox二手商品詳情_商品新舊程度.ReadOnly = true;
                        cbox二手商品詳情_交易方式.ReadOnly = true;
                        cbox二手商品詳情_是否接受議價.ReadOnly = true;

                        int shcid = (int)reader["shcid"];

                        // 判斷當前登入的使用者是否為商品的擁有者
                        btn二手商品詳情_修改商品內容.Visible = (GlobalVar.使用者ID == shcid);
                        btn二手商品詳情_刪除此商品.Visible = (GlobalVar.使用者ID == shcid);

                        // 從資料庫獲取圖片檔名並確定圖片路徑
                        string dbImagePath = reader.IsDBNull(reader.GetOrdinal("shpimage")) ? "" : reader["shpimage"].ToString();
                        string imagePath = Path.Combine(GlobalVar.image_dir, "二手", string.IsNullOrEmpty(dbImagePath) ? 上傳圖片檔名 : dbImagePath);

                        if (File.Exists(imagePath))
                        {
                            pictureBox二手商品詳情.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            // 如果找不到圖片，則使用預設圖片
                            pictureBox二手商品詳情.Image = Image.FromFile(Path.Combine(GlobalVar.image_dir, "二手", 上傳圖片檔名));
                        }
                    }
                    else
                    {
                        // 商品信息未找到的處理
                        using (Form對話框 對話框 = new Form對話框("無法找到指定的商品詳情。"))
                        {
                            對話框.ShowDialog();
                        }
                    }
                }
            }
        }

        private void Set使用者ID()
        {
            string userEmail = GlobalVar.使用者帳號; // 获取用户邮箱

            
            string connectionString = GlobalVar.strDBConnectionString;
            string sql = "SELECT id FROM persons WHERE email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", userEmail);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        GlobalVar.使用者ID = Convert.ToInt32(result);
                    }
                    else
                    {
                        string message = "找不到使用者";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                    }
                }
            }
        }

        //private int Get商品所有者ID()
        //{
        //    int shcid = 0;

        //    // 检查商品ID列表是否为null
        //    if (商品ID列表 == null || 當前商品索引 < 0 || 當前商品索引 >= 商品ID列表.Count)
        //    {
        //        string message = "找不到指定的商品信息";
        //        using (Form對話框 對話框 = new Form對話框(message))
        //        {
        //            對話框.ShowDialog();
        //        }
        //        return shcid;
        //    }

        //    int shid = 商品ID列表[當前商品索引]; // 获取当前商品ID

        //    // 查询数据库获取当前商品的所有者ID
        //    string connectionString = GlobalVar.strDBConnectionString;
        //    string sql = "SELECT shcid FROM secondhand WHERE shid = @Shid";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            command.Parameters.AddWithValue("@Shid", shid);
        //            object result = command.ExecuteScalar();
        //            if (result != null)
        //            {
        //                shcid = Convert.ToInt32(result);
        //            }
        //            else
        //            {
        //                // 如果未找到匹配的商品ID，则提示用户
        //                string message = "找不到指定的商品信息";
        //                using (Form對話框 對話框 = new Form對話框(message))
        //                {
        //                    對話框.ShowDialog();
        //                }
        //            }
        //        }
        //    }

        //    returnshcid;
        //}


        private void 清空欄位()
        {
            // 清空標籤和文字框
            
            txt二手商品詳情_姓名.Text = "";
            txt二手商品詳情_商品名稱.Text = "";
            txt二手商品詳情_商品金額.Text = "";
            txt二手商品詳情_聯絡資訊.Text = "";
            txt二手商品詳情_商品描述.Text = "";

            // 清空ComboBox選擇
            cbox二手商品詳情_交易模式.SelectedIndex = -1;
            cbox二手商品詳情_區域.SelectedIndex = -1;
            cbox二手商品詳情_商品新舊程度.SelectedIndex = -1;
            cbox二手商品詳情_交易方式.SelectedIndex = -1;
            cbox二手商品詳情_是否接受議價.SelectedIndex = -1;

            // 可選：如果你也想要重置pictureBox到預設圖片，可以這樣做
            // 假設有一個預設圖片叫 "defaultImage.png" 在你的預設路徑下
            //string defaultImagePath = System.IO.Path.Combine(GlobalVar.image_dir, "defaultImage.png");
            //if (System.IO.File.Exists(defaultImagePath))
            //{
            //    pictureBox二手商品詳情.Image = Image.FromFile(defaultImagePath);
            //}
            //else
            //{
            //    // 如果沒有預設圖片，可能需要清空pictureBox或設置為null
            //    pictureBox二手商品詳情.Image = null;
            //}
        }

        private void 更新二手最近看過(int 二手商品ID)
        {
            if (!Properties.Settings.Default._二手最近看過.Contains(二手商品ID.ToString()))
            {
                Properties.Settings.Default._二手最近看過.Add(二手商品ID.ToString());
                Properties.Settings.Default.Save(); // 確保變更被保存
            }
        }

        private void AddTo二手最近看過(int 二手商品ID)
        {
            // 检查 _最近看過 是否为 null，如果是，则初始化它
            if (Properties.Settings.Default._二手最近看過 == null)
            {
                Properties.Settings.Default._二手最近看過 = new System.Collections.Specialized.StringCollection();
            }

            // 将商品ID转换为字符串，因为StringCollection存储的是字符串
            string idStr = 二手商品ID.ToString();

            // 如果该ID还不在列表中，则添加它
            if (!Properties.Settings.Default._二手最近看過.Contains(idStr))
            {
                Properties.Settings.Default._二手最近看過.Add(idStr);
                Properties.Settings.Default.Save(); // 记得保存更改
            }
        }

        private void Form二手商品詳情_Load(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------

        private void btn二手商品詳情_上一筆_Click(object sender, EventArgs e)
        {
            if (當前商品索引 > 0) // 確保不是第一個商品
            {
                當前商品索引--;
                Set二手商品信息(商品ID列表[當前商品索引]); // 更新商品詳情
                更新二手最近看過(商品ID列表[當前商品索引]); // 添加到最近看過
                AddTo二手最近看過(商品ID列表[當前商品索引]); // 将当前商品ID添加到最近看过
            }
        }

        private void btn二手商品詳情_下一筆_Click(object sender, EventArgs e)
        {
            if (當前商品索引 < 商品ID列表.Count - 1) // 確保不是最後一個商品
            {
                當前商品索引++;
                Set二手商品信息(商品ID列表[當前商品索引]); // 更新商品詳情
                更新二手最近看過(商品ID列表[當前商品索引]); // 添加到最近看過
                AddTo二手最近看過(商品ID列表[當前商品索引]); // 将当前商品ID添加到最近看过
            }
        }

        private void btn二手商品詳情_發佈_Click(object sender, EventArgs e)
        {
            // 如果已經是新商品模式，不需要再次初始化
            if (!is新商品)
            {
               this.Close(); // 關閉當前顯示商品詳情的表單
            }
            初始化為新商品();
            Form二手商品詳情 form二手商品詳情 = new Form二手商品詳情();
            form二手商品詳情.ShowDialog();
        }

        private void btn二手商品詳情_關閉_Click(object sender, EventArgs e)
        {
            清空欄位();
            this.Close();
        }

        private void btn二手商品詳情_送出_Click(object sender, EventArgs e)
        {
            // 確保用戶已經登入
            if (GlobalVar.使用者ID == 0)
            {
                using (Form對話框 對話框 = new Form對話框("請先登入"))
                {
                    對話框.ShowDialog();
                }
                return;
            }

            // 收集表單數據
            string shbuyorsale = cbox二手商品詳情_交易模式.SelectedItem.ToString();
            string shpname = txt二手商品詳情_商品名稱.Text;
            int shprice = Convert.ToInt32(txt二手商品詳情_商品金額.Text);
            string shcontact = txt二手商品詳情_聯絡資訊.Text;
            string shdesc = txt二手商品詳情_商品描述.Text;
            string shrigion = cbox二手商品詳情_區域.SelectedItem.ToString();
            string shnew = cbox二手商品詳情_商品新舊程度.SelectedItem.ToString();
            string shtradeway = cbox二手商品詳情_交易方式.SelectedItem.ToString();
            string shnego = cbox二手商品詳情_是否接受議價.SelectedItem.ToString();
            DateTime shdate = DateTime.Now;
            string shname = txt二手商品詳情_姓名.Text;
            int shcid = GlobalVar.使用者ID;

            string shpimage = 上傳圖片檔名;
            string fullPath = Path.Combine(GlobalVar.image_dir, "二手", shpimage);

            // 檢查是否需要保存圖片
            if (!File.Exists(fullPath) || shpimage != "預設照片.png")
            {
                pictureBox二手商品詳情.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
            }

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();

                string sql;
                if (is新商品)
                {
                    // 新增商品邏輯
                    sql = @"INSERT INTO secondhand (shbuyorsale, shpname, shprice, shcontact, shdesc, shrigion, shnew, shtradeway, shnego, shpimage, shdate, shname, shcid) 
            VALUES (@shbuyorsale, @shpname, @shprice, @shcontact, @shdesc, @shrigion, @shnew, @shtradeway, @shnego, @shpimage, @shdate, @shname, @shcid)";
                }
                else
                {
                    // 更新現有商品邏輯
                    sql = @"UPDATE secondhand SET shbuyorsale=@shbuyorsale, shpname=@shpname, shprice=@shprice, shcontact=@shcontact, shdesc=@shdesc, shrigion=@shrigion, shnew=@shnew, shtradeway=@shtradeway, shnego=@shnego, shpimage=@shpimage, shdate=@shdate, shname=@shname WHERE shcid=@shcid AND shid=@shid";
                }

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@shbuyorsale", shbuyorsale);
                    cmd.Parameters.AddWithValue("@shpname", shpname);
                    cmd.Parameters.AddWithValue("@shprice", shprice);
                    cmd.Parameters.AddWithValue("@shcontact", shcontact);
                    cmd.Parameters.AddWithValue("@shdesc", shdesc);
                    cmd.Parameters.AddWithValue("@shrigion", shrigion);
                    cmd.Parameters.AddWithValue("@shnew", shnew);
                    cmd.Parameters.AddWithValue("@shtradeway", shtradeway);
                    cmd.Parameters.AddWithValue("@shnego", shnego);
                    cmd.Parameters.AddWithValue("@shpimage", shpimage);
                    cmd.Parameters.AddWithValue("@shdate", shdate);
                    cmd.Parameters.AddWithValue("@shname", shname);
                    cmd.Parameters.AddWithValue("@shcid", shcid);

                    if (!is新商品)
                    {
                        cmd.Parameters.AddWithValue("@shid", 商品ID列表[當前商品索引]);
                    }

                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        string message = is新商品 ? "商品添加成功" : "商品更新成功";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                    }
                    else
                    {
                        using (Form對話框 對話框 = new Form對話框("操作失敗，請重試"))
                        {
                            對話框.ShowDialog();
                        }
                    }
                }
            }

            // 如果是添加新商品，可能需要清空表單或關閉窗口
            if (is新商品)
            {
                初始化為新商品();
            }
        }

        private void btn二手商品詳情_上傳圖片_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "選擇圖片";
            openFileDialog.Filter = "圖片文件(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 顯示選擇的圖片
                pictureBox二手商品詳情.Image = Image.FromFile(openFileDialog.FileName);

                // 获取图片文件名
                上傳圖片檔名 = Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void btn二手商品詳情_修改商品內容_Click(object sender, EventArgs e)
        {
            bool isEditable = !txt二手商品詳情_姓名.ReadOnly; // 根據當前狀態反轉
                                                      // 設置控件的ReadOnly或Visible屬性
            Set控件編輯狀態(isEditable);
        }

        private void Set控件編輯狀態(bool isEditable)
        {

            txt二手商品詳情_姓名.ReadOnly = isEditable;
            txt二手商品詳情_商品名稱.ReadOnly = isEditable;
            txt二手商品詳情_商品金額.ReadOnly = isEditable;
            txt二手商品詳情_聯絡資訊.ReadOnly = isEditable;
            txt二手商品詳情_商品描述.ReadOnly = isEditable;

            cbox二手商品詳情_交易模式.ReadOnly = isEditable;
            cbox二手商品詳情_區域.ReadOnly = isEditable;
            cbox二手商品詳情_商品新舊程度.ReadOnly = isEditable;
            cbox二手商品詳情_交易方式.ReadOnly = isEditable;
            cbox二手商品詳情_是否接受議價.ReadOnly = isEditable;

            btn二手商品詳情_上傳圖片.Visible = !isEditable;
        }

        private void btn二手商品詳情_刪除此商品_Click(object sender, EventArgs e)
        {
            using (Form對話框 對話框 = new Form對話框("確認刪除此商品?"))
            {
                if (對話框.ShowDialog() == DialogResult.OK)
                {
                    // 執行刪除操作
                    int shid = 商品ID列表[當前商品索引]; // 或者其他方式獲取當前商品ID
                    string sql = "DELETE FROM secondhand WHERE shid = @Shid";

                    using (SqlConnection connection = new SqlConnection(GlobalVar.strDBConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Shid", shid);
                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                using (Form對話框 成功對話框 = new Form對話框("商品已刪除"))
                                {
                                    成功對話框.ShowDialog();
                                }
                                // 進一步的UI處理，例如關閉窗口或刷新列表
                            }
                            else
                            {
                                using (Form對話框 失敗對話框 = new Form對話框("刪除失敗"))
                                {
                                    失敗對話框.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
