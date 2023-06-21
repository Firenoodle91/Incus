using System;
using System.Text.RegularExpressions;

namespace HKInc.Utils.Class
{
    public static class StringExtendedFunction
    {
        public static string Left(this string text, int textLen)
        {
            string convText;

            if (text.Length < textLen)
                textLen = text.Length;

            convText = text.Substring(0, textLen);
            return convText;
        }

        public static string Right(this string text, int textLen)
        {
            string convText;
            if (text.Length < textLen)
                textLen = text.Length;

            convText = text.Substring(text.Length - textLen, textLen);
            return convText;
        }

        public static string Mid(this string text, int startInt, int endInt)
        {
            string convText;
            if (startInt < text.Length || endInt < text.Length)
            {
                convText = text.Substring(startInt, endInt);
                return convText;
            }
            else
                return text;
        }

        public static string UpperToUnderBar(this string str)
        {
            return Regex.Replace(str, "([a-z])([A-Z])", "$1_$2").ToUpper();
        }

        public static string UpperToSpace(this string str)
        {
            return Regex.Replace(str, "([a-z])([A-Z])", "$1 $2");
        }

        public static string NumberCheck(this string num)
        {
            string retValue = "";

            Regex regex = new Regex(@"^[0-9]{1,10}$");

            if ((num.Length > 1) && (num.Substring(0, 1) == "-"))
                retValue = "-";

            for (int i = 0; i < num.Length; i++)
            {
                Boolean ismatch = regex.IsMatch(num[i].ToString());
                if (!ismatch)
                    continue;
                else
                    retValue += num[i];
            }
            return retValue;
        }
    }
}
