using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI.Global
{
    /// <summary>
    /// SPI报错类型
    /// </summary>
    public enum ResultType
    {
        未确认 = -1,
        NG,
        OK,
        拉尖,
        少锡,
        体积,
        掠锡,
        多锡,
        偏移,
        擦碰,
        平均高度偏出,
        连锡
    }

    //public enum CheckWinType
    //{
    //    MarkPoint,
    //    Land
    //}
    public enum ShapeType
    {
        None = -1,
        Rectangle,
        Circle,
        other
    }
    class SpiBase
    {
    }
}
