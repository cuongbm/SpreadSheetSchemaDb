using Core.Converters;
using Core.Models.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class EnumValueConverterTest
    {
        [Test]
        public void ConvertFromStringInput()
        {
            var converter = new EnumValueConverter<TestEnum>();
            var value = converter.FromInput("1");
            Assert.That(value, Is.EqualTo(TestEnum.ValueA));
            value = converter.FromInput("4");
            Assert.That(value, Is.EqualTo(TestEnum.ValueC));

            value = converter.FromInput("0");
            Assert.That(value, Is.EqualTo(TestEnum.NoValue));

            value = converter.FromInput("");
            Assert.That(value, Is.EqualTo(TestEnum.NoValue));
        }
    }

    public class TestEnumClass
    {
        public string Sample { get; set; }

        public TestEnum TestEnum { get; set; }

        public TestEnumClass()
        {
        }

        public TestEnumClass(TestEnum testEnum)
        {
            TestEnum = testEnum;
        }
    }
}
