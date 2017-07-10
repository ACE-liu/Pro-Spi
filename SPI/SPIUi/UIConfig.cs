using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPICheckWin;
using System.Windows.Forms;
using static SPI.Global.Configuration;
using System.Reflection;
using System.Resources;
using System.Drawing;

namespace SPI.SPIUi
{
    /// <summary>
    /// 用于设置整数值的控件
    /// </summary>
    internal class UIConfig : UIBase
    {
        //Data part
        /// <summary>
        /// 数据改变时处理函数指针
        /// </summary>
        public event EventHandler DataChangedEvent;
        /// <summary>
        /// 设置的值，公共属性
        /// </summary>
        public double Target { get { return target; } }
        /// <summary>
        /// 设置的值，私有变量
        /// </summary>
        double target;

        //UI part
        /// <summary>
        /// 容器，用于包装需要的其他控件为统一控件。
        /// </summary>
        Panel ui;
        /// <summary>
        /// 显示数值的文本框
        /// </summary>
        protected TextBox tbValue;
        /// <summary>
        /// 显示测试结果类型的文本标签
        /// </summary>
        Label lbpost;
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

        private double upperValue=-1;
        /// <summary>
        /// 常用构造函数
        /// </summary>
        /// <param name="win">包含此UI的检查框</param>
        /// <param name="lvl">显示层级。0为最靠左边，其他层次以等量缩进右缩</param>
        /// <param name="pr">显示在参数前面的文本</param>
        /// <param name="po">显示在参数后面的文本</param>
        /// <param name="ck">是否需要显示测试结果</param>
        public UIConfig(CheckWinBase win, int lvl, string pr, string po,double upValue=-1)
            : base(win, lvl)
        {
            target = 0;
            pre = pr;
            post = po;
            upperValue = upValue;
        }

        /// <summary>
        /// 清理图片
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
            if (null != this.DataChangedEvent)
            {
                this.DataChangedEvent = null;
            }
            if (null != this.ui)
            {
                this.ui.Dispose();
                this.ui = null;
            }
            if (null != this.tbValue)
            {
                this.tbValue.TextChanged -= new EventHandler(targetBox_TextChanged);
                this.tbValue.Dispose();
                this.tbValue = null;
            }
            if (null != this.lbPre)
            {
                this.lbPre.Dispose();
                this.lbPre = null;
            }
            if (null!=lbpost)
            {
                lbpost.Dispose();
                lbpost = null;
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
                ui.Font = CurFocusPanel?.Font;
                //ui.Width = Globles.maxControlWidth - level * Globles.indent - Globles.controlsGap;
                ui.AutoSize = true;
                lbPre = new Label(); lbPre.SuspendLayout();
                lbPre.Text = pre;
                lbPre.Left = ControlsGap;
                //lbPre.Width = ui.Width - 250;//185 pixels for left location fixed controls
                lbPre.AutoSize = true;

                tbValue = new TextBox(); tbValue.SuspendLayout();
                tbValue.Text = target.ToString();
                tbValue.TextChanged += new EventHandler(targetBox_TextChanged);
                tbValue.Left = lbPre.Right + ControlsGap;
                tbValue.Width = 45;
                Label lbPost = new Label(); lbPost.SuspendLayout();
                lbPost.Text = post;
                lbPost.Left = tbValue.Right + ControlsGap;

                lbPre.ResumeLayout();
                tbValue.ResumeLayout();
                lbPost.ResumeLayout();
                ui.Controls.Add(lbPre);
                ui.Controls.Add(tbValue);
                ui.Controls.Add(lbPost);
                ui.Height = SetRelateTop(ui);
                ui.BorderStyle = BorderStyle.FixedSingle;
                ui.ResumeLayout();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                //Int32 temp = int.Parse(tbTarget.Text);
                double temp = double.Parse(tbValue.Text);
                tbValue.Text = temp.ToString();  //去除数字首位是0， 如035，输入框中应该显示35
                if (temp >= 0 && target != temp)
                {
                    target = temp;
                    //标记数据已经改动可能需要保存
                }
                else
                {
                    throw new Exception("?????");
                }
                if (upperValue != -1 && target > upperValue)  // 超过上限
                {
                    tbValue.Text = tbValue.ToString();
                }
            }
            catch (Exception)
            {
                //RmlDebug.Wrong(ex.Message);
                tbValue.Text = target.ToString();
                return;
            }
            DataChanged = true;
            holder.OnDataChange(sender, e);
            if (DataChangedEvent != null) DataChangedEvent(this, e);
        }
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
            //sr.Load(ref target);
        }
        /// <summary>
        /// 从其他模板拷贝数据
        /// </summary>
        /// <param name="ub">要从中拷贝数据的模板</param>
        public override void CopyFrom(UIBase ub)
        {
            UIConfig fr = ub as UIConfig;
            target = fr.target;
        }
    }
}