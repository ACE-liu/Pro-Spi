using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SPI.Global;

using static SPI.Global.Configuration;

namespace SPI
{
    /// <summary>
    /// 用于编辑一组颜色范围组合的用户控件
    /// ???修改可能较少,以后加注释.
    /// </summary>
    public partial class MultiColors : UserControl
    {
        bool isClear = false;
        public MultiColors(List<string> colorTips = null)
        {
            
            InitializeComponent();
            this.Height = StateButton.Bottom + 2;
            txDensity.Text = "2";
			backupColorList = new List<ColorRange>();
            if (colorTips != null && colorTips.Count > 0)
            {
                Label label = new Label();
                label.Top = StateButton.Top;
                label.Font = new Font(StateButton.Font.FontFamily, 16);
                label.Text = string.Join(",", colorTips);
                label.Left = StateButton.Right + 10;
                label.ForeColor = Color.Orange;
                label.Height = StateButton.Height;
                this.Controls.Add(label);
            }
            pictureBox1.Location = new Point(pictureBox1.Location.X + 70, pictureBox1.Location.Y + 30);
            cbUseColorPen.Location = new Point(cbUseColorPen.Location.X + 70, cbUseColorPen.Location.Y + 30);
            btClear.Location = new Point(btClear.Location.X + 0, btClear.Location.Y - 30);
            btRollback.Location = new Point(btRollback.Location.X + 0, btRollback.Location.Y - 30);
        }

        [CategoryAttribute("用户信息"), DescriptionAttribute("属性值变化")]
        public new event EventHandler SizeChanged;
        public event EventHandler DataChanged;
        public ColorRange.OperationType Operation
        {
            get { return curColor.operation; }
            set
            {
                curColor.operation = value;
                operationList.SelectedIndex = (int)curColor.operation;// = (ColorRange.OperationType)
            }
        }
        public List<ColorRange> colors;
        public ColorRange curColor;

        public void SetSource(List<ColorRange> cs)
        {

            settingData = true;
            colors = cs;
            curColor = colors[0];
            GetDataFrom(curColor);
            ColorsList.Items.Clear();
            for (int i = 0; i < colors.Count; i++)
            {
                ColorsList.Items.Add("颜色范围" + i.ToString());
            }
            ColorsList.Items.Add("新建颜色项");
            //ColorsList.SelectedIndex = 0;
            settingData = false;
        }

        bool settingData = false;
        public void GetDataFrom(ColorRange cr)
        {
            this.Operation = cr.operation;
            this.checkBoxRed.Checked = cr.redUsed;
            this.RedEditor.Up = cr.redUp;
            this.RedEditor.Down = cr.redDown;
            this.checkBoxGreen.Checked = cr.greenUsed;
            this.GreenEditor.Up = cr.greenUp;
            this.GreenEditor.Down = cr.greenDown;
            this.checkBoxBlue.Checked = cr.blueUsed;
            this.BlueEditor.Up = cr.blueUp;
            this.BlueEditor.Down = cr.blueDown;
            this.checkBoxGray.Checked = cr.grayUsed;
            this.GrayEditor.Up = cr.grayUp;
            this.GrayEditor.Down = cr.grayDown;

            if (RedEditor.Enabled)
                curImage = ActImage.red;
            else if (GreenEditor.Enabled)
                curImage = ActImage.green;
            else if (BlueEditor.Enabled)
                curImage = ActImage.blue;
            else
                curImage = ActImage.red;
            changeImage();

            ColorPic.Invalidate();
            GrayPic.Invalidate();
        }
        public void SetDataTo(ColorRange cr)
        {
            if (!settingData)
            {
                cr.operation = this.Operation;
                cr.redUsed = this.checkBoxRed.Checked;
                cr.redUp = (byte)this.RedEditor.Up;
                cr.redDown = (byte)this.RedEditor.Down;
                cr.greenUsed = this.checkBoxGreen.Checked;
                cr.greenUp = (byte)this.GreenEditor.Up;
                cr.greenDown = (byte)this.GreenEditor.Down;
                cr.blueUsed = this.checkBoxBlue.Checked;
                cr.blueUp = (byte)this.BlueEditor.Up;
                cr.blueDown = (byte)this.BlueEditor.Down;
                cr.grayUsed = this.checkBoxGray.Checked;
                cr.grayUp = (byte)this.GrayEditor.Up;
                cr.grayDown = (byte)this.GrayEditor.Down;
            }
        }
        public bool ColorAcceptable(Color c)
        {
            if (curColor == null)
                return false;

            int total = c.R + c.G + c.B;
            if (total == 0) total = 1;
            int red = (int)c.R * 100 / total;
            int green = (int)c.G * 100 / total;
            int blue = (int)c.B * 100 / total;
            int gray = total / 3;
            bool rtn = true;
            if (this.curColor.redUsed)
            {
                if (red > curColor.redUp) rtn = false;
                if (red < curColor.redDown) rtn = false;
            }
            if (this.curColor.greenUsed)
            {
                if (green > curColor.greenUp) rtn = false;
                if (green < curColor.greenDown) rtn = false;
            }
            if (this.curColor.blueUsed)
            {
                if (blue > curColor.blueUp) rtn = false;
                if (blue < curColor.blueDown) rtn = false;
            }
            if (this.curColor.grayUsed)
            {
                if (gray > curColor.grayUp) rtn = false;
                if (gray < curColor.grayDown) rtn = false;
            }
            return rtn;
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!settingData)
            {
                changeSize();
                if (SizeChanged != null)
                    SizeChanged(this, new EventArgs());
            }
        }
        void changeData()
        {
            if (!settingData)
            {
                SetDataTo(curColor);
                if (DataChanged != null && !isClear)
                    DataChanged(this, new EventArgs());
            }
        }
        public bool GetExpand()
        {
            return StateButton.Checked;
        }
        public void SetExpand(bool check)
        {
            settingData = true;
            StateButton.Checked = check;
            changeSize();
            settingData = false;
        }
        void changeSize()
        {
			CloseColorPen();
            if (StateButton.Checked)
            {
                StateButton.Text = "结束编辑";
                this.Height = GrayPic.Bottom + 5;
                this.BorderStyle = BorderStyle.FixedSingle;
                EnterHandle();
                loadColorPenText();                                

            }
            else
            {
                StateButton.Text = "编辑颜色";
                this.Height = StateButton.Bottom + 2;
                this.BorderStyle = BorderStyle.None;
                LeaveHandle();
            }
        }

