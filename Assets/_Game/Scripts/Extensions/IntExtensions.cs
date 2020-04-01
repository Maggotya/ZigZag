using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    static class IntExtensions
    {
        public static bool IsInInterval(this int value, int minBorder, int maxBorder)
            => minBorder <= value && value <= maxBorder;
    }
