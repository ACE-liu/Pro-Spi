using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI.Global
{
    /// <summary>
    /// 点相对方框的大致方位.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// .框外
        /// </summary>
        outside,
        /// <summary>
        /// .框内不靠近边处
        /// </summary>
        center,
        /// <summary>
        /// .上边附近
        /// </summary>
        top,
        /// <summary>
        /// .右上角附近
        /// </summary>
        topRight,
        /// <summary>
        /// .右边附近
        /// </summary>
        right,
        /// <summary>
        /// .右下角附近
        /// </summary>
        bottomRight,
        /// <summary>
        /// .下边附近
        /// </summary>
        bottom,
        /// <summary>
        /// .左下角附近
        /// </summary>
        bottomLeft,
        /// <summary>
        /// .左边附近
        /// </summary>
        left,
        /// <summary>
        /// .左上角附近
        /// </summary>
        topLeft
    };
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

    /// <summary>
    /// 元件类别方便界面显示
    /// </summary>
    public enum WinType
    {
        None=-1,
        Board,
        SubBoard,
        Chip,
        BGA,
        MarkPoint,
        Land
    }
    public enum ShapeType
    {
        None = -1,
        Rectangle,
        Circle,
        otherType
    }
    class SpiBase
    {
    }
}
