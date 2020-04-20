﻿using System;
using System.Text.RegularExpressions;

namespace Core.Helpers
{
    public class RangeReference
    {
        public string SheetName { get; private set; }

        public CellReference From { get; private set; } = new CellReference("");

        public CellReference To { get; private set; } = new CellReference("");

        public RangeReference(string a1Notation)
        {
            string[] sArr = a1Notation.Split(new[] { "!" }, StringSplitOptions.RemoveEmptyEntries);
            if (sArr.Length == 1)
            {
                ObtainCellsReference(sArr[0]);
            }
            else
            {
                SheetName = sArr[0];
                ObtainCellsReference(sArr[1]);
            }
        }

        public static RangeReference Create(string a1Notation)
        {
            return new RangeReference(a1Notation);
        }

        private void ObtainCellsReference(string s)
        {
            string[] sArr = s.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            From = new CellReference(sArr[0], this);
            To = new CellReference(sArr.Length > 1 ? sArr[1] : "", this);
        }

        public RangeReference ClearSheetId()
        {
            SheetName = "";
            return this;
        }

        public RangeReference ClearTo()
        {
            To = new CellReference("");
            return this;
        }

        public RangeReference ToSameAsFrom()
        {
            To = From;
            return this;
        }

        public RangeReference SelectOneRow()
        {
            To.Row = From.Row;

            return this;
        }

        public override string ToString()
        {
            var cellRefsStr = CellReferencesToString();
            return string.IsNullOrEmpty(SheetName) ? cellRefsStr : string.Format("{0}!{1}", SheetName, cellRefsStr);
        }

        private string CellReferencesToString()
        {
            var fromStr = From.ToString();
            var toStr = To.ToString();

            return string.IsNullOrEmpty(toStr) ? fromStr : string.Format("{0}:{1}", fromStr, toStr);
        }
    }

    public class CellReference
    {
        static Regex regex = new Regex(@"^([A-Za-z]*)([0-9]*)", RegexOptions.IgnoreCase);
        private readonly RangeReference rangeReference;

        public string Col { get; set; } = "";
        public string Row { get; set; } = "";

        public CellReference(string a1Notation, RangeReference RangeReference = null)
        {
            if (string.IsNullOrEmpty(a1Notation))
            {
                return;
            }

            Match m = regex.Match(a1Notation);
            if (m.Success)
            {
                Col = m.Groups[1].Value;
                Row = m.Groups[2].Value;
            }
            else
            {
                Col = a1Notation;
            }

            rangeReference = RangeReference;
        }

        public RangeReference ReplaceCol(string col)
        {
            Guards.NotNull(col, "col cannot be null");
            Col = col;
            return rangeReference;
        }

        public RangeReference OffsetCol(int number)
        {
            Col = SheetHelper.ColFromNumber(SheetHelper.ColToNumber(Col) + number);
            return rangeReference;
        }

        public RangeReference ReplaceRow(int row)
        {
            return ReplaceRow(row.ToString());
        }

        public RangeReference ReplaceRow(string row)
        {
            Guards.NotNull(row, "row cannot be null");
            Row = row;
            return rangeReference;
        }

        public RangeReference RemoveRow()
        {
            Row = "";
            return rangeReference;
        }

        public RangeReference WithoutRow()
        {
            Row = "";
            return rangeReference;
        }

        public RangeReference OffsetRow(int i)
        {
            int.TryParse(Row, out var val);
            if (string.IsNullOrEmpty(Row))
            {
                val = 1;
            }
            Row = (val + i).ToString();
            return rangeReference;
        }

        public string ToString()
        {
            return string.Format("{0}{1}", Col, Row);
        }
    }
}
