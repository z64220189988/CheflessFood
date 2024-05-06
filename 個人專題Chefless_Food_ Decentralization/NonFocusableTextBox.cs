using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 個人專題Chefless_Food__Decentralization
{
    public class NonFocusableTextBox : TextBox
    {
        protected override void OnEnter(EventArgs e)
        {
            // 調用基類的 OnEnter 方法是可選的，根據你的需求決定
            // base.OnEnter(e);

            // 立即將焦點轉移到父控件，防止 TextBox 獲得焦點
            this.Parent?.Focus();
        }

        public NonFocusableTextBox()
        {
            this.SetStyle(ControlStyles.Selectable, false); // 設置控件不可選
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // 通過重寫 OnMouseDown 方法，可以進一步防止通過鼠標點擊獲得焦點
            this.Parent?.Focus();
        }
    }
}
