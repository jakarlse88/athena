using System.Text.RegularExpressions;

namespace Athena.Models.Validators
{
    internal static class ValidationRegex
    {
        internal static Regex ValidAlphabetic =>
            new Regex(@"^[a-zA-Z ]*$");
        internal static Regex ValidHangeul =>
            new Regex(@"^[\p{IsHangulSyllables} ]*$");

        internal static Regex ValidHanja =>
            new Regex(@"^[\p{IsCJKUnifiedIdeographs} ]*$");
    }
}