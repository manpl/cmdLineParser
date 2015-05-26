using cmdLineParser;
using cmdLineParserTests.Configs;
using System;
using Xunit;

namespace cmdLineParserTests
{
    public class ParserTests
    {
        [Fact]
        public void NullConfigPassed_Create_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => {
                new Parser<Object>(null);
            });
        }

        [Fact]
        public void RequiredFieldSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<OneRequiredFieldConfig>(new OneRequiredFieldConfig());
            var result = parser.Parse("SomeRequiredSetting:value");
            Assert.Equal(result.SomeRequiredSetting, "value");
        }

        [Fact]
        public void RedundantOptionSupplied_Parse_OptionSkiped()
        {
            var parser = new Parser<Object>(new Object());
            var result = parser.Parse("SomethingElse:value");
        }

        [Fact]
        public void RequiredFieldNotSupplied_Parse_ExceptionThrows()
        {
            var parser = new Parser<OneRequiredFieldConfig>(new OneRequiredFieldConfig());
            Assert.Throws<ArgumentException>(() => parser.Parse("SomethingElse:value"));
            
        }

        [Fact]
        public void DuplicateValuesSupplied_Parse_ExceptionThrows()
        {
            var parser = new Parser<Object>(new Object());
            Assert.Throws<ArgumentException>(() => parser.Parse("SomethingElse:value SomethingElse:value"));
        }

        [Fact]
        public void ValueWithAliasSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<NamedFieldConfig>(new NamedFieldConfig());
            var result = parser.Parse("Different:test");
            Assert.Equal("test", result.SomeField);
        }

        [Fact]
        public void IntegerSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<TypedConfig<int>>(new TypedConfig<int>());
            var result = parser.Parse("Property:1");
            Assert.Equal(1, result.Property);
        }
        
        [Fact]
        public void DoubleSupplied_Parse_ValueReturned()
        {
            TypedPropertyTest<double>("Property:1,0", 1.0);
        }

        [Fact]
        public void DecimalSupplied_Parse_ValueReturned()
        {
            TypedPropertyTest<decimal>("Property:1,0", 1m);
        }

        public static void TypedPropertyTest<T>(string input, T expected)
        {
            var parser = new Parser<TypedConfig<T>>(new TypedConfig<T>());
            var result = parser.Parse(input);
            Assert.Equal(expected, result.Property);
        }
    }
}
