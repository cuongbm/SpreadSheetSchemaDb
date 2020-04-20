﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.Helpers
{
    public static class Utils
    {
        public static bool IsWildCardMatchOrContains(this string input, string pattern, bool caseSensitive)
        {
            if (input == null)
                return string.IsNullOrEmpty(pattern);
            if (IsWildCardMatch(input, pattern, caseSensitive)) return true;
            if (caseSensitive) return input.Contains(pattern);
            return input.ToLowerInvariant().Contains(pattern.ToLowerInvariant());
        }

        public static bool IsWildCardMatch(this string input, string pattern, bool caseSensitive)
        {
            if (input == null)
                return string.IsNullOrEmpty(pattern);

            if (!caseSensitive)
            {
                input = input.ToLower();
                pattern = pattern.ToLower();
            }

            pattern = pattern
                .Replace(".", @"\.")
                .Replace('?', '.')
                .Replace("*", ".*");

            pattern = $@"\A{pattern}\z";
            try
            {
                return Regex.IsMatch(input, pattern);
            }
            catch /* (ArgumentException ex) */
            {
                return false;
            }
        }

        public static List<T> BuildNotIgnoredList<T>(int from, int to, Func<int, T> createFunc) where T : IIgnorable
        {
            var result = new List<T>();

            for (int i = from; i <= to; i++)
            {
                var record = createFunc(i);
                if (record.ShouldIgnore) continue;
                result.Add(record);
            }

            return result;
        }
    }

    public interface IIgnorable
    {
        bool ShouldIgnore { get; set; }
    }
}
