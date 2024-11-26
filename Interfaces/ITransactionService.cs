using ScrapingLib.Models;

namespace ScrapingLib.Interfaces
{
    public interface ITransactionService
    {
        /// <summary>
        /// Processes a list of transactions to identify key entries:
        /// the latest deposit, the oldest deposit within a 48-hour window, 
        /// and the oldest withdrawal within 24 hours of the identified deposit.
        /// </summary>
        /// <param name="transactions">The list of transactions to process.</param>
        /// <returns>A tuple containing the identified transactions.</returns>
        (Transaction LatestDeposit, Transaction OldestWithin48Hours, Transaction OldestWithdraw) ProcessTransactions(List<Transaction> transactions);

        /// <summary>
        /// Calculates the total balance starting from the time of the oldest withdrawal,
        /// excluding certain transaction types.
        /// </summary>
        /// <param name="transactions">The list of transactions to process.</param>
        /// <param name="oldestWithdraw">The oldest withdrawal to use as the starting point.</param>
        /// <returns>The calculated total balance.</returns>
        decimal CalculateBalances(List<Transaction> transactions, Transaction oldestWithdraw);
    }
}
