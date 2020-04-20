﻿using Core.Helpers;
 using Core.Schemas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Test
{
    public class TestHelper
    {
        private static int id = 1;
        private static Random random = new Random();

        public static string RandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int max = 1000000000)
        {
            return random.Next(max);
        }

        public static double RandomDouble(double max = 100d, int precision = 2)
        {
            return Math.Round(random.NextDouble() * max, precision, MidpointRounding.AwayFromZero);
        }

        public static string GetFullExportPath(string name)
        {
            return Path.Combine(GetExportFolder(), name);
        }

        public static string GetExportFolder()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Data/Export");
        }

        public static string GetRandomeXlsxExportFile()
        {
            return TestHelper.GetFullExportPath($"{IdGenerator.GetUniqueTicks()}.xlsx");
        }

        public static IList<IList<object>> FromString(string s)
        {
            var result = new List<IList<object>>();
            foreach (var line in s.Split(","))
            {
                result.Add(line.Split(" ").Select(x => x.Replace("_", "")).Cast<object>().ToList());
            }

            return result;
        }

        public static IList<object> ListFromString(string s)
        {
            return s.Split(" ").Select(x => x.Replace("_", "")).Cast<object>().ToList();
        }

        public static string GetCodeRange(string sheetName, PropertySchema schema)
        {
            var codeIndex = schema.GetDefinition("Code").OffsetPos;
            var rangeReference = new RangeReference(sheetName + "!" + schema.DataCellRange);
            rangeReference
                .From.OffsetCol(codeIndex)
                .From.RemoveRow()
                .ToSameAsFrom();
            return rangeReference.ToString();
        }
    }
}
