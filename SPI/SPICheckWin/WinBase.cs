using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI.SPIModel;
using System.Drawing;
using System.ComponentModel;
using SPI.Global;

using static SPI.Global.Configuration;
using System.Windows.Forms;

namespace SPI.SPICheckWin
{
    abstract class WinBase
    {
        public event EventHandler DataChanged = null;
        #region ***************显示的属性*****************
        /// <summary>
        /// 窗口类型
        /// </summary>
        [CategoryAttribute("一般信息"), DescriptionAttribute("类信息"), DisplayName("窗口类型")]
        public WinType WType { get { return wType; } }
        /// <summary>
        /// 显示名称,公共属性
        /// </summary>
        [CategoryAttribute("一般信息"), DescriptionAttribute("名称字符串"), DisplayName("显示名称")]
        public virtual string DisplayName { get { return this.ToString(); } }
        /// <summary>
        /// 方框ID,公共属性
        /// </summary>
        [BrowsableAttribute(true), ReadOnlyAttribute(true), CategoryAttribute("一般信息"), DisplayName("ID")]
        public int ID { get { return id; } set { id = value; } }
        [BrowsableAttribute(true), ReadOnlyAttribute(false), CategoryAttribute("位置信息"), DescriptionAttribute("左上角及尺寸"), DisplayName("窗口位置")]
        public virtual Rectangle Position { get { return ShowShape.Rectangle; } }
        [BrowsableAttribute(true), ReadOnlyAttribute(true), CategoryAttribute("位置信息"), DescriptionAttribute("中心位置"), DisplayName("中心位置")]
        public Point Center { get { return ShowShape.GetCenter(); } }
        [BrowsableAttribute(true), CategoryAttribute("位置信息"), DescriptionAttribute("右边界坐标"), DisplayName("右边界")]
        public int Right { get { return ShowShape.Right; } }
        [BrowsableAttribute(true), CategoryAttribute("位置信息"), DescriptionAttribute("下边界坐标"), DisplayName("下边界")]
        public int Bottom { get { return ShowShape.Bottom; } }
        [BrowsableAttribute(true), ReadOnlyAttribute(true), CategoryAttribute("一般信息")]
        public virtual int SelectColors { get { return -1; } set { } }
        #endregion
        public List<WinBase> SubWinList = null;
        protected int id = -1;
        protected WinType wType = WinType.None;
        public ShapeBase ShowShape { get; protected set; } = null;
        public abstract void Show(Graphics g);

