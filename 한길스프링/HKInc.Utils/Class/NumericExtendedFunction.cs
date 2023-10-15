using System;

namespace HKInc.Utils.Class
{
    public static class NumericExtendedFunction
    {
        public static int GetDecimalToInt(this decimal input)
        {
            return Convert.ToInt32(input);
        }

        public static int GetDoubleToInt(this double input)
        {
            return Convert.ToInt32(input);
        }

        public static string NumberToKoreanString(this double lngNumber)
        {
            bool UseDecimal = false;
            string Sign = "";
            int i = 0;
            int Level = 0;

            string[] NumberChar = new string[] { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
            string[] LevelChar = new string[] { "", "십", "백", "천" };
            string[] DecimalChar = new string[] { "", "만", "억", "조", "경" };

            string strValue = string.Format("{0}", lngNumber);
            string NumToKorea = Sign;
            UseDecimal = false;

            for (i = 0; i < strValue.Length; i++)
            {
                Level = strValue.Length - i;
                if (strValue.Substring(i, 1) != "0")
                {
                    UseDecimal = true;
                    if (((Level - 1) % 4) == 0)
                    {
                        NumToKorea = NumToKorea + NumberChar[int.Parse(strValue.Substring(i, 1))] + DecimalChar[(Level - 1) / 4];
                        UseDecimal = false;
                    }
                    else
                    {
                        if (strValue.Substring(i, 1) == "1")
                        {
                            NumToKorea = NumToKorea + LevelChar[(Level - 1) % 4];
                        }
                        else
                        {
                            NumToKorea = NumToKorea + NumberChar[int.Parse(strValue.Substring(i, 1))] + LevelChar[(Level - 1) % 4];
                        }
                    }
                }
                else
                {
                    if ((Level % 4 == 0) && UseDecimal)
                    {
                        NumToKorea = NumToKorea + DecimalChar[Level / 4];
                        UseDecimal = false;
                    }
                }
            }
            return NumToKorea;
        }
       

        public static double GetDecimalToDouble(this decimal input)
        {
            return Convert.ToDouble(input);
        }

     
    }
}
