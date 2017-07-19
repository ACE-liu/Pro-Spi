using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static SPI.Global.Configuration;

namespace SPI
{
    /// <summary>
    /// 保存程序处理类
    /// </summary>
    public partial class SaveProgramDlg : Form
    {
        public string prjdir;
        public string strSubVer = "";
        public bool SaveImage=false;
        private SaveProgramFinisherMouseClick spfm = null;

        public SaveProgramDlg(string prjfullname)
        {
            InitializeComponent();


            string prjbase = GetPrjFileRootPath();
            if (prjbase == "") { //RmlDebug.Prompt(InternationalLanguage.请先设置文件路径); 
                return; }
            prjbase =prjbase.Substring(0, prjbase.LastIndexOf("\\"));
            this.zTreeView1.ShowTree(prjbase, this.zTreeView1);
            spfm=new SaveProgramFinisherMouseClick(zTreeView1,textTypeName,textPrjName,cbCreateNew,isDir,button1);
            zTreeView1.setMouseEventCallBackObj(spfm);
            this.zTreeView1.SetDefaultTypeAndName(getTreeNameString(prjfullname), GetPrjFileRootPath().Remove(GetPrjFileRootPath().Length - 1), ".");        
        }
        /// <summary>
        /// 输入：aa@bb.cc--->bb.cc.aa
        /// </summary>
        /// <returns></returns>
        private string getTreeNameString(string prjfn)
        {
            string prjname = prjfn.Substring(0, prjfn.LastIndexOf("@"));
            string type = prjfn.Substring(prjfn.LastIndexOf("@")+1);
            if (type != null && type != "")
            {
                return type + "." + prjname;
            }
            else
            {
                return prjname;
            }
        }
        /// <summary>
        /// 获取程序的名称
        /// </summary>
        /// <returns></returns>
        public string getPrjName(string prjFullName)
        {
            if (prjFullName.IndexOf("\\") == -1)
                return prjFullName;
            else
                return prjFullName.Substring(prjFullName.LastIndexOf("\\")+1);
        }
        /// <summary>
        /// 是否保存整图选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cheSaveimg_CheckedChanged(object sender, EventArgs e)
        {
            if (cheSaveimg.Checked)
                SaveImage = true;
            else
                SaveImage = false;
        }
        /// <summary>
        /// 选择的节点是否是目录节点
        /// </summary>
        /// <returns></returns>
        private bool isDir()
        {
            if (this.zTreeView1.getSelectNode() == null)
            {
                return false;
            }
            if (this.zTreeView1.getSelectNode().FirstNode != null)
            {
                if (this.zTreeView1.getSelectNode().FirstNode.Text.EndsWith(".sub"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取选择节点的全路径
        /// </summary>
        /// <returns></returns>
        private string getSelectPath()
        {
            return this.zTreeView1.getSelectNode().FullPath;
        }
        /// <summary>
        /// 转换为程序文件夹
        /// </summary>
        /// <param name="prjdir"></param>
        /// <returns></returns>
        private string change2PrjDir(string prjdir)
        {
             int last= prjdir.LastIndexOf("\\");
             if (last < 0)
             {
                 return prjdir + "@";
             }
             else
             {
                 string pn = prjdir.Substring(last + 1);
                 string type=prjdir.Remove(prjdir.LastIndexOf("\\"));
                 type = type.Replace("\\", ".");
                 return pn + "@" + type;
             }
        }
        /// <summary>
        /// 确认保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, EventArgs e)
        {
            textTypeName.Enabled = true;
            /*-begin-chenyi SavePrjNameTrim 20131023 */
            textPrjName.Text = textPrjName.Text.Trim();
            /*-end-chenyi SavePrjNameTrim 20131023 */
            try
            {
                ///创建程序目录
                if (isDir())
                {
                    string typedir = textTypeName.Text;
                    string filename = textPrjName.Text;
                    string fulldir = getSelectPath();
                    /*-begin xiongyanan 20140527 */
                    if (!this.judgePathAndFileExist(typedir, filename))
                    {
                        return; 
                    }
                    /*-end xiongyanan 20140527 */
                    if (!fulldir.EndsWith("\\"))
                        fulldir = fulldir + "\\";
                    string dirname = "";
                    if (!cbCreateNew.Checked)
                    {
                        dirname = fulldir + filename;
                    }
                    else
                    {
                        dirname = fulldir + typedir + "\\" + filename;
                    }

                    this.prjdir = change2PrjDir(dirname.Substring(GetPrjFileRootPath().Length));
                }
                else
                {
                    /*-begin xiongyanan 20140527 */
                    if (!this.judgePathAndFileExist(this.textTypeName.Text, this.textPrjName.Text))
                    {
                        return;
                    }
                    /*-end xiongyanan 20140527 */
                    textTypeName.Enabled = false;
                    string dir = getSelectPath();
                    dir = dir.Substring(0, dir.LastIndexOf("\\"));
                    dir = dir + "\\" + textPrjName.Text;
                    this.prjdir = change2PrjDir(dir.Substring(GetPrjFileRootPath().Length));
                }
                this.FindForm().DialogResult = DialogResult.OK;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
                //RmlDebug.Prompt(InternationalLanguage.输入错误);
            }
        }

        /// <summary>
        /// 判断输入的路径和文件名是否合理
        /// </summary>
        /// <author>xiongyanan</author>
        /// <date>20140527</date>
        /// <param name="path">路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns>true or false</returns>
        private bool judgePathAndFileExist(string path, string fileName)
        {
            if (string.Empty.Equals(path))
            {
                MessageBox.Show(InternationalLanguage.类别名称名称不能为空);
                return false;
            }

            if (string.Empty.Equals(fileName))
            {
               MessageBox.Show(InternationalLanguage.工程名称不能为空);
                return false;
            }

            return true ;
        }

        /// <summary>
        /// 设置取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textPrjName_TextChanged(object sender, EventArgs e)
        {
            InvalidName(textPrjName);
        }

        public void InvalidName(TextBox textBox)
        {
            Match Invalid = Regex.Match(textBox.Text, @"[#$@&.,|/\\:*?""'<>]");
            if (Invalid.Value != "")
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.IndexOf(Invalid.Value), 1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
        private void cbCreateNew_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCreateNew.Checked == true)
            {
                textTypeName.Text = "";
                textPrjName.Text = "";
                textTypeName.Focus();
                if (!isDir())
                {
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                }
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void textTypeName_TextChanged(object sender, EventArgs e)
        {
            if (cbCreateNew.Checked == true)
            {
                InvalidName(textTypeName);
            }
        }
    }
    /// <summary>
    /// 保存程序鼠标单击回调函数
    /// </summary>
    public class SaveProgramFinisherMouseClick : IFileTreeCallBack
    {

        private FilesTreeView ftv;
        private TextBox textTypeName, textPrjName;
        /*begin hucheng 20160301*/
        private CheckBox cb;
        private Func<bool> isDir;
        private Button btn;
        public SaveProgramFinisherMouseClick(FilesTreeView ftv, TextBox textTypeName, TextBox textPrjName, CheckBox cb, Func<bool> isDir,Button btn)
        /*end hucheng 20160301*/
        {
            this.ftv = ftv;
            this.textPrjName=textPrjName;
            this.textTypeName = textTypeName;
            /*begin hucheng 20160301*/
            this.cb = cb;
            this.isDir = isDir;
            this.btn = btn;
            /*end hucheng 20160301*/
        }
        /// <summary>
        /// 鼠标单击事件响应函数
        /// </summary>
        public void mouseLeftClickCallBack() 
        {
            /*begin hucheng 20160301*/
            if (cb.Checked == true)
            {
                if (!isDir())
                {
                    btn.Enabled = false;
                }
                else
                {
                    btn.Enabled = true;
                }
                return;
            }
            /*-begin-hucheng 20160304 */
            if (ftv.SelectNode.Text.EndsWith(".sub"))
            {
                btn.Enabled = false;
            }
            else
            {
                btn.Enabled = true;
            }
            /*-end-hucheng 20160304 */
            /*end hucheng 20160301*/
            TreeNode tn = ftv.getSelectNode();
            if (tn.Parent != null)
            {
                textTypeName.Text = tn.Parent.Text;
                textPrjName.Text = tn.Text;
            }
            else
            {
                textTypeName.Text = tn.Text;
                textPrjName.Text = "";
            }
         
        }
        public void mouseRightClickCallBack() { }
        public void afterEditNodeName(string newlablename) { }
        public void delNodeCallBack(string[] nodefullpath, string rootdir) { }
        public void mouseHoverCallBack() { }

        public bool isHoverColored()
        {
            return false;
        }

    
    }


}
