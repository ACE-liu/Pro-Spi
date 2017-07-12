using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI.Global
{
    /// <summary>
    /// 点相对方框的大致方位.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// .框外
        /// </summary>
        outside,
        /// <summary>
        /// .框内不靠近边处
        /// </summary>
        center,
        /// <summary>
        /// .上边附近
        /// </summary>
        top,
        /// <summary>
        /// .右上角附近
        /// </summary>
        topRight,
        /// <summary>
        /// .右边附近
        /// </summary>
        right,
        /// <summary>
        /// .右下角附近
        /// </summary>
        bottomRight,
        /// <summary>
        /// .下边附近
        /// </summary>
        bottom,
        /// <summary>
        /// .左下角附近
        /// </summary>
        bottomLeft,
        /// <summary>
        /// .左边附近
        /// </summary>
        left,
        /// <summary>
        /// .左上角附近
        /// </summary>
        topLeft
    };
    /// <summary>
    /// SPI报错类型
    /// </summary>
    public enum ResultType
    {
        未确认 = -1,
        NG,
        OK,
        拉尖,
        少锡,
        体积,
        掠锡,
        多锡,
        偏移,
        擦碰,
        平均高度偏出,
        连锡
    }

    /// <summary>
    /// 元件类别方便界面显示
    /// </summary>
    public enum WinType
    {
        None=-1,
        Board,
        SubBoard,
        Chip,
        BGA,
        MarkPoint,
        Land
    }
    public enum ShapeType
    {
        None = -1,
        Rectangle,
        Circle,
        otherType
    }
    /// <summary>
    /// 颜色范围，可能包含红绿蓝比例和灰度上限下限。
    /// </summary>
    public class ColorRange
    {
        /// <summary>
        /// 指示如何组合颜色的enum类
        /// </summary>
        public enum OperationType
        {
            /// <summary>
            /// 将符合本颜色范围的点加入为抽取点
            /// </summary>
            加,
            /// <summary>
            /// 将符合本颜色范围的点剔除出抽取点
            /// </summary>
            减,
            /// <summary>
            /// 只有同时符合本颜色范围和之前的颜色范围组合的点才是抽取点
            /// </summary>
            与
        };
        /// <summary>
        /// 组合方式。如何与之前的颜色范围组合。一组颜色范围中的第一个应为加。
        /// </summary>
        public OperationType operation;
        /// <summary>
        /// 使用红色范围。
        /// </summary>
        public bool redUsed;
        /// <summary>
        /// 红色上限
        /// </summary>
        public byte redUp;
        /// <summary>
        /// 红色下限
        /// </summary>
        public byte redDown;
        /// <summary>
        /// 使用绿色范围
        /// </summary>
        public bool greenUsed;
        /// <summary>
        /// 绿色上限
        /// </summary>
        public byte greenUp;
        /// <summary>
        /// 绿色下限
        /// </summary>
        public byte greenDown;
        /// <summary>
        /// 使用蓝色范围
        /// </summary>
        public bool blueUsed;
        /// <summary>
        /// 蓝色上限
        /// </summary>
        public byte blueUp;
        /// <summary>
        /// 蓝色下限
        /// </summary>
        public byte blueDown;
        /// <summary>
        /// 使用灰度范围
        /// </summary>
        public bool grayUsed;
        /// <summary>
        /// 灰度上限
        /// </summary>
        public byte grayUp;
        /// <summary>
        /// 灰度下限
        /// </summary>
        public byte grayDown;
        /// <summary>
        /// 默认构造函数，未使用任何条件，所有点都被抽取。
        /// </summary>
        public ColorRange()
        {
            operation = OperationType.加;
            redUsed = false;
            redUp = 66;
            redDown = 33;
            greenUsed = false;
            greenUp = 66;
            greenDown = 33;
            blueUsed = false;
            blueUp = 66;
            blueDown = 33;
            grayUsed = false;
            grayUp = 255;
            grayDown = 50;
        }
        /// <summary>
        /// 保存数据到文件
        /// </summary>
        /// <param name="sw">指示要写入的文件的自定义流类对象</param>
        public void SaveTo(MyWriter sw)
        {
            //sw.Save((int)operation);
            //sw.Save(redUsed);
            //sw.Save(redUp);
            //sw.Save(redDown);
            //sw.Save(greenUsed);
            //sw.Save(greenUp);
            //sw.Save(greenDown);
            //sw.Save(blueUsed);
            //sw.Save(blueUp);
            //sw.Save(blueDown);
            //sw.Save(grayUsed);
            //sw.Save(grayUp);
            //sw.Save(grayDown);
            //sw.SaveLineEnd();
            //sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", (int)operation,redUsed,redUp,redDown,greenUsed,greenUp,greenDown,blueUsed,blueUp,blueDown,grayUsed,grayUp,grayDown);
        }
        /// <summary>
        /// 从文件读入数据
        /// </summary>
        /// <param name="sr">指示要从中读入数据的自定义流类对象</param>
        public void LoadFrom(MyReader sr)
        {
            int i = 0;
            //sr.Load(ref i); operation = (OperationType)i;
            //sr.Load(ref redUsed);
            //sr.Load(ref redUp);
            //sr.Load(ref redDown);
            //sr.Load(ref greenUsed);
            //sr.Load(ref greenUp);
            //sr.Load(ref greenDown);
            //sr.Load(ref blueUsed);
            //sr.Load(ref blueUp);
            //sr.Load(ref blueDown);
            //sr.Load(ref grayUsed);
            //sr.Load(ref grayUp);
            //sr.Load(ref grayDown);
            //sr.LoadLineEnd();
        }
        /// <summary>
        /// 从其他模板拷贝数据
        /// </summary>
        /// <param name="colorRange">要从中拷贝数据的模板</param>
        public void CopyFrom(ColorRange colorRange)
        {
            operation = colorRange.operation;
            redUsed = colorRange.redUsed;
            redUp = colorRange.redUp;
            redDown = colorRange.redDown;
            greenUsed = colorRange.greenUsed;
            greenUp = colorRange.greenUp;
            greenDown = colorRange.greenDown;
            blueUsed = colorRange.blueUsed;
            blueUp = colorRange.blueUp;
            blueDown = colorRange.blueDown;
            grayUsed = colorRange.grayUsed;
            grayUp = colorRange.grayUp;
            grayDown = colorRange.grayDown;
        }
    }
    class SpiBase
    {
    }
}
