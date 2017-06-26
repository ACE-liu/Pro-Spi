using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.Global;

namespace SPI.SPIModel
{
    /// <summary>
    /// 检测窗形状base
    /// </summary>
    abstract class  ShapeBase
    {
        /// <summary>s
        /// 获取中心
        /// </summary>
        /// <returns></returns>
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public abstract Point Location { get; set; }
        internal abstract Rectangle MRectangle { get; }
        internal abstract Point GetCenter();
        internal abstract ShapeType GetShapeType();
        internal abstract bool OnFocus();
        internal abstract void DrawSelf(Graphics g,Pen pen);
        internal abstract Direction MouseOverWhere(Point e);
        internal abstract Point MarkShift { get; set; }
        internal abstract void ChangeSelf(Direction dt);
        internal abstract void Move(Point p);
        internal abstract Point ChangeToShowPoint();

        internal virtual void ShiftCenterTo(Point location)
        {
            X = location.X - Width / 2;
            Y = location.Y - Height / 2;
        }
    }
}
