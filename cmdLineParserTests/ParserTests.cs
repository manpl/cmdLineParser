using cmdLineParser;
using cmdLineParserTests.Configs;
using System;
using Xunit;

namespace cmdLineParserTests
{
    public class ParserTests
    {
        [Fact]
        public void RequiredFieldSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<OneRequiredFieldConfig>();
            var result = parser.Parse("SomeRequiredSetting:value");
            Assert.Equal(result.SomeRequiredSetting, "value");
        }

        [Fact]
        public void RedundantOptionSupplied_Parse_OptionSkiped()
        {
            var parser = new Parser<Object>();
            var result = parser.Parse("SomethingElse:value");
        }

        [Fact]
        public void RequiredFieldNotSupplied_Parse_ExceptionThrows()
        {
            var parser = new Parser<OneRequiredFieldConfig>();
            Assert.Throws<ArgumentException>(() => parser.Parse("SomethingElse:value"));
            
        }

        [Fact]
        public void DuplicateValuesSupplied_Parse_ExceptionThrows()
        {
            var parser = new Parser<Object>();
            Assert.Throws<ArgumentException>(() => parser.Parse("SomethingElse:value SomethingElse:value"));
        }

        [Fact]
        public void ValueWithAliasSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<NamedFieldConfig>();
            var result = parser.Parse("Different:test");
            Assert.Equal("test", result.SomeField);
        }

        [Fact]
        public void IntegerSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<TypedConfig<int>>();
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
            var parser = new Parser<TypedConfig<T>>();
            var result = parser.Parse(input);
            Assert.Equal(expected, result.Property);
        }

        [Fact]
        public void MatchingOptionSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<OptionsConifg<String>>();
            var result = parser.Parse("setting:1");
            Assert.Equal("1", result.Property);
        }

        [Fact]
        public void NonMatchingOptionSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<OptionsConifg<String>>();
            Assert.Throws<ArgumentException>(()=>parser.Parse("setting:unDefined"));
        }

        [Fact]
        public void NonStringMatchingOptionSupplied_Parse_ValueReturned()
        {
            var parser = new Parser<OptionsConifg<int>>();
            var result = parser.Parse("setting:1");
            Assert.Equal(1, result.Property);
        }

        [Fact]
        public void ConfigWithSimplePropertyDescription_Help_DescriptionGenerated()
        {
            var parser = new Parser<DescriptionConfig>();
            var result = parser.Help();
            Assert.Equal("Property - Sample description", result);
        }

        [Fact]
        public void ConfigWithAliasedPropertyDescription_Help_DescriptionGenerated()
        {
            var parser = new Parser<AliasDescriptionConfig>();
            var result = parser.Help();
            Assert.Equal("prop - Sample description", result);
        }

        [Fact]
        public void ConfigWithMultiplePropertiesWithDescription_Help_DescriptionGenerated()
        {
            var parser = new Parser<MultiPropertyDescription>();
            var result = parser.Help();
            var expected = "prop1 - Sample description 1\n";
            expected += "prop2 - Sample description 2";
            Assert.Equal(expected, result);
        }
    }
}
