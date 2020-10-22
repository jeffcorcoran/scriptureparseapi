using System.Collections.Generic;
using System.Linq;

namespace ScriptureParseApi
{
    public static class StringHelper
    {
        public static bool NullOrEmpty(this string val)
        {
            return (val == null || val == string.Empty);
        }

        public static bool IsNumeric(this string val)
        {
            return (int.TryParse(val, out _));
        }

        public static bool IsNumeric(this IEnumerable<string> vals)
        {
            return vals.All(x => int.TryParse(x, out _));
        }
    }
}
