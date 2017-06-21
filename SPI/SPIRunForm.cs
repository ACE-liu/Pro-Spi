using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPI
{
    public partial class SPIRunForm : UserControl
    {
        private static SPIRunForm singleRunForm = null;
        private SPIRunForm()
        {
            InitializeComponent();
        }
        public static SPIRunForm GetInstance()
        {
            if (singleRunForm==null)
            {
                singleRunForm = new SPIRunForm();
            }
            return singleRunForm;
        }
    }
}
