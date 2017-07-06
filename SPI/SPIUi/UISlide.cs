﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPI.SPICheckWin;

using static SPI.Global.Configuration;

namespace SPI.SPIUi
{
    class UISlide:UIBase
    {
        Panel ui;
        Label testItem;
        EditorRangeForm slideControl;
        public string text;
        private bool isLargeValueOk;
        private string backUnit;//单位% um

        
        private double minValue;
        private double leftValue;
        private double rightValue;
        private double maxValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="win"></param>
        /// <param name="lvl"></param>
        /// <param name="text">text==null 表示不添加label</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="isLargeValueOk"></param>
        public UISlide(CheckWinBase win ,string backUnit,int lvl ,string text ,double min ,double max,bool isLargeValueOk):base(win,lvl)
        {
            this.text = text;
            this.backUnit = backUnit;
            this.isLargeValueOk = isLargeValueOk;
            this.minValue = min;
            this.maxValue = max;
        }
        public override void DisposeUIManual()
        {
            if (null != this.slideControl)
            {
                this.slideControl.RemoveAllDelegate();
                this.slideControl.Dispose();
                this.slideControl = null;
            }
            if (null!=ui)
            {
                ui.Dispose();ui = null;
            }
        }
        public override Control CreateUI()
        {
            ui = new Panel();
            ui.SuspendLayout();
            ui.Font = CurFocusPanel?.Font;
            ui.AutoSize = true;
            if (text!=null)
            {
                testItem = new Label(); 
                testItem.Text = text;
                testItem.Left = ControlsGap;
                //lbPre.Width = ui.Width - 250;//185 pixels for left location fixed controls
                testItem.AutoSize = true;
                ui.Controls.Add(testItem);
            }
            slideControl = new EditorRangeForm(backUnit, minValue, maxValue, leftValue, rightValue, isLargeValueOk);
            slideControl.DataChanged += SlideControl_DataChanged;
            if (testItem != null)
            {
                slideControl.Top = testItem.Bottom + ControlsGap;
            }
            else
                slideControl.Top = ControlsGap;
            slideControl.Left = ControlsGap;
            ui.Controls.Add(slideControl);
            return ui;

        }

        private void SlideControl_DataChanged(object sender, EventArgs e)
        {
            this.minValue = slideControl.Min;
            this.maxValue = slideControl.Max;
            this.leftValue = slideControl.Left;
            this.rightValue = slideControl.Right;
        }
    }
}