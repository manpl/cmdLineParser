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

    }
}
