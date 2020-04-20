using Core.Schemas;
using Core.Test;
using Core.Tests.Converter;
using NUnit.Framework;

namespace Core.Tests.Schemas
{
    public class SchemaModelConverterTest
    {
        [Test]
        public void MatchEnumMapToOneProperty()
        {
            PropertySchema testSchema = new PropertySchema();
            testSchema.SystemPropertyDefinitions.Add(new SystemPropertyDefinition()
            {
                Value = "EnumProperty",
                Type = SchemaPropertyType.MatchEnum,
                Text = "Enum test",
                EnumType = typeof(TestEnum),
                MatchEnumValue = TestEnum.ValueA
            });
            SchemaModelConverter converter = new SchemaModelConverter(testSchema);
            var result = converter.Parse<TestEntity>(TestHelper.ListFromString("1 1 1"));

            Assert.That(result.EnumProperty.HasFlag(TestEnum.ValueA));
        }

        [Test]
        public void MultipleMatchEnumMapToOneProperty()
        {
            PropertySchema testSchema = new PropertySchema();
            testSchema.SystemPropertyDefinitions.Add(new SystemPropertyDefinition()
            {
                Value = "EnumProperty",
                Type = SchemaPropertyType.MatchEnum,
                Text = "district management",
                EnumType = typeof(TestEnum),
                MatchEnumValue = TestEnum.ValueA,
                OffsetPos = 1
            });

            testSchema.SystemPropertyDefinitions.Add(new SystemPropertyDefinition()
            {
                Value = "EnumProperty",
                Type = SchemaPropertyType.MatchEnum,
                Text = "province",
                EnumType = typeof(TestEnum),
                MatchEnumValue = TestEnum.ValueB,
                OffsetPos = 2
            });
            SchemaModelConverter converter = new SchemaModelConverter(testSchema);
            var result = converter.Parse<TestEntity>(TestHelper.ListFromString("1 1 1"));

            Assert.That(result.EnumProperty.HasFlag(TestEnum.ValueA));
            Assert.That(result.EnumProperty.HasFlag(TestEnum.ValueB));

            result = converter.Parse<TestEntity>(TestHelper.ListFromString("1 0 1"));

            Assert.That(result.EnumProperty.HasFlag(TestEnum.ValueA), Is.False);
            Assert.That(result.EnumProperty.HasFlag(TestEnum.ValueB), Is.True);
        }
    }

    public class TestEntity
    {
        public TestEnum EnumProperty { get; set; }
    }
}
