using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SPI
{
    /// <summary>
    /// 通用数据属性显示对话框，用于显示任意类型的对象的当前属性值
    /// 使用PropertyGrid可显示任意对象的未标注不显示的属性。
    /// </summary>
    public partial class DataGridDlg : Form
    {
        /// <summary>
        /// 新建通用数据属性显示对话框，用于显示任意类型的对象的当前属性值
        /// </summary>
        public DataGridDlg()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 设置或获取待显示的对象
        /// </summary>
        public Object SelectedObject
        {
            get { 
                return propertyGrid1.SelectedObject;                 
            }
            set {
                propertyGrid1.SelectedObject = value; }
        }
        /// <summary>
        /// 设置datagridview 自动排序功能使能
        /// </summary>
        /// <param name="enable"></param>
        /// <summary>
        /// 设置property
        /// </summary>
        /// <param name="ps"></param>
        public void SetPropertyGridSort(PropertySort ps)
        {
            this.propertyGrid1.PropertySort = ps;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.propertyGrid1.Refresh();
        }
    }
}
