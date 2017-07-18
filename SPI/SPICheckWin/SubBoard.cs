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
        private List<MarkPoint> MarkPoints = null;
        private List<CheckWinBase> Components = null;

        public SubBoard()
        {
            ShowShape = new RectangleMode(500, 500);
            initialize();
        }
        private SubBoard(ShapeBase NewShape)
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
            this.wType = WinType.Board;
            SubWinList = new List<WinBase>();
        }
        public void AddWin(WinBase win)
        {
            SubWinList?.Add(win); //主板中subWinList可能包含子板和元件
            win.Parent = this;
            if (win is MarkPoint)
            {
                AddMarkPoint(win as MarkPoint);
            }
            else if (win is CheckWinBase)
            {
                AddCheckWin(win as CheckWinBase);
            }
        }
        public void AddWin(WinBase win, Point Location)
        {
            win.ShowShape.ShiftCenterTo(Location);
            SubWinList?.Add(win);
            win.Parent = this;
            if (win is MarkPoint)
            {
                AddMarkPoint(win as MarkPoint);
            }
            else if (win is CheckWinBase)
            {
                AddCheckWin(win as CheckWinBase);
            }
        }
        public void AddMarkPoint(MarkPoint mp)
        {
            if (MarkPoints==null)
            {
                MarkPoints = new List<MarkPoint>();
            }
            MarkPoints.Add(mp);
        }
        public void AddCheckWin(CheckWinBase checkwin)
        {
            if (Components==null)
            {
                Components = new List<CheckWinBase>();
            }
            Components.Add(checkwin);
        }
        public void RemoveWin(WinBase win)
        {
            SubWinList?.Remove(win);
            if (win is MarkPoint)
            {
                MarkPoints.Remove(win as MarkPoint);
            }
            else if (win is CheckWinBase)
            {
                Components.Remove(win as CheckWinBase);
            }
            win.OnDeleteFromBoard();
        }
        public override void OnDeleteFromBoard()
        {
            SubWinList?.Clear();
            MarkPoints?.Clear();
            Components?.Clear();
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
        public void Save(MyWriter mw)
        {
            mw.Save(id);
            mw.Save(ShowShape.X);
            mw.Save(ShowShape.X);
            mw.Save(ShowShape.Y);
            mw.Save(ShowShape.Width);
            mw.Save(ShowShape.Height);
            mw.Save(ShowShape.Angle);

            mw.Save(MarkPoints.Count);
            mw.Save(Components.Count);
            mw.SaveLineEnd();
            foreach (var item in MarkPoints)
            {
                item.Save(mw);
            }
            foreach (var item in Components)
            {
                mw.Save(item.WType);
                item.Save(mw);
            }
        }
        public void LoadFrom(MyReader mr)
        {
            id = mr.LoadInt();
            ShowShape.X = mr.LoadInt();
            ShowShape.Y = mr.LoadInt();
            ShowShape.Width = mr.LoadInt();
            ShowShape.Height = mr.LoadInt();
            ShowShape.Angle = mr.LoadDouble();

            int mpc = mr.LoadInt();
            int cpc = mr.LoadInt();
            mr.LoadLineEnd();

            for (int i = 0; i < mpc; i++)
            {
                MarkPoint mp = new MarkPoint();
                mp.LoadFrom(mr);
                AddWin(mp);
            }
            for (int i = 0; i < cpc; i++)
            {
                WinType wt = mr.LoadType();
                CheckWinBase cw = NewComponent(wt);
                cw.LoadFrom(mr);
                AddWin(cw);
            }

        }
    }
}
