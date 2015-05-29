using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdLineParser.Helpers
{
    public static class IListExtensions
    {
        public static void Foreach<T>(this IList<T> enumerable, Action<T, int> action)
        {
            int index = 0;

            foreach (var item in enumerable)
            {
                action(item, index++);
            }
        }
    }
}
