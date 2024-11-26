# Transaction Scraper and Processor

## Overview

This project is a .NET console application designed to scrape transaction data from a webpage, process it according to specific business rules, and generate meaningful results. The application uses **HTML Agility Pack** for web scraping and implements detailed logging to track each step of the process.


## Features

- **Web Scraping**: Extracts transaction data from a dynamically generated HTML table.
- **Data Parsing**: Safely parses columns into appropriate data types using a generic parser.
- **Business Logic**:
  - Identifies deposits and withdrawals within specified time ranges.
  - Excludes specific transaction types.
  - Balances deposits and withdrawals based on time constraints.
- **Error Handling**: Logs all parsing and processing errors without stopping the execution.
- **Logging**: Captures step-by-step actions and errors in both the console and a log file.

### Prerequisites

- .NET 6.0 or later
- HTML Agility Pack (installed via NuGet)


### Sample Logs

- Starting to scrape transactions...
- Transaction ID 10674027 successfully parsed.
- Transaction ID 10658812 successfully parsed.
- Error: Failed to parse Transaction ID 10658813: Input string '' was not in a correct format.

- Processing transactions to find relevant entries...
- Latest deposit found: ID 10674027 at 11/14/2024 10:52:08 AM
- Oldest deposit within 48 hours: ID 10658812 at 11/13/2024 10:29:42 AM
- No withdrawals found within 24 hours.
- Processing complete. Results saved to output.

### Author
- Saqib Lodhi
- GitHub: @slodhi786
- LinkedIn: www.linkedin.com/in/saqiblodhi