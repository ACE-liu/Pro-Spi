namespace SPI
{
    partial class SaveProgramDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveProgramDlg));
            this.cheSaveimg = new System.Windows.Forms.CheckBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textTypeName = new System.Windows.Forms.TextBox();
            this.textPrjName = new System.Windows.Forms.TextBox();
            this.cbCreateNew = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.zTreeView1 = new SPI.FilesTreeView();
            this.SuspendLayout();
            // 
            // cheSaveimg
            // 
            resources.ApplyResources(this.cheSaveimg, "cheSaveimg");
            this.cheSaveimg.Name = "cheSaveimg";
            this.cheSaveimg.UseVisualStyleBackColor = true;
            this.cheSaveimg.CheckedChanged += new System.EventHandler(this.cheSaveimg_CheckedChanged);
            // 
            // bCancel
            // 
            resources.ApplyResources(this.bCancel, "bCancel");
            this.bCancel.Name = "bCancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textTypeName
            // 
            resources.ApplyResources(this.textTypeName, "textTypeName");
            this.textTypeName.Name = "textTypeName";
            this.textTypeName.TextChanged += new System.EventHandler(this.textTypeName_TextChanged);
            // 
            // textPrjName
            // 
            resources.ApplyResources(this.textPrjName, "textPrjName");
            this.textPrjName.Name = "textPrjName";
            this.textPrjName.TextChanged += new System.EventHandler(this.textPrjName_TextChanged);
            // 
            // cbCreateNew
            // 
            resources.ApplyResources(this.cbCreateNew, "cbCreateNew");
            this.cbCreateNew.Name = "cbCreateNew";
            this.cbCreateNew.UseVisualStyleBackColor = true;
            this.cbCreateNew.CheckedChanged += new System.EventHandler(this.cbCreateNew_CheckedChanged);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.bOK_Click);
            // 
            // zTreeView1
            // 
            resources.ApplyResources(this.zTreeView1, "zTreeView1");
            this.zTreeView1.Name = "zTreeView1";
            // 
            // SaveProgramDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbCreateNew);
            this.Controls.Add(this.textPrjName);
            this.Controls.Add(this.textTypeName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cheSaveimg);
            this.Controls.Add(this.zTreeView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveProgramDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FilesTreeView zTreeView1;
        private System.Windows.Forms.CheckBox cheSaveimg;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textTypeName;
        private System.Windows.Forms.TextBox textPrjName;
        private System.Windows.Forms.CheckBox cbCreateNew;
        private System.Windows.Forms.Button button1;
    }
}