        //标记当前窗是否可以移动或者可以尺寸变化
        public bool CanMove = false;
        public bool CanResize = false;
        internal WinBase Parent = null;
        /// <summary>
        /// 计算鼠标相对本检查框处于哪个位置。
        /// 鼠标位置已转换为板坐标。
        /// </summary>
        /// <param name="e">鼠标位置对应的板坐标</param>
        /// <param name="deep">是否搜索子项</param>
        /// <returns></returns>
        public Direction MouseOverWhere(Point e)
        {
            return ShowShape.MouseOverWhere(e); //交给图形解释
        }
        public void Move(Point p)
        {
            ShowShape.Move(p);
        }
        public void Move(int x, int y)
        {
            Move(new Point(x, y));
        }
        public void ResizeAroundCenter(int width, int height)
        {
            ShowShape.ResizeAroundCenter(width, height);
            MoveToRange(Parent);
        }
        /// <summary>
        /// d1位置是否比d2位置关联更强.关联性按边界》中间》外边顺序排列.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool MoreFocus(Direction d1, Direction d2)
        {
            if (d2 > Direction.center)
                return false;
            return d1 > d2;
        }
        public void OnLoseFocus()
        {
            if (!(this is Board))
            {
                CanResize = false;
                CanMove = false;
            }
        }
        /// <summary>
        /// 获取到焦点处理
        /// </summary>
        public void OnFocus()
        {
            if (this is CheckWinBase)
            {
                ShowProperty = false;
            }
            else
                ShowProperty = true;
            GetFocus();
        }
        public static CheckWinBase NewComponent(WinType type)
        {
            CheckWinBase win = null;
            switch (type)
            {
                case WinType.None:
                    break;
                case WinType.Board:
                    break;
                case WinType.SubBoard:
                    break;
                case WinType.Chip:
                    win = new Chip();
                    break;
                case WinType.BGA:
                    //win = new BGA();
                    break;
                case WinType.MarkPoint:
                    break;
                case WinType.Land:
                    break;
                default:
                    break;
            }
            return win;
        }
        public virtual void ChangeColorRange(MultiColors mc)
        {
        }
        public virtual void OnDeleteFromBoard()
        {
            SubWinList?.Clear();
        }
        public virtual void GetFocus()
        {
            if (CurFocusPanel == null)
            {
                return;
            }
            CurFocusPanel.SuspendLayout();
            while (CurFocusPanel.Controls.Count > 0)
            {
                if (null != CurFocusPanel.Controls[0])
                {
                    CollectManualCreateComponentMemory(CurFocusPanel.Controls[0]);
                }
            }
            CurFocusPanel.Controls.Clear();
            CurFocusPanel.AutoScrollPosition = new Point(0, 0);
            CurFocusPanel.ResumeLayout();
            PropertyGrid pg = new PropertyGrid();
            pg.SelectedObject = this;
            pg.Dock = DockStyle.Fill;
            CurFocusPanel.Controls.Add(pg);
        }
        /// <summary>
        /// 递归释放子父控件资源
        /// </summary>
        /// <param name="ctr"></param>
        public void CollectManualCreateComponentMemory(Control ctr)
        {
            if (null == ctr)
            {
                return;
            }
            if (0 == ctr.Controls.Count)
            {
                ctr.Dispose();
                ctr = null;
                return;
            }
            if (null != ctr as PropertyGrid)
            {
                while ((ctr as PropertyGrid).Controls.Count > 0)
                {
                    //this.CollectManualCreateComponentMemory((ctr as PropertyGrid).Controls[0]);
                    (ctr as PropertyGrid).SelectedObject = null;
                    //(ctr as PropertyGrid).PropertyValueChanged -= new PropertyValueChangedEventHandler(Globles.theForm.propertyGrid1_PropertyValueChanged);
                    (ctr as PropertyGrid).Dispose();
                }
            }
            else
            {
                while (ctr.Controls.Count > 0)
                {
                    this.CollectManualCreateComponentMemory(ctr.Controls[0]);
                }
                ctr.Dispose();
                ctr = null;
            }
        }
        /// <summary>
        /// 检查窗的任何数据改变事件处理。
        /// </summary>
        public virtual void OnDataChange(object sender, EventArgs e)
        {
            if (DataChanged != null)
            {
                DataChanged(this, new EventArgs());
            }
            else
            {
                //Globles.theForm.RefreshPropertyGrid();
                //Globles.markedPicture.Refresh();
            }
        }
        /// <summary>
        /// 根据鼠标位置改变框位置或尺寸.
        /// </summary>
        /// <param name="me"></param>
        public virtual void ChangeRect(Point me)
        {
            Point e = MarkedPicture.ShowToPos(me);
            ChangeSelf(e);

            if (Parent != null)
                this.ShrinkToRange(Parent);
            this.ExpandToIncludeSubs(MarkedPicture.ChangingEdge);
            return;
        }
        public void ShrinkToRange(WinBase win)
        {
            ShowShape.ShrinkToRange(win.ShowShape);
        }
        /// <summary>
        /// 扩展自身范围以便包含所有子项范围.
        /// </summary>
        /// <param name="edge">当前正在修改的边</param>
        private void ExpandToIncludeSubs(Direction edge)
        {
            if (SubWinList == null)
            {
                return;
            }
            foreach (WinBase win in SubWinList)
            {
                ShowShape.ExpandToInclude(win.ShowShape, edge);
            }
        }
        /// <summary>
        /// 根据鼠标位置改变自身大小或位置。鼠标位置已转换为板左边。
        /// </summary>
        /// <param name="orge">已转化为板坐标的鼠标位置</param>
        public virtual void ChangeSelf(Point e)
        {
            Point orge = new Point(e.X - ShowShape.MarkShift.X, e.Y - ShowShape.MarkShift.Y);
            switch (MarkedPicture.ChangingEdge)
            {
                case Direction.top:
                    if (CanResize)
                    {
                        if (ShowShape.Height - orge.Y + ShowShape.Y > min) { ShowShape.Height = ShowShape.Height - orge.Y + ShowShape.Y; ShowShape.Y = orge.Y; } else { ShowShape.Y = ShowShape.Height + ShowShape.Y - min; ShowShape.Height = min; }
                    }
                    break;
                case Direction.bottom:
                    if (CanResize)
                        ShowShape.Height = orge.Y - ShowShape.Y;
                    break;
                case Direction.left:
                    if (CanResize)
                    {
                        if (ShowShape.Width - orge.X + ShowShape.X > min) { ShowShape.Width = ShowShape.Width - orge.X + ShowShape.X; ShowShape.X = orge.X; } else { ShowShape.X = ShowShape.X + ShowShape.Width - min; ShowShape.Width = min; }
                    }
                    break;
                case Direction.right:
                    if (CanResize)
                        ShowShape.Width = orge.X - ShowShape.X;
                    break;
                case Direction.topLeft:
                    if (CanResize)
                    {
                        if (ShowShape.Width - orge.X + ShowShape.X > min) { ShowShape.Width = ShowShape.Width - orge.X + ShowShape.X; ShowShape.X = orge.X; } else { ShowShape.X = ShowShape.X + ShowShape.Width - min; ShowShape.Width = min; }
                        if (ShowShape.Height - orge.Y + ShowShape.Y > min) { ShowShape.Height = ShowShape.Height - orge.Y + ShowShape.Y; ShowShape.Y = orge.Y; } else { ShowShape.Y = ShowShape.Height + ShowShape.Y - min; ShowShape.Height = min; }
                    }
                    break;
                case Direction.bottomRight:
                    if (CanResize)
                    {
                        ShowShape.Height = orge.Y - ShowShape.Y;
                        ShowShape.Width = orge.X - ShowShape.X;
                    }
                    break;
                case Direction.topRight:
                    if (CanResize)
                    {
                        if (ShowShape.Height - orge.Y + ShowShape.Y > min) { ShowShape.Height = ShowShape.Height - orge.Y + ShowShape.Y; ShowShape.Y = orge.Y; } else { ShowShape.Y = ShowShape.Height + ShowShape.Y - min; ShowShape.Height = min; }
                        ShowShape.Width = orge.X - ShowShape.X;
                    }
                    break;
                case Direction.bottomLeft:
                    if (CanResize)
                    {
                        if (ShowShape.Width - orge.X + ShowShape.X > min)
                        { ShowShape.Width = ShowShape.Width - orge.X + ShowShape.X; ShowShape.X = orge.X; }
                        else { ShowShape.X = ShowShape.X + ShowShape.Width - min; ShowShape.Width = min; }
                        ShowShape.Height = orge.Y - ShowShape.Y;
                    }
                    break;
                case Direction.center:
                    if ((this.wType != WinType.Board) && CanMove)
                    {
                        Point oldpos = ShowShape.Location;
#pragma warning disable CS1690 // 访问引用封送类的字段上的成员可能导致运行时异常
                        ShowShape.X = orge.X - theMarkPicture.MouseDownDelt.X;
#pragma warning restore CS1690 // 访问引用封送类的字段上的成员可能导致运行时异常
                        ShowShape.Y = orge.Y - theMarkPicture.MouseDownDelt.Y;
                        if (Parent != null) this.MoveToRange(Parent);
                        this.MoveSub(new Point(ShowShape.X - oldpos.X, ShowShape.Y - oldpos.Y));
                    }
                    return;
                case Direction.outside:
                default:
                    //#warning should not come here
                    break;
            }
            if (ShowShape.Width <= 0) ShowShape.Width = min;
            if (ShowShape.Height <= 0) ShowShape.Height = min;
        }
        protected void MoveToRange(WinBase parent)
        {
            ShowShape.MoveToRange(parent.ShowShape);
        }
        protected void MoveSub(Point shift)
        {
            if (SubWinList == null)
            {
                return;
            }
            foreach (var item in SubWinList)
            {
                item.Move(shift);
            }
        }
        /// <summary>
        /// 根据鼠标位置获取选中的Win
        /// </summary>
        /// <param name="de"></param>
        /// <returns></returns>
        public WinBase FindMouseOnRect(Point de)
        {
            if (SubWinList == null || SubWinList.Count == 0)
                return this;
            WinBase centerRect = null;
            foreach (WinBase rct in SubWinList ?? new List<WinBase>())
            {
                Direction dtemp = rct.MouseOverWhere(de);
                switch (dtemp)
                {
                    case Direction.outside:
                        break;
                    case Direction.center:
                        //如果没有边，反馈先找到的中心框。
                        if (centerRect == null || rct == CurFocus)
                            centerRect = rct;
                        break;
                    default:
                        return rct;
                }
            }
            if (centerRect == null)
                return this;
            WinBase win = centerRect.FindMouseOnRect(de);
            if (win != null)
            {
                return win;
            }
            if (CurFocus != null)
            {
                Direction fdir = CurFocus.MouseOverWhere(de);
                Direction mdir = centerRect.MouseOverWhere(de);
                if (MoreFocus(mdir, fdir))
                    return centerRect;
                else
                {
                    if (CurFocus == TheBoard && mdir > Direction.outside)
                        return centerRect;
                    else
                        return CurFocus;
                }
            }
            return centerRect;
        }
    }
}
