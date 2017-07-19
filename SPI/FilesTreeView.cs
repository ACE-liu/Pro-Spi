using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using static SPI.Global.Configuration;

namespace SPI
{
    /// <summary>
    /// 文件树类，实现文件夹、文件的拷贝、删除、重命名、检索等功能
    /// </summary>
    public partial class FilesTreeView : UserControl
    {
        /// <summary>
        /// 当前选择的节点
        /// </summary>
        private TreeNode selectNode = null;
        /*begin hucheng 20160301*/
        public TreeNode SelectNode { get { return selectNode; } }
        public string selectSubPrjName { get { if (selectNode.Text.EndsWith(".sub"))return selectNode.Text; else { return ""; } } }
        /*end hucheng 20160301*/
        private TreeNode stopNode = null;
        
        /// <summary>
        /// FilesTreeView 包含在新建Model中时，设置为true
        /// </summary>
        public IFileTreeCallBack ftCallBack=null;
                 
        public FilesTreeView()
        {
            InitializeComponent();
        }
        //为什么会生成这个有参构造函数？
        //public FilesTreeView(bool status)
        //{
        //    InitializeComponent(status);
        //}
        /// <summary>
        /// 查找树中是否有指定的节点
        /// </summary>
        /// <param name="tnParent"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private TreeNode FindNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strValue && tnParent.FirstNode==null)
            {
                return tnParent;
            }
            TreeNode tnRet = null;
         
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNode(tn, strValue);
                if (tnRet != null) break;
            }
            return tnRet;
        }
        /// <summary>
        /// 查找整个树中是否有指定的节点
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public TreeNode FindNode(string strValue)
        {
            if (this.treeView1.Nodes.Count < 1)
                return null;
            return FindNode(this.treeView1.Nodes[0], strValue);
        }
        /*-end-chenyi SaveModelDefaultNodeSet 20131008 */

        /// <summary>
        /// 设置鼠标单击回调函数
        /// </summary>
        /// <param name="ftCallBack"></param>
        public void setMouseEventCallBackObj(IFileTreeCallBack ftCallBack)
        {
            this.ftCallBack = ftCallBack;
        }
        /// <summary>
        /// 添加节点到树
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            this.treeView1.Nodes.Add(node);
        }

        /// <summary>
        /// 设置节点的默认名称nodetype 是以split分割的字符串
        /// </summary>
        /// <param name="nodefullname"></param>
        public void SetDefaultTypeAndName(string nodetype, string rootname, string split)
        {
           
            if (nodetype == "" || rootname == "")
                return;
            string[] splitstr = null;
            if (nodetype.IndexOf(split) != -1)
            {
                splitstr = nodetype.Split(split.ToCharArray());
            }
            else
            {
                splitstr = new string[1] { nodetype };
            }
            if (this.treeView1.Nodes.Count <= 0)
                return;
            TreeNode root = this.treeView1.Nodes[0];
            while (root.Parent != null)
            {
                root = root.Parent;
            }
            if (root.Text != rootname)
            {
                return;
            }
            TreeNode selectnode = root;
            foreach (string onename in splitstr)
            {
                bool find = false;
                foreach (TreeNode node in selectnode.Nodes)
                {
                    if (node.Text != onename)
                    {
                        continue;
                    }
                    find = true;
                    selectnode = node;
                    break;
                }
                if (find)
                {
                    continue;
                }
                else
                {
                    return;
                }
            }
            setNodeInfo(selectnode);
            
            ftCallBack.mouseLeftClickCallBack();
            TreeNode exnode = selectnode;
            while (exnode != null)
            {
                exnode.Expand();
                exnode = exnode.Parent;
            }

        }
        public void clearSelectNode() 
        {
            this.selectNode = null;
        }
        
        /// <summary>
        /// 获取选中的节点，如果是子程序，则返回它的母程序
        /// </summary>
        /// <returns></returns>
        public TreeNode getSelectNode()
        {
            /*-begin hucheng 20160301 */
            if (this.selectNode != null)
            {
                if (this.selectNode.Text.EndsWith(".sub"))
                {
                    return this.selectNode.Parent;
                }
            }
            /*-end hucheng 20160301 */ 
            return this.selectNode;
        }

        public TreeNode getStopNode()
        {
            return this.stopNode;
        }
        /// <summary>
        /// 设置树形节点可以编辑 
        /// </summary>
        /// <param name="tf"></param>
        public void setLableEdit(bool tf)
        {
            this.treeView1.LabelEdit = tf;
        }
        /// <summary>
        /// 清理树的所有节点
        /// </summary>
        public void ClearAll()
        {
            this.treeView1.Nodes.Clear();
        }
        /// <summary>
        /// 获取含有shstr字符串的目录名称
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="pattern"></param>
        /// <param name="retstr"></param>
        private static  void getPathString(string[] dir, string shstr, List<string> retstr)
        {
            for (int i = 0; i < dir.Length; i++)
            {
                string str = dir[i].Substring(dir[i].LastIndexOf("\\"));
                if (str.IndexOf(shstr) != -1)
                    retstr.Add(dir[i]);
            }
            return;
        }
        /// <summary>
        /// 在dir下获取含有patttern的文件名称
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="patttern"></param>
        /// <param name="retstr"></param>
        private static void GetCurDirALlFiles(string dir, string[] patttern, List<string> retstr)
        {
            string[] allfiles = Directory.GetFiles(dir);
            foreach (string onefile in allfiles)
            {
                string s = onefile.Substring(onefile.LastIndexOf("\\") + 1);
                int i;
                for (i = 0; i < patttern.Length; i++)
                {
                    if (s.IndexOf(patttern[i]) == -1)
                        break;
                }
                if (i > patttern.Length - 1)
                    retstr.Add(onefile);
            }
        }

        /// <summary>
        /// 查找指定目录下是否有搜索的文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="patttern"></param>
        /// <returns></returns>
        private static bool IsContailCertainFiles(string dir, string[] patttern)
        {
            string[] allfiles = Directory.GetFiles(dir);
            foreach (string onefile in allfiles)
            {
                string s = onefile.Substring(onefile.LastIndexOf("\\") + 1);
                int i;
                for (i = 0; i < patttern.Length; i++)
                {
                    if (s.IndexOf(patttern[i]) == -1)
                        break;
                }
                if (i > patttern.Length - 1)
                    return true;
            }

            string[] alldir = Directory.GetDirectories(dir);
            foreach (string onedir in alldir)
            {
                if (IsContailCertainFiles(onedir, patttern))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// 指定目录下是否有指定目录
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="patttern"></param>
        /// <returns></returns>
        private static bool IsContailCertainDir(string dir, string[] patttern)
        {
            string[] alldirs = Directory.GetDirectories(dir);
            foreach (string onedir in alldirs)
            {
                string s = onedir.Substring(onedir.LastIndexOf("\\") + 1);
                int i;
                for (i = 0; i < patttern.Length; i++)
                {
                    if (s.IndexOf(patttern[i]) == -1)
                        break;
                }
                if (i > patttern.Length - 1)
                    return true;

                if (IsContailCertainDir(onedir, patttern))
                    return true;
            }

            return false;

        }
        /// <summary>
        /// 获取目录下的所有目录
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private string[] getAllDirs(string dir)
        {
            string[] alldir = Directory.GetDirectories(dir);
            return alldir;
        }
        /// <summary>
        /// 检索节点下的子节点
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newnode"></param>
        /// <returns></returns>
        private static bool isContainSub(TreeNode root ,TreeNode newnode)
        {
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Text == newnode.Text)
                {
                    return true;
                }
            
            }
            return false;
        }
        /// <summary>
        ///  从树节点中移除指定名称节点
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        private void delSubNode(TreeNode root, TreeNode sub)
        {
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Text == sub.Text)
                {
                    node.Remove();
                    return;
                }
            }
        }
        /// <summary>
        /// 添加新节点到树中
        /// </summary>
        /// <param name="node"></param>
        public void AddNewNode(TreeNode node)
        {
            try
            {
                TreeNode newone = (TreeNode)node.Clone();
                if (!isContainSub(this.selectNode, newone))
                {
                    selectNode.Nodes.Add(newone);
                }
                else
                {
                    delSubNode(this.selectNode, newone);
                    selectNode.Nodes.Add(newone);
                }
                if (selectNode.FirstNode.Text == "")
                {
                    selectNode.FirstNode.Remove();
                }
            }
            catch (Exception ex)
            {
                //RmlDebug.LogLine(ex.StackTrace);
            }
            return;
        
        }
        /// <summary>
        /// 获取路径名称
        /// </summary>
        /// <param name="point"></param>
        public string GetSelectPath(Point point,string basepath,string endstr)
        {
           TreeNode  tn=  this.treeView1.GetNodeAt(point);
           string node = (string)tn.Tag;
           if (tn.FirstNode != null)//获取目录下的所有文件
           {
               string dir = basepath + "\\" + node;
               return dir;
           }
           else
           {
               string file = basepath + "\\" + node + endstr;
               return file;
           }
        }
        /// <summary>
        /// 是否有重名的文件？
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool hasSameName(TreeNode node,string name)
        {
            if (node.Parent == null)
                return false;
            foreach (TreeNode nd in node.Parent.Nodes)
            {
                if (nd.Text == name)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 判断是否是合法的节点名称
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private  bool isValidName(TreeNode node,string name)
        {
            if (name.Length < 0
                || name.IndexOfAny(new char[] { '@', ',', '!' }) != -1
                || hasSameName(node, name)
                )
                return false;

            return true;
        }
        /// <summary>
        /// 修改树形节点的名称。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (!isValidName(e.Node, e.Label) || !IsValidName(e.Label))
                {
                    e.CancelEdit = true;
                    if (e.Label != e.Node.Text)
                    {
                        if (hasSameName(e.Node, e.Label))
                            MessageBox.Show("程序名已存在"); //
                        else
                            MessageBox.Show(InternationalLanguage.名称非法请使用其他的名称);
                    }
                    e.Node.EndEdit(false);
                    return;
                }
                ftCallBack.afterEditNodeName(e.Label.ToString());             
            }
            this.treeView1.LabelEdit = false;
        }
       /// <summary>
       /// 获取选中节点的信息
       /// </summary>
       /// <param name="tn"></param>
        private void setNodeInfo(TreeNode tn)
        {
            string path = (string)tn.Text;
            if (selectNode !=null)
            {
                selectNode.BackColor = Color.White;
                selectNode.ForeColor = Color.Black;
            }
            selectNode = tn;
            selectNode.BackColor = Color.Orange;
        }

        private void setStopNodeInfo(TreeNode tn)
        {
            string path = (string)tn.Text;
            if (stopNode != null)
            {
                stopNode.BackColor = Color.White;
            }
            stopNode = tn;
            stopNode.BackColor = Color.Orange;
        }
        /// <summary>
        /// 处理鼠标停顿时,节点展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            TreeNode tn = e.Node;
            if (null != ftCallBack)
            {
                if (ftCallBack.isHoverColored())
                {
                    setStopNodeInfo(tn);

                    ftCallBack.mouseHoverCallBack();
                }
            }
        }
        /// <summary>
        /// 树形节点点击相应事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void treeView1_NodeMouseClick(object sender,TreeNodeMouseClickEventArgs e)
       {
           TreeNode tn = e.Node;
           if (tn == null || tn.Text == "" || tn.Text == null)
               return;
           setNodeInfo(tn);
           if (e.Button == System.Windows.Forms.MouseButtons.Right)
           {
               if (null != ftCallBack)
               {
                   ftCallBack.mouseRightClickCallBack();
                   ftCallBack.mouseHoverCallBack();
               }
           }
           else
           {
               if (null != ftCallBack)
               {
                   ftCallBack.mouseLeftClickCallBack();
                   ftCallBack.mouseHoverCallBack();
               }
           }
       }
       /// <summary>
       /// 选中的节点是否是目录？
       /// </summary>
       /// <returns></returns>
       public bool IsDir()
       {
           /*-begin hucheng 20160301 */
           //if (this.selectNode == null)
           //    return true;
           //return this.selectNode.FirstNode != null;
           if (this.selectNode.FirstNode != null)
           {
               if (this.selectNode.FirstNode.Text.EndsWith(".sub"))
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
           /*-end hucheng 20160301 */
       }
       /// <summary>
       /// 获取文件的全路径
       /// </summary>
       /// <param name="endstr"></param>
       /// <returns></returns>
       public string getFileFullPath(string endstr)
       {
           if (this.selectNode == null || selectNode.TreeView == null)
               return "";
           return  this.selectNode.FullPath + endstr;
       }
       /// <summary>
       /// 获取树节点下所有的子节点的路径
       /// </summary>
       /// <param name="node"></param>
       /// <param name="list"></param>
       private void getAllNodePath(TreeNode node, List<string> list)
       {
           foreach (TreeNode nd in node.Nodes)
           {
               if (nd.FirstNode == null)
                   list.Add(nd.FullPath);
               else
                   getAllNodePath(nd, list);
           }
       }
       /// <summary>
       /// 删除节点文件
       /// </summary>
       /// <param name="prjpath"></param>
       /// <returns></returns>
       public void DelNodeFromTree(TreeNode node)
       {
           if (node == null)
               return;
           TreeNode nd = node;
           while (nd.Parent != null)
               nd = nd.Parent;
           string[] nodefullpath=null;
           List<string> list = new List<string>();
           /*-begin hucheng 20160301 */
           //if (node.FirstNode != null){
           if (IsDir())
           {
           /*-end hucheng 20160301 */
               getAllNodePath(node, list);
               nodefullpath = list.ToArray();
           }
           else
               nodefullpath = new string[1] { node.FullPath };
           
           string rootstr = nd.Text;
           TreeNode parent = node.Parent;
           node.Remove();///将树中的节点删除
           while (parent != null && parent.Parent!=null&& parent.FirstNode == null)
           {
               TreeNode tn = parent;             
               parent = tn.Parent;
               tn.Remove();
           }
           ftCallBack.delNodeCallBack(nodefullpath, rootstr);
       }
       /// <summary>
       /// 拷贝文件夹
       /// </summary>
       /// <param name="sourcePath"></param>
       /// <param name="despath"></param>
       public static void CopyDirectory(string sourcePath, string despath)
       {
           if (!Directory.Exists(sourcePath))
               return;
           if (!Directory.Exists(despath))
           {
               Directory.CreateDirectory(despath);
           }
           DirectoryInfo info = new DirectoryInfo(sourcePath);
           foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
           {
               try
               {
                   string destName = Path.Combine(despath, fsi.Name);
                   if (fsi is System.IO.FileInfo){
                      File.Copy(fsi.FullName, destName, true);
                   }
                   else
                   {
                      CopyDirectory(fsi.FullName, destName);
                   }
               }
               catch (Exception)
               {
                   continue;
               }
           }
       }
       /// <summary>
       /// 拷贝目录下的所有文件和目录
       /// </summary>
       /// <param name="sourcePath"></param>
       /// <param name="despath"></param>
       /// <param name="endstr"></param>
       /// <param name="isCopyImage"></param>
       /// <param name="ismove"></param>
       public static void CopyDirectory(string sourcePath, string despath,string endstr,bool isCopyImage,bool ismove)
       {
           DirectoryInfo info = new DirectoryInfo(sourcePath);
           if (!Directory.Exists(despath))
           {
               Directory.CreateDirectory(despath);
           }
           foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
           {
               try
               {
                   string destName = Path.Combine(despath, fsi.Name);
                   if (fsi is System.IO.FileInfo) //如果是文件，复制文件 
                   {
                       if (endstr == null)
                       {
                           if (ismove)///是MOVE操作
                               File.Move(fsi.FullName, destName);
                           else
                               File.Copy(fsi.FullName, destName, true);
                       }
                       else if (fsi.FullName.EndsWith(endstr))
                       {
                           if(ismove)
                               File.Move(fsi.FullName, destName);
                           else
                               File.Copy(fsi.FullName, destName, true);
                       }
                   }
                   else
                   {
                       if (!isCopyImage) ///判断是否是程序的完整图
                       {
                           string prj = fsi.FullName;
                           prj = prj + ".txt";
                           if (File.Exists(prj))
                               continue;
                       }

                       if (!Directory.Exists(destName))
                       {
                           Directory.CreateDirectory(destName);
                       }
                       CopyDirectory(fsi.FullName, destName, endstr, isCopyImage, ismove);
                   }
               }
               catch (Exception)
               {
                   continue;
               }
           }

       }
       /// <summary>
       /// 构造树
       /// </summary>
       /// <param name="dirpath"></param>
       /// <param name="trv"></param>
       /// <returns></returns>
       public int ShowTree(string dirpath,FilesTreeView trv, List<string> fileList = null,bool showSub= true)
       {
           if (!Directory.Exists(dirpath))
           {
               return -1;
           }
           ///构造树，并添加到ZTreeView 中
           trv.ClearAll();
           if (dirpath.EndsWith("\\"))
               dirpath = dirpath.Remove(dirpath.Length - 1);
           TreeNode root = new TreeNode(dirpath);
           trv.BuildPrjTree(root, dirpath, ".txt", null, fileList,showSub);
           trv.AddNode(root);
           root.Expand();
           return 0;
       }

       public List<string> getFilterList(string text, List<string> fileList)
       {
           List<string> filterList = new List<string>();
           foreach (string filename in fileList)
           {
               if (!filename.Contains("@"))
                   continue;
               int index = filename.LastIndexOf("\\");
               string prjname = filename.Substring(index + 1, filename.LastIndexOf("@") - index - 1);
               if (prjname.Contains(text))
               {
                   filterList.Add(filename);
               }
               else if (prjname.ToUpper().Contains(text.ToUpper()))
               {
                   filterList.Add(filename);
               }
               else
               {
                   List<string> subPrjPaths = Directory.GetFiles(filename, "*.sub").ToList();
                   if (subPrjPaths.Any(p => Path.GetFileNameWithoutExtension(p).ToUpper().Contains(text.ToUpper())))
                       filterList.Add(filename);
               }
           }
           return filterList;
       }

       public void ShowTreeByFilterList(string dirpath, List<string> fileList, bool showSub = true)
       {
           ///构造树，并添加到ZTreeView 中
           ClearAll();
           if (dirpath.EndsWith("\\"))
               dirpath = dirpath.Remove(dirpath.Length - 1);
           TreeNode root = new TreeNode(dirpath);
           BuildPrjTree(root, ".txt", fileList, null, showSub);
           AddNode(root);
           root.ExpandAll();
       }
   
 
    public int ShowTree(string dirpath, FilesTreeView trv, TextBox tb, TextBox tbs)
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.ShowDialog();
        tb.Text = dialog.SelectedPath;
        if (!Directory.Exists(tb.Text)){
            return -1;
        }
        ///构造树，并添加到ZTreeView 中
        trv.ClearAll();
        TreeNode root = new TreeNode(tb.Text);     
        dialog.Dispose();
        trv.BuildPrjTree(root, tb.Text, ".txt", null);
        trv.AddNode(root);
        if (root.FirstNode == null) ///是个空目录
            root.Nodes.Add("");
        root.Expand();
        return 0;
   }

    /// <summary>
    /// 判断一个目录是否是project文件。
    /// </summary>
    /// <param name="filedir"></param>
    /// <param name="endString"></param>
    /// <returns></returns>
    private bool isProjectFile(string filedir,string endString)
    {
        ///首先必须是一个目录
        if (!Directory.Exists(filedir))
            return false;

        string dirname = filedir.Substring(filedir.LastIndexOf("\\"));           
        ///该文件夹下有一个同名的.txt 文件
        string[] files = Directory.GetFiles(filedir);
        foreach (string file in files)
        {
            int begin = file.LastIndexOf("\\");
            int end = file.LastIndexOf(".");
            if (end <= begin)
                continue;
            string name = file.Substring(begin, end-begin);
            if (dirname == name && file.EndsWith(endString))
            {
                return true;
            }
           
        }
        return false;
    }

    /// <summary>
    /// 判断节点是否已经着色？
    /// </summary>
    /// <param name="trv"></param>
    /// <returns></returns>
    private bool isColored(TreeNode trv)
    {
        if (trv.BackColor == Color.Green)
            return true;
        TreeNode super = trv.Parent;
        if (super != null && super.Parent!=null)
        {
            if (super.BackColor == Color.Green)
                return true;
            else
                return isColored(super);
        }

        return false;
      }

    /// <summary>
    /// 判断一个目录是否是工程目录
    /// </summary>
    /// <param name="dirfullpath"></param>
    /// <returns></returns>
    private bool isPrjDir(string dirfullpath)
    {
        string dirname = dirfullpath.Substring(dirfullpath.LastIndexOf("\\") + 1);
        return dirname.IndexOf("@") != -1;
    }
    
    /// <summary>
    /// 获取所有的类型信息
    /// </summary>
    /// <returns></returns>
    private static string[] getTypeNames(string dirname)
    {
        int tb = dirname.LastIndexOf("@");
        if (dirname.Length == tb + 1)
            return new string[0];
        string[] types = dirname.Substring(tb + 1).Split(".".ToCharArray());
        return types;
    }
    /// <summary>
    /// 获取子节点
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="sonname"></param>
    /// <returns></returns>
    private static TreeNode getSubNode(TreeNode parent, string sonname)
    {
        foreach (TreeNode one in parent.Nodes)
        {
            if (one.Text == sonname)
                return one;
        }
        return null;
    }
    /// <summary>
    ///  查找目录下所有的prj开头的节点prjname@a.b.c
    /// </summary>
    /// <param name="root"></param>
    /// <param name="fulldirpath"></param>
    /// <param name="endString"></param>
    /// <param name="seachstr"></param>
    public void BuildPrjTree(TreeNode root, string fulldirpath, string endString, string seachstr, List<string> fileList = null,bool showSub = true)
    {
        string[] dirs = Directory.GetDirectories(fulldirpath);
        BuildPrjTree(root, endString, new List<string>(dirs), fileList, showSub);
    }

    public void BuildPrjTree(TreeNode root, string endString, List<string> fileList, List<string> rstList = null,bool showSub = true)
    {
        try
        {
            foreach (string one in fileList)
            {
                if (!isPrjDir(one))
                {
                    continue;
                }
                string prjname = one.Substring(one.LastIndexOf("\\") + 1, one.LastIndexOf("@") - one.LastIndexOf("\\") - 1);
                List<string> subPrjPaths = Directory.GetFiles(one, "*.sub").ToList();
                if (File.Exists(one + "\\" + prjname + endString) || (endString == ".txt" && File.Exists(GetWritePath(one + "\\" + prjname))))
                {
                    if (rstList != null)
                    {
                        rstList.Add(one);
                    }

                    string[] types = getTypeNames(one);

                    TreeNode upnode = root;
                    treeView1.ImageList = imageList;
                    upnode.ImageIndex = 0;
                    upnode.SelectedImageIndex = 0;
                    foreach (string tn in types)
                    {
                        TreeNode node = new TreeNode(tn);
                        if (!isContainSub(upnode, node))
                        {
                            upnode.Nodes.Add(node);
                            upnode = node;
                        }
                        else
                        {
                            upnode = getSubNode(upnode, node.Text);
                        }
                        /*-begin hucheng 20160301 */
                        upnode.ImageIndex = 0;
                        upnode.SelectedImageIndex = 0;
                    }
                    TreeNode leaf = new TreeNode(prjname);
                    leaf.ImageIndex = 1;
                    leaf.SelectedImageIndex = 1;
                    leaf.Tag = one;
                    //添加子版本节点
                    if (subPrjPaths.Count > 0 && showSub == true)
                    {
                        AddSubNode(leaf, subPrjPaths);
                    }
                    /*-end hucheng 20160301 */
                    upnode.Nodes.Add(leaf);
                }
            }

            if (root.FirstNode == null)
            {
                TreeNode node = new TreeNode("");
                root.Nodes.Add(node);
            }
        }
        catch (Exception e)
        {
            return;
        }
    }
