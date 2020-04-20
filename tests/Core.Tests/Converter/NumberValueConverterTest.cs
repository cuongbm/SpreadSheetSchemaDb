using System;
using Core.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class NumberValueConverterTest
    {
        [Test]
        public void FromInput_ValidInput()
        {
            Assert.That(new NumberValueConverter().FromInput("32143"), Is.EqualTo(32143L));
            Assert.That(new NumberValueConverter().FromInput(-123), Is.EqualTo(-123L));
        }
        
        [Test]
        public void FromInput_InvalidInput_ThrowException()
        {
            Action act = () => new NumberValueConverter().FromInput("32d1d43");
            Assert.Throws<FormatException>(() => act());
        }
        
        [Test]
        public void ToOutput()
        {
            Assert.That(new NumberValueConverter().ToOutput(435), Is.EqualTo("435"));
        }
    }
}