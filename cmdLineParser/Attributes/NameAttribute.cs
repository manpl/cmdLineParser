using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParser.Attributes
{
    public class NameAttribute : Attribute
    {
        public String Name { get; set; }

        public NameAttribute(String name)
        {
            Name = name;
        }
    }
}
