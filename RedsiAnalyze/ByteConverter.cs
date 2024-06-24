using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class ByteConverter
    {
        public static string ByteHelper(this string byteStr)
        {
            var bt = Convert.ToDouble(byteStr);
            if (bt > 1024 * 1024 * 1024)
            {
                return (bt / (1024 * 1024 * 1024)).ToString("0.00") + "GB";
            }
            if (bt > 1024 * 1024)
            {
                return (bt / (1024 * 1024 )).ToString("0.00") + "MB";
            }
            if (bt > 1024)
            {
                return (bt / (1024)).ToString("0.00") + "KB";
            }
            return bt  + "B";
        }

        public static string ByteHelper(this long length)
        {
            return length.ToString().ByteHelper();
        }

    }
}
