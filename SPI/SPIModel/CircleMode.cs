using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI.SPIModel
{
    public class CircleMode : ShapeBase
    {
        private Point center = Point.Empty;
        private Double radius;

        public static Point DefaultCenter;
        public const double DefaultRadius=20;

         public Point GetCenter()
        {
            return center;
        }
        public CircleMode(Point center,double radius)
        {
            this.center = center;
            this.radius = radius;
        }
        public CircleMode():this(DefaultCenter,DefaultRadius)
        { }
    }
}
