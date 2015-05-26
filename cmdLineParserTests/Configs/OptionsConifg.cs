using cmdLineParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParserTests.Configs
{
    public class OptionsConifg<T>
    {
        [Name("setting"), Option("1", "a", "c", "anotherOne")]
        public T Property { get; set; }
    }
}
