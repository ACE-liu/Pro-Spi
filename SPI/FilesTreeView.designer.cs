namespace SPI
{
    partial class FilesTreeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilesTreeView));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(399, 424);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.treeView1_NodeMouseHover);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            // 
            // FilesTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.treeView1);
            this.Name = "FilesTreeView";
            this.Size = new System.Drawing.Size(399, 424);
            this.ResumeLayout(false);

        }
        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        //private void InitializeComponent(bool Status)
        //{
        //    this.components = new System.ComponentModel.Container();
        //    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilesTreeView));
        //    this.treeView1 = new System.Windows.Forms.TreeView();
        //    this.imageList = new System.Windows.Forms.ImageList(this.components);
        //    this.SuspendLayout();
        //    // 
        //    // treeView1
        //    // 
        //    this.treeView1.BackColor = System.Drawing.SystemColors.Window;
        //    this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        //    this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
        //    this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        //    this.treeView1.ItemHeight = 20;
        //    this.treeView1.Location = new System.Drawing.Point(0, 0);
        //    this.treeView1.Name = "treeView1";
        //    this.treeView1.Size = new System.Drawing.Size(399, 424);
        //    this.treeView1.TabIndex = 0;
        //    this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
        //    this.treeView1.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.treeView1_NodeMouseHover);
        //    this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
        //    // 
        //    // imageList
        //    // 
        //    this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
        //    this.imageList.TransparentColor = System.Drawing.Color.Transparent;
        //    this.imageList.Images.SetKeyName(0, "");
        //    this.imageList.Images.SetKeyName(1, "");
        //    // 
        //    // FilesTreeView
        //    // 
        //    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.AutoSize = true;
        //    this.Controls.Add(this.treeView1);
        //    this.Name = "FilesTreeView";
        //    this.Size = new System.Drawing.Size(399, 424);
        //    this.ResumeLayout(false);

        //}

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TreeView treeView1;

        public System.Windows.Forms.TreeView TreeView1
        {
            get { return treeView1; }
        }

    }
}
