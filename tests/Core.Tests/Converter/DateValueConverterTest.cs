using System;
using Core.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class DateValueConverterTest
    {
        [Test]
        public void FromInput_ValidValue()
        {
            var converter = new DateValueConverter("yyyy-MM-dd");
            Assert.That(converter.FromInput("1990-01-15"), Is.EqualTo(new DateTime(1990, 1, 15)));
            
            converter = new DateValueConverter("yyyy-dd-MM");
            Assert.That(converter.FromInput("1980-06-10  04:5:30"), Is.EqualTo(new DateTime(1980, 10, 6)));
            Assert.That(converter.FromInput("1980-06-10T04:5:30Z"), Is.EqualTo(new DateTime(1980, 10, 6)));
        }
        
        [Test]
        public void FromInput_InvalidValue_ThrowException()
        {
            var converter = new DateValueConverter("yyyy-MM-dd");
            Action act = () => converter.FromInput("1990-20-15");
            Assert.Throws<FormatException>(() => act());
        }

        [Test]
        public void FromInput_Null_ReturnNull()
        {
            var converter = new DateValueConverter("yyyy-MM-dd");
            Assert.That(converter.FromInput(null), Is.EqualTo(null));
        }
        
        [Test]
        public void FromInput_OADdate_TimeIsIgnored()
        {
            var date = new DateTime(2010, 10, 30, 16, 25, 40);
            var converter = new DateValueConverter();
            Assert.That(converter.FromInput(date.ToOADate()), Is.EqualTo(date.Date));
        }
        
        [Test]
        public void ToOutput_FormatIsRespected()
        {
            var date = new DateTime(2010, 10, 30, 16, 25, 40);
            var converter = new DateValueConverter("yyyy-MM-dd");
            Assert.That(converter.ToOutput(date), Is.EqualTo("2010-10-30"));
        }
    }
}