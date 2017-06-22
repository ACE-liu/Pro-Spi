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
    public class RectangleMode:ShapeBase
    {
        private Point center = Point.Empty;
        private ShapeType m_ShapeType = ShapeType.None;
    
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get;  set; }
        public int Height { get;  set; }
        public Point MarkShift { get; set; } = Point.Empty;
        public Rectangle MRectangle { get { return new Rectangle(X + MarkShift.X, Y + MarkShift.Y, Width, Height); } }

        public Point GetCenter()
        {
            return new Point(X + Width / 2,Y + Height / 2);
        }
        /*默认构造函数*/
        public RectangleMode()
        {
            this.Width = 30;
            this.Height = 30;
            this.m_ShapeType = ShapeType.Rectangle;
        }
        public RectangleMode(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.m_ShapeType = ShapeType.Rectangle;
        }
        public ShapeType GetShapeType()
        {
            return m_ShapeType;
        }
        public bool OnFocus()
        {
            return CurFocus.ShowShape == this;
        }
        public void DrawSelf(Graphics g)
        {
            g.DrawRectangle(GetPenByShape(this, 1), X, Y, Width, Height);
        }
        public Direction MouseOverWhere(Point e)
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
