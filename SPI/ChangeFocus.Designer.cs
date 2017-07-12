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
            this.btMove = new System.Windows.Forms.Button();
            this.btResize = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btMove
            // 
            this.btMove.Dock = System.Windows.Forms.DockStyle.Top;
            this.btMove.FlatAppearance.BorderSize = 0;
            this.btMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMove.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btMove.Location = new System.Drawing.Point(0, 0);
            this.btMove.Margin = new System.Windows.Forms.Padding(0);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(106, 35);
            this.btMove.TabIndex = 1;
            this.btMove.Text = "坐标变化";
            this.btMove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // btResize
            // 
            this.btResize.Dock = System.Windows.Forms.DockStyle.Top;
            this.btResize.FlatAppearance.BorderSize = 0;
            this.btResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btResize.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btResize.Location = new System.Drawing.Point(0, 35);
            this.btResize.Margin = new System.Windows.Forms.Padding(0);
            this.btResize.Name = "btResize";
            this.btResize.Size = new System.Drawing.Size(106, 35);
            this.btResize.TabIndex = 9;
            this.btResize.Text = "尺寸变化";
            this.btResize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btResize.UseVisualStyleBackColor = true;
            this.btResize.Click += new System.EventHandler(this.btResize_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(0, 70);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 35);
            this.button1.TabIndex = 10;
            this.button1.Text = "取消";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChangeFocus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(106, 106);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btResize);
            this.Controls.Add(this.btMove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeFocus";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btResize;
        private System.Windows.Forms.Button button1;
    }
}
