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
    public partial class Form商品管理 : UIForm
    {
        private DataTable allProducts = new DataTable();
        private int currentProductIndex = -1; // 用於編輯模式
        private bool isNewProduct = false; // 標記是否為新增商品模式
        
        private string 上傳圖片檔名 = "上傳圖片.png";

        // 在Form商品管理中添加
        public event EventHandler ProductChanged;

        protected virtual void OnProductChanged()
        {
            ProductChanged?.Invoke(this, EventArgs.Empty);
        }


        public Form商品管理()
        {
            InitializeComponent();
            isNewProduct = true; // 設置為新增商品模式
            LoadAllProducts(); // 載入所有商品資料
        }


        // 有參數構造函數，改用pid作為參數
        public Form商品管理(string pid)
        {
            InitializeComponent();
            LoadAllProducts(); // 載入所有商品資料
            SetCurrentProductByPid(pid);
        }


        private void Form商品管理_Load(object sender, EventArgs e)
        {
            if (!isNewProduct && currentProductIndex >= 0)
            {
                UpdateProductInfo(currentProductIndex);
            }
            else if (isNewProduct)
            {
                PrepareNewProductUI();
            }
        }

        private void SetCurrentProductByPid(string pid)
        {
            // 嘗試將pid轉換為整數
            if (int.TryParse(pid, out int pidInt))
            {
                var productRow = allProducts.AsEnumerable().FirstOrDefault(row => row.Field<int>("pid") == pidInt);
                if (productRow != null)
                {
                    currentProductIndex = allProducts.Rows.IndexOf(productRow);
                    UpdateProductInfo(currentProductIndex);
                }
            }
            else
            {
                // pid轉換失敗的處理，例如顯示錯誤訊息
                //MessageBox.Show("商品編號格式不正確。");
                string message = "商品編號格式不正確。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }



        private void LoadAllProducts()
        {
            string query = "SELECT pid, pname, pmode, price, pdesc, ptype, pimage FROM products ORDER BY pid";
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                try
                {
                    con.Open();
                    allProducts.Clear();
                    adapter.Fill(allProducts);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("數據加載失敗: " + ex.Message);
                    string message = "數據加載失敗: " + ex.Message;
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                }
            }
        }

        private void UpdateProductInfo(int index)
        {
            if (index >= 0 && index < allProducts.Rows.Count)
            {
                DataRow product = allProducts.Rows[index];
                txt商品管理_商品編號.Text = product["pid"].ToString();
                txt商品管理_商品名稱.Text = product["pname"].ToString();
                txt商品管理_銷售模式.Text = product["pmode"].ToString();
                txt商品管理_商品金額.Text = product["price"].ToString();
                txt商品管理_商品類型.Text = product["ptype"].ToString();
                txt商品管理_商品描述.Text = product["pdesc"].ToString();

                string imagePath = Path.Combine(GlobalVar.image_dir, product["pimage"].ToString());
                try
                {
                    pictureBox商品管理.Image = Image.FromFile(imagePath);
                }
                catch
                {
                    pictureBox商品管理.Image = null; // 處理圖片加載失敗的情況
                }
            }
        }

        private void PrepareNewProductUI()
        {
            // 清空所有欄位，準備新增商品
            txt商品管理_商品編號.Clear();
            txt商品管理_商品名稱.Clear();
            txt商品管理_銷售模式.Clear();
            txt商品管理_商品金額.Clear();
            txt商品管理_商品類型.Clear();
            txt商品管理_商品描述.Clear();
            pictureBox商品管理.Image = null; // 清除圖片顯示
            // 可以設定其他默認值或UI元素的狀態
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 檢查是否按下了左鍵或右鍵
            if (keyData == Keys.Left)
            {
                btn商品管理_上一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }
            else if (keyData == Keys.Right)
            {
                btn商品管理_下一筆.PerformClick();
                return true; // 表示已處理按鍵事件
            }

            return base.ProcessCmdKey(ref msg, keyData); // 調用基類的處理器
        }

        private void btn商品管理_上一筆_Click(object sender, EventArgs e)
        {
            if (currentProductIndex > 0)
            {
                currentProductIndex--;
                UpdateProductInfo(currentProductIndex);
            }
        }

        private void btn商品管理_下一筆_Click(object sender, EventArgs e)
        {
            if (currentProductIndex < allProducts.Rows.Count - 1)
            {
                currentProductIndex++;
                UpdateProductInfo(currentProductIndex);
            }
        }

        private void btn商品管理_刪除此商品_Click(object sender, EventArgs e)
        {
            // 顯示確認對話框
            using (Form對話框 對話框 = new Form對話框("確認刪除此商品?"))
            {
                if (對話框.ShowDialog() == DialogResult.OK)
                {
                    // 從文本框獲取商品ID並轉換為整數
                    int pid;
                    if (!int.TryParse(txt商品管理_商品編號.Text, out pid))
                    {
                        MessageBox.Show("商品編號格式不正確。");
                        return;
                    }

                    string sql = "DELETE FROM products WHERE pid = @Pid";

                    using (SqlConnection connection = new SqlConnection(GlobalVar.strDBConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Pid", pid);
                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                using (Form對話框 成功對話框 = new Form對話框("商品已刪除"))
                                {
                                    成功對話框.ShowDialog();
                                }
                                // 進一步的UI處理，例如關閉窗口或刷新列表
                                OnProductChanged(); // 觸發商品變更事件
                                this.Close(); // 可以選擇關閉當前窗口，或者清空表單等
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


        private void btn商品管理_送出_Click(object sender, EventArgs e)
        {
            // 定義SQL語句
            string insertSql = @"INSERT INTO products (pname, pmode, price, ptype, pdesc, pimage) 
     VALUES (@pname, @pmode, @price, @ptype, @pdesc, @pimage)";
            string updateSqlWithoutImage = @"UPDATE products 
     SET pname = @pname, pmode = @pmode, price = @price, ptype = @ptype, pdesc = @pdesc 
     WHERE pid = @pid"; // 不包含pimage更新
            string updateSqlWithImage = @"UPDATE products 
     SET pname = @pname, pmode = @pmode, price = @price, ptype = @ptype, pdesc = @pdesc, pimage = @pimage 
     WHERE pid = @pid"; // 包含pimage更新

            // 從表單獲取值
            string pname = txt商品管理_商品名稱.Text;
            string pmode = txt商品管理_銷售模式.Text;
            string price = txt商品管理_商品金額.Text;
            string ptype = txt商品管理_商品類型.Text;
            string pdesc = txt商品管理_商品描述.Text;
            string pimage = pictureBox商品管理.Image != null ? 上傳圖片檔名 : null;

            // 判斷是否選擇了新圖片
            bool imageChanged = pictureBox商品管理.Image != null && !string.IsNullOrWhiteSpace(pimage) && !new[] { "預設照片.png", "上傳圖片.png" }.Contains(pimage);

            // 圖片處理
            if (imageChanged && !string.IsNullOrWhiteSpace(GlobalVar.image_dir))
            {
                if (!Directory.Exists(GlobalVar.image_dir))
                {
                    Directory.CreateDirectory(GlobalVar.image_dir);
                }
                string imagePath = Path.Combine(GlobalVar.image_dir, pimage);
                using (var saveImage = new Bitmap(pictureBox商品管理.Image))
                {
                    saveImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            using (SqlConnection connection = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                connection.Open();
                SqlCommand command;
                if (string.IsNullOrWhiteSpace(txt商品管理_商品編號.Text))
                {
                    // 新增商品
                    command = new SqlCommand(insertSql, connection);
                }
                else
                {
                    // 更新商品，根據是否更換圖片選擇適當的SQL語句
                    command = new SqlCommand(imageChanged ? updateSqlWithImage : updateSqlWithoutImage, connection);
                    command.Parameters.AddWithValue("@pid", int.Parse(txt商品管理_商品編號.Text));
                }

                // 添加共用參數
                command.Parameters.AddWithValue("@pname", pname);
                command.Parameters.AddWithValue("@pmode", pmode);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@ptype", ptype);
                command.Parameters.AddWithValue("@pdesc", pdesc);

                if (imageChanged)
                {
                    // 只有在圖片變更時才更新pimage
                    command.Parameters.AddWithValue("@pimage", pimage);
                }

                int result = command.ExecuteNonQuery();
                string message = result > 0 ? "操作成功" : "操作失敗";

                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }


                if (result > 0)
                {
                    OnProductChanged();
                    this.Close();
                }
            }
        }



        private void btn商品管理_關閉_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btn商品管理_上傳圖片_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "選擇圖片";
            openFileDialog.Filter = "圖片文件(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var tempImage = Image.FromFile(openFileDialog.FileName))
                {
                    
                    pictureBox商品管理.Image = new Bitmap(tempImage);
                    上傳圖片檔名 = Path.GetFileName(openFileDialog.FileName);
                   

                }
            }
        }



    }
}
