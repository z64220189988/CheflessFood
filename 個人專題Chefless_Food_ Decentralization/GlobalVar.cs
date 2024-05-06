using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 個人專題Chefless_Food__Decentralization
{
    internal class GlobalVar
    {
        public static string image_dir = @"Z:\上課\C#\作業\個人專題Chefless_Food_ Decentralization\商品圖檔";
        public static string strDBConnectionString = "";
        public static string 使用者名稱 = "";
        public static string 使用者帳號 = "";
        public static string 使用者密碼 = "";
        public static int 使用者ID = 0;
        public static int 使用者權限 = 0;
        public static bool is登入成功 = false;
        public static string 身分組 = string.Empty;
        public static int 結帳總金額 = 0;
        public static string 付款方式 =string.Empty;
        public static bool 是否維護中 = false;
       
    }

    

    public static class UserSession
    {
        public static void 登出()
        {
            // 清除全局使用者資訊
            GlobalVar.使用者名稱 = string.Empty;
            GlobalVar.使用者帳號 = string.Empty;
            GlobalVar.使用者密碼 = string.Empty;
            GlobalVar.使用者權限 = 0;
            GlobalVar.is登入成功 = false;
            GlobalVar.身分組 = string.Empty;
            GlobalVar.結帳總金額 = 0;
            GlobalVar.付款方式 = string.Empty;

            // 清除記住的用戶名和密碼
            Properties.Settings.Default.Username = string.Empty;
            Properties.Settings.Default.Password = string.Empty;
            Properties.Settings.Default.Save();
        }
    }


}
