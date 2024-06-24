using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class StatisticsHelper
    {
        #region keyDistribution
        public static Dictionary<string, int> StatisticsCountResult = new Dictionary<string, int>();
        public static Dictionary<string, int> ShowStatisticsCountResult = new Dictionary<string, int>();
        public static long Count = 0;
        public static void Init(int isClearLastAnalyzeData=1)
        {
            if (isClearLastAnalyzeData != 1)
            {
                ClearLastAnalyzeData = false;
                return;
            }
            StatisticsCountResult.Clear();
            ShowStatisticsCountResult.Clear();
            Count = 0;
            Count_All = 0;
            Count_LT_KB = 0;
            Count_KB_1 = 0;
            Count_KB_10 = 0;
            Count_KB_100 = 0;
            Count_MB_1 = 0;
            Count_MB_10 = 0;
            Count_MB_50 = 0;
            Count_KB_10_Keys.Clear();
            Count_KB_100_Keys.Clear();
            Count_MB_1_Keys.Clear();
            Count_MB_10_Keys.Clear();
            Count_MB_50_Keys.Clear();
            ClearLastAnalyzeData = true;
        }
        public static void Inc(this string key)
        {
            if (StatisticsCountResult.ContainsKey(key))
            {
                StatisticsCountResult[key] = (StatisticsCountResult[key]+1);
            }
            else
            {
                StatisticsCountResult[key] = 1;
            }
            Count++;
        }
        #endregion
        public static bool ClearLastAnalyzeData = true;
        #region bigKeyDistribution
        public static int Count_All = 0;
        public static int Count_LT_KB = 0;
        public static int Count_KB_1 = 0;
        public static int Count_KB_10 = 0;
        public static int Count_KB_100 = 0;
        public static int Count_MB_1 = 0;
        public static int Count_MB_10 = 0;
        public static int Count_MB_50 = 0;
        public static List<string> Count_KB_10_Keys = new List<string>();
        public static List<string> Count_KB_100_Keys = new List<string>();
        public static List<string> Count_MB_1_Keys = new List<string>();
        public static List<string> Count_MB_10_Keys = new List<string>();
        public static List<string> Count_MB_50_Keys = new List<string>();
        public static void Inc(this int bt,string key)
        {
            Count_All++;
            if (bt > 1024 * 1024 * 50)
            {
                Count_MB_50++;
                if (Count_MB_50 < 100)
                {
                    Count_MB_50_Keys.Add(key);
                }
                return;
            }
            if (bt > 1024 * 1024 * 10)
            {
                Count_MB_10++;
                if (Count_MB_10 < 100)
                {
                    Count_MB_10_Keys.Add(key);
                }
                return;
            }
            if (bt > 1024 * 1024)
            {
                Count_MB_1++;
                if (Count_MB_1 < 100)
                {
                    Count_MB_1_Keys.Add(key);
                }
                return;
            }
            if (bt > 1024 * 100)
            {
                Count_KB_100++;
                if (Count_KB_100 < 100)
                {
                    Count_KB_100_Keys.Add(key);
                }
                return;
            }
            if (bt > 1024 * 10)
            {
                Count_KB_10++;
                if (Count_KB_10 < 100)
                {
                    Count_KB_10_Keys.Add(key);
                }
                return;
            }
            if (bt > 1024)
            {
                Count_KB_1++;
                return;
            }
            Count_LT_KB++;
        }
        #endregion
    }
}
