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
    internal class RectangleMode:ShapeBase
    {
        private Point center = Point.Empty;
        private ShapeType m_ShapeType = ShapeType.None;
    
        public override int X { get; set; }
        public override int Y { get; set; }
        public override int Width { get;  set; }
        public override int Height { get;  set; }
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
        internal override Point MarkShift { get; set; } = Point.Empty;
        internal override Rectangle MRectangle { get { return new Rectangle(X + MarkShift.X, Y + MarkShift.Y, Width, Height); } }

        internal override Point GetCenter()
        {
            return new Point(X + Width / 2,Y + Height / 2);
        }
        internal override void Move(Point p)
        {
            this.X += p.X;
            this.Y += p.Y;
        }
        /// <summary>
        /// 将图形在板中坐标转化为在MarkedPicture中的坐标；
        /// </summary>
        /// <returns></returns>
        internal override Point ChangeToShowPoint()
        {
            return new Point(MarkedPicture.PosToShowX(X), MarkedPicture.PosToShowY(Y));
        }
        /*默认构造函数*/
        internal RectangleMode()
        {
            this.Width = 30;
            this.Height = 30;
            this.m_ShapeType = ShapeType.Rectangle;
            
        }
        internal RectangleMode(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.m_ShapeType = ShapeType.Rectangle;
        }
        internal override ShapeType GetShapeType()
        {
            return m_ShapeType;
        }
        internal override bool OnFocus()
        {
            return CurFocus.ShowShape == this;
        }
        internal override void DrawSelf(Graphics g,Pen p)
        {
            g.DrawRectangle(p, new Rectangle(ChangeToShowPoint(), new Size(Width, Height)));
        }
        internal override void ChangeSelf(Direction de)
        { }
        internal override Direction MouseOverWhere(Point e)
        {
            int nowdelt = (int)(mdelt / MarkedPicture.CurDisplayRate);
            if ((e.X < MRectangle.Left - nowdelt) || (e.X > MRectangle.Right + nowdelt) || (e.Y < MRectangle.Top - nowdelt) || (e.Y > MRectangle.Bottom + nowdelt))
                return Direction.outside;

            bool left = (e.X >= MRectangle.Left - nowdelt) && (e.X <= MRectangle.Left + nowdelt);
            bool right = (e.X >= MRectangle.Right - nowdelt) && (e.X <= MRectangle.Right + nowdelt);
            bool top = (e.Y >= MRectangle.Top - nowdelt) && (e.Y <= MRectangle.Top + nowdelt);
            bool bottom = (e.Y >= MRectangle.Bottom - nowdelt) && (e.Y <= MRectangle.Bottom + nowdelt);

            Direction mystate = Direction.outside;
            if (top)
            {
                if (left) { mystate = Direction.topLeft; }
                else if (right) { mystate = Direction.topRight; }
                else { mystate = Direction.top; }
            }
            else if (bottom)
            {
                if (left) { mystate = Direction.bottomLeft; }
                else if (right) { mystate = Direction.bottomRight; }
                else { mystate = Direction.bottom; }
            }
            else
            {
                if (left) { mystate = Direction.left; }
                else if (right) { mystate = Direction.right; }
                else { mystate = Direction.center; }
            }
            return mystate;
        }
    }
}
