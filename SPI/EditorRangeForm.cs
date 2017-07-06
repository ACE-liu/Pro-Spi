using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPI
{
    public partial class EditorRangeForm : UserControl
    {
        public event EventHandler DataChanged = null;
        public double Max => max;
        public double Min => min;
        public double LeftValue => left.Value;
        public double RightValue => right.Value;
        protected string Unit = "%";
        protected int topControlY = 4;
        protected int BottomControlY => pbColor.Bottom + 4;
        protected double max = 100;
        protected double min = 0;
        protected bool isLargeValueOK = false;
        public double height => pbColor.Height;
        public double width => pbColor.Width;
        /// <summary>
        /// 保留三个brush 
        /// </summary>
        protected Brush OkBrush = Brushes.Green;
        protected Brush warnBrush = Brushes.Yellow;
        protected Brush ngBrush = Brushes.Red;
        protected List<SlideBlock> blockList = null;
        protected SlideBlock curSlideBlock = null;
        protected SlideBlock left;
        protected SlideBlock right;
        public EditorRangeForm()
        {
            InitializeComponent();
            //initControls();

        }
        public virtual void RefreshRect(Graphics e)
        {
            if (blockList != null)
            {
                if (blockList.Count == 2)
                {
                    SlideBlock left = blockList.Find((p) => p.Left == null);
                    double leftLevel = left.DrawXLocation;
                    double rightLevel = left.right.DrawXLocation;
                    if (isLargeValueOK)
                    {
                        e.FillRectangle(ngBrush, new Rectangle(0, 0, (int)leftLevel, (int)height));
                        e.FillRectangle(warnBrush, new Rectangle((int)leftLevel, 0, (int)(rightLevel - leftLevel), (int)height));
                        e.FillRectangle(OkBrush, new Rectangle((int)rightLevel, 0, (int)(width - rightLevel), (int)height));
                    }
                    else
                    {
                        e.FillRectangle(OkBrush, new Rectangle(0, 0, (int)leftLevel, (int)height));
                        e.FillRectangle(warnBrush, new Rectangle((int)leftLevel, 0, (int)(rightLevel - leftLevel), (int)height));
                        e.FillRectangle(ngBrush, new Rectangle((int)rightLevel, 0, (int)(width - rightLevel), (int)height));
                    }
                }
            }
        }
        protected void UpdateChange()
        {
            if (DataChanged!=null)
            {
                DataChanged(this, new EventArgs());
            }
        }
        /// <summary>
        /// 窗体初始化程序，包括单位，范围，以及OK，ng的标准
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        /// <param name="isLargeValueOK"></param>
        public EditorRangeForm(string unit, double min, double max, double leftValue, double rightValue, bool isLargeValueOK)
        {
            InitializeComponent();
            Unit = unit;
            this.min = min;
            this.max = max;
            blockList = new List<SlideBlock>();
            left = new SlideBlock(leftValue, min, max, width, unit);
            right = new SlideBlock(rightValue, min, max, width, unit);
            left.right = right;
            left.Left = null;
            right.Left = left;
            right.right = null;
            blockList.Add(left);
            blockList.Add(right);
            this.isLargeValueOK = isLargeValueOK;
            initControls();
            RefreshControls();
        }
        public virtual void RemoveAllDelegate()
        {
            foreach (var item in DataChanged.GetInvocationList())
            {
                DataChanged -= item as EventHandler;
            }
        }
        /// <summary>
        /// 构造时初始化控件
        /// </summary>
        protected void initControls()
        {
            lbUnit.Text = Unit;
            lbUnit1.Text = Unit;
            tbMax.Text = max.ToString();
            tbMin.Text = min.ToString();

        }
        /// <summary>
        /// 只刷新需要修改的值
        /// </summary>
        protected void RefreshControls()
        {
            if (blockList != null)
            {
                for (int i = 0; i < blockList.Count; i++)
                {
                    int y = 0;
                    if (i % 2 == 0)
                    {
                        y = BottomControlY;
                    }
                    else
                    {
                        y = topControlY;
                    }
                    blockList[i].CreateLb(this, y);
                }
            }
        }
        private bool IsvalidValue(string text)
        {
            return false;
        }
        private void tbMax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double value = double.Parse(tbMax.Text);
                max = value;
            }
            catch (Exception)
            {
                tbMax.Text = max.ToString();
            }
        }
        private double mouseDownCalculateLoc;
        private void pbColor_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownCalculateLoc = e.Location.X;
        }

        private void pbColor_MouseMove(object sender, MouseEventArgs e)
        {
            bool dataChanged = false;
            double xPos = e.Location.X;
            SlideBlock sb = blockList?.Find((p) => p.IsMouseOn(xPos));
            if (sb != null || curSlideBlock != null)
            {
                pbColor.Cursor = Cursors.SizeWE;
            }
            else
            {
                pbColor.Cursor = Cursors.Default;
            }
            if (e.Button == MouseButtons.Left)//鼠标左键按下
            {
                if (curSlideBlock == null)
                {
                    curSlideBlock = sb;
                }
                else
                {
                    curSlideBlock.Add(e.Location.X - mouseDownCalculateLoc);
                    mouseDownCalculateLoc = e.Location.X;
                    dataChanged = true;
                }
            }
            if (dataChanged)
            {
                RefreshControls();
                Refresh();
                UpdateChange();
            }

        }

        private void pbColor_Paint(object sender, PaintEventArgs e)
        {
            RefreshRect(e.Graphics);
        }

        private void pbColor_MouseUp(object sender, MouseEventArgs e)
        {
            curSlideBlock = null;
        }
    }
    /// <summary>
    /// 滑块位置类
    /// </summary>
    public class SlideBlock
    {
        private static int rightShiftX = 20;
        private double ParentWidth;
        const int delt = 3;
        private double value;
        private double minLevel;
        private double maxLevel;
        public SlideBlock Left;
        public SlideBlock right;
        private string lbUnit;
        public Label slideLb = null;
        public double Value => value;
        public double DrawXLocation => value / (maxLevel + minLevel) * ParentWidth;
        public void SetMaxLevel(double maxlevel)
        {
            this.maxLevel = maxlevel;
        }
        public void CreateLb(Control parent, int y)
        {
            if (slideLb == null)
            {
                slideLb = new Label();
                slideLb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                slideLb.Size = new System.Drawing.Size(40, 23);
                slideLb.Text = (int)value + lbUnit;
                slideLb.Location = new Point((int)(value / (maxLevel + minLevel) * ParentWidth) + rightShiftX, y);
                parent.Controls.Add(slideLb);
            }
        }
        private void RefreshX()
        {
            if (slideLb != null)
            {
                slideLb.Location = new Point((int)(value / (maxLevel + minLevel) * ParentWidth) + rightShiftX, slideLb.Location.Y);
                slideLb.Text = (int)value + lbUnit;
            }
        }

        public void Add(double addValue)
        {
            this.value += addValue / ParentWidth * (maxLevel + minLevel);
            OnValueChange();
            RefreshX();
        }
        public SlideBlock(double value, double minLevel, double MaxLevel, double parentWidth, string lbUnit, SlideBlock left = null, SlideBlock right = null)
        {
            this.ParentWidth = parentWidth;
            this.value = value;
            this.minLevel = minLevel;
            this.maxLevel = MaxLevel;
            this.lbUnit = lbUnit;
            this.Left = left;
            this.right = right;
        }
        /// <summary>
        /// 确认鼠标是否在当前滑块
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsMouseOn(double value)
        {
            if (Math.Abs(value - this.DrawXLocation) <= delt)
            {
                return true;
            }
            return false;
        }
        public void OnValueChange()
        {
            if (Left != null)
            {
                if (value <= Left.value)
                {
                    value = Left.value;
                }
            }
            else
            {
                if (value <= minLevel)
                {
                    value = minLevel;
                }
            }
            if (right == null)
            {
                if (value >= maxLevel)
                {
                    value = maxLevel;
                }
            }
            else
            {
                if (value >= right.value)
                {
                    value = right.value;
                }
            }
        }
    }
}
