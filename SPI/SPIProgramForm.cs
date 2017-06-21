﻿using System;
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
    public partial class SPIProgramForm : UserControl
    {
        private static SPIProgramForm singleProForm = null;
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
    }
}
