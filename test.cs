private void listView餐廚設備二手平台_ItemActivate(object sender, EventArgs e)
{
    if (listView餐廚設備二手平台.SelectedItems.Count > 0)
    {
        var selectedItem = listView餐廚設備二手平台.SelectedItems[0];
        int 二手商品ID = Convert.ToInt32(selectedItem.Tag); // 假設您已將商品ID存儲在Tag屬性中

        // 檢查 _最近看過 集合是否為 null，然後添加二手商品ID到 _最近看過
        if (Properties.Settings.Default._二手最近看過 == null)
        {
            Properties.Settings.Default._二手最近看過 = new System.Collections.Specialized.StringCollection();
        }
        if (!Properties.Settings.Default._二手最近看過.Contains(二手商品ID.ToString()))
        {
            Properties.Settings.Default._二手最近看過.Add(二手商品ID.ToString());
            Properties.Settings.Default.Save(); // 保存设置
        }

        // 更新點擊紀錄，假設有一個相應的方法
        二手更新點擊紀錄(二手商品ID);

        // 假設dictListId["技術轉移保證班"]存儲了當前分頁的所有商品ID
        List<int> 二手商品ID列表 = new List<int>();
        int 當前索引 = 二手商品ID列表.IndexOf(二手商品ID);

        // 創建Form商品詳情的實例並傳遞商品ID列表和當前索引
        Form發佈商品 form二手商品詳情 = new Form發佈商品(二手商品ID列表, 當前索引, this);
        form商品詳情.ShowDialog();
    }
}
void 二手更新點擊紀錄(int 商品ID)
{
    // 將商品ID轉換為字串形式
    string 二手商品ID字串 = 二手商品ID.ToString();

    // 檢查該商品ID是否已存在於紀錄中
    if (Properties.Settings.Default._二手最近看過.Contains(二手商品ID字串))
    {
        // 如果已存在，先從紀錄中移除
        Properties.Settings.Default._二手最近看過.Remove(二手商品ID字串);
    }

    // 然後將該商品ID重新添加到紀錄的開頭
    Properties.Settings.Default._二手最近看過.Insert(0, 二手商品ID字串);

    // 儲存變更
    Properties.Settings.Default.Save();
}

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
    public partial class Form商品詳情 : UIForm
    {
        private List<int> 商品列表ID;
        private int 當前商品索引;
        private HOME homeForm;

        public Form商品詳情(List<int> 商品ID列表, int 當前索引,HOME home)
        {
            InitializeComponent();
            this.商品列表ID = 商品ID列表;
            this.當前商品索引 = 當前索引;
            this.homeForm = home;

            // 窗體加載時設置商品信息
            this.Load += (sender, e) => Set商品信息(商品列表ID[當前商品索引]);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 檢查是否按下了左鍵或右鍵
            if (keyData == Keys.Left)
            {
                btn商品詳情_上一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }
            else if (keyData == Keys.Right)
            {
                btn商品詳情_下一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }

            return base.ProcessCmdKey(ref msg, keyData); // 調用基類的處理器
        }

        public void Set商品信息(int 商品ID)
        {
            // 假設 connectionString 是您的數據庫連接字符串
            string connectionString = GlobalVar.strDBConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT pname, pdesc, price, pimage FROM products WHERE pid = @商品ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@商品ID", 商品ID);
                Console.WriteLine($"正在加載商品ID: {商品ID}");
              

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txt商品詳情_商品名稱.Text = reader["pname"].ToString();
                        txt商品詳情_商品描述.Text = reader["pdesc"].ToString();
                        txt商品詳情_商品金額.Text = reader["price"].ToString();
                        txt商品詳情_商品編號.Text = 商品ID.ToString();
                        txt商品詳情_商品名稱.ReadOnly = true;
                        txt商品詳情_商品描述.ReadOnly = true;
                        txt商品詳情_商品金額.ReadOnly = true;
                        txt商品詳情_商品編號.ReadOnly = true;

                        // 假設商品圖片存儲的是圖片路徑
                        string imagePath = $"{GlobalVar.image_dir}\\"+reader["pimage"].ToString();
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            pictureBox商品詳情.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            MessageBox.Show("找不到照片，請檢查圖檔路徑!");
                        }
                    }
                    else
                    {
                        // 處理商品ID無效的情況
                        MessageBox.Show("無法找到指定的商品詳情。");
                    }
                }
            }
        }

        private void Form商品詳情_Load(object sender, EventArgs e)
        {

        }

        private void btn商品詳情_上一筆_Click(object sender, EventArgs e)
        {
            if (當前商品索引 > 0) // 確保不是第一個商品
            {
                當前商品索引--;
                Set商品信息(商品列表ID[當前商品索引]); // 更新商品詳情
                更新最近看過(商品列表ID[當前商品索引]); // 添加到最近看過
                AddTo最近看過(商品列表ID[當前商品索引]); // 将当前商品ID添加到最近看过
            }
        }

        private void btn商品詳情_下一筆_Click(object sender, EventArgs e)
        {
            if (當前商品索引 < 商品列表ID.Count - 1) // 確保不是最後一個商品
            {
                當前商品索引++;
                Set商品信息(商品列表ID[當前商品索引]); // 更新商品詳情
                更新最近看過(商品列表ID[當前商品索引]); // 添加到最近看過
                AddTo最近看過(商品列表ID[當前商品索引]); // 将当前商品ID添加到最近看过
            }
        }

        private void 更新最近看過(int 商品ID)
        {
            if (!Properties.Settings.Default._最近看過.Contains(商品ID.ToString()))
            {
                Properties.Settings.Default._最近看過.Add(商品ID.ToString());
                Properties.Settings.Default.Save(); // 確保變更被保存
            }
        }

        private void AddTo最近看過(int 商品ID)
        {
            // 检查 _最近看過 是否为 null，如果是，则初始化它
            if (Properties.Settings.Default._最近看過 == null)
            {
                Properties.Settings.Default._最近看過 = new System.Collections.Specialized.StringCollection();
            }

            // 将商品ID转换为字符串，因为StringCollection存储的是字符串
            string idStr = 商品ID.ToString();

            // 如果该ID还不在列表中，则添加它
            if (!Properties.Settings.Default._最近看過.Contains(idStr))
            {
                Properties.Settings.Default._最近看過.Add(idStr);
                Properties.Settings.Default.Save(); // 记得保存更改
            }
        }

        private void btn商品詳情_關閉_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn商品詳情_加入購物車_Click(object sender, EventArgs e)
        {
            // 假設已經有一個用於選擇數量的NumericUpDown控件，名為numUpDown數量
            int 數量 = Convert.ToInt32(NumUD商品詳情_加入購物車.Value);
            if (數量 <= 0 || 數量 > 500)
            {
                MessageBox.Show("請輸入有效的購買數量。");
                return;
            }
                                                                                                        
            int 商品ID = 商品列表ID[當前商品索引]; // 使用當前商品索引獲取商品ID
            int 購買者ID = GlobalVar.使用者ID; // 假設GlobalVar.使用者ID存儲了當前登入使用者的ID
            DateTime 加入時間 = DateTime.Now;

            string 插入語句 = "INSERT INTO cart (tobuypid, tobuyquantity, tobuycid, tobuytime) VALUES (@商品ID, @數量, @購買者ID, @加入時間)";

            using (SqlConnection conn = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(插入語句, conn))
                {
                    cmd.Parameters.AddWithValue("@商品ID", 商品ID);
                    cmd.Parameters.AddWithValue("@數量", 數量);
                    cmd.Parameters.AddWithValue("@購買者ID", 購買者ID);
                    cmd.Parameters.AddWithValue("@加入時間", 加入時間);
                 
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string message = $"成功加入購物車！";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("成功加入購物車！");
                    }
                    else
                    {
                        string message = $"加入購物車失敗。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("加入購物車失敗。");
                    }
                    
                }
            }
            // 調用 HOME 表單的更新購物車方法
            homeForm.更新購物車();
            
        }
        

    }
}


