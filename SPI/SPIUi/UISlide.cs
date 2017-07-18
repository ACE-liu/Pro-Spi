using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPI.SPICheckWin;
using SPI.Global;

using static SPI.Global.Configuration;

namespace SPI.SPIUi
{
    class UISlide : UIBase
    {
        Panel ui;
        Label lbResult; //保留结果label显示测试结果
        Label testItem;
        EditorRangeForm slideControl;
        public string text;
        private string backUnit;//单位% um
        string resultDescribe;
        private bool isLargeValueOk;
        bool hasTested = false;

        bool hasAppendSet;//增加切面高度的设置，控件的拓展性很差...

        private double sectionHeight;

        private double minValue;
        private double leftValue;
        private double rightValue;
        private double maxValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="win"></param>
        /// <param name="lvl"></param>
        /// <param name="text">text==null 表示不添加label</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="isLargeValueOk"></param>
        public UISlide(CheckWinBase win, string backUnit, int lvl, string text, double min, double max, bool isLargeValueOk, bool hasAppendSet = false) : base(win, lvl)
        {
            this.text = text;
            this.backUnit = backUnit;
            this.isLargeValueOk = isLargeValueOk;
            this.minValue = min;
            this.maxValue = max;
            this.hasAppendSet = hasAppendSet;
        }
        public override void DisposeUIManual()
        {
            if (null != this.slideControl)
            {
                this.slideControl.RemoveAllDelegate();
                this.slideControl.Dispose();
                this.slideControl = null;
            }
            if (null != ui)
            {
                ui.Dispose(); ui = null;
            }
        }
        /// <summary>
        /// 根据测试值获取测试的结果
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public TestStatus GetTestStatusByValue(double testValue)
        {
            TestStatus rtn = TestStatus.OK;
            if (testValue > maxValue || testValue < minValue)
            {
                throw new ArgumentOutOfRangeException("testValue");
            }
            if (isLargeValueOk)
            {
                if (testValue > rightValue && testValue <= maxValue)
                {
                    rtn = TestStatus.OK;
                }
                else if (testValue >= leftValue && testValue <= rightValue)
                {
                    rtn = TestStatus.WARN;
                }
                else
                    rtn = TestStatus.NG;
            }
            else
            {
                if (testValue > rightValue && testValue <= maxValue)
                {
                    rtn = TestStatus.NG;
                }
                else if (testValue >= leftValue && testValue <= rightValue)
                {
                    rtn = TestStatus.WARN;
                }
                else
                    rtn = TestStatus.OK;
            }
            return rtn;
        }
        public override Control CreateUI()
        {
            ui = new Panel();
            ui.SuspendLayout();
            ui.Font = CurFocusPanel?.Font;
            ui.AutoSize = true;
            if (text != null)
            {
                testItem = new Label();
                testItem.Text = text;
                testItem.Left = ControlsGap;
                testItem.Font = ui.Font;
                testItem.AutoSize = true;
                ui.Controls.Add(testItem);
            }
            if (hasTested)
            {
                lbResult = new Label();
                lbResult.Text = resultDescribe;
                lbResult.Left = (testItem?.Left ?? 0) + ControlsGap;
                testItem.AutoSize = true;
                ui.Controls.Add(lbResult);
            }
            if (hasAppendSet)
            {
                Label lb = new Label();
                lb.Text = "切面高度";
                lb.Left = CurFocusPanel.Width / 2 - 2 * ControlsGap;
                lb.AutoSize = true;
                ui.Controls.Add(lb);
                TextBox tb = new TextBox();
                tb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                tb.Left = lb.Right + ControlsGap;
                tb.Size = new System.Drawing.Size(32, 23);
                tb.TextChanged += Tb_TextChanged;
                ui.Controls.Add(tb);
                Label lb1 = new Label();
                lb1.Text = backUnit;
                lb1.Left = tb.Right + ControlsGap;
                lb1.AutoSize = true;
                ui.Controls.Add(lb1);
            }
            slideControl = new EditorRangeForm(backUnit, minValue, maxValue, leftValue, rightValue, isLargeValueOk);
            slideControl.DataChanged += SlideControl_DataChanged;
            if (testItem != null)
            {
                slideControl.Top = testItem.Bottom + ControlsGap;
            }
            else
                slideControl.Top = ControlsGap;
            slideControl.Left = ControlsGap;
            ui.Controls.Add(slideControl);
            //ui.Width = slideControl.Width;
            //ui.Height = slideControl.Bottom + ControlsGap;
            ui.ResumeLayout();
            return ui;

        }
        public override void Save(MyWriter mw)
        {
            mw.Save(minValue);
            mw.Save(leftValue);
            mw.Save(rightValue);
            mw.Save(maxValue);
        }
        public override void LoadFrom(MyReader mr)
        {
            minValue = mr.LoadDouble();
            leftValue = mr.LoadDouble();
            rightValue = mr.LoadDouble();
            maxValue = mr.LoadDouble();
        }
        private void Tb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double value = 0;
                if (!string.IsNullOrEmpty((sender as TextBox).Text))
                {
                    value = double.Parse((sender as TextBox).Text);
                }
                sectionHeight = value;
            }
            catch (Exception)
            {
                (sender as TextBox).Text = sectionHeight.ToString();
            }
             //(sender as TextBox).Text = sectionHeight.ToString();
        }

        private void SlideControl_DataChanged(object sender, EventArgs e)
        {
            this.minValue = slideControl.Min;
            this.maxValue = slideControl.Max;
            this.leftValue = slideControl.LeftValue;
            this.rightValue = slideControl.RightValue;
        }
    }
}
