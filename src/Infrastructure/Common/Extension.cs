using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
    public static class Extension
    {
        static public string UpperCaseFirstCharacter(this string text) {
            return Regex.Replace(text, "^[a-z]", m => m.Value.ToUpper());
        }
    }
}