using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;
using SPI.SPIUi;

using static SPI.Global.Configuration;
using System.Windows.Forms;

namespace SPI.SPICheckWin
{
    /// <summary>
    /// SPI 检测窗基类
    /// </summary>
     abstract class CheckWinBase:WinBase
    {
        #region*********Field**********
        /// <summary>
        /// SPI测试的抽象方法
        /// </summary>
        internal abstract void Test();
        internal bool hasResult = false;
        public Point MarkShift=>ShowShape.MarkShift;

        private List<UIBase> uis = null;

        #endregion

        #region************Method****************

        /*默认生成矩形检测窗*/
        public CheckWinBase() : this(new RectangleMode())
        {

        }
        public CheckWinBase(int width,int height):this(new RectangleMode(width,height))
        { }
        /*
        生成不同形状的检测窗
        算法也要根据检测窗的形状适配
            */
        public CheckWinBase(ShapeBase showShape)
        {
            this.ShowShape = showShape;
        }
        /// <summary>
        /// 添加UIbase
        /// </summary>
        /// <param name="ui"></param>
        public void AddUi(UIBase ui)
        {
            if (uis==null)
            {
                uis = new List<UIBase>();
            }
            uis.Add(ui);
        }
        /// <summary>
        /// 移除特定的UIbase
        /// </summary>
        /// <param name="ui"></param>
        public void RemoveUi(UIBase ui)
        {
            uis?.Remove(ui);
        }

        public override void GetFocus()
        {
            if (ShowProperty || this.uis.Count == 0)
            {
                //Globles.theForm.SetPropertyFocus(this);

            }
            else
            {
                ///是否可创建UI控件,防止创建控件过于频繁导致的内存不足
                SetCheckWindowFocus();

            }
        }
        /// <summary>
        /// 检查窗的任何数据改变事件处理。
        /// </summary>
        public override void OnDataChange(object sender, EventArgs e)
        {
            SPI.Global.Configuration.DataChanged = true;//标记数据已经改动可能需要保存
            hasResult = false;
            base.OnDataChange(sender, e);    //Change focus show and picture
        }
        /// <summary>
        /// 重排制定控件内子控件位置，并上溯调整所有包含此控件的各级父控件内的控件位置。
        /// </summary>
        /// <param name="ctr">需要重排的最低级的控件</param>
        /// <param name="level">上溯深度</param>
        public void RearrangeAllControl(Control ctr, int level)
        {
            Control c;
            //SuspendLayout all related controls
            CurFocusPanel.SuspendLayout();
            c = ctr;
            for (int i = 0; i <= level; i++)
            {
                c.SuspendLayout();
                c = c.Parent;
            }
            if (c != CurFocusPanel)
                throw new Exception("参数异常....");
            c = ctr;
            for (int i = 0; i <= level; i++)
            {
                c.ResumeLayout();
                c = c.Parent;
            }
            CurFocusPanel.ResumeLayout(false);

            //强制重绘。不调用此函数可能导致滚动条操作显示故障。
            CurFocusPanel.PerformLayout();
        }
        /// <summary>
        /// 在焦点显示容器控件内显示分层数据设置UI界面，即uis对应的界面。
        /// </summary>
        void SetCheckWindowFocus()
        {
            //    //Globles.ShowingUI = true;
            //    if (CurFocusPanel==null)
            //    {
            //        return;
            //    }
            //    CurFocusPanel.SuspendLayout();
            //    while (Globles.panelCurFocus.Controls.Count > 0)
            //    {
            //        if (null != Globles.panelCurFocus.Controls[0])
            //        {
            //            Globles.panelCurFocus.CollectManualCreateComponentMemory(Globles.panelCurFocus.Controls[0]);
            //        }
            //    }
            //    if (null != lastUis)
            //    {
            //        while (lastUis.Count > 0)
            //        {
            //            lastUis[0].DisposeUIManual();
            //            lastUis.RemoveAt(0);
            //        }
            //    }
            //    lastUis.Clear();
            //    /*-end xiongyanan 20140625 */

            //    Globles.propertyGrid1 = null;
            //    //清空 Globles.panelCurFocus 容器并将新的UI界面放入其中。
            //    Globles.panelCurFocus.Controls.Clear();
            //    Panel pn = CreateUI();
            //    if (pn != null)
            //        Globles.panelCurFocus.Controls.Add(pn);
            //    else
            //        RmlDebug.Wrong("Null UI?");

            //    //***** ???用于修正滚动条错误，可能已无用。
            //    Globles.panelCurFocus.AutoScrollPosition = new Point(-p.X, -p.Y);

            //    Globles.panelCurFocus.ResumeLayout();

            //    //sw.Stop();
            //    //RmlDebug.Prompt(sw.ElapsedMilliseconds.ToString());
            //    //Globles.panelCurFocus.PerformLayout();

        }
    #endregion

}
}
