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

        private void button3_Click(object sender, EventArgs e)
        {
            singletonForm?.Hide();
        }

        private void btHor_Click(object sender, EventArgs e)
        {
            int value = decimal.ToInt32(nbMovehorizon.Value);
            if (value != 0)
            {
                CurFocus?.Move(value, 0);
                dataChanged = true;
            }
            this.Close();
        }

        private void btve_Click(object sender, EventArgs e)
        {
            int value = decimal.ToInt32(nbVertical.Value);
            if (value != 0)
            {
                CurFocus?.Move(0, value);
                dataChanged = true;
            }
            Close();
        }

        private void ChangeFocus_Load(object sender, EventArgs e)
        {
            initialize();
        }
        private void initialize()
        {
            dataChanged = false;
        }

        private void btSize_Click(object sender, EventArgs e)
        {
            int value = decimal.ToInt32(nbSizeUp.Value);
            if (value != 0)
            {
                dataChanged = true;
                CurFocus?.ResizeAroundCenter(value, 0);
            }
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int value = decimal.ToInt32(nbSizeDown.Value);
            if (value != 0)
            {
                dataChanged = true;
                CurFocus?.ResizeAroundCenter(0, value);
            }
            Close();
        }

        private void ChangeFocus_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dataChanged)
            {
                theMarkPicture.OnDataChanged();
            }
        }
    }
}
