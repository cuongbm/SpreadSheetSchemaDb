﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Core.Helpers
{
    public class FlagConverter : StringEnumConverter
    {
        public bool IsMultipleValue { get; }

        public FlagConverter() : this(true)
        { }

        public FlagConverter(bool isMultipleValue)
        {
            IsMultipleValue = isMultipleValue;
        }

        public override object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            bool isNullable = (Nullable.GetUnderlyingType(objectType) != null);
            Type enumType = (Nullable.GetUnderlyingType(objectType) ?? objectType);
            if (!enumType.IsEnum)
                throw new JsonSerializationException(string.Format("type {0} is not a enum type", enumType.FullName));
            var prefix = enumType.Name + "_";

            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                    throw new JsonSerializationException();
                return null;
            }

            var token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                token = string.Join(", ", token.ToString().Split(',').Select(s => s.Trim()).ToArray());
            }
            else if (token.Type == JTokenType.Array)
            {
                token = string.Join(", ", token.Select(s => s.ToString().Trim()).ToArray());
            }

            if (string.IsNullOrEmpty(token.ToString()))
            {
                return null;
            }

            using (var subReader = token.CreateReader())
            {
                while (subReader.TokenType == JsonToken.None)
                    subReader.Read();
                return base.ReadJson(subReader, objectType, existingValue, serializer); // Use base class to convert
            }
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            var flags = value.ToString();
            if (!IsMultipleValue)
            {
                writer.WriteRawValue($"\"{flags}\"");
                return;
            }

            var flagsArr = flags.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
            .Select(f => $"\"{f.Trim()}\"");

            writer.WriteRawValue($"[{string.Join(",", flagsArr)}]");
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
