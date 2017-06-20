using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.Global;
using static SPI.Global.ConfigMember;

namespace SPI.SPIModel
{
    public class RectangleMode:ShapeBase
    {
        private Point center = Point.Empty;
        private ShapeType m_ShapeType = ShapeType.None;
    
        public Point LeftPeak { get; private set; }
        public int X { get { return LeftPeak.X; } }
        public int Y { get { return LeftPeak.Y; } }
        public int Width { get;  set; }
        public int Height { get;  set; }
        public Point GetCenter()
        {
            return new Point(LeftPeak.X + Width / 2, LeftPeak.Y + Height / 2);
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
            return CurFocus == this;
        }
        public void DrawSelf(Graphics g)
        {
            g.DrawRectangle(GetPenByShape(this, 1), X, Y, Width, Height);
        }
    }
}
