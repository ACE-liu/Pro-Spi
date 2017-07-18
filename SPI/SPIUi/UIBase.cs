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
    /// <summary>
    /// 用于数据设置和模拟测试结果显示的自定义UI类的基类
    /// </summary>
     class UIBase
    {
        /// <summary>
        /// 包含此UI的检查框
        /// </summary>
        protected CheckWinBase holder;
        /// <summary>
        /// 显示层级。0为最靠左边，其他层次以等量缩进右缩
        /// </summary>
        internal int level;
        /// <summary>
        /// Only show for expert user.
        /// </summary>
        internal bool ExpertOnly;
        /// <summary>
        /// 最后使用此UI的数据版本号。用于处理以前使用但后来取消的参数
        /// </summary>
        protected int LastVersion = int.MaxValue;//If need to Remove an UI, Set the LastVersion to the last DataVersion using the UI, then we can read the old version files and the new version files. 
        /// <summary>
        /// 首次使用此UI的数据版本号。用于处理新增的参数。
        /// </summary>
        protected int FirstVersion = int.MinValue;//If need to add an new UI, Set the FirstVersion to the first DataVersion using the UI, then we can read the old version files and the new version files.
        ///// <summary>
        ///// 隐藏并不测的项
        ///// </summary>
        //public bool hideAndNoCheck = false;
        ///// <summary>
        ///// 必测的项
        ///// </summary>
        //public bool mustCheck = false;
        /// <summary>
        /// 常用构造函数
        /// </summary>
        /// <param name="holderWin">包含此UI的检查框</param>
        /// <param name="showLevel">显示层级。0为最靠左边，其他层次以等量缩进右缩</param>
        public UIBase(CheckWinBase holderWin, int showLevel)
        {
            level = showLevel;
            holder = holderWin;
            holder.AddUi(this);
            ExpertOnly = false;
        }
        /// <summary>
        /// 创建显示控件。由于大部分UI设置的参数不需要显示，设计为只为需要显示的UI类对象创建界面控件。
        /// </summary>
        /// <returns></returns>
        public virtual Control CreateUI()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 手动释放UI
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140625</date>
        public virtual void DisposeUIManual()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 释放UI对象持有的BMP资源
        /// </summary>
        public virtual void DisposeBmp()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 从其他模板拷贝数据
        /// </summary>
        /// <param name="ub">要从中拷贝数据的模板</param>
        public virtual void CopyFrom(UIBase ub)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 添加跟随控件容器，属于此控件下层的下层控件将添加到此容器
        /// </summary>
        /// <returns></returns>
        public virtual Panel AddFollows()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 重排显示布局
        /// </summary>
        public virtual void Relayout()
        {
        }

        public void ResizeFollowSize(Control ctr)
        {
            ctr.Width = CurFocusPanel.Width-ControlsGap*2;
        }
        /// <summary>
        /// 添加控件到下层控件容器
        /// </summary>
        /// <param name="ctr">待加入的控件</param>
        public virtual void Add(Control ctr)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 检查文件中是否包含本UI的数据
        /// </summary>
        /// <param name="sr"></param>
        /// <returns> true if the sr should have the data. </returns>
        internal bool NeedReadFrom(MyReader sr)
        {
            if (sr.DataVersion >= FirstVersion && sr.DataVersion <= LastVersion)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 检查是否需要显示本UI内容到界面
        /// </summary>
        /// <returns></returns>
        internal bool NeedShow()
        {
            return true;
            //return DefaultDataVersion <= LastVersion&&DefaultDataVersion>=FirstVersion;
        }
        public virtual void Save(MyWriter mw)
        {
            throw new NotImplementedException();
        }
        public virtual void LoadFrom(MyReader mr)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  设置子控件Top，返回控件的高度
        /// </summary>
        /// <param name="clist"></param>
        /// <returns></returns>
        public int SetRelateTop(Control ctrl)
        {
            int max = 0;
            foreach (Control one in ctrl.Controls)
            {
                if (one.Height > max)
                    max = one.Height;
            }
            if (max==0)
            {
                return max;
            }
            int uiHeight = max + 2 *ControlsGap;
            foreach (Control one in ctrl.Controls)
            {
                one.Top = (uiHeight - one.Height) / 2;
            }
            return uiHeight + 2;
        }

    }
}
