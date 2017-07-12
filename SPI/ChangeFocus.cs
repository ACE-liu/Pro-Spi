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
    public partial class ChangeFocus : Form
    {
        bool dataChanged = false;
        private static ChangeFocus singletonForm = null;
        public ChangeFocus()
        {
            InitializeComponent();
        }
        public static ChangeFocus CreateInstance()
        {
            if (singletonForm == null||singletonForm.IsDisposed)
            {
                singletonForm = new ChangeFocus();
            }
            return singletonForm;
        }


        private void btMove_Click(object sender, EventArgs e)
        {
            CurFocus.CanMove = true;
            Hide();
        }

        private void btResize_Click(object sender, EventArgs e)
        {
            CurFocus.CanResize = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            singletonForm?.Hide();
        }
    }
}
