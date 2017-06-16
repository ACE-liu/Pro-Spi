using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI.SPIModel
{
    public class RectangleMode:ShapeBase
    {
        private Point center = Point.Empty;
        public Point LeftPeak { get; private set; }
        public int X { get { return LeftPeak.X; } }
        public int Y { get { return LeftPeak.Y; } }
        public int Width { get;  set; }
        public int Height { get;  set; }
        public Point GetCenter()
        {
            return new Point(LeftPeak.X + Width / 2, LeftPeak.Y + Height / 2);
        }
    }
}
