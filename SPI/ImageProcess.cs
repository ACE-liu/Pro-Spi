using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI
{
    class ImageProcess
    {
        /// <summary>
        /// 用于存储当前直方图数据的数组。其中数值按 showHeight 为最大值等比例调整。
        /// </summary>
        public static int[] histoData;
        /// <summary>
        /// 直方图数据数量。256对应0-255的灰度值。
        /// </summary>
        public static int histoNum = 256;
        /// <summary>
        /// 显示控件的高度。将直方图中的最大值缩放为此值，其他数值等比例调整。
        /// </summary>
        public static int showHeight = 150;   //Height of the histogram picture in multi color control.
        /// <summary>
        /// 直方图相关数据初始化
        /// </summary>
        private static void HistoInit()
        {
            histoNum = 256;
            showHeight = 150;   //Height of the histogram picture in multi color control.
            histoData = new int[histoNum];
            for (int i = 0; i < histoNum; i++)
            {
                histoData[i] = 0;
            }
        }
        /// <summary>
        /// 初始化，在程序启动时调用。
        /// </summary>
        public static void Initialize()
        {
            HistoInit();
        }
    }
}
