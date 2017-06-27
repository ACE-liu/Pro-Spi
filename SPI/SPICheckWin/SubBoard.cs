using SPI.Global;
using SPI.SPIModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SPI.Global.Configuration;

namespace SPI.SPICheckWin
{
    class SubBoard:WinBase
    {
        public SubBoard()
        {
            ShowShape = new RectangleMode(800, 500);
            initialize();
        }
        public SubBoard(ShapeBase NewShape)
        {
            this.ShowShape = NewShape;
            initialize();
        }
        /// <summary>
        /// board类初始化公共部分
        /// </summary>
        private void initialize()
        {
            id = 1;
            WinName = "SubBoard";
            this.wType = WinType.Board;
            SubWinList = new List<WinBase>();
        }
        public void AddWin(WinBase win)
        {
            SubWinList?.Add(win); //主板中subWinList可能包含子板和元件
        }
        public void AddWin(WinBase win, Point Location)
        {
            win.ShowShape.ShiftCenterTo(Location);
            SubWinList?.Add(win);
        }
        public void RemoveWin(WinBase win)
        {
            SubWinList?.Remove(win);
        }
        public override void Show(Graphics g)
        {
            ShowShape.DrawSelf(g, GetPenByWin(this));
            if (SubWinList != null)
            {
                foreach (var item in SubWinList)
                {
                    item.Show(g);
                }
            }
            // ShowShape.Show();
            //throw new NotImplementedException();
        }
    }
}
