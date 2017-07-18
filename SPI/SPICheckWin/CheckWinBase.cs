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
        /// <summary>
        /// 颜色设置控件尺寸调整后调用此函数
        /// </summary>
        public void OnSizeChange()
        {
        }
        public override void GetFocus()
        {
            if (ShowProperty || this.uis?.Count == 0)
            {
                base.GetFocus();
            }
            else
            {
                SetCheckWindowFocus();
            }
        }
        public virtual void Save(MyWriter mw)
        {
            mw.Save(ShowShape is RectangleMode);
            mw.Save(ShowShape.X);
            mw.Save(ShowShape.Y);
            mw.Save(ShowShape.Width);
            mw.Save(ShowShape.Height);
            mw.Save(ShowShape.Angle);
            mw.SaveLineEnd();
            foreach (var item in uis)
            {
                item.Save(mw);
            }
            mw.SaveLineEnd();
        }
        public virtual void LoadFrom(MyReader mr)
        {
            bool isRect = mr.LoadBool();
            if (isRect&&ShowShape is CircleMode)
            {
                ShowShape = new RectangleMode();
            }
            else if (!isRect&&ShowShape is RectangleMode)
            {
                ShowShape = new CircleMode();
            }
            ShowShape.X = mr.LoadInt();
            ShowShape.Y = mr.LoadInt();
            ShowShape.Width = mr.LoadInt();
            ShowShape.Height = mr.LoadInt();
            ShowShape.Angle = mr.LoadDouble();
            mr.LoadLineEnd();
            foreach (var item in uis)
            {
                item.LoadFrom(mr);
            }
            mr.LoadLineEnd();
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
            //Relayout all the related controls
            c = ctr;
            for (int i = 0; i <= level-1; i++)
            {
                c = c.Parent;
                RearrangeAControl(c);
                //c = c.Parent;
            }
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
            //Globles.ShowingUI = true;
            if (CurFocusPanel == null)
            {
                return;
            }
            //Point p = CurFocusPanel.AutoScrollPosition;
            CurFocusPanel.SuspendLayout();
            CurFocusPanel.AutoScrollPosition = new Point(0, 0);
            //Point p = CurFocusPanel.AutoScrollPosition;
            while (CurFocusPanel.Controls.Count > 0)
            {
                if (null != CurFocusPanel.Controls[0])
                {
                    CollectManualCreateComponentMemory(CurFocusPanel.Controls[0]);
                }
            }
            CurFocusPanel.Controls.Clear();
            Panel pn = CreateUI();
            if (pn != null)
            {
                CurFocusPanel.Controls.Add(pn);
            }
            //else
            //    //RmlDebug.Wrong("Null UI?");
            //CurFocusPanel.AutoScrollPosition = new Point(-p.X, -p.Y);
            CurFocusPanel.ResumeLayout(true);
        }
        public virtual Panel CreateUI()
        {
            Panel container = new Panel();  //Container of all the UI components of this class.It will be returned.
            container.SuspendLayout();
            UIBase[] lastContainer = new UIBase[5];
            int lastlevel = 0;
            if (uis.Count > 0) lastlevel = uis[0].level;
            lastContainer[lastlevel] = null;
            //***** 根据uis内UI的层级确定各UI的归宿。
            //若下一UI的层级大于本UI，则该控件为本UI的子级
            //否则，则属于前面最接近的低一层级的UI的子级
            for (int i = 0; i < uis.Count; i++)
            {       
                //if ((uis[i].expertOnly && !Globles.CurUser.IsExpert()) || !uis[i].NeedShow())
                //    continue;

                int thislevel = uis[i].level;

                //处理层次变化
                if (i > 0)
                {
                    if (thislevel > lastlevel)
                    {
                        if (lastlevel == 0)
                            container.Controls.Add(lastContainer[lastlevel].AddFollows());
                        else
                            lastContainer[lastlevel - 1].Add(lastContainer[lastlevel].AddFollows());
                    }
                    else if (thislevel < lastlevel)
                    {
                        for (int j = lastlevel; j > thislevel; j--)
                        {
                            lastContainer[j - 1].Relayout();
                        }
                    }
                }
                //在需要显示时创建该UIBase对象对应的UI，加入到子层或根控件。
                if (uis[i].NeedShow())
                {
                    Control ui = uis[i].CreateUI();
                    if (thislevel == 0)
                        container.Controls.Add(ui);
                    else
                        lastContainer[thislevel - 1].Add(ui);

                    lastlevel = thislevel;//不需要搬出if。若不需要显示，在控件显示界面上可视为无此ui。???可能需要测试。
                    lastContainer[thislevel] = uis[i];
                }
            }
            //处理最后一项非0层级情况
            if (lastlevel > 0)
            {
                for (int j = lastlevel; j > 0; j--)
                    lastContainer[j - 1].Relayout();
            }
            RearrangeAControl((Control)container); container.ResumeLayout();
            lastContainer = null;
            return container;
        }
        public virtual void RearrangeAControl(Control container)
        {
            int y = 0;
            int maxWidth = 400;

            foreach (Control ctr in container.Controls)
            {
                //if (ctr.Controls.Count>0)
                //{
                //    RearrangeAControl(ctr);
                //}
                if (ctr.Visible)
                {
                    ctr.Top = y;
                    //y += ctr.Height + 2;
                    y = ctr.Bottom + ControlsGap;
                    container.Height = y;
                    if (ctr.Right > maxWidth) maxWidth = ctr.Right;
                }
            }
            container.Width = maxWidth;
        }
        #endregion

    }
}
