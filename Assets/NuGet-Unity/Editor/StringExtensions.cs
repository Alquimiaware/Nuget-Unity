namespace Alquimiaware.NuGetUnity
{
    using System.Globalization;

    public static class StringExtensions
    {
        public static bool Contains(this string text, string term, CompareOptions compareOptions)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(text, term, compareOptions) >= 0;
        }
    }
}
