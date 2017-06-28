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
       public const int min = 30;
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
        /// <summary>
        /// 保存配置FOV大小
        /// </summary>
        internal static Size FovSize;
        /// <summary>
        /// 为了方便操作坐标及其他数据，在加载MP时保存全局的mp指针；
        /// </summary>
        internal static MarkedPicture theMarkPicture = null;
        /// <summary>
        /// 显示在markedPicture中的图像在主板中的位置。
        /// </summary>
        public static Rectangle showPart;
        #endregion

        #region***********Method***********

        /// <summary>
        /// 根据当前状态获取绘制检测窗的笔
        /// </summary>
        /// <param name="win"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        internal static Pen GetPenByWin(WinBase win, float width=1)
        {
            if(win==null)
            {
                return null;
            }
            else if (win==CurFocus)
            {
                return new Pen(Color.Red, 2);
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
            return GetPenByWin(win, width);
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
            if (window!=null&&CurFocus!=window)
            {
                CurFocus?.OnLoseFocus();
                CurFocus = window;
                window?.OnFocus();
            }
        }
        #endregion
    }
}
