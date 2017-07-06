using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.Global;
using static SPI.Global.Configuration;

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
        public abstract int Right { get; }
        public abstract int Bottom { get; }
        internal abstract Rectangle Rectangle { get; }
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
        internal abstract void ShrinkToRange(ShapeBase other);
        internal abstract void ExpandToInclude(ShapeBase other,Direction edge);
        internal double Angle=0;
        /// <summary>
        /// 辅助将原件每次添加在MarkPicture中间
        /// </summary>
        /// <param name="location"></param>
        /// <param name="displayRate"></param>
        internal virtual void ShiftCenterTo(Point location)
        {
            X = location.X - (int)(Width / 2);
            Y = location.Y - (int)(Height / 2);
        }
        internal virtual void MoveToRange(ShapeBase parent)
        {
            if (X < parent.X) X = parent.X;
            if (Y < parent.Y) Y = parent.Y;
            if (Right > parent.Right) X = parent.Right - Width;
            if (Bottom > parent.Bottom) Y = parent.Bottom - Height;
        }
        internal virtual void ResizeAroundCenter(int width,int height)
        {
            this.Width += width;
            this.Height += height;
            this.X -= width / 2;
            this.Y -= height / 2;
            if (width<min)
            {
                width = min;
            }
            if (height<min)
            {
                height = min;
            }
       
        }
    }
}
