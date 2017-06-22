using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;
using System.Windows.Forms;

using static SPI.Global.Configuration;

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
            ShowShape = new RectangleMode(500,500);
            initialize();
        }
        public Board(ShapeBase NewShape)
        {
            this.ShowShape = NewShape;
            initialize();
            SubWinList = new List<WinBase>();
        }
        /// <summary>
        /// board类初始化公共部分
        /// </summary>
        private void initialize()
        {
            id = 0;
            WinName = "PCB";
        }
        public void AddWin(WinBase win)
        {
            SubWinList.Add(win); //主板中subWinList可能包含子板和元件
        }
        public void RemoveWin(WinBase win)
        {
            SubWinList.Remove(win);
        }
        public override void Show(Graphics g)
        {
            ShowShape.DrawSelf(g);
            if (SubWinList!=null)
            {
                foreach (var item in SubWinList)
                {
                    item.Show(g);
                }
            }
           // ShowShape.Show();
            //throw new NotImplementedException();
        }
        internal void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button ==MouseButtons.Left)
            {
                Point d = e.Location;
                SetFocus(FindMouseOnRect(d));
            }
        }

    }
    #endregion  
}
