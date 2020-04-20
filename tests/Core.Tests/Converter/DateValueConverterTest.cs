using System;
using Core.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class DateValueConverterTest
    {
        [Test]
        public void FromInput_ValidValueTest()
        {
            var converter = new DateValueConverter("yyyy-MM-dd");
            Assert.That(converter.FromInput("1990-01-15"), Is.EqualTo(new DateTime(1990, 01, 15)));
            
            converter = new DateValueConverter("yyyy-dd-MM");
            Assert.That(converter.FromInput("1980-06-10  04:5:30"), Is.EqualTo(new DateTime(1980, 10, 6)));
        }
        
        [Test]
        public void FromInput_InvalidValueTest()
        {
            var converter = new DateValueConverter("yyyy-MM-dd");
            Action act = () => converter.FromInput("1990-20-15");
            Assert.Throws<FormatException>(() => act());
        }
        
        [Test]
        public void FromInput_OADdate_TimeIsIgnored()
        {
            var date = new DateTime(2010, 10, 30, 16, 25, 40);
            var converter = new DateValueConverter();
            Assert.That(converter.FromInput(date.ToOADate()), Is.EqualTo(date.Date));
        }
    }
}