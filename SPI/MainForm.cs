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
            //tab.TabPages[0]
            //tabControl1
            TheBoard = new Board();

        }

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
                    SPIRunForm form = SPIRunForm.GetInstance();
                    form.Dock = DockStyle.Fill;
                    spDivide.Panel2.Controls.Clear();
                    spDivide.Panel2.Controls.Add(form);
                }
                else if (e.ClickedItem == tbProgram)
                {
                    SPIProgramForm form = SPIProgramForm.GetInstance();
                    form.Dock = DockStyle.Fill;
                    spDivide.Panel2.Controls.Clear();
                    spDivide.Panel2.Controls.Add(form);
                }
                else if (e.ClickedItem == tbMaintain)
                {

                }
            }
        }
    }
}
