using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionAttribute : Attribute
    {
        public String[] Options { get; private set; }

        public OptionAttribute(params String[] options)
        {
            Options = options;
        }

        public bool Matches(String value)
        {
            return Options.Any(item => item == value);
        }
    }
}
