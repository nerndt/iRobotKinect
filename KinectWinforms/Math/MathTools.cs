using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectWinforms
{
    public class MathTools
    {
        public static double RoundToSignificantFigures(double num, int n)
        {
            if (num == 0)
            {
                return 0;
            }

            double d = Math.Ceiling(Math.Log10(num < 0 ? -num : num));
            int power = n - (int)d;

            double magnitude = Math.Pow(10, power);
            long shifted = (long)Math.Round(num * magnitude);
            return shifted / magnitude;
        }
    }
}
