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
    public partial class Form結帳畫面 : UIForm
    {
        private DataTable 購物車資料;
        // 在Form結帳畫面類別中添加

        private HOME homeForm; // 主表單參考
        
        public Form結帳畫面(DataTable 購物車資料,HOME homeForm) 
        {
            InitializeComponent();
            this.購物車資料 = 購物車資料;
            this.homeForm = homeForm; // 保存主表單參考
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form結帳畫面_FormClosed);
        }

        private void Form結帳畫面_Load(object sender, EventArgs e)
        {
            
            int 暫存總金額 = GlobalVar.結帳總金額;
            lbl結帳畫面_結帳總金額.Text =暫存總金額.ToString();
            lbl結帳畫面_付款方式.Text = GlobalVar.付款方式;

            // 假設有一個標籤lbl結帳畫面_付款方式用來顯示付款方式
            if (lbl結帳畫面_付款方式.Text == "匯款")
            {
                Form匯款資訊 form匯款資訊 = new Form匯款資訊();
                form匯款資訊.ShowDialog(); // 顯示匯款資訊窗體
            }

            // 在這裡將購物車資料設置到DataGridView結帳中
            DataGridView結帳畫面.DataSource = 購物車資料;

            // 手動調整欄位寬度
            if (DataGridView結帳畫面.Columns["編號"] != null)
                DataGridView結帳畫面.Columns["編號"].Width = 50; // 例如，設置"編號"欄位的寬度為50

            if (DataGridView結帳畫面.Columns["商品名稱"] != null)
                DataGridView結帳畫面.Columns["商品名稱"].Width = 245; // 設置"商品名稱"欄位的寬度為150

            if (DataGridView結帳畫面.Columns["商品規格"] != null)
                DataGridView結帳畫面.Columns["商品規格"].Width = 300; // 設置"商品規格"欄位的寬度為100

            if (DataGridView結帳畫面.Columns["單價"] != null)
                DataGridView結帳畫面.Columns["單價"].Width = 110; // 設置"商品規格"欄位的寬度為100

            if (DataGridView結帳畫面.Columns["數量"] != null)
                DataGridView結帳畫面.Columns["數量"].Width = 70; // 設置"商品規格"欄位的寬度為100

            if (DataGridView結帳畫面.Columns["小計"] != null)
                DataGridView結帳畫面.Columns["小計"].Width = 130; // 設置"商品規格"欄位的寬度為100

        }

        void showDialog(string message)
        {
            using (Form對話框 對話框 = new Form對話框(message))
            {
                對話框.ShowDialog();
            }
        }
        private void btn結帳畫面_送出_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVar.strDBConnectionString))
                {
                    conn.Open();

                    // 插入orders表
                    string insertOrderSql = @"
                    INSERT INTO orders(odesc, ocustomerid, odate, ototal, orderpayway, ostatus)
                    VALUES (@odesc, @ocustomerid, @odate, @ototal, @orderpayway, @ostatus);
                    SELECT SCOPE_IDENTITY();"; // 獲取剛插入記錄的ID

                    SqlCommand cmdOrder = new SqlCommand(insertOrderSql, conn);
                    cmdOrder.Parameters.AddWithValue("@odesc", txt結帳畫面_備註.Text);
                    cmdOrder.Parameters.AddWithValue("@ocustomerid", GlobalVar.使用者ID);
                    cmdOrder.Parameters.AddWithValue("@odate", DateTime.Now);
                    cmdOrder.Parameters.AddWithValue("@ototal", Convert.ToInt32(lbl結帳畫面_結帳總金額.Text));
                    cmdOrder.Parameters.AddWithValue("@orderpayway", lbl結帳畫面_付款方式.Text);
                    cmdOrder.Parameters.AddWithValue("@ostatus", "未出貨");

                    // 執行命令並獲取訂單ID
                    int orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());

                    // 插入ordercontain表
                    foreach (DataGridViewRow row in DataGridView結帳畫面.Rows)
                    {
                        string insertOrderContainSql = @"
                        INSERT INTO ordercontain(ocoid, ocpid, ocquantity)
                        VALUES (@orderid, @ocpid, @ocquantity);";

                        SqlCommand cmdOrderContain = new SqlCommand(insertOrderContainSql, conn);
                        cmdOrderContain.Parameters.AddWithValue("@orderid", orderId);
                        cmdOrderContain.Parameters.AddWithValue("@ocpid", Convert.ToInt32(row.Cells["編號"].Value)); // 假設有一個叫做"商品ID"的列
                        cmdOrderContain.Parameters.AddWithValue("@ocquantity", Convert.ToInt32(row.Cells["數量"].Value)); // 假設有一個叫做"數量"的列

                        cmdOrderContain.ExecuteNonQuery();
                    }

                    showDialog("訂單提交成功！");

                    // 刪除購物車中已購買的項目
                    List<int> purchasedItemIds = 購物車資料.AsEnumerable()
                                                .Select(row => row.Field<int>("編號"))
                                                .ToList();

                    homeForm.DeleteSelectedCartItems(purchasedItemIds); // 使用已有的方法刪除已購買項目

                }
            }
            catch (Exception ex)
            {
                showDialog($"發生錯誤：{ex.Message}");
            }
            finally
            {
                this.Close(); // 或者其他適合的處理方式
            }
        }

        private void btn結帳畫面_關閉_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn結帳畫面_匯款資訊_Click(object sender, EventArgs e)
        {
            Form匯款資訊 form匯款資訊 = new Form匯款資訊();
            form匯款資訊.ShowDialog();
        }

        private void DataGridView結帳畫面_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Set購物車資料(DataTable dataTable)
        {
            DataGridView結帳畫面.DataSource = dataTable;
        }

        private void txt結帳畫面_備註_MouseClick(object sender, EventArgs e)
        {
            txt結帳畫面_備註.Clear();
        }

        private void Form結帳畫面_FormClosed(object sender, FormClosedEventArgs e)
        {
            homeForm.更新購物車(); // 呼叫更新購物車的方法，重新載入購物車數據
        }
    }
}
