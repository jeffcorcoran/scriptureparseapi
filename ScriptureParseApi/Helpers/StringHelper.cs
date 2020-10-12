namespace ScriptureParseApi
{
    public static class StringHelper
    {
        public static bool NullOrEmpty(this string val)
        {
            return (val == null || val == string.Empty);
        }
    }
}
