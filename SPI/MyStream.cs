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
    class MyWriter:StreamWriter
    {
        public int DataVersion;
        public string FileName;
        internal MyWriter(string path):base(path)
        {
            FileName = path;
        }
    }

    class MyReader:StreamReader
    {
        public int DataVersion;
        public string FileName;
        internal MyReader(string path):base(path)
        {
            FileName = path;
        }
    }
}
