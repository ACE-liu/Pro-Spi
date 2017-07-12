using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SPI
{
    /// <summary>
    /// SPI 文件相关类，具体是否按照2D，待design；
    /// </summary>
    public class MyWriter:StreamWriter
    {
        public int DataVersion;
        public string FileName;
        public MyWriter(string path):base(path)
        {
            FileName = path;
        }
    }

    public class MyReader:StreamReader
    {
        public int DataVersion;
        public string FileName;
        public MyReader(string path):base(path)
        {
            FileName = path;
        }
    }
}
