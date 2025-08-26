using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
