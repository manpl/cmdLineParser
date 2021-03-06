﻿using cmdLineParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cmdLineParser.Helpers;

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
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _config = value;
            }
        }

        public Parser()
        {
            Config = new T();
        }

        public T Parse(string input)
        {
            return Parse(input.Split(' '));
        }

        public T Parse(params string[] tokens)
        {
            var pairs = tokens
                .Select(token => token.Split(':'))
                .ToDictionary(pair => pair[0], pair => pair[1]);

            foreach (var kp in pairs)
            {
                SetProperty(kp.Key, kp.Value);
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

        private void SetProperty(String propertName, String propertyValue)
        {
            var property = GetProperty(propertName);

            if (property != null)
            {
                var option = property.GetCustomAttribute<OptionAttribute>();
                if (option != null)
                {
                    option.Validate(propertyValue);
                }

                var type = property.PropertyType;
                Object value = Convert.ChangeType(propertyValue, type);
                property.SetValue(Config, value, null);
            }
            else
            {
                // property does not match anything in config
            }
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

        public String Help()
        {
            String result = "";

            var properties = Config.GetType().GetProperties();
            var propsWithDescription = properties.Where(prop => prop.GetCustomAttribute<DescriptionAttribute>() != null)
                .ToList();
            var count = propsWithDescription.Count;

            propsWithDescription.Foreach((prop, index) =>
            {
                var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                var name = GetPropertyName(prop);
                var description = name + " - " + descAttr.Description;
                result += description;

                if (index < count -1)
                {
                    result += "\n";
                }
            });

            return result;
        }

        private static string GetPropertyName(PropertyInfo property)
        {
            var name = property.Name;
            var aliasAttr = property.GetCustomAttribute<NameAttribute>();
            if (aliasAttr != null)
            {
                name = aliasAttr.Name;
            }
            return name;
        }
    }
}
