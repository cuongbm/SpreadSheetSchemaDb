using System;
using Core.Converters;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class DateTimeValueConverterTest
    {
        [Test]
        public void FromInput_ValidValue()
        {
            var converter = new DateTimeValueConverter("yyyy-MM-ddTHH\\:mm\\:ssK");
            Assert.That(converter.FromInput("2008-09-22T21:57:31+04:00"), 
                Is.EqualTo(new DateTime(2008, 9, 22, 17, 57,31,DateTimeKind.Utc)));
            
            converter = new DateTimeValueConverter("yyyy-MM-ddTHH\\:mm\\:ssZ");
            Assert.That(converter.FromInput("2008-09-22T13:20:31Z"), 
                Is.EqualTo(new DateTime(2008, 9, 22, 13, 20,31,DateTimeKind.Utc)));
            
            converter = new DateTimeValueConverter();
            Assert.That(converter.FromInput("2008-09-22T13:20:31Z"), 
                Is.EqualTo(new DateTime(2008, 9, 22, 13, 20,31,DateTimeKind.Utc)));
        }
        
         
        [Test]
        public void FromInput_InvalidValue_ThrowException()
        {
            var converter = new DateTimeValueConverter();
            Action act = () => converter.FromInput("1990-20-15 04:05:00");
            Assert.Throws<FormatException>(() => act());
        }
        
        [Test]
        public void FromInput_Null_ReturnNull()
        {
            var converter = new DateTimeValueConverter();
            Assert.That(converter.FromInput(null), Is.EqualTo(null));
        }
        
        [Test]
        public void FromInput_OADdate_TimeIsIgnored()
        {
            var date = new DateTime(2010, 10, 30, 16, 25, 40);
            var converter = new DateTimeValueConverter();
            Assert.That(converter.FromInput(date.ToOADate()), Is.EqualTo(date));
        }
        
          
        [Test]
        public void ToOutput_FormatIsRespected()
        {
            var date = new DateTime(2010, 10, 30, 16, 25, 40, DateTimeKind.Utc);
            var converter = new DateTimeValueConverter();
            Assert.That(converter.ToOutput(date), Is.EqualTo("2010-10-30T16:25:40Z"));
        }
    }
}