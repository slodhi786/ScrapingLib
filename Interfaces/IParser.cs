namespace ScrapingLib.Interfaces
{
    /// <summary>
    /// Defines a contract for parsing string values into specified data types.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parses a string value into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to which the string value will be parsed.</typeparam>
        /// <param name="value">The string value to be parsed.</param>
        /// <returns>
        /// The parsed value of type <typeparamref name="T"/>, or the default value of <typeparamref name="T"/> if parsing fails.
        /// </returns>
        T Parse<T>(string value);
    }
}
