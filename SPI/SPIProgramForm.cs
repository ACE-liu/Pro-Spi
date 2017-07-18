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
using SPI.SPIModel;

namespace SPI
{
    public partial class SPIProgramForm : UserControl
    {
        private static SPIProgramForm singleProForm = null;
        private Rectangle markpictureRect => new Rectangle(getRealLocation(markedPicture1, this), markedPicture1.Size);
        private SPIProgramForm()
        {
            InitializeComponent();
            theMarkPicture = markedPicture1;
            CurFocusPanel = splitContainer3.Panel1;
        }
        private static bool isFirstLoad = false;
        public static SPIProgramForm GetInstance()
        {
            isFirstLoad = false;
            if (singleProForm==null)
            {
                singleProForm = new SPIProgramForm();
                isFirstLoad = true;
            }
            return singleProForm;
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
        /// 加载过程mp体积变化，等加载完全后获取设置
        /// </summary>
        internal void OnFirstLoadOnMainform()
        {
            if (isFirstLoad)
            {
                theMarkPicture.OnFirstLoad();
            }
        }

        /// <summary>
        /// 处理鼠标滚动事件
        /// </summary>
        /// <param name="e"></param>
        private void pictureZoom(MouseEventArgs e)
        {
            Rectangle picRect = markpictureRect;
            Point ct;
            if (picRect.Contains(e.Location))
            {
               // ct = new Point(e.X - markedPicture1.Left, e.Y - markedPicture1.Top);
                ct = new Point(e.X - picRect.Left, e.Y - picRect.Top); /*鼠标位置是相对整个窗口 ，markedpicture1的坐标是相对父容器的，所以要返回顶层 */
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
        internal void AddComponent(WinBase win)
        {
            markedPicture1.AddWin(win);
            SetFocus(win);
        }
        /// <summary>
        /// 新建程序
        /// </summary>
        private void buildNewProgram()
        {
            DataGridDlg dlg = new DataGridDlg();
            NewBoardDlgData data = new NewBoardDlgData();
            dlg.SelectedObject = data;
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                data.SetBoardData(TheBoard);
            }
        }
        /// <summary>
        /// 保存程序
        /// </summary>
        private void saveProgram()
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            saveProgram();
        }

        private void tbNewProgram_Click(object sender, EventArgs e)
        {
            buildNewProgram();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AddComponent(new MarkPoint(new CircleMode()));
        }

    }
}
