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
    public partial class Form接案商品詳情 : UIForm
    {
        private List<int> 商品ID列表;
        private int 當前商品索引;
        private HOME homeForm;
        private bool is新商品 = false; // 新增的成員變量用於標識是否為新商品
        private string 上傳圖片檔名 = "預設照片.png";
        bool is上傳圖片 = false;

        //無參數的構造函數，用於創建新商品
        public Form接案商品詳情()
        {
            InitializeComponent();
            初始化為新商品();
            Set使用者ID();
            btn接案商品詳情_發佈.Visible = false;
        }

        //有參數的構造函數，用於引用另一個Form的參數
        public Form接案商品詳情(List<int> 接案商品ID列表, int 當前索引, HOME home)
        {
            InitializeComponent();
            this.商品ID列表 = 接案商品ID列表;
            this.當前商品索引 = 當前索引;
            this.homeForm = home;

            // 窗體加載時設置商品信息
            this.Load += (sender, e) => Set接案商品信息(商品ID列表[當前商品索引]);

        }

        //-------------------------------------------------------------------

        private void 初始化為新商品()
        {
            is新商品 = true;
            btn接案商品詳情_上傳圖片.Visible = true;


            // 設置pictureBox為"上傳圖片.png"
            string uploadImagePath = System.IO.Path.Combine(GlobalVar.image_dir, "上傳圖片.png");
            if (System.IO.File.Exists(uploadImagePath))
            {
                pictureBox接案商品詳情.Image = Image.FromFile(uploadImagePath);
            }

            // 將所有控件設置為可編輯
            txt接案商品詳情_姓名.ReadOnly = false;
            txt接案商品詳情_商品名稱.ReadOnly = false;
            txt接案商品詳情_聯絡資訊.ReadOnly = false;
            txt接案商品詳情_商品描述.ReadOnly = false;

            cbox接案商品詳情_區域.ReadOnly = false;
            cbox接案商品詳情_年資.ReadOnly = false;
            cbox接案商品詳情_交易方式.ReadOnly = false;
            cbox接案商品詳情_交易模式.ReadOnly = false;
           
            btn接案商品詳情_刪除此商品.Visible = false;
            btn接案商品詳情_修改商品內容.Visible = false;

            // 清除所有控件的值
            txt接案商品詳情_姓名.Text = "";
            txt接案商品詳情_商品名稱.Text = "";
            txt接案商品詳情_聯絡資訊.Text = "";
            txt接案商品詳情_商品描述.Text = "";

            cbox接案商品詳情_區域.SelectedIndex = 0;
            cbox接案商品詳情_年資.SelectedIndex = 0;
            cbox接案商品詳情_交易方式.SelectedIndex = 0;
            cbox接案商品詳情_交易模式.SelectedIndex = 0;
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 檢查是否按下了左鍵或右鍵
            if (keyData == Keys.Left)
            {
                btn接案商品詳情_上一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }
            else if (keyData == Keys.Right)
            {
                btn接案商品詳情_下一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }

            return base.ProcessCmdKey(ref msg, keyData); // 調用基類的處理器
        }

        public void Set接案商品信息(int 接案商品ID)
        {

            btn接案商品詳情_上傳圖片.Visible = false;
            // 假設 connectionString 是您的數據庫連接字符串
            string connectionString = GlobalVar.strDBConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM expert WHERE epid = @接案商品ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@接案商品ID", 接案商品ID);
                Console.WriteLine($"正在加載商品ID: {接案商品ID}");


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        txt接案商品詳情_姓名.Text = reader["epname"].ToString();
                        txt接案商品詳情_商品名稱.Text = reader["epskill"].ToString();
                        txt接案商品詳情_聯絡資訊.Text = reader["epcontact"].ToString();
                        txt接案商品詳情_商品描述.Text = reader["epdesc"].ToString();

                        cbox接案商品詳情_區域.Text = reader["eprigion"].ToString();
                        cbox接案商品詳情_年資.Text = reader["epseniority"].ToString();
                        cbox接案商品詳情_交易方式.Text = reader["epserviceway"].ToString();
                        cbox接案商品詳情_交易模式.Text = reader["epbuyorsale"].ToString() ;


                        txt接案商品詳情_姓名.ReadOnly = true;
                        txt接案商品詳情_商品名稱.ReadOnly = true;
                        txt接案商品詳情_聯絡資訊.ReadOnly = true;
                        txt接案商品詳情_商品描述.ReadOnly = true;
                       
                        cbox接案商品詳情_區域.ReadOnly = true;
                        cbox接案商品詳情_年資.ReadOnly = true;
                        cbox接案商品詳情_交易方式.ReadOnly = true;
                        cbox接案商品詳情_交易模式.ReadOnly= true; 

                        int epcid = (int)reader["epcid"];

                        // 判斷當前登入的使用者是否為商品的擁有者
                        btn接案商品詳情_修改商品內容.Visible = (GlobalVar.使用者ID == epcid);
                        btn接案商品詳情_刪除此商品.Visible = (GlobalVar.使用者ID == epcid);


                        // 從資料庫獲取圖片檔名
                        string epimageFromDB = reader.IsDBNull(reader.GetOrdinal("epimage")) ? "" : reader["epimage"].ToString();
                        string imagePath;

                        if (!string.IsNullOrEmpty(epimageFromDB) && File.Exists(Path.Combine(GlobalVar.image_dir, "接案", epimageFromDB)))
                        {
                            imagePath = Path.Combine(GlobalVar.image_dir, "接案", epimageFromDB);
                        }
                        else
                        {
                            // 如果資料庫中沒有檔案名或檔案不存在，使用預設圖片
                            imagePath = Path.Combine(GlobalVar.image_dir, "接案", 上傳圖片檔名); // 上傳圖片檔名應設為預設圖片名稱
                        }

                        if (File.Exists(imagePath))
                        {
                            pictureBox接案商品詳情.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            // 如果沒有找到圖片，則使用預設圖片
                            pictureBox接案商品詳情.Image = Image.FromFile(Path.Combine(GlobalVar.image_dir, "接案", 上傳圖片檔名));
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

        private void 清空欄位()
        {
            // 清空標籤和文字框

            txt接案商品詳情_姓名.Text = "";
            txt接案商品詳情_商品名稱.Text = "";
            txt接案商品詳情_聯絡資訊.Text = "";
            txt接案商品詳情_商品描述.Text = "";

            // 清空ComboBox選擇
            cbox接案商品詳情_區域.SelectedIndex = -1;
            cbox接案商品詳情_年資.SelectedIndex = -1;
            cbox接案商品詳情_交易方式.SelectedIndex = -1;
            cbox接案商品詳情_交易模式.SelectedIndex = -1;
            

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

        private void 更新接案最近看過(int 接案商品ID)
        {
            if (!Properties.Settings.Default._接案最近看過.Contains(接案商品ID.ToString()))
            {
                Properties.Settings.Default._接案最近看過.Add(接案商品ID.ToString());
                Properties.Settings.Default.Save(); // 確保變更被保存
            }
        }

        private void AddTo接案最近看過(int 接案商品ID)
        {
            // 检查 _最近看過 是否为 null，如果是，则初始化它
            if (Properties.Settings.Default._接案最近看過 == null)
            {
                Properties.Settings.Default._接案最近看過 = new System.Collections.Specialized.StringCollection();
            }

            // 将商品ID转换为字符串，因为StringCollection存储的是字符串
            string idStr = 接案商品ID.ToString();

            // 如果该ID还不在列表中，则添加它
            if (!Properties.Settings.Default._接案最近看過.Contains(idStr))
            {
                Properties.Settings.Default._接案最近看過.Add(idStr);
                Properties.Settings.Default.Save(); // 记得保存更改
            }
        }

        private void Form接案商品詳情_Load(object sender, EventArgs e)
        {

        }

        void showDialog(string message)
        {
            using (Form對話框 對話框 = new Form對話框(message))
            {
                對話框.ShowDialog();
            }
        }
        //-------------------------------------------------------------------

        private void btn接案商品詳情_上一筆_Click(object sender, EventArgs e)
        {
            if (當前商品索引 > 0) // 確保不是第一個商品
            {
                當前商品索引--;
                Set接案商品信息(商品ID列表[當前商品索引]); // 更新商品詳情
                更新接案最近看過(商品ID列表[當前商品索引]); // 添加到最近看過
                AddTo接案最近看過(商品ID列表[當前商品索引]); // 将当前商品ID添加到最近看过
            }
        }

        private void btn接案商品詳情_下一筆_Click(object sender, EventArgs e)
        {
            if (當前商品索引 < 商品ID列表.Count - 1) // 確保不是最後一個商品
            {
                當前商品索引++;
                Set接案商品信息(商品ID列表[當前商品索引]); // 更新商品詳情
                更新接案最近看過(商品ID列表[當前商品索引]); // 添加到最近看過
                AddTo接案最近看過(商品ID列表[當前商品索引]); // 将当前商品ID添加到最近看过
            }
        }

        private void btn接案商品詳情_發佈_Click(object sender, EventArgs e)
        {
            // 如果已經是新商品模式，不需要再次初始化
            if (!is新商品)
            {
                this.Close(); // 關閉當前顯示商品詳情的表單
            }
            初始化為新商品();
            Form接案商品詳情 form接案商品詳情 = new Form接案商品詳情();
            form接案商品詳情.ShowDialog();
        }

        private void btn接案商品詳情_關閉_Click(object sender, EventArgs e)
        {
            清空欄位();
            this.Close();
        }

        private void btn接案商品詳情_送出_Click(object sender, EventArgs e)
        {
            if (GlobalVar.使用者ID == 0)
            {
                showDialog("請先登入");
                return;
            }

            // 收集表單數據
            string epname = txt接案商品詳情_姓名.Text;
            string epskill = txt接案商品詳情_商品名稱.Text;
            string epcontact = txt接案商品詳情_聯絡資訊.Text;
            string epdesc = txt接案商品詳情_商品描述.Text;
            string epbuyorsale = cbox接案商品詳情_交易模式.SelectedItem?.ToString() ?? "";
            string epserviceway = cbox接案商品詳情_交易方式.SelectedItem?.ToString() ?? "";
            string eprigion = cbox接案商品詳情_區域.SelectedItem?.ToString() ?? "";
            string epseniority = cbox接案商品詳情_年資.SelectedItem?.ToString() ?? "";
            DateTime epdate = DateTime.Now;
            int epcid = GlobalVar.使用者ID;

            // 根據is上傳圖片判斷是否需要更新圖片檔案名稱
            string epimage;
            if (is上傳圖片)
            {
                epimage = 上傳圖片檔名; // 新上傳的圖片
                string fullPath = Path.Combine(GlobalVar.image_dir, "接案", epimage);
                pictureBox接案商品詳情.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            else if (!is新商品)
            {
                // 對於更新操作，如果沒有上傳新圖片，則不更新epimage字段
                epimage = null; // 表示不更新圖片
            }
            else
            {
                // 對於新增操作但沒有上傳圖片，使用預設圖片
                epimage = "預設照片.png";
            }

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                string sql = is新商品 ?
                    "INSERT INTO expert (epbuyorsale, epskill, epcontact, epdesc, eprigion, epseniority, epserviceway, epimage, epdate, epname, epcid) VALUES (@epbuyorsale, @epskill, @epcontact, @epdesc, @eprigion, @epseniority, @epserviceway, @epimage, @epdate, @epname, @epcid)" :
                    "UPDATE expert SET epbuyorsale=@epbuyorsale, epskill=@epskill, epcontact=@epcontact, epdesc=@epdesc, eprigion=@eprigion, epseniority=@epseniority, epserviceway=@epserviceway, epdate=@epdate, epname=@epname" +
                    (epimage != null ? ", epimage=@epimage" : "") + // 只有當epimage不為null時才更新圖片
                    " WHERE epcid=@epcid AND epid=@epid";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // 添加參數
                    cmd.Parameters.AddWithValue("@epbuyorsale", epbuyorsale);
                    cmd.Parameters.AddWithValue("@epskill", epskill);
                    cmd.Parameters.AddWithValue("@epcontact", epcontact);
                    cmd.Parameters.AddWithValue("@epdesc", epdesc);
                    cmd.Parameters.AddWithValue("@eprigion", eprigion);
                    cmd.Parameters.AddWithValue("@epseniority", epseniority);
                    cmd.Parameters.AddWithValue("@epserviceway", epserviceway);
                    if (epimage != null || is新商品) cmd.Parameters.AddWithValue("@epimage", epimage); // 只有當epimage不為null或是新商品時才設置圖片參數
                    cmd.Parameters.AddWithValue("@epdate", epdate);
                    cmd.Parameters.AddWithValue("@epname", epname);
                    cmd.Parameters.AddWithValue("@epcid", epcid);
                    if (!is新商品) cmd.Parameters.AddWithValue("@epid", 商品ID列表[當前商品索引]);

                    int affectedRows = cmd.ExecuteNonQuery();
                    showDialog(affectedRows > 0 ? (is新商品 ? "商品添加成功" : "商品更新成功") : "操作失敗，請重試");

                }
            }

            if (is新商品) 初始化為新商品(); // 清空表單或進行其他後續操作
            this.Close();
        }

        private void btn接案商品詳情_上傳圖片_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "選擇圖片";
            openFileDialog.Filter = "圖片文件(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 顯示選擇的圖片
                pictureBox接案商品詳情.Image = Image.FromFile(openFileDialog.FileName);

                // 获取图片文件名
                上傳圖片檔名 = Path.GetFileName(openFileDialog.FileName);
                is上傳圖片 = true;
            }
        }

        private void btn接案商品詳情_修改商品內容_Click(object sender, EventArgs e)
        {
            bool isEditable = !txt接案商品詳情_姓名.ReadOnly; // 根據當前狀態反轉
                                                      // 設置控件的ReadOnly或Visible屬性
            Set控件編輯狀態(isEditable);
        }

        private void Set控件編輯狀態(bool isEditable)
        {

            txt接案商品詳情_姓名.ReadOnly = isEditable;
            txt接案商品詳情_商品名稱.ReadOnly = isEditable;
            txt接案商品詳情_聯絡資訊.ReadOnly = isEditable;
            txt接案商品詳情_商品描述.ReadOnly = isEditable;

            cbox接案商品詳情_交易模式.ReadOnly = isEditable;
            cbox接案商品詳情_區域.ReadOnly = isEditable;
            cbox接案商品詳情_年資.ReadOnly = isEditable;
            cbox接案商品詳情_交易方式.ReadOnly = isEditable;

            btn接案商品詳情_上傳圖片.Visible = !isEditable;
        }


        private void btn接案商品詳情_刪除此商品_Click(object sender, EventArgs e)
        {
            using (Form對話框 對話框 = new Form對話框("確認刪除此商品?"))
            {
                if (對話框.ShowDialog() == DialogResult.OK)
                {
                    // 執行刪除操作
                    int epid = 商品ID列表[當前商品索引]; // 或者其他方式獲取當前商品ID
                    string sql = "DELETE FROM expert WHERE epid = @epid";

                    using (SqlConnection connection = new SqlConnection(GlobalVar.strDBConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@epid", epid);
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
