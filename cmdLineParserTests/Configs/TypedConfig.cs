using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParserTests.Configs
{
    public class TypedConfig<T>
    {
        public T Property { get; set; }
    }
}
