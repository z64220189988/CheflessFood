using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using Sunny.UI.Win32;

namespace 個人專題Chefless_Food__Decentralization
{
    public partial class HOME : UIForm
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

        Dictionary<string, List<int>> dictListId = new Dictionary<string, List<int>>();
        Dictionary<string, List<string>> dictList商品名稱 = new Dictionary<string, List<string>>();
        Dictionary<string, List<int>> dictList商品價格 = new Dictionary<string, List<int>>();
        Dictionary<string, List<string>> dictList銷售模式 = new Dictionary<string, List<string>>();
        Dictionary<string, ImageList> dictImageList = new Dictionary<string, ImageList>();

        List<int> list二手Id = new List<int>();
        List<string> list二手商品地區 = new List<string>();
        List<string> list二手商品新舊 = new List<string>();
        List<string> list二手商品交易方式 = new List<string>();
        List<string> list二手是否接受議價 = new List<string>();
        List<string> list二手姓名 = new List<string>();
        List<string> list二手商品名稱 = new List<string>();
        List<int> list二手商品價格 = new List<int>();
        List<string> list二手聯絡資訊 = new List<string>();
        List<string> list二手商品描述 = new List<string>();
        List<string> list二手買或賣 = new List<string>();

        List<int> list接案Id = new List<int>();
        List<string> list接案商品地區 = new List<string>();
        List<string> list接案年資 = new List<string>();
        List<string> list接案商品交易方式 = new List<string>();
        List<string> list接案姓名 = new List<string>();
        List<string> list接案商品名稱 = new List<string>();
        List<string> list接案聯絡資訊 = new List<string>();
        List<string> list接案商品描述 = new List<string>();
        List<string> list接案商品交易模式 = new List<string>();


        string ptype = string.Empty;

        private bool is全選 = true;

        private List<FileStream> fileStreams = new List<FileStream>();

        private string currentSortOrder = "DESC";

        public HOME()
        {
            InitializeComponent();
            InitializeTabHoverEvents();
            TabControl主選單.DrawMode = TabDrawMode.OwnerDrawFixed; // 设置为自定义绘制
            TabControl主選單.DrawItem += TabControl主選單_DrawItem;
            TabControl主選單.MouseMove += TabControl主選單_MouseMove;
            timer1.Tick += new EventHandler(timer1_Tick); // 添加事件處理器
        }

        //==============  滑鼠懸停放大  ===================================================

        private void InitializeTabHoverEvents()
        {
            // 为每个选项卡页添加MouseEnter和MouseLeave事件
            foreach (TabPage tabPage in TabControl主選單.TabPages)
            {
                tabPage.MouseEnter += TabControl主選單_MouseEnter;
                tabPage.MouseLeave += TabControl主選單_MouseLeave;
            }
        }

