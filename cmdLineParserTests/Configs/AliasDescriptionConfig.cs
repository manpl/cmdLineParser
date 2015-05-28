using cmdLineParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParserTests.Configs
{
    public class AliasDescriptionConfig
    {
        [Name("prop"), Description("Sample description")]
        public String Property { get; set; }
    }
}
