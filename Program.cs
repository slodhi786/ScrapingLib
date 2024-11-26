using Microsoft.Extensions.DependencyInjection;

using ScrapingLib.Extensions;
using ScrapingLib.Interfaces;

namespace ScrapingLib
{
    /// <summary>
    /// Entry point for the scraping application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method initializes the application, sets up services, and executes the scraping workflow.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Step 1: Configure dependency injection using the extension method.
            var services = new ServiceCollection();
            services.AddApplicationServices();

            // Build the service provider to resolve dependencies.
            var provider = services.BuildServiceProvider();

            // Step 2: Define the URL to scrape.
            string url = "https://tck.gorselpanel.com/task/hareket.html";

            // Step 3: Resolve services and execute the workflow.
            var scraperService = provider.GetService<IScraperService>();
            var transactionService = provider.GetService<ITransactionService>();

            var transactions = scraperService?.ScrapeTransactions(url);
            if (transactions?.Count == 0) return;

            var (latestDeposit, oldestWithin48Hours, oldestWithdraw) = transactionService.ProcessTransactions(transactions);

            if (oldestWithdraw != null)
            {
                var finalBalance = transactionService.CalculateBalances(transactions, oldestWithdraw);
                Console.WriteLine($"Final balance: {finalBalance}");
            }
        }
    }
}
