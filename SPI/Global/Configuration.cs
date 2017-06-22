using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using SPI.SPICheckWin;
using System.Drawing;

namespace SPI.Global
{
    /*全局类  用于存放常用的静态函数静态字段*/
    internal static class Configuration
    {
        #region***********Field***********

        /// <summary>
        /// 方框的最小尺寸。
        /// </summary>
       public const int min = 2;
        /// <summary>
        /// 选择方框边时允许的鼠标点队应的单板位置偏差。
        /// </summary>
       public const int mdelt = 3;
        /// <summary>
        /// 当前焦点
        /// </summary>
        internal static WinBase CurFocus = null;
        internal static bool AutoChecking = false;

        /// <summary>
        /// 描述添加元件时元件默认位置
        /// </summary>
        internal static Point DefaultLocation = Point.Empty;
        /*当前操作的板*/
        internal static Board TheBoard = null;
        internal static Size FovSize;
        #endregion

        #region***********Method***********
        /// <summary>
        /// 根据当前状态获取绘制检测窗的笔
        /// </summary>
        /// <param name="win"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        internal static Pen GetPenByParams(WinBase win, float width)
        {
            if(win==null)
            {
                return default(Pen);
            }
            else if (win.ShowShape.OnFocus())
            {
                return new Pen(Color.Yellow, width);
            }
            else if((win as CheckWinBase)?.hasTested??false)
            {
                return new Pen(Color.Red, width);
            }
            else
            {
                return new Pen(Color.Blue, width);
            }
        }
        internal static Pen GetPenByShape(ShapeBase shape, float width)
        {
            WinBase win = TheBoard?.SubWinList?.Find(p => p.ShowShape == shape);
            return GetPenByParams(win, width);
        }
        internal static Point AddPoint(Point source,Point addPart)
        {
            return new Point(source.X + addPart.X, source.Y + addPart.Y);
        }
        /// <summary>
        /// 计算2点距离返回整数
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        internal static int CalculateDirec(Point A,Point B)
        {
            return Convert.ToInt32((Math.Sqrt((A.X - B.X) ^ 2 + (A.Y - B.Y) ^ 2)));
        }
        internal static void SetFocus(WinBase window)
        {
            //
        }
        #endregion
    }
}
