﻿using Core.Exceptions;
using Core.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Core.Schemas
{
    public class PropertySchema
    {
        /// <summary>
        /// a1 notaion of data range without sheet name. e.g: A1:Z
        /// </summary>
        public string DataCellRange { get; set; } = "";

        /// <summary>
        /// a1 notaion of id range without sheet name. e.g: C:F
        /// </summary>
        public string IdCellRange { get; set; } = "";

        /// <summary>
        /// Offset with IdCellRange. E.g IdSheetRange C:E, key column is D then KeyColumnOffsetPos should = 1
        /// </summary>
        public int KeyColumnOffsetPos { get; set; }

        public int KeyColumnOffsetPosWithDataCellRange
        {
            get
            {
                return DiffBetweenIdColAndDataCol() + KeyColumnOffsetPos;
            }
        }

        /// <summary>
        /// Offset with IdSheetRange. E.g IdSheetRange C:E, second key column is E then KeyColumnOffsetPos should = 2
        /// </summary>
        public int? SecondKeyColumnOffsetPos { get; set; }

        public int? SecondKeyColumnOffsetPosWithDataCellRange
        {
            get
            {
                if (!SecondKeyColumnOffsetPos.HasValue) return null;
                return DiffBetweenIdColAndDataCol() + SecondKeyColumnOffsetPos.Value;
            }
        }

        /// <summary>
        /// Offset with DataCellRange. E.g DataSheetRange A:W, key column is E then CodeColumnOffsetPos should = 4
        /// </summary>
        public int CodeColumnOffsetPos { get; set; }

        public List<SystemPropertyDefinition> SystemPropertyDefinitions { get; } = new List<SystemPropertyDefinition>();

        public List<CustomPropertyDefinition> CustomPropertyDefinitions { get; } = new List<CustomPropertyDefinition>();

        public CustomPropertyDefinition GetCustomPropertyDefinition(string name)
        {
            return CustomPropertyDefinitions.SingleOrDefault(x => x.Value == name);
        }

        public BasePropertyDefinition GetDefinition(string name)
        {
            BasePropertyDefinition result = null;
            result = SystemPropertyDefinitions.SingleOrDefault(x => x.Value == name);

            if (result == null)
            {
                result = CustomPropertyDefinitions.SingleOrDefault(x => x.Value == name);
            }

            return result;
        }

        public BasePropertyDefinition GetDefinition(string name, bool strict)
        {
            var definition = GetDefinition(name);
            if (definition == null)
            {
                if (strict)
                {
                    throw new BusinessException(Resources.PropertyNotFound, name);
                }
                else
                {
                    return null;
                }
            }
            return definition;
        }

        public BasePropertyDefinition GetDefinitionByLabel(string text)
        {
            BasePropertyDefinition result = null;
            result = SystemPropertyDefinitions.SingleOrDefault(x => x.Text.Equals(text, System.StringComparison.OrdinalIgnoreCase));

            if (result == null)
            {
                result = CustomPropertyDefinitions.SingleOrDefault(x => x.Text.Equals(text, System.StringComparison.OrdinalIgnoreCase));
            }

            return result;
        }

        public int DiffBetweenIdColAndDataCol()
        {
            long idCol = string.IsNullOrEmpty(IdCellRange) ? 0 : SheetHelper.ColToNumber(new RangeReference(IdCellRange).From.Col);
            long startCol = string.IsNullOrEmpty(DataCellRange) ? 0 : SheetHelper.ColToNumber(new RangeReference(DataCellRange).From.Col);
            return (int)(idCol - startCol);
        }

        public static PropertySchema Create(CustomPropertyDefinition customPropertyDefinition)
        {
            var result = new PropertySchema();
            return result.AddCustomDefinition(customPropertyDefinition);
        }

        public static PropertySchema Create(SystemPropertyDefinition systemPropertyDefinition)
        {
            var result = new PropertySchema();
            return result.AddSystemDefinition(systemPropertyDefinition);
        }

        public static PropertySchema Create(string dataCellRange)
        {
            var result = new PropertySchema();
            return result.SetDataCellRange(dataCellRange);
        }

        public PropertySchema SetDataCellRange(string dataCellRange)
        {
            DataCellRange = dataCellRange;
            return this;
        }

        public PropertySchema SetIdCellRange(string idCellRange)
        {
            IdCellRange = idCellRange;
            return this;
        }

        public PropertySchema SetKeyColumnOffsetPos(int keyColumnOffsetPos)
        {
            KeyColumnOffsetPos = keyColumnOffsetPos;
            return this;
        }

        public PropertySchema SetSecondKeyColumnOffsetPos(int secondKeyColumnOffsetPos)
        {
            SecondKeyColumnOffsetPos = secondKeyColumnOffsetPos;
            return this;
        }
        public PropertySchema SetCodeColumnOffsetPos(int codeColumnOffsetPos)
        {
            CodeColumnOffsetPos = codeColumnOffsetPos;
            return this;
        }

        public PropertySchema AddSystemDefinition(SystemPropertyDefinition definition)
        {
            SystemPropertyDefinitions.Add(definition);
            return this;
        }

        public PropertySchema AddCustomDefinition(CustomPropertyDefinition definition)
        {
            CustomPropertyDefinitions.Add(definition);
            return this;
        }
    }
}
