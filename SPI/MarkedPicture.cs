using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPI.Global;
using SPI.SPICheckWin;

using static SPI.Global.Configuration;

namespace SPI
{
    public partial class MarkedPicture : UserControl
    {
        #region *************参数*****************
        /// <summary>
        /// 最大显示比例,常量
        /// </summary>
        //public const double MaxDisplayRate = 2.0;//Maxmam display Rate, 4.0/2.0/1/0.5, Maybe 2.0 is enough
        /// <summary>
        /// 可以处理的最多的显示级别,常量.
        /// </summary>
        const int DisplayLevelNum = 15; // 加大不影响效率和内存. 10对应最小缩小比例为2/2Exp(10)=1/512,800*600尺寸的MarkedPicture可显示600*512>300000点的板,对10um分辨率,宽度大于3m.
        /// <summary>
        /// 原始大小的图对应的显示级别,常量
        /// </summary>
        const int OrgPictureLevel = 1;//Level of the orignal picture. MaxDisplayRete/Exp(2,OrgPictureLevel) should be 1;
        /// <summary>
        /// 每块图像块的Width,常量//暂未用
        /// </summary>
        static int PieceWidth = FovSize.Width / 2;
        /// <summary>
        /// 每块图像块的Height,常量//暂未用
        /// </summary>
        static int PieceHeight = FovSize.Height / 2;
        /// <summary>
        /// 当前显示级别
        /// </summary>
        private static int CurDisplayLevel;

        /// <summary>
        /// 当前显示比例.
        /// </summary>
        public static double CurDisplayRate=1;//Current displayRate
        /// <summary>
        /// 各显示级别对应的显示比例查找表.
        /// </summary>
        private static double[] rateMap = null;

        public static Direction ChangingEdge = Direction.outside;
        /// <summary>
        /// 记录当前鼠标按下时的位置
        /// </summary>
        public static Point mouseDownPosition;
        /// <summary>
        /// 表示是否正在修改Win 位置或坐标，一般鼠标按下才能进行修改
        /// </summary>
        public static bool IsChangingWinNow = false;
        /// <summary>
        /// 记录当前鼠标左键是否被按下
        /// </summary>
        public bool MouseLeftDown = false;
        /// <summary>
        /// 获取到Markedpicture的中心坐标，每次新加检测窗中心位置
        /// </summary>
        /// <returns></returns>
        public Point MpCenter=> new Point(showPart.X + showPart.Width / 2, showPart.Y + showPart.Height / 2);

        /// <summary>
        /// 板显示的偏移
        /// </summary>
        public Point BoardShowShift = Point.Empty;
        /// <summary>
        /// 用于计算拖动时的辅助量，保存鼠标按下时界面的状态值
        /// </summary>
        public Point MouseDownDelt = Point.Empty;

        #endregion
        public MarkedPicture()
        {
            InitializeComponent();
            SetXY(0, 0);
            CurDisplayRate = 1;
        }
        internal void ChangeShowLevel(int delt, int x, int y)
        { }

