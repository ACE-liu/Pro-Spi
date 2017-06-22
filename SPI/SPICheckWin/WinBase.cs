using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;
using System.ComponentModel;
using SPI.Global;

using static SPI.Global.Configuration;

namespace SPI.SPICheckWin
{
    abstract class WinBase
    {
        #region ***************显示的属性*****************
        /// <summary>
        /// 窗口类型
        /// </summary>
        [CategoryAttribute("一般信息"), DescriptionAttribute("类信息"), DisplayName("窗口类型")]
        public WinType WType { get { return wType; } }
        /// <summary>
        /// 显示名称,公共属性
        /// </summary>
        [CategoryAttribute("一般信息"), DescriptionAttribute("名称字符串"), DisplayName("显示名称")]
        public virtual string DisplayName { get { return this.ToString(); } }
        /// <summary>
        /// 方框ID,公共属性
        /// </summary>
        [BrowsableAttribute(true), ReadOnlyAttribute(true), CategoryAttribute("一般信息"), DisplayName("ID")]
        public int ID { get { return id; } set { id = value; } }
        #endregion
        public string WinName;
        public List<WinBase> SubWinList = null;
        protected int id = -1;
        protected WinType wType = WinType.None;
        public ShapeBase ShowShape { get; protected set; } = null;
        public abstract void Show(Graphics g);

        /// <summary>
        /// 计算鼠标相对本检查框处于哪个位置。
        /// 鼠标位置已转换为板坐标。
        /// </summary>
        /// <param name="e">鼠标位置对应的板坐标</param>
        /// <param name="deep">是否搜索子项</param>
        /// <returns></returns>
        public Direction MouseOverWhere(Point e)
        {
            return ShowShape.MouseOverWhere(e); //交给图形解释
        }
        /// <summary>
        /// d1位置是否比d2位置关联更强.关联性按边界》中间》外边顺序排列.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool MoreFocus(Direction d1, Direction d2)
        {
            if (d2 > Direction.center)
                return false;
            return d1 > d2;
        }
        /// <summary>
        /// 根据鼠标位置获取选中的Win
        /// </summary>
        /// <param name="de"></param>
        /// <returns></returns>
        public WinBase FindMouseOnRect(Point de)
        {
            if (SubWinList?.Count == 0)
                return this;
            WinBase centerRect = null;
            foreach (WinBase rct in SubWinList ?? new List<WinBase>())
            {
                Direction dtemp = MouseOverWhere(de);
                switch (dtemp)
                {
                    case Direction.outside:
                        break;
                    case Direction.center:
                        //如果没有边，反馈先找到的中心框。
                        if (centerRect == null)
                            centerRect = rct;
                        break;
                    default:
                        return rct;
                }
            }
            if (centerRect == null)
                return null;
            WinBase win = centerRect.FindMouseOnRect(de);
            if (win!=null)
            {
                return win;
            }
            if (CurFocus != null)
            {
                Direction fdir = CurFocus.MouseOverWhere(de);
                Direction mdir = centerRect.MouseOverWhere(de);
                if (MoreFocus(mdir, fdir))
                    return centerRect;
                else
                {
                    if (CurFocus == TheBoard && mdir > Direction.outside)
                        return centerRect;
                    else
                        return CurFocus;
                }
            }
            return centerRect;
        }
    }
}
