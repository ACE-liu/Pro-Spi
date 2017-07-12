namespace SPI
{
    partial class MultiColors
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
      
              
        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiColors));
            this.checkBoxGray = new System.Windows.Forms.CheckBox();
            this.StateButton = new System.Windows.Forms.CheckBox();
            this.checkBoxRed = new System.Windows.Forms.CheckBox();
            this.checkBoxGreen = new System.Windows.Forms.CheckBox();
            this.checkBoxBlue = new System.Windows.Forms.CheckBox();
            this.operationList = new System.Windows.Forms.ComboBox();
            this.ColorsList = new System.Windows.Forms.ComboBox();
            this.DelButton = new System.Windows.Forms.Button();
            this.cbColPenSize = new System.Windows.Forms.ComboBox();
            this.cbUseColorPen = new System.Windows.Forms.CheckBox();
            this.btRollback = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.GrayPic = new System.Windows.Forms.PictureBox();
            this.ColorPic = new System.Windows.Forms.PictureBox();
            this.tbDensity = new System.Windows.Forms.TrackBar();
            this.lbDensity = new System.Windows.Forms.Label();
            this.txDensity = new System.Windows.Forms.TextBox();
            this.GrayEditor = new SPI.RangeEditor();
            this.BlueEditor = new SPI.RangeEditor();
            this.GreenEditor = new SPI.RangeEditor();
            this.RedEditor = new SPI.RangeEditor();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrayPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDensity)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxGray
            // 
            resources.ApplyResources(this.checkBoxGray, "checkBoxGray");
            this.checkBoxGray.Name = "checkBoxGray";
            this.checkBoxGray.UseVisualStyleBackColor = true;
            this.checkBoxGray.CheckedChanged += new System.EventHandler(this.checkBoxGray_CheckedChanged);
            // 
            // StateButton
            // 
            resources.ApplyResources(this.StateButton, "StateButton");
            this.StateButton.Name = "StateButton";
            this.StateButton.UseVisualStyleBackColor = true;
            this.StateButton.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBoxRed
            // 
            resources.ApplyResources(this.checkBoxRed, "checkBoxRed");
            this.checkBoxRed.Name = "checkBoxRed";
            this.checkBoxRed.UseVisualStyleBackColor = true;
            this.checkBoxRed.CheckedChanged += new System.EventHandler(this.checkBoxRed_CheckedChanged);
            // 
            // checkBoxGreen
            // 
            resources.ApplyResources(this.checkBoxGreen, "checkBoxGreen");
            this.checkBoxGreen.Name = "checkBoxGreen";
            this.checkBoxGreen.UseVisualStyleBackColor = true;
            this.checkBoxGreen.CheckedChanged += new System.EventHandler(this.checkBoxGreen_CheckedChanged);
            // 
            // checkBoxBlue
            // 
            resources.ApplyResources(this.checkBoxBlue, "checkBoxBlue");
            this.checkBoxBlue.Name = "checkBoxBlue";
            this.checkBoxBlue.UseVisualStyleBackColor = true;
            this.checkBoxBlue.CheckedChanged += new System.EventHandler(this.checkBoxBlue_CheckedChanged);
            // 
            // operationList
            // 
            this.operationList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.operationList, "operationList");
            this.operationList.FormattingEnabled = true;
            this.operationList.Items.AddRange(new object[] {
            resources.GetString("operationList.Items"),
            resources.GetString("operationList.Items1"),
            resources.GetString("operationList.Items2")});
            this.operationList.Name = "operationList";
            this.operationList.SelectedIndexChanged += new System.EventHandler(this.operationList_SelectedIndexChanged);
            // 
            // ColorsList
            // 
            this.ColorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ColorsList, "ColorsList");
            this.ColorsList.FormattingEnabled = true;
            this.ColorsList.Name = "ColorsList";
            this.ColorsList.SelectedIndexChanged += new System.EventHandler(this.ColorsList_SelectedIndexChanged);
            // 
            // DelButton
            // 
            resources.ApplyResources(this.DelButton, "DelButton");
            this.DelButton.Name = "DelButton";
            this.DelButton.UseVisualStyleBackColor = true;
            this.DelButton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // cbColPenSize
            // 
            resources.ApplyResources(this.cbColPenSize, "cbColPenSize");
            this.cbColPenSize.FormattingEnabled = true;
            this.cbColPenSize.Items.AddRange(new object[] {
            resources.GetString("cbColPenSize.Items"),
            resources.GetString("cbColPenSize.Items1"),
            resources.GetString("cbColPenSize.Items2"),
            resources.GetString("cbColPenSize.Items3")});
            this.cbColPenSize.Name = "cbColPenSize";
            this.cbColPenSize.Sorted = true;
            this.cbColPenSize.TabStop = false;
            // 
            // cbUseColorPen
            // 
            resources.ApplyResources(this.cbUseColorPen, "cbUseColorPen");
            this.cbUseColorPen.Name = "cbUseColorPen";
            this.cbUseColorPen.UseVisualStyleBackColor = true;
            this.cbUseColorPen.CheckedChanged += new System.EventHandler(this.cbUseColorPen_CheckedChanged);
            // 
            // btRollback
            // 
            resources.ApplyResources(this.btRollback, "btRollback");
            this.btRollback.Name = "btRollback";
            this.btRollback.UseVisualStyleBackColor = true;
            this.btRollback.Click += new System.EventHandler(this.btRollback_Click);
            // 
            // btClear
            // 
            resources.ApplyResources(this.btClear, "btClear");
            this.btClear.Name = "btClear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SPI.Properties.Resources.cursor;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // GrayPic
            // 
            this.GrayPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.GrayPic, "GrayPic");
            this.GrayPic.Name = "GrayPic";
            this.GrayPic.TabStop = false;
            this.GrayPic.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.GrayPic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.GrayPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.GrayPic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // ColorPic
            // 
            this.ColorPic.BackgroundImage = global::SPI.Properties.Resources.red;
            resources.ApplyResources(this.ColorPic, "ColorPic");
            this.ColorPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorPic.Name = "ColorPic";
            this.ColorPic.TabStop = false;
            this.ColorPic.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.ColorPic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorPic_MouseDown);
            // 
            // tbDensity
            // 
            resources.ApplyResources(this.tbDensity, "tbDensity");
            this.tbDensity.Maximum = 20;
            this.tbDensity.Name = "tbDensity";
            this.tbDensity.TickFrequency = 2;
            this.tbDensity.Scroll += new System.EventHandler(this.tbDensity_Scroll);
            // 
            // lbDensity
            // 
            resources.ApplyResources(this.lbDensity, "lbDensity");
            this.lbDensity.Name = "lbDensity";
            // 
            // txDensity
            // 
            resources.ApplyResources(this.txDensity, "txDensity");
            this.txDensity.Name = "txDensity";
            this.txDensity.TextChanged += new System.EventHandler(this.txDensity_TextChanged);
            // 
            // GrayEditor
            // 
            this.GrayEditor.BackColor = System.Drawing.SystemColors.Control;
            this.GrayEditor.BackgroundColor = System.Drawing.Color.Gray;
            this.GrayEditor.Down = 33;
            resources.ApplyResources(this.GrayEditor, "GrayEditor");
            this.GrayEditor.Maximum = 255;
            this.GrayEditor.Minimum = 0;
            this.GrayEditor.Name = "GrayEditor";
            this.GrayEditor.SelectedColor = System.Drawing.Color.White;
            this.GrayEditor.Up = 255;
            this.GrayEditor.Notify += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor4_Notify);
            this.GrayEditor.GetFocus += new SPI.RangeEditor.NotifyEventHandler(this.GrayEditor_GetFocus);
            // 
            // BlueEditor
            // 
            this.BlueEditor.BackColor = System.Drawing.SystemColors.Control;
            this.BlueEditor.BackgroundColor = System.Drawing.Color.DarkBlue;
            this.BlueEditor.Down = 33;
            resources.ApplyResources(this.BlueEditor, "BlueEditor");
            this.BlueEditor.Maximum = 100;
            this.BlueEditor.Minimum = 0;
            this.BlueEditor.Name = "BlueEditor";
            this.BlueEditor.SelectedColor = System.Drawing.Color.Blue;
            this.BlueEditor.Up = 100;
            this.BlueEditor.Notify += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor3_Notify);
            this.BlueEditor.GetFocus += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor3_GetFocus);
            // 
            // GreenEditor
            // 
            this.GreenEditor.BackColor = System.Drawing.SystemColors.Control;
            this.GreenEditor.BackgroundColor = System.Drawing.Color.DarkGreen;
            this.GreenEditor.Down = 33;
            resources.ApplyResources(this.GreenEditor, "GreenEditor");
            this.GreenEditor.Maximum = 100;
            this.GreenEditor.Minimum = 0;
            this.GreenEditor.Name = "GreenEditor";
            this.GreenEditor.SelectedColor = System.Drawing.Color.Green;
            this.GreenEditor.Up = 100;
            this.GreenEditor.Notify += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor2_Notify);
            this.GreenEditor.GetFocus += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor2_GetFocus);
            // 
            // RedEditor
            // 
            this.RedEditor.BackColor = System.Drawing.SystemColors.Control;
            this.RedEditor.BackgroundColor = System.Drawing.Color.DarkRed;
            this.RedEditor.Down = 33;
            resources.ApplyResources(this.RedEditor, "RedEditor");
            this.RedEditor.Maximum = 100;
            this.RedEditor.Minimum = 0;
            this.RedEditor.Name = "RedEditor";
            this.RedEditor.SelectedColor = System.Drawing.Color.Red;
            this.RedEditor.Up = 100;
            this.RedEditor.Notify += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor1_Notify);
            this.RedEditor.GetFocus += new SPI.RangeEditor.NotifyEventHandler(this.rangeEditor1_GetFocus);
            // 
            // MultiColors
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txDensity);
            this.Controls.Add(this.lbDensity);
            this.Controls.Add(this.tbDensity);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.btRollback);
            this.Controls.Add(this.cbUseColorPen);
            this.Controls.Add(this.cbColPenSize);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DelButton);
            this.Controls.Add(this.ColorsList);
            this.Controls.Add(this.operationList);
            this.Controls.Add(this.checkBoxBlue);
            this.Controls.Add(this.checkBoxGreen);
            this.Controls.Add(this.checkBoxRed);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.GrayPic);
            this.Controls.Add(this.GrayEditor);
            this.Controls.Add(this.StateButton);
            this.Controls.Add(this.checkBoxGray);
            this.Controls.Add(this.ColorPic);
            this.Controls.Add(this.BlueEditor);
            this.Controls.Add(this.GreenEditor);
            this.Controls.Add(this.RedEditor);
            resources.ApplyResources(this, "$this");
            this.Name = "MultiColors";
            this.EnabledChanged += new System.EventHandler(this.MultiColors_EnabledChanged);
            this.Enter += new System.EventHandler(this.MultiColors_Enter);
            this.Leave += new System.EventHandler(this.MultiColors_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MultiColors_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrayPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RangeEditor RedEditor;
        private RangeEditor GreenEditor;
        private RangeEditor BlueEditor;
        private System.Windows.Forms.PictureBox ColorPic;
        private System.Windows.Forms.CheckBox checkBoxGray;
        private System.Windows.Forms.CheckBox StateButton;
        private RangeEditor GrayEditor;
        private System.Windows.Forms.PictureBox GrayPic;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.CheckBox checkBoxRed;
        private System.Windows.Forms.CheckBox checkBoxGreen;
        private System.Windows.Forms.CheckBox checkBoxBlue;
        private System.Windows.Forms.ComboBox operationList;
        private System.Windows.Forms.ComboBox ColorsList;
        private System.Windows.Forms.Button DelButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbColPenSize;
        private System.Windows.Forms.CheckBox cbUseColorPen;
        private System.Windows.Forms.Button btRollback;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.TrackBar tbDensity;
        private System.Windows.Forms.Label lbDensity;
        private System.Windows.Forms.TextBox txDensity;
    }
}

