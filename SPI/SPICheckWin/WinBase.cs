using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;

namespace SPI.SPICheckWin
{
    abstract class WinBase
    {
        public ShapeBase ShowShape { get; protected set; } = null;
        public abstract void Show(Graphics g);
    }
}
