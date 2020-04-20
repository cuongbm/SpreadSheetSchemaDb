using System;
using Core.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class DoubleValueConverterTest
    {
        [Test]
        public void FromInput_ValidValue()
        {
            var converter = new DoubleValueConverter(new []{"USD"});
            Assert.That(converter.FromInput("6.5"), Is.EqualTo(6.5));
            Assert.That(converter.FromInput("233.43USD"), Is.EqualTo(233.43));
            Assert.That(converter.FromInput(null), Is.EqualTo(0));
        }
        
        [Test]
        public void FromInput_InvalidValue_ThrowException()
        {
            Action act = () => new DoubleValueConverter().FromInput("abc");
            Assert.Throws<FormatException>(() => act());
        }
        
        [Test]
        public void ToOutput()
        {
            var converter = new DoubleValueConverter();
            Assert.That(converter.ToOutput(4.66), Is.EqualTo("4.66"));
        }
    }
}