        private void TabControl主選單_MouseEnter(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl != null)
            {
                // 改变TabControl的字体大小以实现所有标签的"放大"效果
                tabControl.Font = new Font(tabControl.Font.FontFamily, tabControl.Font.Size + 1, FontStyle.Regular);
            }
        }

        private void TabControl主選單_MouseLeave(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl != null)
            {
                // 恢复TabControl的字体大小
                tabControl.Font = new Font(tabControl.Font.FontFamily, tabControl.Font.Size - 1, FontStyle.Regular);
            }
        }

        private void TabControl主選單_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = TabControl主選單.TabPages[e.Index];
            using (Brush br = new SolidBrush(e.Index == hoveredTab ? Color.Red : Color.Black))
            {
                e.Graphics.DrawString(page.Text, e.Font, br, e.Bounds.X + 3, e.Bounds.Y + 3);
            }
        }

        private int hoveredTab = -1; // 没有标签被鼠标悬停时为-1

        private void TabControl主選單_MouseMove(object sender, MouseEventArgs e)
        {
            int newHoveredTab = -1;
            for (int i = 0; i < TabControl主選單.TabCount; i++)
            {
                if (TabControl主選單.GetTabRect(i).Contains(e.Location))
                {
                    newHoveredTab = i;
                    break;
                }
            }

            if (newHoveredTab != hoveredTab) // 只有在不同的标签上移动时才重绘
            {
                hoveredTab = newHoveredTab;
                TabControl主選單.Invalidate(); // 强制重绘控件
            }
        }

        //==========================================================================
        private void HOME_Activated(object sender, EventArgs e)
        {
            //重新載入二手資料();
            //重新載入接案資料();
            FillOrdersDataGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             Form使用者登入 formLogin = new Form使用者登入();
             formLogin.ShowDialog();
             
            //--------------------------------------------------------

            UpdateUIBasedOnUserPermission();
            //--------------------------------------------------------
            scsb.DataSource = @".";
            scsb.InitialCatalog = "chefless";
            scsb.IntegratedSecurity = true;//windows整合驗證
            scsb.Encrypt = false;
            GlobalVar.strDBConnectionString = scsb.ConnectionString;
            //--------------------------------------------------------

            // 設定每個 ListView 的 LargeImageList 和 SmallImageList
            listView商用料理配方.LargeImageList = imageList商用料理配方;
            listView商用料理配料包.LargeImageList = imageList商品料理配料包;
            listView廚藝顧問出租.LargeImageList = imageList廚藝顧問出租;
            listView商用廚房設備規劃.LargeImageList = imageList商用廚房設備規劃;
            listView技術轉移保證班.LargeImageList = imageList技術轉移保證班;
            listView餐廚設備二手平台.LargeImageList = imageList餐廚設備二手平台;
            listView達人接案平台.LargeImageList = imageList達人接案平台;

            //--------------------------------------------------------

            lbl首頁_登入帳號.Text = $"帳號 : {GlobalVar.使用者帳號}";
            lbl首頁_使用者名稱.Text = $"姓名 : {GlobalVar.使用者名稱}";
            lbl首頁_管理身分組.Text = $"身分組 : {GlobalVar.身分組}";
            txt系統維護_圖檔路徑.Text = $"{GlobalVar.image_dir}";
            //--------------------------------------------------------

            cbox商用料理配方_排序選項.Items.AddRange(new object[] { "價格低到高", "價格高到低", "姓名筆劃多到少", "姓名筆劃少到多" });
            cbox商用料理配方_排序選項.SelectedIndex = 3;

            cbox商用料理配料包_排序選項.Items.AddRange(new object[] { "價格低到高", "價格高到低", "姓名筆劃多到少", "姓名筆劃少到多" });
            cbox商用料理配料包_排序選項.SelectedIndex = 3;

            cbox廚藝顧問出租_排序選項.Items.AddRange(new object[] { "價格低到高", "價格高到低", "姓名筆劃多到少", "姓名筆劃少到多" });
            cbox廚藝顧問出租_排序選項.SelectedIndex = 3;

            cbox商用廚房設備規劃_排序選項.Items.AddRange(new object[] { "價格低到高", "價格高到低", "姓名筆劃多到少", "姓名筆劃少到多" });
            cbox商用廚房設備規劃_排序選項.SelectedIndex = 3;

            cbox技術轉移保證班_排序選項.Items.AddRange(new object[] { "價格低到高", "價格高到低", "姓名筆劃多到少", "姓名筆劃少到多" });
            cbox技術轉移保證班_排序選項.SelectedIndex = 3;

            cbox購物車_選擇付款方式.Items.Add("現金");
            cbox購物車_選擇付款方式.Items.Add("匯款");
            cbox購物車_選擇付款方式.SelectedIndex = 0; // 預設選擇第一個

            LoadEmployeeEmails();

            //--------------------------------------------------------

            初始化資料結構();

            List<int> list二手id = new List<int>();

            ptype = "商用料理配方"; // 或根据需要设置为其他初始值
            TabControl主選單.SelectedIndex = 0; // 设置初始选中的分页
            TabControl主選單_SelectedIndexChanged(null, null); // 手动触发分页切换事件处理方法

            // 加載初始數據
            加載分頁數據(ptype);

            // 添加刪除按鈕的點擊事件
            DataGridView購物車.CellClick += DataGridView購物車_CellClick;

            Console.WriteLine("Form1_Load");

            timer1.Start(); // 啟動計時器

            GlobalVar.是否維護中 = IsNowInMaintenance();
            if (GlobalVar.身分組 == "客戶"&& GlobalVar.是否維護中 == true)
            {
              ShowDialog("目前系統維護中");
              Application.Exit();
            }
           
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            // 使用當前系統時間更新 lbl顯示時間 控件的 Text 屬性
            lbl顯示時間.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void 加載分頁數據(string ptype)
        {
            // 根據當前的排序選項和ptype來加載數據
            string 排序語句 = 獲取排序語句(cbox商用料理配方_排序選項.SelectedItem.ToString().Trim());
            讀取cbox並顯示資料(ptype, 排序語句);
        }

        private string 獲取排序語句(string 排序選項)
        {
            switch (排序選項)
            {
                case "價格低到高":
                    return "ORDER BY price ASC";
                case "價格高到低":
                    return "ORDER BY price DESC";
                case "姓名筆劃多到少":
                    return "ORDER BY pname DESC"; // 假設使用長度來近似筆劃數，具體實現可能需要調整
                case "姓名筆劃少到多":
                    return "ORDER BY pname ASC";
                default:
                    return "";
            }
        }

        

        //===============   首頁區  ============================

        private void UpdateUIBasedOnUserPermission()
        {
            var adminTab = TabControl橫標;

            if (adminTab != null)
            {

                if (GlobalVar.使用者權限 < 1000 && GlobalVar.使用者權限 > 0)
                {
                    adminTab.Visible = true;
                }
                else if (GlobalVar.使用者權限 == 0)
                {
                    adminTab.Visible = false;
                }
                else
                {
                    adminTab.Visible = false;
                }
            }

            // 更新其他UI元素，比如标签
            if (!GlobalVar.is登入成功)
            {
                lbl首頁_登入帳號.Text = "帳號 ";
                lbl首頁_使用者名稱.Text = "姓名  ";
                lbl首頁_管理身分組.Text = "身分組 ";
                UserSession.登出();
            }
            else
            {
                lbl首頁_登入帳號.Text = $"帳號 : {GlobalVar.使用者帳號}";
                lbl首頁_使用者名稱.Text = $"姓名 : {GlobalVar.使用者名稱}";
                lbl首頁_管理身分組.Text = $"身分組 : {GlobalVar.身分組}";
            }
        }

        private void btn首頁_登入_Click(object sender, EventArgs e)
        {
            // 檢查是否已經登入，如果已經登入，跳出訊息執行登出方法
            if (!string.IsNullOrEmpty(GlobalVar.使用者帳號) && !string.IsNullOrEmpty(GlobalVar.使用者密碼))
            {
                // 執行登出方法，清除相關的用戶資訊
                UserSession.登出();
                UpdateUIBasedOnUserPermission();

            }

            // 輸入登入資訊
            Form使用者登入 formUser = new Form使用者登入();
            formUser.ShowDialog();

            // 更新UI元素
            lbl首頁_登入帳號.Text = $"帳號 : {GlobalVar.使用者帳號}";
            lbl首頁_使用者名稱.Text = $"姓名 : {GlobalVar.使用者名稱}";
            lbl首頁_管理身分組.Text = $"身分組 : {GlobalVar.身分組}";
            UpdateUIBasedOnUserPermission();

            if (GlobalVar.身分組 == "客戶")
            {
                pnl會員資料修改.Visible = true;
            }
            else
            {
                pnl會員資料修改.Visible = false;
            }
        }

        private void btn首頁_登出_Click(object sender, EventArgs e)
        {
            UserSession.登出(); // 调用登出方法

            // 清除 _最近看過
            Properties.Settings.Default._最近看過.Clear();
            Properties.Settings.Default._二手最近看過.Clear();
            Properties.Settings.Default._接案最近看過.Clear();
            Properties.Settings.Default.Save(); // 儲存設定，確保更改被保存

            listView商用料理配方.Items.Clear();
            listView商用料理配料包.Items.Clear();
            listView廚藝顧問出租.Items.Clear();
            listView商用廚房設備規劃.Items.Clear();
            listView技術轉移保證班.Items.Clear();
            listView餐廚設備二手平台.Items.Clear();
            listView達人接案平台.Items.Clear();

            lbl首頁_登入帳號.Text = $"帳號 : {GlobalVar.使用者帳號}";
            lbl首頁_使用者名稱.Text = $"姓名 : {GlobalVar.使用者名稱}";
            lbl首頁_管理身分組.Text = $"身分組 : {GlobalVar.身分組}";

            UpdateUIBasedOnUserPermission(); // 用户登出后更新UI

            TabControl主選單.SelectedIndex = 0;
            listView商用料理配方.Items.Clear();

        }

        private void btn首頁_跳轉購物車_Click(object sender, EventArgs e)
        {
            // 使用TabControl的SelectedTab屬性來設定所選擇的TabPage為"購物車"
            TabControl主選單.SelectedTab = 購物車;
        }

        private void btn首頁_應用程式關閉_Click(object sender, EventArgs e)
        {
            // 檢查並清除 _最近看過
            if (Properties.Settings.Default._最近看過 != null)
            {
                Properties.Settings.Default._最近看過.Clear();
            }
            else
            {
                Properties.Settings.Default._最近看過 = new System.Collections.Specialized.StringCollection();
            }

            // 檢查並清除 _二手最近看過
            if (Properties.Settings.Default._二手最近看過 != null)
            {
                Properties.Settings.Default._二手最近看過.Clear();
            }
            else
            {
                Properties.Settings.Default._二手最近看過 = new System.Collections.Specialized.StringCollection();
            }

            // 檢查並清除 _接案最近看過
            if (Properties.Settings.Default._接案最近看過 != null)
            {
                Properties.Settings.Default._接案最近看過.Clear();
            }
            else
            {
                Properties.Settings.Default._接案最近看過 = new System.Collections.Specialized.StringCollection();
            }

            // 儲存變更
            Properties.Settings.Default.Save();

            // 關閉窗體
            Close();
        }

        //===============  首頁區  ==================================
        //
        //===============  共用方法  ==================================

        //-----------------------------------------------------------------------------------------------------------------

        private void TabControl主選單_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 根据选中的TabIndex来设置ptype
            string[] ptypes = { "商用料理配方", "商用料理配料包", "廚藝顧問出租", "商用廚房設備規劃", "技術轉移保證班" };
            int currentIndex = TabControl主選單.SelectedIndex;

            if (currentIndex < 0 || currentIndex >= ptypes.Length)
                return; // 如果索引超出范围，则直接返回

            ptype = ptypes[currentIndex];
            var currentListView = 當前listview(ptype); // 假设这个方法根据ptype返回对应的ListView
            var currentImageList = dictImageList[ptype]; // 从字典中获取对应的ImageList

            if (currentListView != null && currentImageList != null)
            {
                讀取商品資料庫(ptype); // 加载对应ptype的商品数据
                讀取二手商品資料庫();
                讀取接案商品資料庫();
                顯示listView(currentListView, currentImageList, dictListId[ptype], dictList商品名稱[ptype], dictList商品價格[ptype], dictList銷售模式[ptype]);
                顯示二手listView();
                顯示接案listView();
            }

            if (lbl首頁_管理身分組.Text.Contains("客戶"))
            {
                pnl會員資料修改.Visible = true;
            }

            FillCartDataGridView();
            FillOrdersDataGridView();
            FillProductsDataGridView();
            FillOrdersDataGridViewManage();
            SearchMembersAll();
            FillDataGridView打卡紀錄();
            lbl購物車_結帳總金額.Text = "0";
            lbl購買清單_訂單編號數值.Text = "";
            lbl購買清單_訂單總價數值.Text = "0";
        }

        // 顯示對話框
        private void ShowDialog(string message)
        {
            using (Form對話框 對話框 = new Form對話框(message))
            {
                對話框.ShowDialog();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------

        ListView 當前listview(string ptype)
        {
            switch (ptype)
            {
                case "商用料理配方":
                    return listView商用料理配方;
                case "商用料理配料包":
                    return listView商用料理配料包;
                case "廚藝顧問出租":
                    return listView廚藝顧問出租;
                case "商用廚房設備規劃":
                    return listView商用廚房設備規劃;
                case "技術轉移保證班":
                    return listView技術轉移保證班;
                default:
                    return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------

        void 初始化資料結構()
        {
            // 假設有以下ptype
            string[] ptypes = { "商用料理配方", "商用料理配料包", "廚藝顧問出租", "商用廚房設備規劃", "技術轉移保證班" };

            foreach (var ptype in ptypes)
            {
                dictListId[ptype] = new List<int>();
                dictList商品名稱[ptype] = new List<string>();
                dictList商品價格[ptype] = new List<int>();
                dictList銷售模式[ptype] = new List<string>();
                dictImageList[ptype] = new ImageList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------

        void 重新載入資料(string ptype)
        {
            // 根据ptype获取对应的ListView和ImageList
            ListView currentListView = 當前listview(ptype);
            ImageList currentImageList = dictImageList[ptype];

            // 确保找到了ListView和ImageList
            if (currentListView != null && currentImageList != null)
            {
                // 清除当前ListView和ImageList的内容
                currentListView.Items.Clear();
                currentImageList.Images.Clear();

                // 从数据库重新加载数据
                讀取商品資料庫(ptype);

                // 使用新的数据更新ListView
                顯示listView(currentListView, currentImageList, dictListId[ptype], dictList商品名稱[ptype], dictList商品價格[ptype], dictList銷售模式[ptype]);
            }
            else
            {
                // 如果未找到对应的ListView或ImageList，可以在这里处理错误或进行日志记录
                Console.WriteLine($"未找到与{ptype}对应的ListView或ImageList。");
            }
        }

        void 重新載入二手資料()
        {
            // 清除當前ListView和ImageList的內容
            listView餐廚設備二手平台.Items.Clear();
            imageList餐廚設備二手平台.Images.Clear();

            // 從數據庫重新加載二手商品資料
            讀取二手商品資料庫();

            // 使用新的數據更新ListView
            顯示二手listView();
        }

        void 重新載入接案資料()
        {
            // 清除當前ListView和ImageList的內容
            listView達人接案平台.Items.Clear();
            imageList達人接案平台.Images.Clear();

            // 從數據庫重新加載二手商品資料
            讀取接案商品資料庫();

            // 使用新的數據更新ListView
            顯示接案listView();
        }
        //------------------------------------------------------------

        private void HOME_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Username = GlobalVar.使用者帳號;
            Properties.Settings.Default.Password = GlobalVar.使用者密碼;
            Properties.Settings.Default.Save();
        }

        //------------------------------------------------------------

        void 讀取cbox並顯示資料(string ptype, string 排序語句)
        {
            List<int> listId = new List<int>();
            var list商品名稱 = new List<string>();
            var list商品價格 = new List<int>();
            var list銷售模式 = new List<string>();
            var imageList = new ImageList();
            imageList.ImageSize = new Size(100, 100); // 設定圖片大小

            // 清空當前ListView和ImageList中的項目和圖片
            var currentListView = 當前listview(ptype); // 根據實際情況選擇對應的ListView
            currentListView.Items.Clear();
            currentListView.LargeImageList = imageList;
            currentListView.SmallImageList = imageList;

            string strSQL = $"SELECT * FROM products WHERE ptype = '{ptype}' {排序語句}";
            string 預設圖片路徑 = Path.Combine(GlobalVar.image_dir, "預設圖片.png");

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                
                con.Open();
                using (SqlCommand cmd = new SqlCommand(strSQL, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int index = 0; // 用於追蹤圖片列表的索引
                        while (reader.Read())
                        {
                            listId.Add((int)reader["pid"]);
                            list商品名稱.Add((string)reader["pname"]);
                            list商品價格.Add((int)reader["price"]);
                            list銷售模式.Add((string)reader["pmode"]);

                            string image_name = (string)reader["pimage"];
                            string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, image_name);

                            try
                            {
                                using (var fs = new FileStream(完整圖檔路徑, FileMode.Open, FileAccess.Read))
                                {
                                    var img = Image.FromStream(fs);
                                    imageList.Images.Add(img);
                                }
                            }
                            catch
                            {
                                // 如果指定的圖片加載失敗，則使用預設圖片
                                if (File.Exists(預設圖片路徑))
                                {
                                    using (var fs = new FileStream(預設圖片路徑, FileMode.Open, FileAccess.Read))
                                    {
                                        var img = Image.FromStream(fs);
                                        imageList.Images.Add(img); // 將預設圖片加入到ImageList中
                                    }
                                }
                                else
                                {
                                    // 如果連預設圖片也不存在，可以在這裡處理錯誤或者加入一個空白圖片
                                }

                            }
                        }
                    }
                }
            }

            // 更新ListView
            for (int i = 0; i < listId.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i; // 圖片索引
                item.Text = $"{list商品名稱[i]}   $價格 : {list商品價格[i]}元     *銷售模式 : [{list銷售模式[i]}]";
                item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item.Tag = listId[i]; // 將商品ID存儲於Tag中
                currentListView.Items.Add(item);
            }
        }

        //------------------------------------------------------------

        void 讀取商品資料庫(string ptype)
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

        void 讀取二手商品資料庫()
        {
            var listId = list二手Id;
            var list商品地區 = list二手商品地區;
            var list商品新舊 = list二手商品新舊;
            var list商品交易方式 = list二手商品交易方式;
            var list是否接受議價 = list二手是否接受議價;
            var list姓名 = list二手姓名;
            var list商品名稱 = list二手商品名稱;
            var list商品價格 = list二手商品價格;
            var list聯絡資訊 = list二手聯絡資訊;
            var list商品描述 = list二手商品描述;
            var list買或賣 = list二手買或賣;


            var imageList = imageList餐廚設備二手平台;

            // 清除之前的資料
            list二手Id.Clear();
            list二手商品地區.Clear();
            list二手商品新舊.Clear();
            list二手商品交易方式.Clear();
            list二手是否接受議價.Clear();
            list二手姓名.Clear();
            list二手商品名稱.Clear();
            list二手商品價格.Clear();
            list二手聯絡資訊.Clear();
            list二手商品描述.Clear();
            list二手買或賣.Clear();

            imageList.Images.Clear();


            SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
            con.Open();
            string strSQL = $"SELECT top 200* FROM secondhand  ORDER BY shpname ";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;

            while (reader.Read())
            {
                listId.Add((int)reader["shid"]);
                list商品地區.Add((string)reader["shrigion"]);
                list商品新舊.Add((string)reader["shnew"]);
                list商品交易方式.Add((string)reader["shtradeway"]);
                list是否接受議價.Add((string)reader["shnego"]);
                list姓名.Add((string)reader["shname"]);
                list商品名稱.Add((string)reader["shpname"]);
                list商品價格.Add((int)reader["shprice"]);
                list聯絡資訊.Add((string)reader["shcontact"]);
                list商品描述.Add((string)reader["shdesc"]);
                list買或賣.Add((string)reader["shbuyorsale"]);

                string image_name = (string)reader["shpimage"];
                string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, "二手", image_name);
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

        void 讀取接案商品資料庫()
        {
            var listId = list接案Id;
            var list商品地區 = list接案商品地區;
            var list年資 = list接案年資;
            var list商品交易方式 = list接案商品交易方式;
            var list商品交易模式 = list接案商品交易模式;

            var list姓名 = list接案姓名;
            var list商品名稱 = list接案商品名稱;
            var list聯絡資訊 = list接案聯絡資訊;
            var list商品描述 = list接案商品描述;


            var imageList = imageList達人接案平台;

            // 清除之前的資料
            list接案Id.Clear();
            list接案商品地區.Clear();
            list接案年資.Clear();
            list接案商品交易方式.Clear();
            list接案商品交易模式.Clear();

            list接案姓名.Clear();
            list接案商品名稱.Clear();
            list接案聯絡資訊.Clear();
            list接案商品描述.Clear();

            imageList.Images.Clear();

            SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
            con.Open();
            string strSQL = $"SELECT top 200* FROM expert ORDER BY epskill ";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;

            while (reader.Read())
            {
                listId.Add((int)reader["epid"]);
                list商品地區.Add((string)reader["eprigion"]);
                list年資.Add((string)reader["epseniority"]);
                list商品交易方式.Add((string)reader["epserviceway"]);
                list商品交易模式.Add((string)reader["epbuyorsale"]);

                list姓名.Add((string)reader["epname"]);
                list商品名稱.Add((string)reader["epskill"]);
                list聯絡資訊.Add((string)reader["epcontact"]);
                list商品描述.Add((string)reader["epdesc"]);


                string image_name = (string)reader["epimage"];
                string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, "接案", image_name);
                //  2個反斜線等於輸出1個斜線

                //Image img商品圖檔 = Image.FromFile(完整圖檔路徑);

                FileStream fs = File.OpenRead(完整圖檔路徑);
                Image img商品圖檔 = Image.FromStream(fs);
                fs.Close();

                imageList.Images.Add(img商品圖檔);
                count++;
            }
            reader.Close();
            con.Close();
            Console.WriteLine($"讀取{count}筆資料");
        }

        //------------------------------------------------------------

        void 顯示listView(ListView listView, ImageList imageList, List<int> listId, List<string> list商品名稱, List<int> list商品價格, List<string> list銷售模式)
        {
            listView.Clear();
            listView.View = View.Tile;
            listView.Scrollable = true;
            listView.TileSize = new Size(1200, 105);
            //LargeIcon,Tile,List,SmailIcon可選
            imageList.ImageSize = new Size(100, 100);

            listView.LargeImageList = imageList;
            //LargeIcon,Tile

            listView.SmallImageList = imageList;
            //SmallIcon,List

            for (int i = 0; i < listId.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Text = $"{list商品名稱[i]}   $價格 : {list商品價格[i]}元     *銷售模式 : [{list銷售模式[i]}]";
                item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item.Tag = listId[i];
                listView.Items.Add(item); // 使用傳入的listView變量而不是硬編碼的名稱
            }
        }

        void 顯示二手listView()
        {
            listView餐廚設備二手平台.Clear();
            listView餐廚設備二手平台.View = View.Tile;
            listView餐廚設備二手平台.Scrollable = true;
            listView餐廚設備二手平台.TileSize = new Size(1200, 105);
            //LargeIcon,Tile,List,SmailIcon可選
            imageList餐廚設備二手平台.ImageSize = new Size(100, 100);

            listView餐廚設備二手平台.LargeImageList = imageList餐廚設備二手平台;
            //LargeIcon,Tile

            listView餐廚設備二手平台.SmallImageList = imageList餐廚設備二手平台;
            //SmallIcon,List

            for (int i = 0; i < list二手Id.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Text = $"[{list二手買或賣[i]}]     [{list二手商品名稱[i]}]     [{list二手商品新舊[i]}]    $價格 : {list二手商品價格[i]}元   [{list二手商品交易方式[i]}]  ";
                item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item.Tag = list二手Id[i];
                listView餐廚設備二手平台.Items.Add(item); // 使用傳入的listView變量而不是硬編碼的名稱
            }
        }

        void 顯示接案listView()
        {
            listView達人接案平台.Clear();
            listView達人接案平台.View = View.Tile;
            listView達人接案平台.Scrollable = true;
            listView達人接案平台.TileSize = new Size(1200, 105);
            //LargeIcon,Tile,List,SmailIcon可選
            imageList達人接案平台.ImageSize = new Size(100, 100);

            listView達人接案平台.LargeImageList = imageList達人接案平台;
            //LargeIcon,Tile

            listView達人接案平台.SmallImageList = imageList達人接案平台;
            //SmallIcon,List

            for (int i = 0; i < list接案Id.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Text = $"[{list接案商品交易模式[i]}]  [{list接案商品名稱[i]}]  姓名 : [{list接案姓名[i]}]  年資 : [{list接案年資[i]}]  地區 : [{list接案商品地區[i]}]  [{list接案商品交易方式[i]}]";
                item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item.Tag = list接案Id[i];
                listView達人接案平台.Items.Add(item); // 使用傳入的listView變量而不是硬編碼的名稱
            }
        }

        //------------------------------------------------------------

        void 更新ListView顯示最近看過的商品(string ids, ListView listViewnow)
        {
            var listId = new List<int>();
            var list商品名稱 = new List<string>();
            var list商品價格 = new List<int>();
            var list銷售模式 = new List<string>();
            var tempImageList = new List<Image>(); // 用於暫存從文件加載的圖片
            var imageList = new ImageList
            {
                ImageSize = new Size(100, 100) // 根據實際需要調整大小
            };

            // 清空當前ListView中的項目和圖片
            var currentListView = listViewnow; // 根據實際需要選擇對應的ListView
            currentListView.Items.Clear();

            // 使用ids構建查詢數據庫的SQL語句
            string strSQL = $"SELECT * FROM products WHERE pid IN ({ids}); "; // 根據ids的順序進行排序
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(strSQL, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listId.Add((int)reader["pid"]);
                            list商品名稱.Add((string)reader["pname"]);
                            list商品價格.Add((int)reader["price"]);
                            list銷售模式.Add((string)reader["pmode"]);

                            string image_name = reader.IsDBNull(reader.GetOrdinal("pimage")) ? "預設圖片.png" : (string)reader["pimage"];
                            string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, image_name);

                            try
                            {
                                var img = Image.FromFile(完整圖檔路徑);
                                tempImageList.Add(img);
                            }
                            catch
                            {
                                // 如果指定的圖片加載失敗，則使用預設圖片
                                string 預設圖片路徑 = Path.Combine(GlobalVar.image_dir, "預設圖片.png");
                                var img = Image.FromFile(預設圖片路徑);
                                tempImageList.Add(img); // 使用預設圖片
                            }
                        }
                    }
                }
            }

            // 倒序排列所有列表和圖片列表以使最近查看的商品顯示在最上方
            listId.Reverse();
            list商品名稱.Reverse();
            list商品價格.Reverse();
            list銷售模式.Reverse();
            tempImageList.Reverse();

            // 將倒序後的圖片加入到imageList
            foreach (var img in tempImageList)
            {
                imageList.Images.Add(img);
            }

            currentListView.LargeImageList = imageList;
            currentListView.SmallImageList = imageList;

            // 更新ListView
            for (int i = 0; i < listId.Count; i++)
            {
                ListViewItem item = new ListViewItem
                {
                    ImageIndex = i, // 圖片索引
                    Text = $"{list商品名稱[i]}   $價格 : {list商品價格[i]}元     *銷售模式 : [{list銷售模式[i]}]",
                    Font = new Font("微軟正黑體", 14, FontStyle.Regular),
                    Tag = listId[i]
                };
                currentListView.Items.Add(item);
            }
        }

        void 更新二手ListView顯示最近看過的商品(string ids)
        {
            var list二手Id = new List<int>();
            var list二手商品名稱 = new List<string>();
            var list二手商品價格 = new List<int>();
            var list二手商品交易方式 = new List<string>();
            var tempImageList = new List<Image>(); // 用於暫存從文件加載的圖片
            imageList餐廚設備二手平台.Images.Clear(); // 清空當前ImageList的內容

            string strSQL = $"SELECT * FROM secondhand WHERE shid IN ({ids}); "; // 注意：SQL語法和函數可能根據使用的數據庫系統而異

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(strSQL, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list二手Id.Add((int)reader["shid"]);
                            list二手商品名稱.Add((string)reader["shpname"]);
                            list二手商品價格.Add((int)reader["shprice"]);
                            list二手商品交易方式.Add((string)reader["shtradeway"]);

                            string image_name = reader.IsDBNull(reader.GetOrdinal("shpimage")) ? "預設圖片.png" : (string)reader["shpimage"];
                            string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, "二手", image_name);

                            try
                            {
                                var img = Image.FromFile(完整圖檔路徑);
                                tempImageList.Add(img);
                            }
                            catch
                            {
                                //// 如果指定的圖片加載失敗，則使用預設圖片
                                //string 預設圖片路徑 = Path.Combine(GlobalVar.image_dir,"二手","預設圖片.png");
                                //var img = Image.FromFile(預設圖片路徑);
                                //tempImageList.Add(img); // 使用預設圖片
                            }
                        }
                    }
                }
            }

            // 將圖片加入到imageList餐廚設備二手平台
            foreach (var img in tempImageList)
            {
                imageList餐廚設備二手平台.Images.Add(img);
            }

            listView餐廚設備二手平台.Items.Clear(); // 清空當前ListView中的項目
                                            // 更新ListView
            for (int i = 0; i < list二手Id.Count; i++)
            {
                ListViewItem item = new ListViewItem
                {
                    ImageIndex = i, // 圖片索引
                    Text = $"[{list二手買或賣[i]}]     [{list二手商品名稱[i]}]     [{list二手商品新舊[i]}]    $價格 : {list二手商品價格[i]}元   [{list二手商品交易方式[i]}]  ",
                    Font = new Font("微軟正黑體", 14, FontStyle.Regular),
                    Tag = list二手Id[i]
                };
                listView餐廚設備二手平台.Items.Add(item);
            }
        }

        void 更新接案ListView顯示最近看過的商品(string ids)
        {
            var list接案Id = new List<int>();
            var list接案商品地區 = new List<string>();
            var list接案年資 = new List<string>();
            var list接案商品交易方式 = new List<string>();
            var list接案姓名 = new List<string>();
            var list接案商品名稱 = new List<string>();
            var list接案聯絡資訊 = new List<string>();
            var list接案商品描述 = new List<string>();
            var list接案商品交易模式 = new List<string>();

            var tempImageList = new List<Image>(); // 用於暫存從文件加載的圖片
            imageList達人接案平台.Images.Clear(); // 清空當前ImageList的內容

            string strSQL = $"SELECT * FROM expert WHERE epid IN ({ids}); "; // 注意：SQL語法和函數可能根據使用的數據庫系統而異

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(strSQL, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list接案Id.Add((int)reader["epid"]);
                            list接案商品地區.Add((string)reader["eprigion"]);
                            list接案年資.Add((string)reader["epseniority"]);
                            list接案商品交易方式.Add((string)reader["epserviceway"]);
                            list接案姓名.Add((string)reader["epname"]);
                            list接案商品名稱.Add((string)reader["epskill"]);
                            list接案聯絡資訊.Add((string)reader["epcontact"]);
                            list接案商品描述.Add((string)reader["epdesc"]);
                            list接案商品交易模式.Add((string)reader["epbuyorsale"]);

                            string image_name = reader.IsDBNull(reader.GetOrdinal("epimage")) ? "預設圖片.png" : (string)reader["epimage"];
                            string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, "接案", image_name);

                            try
                            {
                                var img = Image.FromFile(完整圖檔路徑);
                                tempImageList.Add(img);
                            }
                            catch
                            {
                                // 如果指定的圖片加載失敗，則使用預設圖片
                                string 預設圖片路徑 = Path.Combine(GlobalVar.image_dir, "接案", "預設圖片.png");
                                var img = Image.FromFile(預設圖片路徑);
                                tempImageList.Add(img); // 使用預設圖片
                            }
                        }
                    }
                }
            }

            // 將圖片加入到imageList達人接案平台
            foreach (var img in tempImageList)
            {
                imageList達人接案平台.Images.Add(img);
            }

            listView達人接案平台.Items.Clear(); // 清空當前ListView中的項目
                                          // 更新ListView
            for (int i = 0; i < list接案Id.Count; i++)
            {
                ListViewItem item = new ListViewItem
                {
                    ImageIndex = i, // 圖片索引
                    Text = $"[{list接案商品交易模式[i]}]  [{list接案商品名稱[i]}]  姓名 : [{list接案姓名[i]}]  年資 : [{list接案年資[i]}]  地區 : [{list接案商品地區[i]}]  [{list接案商品交易方式[i]}]",
                    Font = new Font("微軟正黑體", 14, FontStyle.Regular),
                    Tag = list接案Id[i]
                };
                listView達人接案平台.Items.Add(item);
            }
        }

        //------------------------------------------------------------

        void 商品搜尋(string ptype, string 關鍵字, string 排除1, string 排除2)
        {
            if (string.IsNullOrEmpty(關鍵字))
            {
                string message = "請輸入關鍵字";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請輸入關鍵字");
                return;
            }

            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT * FROM products WHERE ptype = @ptype AND pname LIKE @關鍵字");

            var listView = 當前listview(ptype);
            var imageList = dictImageList[ptype];
            listView.Items.Clear();
            imageList.Images.Clear(); // 清除舊有的圖片

            if (!string.IsNullOrEmpty(排除1))
            {
                sqlQuery.Append(" AND pname NOT LIKE @排除1");
            }
            if (!string.IsNullOrEmpty(排除2))
            {
                sqlQuery.Append(" AND pname NOT LIKE @排除2");
            }

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@ptype", ptype);
                    cmd.Parameters.AddWithValue("@關鍵字", "%" + 關鍵字 + "%");
                    if (!string.IsNullOrEmpty(排除1))
                    {
                        cmd.Parameters.AddWithValue("@排除1", "%" + 排除1 + "%");
                    }
                    if (!string.IsNullOrEmpty(排除2))
                    {
                        cmd.Parameters.AddWithValue("@排除2", "%" + 排除2 + "%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int imageIndex = 0;
                        while (reader.Read())
                        {
                            string imageFilePath = $"{GlobalVar.image_dir}\\" + reader["pimage"].ToString(); // 假設圖片存儲的路徑
                            Image img = Image.FromFile(imageFilePath);
                            imageList.Images.Add(img); // 將圖片加入到ImageList

                            ListViewItem item = new ListViewItem();
                            item.ImageIndex = imageIndex++; // 設定圖片索引
                            string itemText = $"{reader["pname"]}   $價格 : {reader["price"]}元     *銷售模式 : [{reader["pmode"]}]";
                            item.Text = itemText;
                            item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                            item.Tag = reader["pid"];
                            listView.Items.Add(item); // 添加項目到ListView
                        }
                    }
                }
            }
        }

        void 更新點擊紀錄(int 商品ID)
        {
            // 將商品ID轉換為字串形式
            string 商品ID字串 = 商品ID.ToString();

            // 檢查該商品ID是否已存在於紀錄中
            if (Properties.Settings.Default._最近看過.Contains(商品ID字串))
            {
                // 如果已存在，先從紀錄中移除
                Properties.Settings.Default._最近看過.Remove(商品ID字串);
            }

            // 然後將該商品ID重新添加到紀錄的開頭
            Properties.Settings.Default._最近看過.Insert(0, 商品ID字串);

            // 儲存變更
            Properties.Settings.Default.Save();
        }

        //------------------------------------------------------------

        void 二手商品搜尋(string 關鍵字, string 排除1, string 排除2)
        {
            if (string.IsNullOrEmpty(關鍵字))
            {
                string message = "請輸入關鍵字";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請輸入關鍵字");
                return;
            }

            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT * FROM secondhand WHERE shpname LIKE @關鍵字");

            var listView = listView餐廚設備二手平台;
            var imageList = imageList餐廚設備二手平台;

            listView.Items.Clear();
            imageList.Images.Clear(); // 清除舊有的圖片

            if (!string.IsNullOrEmpty(排除1))
            {
                sqlQuery.Append(" AND shpname NOT LIKE @排除1");
            }
            if (!string.IsNullOrEmpty(排除2))
            {
                sqlQuery.Append(" AND shpname NOT LIKE @排除2");
            }

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@關鍵字", "%" + 關鍵字 + "%");
                    if (!string.IsNullOrEmpty(排除1))
                    {
                        cmd.Parameters.AddWithValue("@排除1", "%" + 排除1 + "%");
                    }
                    if (!string.IsNullOrEmpty(排除2))
                    {
                        cmd.Parameters.AddWithValue("@排除2", "%" + 排除2 + "%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int imageIndex = 0;
                        while (reader.Read())
                        {
                            string image_name = reader["shpimage"].ToString();
                            string imageFilePath = Path.Combine(GlobalVar.image_dir, "二手", image_name); // 假設圖片存儲的路徑
                            Image img = Image.FromFile(imageFilePath);
                            imageList.Images.Add(img); // 將圖片加入到ImageList

                            ListViewItem item = new ListViewItem();
                            item.ImageIndex = imageIndex++; // 設定圖片索引
                            string itemText = $"[{reader["shbuyorsale"]}]     [{reader["shpname"]}]     [{reader["shnew"]}]    $價格 : {reader["shprice"]}元   [{reader["shtradeway"]}]";
                            item.Text = itemText;
                            item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                            item.Tag = reader["shid"];
                            listView.Items.Add(item); // 添加項目到ListView
                        }
                    }
                }
            }
        }

        void 二手更新點擊紀錄(int 二手商品ID)
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

        //------------------------------------------------------------

        void 接案商品搜尋(string 關鍵字, string 排除1, string 排除2)
        {
            if (string.IsNullOrEmpty(關鍵字))
            {
                string message = "請輸入關鍵字";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請輸入關鍵字");
                return;
            }

            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT * FROM expert WHERE epskill LIKE @關鍵字");

            var listView = listView達人接案平台;
            var imageList = imageList達人接案平台;

            listView.Items.Clear();
            imageList.Images.Clear(); // 清除舊有的圖片

            if (!string.IsNullOrEmpty(排除1))
            {
                sqlQuery.Append(" AND shpname NOT LIKE @排除1");
            }
            if (!string.IsNullOrEmpty(排除2))
            {
                sqlQuery.Append(" AND shpname NOT LIKE @排除2");
            }

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@關鍵字", "%" + 關鍵字 + "%");
                    if (!string.IsNullOrEmpty(排除1))
                    {
                        cmd.Parameters.AddWithValue("@排除1", "%" + 排除1 + "%");
                    }
                    if (!string.IsNullOrEmpty(排除2))
                    {
                        cmd.Parameters.AddWithValue("@排除2", "%" + 排除2 + "%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int imageIndex = 0;
                        while (reader.Read())
                        {
                            string image_name = reader["epimage"].ToString();
                            string imageFilePath = Path.Combine(GlobalVar.image_dir, "接案", image_name); // 假設圖片存儲的路徑
                            Image img = Image.FromFile(imageFilePath);
                            imageList.Images.Add(img); // 將圖片加入到ImageList

                            ListViewItem item = new ListViewItem();
                            item.ImageIndex = imageIndex++; // 設定圖片索引
                            string itemText = $"[{reader["epbuyorsale"]}]  [{reader["epskill"]}]  姓名 : [{reader["epname"]}]  年資 : [{reader["epseniority"]}]  地區 : [{reader["eprigion"]}]  [{reader["epserviceway"]}]";
                            item.Text = itemText;
                            item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                            item.Tag = reader["epid"];
                            listView.Items.Add(item); // 添加項目到ListView
                        }
                    }
                }
            }
        }

        void 接案更新點擊紀錄(int 接案商品ID)
        {
            // 將商品ID轉換為字串形式
            string 接案商品ID字串 = 接案商品ID.ToString();

            // 檢查該商品ID是否已存在於紀錄中
            if (Properties.Settings.Default._接案最近看過.Contains(接案商品ID字串))
            {
                // 如果已存在，先從紀錄中移除
                Properties.Settings.Default._接案最近看過.Remove(接案商品ID字串);
            }

            // 然後將該商品ID重新添加到紀錄的開頭
            Properties.Settings.Default._接案最近看過.Insert(0, 接案商品ID字串);

            // 儲存變更
            Properties.Settings.Default.Save();
        }

        //===============  共用方法  ==================================
        //
        //===============  商用料理配方  ==============================

        private void btn商用料理配方_搜尋_Click(object sender, EventArgs e)
        {
            string 關鍵字_商用料理配方 = txt商用料理配方_關鍵字.Text.Trim();
            string 排除1_商用料理配方 = txt商用料理配方_排除1.Text.Trim();
            string 排除2_商用料理配方 = txt商用料理配方_排除2.Text.Trim();

            商品搜尋("商用料理配方", 關鍵字_商用料理配方, 排除1_商用料理配方, 排除2_商用料理配方);
        }

        private void txt商用料理配方_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn商用料理配方_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void listView商用料理配方_ItemActivate(object sender, EventArgs e)
        {
            if (listView商用料理配方.SelectedItems.Count > 0)
            {
                var selectedItem = listView商用料理配方.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設您已將商品ID存儲在Tag屬性中

                // 檢查 _最近看過 集合是否為 null
                if (Properties.Settings.Default._最近看過 == null)
                {
                    // 如果是 null，則初始化為一個新的 StringCollection
                    Properties.Settings.Default._最近看過 = new System.Collections.Specialized.StringCollection();
                }

                // 将商品ID添加到_Settings._最近看過
                if (!Properties.Settings.Default._最近看過.Contains(商品ID.ToString()))
                {
                    Properties.Settings.Default._最近看過.Add(商品ID.ToString());
                    Properties.Settings.Default.Save(); // 保存设置
                }

                // 更新點擊紀錄
                更新點擊紀錄(商品ID);

                // 假設dictListId["商用料理配方"]存儲了當前分頁的所有商品ID
                List<int> 商品ID列表 = dictListId["商用料理配方"];
                int 當前索引 = 商品ID列表.IndexOf(商品ID); // 獲取選中商品的索引

                // 創建Form商品詳情的實例並傳遞商品ID列表和當前索引
                Form商品詳情 form商品詳情 = new Form商品詳情(商品ID列表, 當前索引, this);
                form商品詳情.ShowDialog();
            }
        }

        private void btn商用料理配方_重新整理_Click(object sender, EventArgs e)
        {
            重新載入資料("商用料理配方");
            txt商用料理配方_關鍵字.Clear();
            txt商用料理配方_排除1.Clear();
            txt商用料理配方_排除2.Clear();
        }

        private void btn商用料理配方_最近看過_Click(object sender, EventArgs e)
        {
            var 最近看過的商品IDs = Properties.Settings.Default._最近看過;

            if ((最近看過的商品IDs != null) && (最近看過的商品IDs.Count > 0))
            {
                string ids = string.Join(",", 最近看過的商品IDs.Cast<string>());
                // 根据IDs查询数据库并更新ListView
                更新ListView顯示最近看過的商品(ids, listView商用料理配方);
            }
            else
            {
                //MessageBox.Show("没有最近查看的商品。");
                string message = "没有最近查看的商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }

        private void btn商用料理配方_加入購物車_Click(object sender, EventArgs e)
        {
            if (listView商用料理配方.SelectedItems.Count > 0)
            {
                var selectedItem = listView商用料理配方.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設商品ID存儲在Tag屬性中
                int 數量 = NumUD商用料理配方_加入購物車.Value;

                if ((數量 <= 0) && (數量 >= 500))
                {
                    string message = "請輸入有效的購買數量。";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show("請輸入有效的購買數量。");
                    return;
                }

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

                        Console.WriteLine($"儲存的商品ID:{商品ID} 儲存的數量:{數量} 購買者ID:{購買者ID} 加入時間:{加入時間}");

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            數量 = 1;
                            NumUD商用料理配方_加入購物車.Value = 數量;
                            Console.WriteLine($"{數量}");

                            string message = "成功加入購物車！";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("成功加入購物車！");
                            FillCartDataGridView();
                        }
                        else
                        {
                            string message = "加入購物車失敗。";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("加入購物車失敗。");
                        }
                    }
                }
            }
            else
            {
                string message = "請選擇一個商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請選擇一個商品。");
            }

        }

        private void cbox商用料理配方_排序選項_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 獲取選中的排序選項
            var 選擇的排序方式 = cbox商用料理配方_排序選項.SelectedItem.ToString();
            string 排序語句 = "";

            switch (選擇的排序方式)
            {
                case "價格低到高":
                    // 執行價格低到高的排序操作
                    排序語句 = "ORDER BY price ASC";
                    break;
                case "價格高到低":
                    // 執行價格高到低的排序操作
                    排序語句 = "ORDER BY price DESC";
                    break;
                case "姓名筆劃多到少":
                    // 執行姓名筆劃多到少的排序操作
                    排序語句 = "ORDER BY pname DESC"; // 假設使用姓名長度進行排序
                    break;
                case "姓名筆劃少到多":
                    // 執行姓名筆劃少到多的排序操作
                    排序語句 = "ORDER BY pname ASC";
                    break;
            }

            // 重新加載資料
            讀取cbox並顯示資料("商用料理配方", 排序語句);
        }

        //===============  商用料理配方  ==============================

        //
        //===============  商用料理配料包  ==============================

        private void btn商用料理配料包_搜尋_Click(object sender, EventArgs e)
        {
            string 關鍵字_商用料理配料包 = txt商用料理配料包_關鍵字.Text;
            string 排除1_商用料理配料包 = txt商用料理配料包_排除1.Text;
            string 排除2_商用料理配料包 = txt商用料理配料包_排除2.Text;

            商品搜尋("商用料理配料包", 關鍵字_商用料理配料包, 排除1_商用料理配料包, 排除2_商用料理配料包);
        }

        private void txt商用料理配料包_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn商用料理配料包_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void listView商用料理配料包_ItemActivate(object sender, EventArgs e)
        {
            if (listView商用料理配料包.SelectedItems.Count > 0)
            {
                var selectedItem = listView商用料理配料包.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設您已將商品ID存儲在Tag屬性中

                // 檢查 _最近看過 集合是否為 null，然後添加商品ID到 _最近看過
                if (Properties.Settings.Default._最近看過 == null)
                {
                    Properties.Settings.Default._最近看過 = new System.Collections.Specialized.StringCollection();
                }
                if (!Properties.Settings.Default._最近看過.Contains(商品ID.ToString()))
                {
                    Properties.Settings.Default._最近看過.Add(商品ID.ToString());
                    Properties.Settings.Default.Save(); // 保存设置
                }

                // 更新點擊紀錄，假設有一個相應的方法
                更新點擊紀錄(商品ID);

                // 假設dictListId["商用料理配料包"]存儲了當前分頁的所有商品ID
                List<int> 商品ID列表 = dictListId["商用料理配料包"];
                int 當前索引 = 商品ID列表.IndexOf(商品ID);

                // 創建Form商品詳情的實例並傳遞商品ID列表和當前索引
                Form商品詳情 form商品詳情 = new Form商品詳情(商品ID列表, 當前索引, this);
                form商品詳情.ShowDialog();
            }
        }

        private void btn商用料理配料包_重新整理_Click(object sender, EventArgs e)
        {
            重新載入資料("商用料理配料包");
            // 假設「商用料理配料包」分頁的文本輸入欄位名稱如下，請根據實際情況調整
            txt商用料理配料包_關鍵字.Clear();
            txt商用料理配料包_排除1.Clear();
            txt商用料理配料包_排除2.Clear();
        }

        private void btn商用料理配料包_最近看過_Click(object sender, EventArgs e)
        {
            var 最近看過的商品IDs = Properties.Settings.Default._最近看過;

            if ((最近看過的商品IDs != null) && (最近看過的商品IDs.Count > 0))
            {
                string ids = string.Join(",", 最近看過的商品IDs.Cast<string>());
                // 根据IDs查询数据库并更新ListView
                更新ListView顯示最近看過的商品(ids, listView商用料理配料包);
            }
            else
            {
                //MessageBox.Show("没有最近查看的商品。");
                string message = "没有最近查看的商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }

        private void btn商用料理配料包_加入購物車_Click(object sender, EventArgs e)
        {
            if (listView商用料理配料包.SelectedItems.Count > 0)
            {
                var selectedItem = listView商用料理配料包.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設商品ID存儲在Tag屬性中
                int 數量 = NumUD商用料理配料包_加入購物車.Value;

                if ((數量 <= 0) && (數量 >= 500))
                {
                    string message = "請輸入有效的購買數量。";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show("請輸入有效的購買數量。");
                    return;
                }

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

                        Console.WriteLine($"儲存的商品ID:{商品ID} 儲存的數量:{數量} 購買者ID:{購買者ID} 加入時間:{加入時間}");

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            數量 = 1;
                            NumUD商用料理配料包_加入購物車.Value = 數量;
                            Console.WriteLine($"{數量}");

                            string message = "成功加入購物車！";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("成功加入購物車！");
                            FillCartDataGridView();
                        }
                        else
                        {
                            string message = "加入購物車失敗。";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("加入購物車失敗。");
                        }
                    }
                }
            }
            else
            {
                string message = "請選擇一個商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請選擇一個商品。");
            }

        }

        private void cbox商用料理配料包_排序選項_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 獲取選中的排序選項
            var 選擇的排序方式 = cbox商用料理配料包_排序選項.SelectedItem.ToString();
            string 排序語句 = "";


            switch (選擇的排序方式)
            {
                case "價格低到高":
                    // 執行價格低到高的排序操作
                    排序語句 = "ORDER BY price ASC";
                    break;
                case "價格高到低":
                    // 執行價格高到低的排序操作
                    排序語句 = "ORDER BY price DESC";
                    break;
                case "姓名筆劃多到少":
                    // 執行姓名筆劃多到少的排序操作
                    排序語句 = "ORDER BY pname DESC"; // 假設使用姓名長度進行排序
                    break;
                case "姓名筆劃少到多":
                    // 執行姓名筆劃少到多的排序操作
                    排序語句 = "ORDER BY pname ASC";
                    break;
            }

            // 重新加載資料
            讀取cbox並顯示資料("商用料理配料包", 排序語句);
        }

        //===============  商用料理配料包  ==============================
        //
        //===============   廚藝顧問出租  ==============================

        private void btn廚藝顧問出租_搜尋_Click(object sender, EventArgs e)
        {
            string 關鍵字_廚藝顧問出租 = txt廚藝顧問出租_關鍵字.Text;
            string 排除1_廚藝顧問出租 = txt廚藝顧問出租_排除1.Text;
            string 排除2_廚藝顧問出租 = txt廚藝顧問出租_排除2.Text;

            商品搜尋("廚藝顧問出租", 關鍵字_廚藝顧問出租, 排除1_廚藝顧問出租, 排除2_廚藝顧問出租);
        }

        private void txt廚藝顧問出租_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn廚藝顧問出租_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void listView廚藝顧問出租_ItemActivate(object sender, EventArgs e)
        {
            if (listView廚藝顧問出租.SelectedItems.Count > 0)
            {
                var selectedItem = listView廚藝顧問出租.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設您已將商品ID存儲在Tag屬性中

                // 檢查 _最近看過 集合是否為 null，然後添加商品ID到 _最近看過
                if (Properties.Settings.Default._最近看過 == null)
                {
                    Properties.Settings.Default._最近看過 = new System.Collections.Specialized.StringCollection();
                }
                if (!Properties.Settings.Default._最近看過.Contains(商品ID.ToString()))
                {
                    Properties.Settings.Default._最近看過.Add(商品ID.ToString());
                    Properties.Settings.Default.Save(); // 保存设置
                }

                // 更新點擊紀錄，假設有一個相應的方法
                更新點擊紀錄(商品ID);

                // 假設dictListId["廚藝顧問出租"]存儲了當前分頁的所有商品ID
                List<int> 商品ID列表 = dictListId["廚藝顧問出租"];
                int 當前索引 = 商品ID列表.IndexOf(商品ID);

                // 創建Form商品詳情的實例並傳遞商品ID列表和當前索引
                Form商品詳情 form商品詳情 = new Form商品詳情(商品ID列表, 當前索引, this);
                form商品詳情.ShowDialog();
            }
        }

        private void btn廚藝顧問出租_重新整理_Click(object sender, EventArgs e)
        {
            重新載入資料("廚藝顧問出租");
            // 假設「廚藝顧問出租」分頁的文本輸入欄位名稱如下，請根據實際情況調整
            txt廚藝顧問出租_關鍵字.Clear();
            txt廚藝顧問出租_排除1.Clear();
            txt廚藝顧問出租_排除2.Clear();
        }

        private void btn廚藝顧問出租_最近看過_Click(object sender, EventArgs e)
        {
            var 最近看過的商品IDs = Properties.Settings.Default._最近看過;

            if ((最近看過的商品IDs != null) && (最近看過的商品IDs.Count > 0))
            {
                string ids = string.Join(",", 最近看過的商品IDs.Cast<string>());
                // 根据IDs查询数据库并更新ListView
                更新ListView顯示最近看過的商品(ids, listView廚藝顧問出租);
            }
            else
            {
                //MessageBox.Show("没有最近查看的商品。");
                string message = "没有最近查看的商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }

        private void btn廚藝顧問出租_加入購物車_Click(object sender, EventArgs e)
        {
            if (listView廚藝顧問出租.SelectedItems.Count > 0)
            {
                var selectedItem = listView廚藝顧問出租.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設商品ID存儲在Tag屬性中
                int 數量 = NumUD廚藝顧問出租_加入購物車.Value;

                if ((數量 <= 0) && (數量 >= 500))
                {
                    string message = "請輸入有效的購買數量。";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show("請輸入有效的購買數量。");
                    return;
                }

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

                        Console.WriteLine($"儲存的商品ID:{商品ID} 儲存的數量:{數量} 購買者ID:{購買者ID} 加入時間:{加入時間}");

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            數量 = 1;
                            NumUD廚藝顧問出租_加入購物車.Value = 數量;
                            Console.WriteLine($"{數量}");

                            string message = "成功加入購物車！";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("成功加入購物車！");
                            FillCartDataGridView();
                        }
                        else
                        {
                            string message = "加入購物車失敗。";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("加入購物車失敗。");
                        }
                    }
                }
            }
            else
            {
                string message = "請選擇一個商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請選擇一個商品。");
            }
        }

        private void cbox廚藝顧問出租_排序選項_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // 獲取選中的排序選項
            var 選擇的排序方式 = cbox廚藝顧問出租_排序選項.SelectedItem.ToString();
            string 排序語句 = "";


            switch (選擇的排序方式)
            {
                case "價格低到高":
                    // 執行價格低到高的排序操作
                    排序語句 = "ORDER BY price ASC";
                    break;
                case "價格高到低":
                    // 執行價格高到低的排序操作
                    排序語句 = "ORDER BY price DESC";
                    break;
                case "姓名筆劃多到少":
                    // 執行姓名筆劃多到少的排序操作
                    排序語句 = "ORDER BY pname DESC"; // 假設使用姓名長度進行排序
                    break;
                case "姓名筆劃少到多":
                    // 執行姓名筆劃少到多的排序操作
                    排序語句 = "ORDER BY pname ASC";
                    break;
            }

            // 重新加載資料
            讀取cbox並顯示資料("廚藝顧問出租", 排序語句);
        }

        //===============   廚藝顧問出租  =================================
        //
        //===============   商用廚房設備規劃  ==============================

        private void btn商用廚房設備規劃_搜尋_Click(object sender, EventArgs e)
        {
            string 關鍵字_商用廚房設備規劃 = txt商用廚房設備規劃_關鍵字.Text;
            string 排除1_商用廚房設備規劃 = txt商用廚房設備規劃_排除1.Text;
            string 排除2_商用廚房設備規劃 = txt商用廚房設備規劃_排除2.Text;

            商品搜尋("商用廚房設備規劃", 關鍵字_商用廚房設備規劃, 排除1_商用廚房設備規劃, 排除2_商用廚房設備規劃);
        }

        private void txt商用廚房設備規劃_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn商用廚房設備規劃_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void listView商用廚房設備規劃_ItemActivate(object sender, EventArgs e)
        {
            if (listView商用廚房設備規劃.SelectedItems.Count > 0)
            {
                var selectedItem = listView商用廚房設備規劃.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設您已將商品ID存儲在Tag屬性中

                // 檢查 _最近看過 集合是否為 null，然後添加商品ID到 _最近看過
                if (Properties.Settings.Default._最近看過 == null)
                {
                    Properties.Settings.Default._最近看過 = new System.Collections.Specialized.StringCollection();
                }
                if (!Properties.Settings.Default._最近看過.Contains(商品ID.ToString()))
                {
                    Properties.Settings.Default._最近看過.Add(商品ID.ToString());
                    Properties.Settings.Default.Save(); // 保存设置
                }

                // 更新點擊紀錄，假設有一個相應的方法
                更新點擊紀錄(商品ID);

                // 假設dictListId["商用廚房設備規劃"]存儲了當前分頁的所有商品ID
                List<int> 商品ID列表 = dictListId["商用廚房設備規劃"];
                int 當前索引 = 商品ID列表.IndexOf(商品ID);

                // 創建Form商品詳情的實例並傳遞商品ID列表和當前索引
                Form商品詳情 form商品詳情 = new Form商品詳情(商品ID列表, 當前索引, this);
                form商品詳情.ShowDialog();
            }
        }

        private void btn商用廚房設備規劃_重新整理_Click(object sender, EventArgs e)
        {
            重新載入資料("商用廚房設備規劃");
            // 假設「商用廚房設備規劃」分頁的文本輸入欄位名稱如下，請根據實際情況調整
            txt商用廚房設備規劃_關鍵字.Clear();
            txt商用廚房設備規劃_排除1.Clear();
            txt商用廚房設備規劃_排除2.Clear();
        }

        private void btn商用廚房設備規劃_最近看過_Click(object sender, EventArgs e)
        {
            var 最近看過的商品IDs = Properties.Settings.Default._最近看過;

            if ((最近看過的商品IDs != null) && (最近看過的商品IDs.Count > 0))
            {
                string ids = string.Join(",", 最近看過的商品IDs.Cast<string>());
                // 根据IDs查询数据库并更新ListView
                更新ListView顯示最近看過的商品(ids, listView商用廚房設備規劃);
            }
            else
            {
                //MessageBox.Show("没有最近查看的商品。");
                string message = "没有最近查看的商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }

        private void btn商用廚房設備規劃_加入購物車_Click(object sender, EventArgs e)
        {
            if (listView商用廚房設備規劃.SelectedItems.Count > 0)
            {
                var selectedItem = listView商用廚房設備規劃.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設商品ID存儲在Tag屬性中
                int 數量 = NumUD商用廚房設備規劃_加入購物車.Value;

                if ((數量 <= 0) && (數量 >= 500))
                {
                    string message = "請輸入有效的購買數量。";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show("請輸入有效的購買數量。");
                    return;
                }

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

                        Console.WriteLine($"儲存的商品ID:{商品ID} 儲存的數量:{數量} 購買者ID:{購買者ID} 加入時間:{加入時間}");

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            數量 = 1;
                            NumUD商用廚房設備規劃_加入購物車.Value = 數量;
                            Console.WriteLine($"{數量}");

                            string message = "成功加入購物車！";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("成功加入購物車！");
                            FillCartDataGridView();
                        }
                        else
                        {
                            string message = "加入購物車失敗。";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("加入購物車失敗。");
                        }
                    }
                }
            }
            else
            {
                string message = "請選擇一個商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請選擇一個商品。");
            }
        }

        private void cbox商用廚房設備規劃_排序選項_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 獲取選中的排序選項
            var 選擇的排序方式 = cbox商用廚房設備規劃_排序選項.SelectedItem.ToString();
            string 排序語句 = "";


            switch (選擇的排序方式)
            {
                case "價格低到高":
                    // 執行價格低到高的排序操作
                    排序語句 = "ORDER BY price ASC";
                    break;
                case "價格高到低":
                    // 執行價格高到低的排序操作
                    排序語句 = "ORDER BY price DESC";
                    break;
                case "姓名筆劃多到少":
                    // 執行姓名筆劃多到少的排序操作
                    排序語句 = "ORDER BY pname DESC"; // 假設使用姓名長度進行排序
                    break;
                case "姓名筆劃少到多":
                    // 執行姓名筆劃少到多的排序操作
                    排序語句 = "ORDER BY pname ASC";
                    break;
            }

            // 重新加載資料
            讀取cbox並顯示資料("商用廚房設備規劃", 排序語句);
        }

        //===============   商用廚房設備規劃  ==============================
        //
        //===============   技術轉移保證班  ==============================

        private void btn技術轉移保證班_搜尋_Click(object sender, EventArgs e)
        {
            string 關鍵字_技術轉移保證班 = txt技術轉移保證班_關鍵字.Text;
            string 排除1_技術轉移保證班 = txt技術轉移保證班_排除1.Text;
            string 排除2_技術轉移保證班 = txt技術轉移保證班_排除2.Text;

            商品搜尋("技術轉移保證班", 關鍵字_技術轉移保證班, 排除1_技術轉移保證班, 排除2_技術轉移保證班);
        }

        private void txt技術轉移保證班_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn技術轉移保證班_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void listView技術轉移保證班_ItemActivate(object sender, EventArgs e)
        {
            if (listView技術轉移保證班.SelectedItems.Count > 0)
            {
                var selectedItem = listView技術轉移保證班.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設您已將商品ID存儲在Tag屬性中

                // 檢查 _最近看過 集合是否為 null，然後添加商品ID到 _最近看過
                if (Properties.Settings.Default._最近看過 == null)
                {
                    Properties.Settings.Default._最近看過 = new System.Collections.Specialized.StringCollection();
                }
                if (!Properties.Settings.Default._最近看過.Contains(商品ID.ToString()))
                {
                    Properties.Settings.Default._最近看過.Add(商品ID.ToString());
                    Properties.Settings.Default.Save(); // 保存设置
                }

                // 更新點擊紀錄，假設有一個相應的方法
                更新點擊紀錄(商品ID);

                // 假設dictListId["技術轉移保證班"]存儲了當前分頁的所有商品ID
                List<int> 商品ID列表 = dictListId["技術轉移保證班"];
                int 當前索引 = 商品ID列表.IndexOf(商品ID);

                // 創建Form商品詳情的實例並傳遞商品ID列表和當前索引
                Form商品詳情 form商品詳情 = new Form商品詳情(商品ID列表, 當前索引, this);
                form商品詳情.ShowDialog();
            }
        }

        private void btn技術轉移保證班_重新整理_Click(object sender, EventArgs e)
        {
            重新載入資料("技術轉移保證班");
            // 假設「技術轉移保證班」分頁的文本輸入欄位名稱如下，請根據實際情況調整
            txt技術轉移保證班_關鍵字.Clear();
            txt技術轉移保證班_排除1.Clear();
            txt技術轉移保證班_排除2.Clear();
        }

        private void btn技術轉移保證班_最近看過_Click(object sender, EventArgs e)
        {
            var 最近看過的商品IDs = Properties.Settings.Default._最近看過;

            if ((最近看過的商品IDs != null) && (最近看過的商品IDs.Count > 0))
            {
                string ids = string.Join(",", 最近看過的商品IDs.Cast<string>());
                // 根据IDs查询数据库并更新ListView
                更新ListView顯示最近看過的商品(ids, listView技術轉移保證班);
            }
            else
            {
                //MessageBox.Show("没有最近查看的商品。");
                string message = "没有最近查看的商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }

        private void btn技術轉移保證班_加入購物車_Click(object sender, EventArgs e)
        {
            if (listView技術轉移保證班.SelectedItems.Count > 0)
            {
                var selectedItem = listView技術轉移保證班.SelectedItems[0];
                int 商品ID = Convert.ToInt32(selectedItem.Tag); // 假設商品ID存儲在Tag屬性中
                int 數量 = NumUD技術轉移保證班_加入購物車.Value;

                if ((數量 <= 0) && (數量 >= 500))
                {
                    string message = "請輸入有效的購買數量。";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show("請輸入有效的購買數量。");
                    return;
                }

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

                        Console.WriteLine($"儲存的商品ID:{商品ID} 儲存的數量:{數量} 購買者ID:{購買者ID} 加入時間:{加入時間}");

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            數量 = 1;
                            NumUD技術轉移保證班_加入購物車.Value = 數量;
                            Console.WriteLine($"{數量}");

                            string message = "成功加入購物車！";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("成功加入購物車！");
                            FillCartDataGridView();
                        }
                        else
                        {
                            string message = "加入購物車失敗。";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("加入購物車失敗。");
                        }
                    }
                }
            }
            else
            {
                string message = "請選擇一個商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請選擇一個商品。");
            }
        }

        private void cbox技術轉移保證班_排序選項_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 獲取選中的排序選項
            var 選擇的排序方式 = cbox技術轉移保證班_排序選項.SelectedItem.ToString();
            string 排序語句 = "";


            switch (選擇的排序方式)
            {
                case "價格低到高":
                    // 執行價格低到高的排序操作
                    排序語句 = "ORDER BY price ASC";
                    break;
                case "價格高到低":
                    // 執行價格高到低的排序操作
                    排序語句 = "ORDER BY price DESC";
                    break;
                case "姓名筆劃多到少":
                    // 執行姓名筆劃多到少的排序操作
                    排序語句 = "ORDER BY pname DESC"; // 假設使用姓名長度進行排序
                    break;
                case "姓名筆劃少到多":
                    // 執行姓名筆劃少到多的排序操作
                    排序語句 = "ORDER BY pname ASC";
                    break;
            }

            // 重新加載資料
            讀取cbox並顯示資料("技術轉移保證班", 排序語句);
        }

        //===============   技術轉移保證班  ===============================
        //
        //===============   餐廚設備二手平台  ==============================

        private void btn餐廚設備二手平台_搜尋_Click(object sender, EventArgs e)
        {
            string 關鍵字_餐廚設備二手平台 = txt餐廚設備二手平台_關鍵字.Text;
            string 排除1_餐廚設備二手平台 = txt餐廚設備二手平台_排除1.Text;
            string 排除2_餐廚設備二手平台 = txt餐廚設備二手平台_排除2.Text;

            二手商品搜尋(關鍵字_餐廚設備二手平台, 排除1_餐廚設備二手平台, 排除2_餐廚設備二手平台);
        }

        private void txt餐廚設備二手平台_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn餐廚設備二手平台_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

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


                List<int> 二手商品ID列表 = list二手Id;
                int 當前索引 = 二手商品ID列表.IndexOf(二手商品ID);


                Form二手商品詳情 form二手商品詳情 = new Form二手商品詳情(二手商品ID列表, 當前索引, this);
                form二手商品詳情.ShowDialog();
            }
        }

        private void btn餐廚設備二手平台_重新整理_Click(object sender, EventArgs e)
        {
            重新載入二手資料();
            txt餐廚設備二手平台_關鍵字.Clear();
            txt餐廚設備二手平台_排除1.Clear();
            txt餐廚設備二手平台_排除2.Clear();
        }

        private void btn餐廚設備二手平台_最近看過_Click(object sender, EventArgs e)
        {
            var 最近看過的商品IDs = Properties.Settings.Default._二手最近看過;

            if ((最近看過的商品IDs != null) && (最近看過的商品IDs.Count > 0))
            {
                string ids = string.Join(",", 最近看過的商品IDs.Cast<string>());
                // 根据IDs查询数据库并更新ListView
                更新二手ListView顯示最近看過的商品(ids);
            }
            else
            {
                //MessageBox.Show("没有最近查看的商品。");
                string message = "没有最近查看的商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }

        private void btn餐廚設備二手平台_我的商品_Click(object sender, EventArgs e)
        {
            string userEmail = GlobalVar.使用者帳號; // 從GlobalVar獲取當前使用者的電子郵件地址
            string connectionString = GlobalVar.strDBConnectionString; // 數據庫連接字符串

            // 先清除之前可能存在的資料
            list二手Id.Clear();
            list二手商品名稱.Clear();
            list二手商品價格.Clear();
            list二手商品交易方式.Clear();
            imageList餐廚設備二手平台.Images.Clear();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 查詢使用者ID
                string queryPerson = "SELECT id FROM persons WHERE email = @Email";
                SqlCommand cmdPerson = new SqlCommand(queryPerson, con);
                cmdPerson.Parameters.AddWithValue("@Email", userEmail);
                int userId = 0;

                using (SqlDataReader reader = cmdPerson.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userId = Convert.ToInt32(reader["id"]); // 讀取並轉換使用者ID
                    }
                }

                // 如果找到了使用者ID，則根據該ID查詢二手商品
                if (userId != 0)
                {
                    string querySecondhand = "SELECT * FROM secondhand WHERE shcid = @UserID";
                    SqlCommand cmdSecondhand = new SqlCommand(querySecondhand, con);
                    cmdSecondhand.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = cmdSecondhand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list二手Id.Add((int)reader["shid"]);
                            list二手商品名稱.Add(reader["shpname"].ToString());
                            list二手商品價格.Add((int)reader["shprice"]);
                            list二手商品交易方式.Add(reader["shtradeway"].ToString());

                            string image_name = reader.IsDBNull(reader.GetOrdinal("shpimage")) ? "預設圖片.png" : reader["shpimage"].ToString();
                            string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, "二手", image_name);

                            try
                            {
                                Image img = Image.FromFile(完整圖檔路徑);
                                imageList餐廚設備二手平台.Images.Add(img);
                            }
                            catch
                            {
                                // 如果指定的圖片加載失敗，則使用預設圖片
                                string 預設圖片路徑 = Path.Combine(GlobalVar.image_dir, "二手", "預設圖片.png");
                                var img = Image.FromFile(預設圖片路徑);
                                imageList餐廚設備二手平台.Images.Add(img); // 使用預設圖片
                            }
                        }
                    }
                }
            }

            // 使用更新後的全局變量資料來顯示二手商品列表
            顯示二手listView();
        }

        private void btn餐廚設備二手平台_發佈商品_Click(object sender, EventArgs e)
        {
            Form二手商品詳情 form二手商品詳情 = new Form二手商品詳情();
            form二手商品詳情.ShowDialog();
        }

        private void cbox餐廚設備二手平台_只顯示選項_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = cbox餐廚設備二手平台_只顯示選項.SelectedItem.ToString(); // 获取选中的选项
            string connectionString = GlobalVar.strDBConnectionString; // 数据库连接字符串
            list二手Id.Clear();
            list二手商品名稱.Clear();
            list二手商品價格.Clear();
            list二手商品交易方式.Clear();
            list二手買或賣.Clear();
            imageList餐廚設備二手平台.Images.Clear(); // 清空ImageList

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string querySecondhand;
                // 根据选择的选项构造SQL查询
                if (selectedOption == "全部顯示")
                {
                    querySecondhand = "SELECT * FROM secondhand";
                }
                else
                {
                    querySecondhand = "SELECT * FROM secondhand WHERE shbuyorsale = @shbuyorsale";
                }

                SqlCommand cmdSecondhand = new SqlCommand(querySecondhand, con);

                // 如果不是选择"全部顯示"，则添加参数
                if (selectedOption != "全部顯示")
                {
                    cmdSecondhand.Parameters.AddWithValue("@shbuyorsale", selectedOption);
                }

                using (SqlDataReader reader = cmdSecondhand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list二手Id.Add((int)reader["shid"]);
                        list二手商品名稱.Add(reader["shpname"].ToString());
                        list二手商品價格.Add((int)reader["shprice"]);
                        list二手商品交易方式.Add(reader["shtradeway"].ToString());
                        list二手買或賣.Add(reader["shbuyorsale"].ToString());

                        string image_name = reader.IsDBNull(reader.GetOrdinal("shpimage")) ? "預設圖片.png" : reader["shpimage"].ToString();
                        string imagePath = Path.Combine(GlobalVar.image_dir, "二手", image_name);

                        try
                        {
                            var image = Image.FromFile(imagePath);
                            imageList餐廚設備二手平台.Images.Add(image);
                        }
                        catch
                        {
                            var defaultImage = Image.FromFile(Path.Combine(GlobalVar.image_dir, "二手", "預設圖片.png")); // 使用默认图片
                            imageList餐廚設備二手平台.Images.Add(defaultImage);
                        }
                    }
                }
            }

            // 使用更新后的全局变量数据来显示二手商品列表
            顯示二手listView();
        }

        private void cbox餐廚設備二手平台_排序選項_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = cbox餐廚設備二手平台_排序選項.SelectedItem.ToString(); // 获取选中的排序选项
            string connectionString = GlobalVar.strDBConnectionString; // 数据库连接字符串
            string orderByClause = "";

            // 根据选中的选项确定排序方式
            switch (selectedOption)
            {
                case "新到舊":
                    orderByClause = "shdate DESC";
                    break;
                case "舊到新":
                    orderByClause = "shdate ASC";
                    break;
                case "價格高到低":
                    orderByClause = "shprice DESC";
                    break;
                case "價格低到高":
                    orderByClause = "shprice ASC";
                    break;
            }

            list二手Id.Clear();
            list二手商品名稱.Clear();
            list二手商品價格.Clear();
            list二手商品交易方式.Clear();
            imageList餐廚設備二手平台.Images.Clear(); // 清空ImageList

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string querySecondhand = $"SELECT * FROM secondhand ORDER BY {orderByClause}";
                SqlCommand cmdSecondhand = new SqlCommand(querySecondhand, con);

                using (SqlDataReader reader = cmdSecondhand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list二手Id.Add((int)reader["shid"]);
                        list二手商品名稱.Add(reader["shpname"].ToString());
                        list二手商品價格.Add((int)reader["shprice"]);
                        list二手商品交易方式.Add(reader["shtradeway"].ToString());

                        string image_name = reader.IsDBNull(reader.GetOrdinal("shpimage")) ? "預設圖片.png" : reader["shpimage"].ToString();
                        string imagePath = Path.Combine(GlobalVar.image_dir, "二手", image_name);

                        try
                        {
                            var image = Image.FromFile(imagePath);
                            imageList餐廚設備二手平台.Images.Add(image);
                        }
                        catch
                        {
                            var defaultImage = Image.FromFile(Path.Combine(GlobalVar.image_dir, "二手", "預設圖片.png")); // 使用默认图片
                            imageList餐廚設備二手平台.Images.Add(defaultImage);
                        }
                    }
                }
            }

            // 使用更新后的全局变量数据来显示二手商品列表
            顯示二手listView();
        }


        //===============   餐廚設備二手平台  ==============================
        //
        //===============   達人接案平台  =================================

        private void btn達人接案平台_搜尋_Click(object sender, EventArgs e)
        {
            string 關鍵字_達人接案平台 = txt達人接案平台_關鍵字.Text;
            string 排除1_達人接案平台 = txt達人接案平台_排除1.Text;
            string 排除2_達人接案平台 = txt達人接案平台_排除2.Text;

            接案商品搜尋(關鍵字_達人接案平台, 排除1_達人接案平台, 排除2_達人接案平台);
        }

        private void txt達人接案平台_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn達人接案平台_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void listView達人接案平台_ItemActivate(object sender, EventArgs e)
        {
            if (listView達人接案平台.SelectedItems.Count > 0)
            {
                var selectedItem = listView達人接案平台.SelectedItems[0];
                int 接案商品ID = Convert.ToInt32(selectedItem.Tag); // 假設您已將商品ID存儲在Tag屬性中

                // 檢查 _最近看過 集合是否為 null，然後添加接案商品ID到 _最近看過
                if (Properties.Settings.Default._接案最近看過 == null)
                {
                    Properties.Settings.Default._接案最近看過 = new System.Collections.Specialized.StringCollection();
                }
                if (!Properties.Settings.Default._接案最近看過.Contains(接案商品ID.ToString()))
                {
                    Properties.Settings.Default._接案最近看過.Add(接案商品ID.ToString());
                    Properties.Settings.Default.Save(); // 保存设置
                }

                // 更新點擊紀錄，假設有一個相應的方法
                接案更新點擊紀錄(接案商品ID);


                List<int> 接案商品ID列表 = list接案Id;
                int 當前索引 = 接案商品ID列表.IndexOf(接案商品ID);


                Form接案商品詳情 form接案商品詳情 = new Form接案商品詳情(接案商品ID列表, 當前索引, this);
                form接案商品詳情.ShowDialog();
            }
        }

        private void btn達人接案平台_重新整理_Click(object sender, EventArgs e)
        {
            重新載入接案資料();
            txt達人接案平台_關鍵字.Clear();
            txt達人接案平台_排除1.Clear();
            txt達人接案平台_排除2.Clear();
        }

        private void btn達人接案平台_最近看過_Click(object sender, EventArgs e)
        {
            var 最近看過的商品IDs = Properties.Settings.Default._接案最近看過;

            if ((最近看過的商品IDs != null) && (最近看過的商品IDs.Count > 0))
            {
                string ids = string.Join(",", 最近看過的商品IDs.Cast<string>());
                // 根据IDs查询数据库并更新ListView
                更新接案ListView顯示最近看過的商品(ids);
            }
            else
            {
                //MessageBox.Show("没有最近查看的商品。");
                string message = "没有最近查看的商品。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
            }
        }

        private void btn達人接案平台_我的項目_Click(object sender, EventArgs e)
        {
            string userEmail = GlobalVar.使用者帳號; // 從GlobalVar獲取當前使用者的電子郵件地址
            string connectionString = GlobalVar.strDBConnectionString; // 數據庫連接字符串

            // 先清除之前可能存在的資料
            list接案Id.Clear();
            list接案商品名稱.Clear();
            list接案商品交易方式.Clear();
            list接案姓名.Clear();
            list接案商品地區.Clear();
            list接案年資.Clear();
            list接案商品交易模式.Clear();
            imageList達人接案平台.Images.Clear();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 查詢使用者ID
                string queryPerson = "SELECT id FROM persons WHERE email = @Email";
                SqlCommand cmdPerson = new SqlCommand(queryPerson, con);
                cmdPerson.Parameters.AddWithValue("@Email", userEmail);
                int userId = 0;

                using (SqlDataReader reader = cmdPerson.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userId = Convert.ToInt32(reader["id"]); // 讀取並轉換使用者ID
                    }
                }

                // 如果找到了使用者ID，則根據該ID查詢商品
                if (userId != 0)
                {
                    string queryExpert = "SELECT * FROM expert WHERE epcid = @UserID";
                    SqlCommand cmdSecondhand = new SqlCommand(queryExpert, con);
                    cmdSecondhand.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = cmdSecondhand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list接案Id.Add((int)reader["epid"]);
                            list接案商品名稱.Add(reader["epskill"].ToString());
                            list接案姓名.Add(reader["epname"].ToString());
                            list接案商品交易方式.Add(reader["epserviceway"].ToString());
                            list接案商品交易模式.Add(reader["epbuyorsale"].ToString());
                            list接案年資.Add(reader["epseniority"].ToString());
                            list接案商品地區.Add(reader["eprigion"].ToString());

                            string image_name = reader.IsDBNull(reader.GetOrdinal("epimage")) ? "預設圖片.png" : reader["epimage"].ToString();
                            string 完整圖檔路徑 = Path.Combine(GlobalVar.image_dir, "接案", image_name);

                            try
                            {
                                Image img = Image.FromFile(完整圖檔路徑);
                                imageList達人接案平台.Images.Add(img);
                            }
                            catch
                            {
                                // 如果指定的圖片加載失敗，則使用預設圖片
                                string 預設圖片路徑 = Path.Combine(GlobalVar.image_dir, "接案", "預設圖片.png");
                                var img = Image.FromFile(預設圖片路徑);
                                imageList達人接案平台.Images.Add(img); // 使用預設圖片
                            }
                        }
                    }
                }
            }

            // 使用更新後的全局變量資料來顯示二手商品列表
            顯示接案listView();
        }

        private void btn達人接案平台_發佈專長_Click(object sender, EventArgs e)
        {
            Form接案商品詳情 formCMPDP = new Form接案商品詳情();
            formCMPDP.ShowDialog();
        }

        private void cbox達人接案平台_只顯示選項_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = cbox達人接案平台_只顯示選項.SelectedItem.ToString(); // 获取选中的选项
            string connectionString = GlobalVar.strDBConnectionString; // 数据库连接字符串
            list接案Id.Clear();
            list接案商品名稱.Clear();
            list接案商品交易方式.Clear();
            list接案姓名.Clear();
            list接案商品地區.Clear();
            list接案年資.Clear();
            list接案商品交易模式.Clear();
            imageList達人接案平台.Images.Clear();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string querySecondhand;
                // 根据选择的选项构造SQL查询
                if (selectedOption == "全部顯示")
                {
                    querySecondhand = "SELECT * FROM expert";
                }
                else
                {
                    querySecondhand = "SELECT * FROM expert WHERE epbuyorsale = @epbuyorsale";
                }

                SqlCommand cmdSecondhand = new SqlCommand(querySecondhand, con);

                // 如果不是选择"全部顯示"，则添加参数
                if (selectedOption != "全部顯示")
                {
                    cmdSecondhand.Parameters.AddWithValue("@epbuyorsale", selectedOption);
                }

                using (SqlDataReader reader = cmdSecondhand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list接案Id.Add((int)reader["epid"]);
                        list接案商品名稱.Add(reader["epskill"].ToString());
                        list接案商品交易方式.Add(reader["epserviceway"].ToString());
                        list接案姓名.Add(reader["epname"].ToString());
                        list接案商品交易模式.Add(reader["epbuyorsale"].ToString());
                        list接案年資.Add(reader["epseniority"].ToString());
                        list接案商品地區.Add(reader["eprigion"].ToString());

                        string image_name = reader.IsDBNull(reader.GetOrdinal("epimage")) ? "預設圖片.png" : reader["epimage"].ToString();
                        string imagePath = Path.Combine(GlobalVar.image_dir, "接案", image_name);

                        try
                        {
                            var image = Image.FromFile(imagePath);
                            imageList達人接案平台.Images.Add(image);
                        }
                        catch
                        {
                            var defaultImage = Image.FromFile(Path.Combine(GlobalVar.image_dir, "接案", "預設圖片.png")); // 使用默认图片
                            imageList達人接案平台.Images.Add(defaultImage);
                        }
                    }
                }
            }

            // 使用更新后的全局变量数据来显示二手商品列表
            顯示接案listView();
        }

        //===============   達人接案平台  ==============================
        //
        //===============   購物車  ================================== 

        private void btn購物車_全選_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DataGridView購物車.Rows)
            {
                row.Cells["選取"].Value = is全選;
            }
            // 切換狀態
            is全選 = !is全選;
        }

        //-----------------------------------------------------------------------------------------------------

        private void btn購物車_刪除_Click(object sender, EventArgs e)
        {
            List<int> toDeleteIds = new List<int>();

            foreach (DataGridViewRow row in DataGridView購物車.Rows)
            {
                if (Convert.ToBoolean(row.Cells["選取"].Value))
                {
                    int 商品編號 = Convert.ToInt32(row.Cells["編號"].Value);
                    toDeleteIds.Add(商品編號);
                }
            }

            if (toDeleteIds.Count > 0)
            {
                DeleteSelectedCartItems(toDeleteIds);
                FillCartDataGridView(); // 重新加載購物車數據
            }
        }

        public void DeleteSelectedCartItems(List<int> tobuypids)
        {
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                foreach (var pid in tobuypids)
                {
                    string query = "DELETE FROM cart WHERE tobuypid = @tobuypid AND tobuycid = @tobuycid";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@tobuypid", pid);
                        cmd.Parameters.AddWithValue("@tobuycid", GlobalVar.使用者ID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        //----------------------------  保證總金額隨時更新  ----------------------------------------------------------

        private void DataGridView購物車_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 檢查改變的是否為"選取"列
            if (e.ColumnIndex == DataGridView購物車.Columns["選取"].Index && e.RowIndex != -1)
            {
                UpdateTotalAmount();
            }

        }

        private void UpdateTotalAmount()
        {
            int totalAmount = 0;
            foreach (DataGridViewRow row in DataGridView購物車.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["選取"].Value);
                if (isSelected)
                {
                    int subtotal = Convert.ToInt32(row.Cells["小計"].Value);
                    totalAmount += subtotal;
                }
            }
            lbl購物車_結帳總金額.Text = totalAmount.ToString("N0");
        }

        private void DataGridView購物車_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // 如果是 CheckBox 列的狀態改變，立即提交更改
            if (DataGridView購物車.CurrentCell is DataGridViewCheckBoxCell)
            {
                DataGridView購物車.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //-----------------------------------------------------------------------------------------------------

        private void btn購物車_重新整理_Click(object sender, EventArgs e)
        {
            FillCartDataGridView();
            lbl購物車_結帳總金額.Text = "0";
        }

        //-----------------------------------------------------------------------------------------------------

        private void btn購物車_去買單_Click(object sender, EventArgs e)
        {
            int 結帳總金額;
            string 金額整數 = lbl購物車_結帳總金額.Text;

            // 先檢查是否有選擇任何商品
            if (string.IsNullOrEmpty(金額整數))
            {
                string message = "沒有選擇任何商品!";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("沒有選擇任何商品!");
                return; // 結束方法的執行
            }

            金額整數 = 金額整數.Replace(",", "");

            // 嘗試將文本值轉換為整數
            if (int.TryParse(金額整數, out 結帳總金額))
            {
                GlobalVar.結帳總金額 = 結帳總金額;
            }
            else
            {
                // 轉換失敗時的處理，例如顯示錯誤訊息
                string message = "金額格式無效！";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("金額格式無效！");
                return; // 不執行下面的程式碼
            }

            GlobalVar.付款方式 = cbox購物車_選擇付款方式.SelectedItem.ToString().Trim();

            Console.WriteLine($"結帳總金額：{GlobalVar.結帳總金額}");
            Console.WriteLine($"付款方式：{GlobalVar.付款方式}");

            // 構建一個DataTable來存儲選中的購物車項目
            DataTable 購物車選中資料 = new DataTable();
            購物車選中資料.Columns.Add("編號", typeof(int));
            購物車選中資料.Columns.Add("商品名稱", typeof(string));
            購物車選中資料.Columns.Add("商品規格", typeof(string));
            購物車選中資料.Columns.Add("單價", typeof(int));
            購物車選中資料.Columns.Add("數量", typeof(int));
            購物車選中資料.Columns.Add("小計", typeof(int));

            bool 有選中商品 = false;
            foreach (DataGridViewRow row in DataGridView購物車.Rows)
            {
                if (Convert.ToBoolean(row.Cells["選取"].Value))
                {
                    有選中商品 = true;
                    購物車選中資料.Rows.Add(
                        row.Cells["編號"].Value,
                        row.Cells["商品名稱"].Value,
                        row.Cells["商品規格"].Value,
                        row.Cells["單價"].Value,
                        row.Cells["數量"].Value,
                        row.Cells["小計"].Value
                    );
                }
            }

            if (!有選中商品)
            {
                string message = "請至少選擇一項商品進行結帳。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請至少選擇一項商品進行結帳。");
                return;
            }

            FillCartDataGridView();
            lbl購物車_結帳總金額.Text = "0";

            Form結帳畫面 form結帳畫面 = new Form結帳畫面(購物車選中資料, this);

            form結帳畫面.ShowDialog(); // 先顯示結帳畫面
        }

        //-----------------------------------------------------------------------------------------------------
        public void 更新購物車()
        {
            FillCartDataGridView();
        }

        public void FillCartDataGridView()
        {
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                string query = @"
                SELECT 
                c.tobuypid AS '編號', 
                p.pname AS '商品名稱', 
                p.pmode AS '商品規格', 
                p.price AS '單價', 
                SUM(c.tobuyquantity) AS '數量', 
                p.price * SUM(c.tobuyquantity) AS '小計'
                FROM cart c
                JOIN products p ON c.tobuypid = p.pid
                WHERE c.tobuycid = @購買者ID
                GROUP BY c.tobuypid, p.pname, p.pmode, p.price";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@購買者ID", GlobalVar.使用者ID);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        DataGridView購物車.DataSource = dataTable;

                        // 確保先清除現有的自定義列
                        if (DataGridView購物車.Columns["選取"] != null) DataGridView購物車.Columns.Remove("選取");
                        if (DataGridView購物車.Columns["刪除"] != null) DataGridView購物車.Columns.Remove("刪除");

                        // 添加Checkbox列
                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.Name = "選取";
                        checkBoxColumn.HeaderText = "選取";
                        checkBoxColumn.Width = 50;
                        checkBoxColumn.TrueValue = true;
                        checkBoxColumn.FalseValue = false;

                        DataGridView購物車.Columns.Insert(0, checkBoxColumn); // 將其插入到第一列

                        // 添加Button列
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.Name = "刪除";
                        buttonColumn.HeaderText = "刪除";
                        buttonColumn.Text = "刪除";
                        buttonColumn.UseColumnTextForButtonValue = true; // 這樣每個按鈕都會顯示"刪除"
                        buttonColumn.Width = 100;
                        DataGridView購物車.Columns.Add(buttonColumn); // 添加到最後一列

                        // 設置列寬
                        DataGridView購物車.Columns["編號"].Width = 50;
                        DataGridView購物車.Columns["商品名稱"].Width = 340;
                        DataGridView購物車.Columns["商品規格"].Width = 310;
                        DataGridView購物車.Columns["單價"].Width = 100;
                        DataGridView購物車.Columns["數量"].Width = 80;
                        DataGridView購物車.Columns["小計"].Width = 125;


                        // 設置其他列為只讀
                        foreach (DataGridViewColumn column in DataGridView購物車.Columns)
                        {
                            if (column.Name != "選取" && column.Name != "刪除")
                            {
                                column.ReadOnly = true;
                            }
                        }

                        // 在FillCartDataGridView方法的最後添加
                        int 總金額 = dataTable.AsEnumerable().Sum(row => row.Field<int>("小計"));
                        lbl購物車_結帳總金額.Text = 總金額.ToString("N0"); // 格式化為帶有千位分隔符的整數格式
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------------

        public void RemovePurchasedItems(List<int> purchasedItemIds)
        {
            foreach (var id in purchasedItemIds)
            {
                foreach (DataGridViewRow row in DataGridView購物車.Rows)
                {
                    if (row.Cells["編號"].Value.ToString() == id.ToString())
                    {
                        DataGridView購物車.Rows.Remove(row);
                    }
                }
            }
            更新購物車(); // 可能需要更新界面或重新計算總金額
        }
        //-----------------------------------------------------------------------------------------------

        private void DataGridView購物車_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 檢查是否點擊了刪除按鈕列
            if (e.ColumnIndex == DataGridView購物車.Columns["刪除"].Index && e.RowIndex >= 0)
            {
                // 獲取選中行的商品編號
                int 商品編號 = Convert.ToInt32(DataGridView購物車.Rows[e.RowIndex].Cells["編號"].Value);

                // 從資料庫中刪除該商品
                DeleteCartItem(商品編號);

                // 重新載入DataGridView
                FillCartDataGridView();
            }
        }

        private void DeleteCartItem(int tobuypid)
        {
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                string query = "DELETE FROM cart WHERE tobuypid = @tobuypid AND tobuycid = @tobuycid";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@tobuypid", tobuypid);
                    cmd.Parameters.AddWithValue("@tobuycid", GlobalVar.使用者ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //-------------------------------------------------------
        //===============   購物車  ==================================
        //
        //===============   購買清單  ==================================
        private void FillOrdersDataGridView()
        {
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                string query = $@"
                SELECT 
                o.oid AS '訂單編號', 
                o.ocustomerid AS '會員ID', 
                p.姓名 AS '訂購人',  
                o.ostatus AS '訂單狀態', 
                o.orderpayway AS '付款方式', 
                o.odesc AS '備註'
                FROM orders o
                JOIN persons p ON o.ocustomerid = p.id
                ORDER BY o.odate {currentSortOrder}";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // 假設您的DataGridView命名為DataGridView購買清單
                        DataGridView購買清單.DataSource = dataTable;

                        // 設置列寬，適當調整以符合您的UI設計
                        DataGridView購買清單.Columns["訂單編號"].Width = 100;
                        DataGridView購買清單.Columns["會員ID"].Width = 80;  // 新增的會員ID欄位
                        DataGridView購買清單.Columns["訂購人"].Width = 140;
                        DataGridView購買清單.Columns["訂單狀態"].Width = 100;
                        DataGridView購買清單.Columns["付款方式"].Width = 100;
                        DataGridView購買清單.Columns["備註"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // 備註欄位填滿剩餘空間

                        // 設置文字對齊方式
                        DataGridView購買清單.Columns["訂單編號"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView購買清單.Columns["會員ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView購買清單.Columns["訂購人"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView購買清單.Columns["訂單狀態"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView購買清單.Columns["付款方式"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void FillPurchaseList(string orderID)
        {
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                string query = @"
                SELECT 
                CONVERT(NVARCHAR(50), oc.ocoid) AS '訂單編號', 
                CONVERT(NVARCHAR(50), oc.ocpid) AS '商品編號', 
                p.pname AS '商品名稱', 
                p.pmode AS '商品規格', 
                p.ptype AS '商品類型', 
                p.price AS '單價',
                CONVERT(NVARCHAR(50), oc.ocquantity) AS '購買數量',
                (p.price * oc.ocquantity) AS '小計'
                FROM ordercontain oc
                JOIN products p ON oc.ocpid = p.pid
                WHERE oc.ocoid = @訂單編號";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@訂單編號", orderID);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // 將資料綁定到DataGridView
                        DataGridView購買清單_顯示商品.DataSource = dataTable;

                        // 計算總和
                        if (dataTable.Rows.Count > 0)
                        {
                            var total = dataTable.AsEnumerable()
                                .Sum(row => row.Field<int>("小計"));

                            // 設定lbl購買清單_訂單總價數值的文字
                            lbl購買清單_訂單總價數值.Text = total.ToString("N0"); // "N0" 格式化為不帶小數的數字
                        }
                        else
                        {
                            lbl購買清單_訂單總價數值.Text = "0"; // 如果沒有資料，設定為0
                        }
                        // 設定列寬
                        DataGridView購買清單_顯示商品.Columns["訂單編號"].Width = 100;
                        DataGridView購買清單_顯示商品.Columns["訂單編號"].Visible = false;
                        DataGridView購買清單_顯示商品.Columns["商品編號"].Width = 100;
                        DataGridView購買清單_顯示商品.Columns["商品編號"].Visible = false;
                        DataGridView購買清單_顯示商品.Columns["商品名稱"].Width = 300;
                        DataGridView購買清單_顯示商品.Columns["商品規格"].Width = 320;
                        DataGridView購買清單_顯示商品.Columns["商品類型"].Width = 200;
                        DataGridView購買清單_顯示商品.Columns["購買數量"].Width = 100;
                        DataGridView購買清單_顯示商品.Columns["小計"].Width = 100;
                        DataGridView購買清單_顯示商品.Columns["小計"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }

        private void DataGridView購買清單_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 取得選取的資料行
                DataGridViewRow selectedRow = DataGridView購買清單.Rows[e.RowIndex];

                // 獲取選取資料行中的訂單編號（YourOrderID）
                string orderID = selectedRow.Cells["訂單編號"].Value.ToString();

                // 在DataGridView購買清單_顯示商品中顯示該訂單的相關資訊，您可以使用FillPurchaseList或類似的方法
                FillPurchaseList(orderID);

                // 設定lbl購買清單_訂單編號數值的文字

                lbl購買清單_訂單編號數值.Text = orderID;
            }
        }

        private void btn購買清單_聯繫客服_Click(object sender, EventArgs e)
        {
            // 指定要打開的連結
            string url = "https://line.me/ti/p/9-exJnHnXQ";
            System.Diagnostics.Process.Start(url);
        }

        private void btn購買清單_重新整理_Click(object sender, EventArgs e)
        {
            FillOrdersDataGridView();

            lbl購買清單_訂單編號數值.Text = "";
            lbl購買清單_訂單總價數值.Text = "0";

            // 清空顯示商品的 DataGridView
            DataGridView購買清單_顯示商品.DataSource = null; // 如果您是用 DataSource 綁定的資料，這樣做將清空 DataGridView
            DataGridView購買清單_顯示商品.Rows.Clear(); // 直接清空所有行，適用於沒有使用 DataSource 或需要額外清除行的情況

            // 另外，如果您設置了任何標題或其他相關的UI元素，也可以在這裡重置它們
        }

        private void btn購買清單_新舊排序_Click(object sender, EventArgs e)
        {
            // 切換排序方向
            currentSortOrder = currentSortOrder == "DESC" ? "ASC" : "DESC";

            // 根據新的排序方向重新填充 DataGridView
            FillOrdersDataGridView();
        }

        //===============   購買清單  ==================================
        //
        //===============   會員系統  ==================================

        private void btn會員系統_送出_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                conn.Open();
                string sql = "SELECT 密碼, 姓名 FROM persons WHERE email = @Email";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", txt會員系統_Email.Text.Trim());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string dbPassword = reader["密碼"].ToString();
                        string dbName = reader["姓名"].ToString();

                        bool is密碼正確 = dbPassword == txt會員系統_密碼.Text.Trim();
                        bool isNewPasswordDifferent = !string.IsNullOrEmpty(txt會員系統_新密碼.Text) && txt會員系統_新密碼.Text.Trim() != dbPassword;
                        bool isNewPasswordConfirmed = txt會員系統_新密碼.Text == txt會員系統_新密碼確認.Text;
                        bool is姓名不同 = txt會員系統_姓名.Text.Trim() != dbName;

                        if (is密碼正確 && (is姓名不同 || (isNewPasswordDifferent && isNewPasswordConfirmed)))
                        {
                            string updateSql = "UPDATE persons SET 姓名 = @NewName" +
                                               (isNewPasswordDifferent && isNewPasswordConfirmed ? ", 密碼 = @NewPassword" : "") +
                                               " WHERE email = @Email";

                            SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                            updateCmd.Parameters.AddWithValue("@NewName", txt會員系統_姓名.Text.Trim());
                            if (isNewPasswordDifferent && isNewPasswordConfirmed)
                            {
                                updateCmd.Parameters.AddWithValue("@NewPassword", txt會員系統_新密碼.Text.Trim());
                            }
                            updateCmd.Parameters.AddWithValue("@Email", txt會員系統_Email.Text.Trim());

                            conn.Close(); // Close the reader connection
                            conn.Open(); // Reopen for update command
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                string message = "更新成功！";
                                using (Form對話框 對話框 = new Form對話框(message))
                                {
                                    對話框.ShowDialog();
                                }
                                //MessageBox.Show("更新成功！");
                            }
                            else
                            {
                                string message = "更新失敗！";
                                using (Form對話框 對話框 = new Form對話框(message))
                                {
                                    對話框.ShowDialog();
                                }
                                //MessageBox.Show("更新失敗！");
                            }
                        }
                        else
                        {
                            // 提示信息根據情況調整以提供更準確的反饋
                            if (!is密碼正確)
                            {
                                string message = "密碼不正確！";
                                using (Form對話框 對話框 = new Form對話框(message))
                                {
                                    對話框.ShowDialog();
                                }
                                //MessageBox.Show("密碼不正確！");
                            }
                            else if (!isNewPasswordConfirmed)
                            {
                                string message = "新密碼與確認密碼不匹配！";
                                using (Form對話框 對話框 = new Form對話框(message))
                                {
                                    對話框.ShowDialog();
                                }
                                //MessageBox.Show("新密碼與確認密碼不匹配！");
                            }
                            else if (!isNewPasswordDifferent)
                            {
                                string message = "新密碼不能與原密碼相同！";
                                using (Form對話框 對話框 = new Form對話框(message))
                                {
                                    對話框.ShowDialog();
                                }
                                //MessageBox.Show("新密碼不能與原密碼相同！");
                            }
                        }
                    }
                    else
                    {
                        string message = "找不到使用者資料！";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("找不到使用者資料！");
                    }
                }
            }
        }

        private void btn會員系統_清空_Click(object sender, EventArgs e)
        {
            清空會員系統();
        }

        void 清空會員系統()
        {
            txt會員系統_密碼.Clear();
            txt會員系統_新密碼.Clear();
            txt會員系統_新密碼確認.Clear();
            txt會員系統_Email.Clear();
            txt會員系統_姓名.Clear();
            // txt會員系統_Email.Clear(); // 根據需求決定是否清空
        }

        private void btn會員系統_刪除此帳號_Click(object sender, EventArgs e)
        {
            using (Form對話框 對話框 = new Form對話框($"是否確定刪除此帳號:\n{GlobalVar.使用者帳號}?"))
            {
                var dialogResult = 對話框.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    using (SqlConnection conn = new SqlConnection(GlobalVar.strDBConnectionString))
                    {
                        conn.Open();
                        // 使用 GlobalVar 中的使用者帳號和密碼進行刪除操作
                        string sql = "DELETE FROM persons WHERE email = @Email AND 密碼 = @Password";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Email", GlobalVar.使用者帳號); // 假設 GlobalVar.使用者帳號 儲存的是 email
                        cmd.Parameters.AddWithValue("@Password", GlobalVar.使用者密碼);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // 這裡再次使用自定義對話框來顯示帳號刪除成功的訊息
                            using (Form對話框 成功對話框 = new Form對話框("帳號刪除成功！"))
                            {
                                成功對話框.ShowDialog();
                            }
                            // 登出操作
                            UserSession.登出();
                            lbl首頁_使用者名稱.Text = "姓名";
                            lbl首頁_登入帳號.Text = "帳號";
                            lbl首頁_管理身分組.Text = "身分組";
                            // 更新UI或跳轉操作，例如回到登入頁面
                            Form使用者登入 form使用者登入 = new Form使用者登入();
                            form使用者登入.ShowDialog();
                        }
                        else
                        {
                            // 使用自定義對話框顯示帳號刪除失敗的訊息
                            using (Form對話框 失敗對話框 = new Form對話框("帳號刪除失敗，無法找到匹配的帳號或密碼錯誤。"))
                            {
                                失敗對話框.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private void btn會員系統_修改密碼_Click(object sender, EventArgs e)
        {
            // 判斷是否已登入且使用者權限足夠
            if (GlobalVar.is登入成功 == true && GlobalVar.使用者權限 >= 1000)
            {
                // 使用 ! 運算符來切換 showpassword 的值
                bool showpassword = !txt會員系統_新密碼.Visible;

                // 根據 showpassword 的值來顯示或隱藏元素
                txt會員系統_新密碼.Visible = showpassword;
                txt會員系統_新密碼確認.Visible = showpassword;
                lbl會員系統_新密碼.Visible = showpassword;
                lbl會員系統_新密碼確認.Visible = showpassword;
            }
        }

        //===============   會員系統  ========================================
        //
        //===============   管理者系統_商品管理  ==================================

        public void FillProductsDataGridView()
        {
            string query = "SELECT pid, pname, pmode, price, pdesc, ptype,pimage FROM products";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable table = new DataTable();

                try
                {
                    con.Open();
                    adapter.Fill(table);

                    // 先清空DataGridView現有的列和行
                    DataGridView商品管理.Columns.Clear();
                    DataGridView商品管理.Rows.Clear();

                    //添加選取列
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                    checkBoxColumn.Name = "選取";
                    checkBoxColumn.HeaderText = "選取";
                    DataGridView商品管理.Columns.Insert(0, checkBoxColumn); // 插入到最前面

                    // 添加其他列
                    DataGridView商品管理.Columns.Add("pid", "編號");
                    DataGridView商品管理.Columns.Add("pname", "商品名稱");
                    DataGridView商品管理.Columns.Add("pmode", "商品規格");
                    DataGridView商品管理.Columns.Add("price", "單價");
                    DataGridView商品管理.Columns.Add("ptype", "商品類型");

                    // 添加說明按鈕列
                    DataGridViewButtonColumn descButtonColumn = new DataGridViewButtonColumn();
                    descButtonColumn.Name = "pdesc";
                    descButtonColumn.HeaderText = "說明";
                    descButtonColumn.Text = "查看說明";
                    descButtonColumn.UseColumnTextForButtonValue = true;
                    DataGridView商品管理.Columns.Add(descButtonColumn);

                    // 添加一個隱藏列用於存儲pimage的值
                    DataGridViewTextBoxColumn pimageColumn = new DataGridViewTextBoxColumn();
                    pimageColumn.Name = "pimage";
                    pimageColumn.HeaderText = "圖片名稱";
                    pimageColumn.Visible = false; // 設置為隱藏
                    DataGridView商品管理.Columns.Add(pimageColumn);

                    // 設定行頭的寬度
                    DataGridView商品管理.RowHeadersWidth = 25; // 這裡設定行頭的寬度，可以根據需要調整

                    // 手動設定特定欄位的寬度
                    DataGridView商品管理.Columns["pid"].Width = 50; // 編號欄位的寬度
                    DataGridView商品管理.Columns["pmode"].Width = 350; // 商品規格欄位的寬度
                    DataGridView商品管理.Columns["price"].Width = 130; // 單價欄位的寬度
                    DataGridView商品管理.Columns["ptype"].Width = 150; // 商品類型欄位的寬度
                    DataGridView商品管理.Columns["選取"].Width = 50; // 選取欄位的寬度
                    DataGridView商品管理.Columns["pdesc"].Width = 100; // 說明按鈕欄位的寬度

                    // 設定「商品名稱」欄位自動填滿剩餘寬度
                    DataGridView商品管理.Columns["pname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    // 為了確保「商品名稱」欄位能夠自動填滿剩餘寬度，其他欄位應設定為不自動調整大小
                    foreach (DataGridViewColumn column in DataGridView商品管理.Columns)
                    {
                        if (column.Name != "pname")
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        }
                    }

                    // 填充數據到DataGridView，注意不直接添加pdesc值
                    foreach (DataRow row in table.Rows)
                    {
                        // 將pdesc存儲在Tag屬性中以便後續訪問，並直接添加pimage值到對應的列
                        int index = DataGridView商品管理.Rows.Add(new object[] { false, row["pid"], row["pname"], row["pmode"], row["price"], row["ptype"], "查看說明", row["pimage"] });
                        DataGridView商品管理.Rows[index].Cells["pdesc"].Tag = row["pdesc"];
                        // 注意：不需要對pimage做特別處理，因為它已經作為隱藏列的一部分被添加
                    }
                }
                catch (Exception ex)
                {
                    string message = "數據加載失敗: "+ ex.Message;
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show("數據加載失敗: ",ex.Message);
                }
            }
        }

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;

        protected virtual void OnDataChanged()
        {
            if (DataChanged != null)
            {
                DataChanged(this, EventArgs.Empty);
            }
        }

        private void Form商品管理_ProductChanged(object sender, EventArgs e)
        {
            FillProductsDataGridView(); // 刷新DataGridView
        }

        private void FillDataGridView(DataTable table)
        {
            // 清空DataGridView現有的列和行
            DataGridView商品管理.Columns.Clear();
            DataGridView商品管理.Rows.Clear();

            // 添加選取列
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Name = "選取";
            checkBoxColumn.HeaderText = "選取";
            DataGridView商品管理.Columns.Insert(0, checkBoxColumn);

            // 添加其他列
            DataGridView商品管理.Columns.Add("pid", "編號");
            DataGridView商品管理.Columns.Add("pname", "商品名稱");
            DataGridView商品管理.Columns.Add("pmode", "商品規格");
            DataGridView商品管理.Columns.Add("price", "單價");
            DataGridView商品管理.Columns.Add("ptype", "商品類型");

            // 添加說明按鈕列
            DataGridViewButtonColumn descButtonColumn = new DataGridViewButtonColumn();
            descButtonColumn.Name = "pdesc";
            descButtonColumn.HeaderText = "說明";
            descButtonColumn.Text = "查看說明";
            descButtonColumn.UseColumnTextForButtonValue = true;
            DataGridView商品管理.Columns.Add(descButtonColumn);

            // 添加隱藏的pimage列
            DataGridViewTextBoxColumn pimageColumn = new DataGridViewTextBoxColumn();
            pimageColumn.Name = "pimage";
            pimageColumn.HeaderText = "圖片名稱";
            pimageColumn.Visible = false; // 設置為隱藏
            DataGridView商品管理.Columns.Add(pimageColumn);

            // 設定行頭的寬度
            DataGridView商品管理.RowHeadersWidth = 25;

            // 手動設定特定欄位的寬度
            // 注意：您可能需要根據實際情況調整這些寬度
            DataGridView商品管理.Columns["pid"].Width = 50;
            DataGridView商品管理.Columns["pmode"].Width = 350;
            DataGridView商品管理.Columns["price"].Width = 130;
            DataGridView商品管理.Columns["ptype"].Width = 150;
            DataGridView商品管理.Columns["選取"].Width = 50;
            DataGridView商品管理.Columns["pdesc"].Width = 100;

            // 設定商品名稱欄位自動填滿剩餘寬度
            DataGridView商品管理.Columns["pname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // 填充數據到DataGridView
            foreach (DataRow row in table.Rows)
            {
                int index = DataGridView商品管理.Rows.Add(new object[] {
            false, row["pid"], row["pname"], row["pmode"], row["price"], row["ptype"], "查看說明", row["pimage"]
        });
                DataGridView商品管理.Rows[index].Cells["pdesc"].Tag = row["pdesc"]; // 將說明存儲在Tag屬性中
            }
        }

        private void DataGridView商品管理_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // 确保访问的列是"查看說明"按钮列
                if (e.ColumnIndex == senderGrid.Columns["pdesc"].Index)
                {
                    var pidCell = senderGrid.Rows[e.RowIndex].Cells["pid"];
                    var pnameCell = senderGrid.Rows[e.RowIndex].Cells["pname"];
                    var pmodeCell = senderGrid.Rows[e.RowIndex].Cells["pmode"];
                    var priceCell = senderGrid.Rows[e.RowIndex].Cells["price"];
                    var ptypeCell = senderGrid.Rows[e.RowIndex].Cells["ptype"];
                    var pdescCell = senderGrid.Rows[e.RowIndex].Cells["pdesc"].Tag; // 使用Tag属性存储pdesc值
                    var pimageCell = senderGrid.Rows[e.RowIndex].Cells["pimage"];

                    // 检查单元格值是否为null
                    string pid = pidCell?.Value?.ToString() ?? "";
                    string pname = pnameCell?.Value?.ToString() ?? "";
                    string pmode = pmodeCell?.Value?.ToString() ?? "";
                    string price = priceCell?.Value?.ToString() ?? "";
                    string ptype = ptypeCell?.Value?.ToString() ?? "";
                    string pdesc = pdescCell?.ToString() ?? ""; // Tag可能为null
                    string pimage = pimageCell?.Value?.ToString() ?? "";


                    // 打開Form商品管理並傳遞數據
                    using (Form商品管理 form商品管理 = new Form商品管理(pid))
                    {
                        // 傳遞數據到Form商品管理的相應控件
                        form商品管理.txt商品管理_商品編號.Text = pid;
                        form商品管理.txt商品管理_商品名稱.Text = pname;
                        form商品管理.txt商品管理_銷售模式.Text = pmode;
                        form商品管理.txt商品管理_商品金額.Text = price;
                        form商品管理.txt商品管理_商品類型.Text = ptype;
                        form商品管理.txt商品管理_商品描述.Text = pdesc;
                        form商品管理.txt商品管理_商品編號.ReadOnly = true; // 設為唯讀

                        // 處理圖片顯示
                        var imagePath = Path.Combine(GlobalVar.image_dir, pimage);
                        var 預設圖片路徑 = Path.Combine(GlobalVar.image_dir, "預設照片.png");
                        try
                        {
                            using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                            {
                                form商品管理.pictureBox商品管理.Image = Image.FromStream(stream);
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            // 如果找不到圖片，可以設置一個預設圖片或不做操作
                            using (FileStream stream = new FileStream(預設圖片路徑, FileMode.Open, FileAccess.Read))
                            {
                                form商品管理.pictureBox商品管理.Image = Image.FromStream(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            // 處理其他可能的異常，例如文件訪問權限問題
                            string message = $"無法加載圖片: {ex.Message}";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show($"無法加載圖片: {ex.Message}");
                            // 可以考慮在這裡加載一個預設圖片
                        }

                        form商品管理.ProductChanged += Form商品管理_ProductChanged;
                        form商品管理.ShowDialog();
                    }
                }
            }
        }

        private void DataGridView商品管理_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 排除非資料庫字段列
            var editedColumn = DataGridView商品管理.Columns[e.ColumnIndex].Name;
            if (editedColumn == "選取" || editedColumn == "pdesc") return; // 不更新這些列

            // 獲取編輯後的單元格值和pid
            var newValue = DataGridView商品管理.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            var pid = DataGridView商品管理.Rows[e.RowIndex].Cells["pid"].Value.ToString();

            // 構造並執行SQL更新語句
            string sql = $"UPDATE products SET {editedColumn} = @newValue WHERE pid = @pid";
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.Parameters.AddWithValue("@pid", pid);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        string message = "資料更新成功";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                    }
                    else
                    {
                        string message = "資料更新失敗";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                    }
                }
            }
        }

        private void btn商品管理_搜尋_Click(object sender, EventArgs e)
        {
            string keyword = txt商品管理_關鍵字.Text.Trim(); // 確保關鍵字前後無多餘空格

            if (string.IsNullOrEmpty(keyword))
            {
                string message = "請輸入搜索關鍵字。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請輸入搜索關鍵字。");
                return;
            }

            string query = "SELECT pid, pname, pmode, price, pdesc, ptype, pimage FROM products WHERE pname LIKE @Keyword";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        FillDataGridView(table); // 假設FillDataGridView是您已經實現的，用於將DataTable中的數據填充到DataGridView的方法
                    }
                    else
                    {
                        string message = "未找到匹配的商品。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("未找到匹配的商品。");
                    }
                }
                catch (Exception ex)
                {
                    string message = $"數據加載失敗: {ex.Message}";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show($"數據加載失敗: {ex.Message}");
                }
            }
        }

        private void txt商品管理_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn商品管理_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void cbox商品管理_依據_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cbox商品管理_依據.SelectedItem.ToString();
            string query = $"SELECT pid, pname, pmode, price, pdesc, ptype, pimage FROM products WHERE ptype = @SelectedType";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@SelectedType", selectedType);
                DataTable table = new DataTable();

                try
                {
                    con.Open();
                    adapter.Fill(table);
                    FillDataGridView(table);
                }
                catch (Exception ex)
                {
                    string message = $"數據加載失敗: {ex.Message}";
                    using (Form對話框 對話框 = new Form對話框(message))
                    {
                        對話框.ShowDialog();
                    }
                    //MessageBox.Show("數據加載失敗: " + ex.Message);
                }
            }
        }

        private void btn商品管理_重新整理_Click(object sender, EventArgs e)
        {
            FillProductsDataGridView();
            cbox商品管理_依據.SelectedIndex = 0;
            txt商品管理_關鍵字.Text = string.Empty;
        }

        private void btn商品管理_上架新商品_Click(object sender, EventArgs e)
        {
            using (Form商品管理 form商品管理 = new Form商品管理())
            {
                // 設定為新增商品模式，可以根據Form商品管理內部邏輯進行調整
                form商品管理.txt商品管理_商品編號.ReadOnly = true; // 設置商品編號為唯讀
                form商品管理.pictureBox商品管理.Image = Image.FromFile(Path.Combine(GlobalVar.image_dir, "上傳圖片.png")); // 設置默認圖片

                // 其他初始化設定，比如清空文本框等
                form商品管理.ProductChanged += Form商品管理_ProductChanged;
                form商品管理.ShowDialog(); // 顯示Form商品管理
            }
        }

        private void btn商品管理_刪除選中的商品_Click(object sender, EventArgs e)
        {
            List<string> idsToDelete = new List<string>();

            // 遍歷DataGridView找出被選中的行
            foreach (DataGridViewRow row in DataGridView商品管理.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["選取"] as DataGridViewCheckBoxCell;
                bool isSelected = Convert.ToBoolean(checkBoxCell.Value);
                if (isSelected)
                {
                    idsToDelete.Add(row.Cells["pid"].Value.ToString()); // 添加要刪除的商品ID
                }
            }

            // 如果沒有選中任何商品，提示並返回
            if (idsToDelete.Count == 0)
            {
                string message = "請選擇至少一個商品進行刪除。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("請選擇至少一個商品進行刪除。");
                return;
            }

            // 進行刪除操作
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                foreach (string id in idsToDelete)
                {
                    string query = "DELETE FROM products WHERE pid = @pid";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@pid", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // 刪除後重新加載DataGridView
            FillProductsDataGridView(); // 假設FillProductsDataGridView()是用來加載商品數據的方法
        }


        //===============   管理者系統_商品管理  ==================================
        //
        //===============   管理者系統_訂單管理 ==================================
        private void FillOrdersDataGridViewManage()
        {
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                string query = $@"
                SELECT 
                o.oid AS '訂單編號', 
                o.ocustomerid AS '會員ID', 
                p.姓名 AS '訂購人',  
                o.ostatus AS '訂單狀態', 
                o.orderpayway AS '付款方式', 
                o.odesc AS '備註'
                FROM orders o
                JOIN persons p ON o.ocustomerid = p.id
                ORDER BY o.odate {currentSortOrder}";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // 假設您的DataGridView命名為DataGridView購買清單
                        DataGridView訂單管理_顯示訂單.DataSource = dataTable;

                        // 設置列寬，適當調整以符合您的UI設計
                        DataGridView訂單管理_顯示訂單.Columns["訂單編號"].Width = 100;
                        DataGridView訂單管理_顯示訂單.Columns["會員ID"].Width = 80;  // 新增的會員ID欄位
                        DataGridView訂單管理_顯示訂單.Columns["訂購人"].Width = 140;
                        DataGridView訂單管理_顯示訂單.Columns["訂單狀態"].Width = 100;
                        DataGridView訂單管理_顯示訂單.Columns["付款方式"].Width = 100;
                        DataGridView訂單管理_顯示訂單.Columns["備註"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // 備註欄位填滿剩餘空間

                        // 設置特定列為只讀
                        DataGridView訂單管理_顯示訂單.Columns["訂單編號"].ReadOnly = true;
                        DataGridView訂單管理_顯示訂單.Columns["會員ID"].ReadOnly = true;
                        DataGridView訂單管理_顯示訂單.Columns["訂購人"].ReadOnly = true;

                        // 設置文字對齊方式
                        DataGridView訂單管理_顯示訂單.Columns["訂單編號"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["會員ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["訂購人"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["訂單狀態"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["付款方式"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void FillPurchaseListManage(string orderID)
        {
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                string query = @"
                SELECT 
                CONVERT(NVARCHAR(50), oc.ocoid) AS '訂單編號', 
                CONVERT(NVARCHAR(50), oc.ocpid) AS '商品編號', 
                p.pname AS '商品名稱', 
                p.pmode AS '商品規格', 
                p.ptype AS '商品類型', 
                p.price AS '單價',
                CONVERT(NVARCHAR(50), oc.ocquantity) AS '購買數量',
                (p.price * oc.ocquantity) AS '小計'
                FROM ordercontain oc
                JOIN products p ON oc.ocpid = p.pid
                WHERE oc.ocoid = @訂單編號";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@訂單編號", orderID);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // 將資料綁定到DataGridView
                        DataGridView訂單管理_訂單詳情.DataSource = dataTable;

                        // 計算總和
                        if (dataTable.Rows.Count > 0)
                        {
                            var total = dataTable.AsEnumerable()
                                .Sum(row => row.Field<int>("小計"));

                            // 設定lbl購買清單_訂單總價數值的文字
                            lbl訂單管理_訂單總價數值.Text = total.ToString("N0"); // "N0" 格式化為不帶小數的數字
                        }
                        else
                        {
                            lbl訂單管理_訂單總價數值.Text = "0"; // 如果沒有資料，設定為0
                        }
                        // 設定列寬
                        DataGridView訂單管理_訂單詳情.Columns["訂單編號"].Width = 100;
                        DataGridView訂單管理_訂單詳情.Columns["訂單編號"].Visible = false;
                        DataGridView訂單管理_訂單詳情.Columns["商品編號"].Width = 100;
                        DataGridView訂單管理_訂單詳情.Columns["商品編號"].Visible = false;
                        DataGridView訂單管理_訂單詳情.Columns["商品名稱"].Width = 280;
                        DataGridView訂單管理_訂單詳情.Columns["商品規格"].Width = 280;
                        DataGridView訂單管理_訂單詳情.Columns["商品類型"].Width = 200;
                        DataGridView訂單管理_訂單詳情.Columns["購買數量"].Width = 100;
                        DataGridView訂單管理_訂單詳情.Columns["小計"].Width = 100;
                        DataGridView訂單管理_訂單詳情.Columns["小計"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        // 將所有列設為唯讀，然後將 '購買數量' 列設為可編輯
                        foreach (DataGridViewColumn column in DataGridView訂單管理_訂單詳情.Columns)
                        {
                            column.ReadOnly = true; // 預設將所有列設為唯讀
                        }

                        // 將 '購買數量' 列設為可編輯
                        DataGridView訂單管理_訂單詳情.Columns["購買數量"].ReadOnly = false;

                    }
                }
            }
        }

        private void DataGridView訂單管理_顯示訂單_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView)sender;
            var row = dataGridView.Rows[e.RowIndex];
            var orderId = row.Cells["訂單編號"].Value.ToString();
            var columnName = dataGridView.Columns[e.ColumnIndex].Name;
            var newValue = row.Cells[e.ColumnIndex].Value.ToString();

            UpdateOrder(orderId, columnName, newValue);
        }

        private void UpdateOrder(string orderId, string columnName, string newValue)
        {
            // 將界面上的列名映射到資料庫的實際列名
            var columnMapping = new Dictionary<string, string>
             {
                 { "訂單編號", "oid" },
                 { "會員ID", "ocustomerid" },
                 { "訂購人", "name" }, // 假設"訂購人"在persons表中對應到名為name的列
                 { "訂單狀態", "ostatus" },
                 { "付款方式", "orderpayway" },
                 { "備註", "odesc" } // 確認這是正確的映射
             };

            // 檢查是否存在映射，如果不存在，直接使用columnName
            var dbColumnName = columnMapping.ContainsKey(columnName) ? columnMapping[columnName] : columnName;

            // 構建SQL查詢
            string query = $"UPDATE orders SET {dbColumnName} = @newValue WHERE oid = @orderId";

            // 執行SQL查詢
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // 使用正確的參數值
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    // 執行查詢
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string message = "訂單更新成功。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("訂單更新成功。");
                    }
                    else
                    {
                        string message = "訂單更新失敗。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("訂單更新失敗。");
                    }
                }
            }
        }

        private void DataGridView訂單管理_訂單詳情_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView)sender;
            if (e.ColumnIndex == dataGridView.Columns["購買數量"].Index) // 確保是在編輯 '購買數量' 列
            {
                var editedRow = dataGridView.Rows[e.RowIndex];
                var orderContainId = editedRow.Cells["訂單編號"].Value.ToString(); // 假設ocoid對應訂單內容的唯一ID
                var productId = editedRow.Cells["商品編號"].Value.ToString(); // 商品編號
                var newQuantityStr = editedRow.Cells["購買數量"].Value.ToString(); // 新的購買數量

                // 確認新的購買數量是有效的整數
                int newQuantity;
                if (!int.TryParse(newQuantityStr, out newQuantity))
                {
                    ShowDialog("購買數量必須是有效的整數。"); // 或使用自定義對話框
                    return;
                }

                // 更新資料庫中的購買數量
                UpdateOrderContain(orderContainId, productId, newQuantity);

                // 立即重新計算並更新 '小計' 的值
                var priceStr = editedRow.Cells["單價"].Value.ToString();
                decimal price;
                if (decimal.TryParse(priceStr, out price))
                {
                    var subtotal = price * newQuantity;
                    editedRow.Cells["小計"].Value = subtotal.ToString(); // 更新 '小計' 值
                }

                // 可選：如果您想更新畫面上的總計，這裡可以重新計算整個 DataGridView 的總和
                UpdateTotalAmount訂單管理();
            }
        }

        private void UpdateTotalAmount訂單管理()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in DataGridView訂單管理_訂單詳情.Rows)
            {
                // 檢查是否為新行模板，避免處理未綁定數據的新行
                if (row.IsNewRow) continue;

                // 安全地檢查 Value 是否為 null
                if (row.Cells["小計"].Value != null && decimal.TryParse(row.Cells["小計"].Value.ToString(), out decimal subtotal))
                {
                    total += subtotal;
                }
            }
            lbl訂單管理_訂單總價數值.Text = total.ToString("N0"); // 更新總計顯示
        }

        private void UpdateOrderContain(string orderContainId, string productId, int newQuantity)
        {
            string query = @"
                            UPDATE ordercontain 
                            SET ocquantity = @newQuantity
                            WHERE ocoid = @orderContainId AND ocpid = @productId";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                    cmd.Parameters.AddWithValue("@orderContainId", orderContainId);
                    cmd.Parameters.AddWithValue("@productId", productId);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string message = "訂單詳情更新成功。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("訂單詳情更新成功。");
                    }
                    else
                    {
                        string message = "訂單詳情更新失敗。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("訂單詳情更新失敗。");
                    }
                }
            }
        }

        private void DataGridView訂單管理_顯示訂單_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 取得選取的資料行
                DataGridViewRow selectedRow = DataGridView訂單管理_顯示訂單.Rows[e.RowIndex];

                // 獲取選取資料行中的訂單編號（YourOrderID）
                string orderID = selectedRow.Cells["訂單編號"].Value.ToString();

                // 在DataGridView購買清單_顯示商品中顯示該訂單的相關資訊，您可以使用FillPurchaseList或類似的方法
                FillPurchaseListManage(orderID);

                // 設定lbl購買清單_訂單編號數值的文字

                lbl訂單管理_訂單編號.Text = orderID;
            }
        }

        private void btn訂單管理_重新整理_Click(object sender, EventArgs e)
        {
            FillOrdersDataGridViewManage();

            lbl訂單管理_訂單編號.Text = "";
            lbl訂單管理_訂單總價數值.Text = "0";

            // 清空顯示商品的 DataGridView
            DataGridView訂單管理_訂單詳情.DataSource = null; // 如果您是用 DataSource 綁定的資料，這樣做將清空 DataGridView
            DataGridView訂單管理_訂單詳情.Rows.Clear(); // 直接清空所有行，適用於沒有使用 DataSource 或需要額外清除行的情況

        }

        private void btn訂單管理_搜尋_Click(object sender, EventArgs e)
        {
            var keyword = txt訂單管理_關鍵字.Text.Trim();
            SearchOrdersByEmail(keyword);
        }

        private void txt訂單管理_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn訂單管理_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void SearchOrdersByEmail(string email)
        {
            string query = @"
                            SELECT o.oid AS '訂單編號', 
                            o.ocustomerid AS '會員ID', 
                            p.姓名 AS '訂購人',  
                            o.ostatus AS '訂單狀態', 
                            o.orderpayway AS '付款方式', 
                            o.odesc AS '備註'
                            FROM orders o
                            JOIN persons p ON o.ocustomerid = p.id
                            WHERE p.email = @Email";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridView訂單管理_顯示訂單.DataSource = dataTable;

                        // 重新設置列寬和對齊方式，以保持格式一致
                        DataGridView訂單管理_顯示訂單.Columns["訂單編號"].Width = 100;
                        DataGridView訂單管理_顯示訂單.Columns["會員ID"].Width = 80;
                        DataGridView訂單管理_顯示訂單.Columns["訂購人"].Width = 140;
                        DataGridView訂單管理_顯示訂單.Columns["訂單狀態"].Width = 100;
                        DataGridView訂單管理_顯示訂單.Columns["付款方式"].Width = 100;
                        DataGridView訂單管理_顯示訂單.Columns["備註"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        // 設置文字對齊方式
                        DataGridView訂單管理_顯示訂單.Columns["訂單編號"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["會員ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["訂購人"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["訂單狀態"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridView訂單管理_顯示訂單.Columns["付款方式"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void btn訂單管理_標記為已完成_Click(object sender, EventArgs e)
        {
            if (DataGridView訂單管理_顯示訂單.SelectedRows.Count > 0)
            {
                var selectedRow = DataGridView訂單管理_顯示訂單.SelectedRows[0];
                var orderId = selectedRow.Cells["訂單編號"].Value.ToString();
                UpdateOrderStatus(orderId, "已完成");
            }
        }

        private void UpdateOrderStatus(string orderId, string status)
        {
            string query = "UPDATE orders SET ostatus = @status WHERE oid = @orderId";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }
            }

            // 可能需要重新載入或更新DataGridView以顯示最新的訂單狀態
            FillOrdersDataGridViewManage();
        }

        private void FilterOrdersByStatus(string status)
        {
            string query = $@"
                            SELECT 
                            o.oid AS '訂單編號', 
                            o.ocustomerid AS '會員ID', 
                            p.姓名 AS '訂購人', 
                            o.ostatus AS '訂單狀態', 
                            o.orderpayway AS '付款方式', 
                            o.odesc AS '備註'
                            FROM orders o
                            JOIN persons p ON o.ocustomerid = p.id
                            WHERE o.ostatus = @Status
                            ORDER BY o.odate";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", status);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridView訂單管理_顯示訂單.DataSource = dataTable;

                        // 重新設置DataGridView格式，確保顯示格式與其他顯示方式一致
                        // 此處省略列設置代碼，請參考之前的設置方法
                    }
                }
            }
        }

        private void btn訂單管理_顯示已完成_Click(object sender, EventArgs e)
        {
            FilterOrdersByStatus("已完成");
        }

        private void btn訂單管理_標記為處理中_Click(object sender, EventArgs e)
        {
            if (DataGridView訂單管理_顯示訂單.SelectedRows.Count > 0)
            {
                var selectedRow = DataGridView訂單管理_顯示訂單.SelectedRows[0];
                var orderId = selectedRow.Cells["訂單編號"].Value.ToString();
                UpdateOrderStatus(orderId, "處理中");
            }
        }

        private void btn訂單管理_顯示處理中_Click(object sender, EventArgs e)
        {
            FilterOrdersByStatus("處理中");
        }

        private void btn訂單管理_取消此筆訂單_Click(object sender, EventArgs e)
        {
            if (DataGridView訂單管理_顯示訂單.SelectedRows.Count > 0)
            {
                var selectedRow = DataGridView訂單管理_顯示訂單.SelectedRows[0];
                var orderId = selectedRow.Cells["訂單編號"].Value.ToString();
                UpdateOrderStatus(orderId, "已取消");
            }
        }

        private void btn訂單管理_顯示已取消_Click(object sender, EventArgs e)
        {
            FilterOrdersByStatus("已取消");
        }

        private void btn訂單管理_標記為未出貨_Click(object sender, EventArgs e)
        {
            if (DataGridView訂單管理_顯示訂單.SelectedRows.Count > 0)
            {
                var selectedRow = DataGridView訂單管理_顯示訂單.SelectedRows[0];
                var orderId = selectedRow.Cells["訂單編號"].Value.ToString();
                UpdateOrderStatus(orderId, "未出貨");
            }
        }

        private void btn訂單管理_顯示未出貨_Click(object sender, EventArgs e)
        {
            FilterOrdersByStatus("未出貨");
        }

        //===============   管理者系統_訂單管理  ==================================
        //
        //===============   管理者系統_會員管理  ==================================

        private void btn會員管理_搜尋_Click(object sender, EventArgs e)
        {
            string keyword = txt會員管理_關鍵字.Text.Trim();
            SearchMembersByEmail(keyword);
        }

        private void SearchMembersByEmail(string email)
        {
            string query = @"
                            SELECT 
                            id AS '會員編號', 
                            email AS '會員帳號', 
                            姓名 AS '會員姓名', 
                            密碼 AS '會員密碼', 
                            手機號碼 AS '會員電話', 
                            權限 AS '會員權限'
                            FROM persons
                            WHERE email LIKE '%' + @Email + '%'";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridView會員管理.DataSource = dataTable;

                        // 設置列的唯讀和自動填滿
                        SetupDataGridViewColumns();
                    }
                }
            }
        }

        private void SetupDataGridViewColumns()
        {
            // 將會員編號設為唯讀
            DataGridView會員管理.Columns["會員編號"].ReadOnly = true;
            DataGridView會員管理.Columns["會員權限"].ReadOnly = true;

            // 將會員帳號設為自動填滿
            DataGridView會員管理.Columns["會員帳號"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // 選擇性設置其他列的寬度或隱藏
            DataGridView會員管理.Columns["會員姓名"].Width = 150;
            DataGridView會員管理.Columns["會員密碼"].Visible = true; // 假設不顯示密碼
            DataGridView會員管理.Columns["會員密碼"].Width = 150; // 假設不顯示密碼
            DataGridView會員管理.Columns["會員電話"].Width = 190;
            DataGridView會員管理.Columns["會員權限"].Width = 100;
        }

        private void txt會員管理_關鍵字_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn會員管理_搜尋.PerformClick();
                e.SuppressKeyPress = true; // 防止發出嗶嗶聲
            }
        }

        private void btn會員管理_重新整理_Click(object sender, EventArgs e)
        {
            DataGridView會員管理.ClearAll();
            SearchMembersAll();
        }

        private void SearchMembersAll()
        {
            string query = @"
                            SELECT 
                            id AS '會員編號', 
                            email AS '會員帳號', 
                            姓名 AS '會員姓名', 
                            密碼 AS '會員密碼', 
                            手機號碼 AS '會員電話', 
                            權限 AS '會員權限'
                            FROM persons
                            ";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridView會員管理.DataSource = dataTable;

                        // 設置列的唯讀和自動填滿
                        SetupDataGridViewColumns();
                    }
                }
            }
        }

        private void btn會員管理_全部會員_Click(object sender, EventArgs e)
        {
            SearchMembersAll();
        }

        private void btn會員管理_刪除選中帳號_Click(object sender, EventArgs e)
        {
            if (DataGridView會員管理.SelectedRows.Count > 0)
            {
                string confirmMessage = "確定刪除此會員資料?";
                using (Form對話框 confirmDialog = new Form對話框(confirmMessage))
                {
                    var confirmResult = confirmDialog.ShowDialog();
                    if (confirmResult == DialogResult.OK)
                    {
                        var selectedRow = DataGridView會員管理.SelectedRows[0];
                        var memberId = Convert.ToInt32(selectedRow.Cells["會員編號"].Value);

                        DeleteMember(memberId);
                        SearchMembersAll(); // 刷新显示
                    }
                }
            }
            else
            {
                string message = "請選擇一個會員進行刪除。";
                using (Form對話框 messageDialog = new Form對話框(message))
                {
                    messageDialog.ShowDialog();
                }
            }
        }

        private void DeleteMember(int memberId)
        {
            string query = "DELETE FROM persons WHERE id = @MemberId";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MemberId", memberId);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string message = "會員資料刪除成功。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("會員資料刪除成功。");
                    }
                    else
                    {
                        string message = "刪除失敗，請重試。";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show("刪除失敗，請重試。");
                    }
                }
            }
        }

        private void DataGridView會員管理_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView)sender;
            var editedRow = dataGridView.Rows[e.RowIndex];
            var memberId = Convert.ToInt32(editedRow.Cells["會員編號"].Value);
            var columnName = dataGridView.Columns[e.ColumnIndex].Name;
            var newValue = editedRow.Cells[e.ColumnIndex].Value.ToString();

            // 將顯示的列名映射到資料庫的欄位名
            var columnMapping = new Dictionary<string, string>
          {
             {"會員帳號", "email"},
             {"會員姓名", "姓名"},
            {"會員電話", "手機號碼"}
          };

            if (columnMapping.ContainsKey(columnName))
            {
                var dbColumnName = columnMapping[columnName];
                UpdateMemberInfo(memberId, dbColumnName, newValue);
            }
            else
            {
                string message = "不支援的欄位編輯。";
                using (Form對話框 對話框 = new Form對話框(message))
                {
                    對話框.ShowDialog();
                }
                //MessageBox.Show("不支援的欄位編輯。");
            }
          }

        private void UpdateMemberInfo(int memberId, string dbColumnName, string newValue)
        {
            string query = $"UPDATE persons SET {dbColumnName} = @NewValue WHERE id = @MemberId";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NewValue", newValue);
                    cmd.Parameters.AddWithValue("@MemberId", memberId);

                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            string message = "資料更新成功。";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("資料更新成功。");
                        }
                        else
                        {
                            string message = "更新失敗，請重試。";
                            using (Form對話框 對話框 = new Form對話框(message))
                            {
                                對話框.ShowDialog();
                            }
                            //MessageBox.Show("更新失敗，請重試。");
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = $"更新出錯：{ex.Message}";
                        using (Form對話框 對話框 = new Form對話框(message))
                        {
                            對話框.ShowDialog();
                        }
                        //MessageBox.Show($"更新出錯：{ex.Message}");
                    }
                }
            }
        }

        //===============   管理者系統_會員管理  =====================================
        //
        //===============   管理者系統_系統管理_打卡  ==================================

        private void FillDataGridView打卡紀錄()
        {
            string query = @"
                     SELECT 
                     d.staffid AS '員工編號', 
                     p.姓名 AS '員工姓名', 
                     d.checkin AS '上班打卡時間', 
                     d.checkout AS '下班打卡時間'
                     FROM 
                     duty d
                     INNER JOIN 
                     persons p ON d.staffid = p.id";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridView打卡紀錄.DataSource = dataTable;
                    }
                }
            }

            // 设置列属性
            SetupDataGridViewColumns打卡();
        }

        private void SetupDataGridViewColumns打卡()
        {
            // 格式化显示时间
            DataGridView打卡紀錄.Columns["上班打卡時間"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            DataGridView打卡紀錄.Columns["下班打卡時間"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            // 设置所有列为唯獨
            foreach (DataGridViewColumn column in DataGridView打卡紀錄.Columns)
            {
                column.ReadOnly = true;
            }

            // 设置員工姓名列自动填充
            DataGridView打卡紀錄.Columns["員工姓名"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // 其他列手动调整宽度
            DataGridView打卡紀錄.Columns["員工編號"].Width = 100; // 示例宽度，可根据实际需要调整
            DataGridView打卡紀錄.Columns["員工編號"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView打卡紀錄.Columns["上班打卡時間"].Width = 300; // 示例宽度，可根据实际需要调整
            DataGridView打卡紀錄.Columns["下班打卡時間"].Width = 300; // 示例宽度，可根据实际需要调整
        }

        private void LoadEmployeeEmails()
        {
            string query = "SELECT email FROM persons WHERE 權限 < 1000";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbox打卡_你的員工帳號.Items.Add(reader["email"].ToString());
                        }
                    }
                }
            }
        }

        private void btn打卡_上班打卡_Click(object sender, EventArgs e)
        {
            string email = cbox打卡_你的員工帳號.SelectedItem.ToString();
            string passwordInput = txt打卡_密碼.Text;

            string query = "SELECT id, 密碼, 權限 FROM persons WHERE email = @Email";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string password = reader["密碼"].ToString();
                            if (password == passwordInput)
                            {
                                int staffId = Convert.ToInt32(reader["id"]);
                                int permission = Convert.ToInt32(reader["權限"]);
                                InsertCheckInRecord(staffId, permission);
                                
                            }
                            else
                            {
                                string message = "密碼不正確。";
                                using (Form對話框 dialog = new Form對話框(message))
                                {
                                    dialog.ShowDialog();
                                }
                                //MessageBox.Show("密碼不正確。");
                            }
                        }
                        else
                        {
                            string message = "未找到該員工資料。";
                            using (Form對話框 dialog = new Form對話框(message))
                            {
                                dialog.ShowDialog();
                            }
                            //MessageBox.Show("未找到該員工資料。");
                        }
                    }
                }
                FillDataGridView打卡紀錄();
            }
            
        }

        private void InsertCheckInRecord(int staffId, int permission)
        {
            string query = "INSERT INTO duty (staffid, checkin, 權限) VALUES (@StaffId, GETDATE(), @Permission)";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StaffId", staffId);
                    cmd.Parameters.AddWithValue("@Permission", permission);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string message = "上班打卡成功。";
                        using (Form對話框 dialog = new Form對話框(message))
                        {
                            dialog.ShowDialog();
                        }
                        //MessageBox.Show("上班打卡成功。");
                    }
                    else
                    {
                        string message = "打卡失敗，請重試";
                        using (Form對話框 dialog = new Form對話框(message))
                        {
                            dialog.ShowDialog();
                        }
                        //MessageBox.Show("打卡失敗，請重試");
                    }
                }
            }
        }

        private void btn打卡_下班打卡_Click(object sender, EventArgs e)
        {
            string email = cbox打卡_你的員工帳號.SelectedItem.ToString();
            string passwordInput = txt打卡_密碼.Text;

            string query = "SELECT id, 密碼 FROM persons WHERE email = @Email";
            int staffId = -1;

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && reader["密碼"].ToString() == passwordInput)
                        {
                            staffId = Convert.ToInt32(reader["id"]);
                        }
                        else
                        {
                            string message = "密碼不正確或未找到該員工資料。";
                            using (Form對話框 dialog = new Form對話框(message))
                            {
                                dialog.ShowDialog();
                            }
                            return;
                        }
                    }
                }
                
            }

            //嘗試更新今天的打卡紀錄
            string updateQuery = @"
        UPDATE duty
        SET checkout = GETDATE()
        WHERE staffid = @StaffId AND CAST(checkin AS DATE) = CAST(GETDATE() AS DATE)";

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@StaffId", staffId);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string message = "下班打卡成功。";
                        using (Form對話框 dialog = new Form對話框(message))
                        {
                            dialog.ShowDialog();
                        }
                    }
                    else
                    {
                        string message = "下班打卡失敗，請重試或聯絡管理員";
                        using (Form對話框 dialog = new Form對話框(message))
                        {
                            dialog.ShowDialog();
                        }
                    }
                }
            }
            // 确保 FillDataGridView打卡紀錄(); 调用在这里，以便无论更新操作成功与否都能刷新数据显示
            FillDataGridView打卡紀錄();
        }

        //===============   管理者系統_系統管理_打卡  ==================================
        //
        //===============   管理者系統_系統管理_權限修改  ==================================

        private void btn權限修改_確認更改權限_Click(object sender, EventArgs e)
        {
            string email1 = txt權限修改_Email1.Text;
            string password1 = txt權限修改_密碼1.Text;
            string email2 = txt權限修改_Email2.Text;
            string password2 = txt權限修改_密碼2.Text;
            string newRole = cbox權限修改_身分組1.SelectedItem.ToString();

            // 權限映射
            Dictionary<string, int> permissionMap = new Dictionary<string, int>
            {
             {"客戶", 1000},
             {"店員", 100},
             {"店長", 10},
             {"老闆", 1}
            };

            if (string.IsNullOrWhiteSpace(email1) || string.IsNullOrWhiteSpace(password1) ||
                string.IsNullOrWhiteSpace(email2) || string.IsNullOrWhiteSpace(password2) ||
                !permissionMap.ContainsKey(newRole))
            {
                ShowDialog("所有欄位都必須填寫，並選擇有效的身分組。");
                return;
            }

            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();

                // 檢查修改者資訊
                var modifierInfo = GetUserInfoByEmailAndPassword(con, email2, password2);
                if (modifierInfo == null)
                {
                    ShowDialog("修改者資訊不正確。");
                    return;
                }

                // 檢查被修改者資訊
                var toBeModifiedInfo = GetUserInfoByEmailAndPassword(con, email1, password1);
                if (toBeModifiedInfo == null)
                {
                    ShowDialog("被修改者資訊不正確。");
                    return;
                }

                // 檢查修改權限是否足夠
                if (!IsModificationAllowed(modifierInfo.Item3, permissionMap[newRole], toBeModifiedInfo.Item3))
                {
                    ShowDialog("您無法將被修改者的權限設定成此身分組。");
                    return;
                }

                // 更新被修改者權限
                if (UpdateUserPermission(con, toBeModifiedInfo.Item1, permissionMap[newRole]))
                {
                    // 使用 string.Format 或者 $"" 來格式化字符串，包含新角色的名稱
                    string successMessage = $"權限修改成功，現在權限為[{newRole}]。";
                    ShowDialog(successMessage);
                }
                else
                {
                    ShowDialog("權限修改失敗，請重試。");
                }
            }
        }

        // 根據Email和密碼獲取用戶資訊
        private Tuple<int, string, int> GetUserInfoByEmailAndPassword(SqlConnection con, string email, string password)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT id, email, 權限 FROM persons WHERE email = @Email AND 密碼 = @Password", con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string userEmail = reader.GetString(1);
                        int permission = reader.GetInt32(2);
                        return new Tuple<int, string, int>(id, userEmail, permission);
                    }
                }
            }
            return null;
        }

        // 檢查是否允許權限修改
        private bool IsModificationAllowed(int modifierPermission, int newPermission, int toBeModifiedPermission)
        {
            // 確保修改者的權限值小於被修改者的權限值，並允許修改者將被修改者的權限設定為任何他們想要的權限（只要這個新權限比被修改者原本的權限值大）
            return modifierPermission < toBeModifiedPermission && newPermission >= modifierPermission;
        }

        // 更新用戶權限
        private bool UpdateUserPermission(SqlConnection con, int userId, int newPermission)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE persons SET 權限 = @NewPermission WHERE id = @UserId", con))
            {
                cmd.Parameters.AddWithValue("@NewPermission", newPermission);
                cmd.Parameters.AddWithValue("@UserId", userId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        private void btn權限修改_清空所有欄位_Click(object sender, EventArgs e)
        {
            txt權限修改_Email1.Clear();
            txt權限修改_Email2.Clear();
            txt權限修改_密碼1.Clear();
            txt權限修改_密碼2.Clear();
            cbox權限修改_身分組1.SelectedIndex = -1;
        }

        //===============   管理者系統_系統管理_權限修改  ==================================
        //
        //===============   管理者系統_系統管理_系統維護  ==================================

        private void btn系統維護_修改路徑_Click(object sender, EventArgs e)
        {
            GlobalVar.image_dir = txt系統維護_圖檔路徑.Text.Trim();
            ShowDialog("儲存路徑成功!");
        }

        private void btn系統維護_立即啟動維護_Click(object sender, EventArgs e)
        {
            // 設置當前時間為維護開始時間，並將結束時間設置為一個預定的未來時間
            StartMaintenanceNow();
            ShowDialog("已啟動維護模式");
        }

        private void StartMaintenanceNow()
        {
            // 假設維護模式持續1小時
            var startTime = DateTime.Now.ToString("HH:mm");
            var endTime = DateTime.Now.AddHours(1).ToString("HH:mm");
            SaveMaintenanceTime(startTime, endTime);
        }

        private void btn系統維護_立即關閉維護_Click(object sender, EventArgs e)
        {
            // 結束維護模式
            EndMaintenanceNow();
            cbox系統維護_維護開始時間.SelectedIndex = -1;
            cbox系統維護_維護結束時間.SelectedIndex = -1;
        }

        private void EndMaintenanceNow()
        {
            // 將維護結束時間設置為當前時間，表示維護已經結束
            var endTime = DateTime.Now.ToString("HH:mm");
            Properties.Settings.Default.MaintenanceEndTime = endTime;
            Properties.Settings.Default.Save();
            ShowDialog("已關閉維護模式!");
        }

        private void btn系統維護_修改維護時間_Click(object sender, EventArgs e)
        {
            // 檢查是否已選擇開始和結束時間
            if (cbox系統維護_維護開始時間.SelectedItem == null || cbox系統維護_維護結束時間.SelectedItem == null)
            {
                ShowDialog("請選擇維護時間！");
                return; // 終止方法執行
            }

            string startTime = cbox系統維護_維護開始時間.SelectedItem.ToString();
            string endTime = cbox系統維護_維護結束時間.SelectedItem.ToString();

            // 儲存這些時間到設定或檔案中
            SaveMaintenanceTime(startTime, endTime);
            ShowDialog("已儲存維護時間！");
        }

        private void SaveMaintenanceTime(string startTime, string endTime)
        {
            Properties.Settings.Default.MaintenanceStartTime = startTime;
            Properties.Settings.Default.MaintenanceEndTime = endTime;
            Properties.Settings.Default.Save();
        }

        private bool IsNowInMaintenance()
        {
            var now = DateTime.Now.TimeOfDay;

            TimeSpan startTime = TimeSpan.Zero;
            TimeSpan endTime = TimeSpan.Zero;

            // 定義時間格式
            string[] timeFormats = new[] { "HH:mm", "h:mm tt" }; // 包括24小時和12小時制的格式

            // 解析開始時間
            if (!TimeSpan.TryParse(Properties.Settings.Default.MaintenanceStartTime, out startTime))
            {
                if (DateTime.TryParseExact(Properties.Settings.Default.MaintenanceStartTime, timeFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTimeStart))
                {
                    startTime = dateTimeStart.TimeOfDay;
                }
                else
                {
                    // 如果解析失敗，可以在這裡記錄錯誤或採取其他措施
                    Console.WriteLine("無法解析維護開始時間");
                    return false; // 或者根據您的業務邏輯選擇其他處理方式
                }
            }

            // 解析結束時間
            if (!TimeSpan.TryParse(Properties.Settings.Default.MaintenanceEndTime, out endTime))
            {
                if (DateTime.TryParseExact(Properties.Settings.Default.MaintenanceEndTime, timeFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTimeEnd))
                {
                    endTime = dateTimeEnd.TimeOfDay;
                }
                else
                {
                    // 如果解析失敗，可以在這裡記錄錯誤或採取其他措施
                    Console.WriteLine("無法解析維護結束時間");
                    return false; // 或者根據您的業務邏輯選擇其他處理方式
                }
            }

            // 判斷是否在維護時間內
            if (endTime < startTime)
            {
                // 跨夜維護
                return now <= endTime || now >= startTime;
            }
            else
            {
                // 同一天內維護
                return now >= startTime && now <= endTime;
            }
        }

        //===============   管理者系統_系統管理_系統維護  ==================================

    }
}
