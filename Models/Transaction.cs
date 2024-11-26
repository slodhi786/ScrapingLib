namespace ScrapingLib.Models
{
    /// <summary>
    /// Represents a financial transaction with details such as transaction ID, customer code, amount, and type.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the transaction ID, which is typically a unique identifier assigned to each transaction.
        /// </summary>
        public long TransID { get; set; }

        /// <summary>
        /// Gets or sets the customer code associated with the transaction, typically used to identify the customer making the transaction.
        /// </summary>
        public int CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the transaction occurred.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction, which can help differentiate between different types (e.g., deposit, withdrawal, etc.).
        /// </summary>
        public int TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the amount involved in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the transaction is a deposit.
        /// </summary>
        public bool IsDeposit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the transaction is a withdrawal.
        /// </summary>
        public bool IsWithdraw { get; set; }
    }
}
