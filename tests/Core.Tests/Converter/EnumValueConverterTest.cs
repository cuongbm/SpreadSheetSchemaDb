using System;
using Core.Converters;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using NUnit.Framework;

namespace Core.Tests.Converter
{
    public class EnumValueConverterTest
    {
        [Test]
        public void FromInput_NumberValue()
        {
            var converter = new EnumValueConverter<TestEnum>();
            var value = converter.FromInput("1");
            Assert.That(value, Is.EqualTo(TestEnum.ValueA));
            
            value = converter.FromInput(1);
            Assert.That(value, Is.EqualTo(TestEnum.ValueA));
            
            value = converter.FromInput("4");
            Assert.That(value, Is.EqualTo(TestEnum.ValueC));

            value = converter.FromInput("0");
            Assert.That(value, Is.EqualTo(TestEnum.NoValue));

            value = converter.FromInput("");
            Assert.That(value, Is.EqualTo(TestEnum.NoValue));
        }

        [Test]
        public void ToOutput_NumberValue()
        {
            var converter = new EnumValueConverter<TestEnum>();
            Assert.That(converter.ToOutput(TestEnum.ValueB), Is.EqualTo("2"));
        }
        
        [Test]
        public void SetProperty_SetToCorrectProperty()
        {
            var obj1 = new TestClass(TestEnum.ValueB);
            var converter = new EnumValueConverter<TestEnum>();

            converter.SetProperty(obj1, "Property1", TestEnum.ValueC);
                
            Assert.That(obj1.Property1, Is.EqualTo(TestEnum.ValueC));
        }
        
        [Test]
        public void SetProperty_InvalidPropertyName_ThrowException()
        {
            var obj1 = new TestClass(TestEnum.ValueB);
            var converter = new EnumValueConverter<TestEnum>();

            Action act = () => converter.SetProperty(obj1, "Property3", TestEnum.ValueC);

            Assert.Throws<ArgumentException>(() => act());
        }
    }

    class TestClass
    {
        public TestEnum Property1 { get; set; }
        
        public TestEnum Property2 { get; }

        public TestClass(TestEnum property1)
        {
            Property1 = property1;
        }
    }
}
