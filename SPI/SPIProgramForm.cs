using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPI.SPICheckWin;

using static SPI.Global.Configuration;

namespace SPI
{
    public partial class SPIProgramForm : UserControl
    {
        private static SPIProgramForm singleProForm = null;
        private Rectangle markpictureRect => new Rectangle(getRealLocation(markedPicture1, this), markedPicture1.Size);
        private SPIProgramForm()
        {
            InitializeComponent();
        }
        public static SPIProgramForm GetInstance()
        {
            if (singleProForm==null)
            {
                singleProForm = new SPIProgramForm();
            }
            return singleProForm;
        }
        private void tbAddWin_Click(object sender, EventArgs e)
        {
            CheckWinBase win = new Chip(40, 40);
            AddComponent(win);
        }
        private void AddComponent(CheckWinBase checkwin)
        {

        }
        /// <summary>
        /// 滚轮滑动事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            pictureZoom(e);
        }
        /// <summary>
        /// 获取窗体相对容器的容器的位置
        /// </summary>
        /// <param name="control"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Point getRealLocation(Control control , Control parent)
        {
            Point rtn = control.Location;
            if (control.Parent!=parent)
            {
                return AddPoint(rtn, getRealLocation(control.Parent, parent));
            }
            return rtn;
        }
        
        private void pictureZoom(MouseEventArgs e)
        {
            Rectangle picRect = markpictureRect;
            Point ct=markedPicture1.Parent.Location;

            if (picRect.Contains(e.Location))
            {
                ct = new Point(e.X - markedPicture1.Left, e.Y - markedPicture1.Top);
            }
            else
                return;

            int level = 0;//显示级别改变量
            if (e.Delta > 0) level--;
            else if (e.Delta < 0) level++;
            if (level != 0)
            {
                markedPicture1.ChangeShowLevel(level, ct.X, ct.Y);
            }
        }
    }
}