        private void MarkedPicture_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            PaintTheBoard(sender, e);
            //***** 画中心位置十字叉
            g.DrawLine(Pens.Gray, new Point(Width / 2, 0), new Point(Width / 2, Height));
            g.DrawLine(Pens.Gray, new Point(0, Height / 2), new Point(Width, Height / 2));
        }
        /// <summary>
        /// 将显示区域的左上角设置的指定板坐标位置.
        /// </summary>
        /// <param name="x0">左上角X坐标</param>
        /// <param name="y0">左上角Y坐标</param>
        public void SetXY(int x0, int y0)
        {
            showPart = new Rectangle(x0, y0, (int)(ClientRectangle.Width / MarkedPicture.CurDisplayRate), (int)(ClientRectangle.Height / MarkedPicture.CurDisplayRate));
        }
        /// <summary>
        /// 根据鼠标的显示位置计算鼠标按下时的特征值，用于计算鼠标移动时对应的板位置的调整量。
        /// </summary>
        /// <param name="e"></param>
        public void SetDownDelt(Point e)
        {
            if (CurFocus is Board)
            {
                MouseDownDelt.X = (int)(e.X / CurDisplayRate) + showPart.X;
                MouseDownDelt.Y = (int)(e.Y / CurDisplayRate) + showPart.Y;
            }
            else
            {
                Point de = ShowToPos(e);
                MouseDownDelt.X = de.X - CurFocus.ShowShape.MRectangle.X;
                MouseDownDelt.Y = de.Y - CurFocus.ShowShape.MRectangle.Y;
            }
        }
        /// <summary>
        /// 绘制整板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PaintTheBoard(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            TheBoard?.Show(g);
        }
        /// <summary>
        /// 板X坐标到显示坐标。
        /// </summary>
        /// <param name="x">板坐标x</param>
        /// <returns>显示坐标x</returns>
        public static int PosToShowX(int x)
        {
            return (int)((x - showPart.X) * MarkedPicture.CurDisplayRate);
        }
        /// <summary>
        /// 板Y坐标到显示坐标。
        /// </summary>
        /// <param name="y">板坐标y</param>
        /// <returns>显示坐标y</returns>
        public static int PosToShowY(int y)
        {
            return (int)((y - showPart.Y) * MarkedPicture.CurDisplayRate);
        }
        public static Point ShowToPos(Point p)
        {
            return new Point((int)(showPart.X + p.X / CurDisplayRate), (int)(showPart.Y + p.Y / CurDisplayRate));
        }
        internal void AddWin(WinBase win)
        {
            TheBoard?.AddWin(win, MpCenter);
            Refresh();
        }
        private void MarkedPicture_MouseDown(object sender, MouseEventArgs e)
        {
            SetDownDelt(e.Location);
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPosition = e.Location;  //记录每次点击时鼠标按下的位置
                MouseLeftDown = true;
            }
            TheBoard?.OnMouseDown(sender,e);//ShowToPos(e.Location));
        }

        private void MarkedPicture_MouseUp(object sender, MouseEventArgs e)
        {
            IsChangingWinNow = false;
            if (!MouseLeftDown)
            {
                return;
            }
            TheBoard?.OnMouseUp(sender,e);
            MouseLeftDown = false;
            Refresh();
        }
        /// <summary>
        /// 根据点相对方框的方位设置控件中显示的鼠标形状.
        /// </summary>
        /// <param name="mp">控件</param>
        /// <param name="e">点的板坐标</param>
        private void SetCursor(Point e)
        {
            Direction dir = CurFocus?.MouseOverWhere(e) ?? Direction.outside;
            switch (dir)
            {
                case Direction.center:
                    Cursor = Cursors.SizeAll;
                    break;
                case Direction.top:
                case Direction.bottom:
                    Cursor = Cursors.SizeNS;
                    break;
                case Direction.topRight:
                case Direction.bottomLeft:
                    Cursor = Cursors.SizeNESW;
                    break;
                case Direction.topLeft:
                case Direction.bottomRight:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case Direction.left:
                case Direction.right:
                    Cursor = Cursors.SizeWE;
                    break;
                default:
                    Cursor = Cursors.Default;
                    break;
            }
        }
        private void MarkedPicture_MouseMove(object sender, MouseEventArgs e)
        {
            Point de = ShowToPos(e.Location);
            if (IsChangingWinNow)
            {
                if (MarkedPicture.ChangingEdge == Direction.center && CurFocus is Board)
                {
                    theMarkPicture.SetXY(MouseDownDelt.X - (int)(e.Location.X / CurDisplayRate), MouseDownDelt.Y - (int)(e.Location.Y / CurDisplayRate));
                }
                else
                    CurFocus?.ChangeRect(e.Location);
                SetCursor(de);
                Refresh();
            }
            else if(e.Button == MouseButtons.Right)
            { }
            else
            {
                WinBase mouseFocus = TheBoard?.FindMouseOnRect(de);

                if ((mouseFocus == CurFocus) /*&& (CurFocus !=TheBoard)*/)
                {
                    //鼠标移动到当前焦点的范围内时，根据相对位置设置鼠标形状指示将改变哪个边。
                    SetCursor(de);
                }
                else
                {
                    //鼠标不在焦点范围时，显示为默认鼠标。
                    Cursor = Cursors.Default;
                }
            }
        }
    }
}
