using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SPI
{
    /// <summary>
    /// 带数值和图像显示的上下限范围设置控件.
    /// </summary>
    public partial class RangeEditor : UserControl
    {
        #region Property Part
        int maximum = 255;
        int minimum = 0;
        int up = 255;
        int down = 0;
        Color bkcolor = Color.DarkRed;
        Color dwcolor = Color.Red;
        /// <summary>
        /// 通知事件的函数格式定义.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void NotifyEventHandler(object sender, EventArgs e);
        /// <summary>
        /// 属性值变化时的处理函数
        /// </summary>
        public event NotifyEventHandler Notify;
        /// <summary>
        /// 获得焦点时的处理函数
        /// </summary>
        public event NotifyEventHandler GetFocus;
        /// <summary>
        /// 设置或读取最大值
        /// </summary>
        public int Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value; 
                if (maximum < minimum) maximum = minimum; 
                if (up > maximum) Up = maximum; 
                if (down > maximum) Down = maximum; 
            }
        }
        /// <summary>
        /// 设置或读取最小值
        /// </summary>
        public int Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                if (maximum < minimum) minimum = maximum;
                if (up < minimum) Up = minimum;
                if (down < minimum) Down = minimum;
            }
        }
        /// <summary>
        /// 设置或获取上限值
        /// </summary>
        public int Up
        {
            get { return up; }
            set
            {
                if (Enabled)
                    textBox1.Text = value.ToString();
                else
                    up = value;
            }
        }
        /// <summary>
        /// 设置或获取下限值
        /// </summary>
        public int Down
        {
            get { return down; }
            set
            {
                if (Enabled)
                    textBox2.Text = value.ToString();
                else
                    down = value;
            }
        }
        /// <summary>
        /// 设置或获取背景颜色
        /// </summary>
        public Color BackgroundColor
        {
            get { return bkcolor; }
            set
            {
                bkcolor = value;
                pictureBox1.BackColor = bkcolor;
                pictureBox1.Invalidate();
            }
        }
        /// <summary>
        /// 设置或获取选中范围的显示色.
        /// </summary>
        public Color SelectedColor
        {
            get { return dwcolor; }
            set
            {
                dwcolor = value;
                pictureBox1.Invalidate();
            }
        }
        #endregion
        Rectangle rect;

        public RangeEditor()
        {
            InitializeComponent();
            rect = new Rectangle();
            textBox1.Text = up.ToString();
            textBox2.Text = down.ToString();
            dataChanged();
        }

        private void RangeEditor_Resize(object sender, EventArgs e)
        {
            textBox1.Left = 0;
            textBox2.Left = 0;
            pictureBox1.Left = 0;
            textBox1.Top = 0;
            textBox2.Width = textBox1.Width;
            this.Width = this.textBox1.Width;
            if (Height < textBox2.Height * 4)
                Height = textBox2.Height * 4;
            textBox2.Top = this.Height - textBox2.Height;
            pictureBox1.Height = this.Height - textBox2.Height - textBox1.Height-2;
            pictureBox1.Top = textBox1.Bottom+1;
            pictureBox1.Width = textBox1.Width;
            getRect();
        }

        private Rectangle getRect()
        {
            rect.X = 1;
            rect.Y = pictureBox1.Height * (maximum - up) / maximum;
            rect.Width = pictureBox1.Width-2;
            rect.Height = pictureBox1.Height * ((up - down)) / maximum;
            return rect;
        }
        /// <summary>
        /// 是否正在使用鼠标修改值状态.
        /// </summary>
        bool changeing = false;
        /// <summary>
        /// 允许的鼠标按下位置与精确位置的偏差
        /// </summary>
        int delt = 3;
        /// <summary>
        /// 鼠标调整时的修改类型
        /// </summary>
        enum changeType
        {
            /// <summary>
            /// 修改上限
            /// </summary>
            up,
            /// <summary>
            /// 修改上下限
            /// </summary>
            all,
            /// <summary>
            /// 修改下限
            /// </summary>
            down,
            /// <summary>
            /// 未修改
            /// </summary>
            none,
            /// <summary>
            /// 上限上移
            /// </summary>
            uUp,
            /// <summary>
            /// 上限下移
            /// </summary>
            dUp,
            /// <summary>
            /// 下限上移
            /// </summary>
            uDown,
            /// <summary>
            /// 下限上移
            /// </summary>
            dDown
        };
        /// <summary>
        /// 当前修改类型.
        /// </summary>
        changeType where;
        /// <summary>
        /// 鼠标按下时的位置与up的偏差信息,用于根据鼠标移动位置计算up值.
        /// </summary>
        int mouseDownDelt;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (GetFocus != null)
                GetFocus(this, new EventArgs());

            switch (GetChangeType(e.Location))
            {
                case changeType.none:
                    changeing = false;
                    where = changeType.none;
                    break;
                case changeType.uDown:
                    textBox2.Text = (down + 1).ToString();
                    break;
                case changeType.uUp:
                    textBox1.Text = (up + 1).ToString();
                    break;
                case changeType.dDown:
                    textBox2.Text = (down - 1).ToString();
                    break;
                case changeType.dUp:
                    textBox1.Text = (up - 1).ToString();
                    break;
                default:
                    changeing = true;
                    mouseDownDelt = maximum- maximum * e.Y / pictureBox1.Height - up;
                    pictureBox1_MouseMove(sender, e);
                    break;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            changeing = false;
            where = changeType.none;
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (changeing)
            {
                switch (where)
                {
                    case changeType.up:
                        up = maximum - maximum * (e.Y) / pictureBox1.Height;
                        if (up < down)
                            up = down;
                        if (up > maximum)
                            up = maximum;
                        textBox1.Text = up.ToString();
                        break;
                    case changeType.down:
                        down = maximum - maximum * (e.Y) / pictureBox1.Height;
                        if (down > up)
                            down = up;
                        if (down < 0)
                            down = 0;
                        textBox2.Text = down.ToString();
                        break;
                    case changeType.all:
                        int spane = up-down;
                        up = maximum - maximum * (e.Y) / pictureBox1.Height - mouseDownDelt;
                        down = up - spane;
                        if (up > maximum)
                        {
                            up = maximum;
                            down = up - spane;
                        }
                        else if( down < minimum)
                        {
                            down = minimum;
                            up = down + spane;
                        }
                        textBox1.Text = up.ToString();
                        textBox2.Text = down.ToString();
                        break;
                }
            }
            else
            {
                GetChangeType(e.Location);
            }
        }
        /// <summary>
        /// 数据修改后的相关处理
        /// </summary>
        private void dataChanged()
        {
            getRect();
            pictureBox1.Invalidate();
            if (Notify != null)
            {
                EventArgs e = new EventArgs();
                Notify(this, e);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (this.Enabled)
            {
                System.Drawing.Drawing2D.HatchBrush hb = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal, bkcolor, dwcolor);
                Pen p = new Pen(dwcolor);

                if (rect.Height < 2)
                    e.Graphics.DrawLine(p, rect.X, rect.Y, rect.X + Width, rect.Y);
                else
                    e.Graphics.FillRectangle(hb, rect);

                hb.Dispose();
                p.Dispose();
            }
        }
        #region Helper functions
        void GetUpValue()
        {
            try
            {
                up = int.Parse(textBox1.Text);
                if (up > maximum)
                {
                    up = maximum;
                    textBox1.Text = up.ToString();
                }
                else if (up < minimum)
                {
                    up = down;
                    textBox1.Text = up.ToString();
                }
            }
            catch (Exception ex)
            {
                //RmlDebug.Wrong(ex.Message);
                textBox1.Text = up.ToString();
            }
        }
        void GetDownValue()
        {
            try
            {
                down = int.Parse(textBox2.Text);
                if (down > maximum)
                {
                    down = up;
                    textBox2.Text = down.ToString();
                }
                else if (down < minimum)
                {
                    down = minimum;
                    textBox2.Text = down.ToString();
                }
            }
            catch (Exception ex)
            {
                //RmlDebug.Wrong(ex.Message);
                textBox2.Text = down.ToString();
            }
        }
        private changeType GetChangeType(Point e)
        {
            if ((e.Y < rect.Y - delt))
            {
                where = changeType.uUp;
                pictureBox1.Cursor = Cursors.PanNorth;
            }
            else if ((e.Y > rect.Bottom + delt))
            {
                where = changeType.dDown;
                pictureBox1.Cursor = Cursors.PanSouth;
            }
            else
            {
                if (e.Y < rect.Top + delt)
                {
                    if (e.Y > rect.Bottom)
                        where = changeType.down;
                    else
                        where = changeType.up;
                    pictureBox1.Cursor = Cursors.SizeNS;
                }
                else if (e.Y > rect.Bottom - delt)
                {
                    if (e.Y < rect.Top)
                        where = changeType.up;
                    else
                        where = changeType.down;
                    pictureBox1.Cursor = Cursors.SizeNS;
                }
                else if (rect.Height * (up - down) / maximum > 10 * delt)
                {
                    if (e.Y < rect.Top + 4 * delt)
                    {
                        where = changeType.dUp;
                        pictureBox1.Cursor = Cursors.PanSouth;
                    }
                    else if (e.Y > rect.Bottom - 4 * delt)
                    {
                        where = changeType.uDown;
                        pictureBox1.Cursor = Cursors.PanNorth;
                    }
                    else
                    {
                        where = changeType.all;
                        pictureBox1.Cursor = Cursors.SizeAll;
                    }
                }
                else
                {
                        where = changeType.all;
                        pictureBox1.Cursor = Cursors.SizeAll;
                }
            }
            return where;
        }
        #endregion

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (GetFocus != null)
                GetFocus(this, new EventArgs());
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (GetFocus != null)
                GetFocus(this, new EventArgs());
        }

        private void RangeEditor_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                textBox1.Text = up.ToString();
                textBox2.Text = down.ToString();
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
            pictureBox1.Invalidate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                GetDownValue();
                dataChanged();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                GetUpValue();
                dataChanged();
            }
        }
    }
}

