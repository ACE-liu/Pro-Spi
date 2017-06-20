using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;

namespace SPI.SPICheckWin
{
    #region **********Board类**************
    /*
    默认整板shape 为长方形
    */
    class Board:WinBase
    {
        public Board()
        {
            this.ShowShape = new RectangleMode();
        }
        public List<WinBase> SubWinList = null;
        public Board(ShapeBase NewShape)
        {
            this.ShowShape = NewShape;
        }

        public override void Show(Graphics g)
        {
           // ShowShape.Show();
            //throw new NotImplementedException();
        }

    }
    #endregion  
}
