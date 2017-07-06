using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SPI.Global.Configuration;
using SPI.SPICheckWin;

namespace SPI.SPIUi
{
    class UICheckBox : UIBase
    {
        private string text;
        private Panel follows;
        public bool Visible;

        private CheckBox cb = null;
        public UICheckBox(CheckWinBase win, int lvl, string text, bool isVisible) : base(win, lvl)
        {
            this.text = text;
            Visible = isVisible;
        }

        public override Control CreateUI()
        {
            cb = new CheckBox();
            cb.SuspendLayout();
            cb.Font = CurFocusPanel.Font;
            cb.Left = ControlsGap;
            cb.Text = text;
            cb.AutoSize = true;
            cb.CheckedChanged += Cb_CheckedChanged;
            cb.ResumeLayout();
            return cb;
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            if (follows!=null&&!HideFollows)
            {
                if (cb.Checked)
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
        }
        public override Panel AddFollows()
        {
            follows = new Panel();
            follows.BorderStyle = BorderStyle.Fixed3D;
            follows.Left = cb.Left;
            follows.Top = cb.Bottom + ControlsGap;
            follows.VisibleChanged += Follows_VisibleChanged;
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
        private void Follows_VisibleChanged(object sender, EventArgs e)
        {
            Relayout();
        }
        public override void Relayout()
        {
            if (follows != null && follows.Visible)
            {
                int y = 0;
                foreach (Control control in follows.Controls)
                {
                    control.Top = y;
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