        private void rangeEditor1_GetFocus(object sender, EventArgs e)
        {
            curImage = ActImage.red;
            changeImage();
            EnterHandle();
        }

        private void rangeEditor2_GetFocus(object sender, EventArgs e)
        {
            curImage = ActImage.green;
            changeImage();
            EnterHandle();
        }

        private void rangeEditor3_GetFocus(object sender, EventArgs e)
        {
            curImage = ActImage.blue;
            changeImage();
            EnterHandle();
        }
        private void GrayEditor_GetFocus(object sender, EventArgs e)
        {
            EnterHandle();
        }

        enum ActImage { red, green, blue };
        ActImage curImage = ActImage.red;
        private void rangeEditor1_Notify(object sender, EventArgs e)
        {
            if (!UsingColPenNow)
                changeData();
            ColorPic.Invalidate();
        }

        private void rangeEditor2_Notify(object sender, EventArgs e)
        {
            if (!UsingColPenNow)
                changeData();
            ColorPic.Invalidate();
        }

        private void rangeEditor3_Notify(object sender, EventArgs e)
        {
            if (!UsingColPenNow)
                changeData();
            ColorPic.Invalidate();
        }

        private void checkBoxRed_CheckedChanged(object sender, EventArgs e)
        {
            
            if (!UsingColPenNow)
                changeData();

            RedEditor.Enabled = checkBoxRed.Checked;
            if (RedEditor.Enabled)
            {
                curImage = ActImage.red;
                changeImage();
            }
            ColorPic.Invalidate();
        }


        private void checkBoxGreen_CheckedChanged(object sender, EventArgs e)
        {
           
            if (!UsingColPenNow)
                changeData();

            GreenEditor.Enabled = checkBoxGreen.Checked;
            if (GreenEditor.Enabled)
            {
                curImage = ActImage.green;
                changeImage();
            }
            ColorPic.Invalidate();
        }


        private void checkBoxBlue_CheckedChanged(object sender, EventArgs e)
        {
            
            if (!UsingColPenNow)
                changeData();

            BlueEditor.Enabled = checkBoxBlue.Checked;
            if (BlueEditor.Enabled)
            {
                curImage = ActImage.blue;
                changeImage();
            }
            ColorPic.Invalidate();
        }

