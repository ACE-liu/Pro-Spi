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
    internal static class ConfigMember
    {
        /// <summary>
        /// 当前焦点
        /// </summary>
        internal static ShapeBase CurFocus = null;
        internal static bool AutoChecking = false;
        internal static Board TheBoard = null;
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
        internal static void SetFocus(WinBase window)
        {
            //
        }
    }
}