/*-begin hucheng 20160301 */
    //添加子版本节点
    public void AddSubNode(TreeNode parent, List<string> subPrjPaths)
    {
        foreach (string subPrjPath in subPrjPaths)
        {
            string subPrjName = Path.GetFileName(subPrjPath);
            TreeNode tn = new TreeNode(subPrjName);
            tn.ImageIndex = 1;
            tn.SelectedImageIndex = 1;
            parent.Nodes.Add(tn);
        }
    }
/*-end hucheng 20160301 */
    /// <summary>
    /// 是否是模板库目录
    /// </summary>
    /// <param name="dirfullpath"></param>
    /// <returns></returns>
    private static  bool isModule(string dirfullpath)
    {
        string dirname = dirfullpath.Substring(dirfullpath.LastIndexOf("\\") + 1);
        return dirname.IndexOf("@") != -1;
    
    }
 
    /// <summary>
    /// 构建Model树
    /// </summary>
    /// <param name="root"></param>
    /// <param name="fulldirpath"></param>
    /// <param name="endString"></param>
    /// <param name="seachstr"></param>
    public static void BuildModTree(TreeNode root, string fulldirpath, string endString, string seachstr)
    {
        if (!Directory.Exists(fulldirpath))
        {
            return;
        }
        string[] dirs = Directory.GetDirectories(fulldirpath);
        foreach (string one in dirs)
        {
            if (!isModule(one))
            {
                continue;
            }
            string modename = one.Substring(one.LastIndexOf("\\") + 1, one.LastIndexOf("@") - one.LastIndexOf("\\") - 1);
            if (File.Exists(one + "\\" + modename + endString))
            {
                string[] types = getTypeNames(one);
                TreeNode upnode = root;
                foreach (string tn in types)
                {
                    TreeNode node = new TreeNode(tn);
                    if (!isContainSub(upnode, node))
                    {
                        upnode.Nodes.Add(node);
                        upnode = node;
                    }
                    else
                    {
                        upnode = getSubNode(upnode, node.Text);
                    }
                }
                TreeNode leaf = new TreeNode(modename);
                upnode.Nodes.Add(leaf);

            }

        }

        if (root.FirstNode == null)
        {
            TreeNode node = new TreeNode("");
            root.Nodes.Add(node);
        }

     }      
        /// <summary>
        /// 获取root下的所有叶子节点？
        /// </summary>
        /// <param name="root"></param>
        /// <param name="list"></param>
        public void getAllChild(TreeNode root, List<TreeNode> list)
        {
            if (root.FirstNode == null)
                return;
            foreach (TreeNode node in root.Nodes)
            {
                if (node.FirstNode == null)
                    list.Add(node);
                else
                    getAllChild(node, list);
            }
        }
        /// <summary>
        /// 从root下查找名称为nodename的节点并返回
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nodename"></param>
        /// <returns></returns>
        public TreeNode getNode(TreeNode root,string nodename)
        {
            if (null == root)
                return null;
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Text == nodename)
                    return node;
            }
            return null;
        }
        /// <summary>
        /// 添加节点到root中
        /// </summary>
        /// <param name="root"></param>
        /// <param name="onedir"></param>
        /// <param name="endString"></param>
        public void AddNode2Tree(TreeNode root,string onedir,string endString)
        {
            try
            {
                string modename = onedir.Substring(onedir.LastIndexOf("\\") + 1, onedir.LastIndexOf("@") - onedir.LastIndexOf("\\") - 1);
                if (File.Exists(onedir + "\\" + modename + endString))
                {
                    string[] types = getTypeNames(onedir);
                    TreeNode upnode = root;
                    foreach (string tn in types)
                    {
                        TreeNode node = new TreeNode(tn);
                        if (!isContainSub(upnode, node))
                        {
                            upnode.Nodes.Add(node);
                            upnode = node;
                        }
                        else
                        {
                            upnode = getSubNode(upnode, node.Text);
                        }

                    }
                    TreeNode leaf = new TreeNode(modename);
                    upnode.Nodes.Add(leaf);

                }
            }
            catch
            {
                //RmlDebug.LogLine("model search error:"+onedir);
            }
     
        }
        /// <summary>
        /// fulldirpath下的所有目录和文件构造树。 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="fulldirpath"></param>
        /// <param name="endString"></param>
        /// <param name="seachstr"></param>
        /// <param name="seachstr2">着色树节点收缩条件</param>
        /// <param name="needsearch"></param>
        public static  void BuildTextTree(TreeNode root, string fulldirpath, string endString, string seachstr, string seachstr2, bool needsearch)
        {
            ///获取节点子目录
            string[] dir = Directory.GetDirectories(fulldirpath);
            List<string> colordir = new List<string>();
            List<string> dirlist = new List<string>();
            if (seachstr != null)
            {
                getPathString(dir, seachstr, dirlist);
            }
            if (needsearch)
            {
                getPathString(dir, seachstr2, colordir);
            }
            if (dir.Length != 0)
            {
                foreach (string onedir in dir)
                {
                    if (onedir.IndexOf("System Volume") != -1)
                        continue;
                    try
                    {
                        int start = onedir.LastIndexOf("\\");
                        string dirname = onedir.Substring(start + 1);
                        TreeNode node = new TreeNode(dirname);
                        if (needsearch == true && colordir.Contains(onedir))
                        {
                            node.BackColor = Color.Orange;
                        }
                        if (seachstr != null)
                        {
                            if (dirlist != null && dirlist.Contains(onedir)) //文件夹包含在检索目录中
                            {
                                root.Nodes.Add(node);
                                if (!IsContailCertainFiles(onedir, new string[1] { endString }) && Directory.GetDirectories(onedir).Length == 0)
                                {
                                    node.Nodes.Add("");
                                }
                                BuildTextTree(node, onedir, endString, null, seachstr2, needsearch); //将节点下的所有文件添加到树节点中
                            }
                            else
                            {
                                if (IsContailCertainFiles(onedir, new string[2] { seachstr, endString }) || IsContailCertainDir(onedir, new string[1] { seachstr }))
                                {
                                    root.Nodes.Add(node);
                                }
                                BuildTextTree(node, onedir, endString, seachstr, seachstr2, needsearch);
                            }

                        }
                        else
                        {
                            root.Nodes.Add(node);
                            if (!IsContailCertainFiles(onedir, new string[1] { endString }) && Directory.GetDirectories(onedir).Length == 0)
                            {
                                node.Nodes.Add("");
                            }
                            BuildTextTree(node, onedir, endString, null, seachstr2, needsearch);
                        }
                    }
                    catch (Exception )  //存在 system volum info 访问被拒绝异常
                    {
                        continue;
                    }
                }

            }
            ///获取节点下的所有文件
            List<string> files = new List<string>();
            List<string> colorfile = new List<string>();
            if (seachstr != null)
            {
                GetCurDirALlFiles(fulldirpath, new string[1] { seachstr }, files);
            }
            else
            {
                files = Directory.GetFiles(fulldirpath).ToList();
            }
            if (needsearch == true)
            {
                GetCurDirALlFiles(fulldirpath, new string[1] { seachstr2 }, colorfile);
            }
            if (files.Count != 0)
            {
                foreach (string onefile in files)
                {
                    try
                    {
                        if (!onefile.EndsWith(endString))
                            continue;
                        int start = onefile.LastIndexOf("\\");
                        int end = onefile.LastIndexOf(".");
                        string filename = onefile.Substring(start + 1, end - start - 1);
                        TreeNode node = new TreeNode(filename);
                        root.Nodes.Add(node);
                        if (needsearch == true && colorfile.Contains(onefile))
                        {
                            node.BackColor = Color.Orange;
                        }
                    }
                    catch (Exception )
                    {
                        continue;
                    }
                }
            }
        }
    }
}