        void changeImage()
        {
            switch (curImage)
            {
                case ActImage.red:
                    this.ColorPic.BackgroundImage = RedColorBackground;
                    break;
                case ActImage.green:
                    this.ColorPic.BackgroundImage = GreenColorBackground;
                    break;
                case ActImage.blue:
                    this.ColorPic.BackgroundImage = BlueColorBackground;
                    break;
            }
        }
        private void checkBoxGray_CheckedChanged(object sender, EventArgs e)
        {
            
            if (!UsingColPenNow)
                changeData();

            GrayEditor.Enabled = checkBoxGray.Checked;
            if (!checkBoxGray.Checked)
                GrayPic.Cursor = Cursors.Default;
            GrayPic.Invalidate();
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            RangeEditor up, left, right;
            switch (curImage)
            {
                case ActImage.red:
                    up = RedEditor;
                    left = GreenEditor;
                    right = BlueEditor;
                    break;
                case ActImage.green:
                    up = GreenEditor;
                    left = BlueEditor;
                    right = RedEditor;
                    break;
                case ActImage.blue:
                    up = BlueEditor;
                    left = RedEditor;
                    right = GreenEditor;
                    break;
                default:
                    up = RedEditor;
                    left = GreenEditor;
                    right = BlueEditor;
                    break;
            }

            int h = ColorPic.ClientSize.Height;
            int w = ColorPic.ClientSize.Width;

            //System.Drawing.Drawing2D.HatchBrush hb = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Percent20, Color.Black,Color.Red);
            Brush hb = Brushes.Black;
            double x111 = 0, x112 = 0, x121 = 0, x122 = 0, y111 = 0, y112 = 0, y121 = 0, y122 = 0;
            if (up.Enabled)
            {
                y111 = (double)h - h * up.Up / 100;
                y112 = y111;
                x111 = (double)w / 2 - (y111 / 1.732f);
                x112 = (double)w / 2 + (y111 / 1.732f);
                y121 = (double)h - h * up.Down / 100;
                y122 = y121;
                x121 = (double)w / 2 - (y121 / 1.732f);
                x122 = (double)w / 2 + (y121 / 1.732f);
                e.Graphics.FillPolygon(hb, new Point[] { new Point(w / 2, 0), new Point((int)x111, (int)y111), new Point((int)x112, (int)y112) });
                e.Graphics.FillPolygon(hb, new Point[] { new Point(0, h), new Point(w, h), new Point((int)x122, (int)y122), new Point((int)x121, (int)y121) });

            }

            double x211 = 0, x212 = 0, x221 = 0, x222 = 0, y211 = 0, y212 = 0, y221 = 0, y222 = 0;
            if (right.Enabled)
            {
                x211 = (double)w - w * (100 - right.Up) / 100;
                x212 = (double)w - w * (100 - right.Up) / 100 / 2;
                y211 = (double)h;
                y212 = (double)(x212 - w / 2) * 1.732f;
                x221 = (double)w - w * (100 - right.Down) / 100;
                x222 = (double)w - w * (100 - right.Down) / 100 / 2;
                y221 = (double)h;
                y222 = (double)(x222 - w / 2) * 1.732f;
                e.Graphics.FillPolygon(hb, new Point[] { new Point(w, h), new Point((int)x211, (int)y211), new Point((int)x212, (int)y212) });
                e.Graphics.FillPolygon(hb, new Point[] { new Point(w / 2, 0), new Point(0, h), new Point((int)x221, (int)y221), new Point((int)x222, (int)y222) });
            }

            double x311 = 0, x312 = 0, x321 = 0, x322 = 0, y311 = 0, y312 = 0, y321 = 0, y322 = 0;
            if (left.Enabled)
            {
                x311 = (double)w * (100 - left.Up) / 100;
                x312 = (double)w * (100 - left.Up) / 100 / 2;
                y311 = (double)h;
                y312 = (double)(w / 2 - x312) * 1.732f;
                x321 = (double)w * (100 - left.Down) / 100;
                x322 = (double)w * (100 - left.Down) / 100 / 2;
                y321 = (double)h;
                y322 = (double)(w / 2 - x322) * 1.732f;
                e.Graphics.FillPolygon(hb, new Point[] { new Point(0, h), new Point((int)x311, (int)y311), new Point((int)x312, (int)y312) });
                e.Graphics.FillPolygon(hb, new Point[] { new Point(w / 2, 0), new Point(w, h), new Point((int)x321, (int)y321), new Point((int)x322, (int)y322) });
            }
            Pen p;
            if (right.Enabled)
            {
                p = new Pen(right.SelectedColor, 2);
                e.Graphics.DrawLine(p, (float)x211, (float)y211, (float)x212, (float)y212);
                e.Graphics.DrawLine(p, (float)x221, (float)y221, (float)x222, (float)y222);
                e.Graphics.DrawLine(p, (float)x221, (float)y221, (float)x211, (float)y211);
                e.Graphics.DrawLine(p, (float)x222, (float)y222, (float)x212, (float)y212);
                p.Dispose();
            }
            if (left.Enabled)
            {
                p = new Pen(left.SelectedColor, 2);
                e.Graphics.DrawLine(p, (float)x311, (float)y311, (float)x312, (float)y312);
                e.Graphics.DrawLine(p, (float)x321, (float)y321, (float)x322, (float)y322);
                e.Graphics.DrawLine(p, (float)x321, (float)y321, (float)x311, (float)y311);
                e.Graphics.DrawLine(p, (float)x322, (float)y322, (float)x312, (float)y312);
                p.Dispose();
            }
            if (up.Enabled)
            {
                p = new Pen(up.SelectedColor, 2);
                e.Graphics.DrawLine(p, (float)x111, (float)y111, (float)x112, (float)y112);
                e.Graphics.DrawLine(p, (float)x121, (float)y121, (float)x122, (float)y122);
                e.Graphics.DrawLine(p, (float)x121, (float)y121, (float)x111, (float)y111);
                e.Graphics.DrawLine(p, (float)x122, (float)y122, (float)x112, (float)y112);
                p.Dispose();
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (GrayEditor.Enabled)
            {
                int h = GrayPic.ClientSize.Height;
                int w = GrayPic.ClientSize.Width;
                Pen p = null;
                if (ImageProcess.histoData != null)
                {
                    p = new Pen(Color.Red);
                    for (int i = 0; i < 256; i++)
                    {
                        e.Graphics.DrawLine(p, i, h, i, h - ImageProcess.histoData[i]);
                    }
                    p.Dispose();
                }
                int u = GrayEditor.Up;
                int d = GrayEditor.Down;
                if (u - d > 2)
                {
                    p = new Pen(Color.Purple, 2);
                    e.Graphics.DrawRectangle(p, d, 0, u - d, h);
                    p.Dispose();
                }
                else
                {
                    p = new Pen(Color.Purple, u - d + 1);
                    e.Graphics.DrawLine(p, d, 0, d, h);
                    p.Dispose();
                }
            }
        }

        private void rangeEditor4_Notify(object sender, EventArgs e)
        {
            if (!UsingColPenNow)
                changeData();

            GrayPic.Invalidate();
        }


        enum changeType { up, all, down, none, uUp, dUp, uDown, dDown };
        bool changeing = false;
        changeType where = changeType.none;
        int mouseDownUp = 0;
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
            EnterHandle();
            if (!checkBoxGray.Checked)
                return;
            switch (GetChangeType(e.Location))
            {
                case changeType.none:
                    changeing = false;
                    where = changeType.none;
                    break;
                case changeType.uDown:
                    GrayEditor.Down++;
                    break;
                case changeType.uUp:
                    GrayEditor.Up++;
                    break;
                case changeType.dDown:
                    GrayEditor.Down--;
                    break;
                case changeType.dUp:
                    GrayEditor.Up--;
                    break;
                default:
                    changeing = true;
                    mouseDownUp = e.X - GrayEditor.Up;// maximum - maximum * e.Y / pictureBox1.Height - up;
                    pictureBox2_MouseMove(sender, e);
                    break;
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (!checkBoxGray.Checked)
                return;
            changeing = false;
            where = changeType.none;
            GrayPic.Cursor = Cursors.Default;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!checkBoxGray.Checked)
                return;
            if (changeing)
            {
                switch (where)
                {
                    case changeType.up:
                        GrayEditor.Up = e.X;
                        break;
                    case changeType.down:
                        GrayEditor.Down = e.X;
                        break;
                    case changeType.all:
                        int spane = GrayEditor.Up - GrayEditor.Down;
                        GrayEditor.Up = e.X - mouseDownUp;
                        GrayEditor.Down = GrayEditor.Up - spane;
                        if (GrayEditor.Up - GrayEditor.Down != spane)
                            GrayEditor.Up = GrayEditor.Down + spane;
                        break;
                }
            }
            else
            {
                GetChangeType(e.Location);
            }
        }
        private changeType GetChangeType(Point e)
        {
            Rectangle rect = new Rectangle(GrayEditor.Down, 0, GrayEditor.Up - GrayEditor.Down, GrayPic.ClientSize.Height);
            int delt = 3;
            if ((e.X < rect.X - delt))
            {
                where = changeType.dDown;
                GrayPic.Cursor = Cursors.PanWest;
            }
            else if ((e.X > rect.Right + delt))
            {
                where = changeType.uUp;
                GrayPic.Cursor = Cursors.PanEast;
            }
            else
            {
                if (e.X < rect.Left + delt)
                {
                    if (e.X > rect.Right)
                        where = changeType.up;
                    else
                        where = changeType.down;
                    GrayPic.Cursor = Cursors.SizeWE;
                }
                else if (e.X > rect.Right - delt)
                {
                    if (e.X < rect.Left)
                        where = changeType.down;
                    else
                        where = changeType.up;
                    GrayPic.Cursor = Cursors.SizeWE;
                }
                else if (rect.Width > 10 * delt)
                {
                    if (e.X < rect.Left + 4 * delt)
                    {
                        where = changeType.uDown;
                        GrayPic.Cursor = Cursors.PanEast;
                    }
                    else if (e.X > rect.Right - 4 * delt)
                    {
                        where = changeType.dUp;
                        GrayPic.Cursor = Cursors.PanWest;
                    }
                    else
                    {
                        where = changeType.all;
                        GrayPic.Cursor = Cursors.SizeAll;
                    }
                }
                else
                {
                    where = changeType.all;
                    GrayPic.Cursor = Cursors.SizeAll;
                }
            }
            return where;
        }


