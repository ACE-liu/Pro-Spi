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
    internal class CircleMode : ShapeBase
    {
        private Point center = Point.Empty;
        private ShapeType m_ShapeType = ShapeType.None;

        public override int X { get; set; }
        public override int Y { get; set; }
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override int Bottom => Y + Height;
        public override int Right => X + Width;
        public override Point Location
        {
            get
            {
                return new Point(X, Y);
            }

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public static Point DefaultCenter;
        public const int DefaultRadius=20;
        internal override Point MarkShift { get; set; }
        public Point ShiftCenter { get { return new Point(center.X + MarkShift.X, center.Y + MarkShift.Y); } }
        internal override Point GetCenter()
        {
            return center;
        }
        public CircleMode()
        {
            this.Width = 30;
            this.Height = 30;
            this.m_ShapeType = ShapeType.Circle;
        }
        public CircleMode(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.m_ShapeType = ShapeType.Circle;
        }
        internal override ShapeType GetShapeType()
        {
            return m_ShapeType;
        }
        internal override void Move(Point p)
        {
            this.X += p.X;
            this.Y += p.Y;
        }
        internal override void ExpandToInclude(ShapeBase other,Direction edge)
        {
            if (edge == Direction.center)
            {
                if (other.X < this.X) this.X = other.X;
                if (other.Y < this.Y) this.Y = other.Y;
                if (other.Right > this.Right) this.X = other.Right - this.Width;
                if (other.Bottom > this.Bottom) this.Y = other.Bottom - this.Height;
            }
            else
            {
                if (other.X < this.X) { this.Width += (this.X - other.X); this.X = other.X; }
                if (other.Y < this.Y) { this.Height += (this.Y - other.Y); this.Y = other.Y; }
                if (other.Right > this.Right) this.Width = other.Right - this.X;
                if (other.Bottom > this.Bottom) this.Height = other.Bottom - this.Y;
            }
        }
        /// <summary>
        /// 将方框压缩到指定范围.
        /// </summary>
        /// <param name="rt">压缩到此范围</param>
        internal override void ShrinkToRange(ShapeBase other)
        {
            if (X < other.X)
            {
                Width -= other.X - X;
                X = other.X;
            }
            if (Y < other.Y)
            {
                Height -= other.Y - Y;
                Y = other.Y;
            }
            if (Right > other.Right)
            {
                Width = other.Right - X;
            }
            if (Bottom > other.Bottom)
            {
                Height = other.Bottom - Y;
            }
            if (Width < min)
            {
                Width = min;
                if (Right > other.Right) X = other.Right - Width;
            }
            if (Height < min)
            {
                Height = min;
                if (Bottom > other.Bottom) Y = other.Bottom - Height;
            }
        }
        internal override bool OnFocus()
        {
            return CurFocus.ShowShape == this;
        }
        internal override Rectangle MRectangle { get { return new Rectangle(X + MarkShift.X, Y + MarkShift.Y, Width, Height); } }
        /// <summary>
        /// 将图形在板中坐标转化为在MarkedPicture中的坐标；
        /// </summary>
        /// <returns></returns>
        internal override Point ChangeToShowPoint()
        {
            return new Point(MarkedPicture.PosToShowX(X), MarkedPicture.PosToShowY(Y));
        }
        internal override void DrawSelf(Graphics g,Pen p)
        {
            g.DrawEllipse(p, new RectangleF(ChangeToShowPoint(),new Size(Width,Height)));
        }
        internal override void ChangeSelf(Direction dt)
        {
        }
        internal override Direction MouseOverWhere(Point e)
        {
            int nowdelt = (int)(mdelt / MarkedPicture.CurDisplayRate);
            int direction = CalculateDirec(ShiftCenter, e);
            if (direction> mdelt+nowdelt)
            {
                return Direction.outside;
            }
            else if(direction<=mdelt+nowdelt&&direction>=mdelt-nowdelt)
            {
                //再判断...
                if (e.X==ShiftCenter.X&&e.Y<ShiftCenter.Y)
                {
                    return Direction.top;
                }
                else if (e.X == ShiftCenter.X && e.Y > ShiftCenter.Y)
                {
                    return Direction.bottom;
                }
                else if (e.X<ShiftCenter.X&& e.Y < ShiftCenter.Y)
                {
                    return Direction.topLeft;
                }
                else if(e.X < ShiftCenter.X && e.Y > ShiftCenter.Y)
                {
                    return Direction.bottomLeft;
                }
                else if (e.X>ShiftCenter.X&&e.Y<ShiftCenter.Y)
                {
                    return Direction.topRight;
                }
                else
                {
                    return Direction.bottomRight;
                }
            }
            else
            {
                return Direction.center;
            }
        }
    }
}
