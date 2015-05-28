using cmdLineParser;
using cmdLineParserTests.Configs;
using Ploeh.AutoFixture.Xunit2;
using System;
using Xunit;

namespace cmdLineParserTests
{
    public class ParserTests
    {
        [Theory, AutoData]
        public void RequiredFieldSupplied_Parse_ValueReturned(Parser<OneRequiredFieldConfig> parser)
        {
            var result = parser.Parse("SomeRequiredSetting:value");
            Assert.Equal(result.SomeRequiredSetting, "value");
        }

        [Theory, AutoData]
        public void RedundantOptionSupplied_Parse_OptionSkiped(Parser<Object> parser)
        {
            var result = parser.Parse("SomethingElse:value");
        }

        [Theory, AutoData]
        public void RequiredFieldNotSupplied_Parse_ExceptionThrows(Parser<OneRequiredFieldConfig> parser)
        {
            Assert.Throws<ArgumentException>(() => parser.Parse("SomethingElse:value"));
        }

        [Theory, AutoData]
        public void DuplicateValuesSupplied_Parse_ExceptionThrows(Parser<Object> parser)
        {
            Assert.Throws<ArgumentException>(() => parser.Parse("SomethingElse:value SomethingElse:value"));
        }

        [Theory, AutoData]
        public void ValueWithAliasSupplied_Parse_ValueReturned(Parser<NamedFieldConfig> parser)
        {
            var result = parser.Parse("Different:test");
            Assert.Equal("test", result.SomeField);
        }

        [Theory, AutoData]
        public void IntegerSupplied_Parse_ValueReturned(Parser<TypedConfig<int>> parser)
        {
            var result = parser.Parse("Property:1");
            Assert.Equal(1, result.Property);
        }
        
        [Theory, AutoData]
        public void DoubleSupplied_Parse_ValueReturned(Parser<TypedConfig<double>> parser)
        {
            var result = parser.Parse("Property:1,0");
            Assert.Equal(1d, result.Property);
        }

        [Theory, AutoData]
        public void DecimalSupplied_Parse_ValueReturned(Parser<TypedConfig<decimal>> parser)
        {
            var result = parser.Parse("Property:1,0");
            Assert.Equal(1m, result.Property);
        }

        [Theory, AutoData]
        public void MatchingOptionSupplied_Parse_ValueReturned(Parser<OptionsConifg<String>> parser)
        {
            var result = parser.Parse("setting:1");
            Assert.Equal("1", result.Property);
        }

        [Theory, AutoData]
        public void NonMatchingOptionSupplied_Parse_ValueReturned(Parser<OptionsConifg<String>> parser)
        {
            Assert.Throws<ArgumentException>(()=>parser.Parse("setting:unDefined"));
        }

        [Theory, AutoData]
        public void NonStringMatchingOptionSupplied_Parse_ValueReturned(Parser<OptionsConifg<int>> parser)
        {
            var result = parser.Parse("setting:1");
            Assert.Equal(1, result.Property);
        }

        [Theory, AutoData]
        public void ConfigWithSimplePropertyDescription_Help_DescriptionGenerated(Parser<DescriptionConfig> parser)
        {
            var result = parser.Help();
            Assert.Equal("Property - Sample description", result);
        }

        [Theory, AutoData]
        public void ConfigWithAliasedPropertyDescription_Help_DescriptionGenerated(Parser<AliasDescriptionConfig> parser)
        {
            var result = parser.Help();
            Assert.Equal("prop - Sample description", result);
        }

        [Theory, AutoData]
        public void ConfigWithMultiplePropertiesWithDescription_Help_DescriptionGenerated(Parser<MultiPropertyDescription> parser)
        {
            var result = parser.Help();
            var expected = "prop1 - Sample description 1\n";
            expected += "prop2 - Sample description 2";
            Assert.Equal(expected, result);
        }
    }
}
