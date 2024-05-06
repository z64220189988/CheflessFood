using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace 個人專題Chefless_Food__Decentralization
{
    public partial class Form對話框 : UIForm
    {
        public Form對話框(string message)
        {
            InitializeComponent();
            txt訊息提示.Text = message;
        }

        private void btn訊息提示_確認_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // 設置對話框的返回值為 OK
            this.Close(); // 關閉對話框
        }

        private void Form對話框_Load(object sender, EventArgs e)
        {
            // 將焦點設置到除了 txt訊息提示 之外的另一個控件
            // 假設你有一個按鈕 btn訊息提示_確認，可以在表單加載時將焦點設置給它
            btn訊息提示_確認.Focus();
        }
    }
}
