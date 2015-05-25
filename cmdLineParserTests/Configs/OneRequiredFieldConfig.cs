using cmdLineParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParserTests.Configs
{
    public class OneRequiredFieldConfig
    {
        [Required]
        public String SomeRequiredSetting { get; set; }
    }
}
