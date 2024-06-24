using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
   public static class ColorConsole
    {
        #region writeline
        public static void WriteLineRed(this string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineGreen(this string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineYellow(this string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineBlue(this string str)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineDarkRed(this string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineDarkYellow(this string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineGray(this string str)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLine(this string str)
        {
            Console.WriteLine(str);
        }
        #endregion

        #region write
        public static void WriteRed(this string str, string append = "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str + append);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteGreen(this string str, string append = "")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str + append);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteDarkRed(this string str, string append = "")
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str + append);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteDarkYellow(this string str, string append = "")
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(str + append);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteYellow(this string str, string append = "")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(str + append);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteGray(this string str, string append = "")
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(str + append);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Write(this string str,string append="")
        {
            Console.Write(str + append);
        }
        #endregion
    }
}
