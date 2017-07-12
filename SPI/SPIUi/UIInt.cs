using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.Global;
using System.Windows.Forms;
using static SPI.Global.Configuration;
using SPI.SPICheckWin;
using System.Drawing;
using System.Resources;
using System.Reflection;

namespace SPI.SPIUi
{
    /// <summary>
    /// 用于设置整数值的控件
    /// </summary>
    internal class UIInt : UIBase
    {
        //Data part
        /// <summary>
        /// 数据改变时处理函数指针
        /// </summary>
        public event EventHandler DataChanged;
        /// <summary>
        /// 设置的值，公共属性
        /// </summary>
        public int Target { get { return target; } set { target = value; } }
        /// <summary>
        /// 设置的值，私有变量
        /// </summary>
        int target;
        /// <summary>
        /// 实测值
        /// </summary>
        public int actual;
        /// <summary>
        /// 测试结果类型
        /// </summary>
        ResultType testResult;
        /// <summary>
        /// 是否已有测试结果
        /// </summary>
        bool hasResult;

        //UI part
        /// <summary>
        /// 容器，用于包装需要的其他控件为统一控件。
        /// </summary>
        Panel ui;
        /// <summary>
        /// 显示数值的文本框
        /// </summary>
        protected TextBox tbTarget;
        /// <summary>
        /// 显示测试结果数值的文本框
        /// </summary>
        TextBox tbTestValue;
        /// <summary>
        /// 显示测试结果类型的文本标签
        /// </summary>
        Label lbTestResult;
        /// <summary>
        /// 显示在参数前面的标签
        /// </summary>
        Label lbPre;
        /// <summary>
        /// 显示在参数前面的文本
        /// </summary>
        public string pre;
        /// <summary>
        /// 显示在参数后面的文本
        /// </summary>
        string post;
        /// <summary>
        /// 是否需要显示测试结果
        /// </summary>
        protected bool check;
        /// <summary>
        /// 常用构造函数
        /// </summary>
        /// <param name="win">包含此UI的检查框</param>
        /// <param name="lvl">显示层级。0为最靠左边，其他层次以等量缩进右缩</param>
        /// <param name="pr">显示在参数前面的文本</param>
        /// <param name="po">显示在参数后面的文本</param>
        /// <param name="ck">是否需要显示测试结果</param>
        public UIInt(CheckWinBase win, int lvl, string pr, string po, bool ck)
            : base(win, lvl)
        {
            target = 0;
            hasResult = false;
            pre = pr;
            post = po;
            check = ck;
        }

        /// <summary>
        /// 创建UI前先释放之前的资源，防止内存泄露
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140625</date>
        public override void DisposeUIManual()
        {
            if (null != this.DataChanged)
            {
                this.DataChanged = null;
            }
            if (null != this.ui)
            {
                this.ui.Dispose();
                this.ui = null;
            }
            if (null != this.tbTarget)
            {
                this.tbTarget.TextChanged -= new EventHandler(targetBox_TextChanged);
                this.tbTarget.Dispose();
                this.tbTarget = null;
            }
            if (null != this.tbTestValue)
            {
                this.tbTestValue.Dispose();
                this.tbTestValue = null;
            }
            if (null != this.lbTestResult)
            {
                this.lbTestResult.Dispose();
                this.lbTestResult = null;
            }
            if (null != this.lbPre)
            {
                this.lbPre.Dispose();
                this.lbPre = null;
            }
        }

