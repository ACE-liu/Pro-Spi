using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI.SPIModel
{
    /// <summary>
    /// 检测窗形状base
    /// </summary>
    interface  ShapeBase
    {
        /// <summary>s
        /// 获取中心
        /// </summary>
        /// <returns></returns>
        Point GetCenter();
    }
}
