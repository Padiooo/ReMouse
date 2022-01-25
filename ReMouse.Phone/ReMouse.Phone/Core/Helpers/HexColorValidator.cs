using System.Text.RegularExpressions;

namespace ReMouse.Phone.Core.Helpers
{
    public static class HexColorValidator
    {
        private static readonly Regex regexHex1 = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$", RegexOptions.Compiled);
        private static readonly Regex regexHex2 = new Regex("^#(?:[0-9a-fA-F]{3,4}){1,2}$", RegexOptions.Compiled);

        public static bool IsValidColorHex(string text)
        {
            return text != null && (regexHex1.IsMatch(text) || regexHex2.IsMatch(text));
        }
    }
}