        private void ColorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDataTo(curColor);
            settingData = true;
            if (ColorsList.SelectedIndex < colors.Count)
            {
                curColor = colors[ColorsList.SelectedIndex];
                GetDataFrom(curColor);
            }
            else if (ColorsList.SelectedIndex == colors.Count)
            {
                int oindex = ColorsList.SelectedIndex;
                curColor = new ColorRange();
                GetDataFrom(curColor);
                colors.Add(curColor);
                ColorsList.Items.RemoveAt(oindex);
                ColorsList.Items.Add("颜色范围" + oindex.ToString());
                ColorsList.Items.Add("新建颜色项");
                ColorsList.SelectedIndex = oindex;
                if (DataChanged != null)
                    DataChanged(this, new EventArgs());
            }
            if (colors.Count > 1)
            {
                DelButton.Text = "删除" + ColorsList.SelectedIndex.ToString();
                DelButton.Enabled = true;
            }
            else
            {
                DelButton.Enabled = false;
            }
            ColorPic.Focus();
            CurFocus.ChangeColorRange(this);
            settingData = false;
        }

        private void operationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (curColor != null)
            {
                curColor.operation = (ColorRange.OperationType)operationList.SelectedIndex;
                changeData();
            }
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            if (colors.Count > 1)
            {
                //int oindex = ColorsList.SelectedIndex;
                colors.RemoveAt(ColorsList.SelectedIndex);
                SetSource(colors);
                ColorsList.SelectedIndex = colors.Count - 1;    //定位到最后一个
                if (DataChanged != null)
                    DataChanged(this, new EventArgs());
            }
        }

        private void MultiColors_MouseDown(object sender, MouseEventArgs e)
        {
            //Globles.ShowMousePos(e.X,e.Y);
        }

        private void MultiColors_Enter(object sender, EventArgs e)
        {
            EnterHandle();
        }
        private void MultiColors_Leave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }
        void EnterHandle()
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            CurFocus.ChangeColorRange(this);
        }
        void LeaveHandle()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            CurFocus.ChangeColorRange(null);
        }

        private void ColorPic_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
            EnterHandle();
        }
        private void MultiColors_EnabledChanged(object sender, EventArgs e)
        {
            if (StateButton.Checked)
            {
                settingData = true;
                StateButton.Checked = false;
                changeSize();
                if (SizeChanged != null)
                    SizeChanged(this, new EventArgs());
                settingData = false;
            }
        }

        /// <summary>
        /// 手动释放MultiColor中的控件
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        public void DisposeMultiColorManual()
        {
            this.DisposeCheckBoxGrayManual();
            this.DisposeStateButtonManual();
            this.DisposeCheckBoxRedManual();
            this.DisposeCheckBoxGreenManual();
            this.DisposeCheckBoxBlueManual();
            this.DisposeOperationListManual();
            this.DisposeColorsListManual();
            this.DisposeDelButtonManual();
            this.DisposePictureBox5Manual();
            this.DisposePictureBox4Manual();
            this.DisposePictureBox3Manual();
            this.DisposeGrayPicManual();
            this.DisposeColorPicManual();
            this.DisposeGrayEditorManual();
            this.DisposeBlueEditorManual();
            this.DisposeGreenEditorManual();
            this.DisposeRedEditorManual();
            this.DisposeSelfManual();
        }

        /// <summary>
        /// 手动释放控件checkBoxGray，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeCheckBoxGrayManual()
        {
            if (null != this.checkBoxGray)
            {
                this.checkBoxGray.CheckedChanged -= new System.EventHandler(this.checkBoxGray_CheckedChanged);
                this.checkBoxGray.Dispose();
                this.checkBoxGray = null;
            }
        }

        /// <summary>
        /// 手动释放控件StateButton，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeStateButtonManual()
        {
            if (null != this.StateButton)
            {
                this.StateButton.CheckedChanged -= new System.EventHandler(this.checkBox3_CheckedChanged);
                this.StateButton.Dispose();
                this.StateButton = null;
            }
        }

        /// <summary>
        /// 手动释放控件checkBoxRed，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeCheckBoxRedManual()
        {
            if (null != this.checkBoxRed)
            {
                this.checkBoxRed.CheckedChanged -= new System.EventHandler(this.checkBoxRed_CheckedChanged);
                this.checkBoxRed.Dispose();
                this.checkBoxRed = null;
            }
        }

        /// <summary>
        /// 手动释放控件checkBoxGreen，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeCheckBoxGreenManual()
        {
            if (null != this.checkBoxGreen)
            {
                this.checkBoxGreen.CheckedChanged -= new System.EventHandler(this.checkBoxGreen_CheckedChanged);
                this.checkBoxGreen.Dispose();
                this.checkBoxGreen = null;
            }
        }

        /// <summary>
        /// 手动释放控件checkBoxBlue，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeCheckBoxBlueManual()
        {
            if (null != this.checkBoxBlue)
            {
                this.checkBoxBlue.CheckedChanged -= new System.EventHandler(this.checkBoxBlue_CheckedChanged);
                this.checkBoxBlue.Dispose();
                this.checkBoxBlue = null;
            }
        }

        /// <summary>
        /// 手动释放控件operationList，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeOperationListManual()
        {
            if (null != this.operationList)
            {
                this.operationList.SelectedIndexChanged -= new System.EventHandler(this.operationList_SelectedIndexChanged);
                this.operationList.Dispose();
                this.operationList = null;
            }
        }

        /// <summary>
        /// 手动释放控件ColorsList，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeColorsListManual()
        {
            if (null != this.ColorsList)
            {
                this.ColorsList.SelectedIndexChanged -= new System.EventHandler(this.ColorsList_SelectedIndexChanged);
                this.ColorsList.Dispose();
                this.ColorsList = null;
            }
        }

        /// <summary>
        /// 手动释放控件DelButton，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeDelButtonManual()
        {
            if (null != this.DelButton)
            {
                this.DelButton.Dispose();
                this.DelButton = null;
            }
        }

        /// <summary>
        /// 手动释放控件pictureBox5，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposePictureBox5Manual()
        {
            if (null != this.pictureBox5)
            {
                this.pictureBox5.Dispose();
                this.pictureBox5 = null;
            }
        }

        /// <summary>
        /// 手动释放控件pictureBox4，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposePictureBox4Manual()
        {
            if (null != this.pictureBox4)
            {
                this.pictureBox4.Dispose();
                this.pictureBox4 = null;
            }
        }

        /// <summary>
        /// 手动释放控件pictureBox3，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposePictureBox3Manual()
        {
            if (null != this.pictureBox3)
            {
                this.pictureBox3.Dispose();
                this.pictureBox3 = null;
            }
        }

        /// <summary>
        /// 手动释放控件GrayPic，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeGrayPicManual()
        {
            if (null != this.GrayPic)
            {
                this.GrayPic.Paint -= new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
                this.GrayPic.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
                this.GrayPic.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
                this.GrayPic.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
                this.GrayPic.Dispose();
                this.GrayPic = null;
            }
        }

        /// <summary>
        /// 手动释放控件ColorPic，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeColorPicManual()
        {
            if (null != this.ColorPic)
            {
                this.ColorPic.Paint -= new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
                this.ColorPic.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ColorPic_MouseDown);
                this.ColorPic.Dispose();
                this.ColorPic = null;
            }
        }

        /// <summary>
        /// 手动释放控件GrayEditor，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeGrayEditorManual()
        {
            if (null != this.GrayEditor)
            {
                this.GrayEditor.Notify -= new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor4_Notify);
                this.GrayEditor.GetFocus -= new SPI.RangeEditor.NotifyEventHandler(this.GrayEditor_GetFocus);
                this.GrayEditor.Dispose();
                this.GrayEditor = null;
            }
        }

        /// <summary>
        /// 手动释放控件BlueEditor，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeBlueEditorManual()
        {
            if (null != this.BlueEditor)
            {
                this.BlueEditor.Notify += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor3_Notify);
                this.BlueEditor.GetFocus += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor3_GetFocus);
                this.BlueEditor.Dispose();
                this.BlueEditor = null;
            }
        }

        /// <summary>
        /// 手动释放控件GreenEditor，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeGreenEditorManual()
        {
            if (null != this.GreenEditor)
            {
                this.GreenEditor.Notify += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor2_Notify);
                this.GreenEditor.GetFocus += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor2_GetFocus);
                this.GreenEditor.Dispose();
                this.GreenEditor = null;
            }
        }

        /// <summary>
        /// 手动释放控件RedEditor，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeRedEditorManual()
        {
            if (null != this.RedEditor)
            {
                this.RedEditor.Notify += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor1_Notify);
                this.RedEditor.GetFocus += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor1_GetFocus);
                this.RedEditor.Dispose();
                this.RedEditor = null;
            }
        }

        /// <summary>
        /// 手动释放控件本身，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140626</date>
        private void DisposeSelfManual()
        {
            if (null != this)
            {
                this.EnabledChanged -= new System.EventHandler(this.MultiColors_EnabledChanged);
                this.Enter -= new System.EventHandler(this.MultiColors_Enter);
                this.Leave -= new System.EventHandler(this.MultiColors_Leave);
                this.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.MultiColors_MouseDown);
                this.Dispose();
            }
        }
        #region 抽色笔相关功能
        /// <summary>
        /// 开启/关闭 抽色笔抽色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbUseColorPen_CheckedChanged(object sender, EventArgs e)
        {
           UseColPen= cbUseColorPen.Checked;
           if (UseColPen)
           {
               changeData();
               CurMc = this;
               BackUpCurColor();
               this.Invalidate();
               loadColorPenText();              
           }
           else
               CurMc = null;
         }
        /// <summary>
        /// 保存抽色笔配置
        /// </summary>
        private void saveColorPenText()
        {
//             string savePath = Directory.GetCurrentDirectory() + @"\ColorPen.txt";
//             StreamWriter sw = null;
// 
//             try
//             {
//                 sw = new StreamWriter(savePath);
//                 sw.WriteLine(txDensity.Text);
//             }
//             catch (Exception)
//             {
// 
//             }
//             sw.Dispose();
        }
        /// <summary>
        /// 加载抽色笔配置
        /// </summary>
        private void loadColorPenText()
        {
//             string savePath = Directory.GetCurrentDirectory() + @"\ColorPen.txt";
//             if (!File.Exists(savePath))
//             {
//                 return;
//             }
//             StreamReader sr = null;
//             try
//             {
//                 sr = new StreamReader(savePath);
//                 txDensity.Text = sr.ReadLine();                   
//             }
//             catch (Exception)
//             {
// 
//             }
//             sr.Dispose();
        }

        /// <summary>
        /// 获取当前颜色笔大小
        /// </summary>
        /// <returns></returns>
        public Size GetColorPenSize()
        {
            string indexs = cbColPenSize.Text.ToString();
            int width = SafeParseInt(indexs.Substring(0, 1),1);
            int height = SafeParseInt(indexs.Substring(2, 1), 1);
            return new Size(width, height);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MultiColors_Enter(null, null);
            cbUseColorPen.Checked = !cbUseColorPen.Checked;
        }
        /// <summary>
        /// 关闭抽色笔
        /// </summary>
        public void CloseColorPen()
        {
            cbUseColorPen.Checked = false;
            UseColPen = false;
            UsingColPenNow = false;
            if (backupColorList != null) backupColorList.Clear();
        }

        private List<ColorRange> backupColorList = null;        
        public void BackUpCurColor()
        {
            if (curColor != null) 
            {
                ColorRange tempColor = new ColorRange();
                tempColor.CopyFrom(curColor);
                ColorRange lastColor = null;
                if (backupColorList.Count > 1)
                {
                    lastColor = backupColorList[backupColorList.Count - 1];
                    if (tempColor.redUsed == lastColor.redUsed &&
                        tempColor.redUp == lastColor.redUp &&
                        tempColor.redDown == lastColor.redDown &&
                        tempColor.greenUsed == lastColor.greenUsed &&
                        tempColor.greenUp == lastColor.greenUp &&
                        tempColor.greenDown == lastColor.greenDown &&
                        tempColor.blueUsed == lastColor.blueUsed &&
                        tempColor.blueUp == lastColor.blueUp &&
                        tempColor.blueDown == lastColor.blueDown &&
                        tempColor.grayUsed == lastColor.grayUsed &&
                        tempColor.grayUp == lastColor.grayUp &&
                        tempColor.grayDown == lastColor.grayDown)
                        return;
                }
                backupColorList.Add(tempColor); }
        }
        /// <summary>
        /// 清楚当前的颜色设置
        /// </summary>
        public void ClearCurcolor()
        {
            ColorRange cr = new ColorRange();
            cr.redUsed = true;
            cr.redUp = 0;
            cr.redDown = 0;
            cr.greenUsed = true;
            cr.greenUp = 0;
            cr.greenDown = 0;
            cr.blueUsed = true;
            cr.blueUp = 0;
            cr.blueDown = 0;
            cr.grayUsed = true;
            cr.grayUp = 0;
            cr.grayDown = 0;
            cr.operation = curColor.operation;

            isClear = true;
            GetDataFrom(cr);
            isClear = false;
            changeData();

            if (backupColorList != null) backupColorList.Clear();
        }

        /// <summary>
        /// 恢复抽色笔上一次的抽色
        /// </summary>
        public void RestoreLastColor()
        {
            if (backupColorList != null && backupColorList.Count > 0)
            {
                ColorRange cr = backupColorList[backupColorList.Count - 1];
                GetDataFrom(cr);
                changeData();
                backupColorList.RemoveAt(backupColorList.Count - 1);
            }
        }
        /// <summary>
        /// 抽色浓度
        /// </summary>
        int density
        {
            get { return int.Parse(txDensity.Text); }
        }

        /// <summary>
        /// 扩展颜色范围
        /// </summary>
        /// <param name="cr"></param>
        public void EnlageColorRange(ColorRange cr)
        {
            ColorRange c = this.curColor;
            bool hascolor = c.redUsed || c.blueUsed || c.greenUsed || c.grayUsed;
            //浓度扩展
            cr.redUp = (byte)((cr.redUp + density) > 100 ? 100 : (cr.redUp + density));
            cr.redDown = (byte)((cr.redDown - density) < 0 ? 0 : (cr.redDown - density));
            cr.greenUp = (byte)((cr.greenUp + density) > 100 ? 100 : (cr.greenUp + density));
            cr.greenDown = (byte)((cr.greenDown - density) < 0 ? 0 : (cr.greenDown - density));
            cr.blueUp = (byte)((cr.blueUp + density) > 100 ? 100 : (cr.blueUp + density));
            cr.blueDown = (byte)((cr.blueDown - density) < 0 ? 0 : (cr.blueDown - density));
            cr.grayUp = (byte)((cr.grayUp + density) > 255 ? 255 : (cr.grayUp + density));
            cr.grayDown = (byte)((cr.grayDown - density) < 0 ? 0 : (cr.grayDown - density));

            if (!(c.redDown == 0 && c.redUp == 0))
            {
                if (c.redUsed && c.redDown < cr.redDown) cr.redDown = c.redDown;
                if (c.redUsed && c.redUp > cr.redUp) cr.redUp = c.redUp;
            }
            if (hascolor && !c.redUsed) { cr.redUp = 100; cr.redDown = 0; }

            if (!(c.greenDown == 0 && c.greenUp == 0))
            {
                if (c.greenUsed && c.greenDown < cr.greenDown) cr.greenDown = c.greenDown;
                if (c.greenUsed && c.greenUp > cr.greenUp) cr.greenUp = c.greenUp;
            }
            if (hascolor && !c.greenUsed) { cr.greenUp = 100; cr.greenDown = 0; }

            if (!(c.blueDown == 0 && c.blueUp == 0))
            {
                if (c.blueUsed && c.blueDown < cr.blueDown) cr.blueDown = c.blueDown;
                if (c.blueUsed && c.blueUp > cr.blueUp) cr.blueUp = c.blueUp;
            }
            if (hascolor && !c.blueUsed) { cr.blueUp = 100; cr.blueDown = 0; }

            if (!(c.grayDown == 0 && c.grayUp == 0))
            {
                if (c.grayUsed && c.grayDown < cr.grayDown) cr.grayDown = c.grayDown;
                if (c.grayUsed && c.grayUp > cr.grayUp) cr.grayUp = c.grayUp;
            }
            if (hascolor && !c.grayUsed) { cr.grayDown = 0; cr.grayUp = 255; }

            cr.operation = c.operation;

            GetDataFrom(cr);
            changeData();
        }

        public ColorRange GetCurColor() { return this.curColor; }
        
		private void btRollback_Click(object sender, EventArgs e)
        {
            RestoreLastColor();
        }
       
		private void btClear_Click(object sender, EventArgs e)
        {
            ClearCurcolor();
        }

        private void tbDensity_Scroll(object sender, EventArgs e)
        {
            txDensity.Text = tbDensity.Value.ToString();
            saveColorPenText();
        }

        private void txDensity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbDensity.Value = int.Parse(txDensity.Text);
            }
            catch (Exception)
            {
                txDensity.Text = tbDensity.Value.ToString();
            }
            saveColorPenText();
        }

        #endregion

        
    }
}

