using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static double CurDisplayRate;//Current displayRate
        /// <summary>
        /// 各显示级别对应的显示比例查找表.
        /// </summary>
        private static double[] rateMap = null;

        private Point mouseDownPosition;
        #endregion
        public MarkedPicture()
        {
            InitializeComponent();
        }
        internal void ChangeShowLevel(int delt ,int x,int y)
        { }

        private void MarkedPicture_Paint(object sender, PaintEventArgs e)
        {
            PaintTheBoard(sender, e);
        }
        /// <summary>
        /// 绘制整板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PaintTheBoard(object sender,PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            TheBoard?.Show(g);
        }

        private void MarkedPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button== MouseButtons.Left)
            {
                mouseDownPosition = e.Location;  //记录每次点击时鼠标按下的位置
            }
        }

        private void MarkedPicture_MouseUp(object sender, MouseEventArgs e)
        {
            TheBoard?.OnMouseUp(e);
        }
    }
}
