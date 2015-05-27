using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute(String description)
        {
            this.Description = description;
        }
    }
}
