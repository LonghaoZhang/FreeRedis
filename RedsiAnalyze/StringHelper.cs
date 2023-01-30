using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class StringHelper
    {
        public static string GetPrefix(this string key,int deep)
        {
            string standardKey = key.Replace(".",":").Replace("@",":").Replace("-",":").Replace("_", ":");
            string result = string.Empty;
            for (var i = 0; i < deep; i++)
            {
                var deepIndex = standardKey.IndexOf(':', (result.Length+1));
                if (deepIndex > 0)
                {
                    result += key.Substring(result.Length, deepIndex - result.Length);
                }
            }
            return result; 
        }
        //" abc=123 "
        public static (string k, string v) GetEquelVal(this string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return ("","");
            }
            item= item.Trim();
            var index = item.IndexOf("=");
            var k = item.Substring(0, index);
            var v = item.Substring(index+1,item.Length-index-1);
            return (k,v);
        }
    }
}
