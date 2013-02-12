using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crisp.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Takes an arg string ("arg1=value", "arg2 = value" etc) and converts to dict.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ParseKeyValuePairs(this string[] arguments)
        {
            var result = new Dictionary<string, object>();

            if (arguments.Length % 2 != 0)
                throw new ArgumentException("Invalid number of arguments (%2 != 0)");

            for (int index = 0; index < arguments.Length; index += 2)
            {
                string key = arguments[index];
                string value = arguments[index + 1];
                result.Add(key, value);
            }

            return result;
        }
    }
}
