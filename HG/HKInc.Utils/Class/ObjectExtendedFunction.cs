using System;

namespace HKInc.Utils.Class
{
    public static class ObjectExtendedFunction
    {
        public static bool YesNoToBool(this object input)
        {
            return input.ToString() == "Y" ? true : false;
        }
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return true;
            else
                return false;
        }

        public static Decimal GetNullToZero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")            
                return 0;            
            else            
                return Convert.ToDecimal(obj);            
        }

        public static int GetIntNullToZero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")            
                return 0;            
            else            
                return Convert.ToInt32(obj);            
        }

        public static double GetDoubleNullToZero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")            
                return 0;            
            else            
                return Convert.ToDouble(obj);            
        }

        public static double? GetDoubleNullToNull(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return null;
            else
                return Convert.ToDouble(obj);
        }
        public static string GetNullToEmpty(this object obj)
        {
            if (obj == null || obj == DBNull.Value)        
                return string.Empty;            
            else            
                return obj.ToString().Trim();            
        }
        public static string GetNullToNull(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return null;
            else
                return obj.ToString().Trim();
        }

        public static decimal GetDecimalNullToZero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 0;
            else
                return Convert.ToDecimal(obj);
        }

        public static decimal? GetDecimalNullToNull(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return null;
            else
                return Convert.ToDecimal(obj);
        }


        public static string GetMinutesToHourString(this int minutes)
        {
            if (minutes == 0) return "00:00";

            if (minutes < 0)
            {
                int absMinutes = minutes * -1;
                TimeSpan span = TimeSpan.FromMinutes(absMinutes);
                return $"-{(int)span.TotalHours:n0}:{span.Minutes:00}";
            }
            else
            {
                TimeSpan span = TimeSpan.FromMinutes(minutes);
                return $"{(int)span.TotalHours:n0}:{span.Minutes:00}";
            }
        }

        public static DateTime? GetNullToDateTime(this DateTime dateTime)
        {
            if (dateTime == new DateTime(0001, 01, 01)) return null;
            else return dateTime;
        }

        public static DateTime GetFullTimeToDateTime(this DateTime dateTime)
        {
            if (dateTime == new DateTime(0001, 01, 01)) return new DateTime(0001, 01, 01);
            else
            {
                var ConvertDateTime = DateTime.Parse(dateTime.ToShortDateString());
                return ConvertDateTime.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(990);
                //if (dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0)
                //    return ConvertDateTime.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                //else
                //    return dateTime;
            }
        }

        public static bool GetDateTimeToNullCheck(this DateTime dateTime)
        {
            if (dateTime == new DateTime(0001, 01, 01)) return true;
            else return false;
        }

        public static System.Drawing.Image GetByteArrayToImage(this byte[] byteArray)
        {
            using (var memoryStream = new System.IO.MemoryStream(byteArray))
            {
                return System.Drawing.Image.FromStream(memoryStream);
            }
        }

        public static System.Drawing.Bitmap GetByteArrayToBitMap(this byte[] byteArray)
        {
            using (var ms = new System.IO.MemoryStream(byteArray))
            {
                return new System.Drawing.Bitmap(ms);
            }
        }
    }
}
