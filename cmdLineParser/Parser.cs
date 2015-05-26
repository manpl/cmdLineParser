using cmdLineParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParser
{
    public class Parser<T> where T : new()
    {
        private T _config;
        private T Config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
            }
        }

        public Parser(T config)
        {
            if (config == null)
            {
                throw new ArgumentNullException();
            }

            Config = config;
        }

        public T Parse(string input)
        {
            var pairs = input.Split(' ')
                .Select(token => token.Split(':'))
                .ToDictionary(pair => pair[0], pair => pair[1]);

            foreach (var kp in pairs)
            {
                var property = GetProperty(kp.Key);

                if (property != null)
                {
                    var type = property.PropertyType;
                    Object value = Convert.ChangeType(kp.Value, type);
                    property.SetValue(Config, value, null);
                }
                else
                {
                    // property does not match anything in config
                }
            }

            var properties = Config.GetType().GetProperties();

            properties.ToList().ForEach(prop =>
            {
                if (prop.GetCustomAttribute<RequiredAttribute>() != null && !pairs.ContainsKey(prop.Name))
                {
                    throw new ArgumentException("No value found for " + prop.Name);
                }
            });

            return Config;
        }

        private PropertyInfo GetProperty(string name)
        {
            var byName = Config.GetType().GetProperties().SingleOrDefault(prop => prop.Name == name);

            if (byName != null)
            {
                return byName;
            }

            var byAlias = Config.GetType().GetProperties().SingleOrDefault(prop =>
            {
                var nameAtt = prop.GetCustomAttribute<NameAttribute>();
                return nameAtt == null ?  false : nameAtt.Name == name;
            });

            return byAlias;
        }
    }
}
