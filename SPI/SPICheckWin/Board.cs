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
using System.ComponentModel;

namespace SPI.SPICheckWin
{
    #region **********Board类**************
    /*
    默认整板shape 为长方形
    */
    class Board : WinBase
    {
        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.PCB名称), DisplayName(InternationalLanguage.PCB名称)]
        public string PcbName
        {
            get { return pcbName; }
            set { pcbName = value; }
        }
        private string pcbName;
        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.PCB长度单位μm), DisplayName(InternationalLanguage.PCB长度)]
        public int PcbWidth { get { return pcbWidth; } set { pcbWidth = value; } }
        private int pcbWidth;

        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.轨道宽度), DisplayName("轨道宽度")]
        public int TrackWidth { get { return trackWidth; } set { trackWidth = value; } }
        private int trackWidth;

        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.夹紧量单位μm), ReadOnly(false), DisplayName(InternationalLanguage.夹紧量)]
        public int SpareWidth
        { get { return spareWidth; } set { spareWidth = value; } }
        private int spareWidth;
        [CategoryAttribute(InternationalLanguage.单板信息), DescriptionAttribute("分辨率微米像素"), ReadOnlyAttribute(true), DisplayName("分辨率")]
        public double PixelSize { get { return _pixelSize; } set { _pixelSize = value; } }
        double _pixelSize;

        [Category(InternationalLanguage.单板信息), Description("启用主板条码"), DisplayName("启用主板条码")]
        public bool UseMainBoardBarcode
        {
            get { return useMainBoardBarcode; }
            set { useMainBoardBarcode = value; }
        }
        private bool useMainBoardBarcode;

        [Category(InternationalLanguage.单板信息),Description("启用子板条码"),DisplayName("启用子板条码")]
        public bool UseSubBoardBarcode
        {
            get { return useSubBoardBarcode; }
            set { useSubBoardBarcode = value; }
        }
        private bool useSubBoardBarcode;
        [Description(InternationalLanguage.单板信息), ReadOnly(false), DisplayName(InternationalLanguage.客户名称)]
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        private string customer;
        [Category(InternationalLanguage.单板信息),Description("网板厚度"),DisplayName("网板厚度")]
        public double HalftoneHeight
        { get { return halftoneHeight; }
            set { halftoneHeight = value; }
        }
        private double halftoneHeight;
        /// <summary>
        /// 保留各种类型元件指针，避免元件数量过多在subWinList中遍历查找比较耗时
        /// </summary>
        private List<MarkPoint> AllMarkPoints =null;
        private List<SubBoard> SubBoards = null;
        private List<CheckWinBase> Components = null;
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
            CanMove = true;
            CanResize = false;
            pcbName = "PCB";
            customer = "";
            useMainBoardBarcode = false;
            useSubBoardBarcode = false;
            this.wType = WinType.Board;
            SubWinList = new List<WinBase>();
        }
        public void AddWin(WinBase win)
        {
            SubWinList?.Add(win); //主板中subWinList可能包含子板和元件
            win.Parent = this;
            if (win is MarkPoint)
            {
                AddMarkpoint(win as MarkPoint);
            }
            else if (win is SubBoard)
            {
                AddSubBoard(win as SubBoard);
            }
            else if (win is CheckWinBase)
            {
                AddCheckWin(win as CheckWinBase);
            }
        }
        public void AddCheckWin(CheckWinBase checkwin)
        {
            if (Components ==null)
            {
                Components = new List<CheckWinBase>();
            }
            Components.Add(checkwin);
        }
        public void AddSubBoard(SubBoard sub)
        {
            if (SubBoards==null)
            {
                SubBoards = new List<SubBoard>();
            }
            SubBoards.Add(sub);
        }
        public void AddMarkpoint(MarkPoint mp)
        {
            if (AllMarkPoints==null)
            {
                AllMarkPoints = new List<MarkPoint>();
            }
            AllMarkPoints.Add(mp);
        }
        public void AddWin(WinBase win, Point Location)
        {
            win.ShowShape.ShiftCenterTo(Location);
            SubWinList?.Add(win);
            win.Parent = this;
        }
        /// <summary>
        /// 删除元件
        /// </summary>
        /// <param name="win"></param>
        public void RemoveWin(WinBase win)
        {
            SubWinList?.Remove(win);
            if (win is MarkPoint)
            {
                AllMarkPoints.Remove(win as MarkPoint);
            }
            else if(win is SubBoard)
            {
                SubBoards.Remove(win as SubBoard);
            }
            else
            {
                Components.Remove(win as CheckWinBase);
            }
            win.OnDeleteFromBoard();
        }
        public void Save(MyWriter mw)
        {
            mw.Save(pcbName);
            mw.Save(pcbWidth);
            mw.Save(trackWidth);
            mw.Save(spareWidth);
            mw.Save(_pixelSize);
            mw.Save(useMainBoardBarcode);
            mw.Save(useSubBoardBarcode);
            mw.Save(customer);
            mw.Save(halftoneHeight);

            mw.Save(AllMarkPoints.Count);
            mw.Save(SubBoards.Count);
            mw.Save(Components.Count);
            mw.SaveLineEnd();
            foreach (var item in AllMarkPoints)
            {
                item.Save(mw);
            }
            foreach (var item in SubBoards)
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
            pcbName = mr.LoadString();
            pcbWidth = mr.LoadInt();
            trackWidth = mr.LoadInt();
            spareWidth = mr.LoadInt();
            _pixelSize = mr.LoadDouble();
            useMainBoardBarcode = mr.LoadBool();
            useSubBoardBarcode = mr.LoadBool();
            customer = mr.LoadString();
            halftoneHeight = mr.LoadDouble();

            int mpc = mr.LoadInt();
            int sbc = mr.LoadInt();
            int cpc = mr.LoadInt();

            mr.LoadLineEnd();
            for (int i = 0; i < mpc; i++)
            {
                MarkPoint mp = new MarkPoint();
                mp.LoadFrom(mr);
                AddWin(mp);
            }
            for (int i = 0; i < sbc; i++)
            {
                SubBoard sb = new SubBoard();
                sb.LoadFrom(mr);
                AddWin(sb);
            }
            for (int i = 0; i < cpc; i++)
            {
                WinType wt = mr.LoadType();
                CheckWinBase cw = NewComponent(wt);
                cw.LoadFrom(mr);
                AddWin(cw);
            }
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
                else if (CurFocus is Board)
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
        public void SaveProgram(string path)
        {

        }
    }
    #endregion  

    /// <summary>
    /// 新建项目对话框使用的参数，其中的属性将被显示到屏幕，可由用户界面修改。
    /// </summary>
    internal class NewBoardDlgData
    {
        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.PCB名称), DisplayName(InternationalLanguage.PCB名称)]
        public string PcbName
        {
            get { return pcbName; }
            set { pcbName = value; }
        }
        private string pcbName;
        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.PCB长度单位μm), DisplayName(InternationalLanguage.PCB长度)]
        public int PcbWidth { get { return pcbWidth; } set { pcbWidth = value; } }
        private int pcbWidth;

        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.轨道宽度), DisplayName("轨道宽度")]
        public int TrackWidth { get { return trackWidth; } set { trackWidth = value; } }
        private int trackWidth;

        [Category(InternationalLanguage.单板信息), Description(InternationalLanguage.夹紧量单位μm), ReadOnly(false), DisplayName(InternationalLanguage.夹紧量)]
        public int SpareWidth
        { get { return spareWidth; } set { spareWidth = value; } }
        private int spareWidth;
        [CategoryAttribute(InternationalLanguage.单板信息), DescriptionAttribute("分辨率微米像素"), ReadOnlyAttribute(true), DisplayName("分辨率")]
        public double PixelSize { get { return _pixelSize; } set { _pixelSize = value; } }
        double _pixelSize;

        [Category(InternationalLanguage.单板信息), Description("启用主板条码"), DisplayName("启用主板条码")]
        public bool UseMainBoardBarcode
        {
            get { return useMainBoardBarcode; }
            set { useMainBoardBarcode = value; }
        }
        private bool useMainBoardBarcode;

        [Category(InternationalLanguage.单板信息), Description("启用子板条码"), DisplayName("启用子板条码")]
        public bool UseSubBoardBarcode
        {
            get { return useSubBoardBarcode; }
            set { useSubBoardBarcode = value; }
        }
        private bool useSubBoardBarcode;
        [Description(InternationalLanguage.单板信息), ReadOnly(false), DisplayName(InternationalLanguage.客户名称)]
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        private string customer;
        [Category(InternationalLanguage.单板信息), Description("网板厚度"), DisplayName("网板厚度")]
        public double HalftoneHeight
        {
            get { return halftoneHeight; }
            set { halftoneHeight = value; }
        }
        private double halftoneHeight;

        public NewBoardDlgData()
        {
            pcbName = TheBoard.PcbName;
            PcbWidth = TheBoard.PcbWidth;
            trackWidth = TheBoard.TrackWidth;
            spareWidth = TheBoard.SpareWidth;
            _pixelSize = TheBoard.PixelSize;
            useMainBoardBarcode = TheBoard.UseMainBoardBarcode;
            useSubBoardBarcode = TheBoard.UseSubBoardBarcode;
            customer = TheBoard.Customer;
            halftoneHeight = TheBoard.HalftoneHeight;
        }

        public void SetBoardData(Board bd)
        {
            bd.PcbName = pcbName;
            bd.PcbWidth = pcbWidth;
            bd.TrackWidth = trackWidth;
            bd.SpareWidth = spareWidth;
            bd.PixelSize = _pixelSize;
            bd.UseMainBoardBarcode = useMainBoardBarcode;
            bd.UseSubBoardBarcode = useSubBoardBarcode;
            bd.Customer = customer;
            bd.HalftoneHeight = halftoneHeight;
        }
    }
}
