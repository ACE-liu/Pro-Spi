using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SPI.Global.Configuration;
using SPI.SPICheckWin;
using System.Drawing;

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
            _check = false;
        }
        bool _check;
        public bool Check
        {
            get { return _check; }
            set
            {
                if (follows != null)
                {
                    follows.Visible = value;
                }
                _check = value;
            }
        }
        public override Control CreateUI()
        {
            cb = new CheckBox();
            cb.SuspendLayout();
            cb.Font = CurFocusPanel.Font;
            cb.Left = ControlsGap;
            cb.Text = text;
            cb.AutoSize = true;
            cb.Checked = _check;
            cb.CheckedChanged += Cb_CheckedChanged;
            cb.ResumeLayout();
            return cb;
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            cb.ForeColor = cb.Checked ? Color.Black : Color.Gray;
            Check = cb.Checked;
            if (follows!=null&&HideFollows)
            {
                //if (cb.Checked)
                //{
                //    //holder.RearrangeAllControl(follows, level + 1);
                //}
                //else
                //{
                //    follows.SuspendLayout();
                //    Relayout();
                //    //follows.ResumeLayout();
                //}
                holder.RearrangeAllControl(follows, level + 1);
            }
        }
        public override Panel AddFollows()
        {
            follows = new Panel();
            //follows.AutoSize = true;
            follows.BorderStyle = BorderStyle.FixedSingle;
            follows.Left = cb.Left;
            follows.Top = cb.Bottom + ControlsGap;
            follows.Visible = _check;
            follows.VisibleChanged += Follows_VisibleChanged;
            return follows;
        }
        public override void LoadFrom(MyReader mr)
        {
            Check = mr.LoadBool();
        }
        public override void Save(MyWriter mw)
        {
            mw.Save(_check);
        }
        public override void Add(Control ctr)
        {
            if (follows == null)
            {
                AddFollows().Controls.Add(ctr);
            }
            else
                follows.Controls.Add(ctr);
            ResizeFollowSize(follows);
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
