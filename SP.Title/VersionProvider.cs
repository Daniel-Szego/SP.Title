using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SP.Title
{
    public static class VersionProvider
    {
        public static string ProviderInfo = "";
        public static string VersionNumber = "Release 0.87";

        public static string GetBuild()
        {
            //Assembly executingAssembly = Assembly.GetExecutingAssembly();
            //DateTime time = RetrieveLinkerTimestamp(executingAssembly.Location);
            //return (time.Year.ToString(CultureInfo.InvariantCulture).Substring(2) + time.DayOfYear.ToString(CultureInfo.InvariantCulture) + (DateTime.IsLeapYear(time.Year) ? " (366days)" : ""));
            return string.Empty;
        }

        private static DateTime RetrieveLinkerTimestamp(string filePath)
        {
            Stream stream = null;
            byte[] buffer = new byte[0x800];
            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                stream.Read(buffer, 0, 0x800);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            int num = BitConverter.ToInt32(buffer, 60);
            int num2 = BitConverter.ToInt32(buffer, num + 8);
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0);
            time = time.AddSeconds((double)num2);
            return time.AddHours((double)TimeZone.CurrentTimeZone.GetUtcOffset(time).Hours);
        }


    }
}
