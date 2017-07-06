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
    public partial class DoubleEditorRangeForm : EditorRangeForm
    {
        public double LeftValue1 => left1.Value;
        public double RightValue1 => right1.Value;
        SlideBlock left1;
        SlideBlock right1;
        public DoubleEditorRangeForm()
        {
            InitializeComponent();
        }
        public override void RefreshRect(Graphics e)
        {
             if (blockList?.Count == 4)
            {
                SlideBlock left = blockList.Find((p) => p.Left == null);
                double leftLevel = left.DrawXLocation;
                double rightLevel = left.right.DrawXLocation;

                double leftLevel1 = left.right.right.DrawXLocation;
                double rightLevel1 = left.right.right.right.DrawXLocation;

                if (isLargeValueOK)
                {
                    e.FillRectangle(OkBrush, new Rectangle(0, 0, (int)leftLevel, (int)height));
                    e.FillRectangle(warnBrush, new Rectangle((int)leftLevel, 0, (int)(rightLevel - leftLevel), (int)height));
                    e.FillRectangle(ngBrush, new Rectangle((int)rightLevel, 0, (int)(leftLevel1 - rightLevel), (int)height));
                    e.FillRectangle(warnBrush, new Rectangle((int)leftLevel1, 0, (int)(rightLevel1 - leftLevel1), (int)height));
                    e.FillRectangle(OkBrush, new Rectangle((int)rightLevel1, 0, (int)(width - rightLevel1), (int)height));
                }
                else
                {
                    e.FillRectangle(ngBrush, new Rectangle(0, 0, (int)leftLevel, (int)height));
                    e.FillRectangle(warnBrush, new Rectangle((int)leftLevel, 0, (int)(rightLevel - leftLevel), (int)height));
                    e.FillRectangle(OkBrush, new Rectangle((int)rightLevel, 0, (int)(leftLevel1 - rightLevel), (int)height));
                    e.FillRectangle(warnBrush, new Rectangle((int)leftLevel1, 0, (int)(rightLevel1 - leftLevel1), (int)height));
                    e.FillRectangle(ngBrush, new Rectangle((int)rightLevel1, 0, (int)(width - rightLevel1), (int)height));
                }
            }
        }
        /// <summary>
        /// 4个滑块的构造函数
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        /// <param name="isLargeValueOK"></param>
        /// <param name="slideCount"></param>
        public DoubleEditorRangeForm(string unit, double min, double max, double leftValue, double rightValue, double leftValue1, double rightValue1, bool isLargeValueOK, int slideCount = 4)
        {
            InitializeComponent();
            Unit = unit;
            this.min = min;
            this.max = max;
            blockList = new List<SlideBlock>();
            left = new SlideBlock(leftValue, min, max, width, unit);
            right = new SlideBlock(rightValue, min, max, width, unit);
            left1 = new SlideBlock(leftValue1, min, max, width, unit);
            right1 = new SlideBlock(rightValue1, min, max, width, unit);
            left.right = right;
            left.Left = null;
            right.Left = left;
            right.right = left1;
            left1.Left = right;
            left1.right = right1;
            right1.Left = left1;
            right1.right = null;
            blockList.Add(left);
            blockList.Add(right);
            blockList.Add(left1);
            blockList.Add(right1);
            this.isLargeValueOK = isLargeValueOK;
            initControls();
            RefreshControls();
        }
    }
}
