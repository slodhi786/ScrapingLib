namespace ScrapingLib.Interfaces
{
    /// <summary>
    /// Defines a contract for logging messages in the application.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message to the configured output (console and/or log file).
        /// The message is appended to the log file by default, unless specified otherwise.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="logInConsole">A flag indicating whether to log the message to the console. Defaults to <c>true</c>.</param>
        void Log(string message, bool logInConsole = true);
    }
}