        /// <summary>
        /// 创建显示控件。由于大部分UI设置的参数不需要显示，设计为只为需要显示的UI类对象创建界面控件。
        /// </summary>
        /// <returns></returns>
        public override Control CreateUI()
        {
            try
            {
                ui = new Panel(); ui.SuspendLayout();
                ui.Font = CurFocusPanel.Font;
                ui.Width = CurFocusPanel.Width - ControlsGap *(2+level);
                lbPre = new Label(); lbPre.SuspendLayout();
                lbPre.AutoSize = true;
                lbPre.Font = ui.Font;
                lbPre.Text = pre;
                lbPre.Left = ControlsGap;
                tbTarget = new TextBox(); tbTarget.SuspendLayout();
                tbTarget.Text = target.ToString();
                tbTarget.TextChanged += new EventHandler(targetBox_TextChanged);
                tbTarget.Left = lbPre.Left+ (int)GetControlSize(lbPre,pre,lbPre.Font).Width+ControlsGap;
                tbTarget.Width = 45;
                Label lbPost = new Label(); lbPost.SuspendLayout();
                lbPost.Text = post;
                lbPost.Left = tbTarget.Right + ControlsGap;
                lbPost.Width = 60;

                lbPre.ResumeLayout();
                tbTarget.ResumeLayout();
                lbPost.ResumeLayout();
                ui.Controls.Add(lbPre);
                ui.Controls.Add(tbTarget);
                ui.Controls.Add(lbPost);
                if (check)
                {
                   // RmlDebug.Assume(pre != null);
                    Label lbNameTest = new Label(); lbNameTest.SuspendLayout();
                    lbNameTest.Font = CurFocusPanel.Font;

                    lbNameTest.Text = "实测";
                    lbNameTest.AutoSize = true;
                    lbNameTest.Left = ui.Width - 140;
                    lbNameTest.Top += 4;
                    tbTestValue = new TextBox(); tbTestValue.SuspendLayout();
                    tbTestValue.Width = 50;
                    tbTestValue.Left = ui.Width - 85;
                    tbTestValue.ReadOnly = true;
                    if (hasResult) tbTestValue.Text = actual.ToString();
                    lbTestResult = new Label(); lbTestResult.SuspendLayout();
                    lbTestResult.Left = tbTestValue.Right + ControlsGap;
                    lbTestResult.Top += 4;
                    lbTestResult.AutoSize = true;
                    if (hasResult) lbTestResult.Text = (testResult == ResultType.OK) ? "OK" : "NG";

                    lbNameTest.ResumeLayout();
                    tbTestValue.ResumeLayout();
                    lbTestResult.ResumeLayout();
                    ui.Controls.Add(lbNameTest);
                    ui.Controls.Add(tbTestValue);
                    ui.Controls.Add(lbTestResult);

                }
                ui.Height = SetRelateTop(ui);
                ui.BorderStyle = BorderStyle.FixedSingle;
                ui.ResumeLayout();
            }
            catch (Exception ex)
            {
                //RmlDebug.Wrong(ex.Message);
            }
            return ui;

        }

        /// <summary>
        /// 处理数据文本改变事件的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void targetBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 temp = int.Parse(tbTarget.Text);
                tbTarget.Text = temp.ToString();  //去除数字首位是0， 如035，输入框中应该显示35
                if (temp >= 0 && target != temp)
                {
                    target = temp;
                    //标记数据已经改动可能需要保存
                }
                else
                {
                    throw new Exception("参数异常");
                }
            }
            catch (Exception)
            {
                //RmlDebug.Wrong(ex.Message);
                tbTarget.Text = target.ToString();
                return;
            }
            global::SPI.Global.Configuration.DataChanged = true;
            holder.OnDataChange(sender, e);
            if (DataChanged != null) DataChanged(this, e);
        }
        /// <summary>
        /// 设置测试结果文本
        /// </summary>
        /// <param name="ok">测试结果是否OK</param>
        internal void SetResult(bool ok)
        {
            if (HolderShowing(holder))
            {
                lbTestResult.Text = ok ? "OK" : "NG";
            }
        }
        /// <summary>
        /// 设置测试结果文本，并返回生成的测试结果对象
        /// </summary>
        /// <param name="testValue">测试结果数值</param>
        /// <param name="rst">测试结果类型</param>
        /// <returns>生成的测试结果对象</returns>
        //public TestResult SetResult(int testValue, ResultType rst)
        //{
        //    hasResult = true;
        //    actual = testValue;
        //    testResult = rst;

        //    if (Globles.HolderShowing(holder))
        //    {
        //        if (tbTestValue != null)
        //            tbTestValue.Text = testValue.ToString();
        //        if (lbTestResult != null)
        //            lbTestResult.Text = (testResult == ResultType.OK) ? "OK" : "NG";
        //    }
        //    return new TestResult(Globles.curFovId, holder, pre, Target.ToString(), testValue.ToString(), rst);
        //}
        /// <summary>
        /// 设置测试结果文本，生成测试结果并加入父结果对象，返回生成的测试结果对象
        /// </summary>
        /// <param name="testValue"></param>
        /// <param name="rst"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        //public TestResult SetResult(int testValue, ResultType rst, TestResult parent)
        //{
        //    TestResult tr = SetResult(testValue, rst);
        //    if (parent.item == tr.item)
        //    {
        //        parent.target = tr.target;
        //        parent.value = tr.value;
        //        parent.result = tr.result;
        //    }
        //    else
        //    {
        //        parent.ErrorTransferAdd(tr);
        //    }
        //    return tr;
        //}
        /// <summary>
        /// 保存数据到文件
        /// </summary>
        /// <param name="sw">指示要写入的文件的自定义流类对象</param>
        public override void SaveTo(MyWriter sw)
        {
            //sw.Save(target);
        }
        /// <summary>
        /// 从文件读入数据
        /// </summary>
        /// <param name="sr">指示要从中读入数据的自定义流类对象</param>
        public override void LoadFrom(MyReader sr)
        {
           // sr.Load(ref target);
        }
        /// <summary>
        /// 从其他模板拷贝数据
        /// </summary>
        /// <param name="ub">要从中拷贝数据的模板</param>
        public override void CopyFrom(UIBase ub)
        {
            UIInt fr = (UIInt)ub;
            target = fr.target;
        }

        public void ClearResult()
        {
            if (tbTestValue != null)
            {
                tbTestValue.Text = "";
            }
        }
    }
}
