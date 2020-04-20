﻿using System;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public static class SheetHelper
    {
        private const int Seed = 26;
        public static long ColToNumber(string s)
        {
            var sum = 0L;
            for (var i = 0; i < s.Length; i++)
            {
                var pos = s.Length - i - 1;
                int val = s[i] - 65 + 1;
                var mul = Math.Pow(Seed, pos);
                sum += (long)Math.Round(val * mul);
            }

            return sum;
        }

        public static string ColFromNumber(long n)
        {
            StringBuilder sb = new StringBuilder();

            long result = n / Seed;
            while (true)
            {
                long mod = n - result * Seed;
                int charNumber = mod == 0 ? 65 : (int)(mod - 1 + 65);

                sb.Append(char.ConvertFromUtf32(charNumber));
                if (result == 0) break;
                n = result;
                result = result / Seed;
            }

            return new string(sb.ToString().Reverse().ToArray());
        }

        public static string OffsetCol(string col, int offset)
        {
            return ColFromNumber(ColToNumber(col) + offset);
        }

        public static RangeReference CreateRangeReference(string baseRange, int offsetCol, int rowNumber)
        {
            var selectedRowRange = new RangeReference(baseRange);
            selectedRowRange
                .From.OffsetCol(offsetCol)
                .From.ReplaceRow(rowNumber)
                .ToSameAsFrom();
            return selectedRowRange;
        }
    }
}
