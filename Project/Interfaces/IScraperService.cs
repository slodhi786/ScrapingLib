using ScrapingLib.Models;

using System.Collections.Generic;

namespace ScrapingLib.Interfaces
{
    /// <summary>
    /// Defines methods for scraping transaction data from web pages.
    /// </summary>
    public interface IScraperService
    {
        /// <summary>
        /// Scrapes transaction data from the specified URL and returns a list of parsed transactions.
        /// </summary>
        /// <param name="url">The URL of the webpage containing transaction data.</param>
        /// <returns>A list of transactions extracted from the webpage.</returns>
        List<Transaction> ScrapeTransactions(string url);
    }
}
