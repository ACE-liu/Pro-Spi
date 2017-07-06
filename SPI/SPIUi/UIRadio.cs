using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPI.SPICheckWin;
using System.Drawing;

using static SPI.Global.Configuration;


namespace SPI.SPIUi
{
    class UIRadio:UIBase
    {
        ///是否显示该控件
        public bool isVisible = true;
        private string text  = "Button";
        private RadioButton rb;

        Panel follows = null;
        public UIRadio(CheckWinBase win,int lvl ,bool isVisible , string text):base(win,lvl)
        {
            this.isVisible = isVisible;
            this.text = text;
        }
        /// <summary>
        /// 创建UI前先释放之前的资源，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140625</date>
        public override void DisposeUIManual()
        {
            if (null != this.follows)
            {
                this.follows.Dispose();
                this.follows = null;
            }
            if (null != this.rb)
            {
                this.rb.CheckedChanged -= new EventHandler(rb_CheckedChanged);
                this.rb.Dispose();
                this.rb = null;
            }
        }
        public override Control CreateUI()
        {
            rb = new RadioButton(); rb.SuspendLayout();
            rb.Font = CurFocusPanel.Font;

            rb.Left = ControlsGap;
            rb.Text = text;
            rb.AutoSize = true;
            //rb.Checked = Check;
            rb.ForeColor = rb.Checked ? Color.Black : Color.Gray;
            rb.CheckedChanged += new EventHandler(rb_CheckedChanged);
            rb.ResumeLayout();
            return rb;
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (follows!=null&&!HideFollows)
            {
                if (rb.Checked)
                {
                    holder.RearrangeAllControl(follows, level + 1);
                }
                else
                {
                    follows.SuspendLayout();
                    Relayout();
                    //follows.ResumeLayout();
                }
            }
            holder?.OnDataChange(sender, e);
        }
        public override Panel AddFollows()
        {
            follows = new Panel();
            follows.BorderStyle = BorderStyle.Fixed3D;
            follows.Left = rb.Left;
            follows.Top = rb.Bottom + ControlsGap;
            follows.VisibleChanged += new EventHandler(follows_VisibleChanged);
            return follows;
        }
        public override void Add(Control ctr)
        {
            if (follows == null)
            {
                AddFollows().Controls.Add(ctr);
            }
            else
                follows.Controls.Add(ctr);
        }
        private void follows_VisibleChanged(object sender, EventArgs e)
        {
            Relayout();
        }
        public override void Relayout()
        {
            if (follows!=null&&follows.Visible)
            {
                int y = 0;
                foreach (Control control in follows.Controls)
                {
                    control.Top = y ;
                    if (control.Visible)
                    {
                        y = control.Bottom + ControlsGap;
                    }
                    follows.Height = y;
                }
            }
        }

    }
}
