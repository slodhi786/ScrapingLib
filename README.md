# Scraping and Transaction Processing Solution

## Overview
This project is designed to scrape transaction data from a specified URL, process it to find the latest deposit, the oldest deposit within 48 hours, and the oldest withdrawal within 24 hours, and then calculate the final balance based on relevant transactions. The solution leverages HTML scraping, dependency injection, and logging for efficient operation and debugging.

## Features
- **Web Scraping**: Scrapes transaction data from a table on a given URL.
- **Transaction Processing**: Identifies key transactions based on time intervals (latest deposit, oldest deposit within 48 hours, oldest withdrawal within 24 hours).
- **Balance Calculation**: Calculates the final balance based on the relevant transactions.
- **Logging**: Logs application events and errors to a file for debugging and record-keeping.
- **Dependency Injection**: Uses Microsoft.Extensions.DependencyInjection for modular and testable code.

## Approach

### 1. Data Scraping
The `ScraperService` class uses the `HtmlAgilityPack` library to scrape data from a given URL. It loads the HTML document, extracts transaction rows from a table, and maps each row to a `Transaction` object. These transactions are then processed for further analysis.

### 2. Transaction Processing
The `TransactionService` class processes the scraped transactions to find:
- **Latest Deposit**: The most recent deposit.
- **Oldest Deposit within 48 hours**: The earliest deposit within 48 hours of the latest deposit.
- **Oldest Withdrawal within 24 hours**: The earliest withdrawal within 24 hours of the oldest deposit.

It then calculates the final balance by adding deposits and subtracting withdrawals, excluding transactions with specific invalid types.

### 3. Dependency Injection
The project uses **Microsoft.Extensions.DependencyInjection** to handle dependency injection. This makes the code modular and allows for easier testing and maintenance. The services are configured in the `Program` class using a `ServiceCollection`.

### 4. Logging
The `Logger` class writes logs to a file (`log.txt`) in the application's base directory. Logs include important milestones (e.g., number of transactions scraped, details of the latest deposit) and error messages (e.g., issues encountered during scraping or processing).

### 5. Execution Flow
The program:
1. Configures and wires up services (logging, scraping, transaction processing).
2. Scrapes transactions from a provided URL.
3. Processes the transactions to find the required deposits and withdrawals.
4. Calculates and displays the final balance.

## Challenges Faced
- **Scraping Complex HTML Tables**: Extracting transaction data from an HTML table with dynamic rows required careful XPath navigation and column parsing.
- **Error Handling**: Ensuring robust error handling, particularly during web scraping, was crucial for successful execution.
- **Transaction Processing Logic**: Handling edge cases and ensuring correct time-based comparisons for deposits and withdrawals.
- **Testing and Validation**: Validating scraping logic and transaction processing against real-world data was difficult without a stable and predictable source.
- **Dependency Injection Setup**: Configuring DI properly required understanding how to wire up services, but once done, it made the codebase more maintainable and testable.

## Future Improvements
- **Unit Testing**: Implement unit tests for services like `TransactionService` and `ScraperService` to ensure the correctness of the logic.
- **Error Handling Enhancements**: Improve error handling, especially for edge cases like invalid data or server failures.
- **Real-time Scraping**: Consider integrating real-time web scraping to continuously monitor transactions.
- **Performance Optimization**: Improve performance for scraping and processing large datasets.

## Running the Application

### Prerequisites
- .NET 6.0 or later
- NuGet packages:
  - HtmlAgilityPack
  - Microsoft.Extensions.DependencyInjection

### Steps to Run
1. Clone the repository to your local machine.
2. Install the required NuGet packages.
3. Build the project using the `dotnet build` command.
4. Run the application with `dotnet run`.

### Sample Output
The application will scrape transaction data from the specified URL, process the transactions, and display the final balance, e.g.:

- Starting to scrape transactions...
- Scraped 1000 transactions.
- Processing transactions to find relevant entries...
- Latest deposit found: ID 10674027 at 11/14/2024 10:52:08 AM
- Oldest deposit within 48 hours: ID 10644710 at 11/12/2024 1:15:03 PM
- Oldest withdraw found: ID 10638788 at 11/11/2024 4:37:15 PM
- Calculating balances excluding Payment Type ID 76...
- Processed withdraw ID 10638788: Subtracted 15000, new balance is -15000
- Processed deposit ID 10638782: Added 2000, new balance is -13000
- ......
- Final balance: 86850


### Author
- Saqib Lodhi
- GitHub: @slodhi786
- LinkedIn: www.linkedin.com/in/saqiblodhi