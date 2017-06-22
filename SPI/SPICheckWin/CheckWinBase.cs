using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;

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
        internal bool hasTested = false;
        public Point MarkShift=>ShowShape.MarkShift;

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
        #endregion
    }
}
