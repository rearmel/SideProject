using System.Text.RegularExpressions;

namespace SideProject.Helpers
{
    public static class AssetHelper
    {
        public static bool IsValidCode(string code)
        {
            return !string.IsNullOrWhiteSpace(code) && Regex.IsMatch(code, @"^[a-zA-Z0-9]+$");
        }
    }
}
