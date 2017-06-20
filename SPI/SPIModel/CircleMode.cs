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
    public class CircleMode : ShapeBase
    {
        private Point center = Point.Empty;
        private ShapeType m_ShapeType = ShapeType.None;
        private int radius;

        public static Point DefaultCenter;
        public const int DefaultRadius=20;

         public Point GetCenter()
        {
            return center;
        }
        public ShapeType GetShapeType()
        {
            return this.m_ShapeType;
        }
        public CircleMode(Point center,int radius)
        {
            this.m_ShapeType = ShapeType.Circle;
            this.center = center;
            this.radius = radius;
        }
        public CircleMode():this(DefaultCenter,DefaultRadius)
        {
        }
        public bool OnFocus()
        {
            return CurFocus == this;
        }
        public void DrawSelf(Graphics g)
        {
            g.DrawEllipse(GetPenByShape(this, 1), new RectangleF(center.X - radius, center.Y - radius, 2 * radius, 2 * radius));
        }
    }
}
