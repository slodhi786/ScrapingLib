namespace ScrapingLib.Interfaces
{
    /// <summary>
    /// Defines a contract for logging messages in the application.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message to the configured output.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        void Log(string message);
    }
}
