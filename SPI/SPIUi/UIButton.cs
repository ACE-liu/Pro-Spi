using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPICheckWin;
using System.Windows.Forms;

using static SPI.Global.Configuration;

namespace SPI.SPIUi
{
    class UIButton:UIBase
    {
        ///是否显示该控件
        public bool isVisible = true;
        private string strButtonName = "Button";
        private CheckBox ui;
        /// <summary>
        /// 下层UI容器
        /// </summary>
        Panel follows;

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

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="win">包含此UI的检查框</param>
        /// <param name="lvl">显示层级。0为最靠左边，其他层次以等量缩进右缩</param>
        public UIButton(CheckWinBase win, int lvl, bool isVisible, string strButtonName)
            : base(win, lvl)
        {
            ui = null;
            this.isVisible = isVisible;
            this.strButtonName = strButtonName;
            Check = false;

        }
        /// <summary>
        /// 释放图片资源
        /// </summary>
        public override void DisposeBmp()
        {
        }

        /// <summary>
        /// 创建UI前先释放之前的资源，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140625</date>
        public override void DisposeUIManual()
        {
            if (null != this.ui)
            {
                //this.ui.DisposeMultiColorManual();
                //this.ui.SizeChanged -= new EventHandler(ui_SizeChanged);
                this.ui.Click -= new EventHandler(Ui_CheckedChanged);
                this.ui.Dispose();
                this.ui = null;
            }
        }

        /// <summary>
        /// 创建显示控件。由于大部分UI设置的参数不需要显示，设计为只为需要显示的UI类对象创建界面控件。
        /// </summary>
        /// <returns></returns>
        public override Control CreateUI()
        {
            ui = new CheckBox();
            ui.SuspendLayout();
            ui.Checked = _check;
            //ui.SizeChanged += new EventHandler(ui_SizeChanged);
            ui.CheckedChanged += Ui_CheckedChanged;
            ui.Appearance = Appearance.Button;
            ui.Text = strButtonName;
            //ui.Width = 90;
            //ui.Height = 31;
            ui.AutoSize = true;
            ui.Font = CurFocusPanel.Font;
            ui.Left = ControlsGap;
            ui.UseVisualStyleBackColor = true;
            ui.ResumeLayout();
            return ui;
        }
        public override Panel AddFollows()
        {
            follows = new Panel();
            follows.BorderStyle = BorderStyle.FixedSingle;
            follows.Left = ui.Left;
            follows.Top = ui.Bottom + ControlsGap;
            follows.VisibleChanged += new EventHandler(follows_VisibleChanged);
            follows.Visible = Check;
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
            ResizeFollowSize(follows);
        }
        private void follows_VisibleChanged(object sender, EventArgs e)
        {
            Relayout();
        }

        private void Ui_CheckedChanged(object sender, EventArgs e)
        {
            Check = ui.Checked;
            if (follows != null && HideFollows)
            {
                ResizeFollowSize(follows);
                holder.RearrangeAllControl(follows, level + 1);
                //if (ui.Checked)
                //{
                //    holder.RearrangeAllControl(follows, level + 1);
                //}
                //else
                //{
                //    follows.SuspendLayout();
                //    Relayout();
                //}
            }
            ui_DataChanged(sender, e);
        }
        /// <summary>
        /// 重排显示布局
        /// </summary>
        public override void Relayout()//Relayout the follows if exists.
        {
            if (follows != null)
            {
                if (follows.Visible)
                {
                    int y = 0;// Globles.controlsGap;
                    foreach (Control ctr in follows.Controls)
                    {
                        ctr.Top = y;
                        if (ctr.Visible)
                        {
                            y = ctr.Bottom + ControlsGap;
                        }
                        follows.Height = y;
                    }
                }
            }
        }
        /// <summary>
        /// 处理数据改变事件的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ui_DataChanged(object sender, EventArgs e)
        {
            DataChanged = true;//标记数据已经改动可能需要保存
            holder.OnDataChange(sender, e);
        }
        /// <summary>
        /// 保存数据到文件
        /// </summary>
        /// <param name="sw">指示要写入的文件的自定义流类对象</param>
        public override void SaveTo(MyWriter sw)
        {
           // sw.Save(strButtonName);
        }
        /// <summary>
        /// 从文件读入数据
        /// </summary>
        /// <param name="sr">指示要从中读入数据的自定义流类对象</param>
        public override void LoadFrom(MyReader sr)
        {
           // sr.Load(ref strButtonName);//= int.Parse(sr.ReadLine());

        }
        /// <summary>
        /// 从其他模板拷贝数据
        /// </summary>
        /// <param name="ub">要从中拷贝数据的模板</param>
        public override void CopyFrom(UIBase ub)
        {

        }
    }
}