void 讀取二手商品資料庫(string ptype)
{
    var listId = dictListId[ptype];
    var list商品名稱 = dictList商品名稱[ptype];
    var list商品價格 = dictList商品價格[ptype];
    var list銷售模式 = dictList銷售模式[ptype];
    var imageList = dictImageList[ptype];

    // 清除之前的資料
    listId.Clear();
    list商品名稱.Clear();
    list商品價格.Clear();
    list銷售模式.Clear();
    imageList.Images.Clear();


    SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
    con.Open();
    string strSQL = $"SELECT top 200* FROM products WHERE ptype = '{ptype}' ORDER BY pname ";
    SqlCommand cmd = new SqlCommand(strSQL, con);
    SqlDataReader reader = cmd.ExecuteReader();

    int count = 0;

    while (reader.Read())
    {
        listId.Add((int)reader["pid"]);
        list商品名稱.Add((string)reader["pname"]);
        list商品價格.Add((int)reader["price"]);
        list銷售模式.Add((string)reader["pmode"]);
        string image_name = (string)reader["pimage"];
        string 完整圖檔路徑 = $"{GlobalVar.image_dir}\\{image_name}";
        //  2個反斜線等於輸出1個斜線

        //Image img商品圖檔 = Image.FromFile(完整圖檔路徑);

        System.IO.FileStream fs = System.IO.File.OpenRead(完整圖檔路徑);
        Image img商品圖檔 = Image.FromStream(fs);
        fs.Close();

        imageList.Images.Add(img商品圖檔);
        count++;
    }
    reader.Close();
    con.Close();
    Console.WriteLine($"讀取{count}筆資料");
}