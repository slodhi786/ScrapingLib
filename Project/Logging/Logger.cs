using ScrapingLib.Interfaces;

namespace ScrapingLib.Services
{
    /// <summary>
    /// Provides logging functionality for recording messages to both the console and a log file.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly string _logFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// Sets the file path for the log file to be located in the base directory.
        /// </summary>
        public Logger()
        {
            // Define the log file path based on the application's base directory
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        }

        /// <summary>
        /// Logs a message to both the console and a log file. 
        /// Optionally appends the message to the log file based on the specified flag.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="logInConsole">A flag indicating whether to log the message to the console. Defaults to <c>true</c>.</param>
        public void Log(string message, bool logInConsole = true)
        {
            try
            {
                if (logInConsole)
                {
                    // Output to console
                    Console.WriteLine(message);
                }

                // Append the log message to the log file with timestamp
                File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Handle any errors during the logging process
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}
