namespace Athena.Models.Validators
{
    internal static class ValidationRegex
    {
        /// <summary>
        /// String must contain alphabetic (lower and upper-case) characters and spaces only.
        /// Furthermore, it can neither start nor end with a space,
        /// and more than one space between words/characters is disallowed.
        /// </summary>
        internal const string ValidAlphabetic =
            @"^[A-Za-z]+( [A-Za-z]+)*$";

        /// <summary>
        /// String must contain only hangeul vowels and spaces.
        /// Furthermore, it can neither start nor end with a space,
        /// and more than one space between words/characters is disallowed.
        /// </summary>
        internal const string ValidHangeul =
            @"^\p{IsHangulSyllables}+( \p{IsHangulSyllables}+)*$";
            

        /// <summary>
        /// String must contain only hanja characters and spaces.
        /// Furthermore, it can neither start nor end with a space,
        /// and more than one space between words/characters is disallowed.
        /// </summary>
        internal const string ValidHanja =
            @"^\p{IsCJKUnifiedIdeographs}+( \p{IsCJKUnifiedIdeographs}+)*$";
    }
}