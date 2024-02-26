using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ToFormat(this decimal amount)
        {
            return decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
