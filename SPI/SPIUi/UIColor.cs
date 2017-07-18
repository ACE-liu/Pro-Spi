using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPICheckWin;
using SPI.Global;
using static SPI.Global.Configuration;
using System.Windows.Forms;

namespace SPI.SPIUi
{
    /// <summary>
    /// 用于编辑颜色的UI控件
    /// </summary>
    internal class UIColor : UIBase
    {
        /// <summary>
        /// 一组颜色参数。各颜色范围组合后形成一个完整的颜色参数。
        /// </summary>
        public List<ColorRange> Colors { get { return _colors; } }
        List<ColorRange> _colors;
        /// <summary>
        /// 是否扩展显示。由于颜色参数编辑框较大，不编辑时显示为收缩状态占较少位置。
        /// </summary>
        public bool expand;
        /// <summary>
        /// 界面显示控件。由于多数UI不需要显示，仅在需要显示时创建对应的UI控件。
        /// </summary>
        MultiColors ui;

        ///是否显示该控件
        public bool isVisible = true;

        private List<string> _colorTips = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="win">包含此UI的检查框</param>
        /// <param name="lvl">显示层级。0为最靠左边，其他层次以等量缩进右缩</param>
        public UIColor(CheckWinBase win, int lvl, bool isVisible, List<string> colorTips = null)
            : base(win, lvl)
        {
            _colors = new List<ColorRange>();
            _colors.Add(new ColorRange());
            _colorTips = colorTips;
            expand = false;
            ui = null;
            this.isVisible = isVisible;

        }
        /// <summary>
        /// 释放图片资源
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
            if (null != this.ui)
            {
                this.ui.DisposeMultiColorManual();
                this.ui.SizeChanged -= new EventHandler(ui_SizeChanged);
                this.ui.DataChanged -= new EventHandler(ui_DataChanged);
                this.ui.Dispose();
                this.ui = null;
            }
        }

        /// <summary>
        /// 创建显示控件。由于大部分UI设置的参数不需要显示，设计为只为需要显示的UI类对象创建界面控件。
        /// </summary>
        /// <returns></returns>
        public override Control CreateUI()
        {
            ui = new MultiColors(_colorTips);
            ui.SuspendLayout();
            ui.SetSource(_colors);//.SetData(colors[0]);
            ui.SetExpand(expand);
            ui.SizeChanged += new EventHandler(ui_SizeChanged);
            ui.DataChanged += new EventHandler(ui_DataChanged);
            ui.ResumeLayout();
            return ui;
        }
        /// <summary>
        /// 处理数据改变事件的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ui_DataChanged(object sender, EventArgs e)
        {
            DataChanged = true;//标记数据已经改动可能需要保存
            holder.OnDataChange(sender, e);
        }
        /// <summary>
        /// 处理显示大小变化事件的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ui_SizeChanged(object sender, EventArgs e)
        {
            expand = ui.GetExpand();
            holder.RearrangeAllControl(ui, level+1);
            holder.OnSizeChange();
        }
        /// <summary>
        /// 保存数据到文件
        /// </summary>
        /// <param name="sw">指示要写入的文件的自定义流类对象</param>
        public override void Save(MyWriter mw)
        {
            mw.Save(_colors.Count);
            foreach (ColorRange cr in _colors)
            {
                cr.SaveTo(mw);
            }
        }
        /// <summary>
        /// 从文件读入数据
        /// </summary>
        /// <param name="sr">指示要从中读入数据的自定义流类对象</param>
        public override void LoadFrom(MyReader sr)
        {
            int crc = sr.LoadInt();        
            for (int i = 0; i < crc; i++)
            {
                ColorRange cr = new ColorRange();
                cr.LoadFrom(sr);
                this._colors.Add(cr);
            }
        }
        /// <summary>
        /// 从其他模板拷贝数据
        /// </summary>
        /// <param name="ub">要从中拷贝数据的模板</param>
        public override void CopyFrom(UIBase ub)
        {
            UIColor fr = (UIColor)ub;
            int crc = fr._colors.Count;
            this._colors.Clear();

            for (int i = 0; i < crc; i++)
            {
                ColorRange cr = new ColorRange();
                cr.CopyFrom(fr._colors[i]);
                this._colors.Add(cr);
            }
            if (HolderShowing(holder))
            {
#if DEBUG
                if (ui == null)
                {
                   // RmlDebug.Prompt("ui is null in BasicClass CopyFrom function！");
                }
#endif
                if (ui != null)
                    ui.SetSource(_colors);//.SetData(colors[0]);
            }
        }
    }
}
