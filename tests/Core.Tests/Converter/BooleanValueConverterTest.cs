using Core.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class BooleanValueConverterTest
    {
        [Test]
        public void FromInputTest()
        {
            var converter  = new BooleanValueConverter();
            Assert.That(converter.FromInput("1"), Is.EqualTo(true));
            Assert.That(converter.FromInput("0"), Is.EqualTo(false));
            
            Assert.That(converter.FromInput(true), Is.EqualTo(true));
            Assert.That(converter.FromInput(false), Is.EqualTo(false));
        }
        
        [Test]
        public void ToOutputTest()
        {
            var converter  = new BooleanValueConverter();
            Assert.That(converter.ToOutput(true), Is.EqualTo("1"));
            Assert.That(converter.ToOutput(false), Is.EqualTo(""));
        }
    }
}