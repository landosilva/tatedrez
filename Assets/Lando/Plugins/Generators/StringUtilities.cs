using System.Text.RegularExpressions;

namespace LightningRod.Utilities
{
    public static class StringUtilities
    {
        private const string WHITESPACE_PATTERN = @"\s+";
        private static readonly Regex _whitespaceRegex = new(WHITESPACE_PATTERN);
        
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static string ReplaceWhitespace(string input, string replacement = "") 
            => _whitespaceRegex.Replace(input, replacement);
        
        public static string Indent(int level) => "".PadLeft(level * 4);
    }
}
