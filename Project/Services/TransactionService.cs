using ScrapingLib.Interfaces;
using ScrapingLib.Models;

namespace ScrapingLib.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="logger">An implementation of the <see cref="ILogger"/> interface for logging.</param>
        public TransactionService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Processes a list of transactions to identify key entries: the latest deposit, the oldest deposit 
        /// within 48 hours from the latest deposit, and the oldest withdrawal within 24 hours of the identified deposit.
        /// </summary>
        /// <param name="transactions">The list of transactions to process.</param>
        /// <returns>
        /// A tuple containing:
        /// - The latest deposit.
        /// - The oldest deposit within the 48-hour window.
        /// - The oldest withdrawal within the 24-hour window of the identified deposit.
        /// If no matching transactions are found, the tuple values may be null.
        /// </returns>
        public (Transaction LatestDeposit, Transaction OldestWithin48Hours, Transaction OldestWithdraw) ProcessTransactions(List<Transaction> transactions)
        {
            _logger.Log("Processing transactions to find relevant entries...");

            // Step 1: Identify the latest deposit in the dataset.
            // Rationale: The latest deposit acts as the reference point for subsequent calculations.
            var latestDeposit = transactions.Where(t => t.IsDeposit).OrderByDescending(t => t.Time).FirstOrDefault();
            if (latestDeposit == null)
            {
                _logger.Log("No deposits found.");
                return (null, null, null); // Exit if there are no deposits.
            }

            _logger.Log($"Latest deposit found: ID {latestDeposit.ID} at {latestDeposit.Time}");

            // Step 2: Find the oldest deposit within the 48-hour window from the latest deposit.
            // Rationale: This range ensures we include deposits close to the latest activity for calculations.
            var oldestWithin48Hours = transactions
                .Where(t => t.IsDeposit && t.Time <= latestDeposit.Time && t.Time >= latestDeposit.Time.AddHours(-48))
                .OrderBy(t => t.Time)
                .FirstOrDefault();

            if (oldestWithin48Hours == null)
            {
                _logger.Log("No deposits found within 48 hours.");
                return (latestDeposit, null, null); // Exit if no deposits meet this condition.
            }

            _logger.Log($"Oldest deposit within 48 hours: ID {oldestWithin48Hours.ID} at {oldestWithin48Hours.Time}");

            // Step 3: Find the oldest withdraw within a 24-hour window from the oldest deposit.
            // Rationale: This step narrows down relevant withdrawals to ensure they are close enough for pairing.
            var oldestWithdraw = transactions
                .Where(t => t.IsWithdraw && t.Time <= oldestWithin48Hours.Time && t.Time >= oldestWithin48Hours.Time.AddHours(-24))
                .OrderBy(t => t.Time)
                .FirstOrDefault();

            if (oldestWithdraw == null)
            {
                _logger.Log("No withdrawals found within 24 hours.");
                return (latestDeposit, oldestWithin48Hours, null); // Exit if no withdrawals meet this condition.
            }

            _logger.Log($"Oldest withdraw found: ID {oldestWithdraw.ID} at {oldestWithdraw.Time}");

            // Return the identified transactions for further calculations.
            return (latestDeposit, oldestWithin48Hours, oldestWithdraw);
        }

        /// <summary>
        /// Calculates the total balance based on deposits and withdrawals, starting from the time of the oldest withdrawal.
        /// Excludes transactions with Payment Type ID 76.
        /// </summary>
        /// <param name="transactions">The list of transactions to process.</param>
        /// <param name="oldestWithdraw">The oldest withdrawal to use as the starting point for balance calculations.</param>
        /// <returns>The total calculated balance after processing all relevant transactions.</returns>
        public decimal CalculateBalances(List<Transaction> transactions, Transaction oldestWithdraw)
        {
            _logger.Log("Calculating balances excluding Payment Type ID 76...");

            // Step 1: Filter transactions starting from the time of the oldest withdrawal.
            // Rationale: Only consider transactions relevant to the identified window, excluding invalid types (ID 76).
            var validTransactions = transactions
                .Where(t => t.Time >= oldestWithdraw.Time && t.TransactionType != 76)
                .OrderBy(t => t.Time) // Ensure transactions are processed in chronological order.
                .ToList();

            decimal totalBalance = 0;

            // Step 2: Iterate through the filtered transactions and calculate the running balance.
            // Rationale: Deposits add to the balance, and withdrawals subtract from it.
            foreach (var transaction in validTransactions)
            {
                if (transaction.IsDeposit)
                {
                    totalBalance += transaction.Amount;
                    _logger.Log($"Processed deposit ID {transaction.ID}: Added {transaction.Amount}, new balance is {totalBalance}");
                }

                if (transaction.IsWithdraw)
                {
                    totalBalance -= transaction.Amount;
                    _logger.Log($"Processed withdraw ID {transaction.ID}: Subtracted {transaction.Amount}, new balance is {totalBalance}");
                }
            }

            // Step 3: Return the final calculated balance.
            // Rationale: This balance reflects all valid transactions from the specified window.
            _logger.Log($"Final balance: {totalBalance}", false);
            return totalBalance;
        }
    }
}
