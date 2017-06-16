using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;

namespace SPI.SPICheckWin
{
    /// <summary>
    /// SPI 检测窗基类
    /// </summary>
    abstract class CheckWinBase
    {
        /// <summary>
        /// SPI测试的抽象方法
        /// </summary>
        internal abstract void Test();
        void getCenter()
        {
            CircleMode mode = new CircleMode();
            
        }
    }
}
