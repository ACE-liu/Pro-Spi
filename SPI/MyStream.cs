using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using SPI.Global;

namespace SPI
{
    /// <summary>
    /// SPI 文件相关类
    /// </summary>
    public class MyWriter:StreamWriter
    {
        public int DataVersion;
        public string FileName;
        public MyWriter(string path):base(path)
        {
            FileName = path;
        }
        public void Save(WinType type)
        {
            Write(((int)type).ToString() + ",");
        }
        public void Save(int i)
        {
            Save(i.ToString());
        }
        public void Save(double i)
        {
            Save(i.ToString());
        }
        public void SaveLineEnd()
        {
            WriteLine();
        }
        public void Save(byte b)
        {
            Save(b.ToString());
        }
        public void Save(bool b)
        {
            Save(b.ToString());
        }
        public void Save(string str)
        {
            Write((str ?? "") + ",");
        }
        public void SaveLineEnd(string str)
        {
            WriteLine(str);
        }
    }

    public class MyReader:StreamReader
    {
        public int DataVersion;
        public string FileName;
        private int left;
        string[] strings;
        int curLine;
        public MyReader(string path):base(path)
        {
            FileName = path;
            curLine = 0;
            getString();
            if (left > 0 && strings[0] == "DataVersion")
            {
                LoadString();//string[0];
                DataVersion = LoadInt();
                LoadLineEnd();
            }
        }
        public MyReader(string path,Encoding coding):base(path,coding)
        {
            FileName = path;
            curLine = 0;
            getString();
            if (left>0&&strings[0]=="DataVersion")
            {
                LoadString();
                DataVersion = LoadInt();
                LoadLineEnd();
            }
        }
        public static Encoding GetEncodingType(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            Encoding r = GetType(fs);
            fs.Close();
            return r;
        }
        public static Encoding GetType(FileStream fs)
        {
            //byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            //byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            //byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
            Encoding reVal = Encoding.Default;

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;
        }
        /// <summary>
        /// 判断是否是不带 BOM 的 UTF8 格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
        public WinType LoadType()
        {
            WinType rtn = WinType.None;
            getString();
            if (strings!=null)
            {
                rtn = (WinType)int.Parse(strings[strings.Length - left]);
                left--;
            }
            return rtn;
        }
        public double LoadDouble()
        {
            double rtn = -1;
            getString();
            if (strings!=null)
            {
                rtn = double.Parse(strings[strings.Length - left]);
                left--;
            }
            return rtn;
        }
        public byte LoadByte()
        {
            byte rtn = 0;
            getString();
            if (strings!=null)
            {
                rtn = byte.Parse(strings[strings.Length - left]);
                left--;
            }
            return rtn;
        }
        public bool LoadBool()
        {
            bool rtn = false;
            getString();
            if (strings!=null)
            {
                rtn = bool.Parse(strings[strings.Length - left]);
                left--;
            }
            return rtn;
        }
        public void LoadLineEnd()
        {
            left = 0;
        }
        public int LoadInt()
        {
            int rtn = -1;
            getString();
            if (strings!=null)
            {
                rtn = int.Parse(strings[strings.Length - left]);
                left--;
            }
            return rtn;
        }
        public string LoadString()
        {
            string rtn = "";
            getString();
            if (strings!=null)
            {
                rtn = strings[strings.Length - left];
                left--;
            }
            return rtn;
        }
        void getString()
        {
            if (left==0)
            {
                string str = ReadLine(); curLine++;
                if (string.IsNullOrEmpty(str))
                {
                    str = ReadLine(); curLine++;
                }
                if (string.IsNullOrEmpty(str))
                {
                    left = 0;
                    strings = null;
                    return;
                }
                strings = str.Split(',');
                left = strings.Length;
            }
        }
    }
}
