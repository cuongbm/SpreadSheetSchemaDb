using System;
using Core.Converters;
using Core.Models.Converters;
using Core.Schemas;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace Core.Tests.Converter
 {
     public class ConverterFactoryTest
     {
         [TestCase(TestEnum.ValueA, TestName = "GetMatchedEnumConverter_SingleEnumValue")]
         [TestCase(TestEnum.ValueB | TestEnum.ValueC,
             TestName = "GetMatchedEnumConverter_FlagEnumValues")]
         public void GetMatchedEnumConverter_StrongTypeMatchedEnum_Test(Enum matchEnumValue)
         {
             var definition = new SystemPropertyDefinition();
             definition.Type = SchemaPropertyType.MatchEnum;
             definition.EnumType = typeof(TestEnum);
             definition.MatchEnumValue = matchEnumValue;

             var converter = ConverterFactory.GetConverter(definition);

             Assert.That(converter is MatchEnumValueConverter<TestEnum>);
             Assert.That(((MatchEnumValueConverter<TestEnum>) converter).Value,
                 Is.EqualTo(new EnumValueConverter<TestEnum>().FromInput(matchEnumValue)));
         }
     }
 }
