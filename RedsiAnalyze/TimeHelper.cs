using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class TimeHelper
    {
        public static string TimeGetSecondHelper(this long second)
        {
            if (second >60*60*24)
            {
                return ((double)second / (60 * 60 * 24)).ToString("0.00")+"d";
            }
            if (second > 60 * 60)
            {
                return ((double)second / (60 * 60)).ToString("0.00") + "h";
            }
            if (second > 60)
            {
                return ((double)second / 60).ToString("0.00") + "m";
            }
            return second + "s";            
        }
        public static string TimeGetMSHelper(this long ms)
        {
            if (ms > 60 * 60 * 24*1000)
            {
                return ((double)ms / (60 * 60 * 24*1000)).ToString("0.00") + "d";
            }
            if (ms > 60 * 60 * 1000)
            {
                return ((double)ms / (60 * 60 * 1000)).ToString("0.00") + "h";
            }
            if (ms > 60 * 1000)
            {
                return ((double)ms / (60 * 1000)).ToString("0.00") + "m";
            }
            if (ms > 1000)
            {
                return ((double)ms / (1000)).ToString("0.00") + "s";
            }
            return ms + "ms";
        }
        public static string TimeGetUSHelper(this long us)
        {
            if (us > 60 * 60 * 24 * 1000)
            {
                return ((double)us / (60 * 60 * 24 * 1000)).ToString("0.00") + "h";
            }
            if (us > 60 * 60 * 1000)
            {
                return ((double)us / (60 * 60 * 1000)).ToString("0.00") + "m";
            }
            if (us > 60 * 1000)
            {
                return ((double)us / (60 * 1000)).ToString("0.00") + "s";
            }
            if (us > 1000)
            {
                return ((double)us / (1000)).ToString("0.00") + "ms";
            }
            return us + "us";
        }
        public static string TimeStrHelper(this long timestamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timestamp + "0000000");
            TimeSpan timeSpan = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(timeSpan).AddHours(8);
            return targetDt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string TimeStrHelper(this int timestamp)
        {
           return ((long)timestamp).TimeStrHelper();
        }
    }
}
