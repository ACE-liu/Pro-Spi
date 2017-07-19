using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using SPI.SPICheckWin;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace SPI.Global
{
    /* 用于存放常用的静态函数静态字段*/
    public static class Configuration
    {
        #region***********Field***********

        /// <summary>
        /// 方框的最小尺寸。
        /// </summary>
       public const int min = 30;
        /// <summary>
        /// 选择方框边时允许的鼠标点队应的单板位置偏差。
        /// </summary>
       public const int mdelt = 3;
       public const int DefaultDataVersion = int.MaxValue;
        /// <summary>
        /// 默认文件格式
        /// </summary>
        public const string fileFormat = ".dat";
        /// <summary>
        /// 当前焦点
        /// </summary>
        internal static WinBase CurFocus = null;
        internal static bool AutoChecking = false;
        internal static MainForm theMainForm = null;
        internal static bool ShowingUI = false;
        /// <summary>
        /// 描述添加元件时元件默认位置
        /// </summary>
        internal static Point DefaultLocation = Point.Empty;
        /*当前操作的板*/
        internal static Board TheBoard = null;

        internal static int ControlsGap=6;
        internal static bool DataChanged = false;//标记数据已经改动可能需要保存
        internal static bool HideFollows = true;
        /// <summary>
        /// 是否使用抽色笔 
        /// </summary>
        public static bool UseColPen = false;
        /// <summary>
        /// 当前正在编辑的抽色
        /// </summary>
        public static MultiColors CurMc = null;
        /// <summary>
        /// 是否正在使用抽色笔
        /// </summary>
        internal static bool UsingColPenNow = false;
        public static Bitmap RedColorBackground;
        public static Bitmap GreenColorBackground;  //   
        public static Bitmap BlueColorBackground;   // 
        /// <summary>
        /// 保存配置FOV大小
        /// </summary>
        internal static Size FovSize;
        /// <summary>
        /// 为了方便操作坐标及其他数据，在加载MP时保存全局的mp指针；
        /// </summary>
        internal static MarkedPicture theMarkPicture = null;
        internal static System.Windows.Forms.Panel CurFocusPanel = null;

        #region ***程序路径相关配置**
        public static string ProgramRootPath=>Directory.GetCurrentDirectory();
        public static string ProgramFilePath=>ToDirPath(ProgramRootPath)+"prjtxt\\";  //暂时和2D命名一样
        public static string ProgramImagePath => ToDirPath(ProgramRootPath) + "prjimg\\";

        public static string CurProjectName; //当前程序名
        #endregion

        /// <summary>
        /// 显示在markedPicture中的图像在主板中的位置。
        /// </summary>
        public static Rectangle showPart;
        internal static bool ShowProperty = false;
        #endregion

        #region***********Method***********
        public static string ToDirPath(string path)
        {
            if (path.EndsWith("\\")||path.EndsWith("/"))
            {
                return path;
            }
            return path + "\\";
        }
        /// <summary>
        /// 全局初始化
        /// </summary>
        public static void Initialize()
        {

        }
        /// <summary>
        /// 获取写文件的相对路径
        /// </summary>
        /// <param name="path">不带后缀的路径名</param>
        /// <param name="format">格式名</param>
        /// <returns></returns>
        public static string GetWritePath(string path, string format = fileFormat)
        {
            return path + format;
        }
        public static string GetPrjFileRootPath()
        {
            if (string.IsNullOrEmpty(ProgramFilePath))
            {
                return "";
            }
            return ToDirPath(ProgramFilePath);
        }
        /// <summary>
        /// 指定检查框参数设置UI是否正显示在界面上。
        /// </summary>
        /// <param name="holder"></param>
        /// <returns></returns>
        internal static bool HolderShowing(CheckWinBase holder)
        {
            return (holder == CurFocus) && !AutoChecking && ShowingUI;
        }
        /// <summary>
        /// 获取窗体相对容器的容器的位置
        /// </summary>
        /// <param name="control"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Point getRealLocation(Control control, Control parent)
        {
            Point rtn = control.Location;
            if (control.Parent != parent)
            {
                return AddPoint(rtn, getRealLocation(control.Parent, parent));
            }
            return rtn;
        }
        public static SizeF GetControlSize(Control c, string text, Font font)
        {
            Graphics g = Graphics.FromHwnd(c.Handle);
            SizeF size = g.MeasureString(text, font);
            g.Dispose();
            return size;
        }
        /// <summary>
        /// 返回文本的整数值
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="dft">默认值</param>
        /// <returns></returns>
        internal static int SafeParseInt(string txt, int dft)
        {
            try
            {
                return int.Parse(txt);
            }
            catch (Exception)
            {
               // RmlDebug.LogLine("parse int failed.\r\n" + "txt:" + txt);
            }
            return dft;
        }
        /// <summary>
        /// 根据当前状态获取绘制检测窗的笔
        /// </summary>
        /// <param name="win"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        internal static Pen GetPenByWin(WinBase win, float width=1)
        {
            if(win==null)
            {
                return null;
            }
            if (win==MarkedPicture.MouseOnWin||win is Board)
            {
                width = 2;
            }
            if (win==CurFocus)
            {
                return new Pen(Color.Red, width);
            }
            else if((win as CheckWinBase)?.hasResult??false)
            {
                return new Pen(Color.Red, width);
            }
            else
            {
                return new Pen(Color.Blue, width);
            }
        }
        internal static Pen GetPenByShape(ShapeBase shape, float width)
        {
            WinBase win = TheBoard?.SubWinList?.Find(p => p.ShowShape == shape);
            return GetPenByWin(win, width);
        }
        internal static Point AddPoint(Point source,Point addPart)
        {
            return new Point(source.X + addPart.X, source.Y + addPart.Y);
        }
        /// <summary>
        /// 计算2点距离返回整数
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        internal static int CalculateDirec(Point A,Point B)
        {
            return Convert.ToInt32((Math.Sqrt((A.X - B.X) ^ 2 + (A.Y - B.Y) ^ 2)));
        }
        internal static void SetFocus(WinBase window)
        {
            //
            if (window!=null&&CurFocus!=window)
            {
                CurFocus?.OnLoseFocus();
                CurFocus = window;
                window?.OnFocus();
            }
        }
        internal static bool IsValidName(string text)
        {
            string pattern = @"[#$@&.,|/\\:*?""'<>]";
            Match mt =Regex.Match(text, pattern);
            return mt.Value == "";
        }
        #endregion
    }
}
