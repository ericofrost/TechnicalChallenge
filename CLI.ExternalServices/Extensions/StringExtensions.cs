namespace CLI.ExternalServices.Extensions
{
    /// <summary>
    /// Class with string extensions helper methods
    /// </summary>
    public static class StringExtensions
    {
        // <summary>
        /// Gets the text between curly braces
        /// </summary>
        /// <param name="input">The text to filter</param>
        /// <returns>The text between curly braces</returns>
        public static string RemoveCurlyBraces(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Replace("{", string.Empty).Replace("}", string.Empty);
        }
    }
}
