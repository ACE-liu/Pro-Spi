using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPI
{
    public interface IFileTreeCallBack
    {
     
        /// <summary>
        /// 鼠标左键单击FileTreeView节点后的回调函数
        /// </summary>
        void mouseLeftClickCallBack();
        /// <summary>
        /// 鼠标右键单击FileTreeView节点后的回调函数
        /// </summary>
        void mouseRightClickCallBack();
        /// <summary>
        /// 修改节点名称回调函数
        /// </summary>
        void afterEditNodeName(string newlablename);
        /// <summary>
        /// 删除节点回调函数
        /// </summary>
        void delNodeCallBack(string[] nodefullpath,string rootdir);
        /// <summary>
        /// 鼠标停靠到节点后的事件
        /// </summary>
        void mouseHoverCallBack();
        /// <summary>
        /// 鼠标停留节点是否着色
        /// </summary>
        /// <returns></returns>
        bool isHoverColored();
   
                 
    }


}
