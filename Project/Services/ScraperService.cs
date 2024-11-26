using HtmlAgilityPack;

using ScrapingLib.Interfaces;
using ScrapingLib.Models;

namespace ScrapingLib.Services
{
    /// <summary>
    /// Implementation of the IScraperService interface.
    /// Provides functionality to scrape transaction data from an HTML table on a web page.
    /// </summary>
    public class ScraperService : IScraperService
    {
        private readonly HtmlWeb _web;
        private readonly ILogger _logger;
        private readonly IParser _parser;

        /// <summary>
        /// Initializes a new instance of the ScraperService class.
        /// </summary>
        /// <param name="logger">Logger to track the scraping process.</param>
        /// <param name="parser">Parser to convert raw HTML cell data into typed values.</param>
        public ScraperService(ILogger logger, IParser parser)
        {
            _web = new HtmlWeb();
            _logger = logger;
            _parser = parser;
        }

        /// <summary>
        /// Scrapes transaction data from the specified URL.
        /// </summary>
        /// <param name="url">The URL of the webpage containing transaction data.</param>
        /// <returns>A list of Transaction objects extracted from the HTML table.</returns>
        public List<Transaction> ScrapeTransactions(string url)
        {
            _logger.Log("Starting to scrape transactions...");
            var transactions = new List<Transaction>();

            try
            {
                // Load the HTML document from the provided URL.
                var document = _web.Load(url);

                // Select all table rows from the document.
                var rows = document.DocumentNode.SelectNodes("//table/tr");
                if (rows == null)
                {
                    _logger.Log("No rows found in the table.");
                    return transactions;
                }

                // Iterate through each row in the table.
                foreach (var row in rows)
                {
                    // Extract cells from the row.
                    var cells = row.SelectNodes("td");

                    // Skip rows that don't have the expected number of cells.
                    if (cells == null || cells.Count < 8) continue;

                    // Parse each cell and create a new Transaction object.
                    transactions.Add(new Transaction
                    {
                        ID = _parser.Parse<int>(cells[0].InnerText),
                        TransID = _parser.Parse<long>(cells[1].InnerText),
                        CustomerCode = _parser.Parse<int>(cells[2].InnerText),
                        Time = _parser.Parse<DateTime>(cells[3].InnerText),
                        TransactionType = _parser.Parse<int>(cells[4].InnerText),
                        Amount = _parser.Parse<decimal>(cells[5].InnerText),
                        IsDeposit = _parser.Parse<bool>(cells[6].InnerText),
                        IsWithdraw = _parser.Parse<bool>(cells[7].InnerText)
                    });
                }

                _logger.Log($"Scraped {transactions.Count} transactions.");
            }
            catch (Exception ex)
            {
                // Log any errors encountered during the scraping process.
                _logger.Log($"Error scraping transactions: {ex.Message}");
            }

            return transactions;
        }
    }
}
