using cmdLineParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParserTests.Configs
{
    class MultiPropertyDescription
    {
        [Name("prop1"), Description("Sample description 1")]
        public String Property1 { get; set; }

        [Name("prop2"), Description("Sample description 2")]
        public String Property2 { get; set; }
    }
}
