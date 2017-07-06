using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;
using System.Windows.Forms;

using static SPI.Global.Configuration;
using SPI.Global;

namespace SPI.SPICheckWin
{
    #region **********Board类**************
    /*
    默认整板shape 为长方形
    */
    class Board : WinBase
    {

        public Board()
        {
            ShowShape = new RectangleMode(800, 500);
            initialize();
        }
        /// <summary>
        /// 记录板左上角在markpicture中的坐标；
        /// </summary>
        public Point MpLocation => ShowShape.Location;
        public Board(ShapeBase NewShape)
        {
            this.ShowShape = NewShape;
            initialize();
        }
        /// <summary>
        /// board类初始化公共部分
        /// </summary>
        private void initialize()
        {
            id = 0;
            WinName = "PCB";
            this.wType = WinType.Board;
            SubWinList = new List<WinBase>();
        }
        public void AddWin(WinBase win)
        {
            SubWinList?.Add(win); //主板中subWinList可能包含子板和元件
        }
        public void AddWin(WinBase win,Point Location)
        {
            win.ShowShape.ShiftCenterTo(Location);
            SubWinList?.Add(win);
            win.Parent = this;
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
        internal void OnMouseDown(object sender, MouseEventArgs e)
        {
            Point de = MarkedPicture.ShowToPos(e.Location);
            if (e.Button == MouseButtons.Left)
            {
                WinBase mf = this.FindMouseOnRect(de);
                if (mf == CurFocus)
                {
                    MarkedPicture.IsChangingWinNow = true;
                    if (CurFocus != TheBoard)
                    {
                        //设置将要修改哪个边。
                        MarkedPicture.ChangingEdge = CurFocus.ShowShape.MouseOverWhere(de);
                    }
                    else
                    {
                        //单板只能修改显示位置
                        theMarkPicture.Cursor = Cursors.NoMove2D;//移动中心位置
                        MarkedPicture.ChangingEdge = Direction.center;
                    }
                }
                else if(CurFocus is Board)
                {
                    //单板只能修改显示位置
                    theMarkPicture.SetDownDelt(e.Location);
                    theMarkPicture.Cursor = Cursors.NoMove2D;
                    MarkedPicture.IsChangingWinNow = true;
                    MarkedPicture.ChangingEdge = Direction.center;
                }
            }
            //else if (e.Button == MouseButtons.Right)
            //{
            //    //***** 右键按下时移动板显示位置。
            //    SetFocus(TheBoard);
            //    theMarkPicture.Cursor = Cursors.NoMove2D;
            //}
        }
        internal void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point d = MarkedPicture.ShowToPos(e.Location);
                SetFocus(FindMouseOnRect(d));
            }
        }
        internal void OnMouseMove(object sender, MouseEventArgs e)
        {
        }
    }
    #endregion  
}
