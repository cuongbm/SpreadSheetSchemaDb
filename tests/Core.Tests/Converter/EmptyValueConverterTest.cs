using Core.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class EmptyValueConverterTest
    {
        [Test]
        public void FromInput_Anything_ReturnNull()
        {
            Assert.That(new EmptyValueConverter().FromInput("abc"), Is.EqualTo(null));
            Assert.That(new EmptyValueConverter().FromInput(123), Is.EqualTo(null));
        }
        
        [Test]
        public void ToOutput_Anything_OutputEmpty()
        {
            Assert.That(new EmptyValueConverter().ToOutput("abc"), Is.EqualTo(""));
            Assert.That(new EmptyValueConverter().ToOutput(123), Is.EqualTo(""));
        }
    }
}