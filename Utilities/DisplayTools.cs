using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iRobotKinect
{
    public class DisplayTools
    {
        public static string RoundToSignificantFiguresForDisplay(float? Value, int significantFigures)
        {
            if (Value == null)
            {
                return string.Empty;
            }
            else
            {
                if (float.IsNaN((float)Value) == true)
                    return string.Empty;

                return MathTools.RoundToSignificantFigures((double)Value, significantFigures).ToString("g7");
            }
        }

        public static string RoundToSignificantFiguresForDisplay(double? Value, int significantFigures)
        {
            if (Value == null)
            {
                return string.Empty;
            }
            else
            {
                if (double.IsNaN((double)Value) == true)
                    return string.Empty;

                return MathTools.RoundToSignificantFigures((double)Value, significantFigures).ToString("g7");
            }
        }

        public static string TruncateToMaxDecimalDigits(double? Value, int MaxDecimalDigits)
        {
            if (Value == null)
                return string.Empty;

            if (double.IsNaN((double)Value) == true)
                return string.Empty;

            if (MaxDecimalDigits < 0)
                MaxDecimalDigits = 0;

            // Truncate Value to MaxDecimalDigits
            double Multiplier = Math.Pow(10, MaxDecimalDigits);
            Value = Math.Truncate((double)Value * Multiplier) / Multiplier;

            int i = MaxDecimalDigits;
            string retString = ((double)Value).ToString(string.Format("F{0}", i));
            string OneLessDecimalDigitString;
            double retValue, OneLessDecimalDigitValue;

            for (; i > 0; --i)
            {
                retValue = Convert.ToDouble(retString);
                OneLessDecimalDigitString = ((double)Value).ToString(string.Format("F{0}", i - 1));
                OneLessDecimalDigitValue = Convert.ToDouble(OneLessDecimalDigitString);

                if (retValue != OneLessDecimalDigitValue)
                    break;

                retString = OneLessDecimalDigitString;
            }

            return retString;
        }
    }
}
