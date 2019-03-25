using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace xehardiversity
{
    public static class Extensions
    {
        public static string CheckUpdated(this string value, DateTime check)
        {
            return check.ToShortDateString() == "01/01/0001" | check.ToShortDateString() == "1/1/0001" | check.ToShortDateString() == "01/01/1753" ? "Never" : check.CorrectDateFormat();
        }

        public static string SqlSingleQuotes(this string value)
        {
            // This will take A'a and change it to A''a. SQL needs to have '' and not just one
            return Regex.Replace(value, "([a-zA-Z])'([a-zA-Z])", "$1''$2");

        }

        public static DateTime SqlTranslateDateTime(this DateTime value)
        {
            if (value == DateTime.MinValue)
            {
                value = (DateTime)SqlDateTime.MinValue;
            }
            return value;
        }

        public static string CorrectDateFormat(this DateTime value)
        {
            return String.Format("{0}/{1}/{2}", value.Month, value.Day, value.Year);
        }

        public static string BrowserDateFormat(this DateTime value)
        {
            string rDay = value.Day < 10 ? "0" + value.Day : value.Day.ToString();
            string rMonth = value.Month < 10 ? "0" + value.Month : value.Month.ToString();
            string rYear = value.Year == 1 ? "0001" : value.Year.ToString();
            return String.Format("{0}-{1}-{2}", rYear, rMonth, rDay);
        }

        public static void Intem(this string[] value, out int[] ints, out string um)
        {
            ints = new int[value.Length];//   //  \\  \/ /\
            for (int i = 0; i < ints.Length; i++)
            {
                try
                {
                    ints[i] = int.Parse(value[i]);
                }
                catch (Exception e)
                {
                    um = "Something failed" + e.Message;
                }
            }
            um = "All good!";
        }

        public static string[] Splitem(this string value)
        {
            return Regex.Split(value, "@_@");
        }

        public static string Ci(this bool value)
        {
            return value == false ? "Incomplete" : "Complete";
        }
    }
}