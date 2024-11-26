using ScrapingLib.Interfaces;

namespace ScrapingLib.Services
{
    /// <summary>
    /// A parser service for converting string values into a specified type.
    /// </summary>
    public class Parser : IParser
    {
        /// <summary>
        /// Parses the given string value into the specified type.
        /// Handles conversions to common types and provides a default value on failure.
        /// </summary>
        /// <typeparam name="T">The target type to parse the value into.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <returns>
        /// Parsed value of type T, or the default value of T if parsing fails or the input is null/empty.
        /// </returns>
        public T Parse<T>(string value)
        {
            // Return the default value for the type if the input string is null, empty, or whitespace.
            if (string.IsNullOrWhiteSpace(value)) return default;

            try
            {
                // Special case: If the target type is boolean, interpret "1" as true and other values as false.
                if (typeof(T) == typeof(bool)) return (T)(object)(value.Trim() == "1");

                // For other types, use Convert.ChangeType for generic type conversion.
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                // If any exception occurs during conversion, return the default value for the type.
                return default;
            }
        }
    }
}
