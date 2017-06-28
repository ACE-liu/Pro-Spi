using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPI.SPICheckWin;
using static SPI.Global.Configuration;

namespace SPI
{
    public partial class MainForm : Form
    {
        private ToolStripItem CurSelectedItem = null;
        public MainForm()
        {
            InitializeComponent();
            LoadLastProgramMsg();
            onLoadRunForm();
        }
        /// <summary>
        /// 打开程序编辑界面
        /// </summary>
        private void onLoadProgramForm()
        {
            SPIProgramForm form = SPIProgramForm.GetInstance();
            form.Dock = DockStyle.Fill;
            spDivide.Panel2.Controls.Clear();
            spDivide.Panel2.Controls.Add(form);
            form.OnFirstLoadOnMainform();
        }
        /// <summary>
        /// 打开运行界面
        /// </summary>
        private void onLoadRunForm()
        {
            SPIRunForm form = SPIRunForm.GetInstance();
            form.Dock = DockStyle.Fill;
            spDivide.Panel2.Controls.Clear();
            spDivide.Panel2.Controls.Add(form);
        }
        /// <summary>
        /// 加载上一次程序信息；
        /// </summary>
        private void LoadLastProgramMsg()
        { }
        /// <summary>
        /// 打开维修站
        /// </summary>
        private void onLoadMatainForm()
        { }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == CurSelectedItem)
            {
                return;
            }
            else
            {
                spDivide.Panel2.Controls.Clear();
                CurSelectedItem = e.ClickedItem;
                if (e.ClickedItem == tbRun)
                {
                    onLoadRunForm();
                }
                else if (e.ClickedItem == tbProgram)
                {
                    onLoadProgramForm();
                }
                else if (e.ClickedItem == tbMaintain)
                {
                    onLoadMatainForm();
                }
            }
        }
    }
}
