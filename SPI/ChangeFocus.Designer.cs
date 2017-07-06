namespace SPI
{
    partial class ChangeFocus
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
            this.btHor = new System.Windows.Forms.Button();
            this.nbMovehorizon = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.nbSizeDown = new System.Windows.Forms.NumericUpDown();
            this.button7 = new System.Windows.Forms.Button();
            this.nbSizeUp = new System.Windows.Forms.NumericUpDown();
            this.btSize = new System.Windows.Forms.Button();
            this.nbVertical = new System.Windows.Forms.NumericUpDown();
            this.btve = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nbMovehorizon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbSizeDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbSizeUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbVertical)).BeginInit();
            this.SuspendLayout();
            // 
            // btHor
            // 
            this.btHor.FlatAppearance.BorderSize = 0;
            this.btHor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btHor.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btHor.Location = new System.Drawing.Point(0, 0);
            this.btHor.Margin = new System.Windows.Forms.Padding(0);
            this.btHor.Name = "btHor";
            this.btHor.Size = new System.Drawing.Size(105, 35);
            this.btHor.TabIndex = 1;
            this.btHor.Text = "水平移动";
            this.btHor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btHor.UseVisualStyleBackColor = true;
            this.btHor.Click += new System.EventHandler(this.btHor_Click);
            // 
            // nbMovehorizon
            // 
            this.nbMovehorizon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nbMovehorizon.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nbMovehorizon.Location = new System.Drawing.Point(105, 0);
            this.nbMovehorizon.Margin = new System.Windows.Forms.Padding(0);
            this.nbMovehorizon.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nbMovehorizon.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nbMovehorizon.Name = "nbMovehorizon";
            this.nbMovehorizon.Size = new System.Drawing.Size(65, 33);
            this.nbMovehorizon.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(0, 139);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(170, 38);
            this.button3.TabIndex = 15;
            this.button3.Text = "取消";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // nbSizeDown
            // 
            this.nbSizeDown.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nbSizeDown.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nbSizeDown.Location = new System.Drawing.Point(103, 104);
            this.nbSizeDown.Margin = new System.Windows.Forms.Padding(0);
            this.nbSizeDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nbSizeDown.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nbSizeDown.Name = "nbSizeDown";
            this.nbSizeDown.Size = new System.Drawing.Size(67, 31);
            this.nbSizeDown.TabIndex = 14;
            // 
            // button7
            // 
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.Location = new System.Drawing.Point(0, 104);
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(105, 35);
            this.button7.TabIndex = 13;
            this.button7.Text = "高度变化";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // nbSizeUp
            // 
            this.nbSizeUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nbSizeUp.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nbSizeUp.Location = new System.Drawing.Point(105, 69);
            this.nbSizeUp.Margin = new System.Windows.Forms.Padding(0);
            this.nbSizeUp.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nbSizeUp.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nbSizeUp.Name = "nbSizeUp";
            this.nbSizeUp.Size = new System.Drawing.Size(65, 35);
            this.nbSizeUp.TabIndex = 12;
            // 
            // btSize
            // 
            this.btSize.FlatAppearance.BorderSize = 0;
            this.btSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSize.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSize.Location = new System.Drawing.Point(-1, 69);
            this.btSize.Margin = new System.Windows.Forms.Padding(0);
            this.btSize.Name = "btSize";
            this.btSize.Size = new System.Drawing.Size(105, 35);
            this.btSize.TabIndex = 11;
            this.btSize.Text = "长度变化";
            this.btSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSize.UseVisualStyleBackColor = true;
            this.btSize.Click += new System.EventHandler(this.btSize_Click);
            // 
            // nbVertical
            // 
            this.nbVertical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nbVertical.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nbVertical.Location = new System.Drawing.Point(105, 33);
            this.nbVertical.Margin = new System.Windows.Forms.Padding(0);
            this.nbVertical.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nbVertical.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nbVertical.Name = "nbVertical";
            this.nbVertical.Size = new System.Drawing.Size(65, 35);
            this.nbVertical.TabIndex = 10;
            // 
            // btve
            // 
            this.btve.FlatAppearance.BorderSize = 0;
            this.btve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btve.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btve.Location = new System.Drawing.Point(0, 34);
            this.btve.Margin = new System.Windows.Forms.Padding(0);
            this.btve.Name = "btve";
            this.btve.Size = new System.Drawing.Size(105, 35);
            this.btve.TabIndex = 9;
            this.btve.Text = "上下移动";
            this.btve.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btve.UseVisualStyleBackColor = true;
            this.btve.Click += new System.EventHandler(this.btve_Click);
            // 
            // ChangeFocus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(171, 177);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.nbSizeDown);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.nbSizeUp);
            this.Controls.Add(this.btSize);
            this.Controls.Add(this.nbVertical);
            this.Controls.Add(this.btve);
            this.Controls.Add(this.nbMovehorizon);
            this.Controls.Add(this.btHor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeFocus";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChangeFocus_FormClosed);
            this.Load += new System.EventHandler(this.ChangeFocus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nbMovehorizon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbSizeDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbSizeUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbVertical)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btHor;
        private System.Windows.Forms.NumericUpDown nbMovehorizon;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown nbSizeDown;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.NumericUpDown nbSizeUp;
        private System.Windows.Forms.Button btSize;
        private System.Windows.Forms.NumericUpDown nbVertical;
        private System.Windows.Forms.Button btve;
    }
}
