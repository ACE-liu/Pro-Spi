﻿namespace SPI
{
    partial class EditorRangeForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbColor = new System.Windows.Forms.PictureBox();
            this.btMax = new System.Windows.Forms.Button();
            this.btMin = new System.Windows.Forms.Button();
            this.lbUnit = new System.Windows.Forms.Label();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.lbMin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            this.SuspendLayout();
            // 
            // pbColor
            // 
            this.pbColor.BackColor = System.Drawing.Color.Lime;
            this.pbColor.Location = new System.Drawing.Point(43, 28);
            this.pbColor.Margin = new System.Windows.Forms.Padding(0);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(415, 34);
            this.pbColor.TabIndex = 0;
            this.pbColor.TabStop = false;
            this.pbColor.Paint += new System.Windows.Forms.PaintEventHandler(this.pbColor_Paint);
            this.pbColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbColor_MouseDown);
            this.pbColor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbColor_MouseMove);
            this.pbColor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbColor_MouseUp);
            // 
            // btMax
            // 
            this.btMax.Location = new System.Drawing.Point(458, 28);
            this.btMax.Margin = new System.Windows.Forms.Padding(0);
            this.btMax.Name = "btMax";
            this.btMax.Size = new System.Drawing.Size(43, 34);
            this.btMax.TabIndex = 1;
            this.btMax.Text = "右";
            this.btMax.UseVisualStyleBackColor = true;
            this.btMax.Click += new System.EventHandler(this.btMax_Click);
            // 
            // btMin
            // 
            this.btMin.Location = new System.Drawing.Point(0, 28);
            this.btMin.Margin = new System.Windows.Forms.Padding(0);
            this.btMin.Name = "btMin";
            this.btMin.Size = new System.Drawing.Size(43, 34);
            this.btMin.TabIndex = 2;
            this.btMin.Text = "左";
            this.btMin.UseVisualStyleBackColor = true;
            this.btMin.Click += new System.EventHandler(this.btMin_Click);
            // 
            // lbUnit
            // 
            this.lbUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUnit.AutoSize = true;
            this.lbUnit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUnit.Location = new System.Drawing.Point(480, 4);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(19, 17);
            this.lbUnit.TabIndex = 3;
            this.lbUnit.Text = "%";
            // 
            // tbMax
            // 
            this.tbMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMax.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMax.Location = new System.Drawing.Point(442, 3);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(32, 23);
            this.tbMax.TabIndex = 4;
            this.tbMax.TextChanged += new System.EventHandler(this.tbMax_TextChanged);
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMin.Location = new System.Drawing.Point(14, 4);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(19, 17);
            this.lbMin.TabIndex = 5;
            this.lbMin.Text = "%";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(342, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "OK";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(391, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "WARN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(440, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 22);
            this.label3.TabIndex = 9;
            this.label3.Text = "NG";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EditorRangeForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMin);
            this.Controls.Add(this.tbMax);
            this.Controls.Add(this.lbUnit);
            this.Controls.Add(this.btMin);
            this.Controls.Add(this.btMax);
            this.Controls.Add(this.pbColor);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.Name = "EditorRangeForm";
            this.Size = new System.Drawing.Size(504, 111);
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbColor;
        private System.Windows.Forms.Button btMax;
        private System.Windows.Forms.Button btMin;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
