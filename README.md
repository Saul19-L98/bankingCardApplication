# bankingCardApplication
This project is made of 1 solutions and 2 projects, the project BankingApp.WebApp is the MVC frontend Web Application adn the BankingApp.Restapi is where the backend and business logic is located.

## Create the Data Base with Migrations ⚒️

In the BankingApp.Restapi there is a folder named 'Migrations' here is where the scripts that create the Data Base are located, to execute that script, you should execute this command in the Nuget Package Manger Console:
``` cli
Update-Database
```
Once executed, the Data Base will be created in your computer.

## Connection to Data Base 📜

Then connectiong to the data base you will need to add a 'ConnectionStrings' with the "Manage User Secrets" property of VS community,
here is an exxample of the string:
``` json
ConnectionStrings:AccountDbConnectionString": "Server=ServerName;Database=AccountDb;Trusted_Connection=True;TrustServerCertificate=true"
```
Replace the 'ServerName' with the name of your server. Then in the Program.cs of the BankingApp.Restapi add the next line:
``` cs
builder.Services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AccountDbConnectionString")));
```
With that the backend should be connected to the Data Base.

## Storage Procedurals

### 1. GetCardAccountDetails
The stored procedure GetCardAccountDetails is designed to retrieve comprehensive financial details about the primary cardholder's account from a database. 
``` sql
CREATE PROCEDURE GetCardAccountDetails
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        c.[Name],
        c.[CardNumber],
        CONVERT(DECIMAL(10, 2), c.[CreditLimit]) AS [CreditLimit],
        CONVERT(DECIMAL(10, 2), a.[CurrentBalance]) AS [CurrentBalance],
        CONVERT(DECIMAL(10, 2), c.[CreditLimit] - a.[CurrentBalance]) AS [AvailableBalance],
        CONVERT(DECIMAL(10, 2), a.[CurrentBalance] * a.[BonusInterest]) AS [BonifiableInterest],
        CONVERT(DECIMAL(10, 2), a.[CurrentBalance] * a.[MinimumBalancePercentage]) AS [MinimumPaymentDue],
        CONVERT(DECIMAL(10, 2), a.[CurrentBalance]) AS [TotalCashAmountPayable],
        CONVERT(DECIMAL(10, 2), a.[CurrentBalance] + (a.[CurrentBalance] * a.[BonusInterest])) AS [TotalCashAmountToPayWithInterest]
    FROM 
        [AccountDb].[dbo].[Card] c
    INNER JOIN 
        [AccountDb].[dbo].[Account] a ON c.[Id] = a.[CardId];
END
```

### 2.GetPurchasesForCurrentMonth
The stored procedure GetPurchasesForCurrentMonth is designed to retrieve a list of purchase transactions from the database that have occurred in the current calendar month.
``` sql
CREATE PROCEDURE GetPurchasesForCurrentMonth
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);

    -- Retrieve purchases for the current month
    SELECT 
        t.[Date],
        t.[Description],
        t.[Amount]
    FROM 
        [AccountDb].[dbo].[Transactions] t
    INNER JOIN 
        [AccountDb].[dbo].[TransactionTypes] tt ON t.[TransactionTypeId] = tt.[Id]
    WHERE 
        t.[Date] >= @CurrentMonthStart
        AND t.[Date] < DATEADD(MONTH, 1, @CurrentMonthStart)
        AND tt.[Type] = 'Purchase'; 
	-- Assuming 'Purchase' is the type for purchases
END
```

### 3.GetTotalPurchasesCurrentAndPreviousMonth
The GetTotalPurchasesCurrentAndPreviousMonth stored procedure is crafted to calculate the sum of all purchase transactions for both the current and the previous months.
``` sql
CREATE PROCEDURE GetTotalPurchasesCurrentAndPreviousMonth
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    DECLARE @PreviousMonthStart DATE = DATEADD(MONTH, -1, @CurrentMonthStart);

    -- Total for the current month
    SELECT 
        SUM(t.[Amount]) AS TotalCurrentMonth
    FROM 
        [AccountDb].[dbo].[Transactions] t
    INNER JOIN 
        [AccountDb].[dbo].[TransactionTypes] tt ON t.[TransactionTypeId] = tt.[Id]
    WHERE 
        t.[Date] >= @CurrentMonthStart
        AND tt.[Type] = 'Purchase';

    -- Total for the previous month
    SELECT 
        SUM(t.[Amount]) AS TotalPreviousMonth
    FROM 
        [AccountDb].[dbo].[Transactions] t
    INNER JOIN 
        [AccountDb].[dbo].[TransactionTypes] tt ON t.[TransactionTypeId] = tt.[Id]
    WHERE 
        t.[Date] >= @PreviousMonthStart
        AND t.[Date] < @CurrentMonthStart
        AND tt.[Type] = 'Purchase';
END
```

### 4. AddPurchase
The AddPurchase stored procedure is responsible for recording a new purchase transaction and updating the account's current balance accordingly.
``` sql
CREATE PROCEDURE AddPurchase
    @Id UNIQUEIDENTIFIER,
    @Date DATETIME,
    @Description NVARCHAR(MAX),
    @Amount DECIMAL(18, 2),
    @AccountId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Add the purchase transaction
    INSERT INTO [AccountDb].[dbo].[Transactions] (Id, AccountId, Date, Description, Amount, TransactionTypeId)
    VALUES (@Id, @AccountId, @Date, @Description, @Amount,(SELECT Id FROM [AccountDb].[dbo].[TransactionTypes] WHERE Type = 'Purchase'));

    -- Update the CurrentBalance (increase for purchase)
    UPDATE [AccountDb].[dbo].[Account]
    SET CurrentBalance = CurrentBalance + @Amount
    WHERE Id = @AccountId;
END
```
### 5 MakePayment
The MakePayment stored procedure is designed to record a payment transaction against a specific account and update the account's current balance by decreasing it by the payment amount.
``` sql
CREATE PROCEDURE MakePayment
    @Id UNIQUEIDENTIFIER,
    @Date DATETIME,
	@Description NVARCHAR(MAX),
    @Amount DECIMAL(18, 2),
    @AccountId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Add the payment transaction
    INSERT INTO [AccountDb].[dbo].[Transactions] (Id, AccountId, Date, Description, Amount, TransactionTypeId)
    VALUES (@Id, @AccountId, @Date, @Description, @Amount, (SELECT Id FROM [AccountDb].[dbo].[TransactionTypes] WHERE Type = 'Payment'));

    -- Update the CurrentBalance (decrease for payment)
    UPDATE [AccountDb].[dbo].[Account]
    SET CurrentBalance = CurrentBalance - @Amount
    WHERE Id = @AccountId;
END
```

### 6 GetSingleAccountId
The GetSingleAccountId stored procedure is a simple SQL script used to retrieve the unique identifier (Id) of the first account record found in the Account table within the [AccountDb].[dbo] schema.
``` sql
CREATE PROCEDURE GetSingleAccountId
AS
BEGIN
    SET NOCOUNT ON;

    -- Retrieve the first account ID
    SELECT TOP 1 Id
    FROM [AccountDb].[dbo].[Account];
END
```

### 7 GetTransactionsForCurrentMonth
The GetTransactionsForCurrentMonth stored procedure is designed to fetch all the transactions that have occurred in the current month, including their dates, descriptions, amounts, and types.
``` sql
CREATE PROCEDURE GetTransactionsForCurrentMonth
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    DECLARE @NextMonthStart DATE = DATEADD(MONTH, 1, @CurrentMonthStart);

    SELECT 
        t.[Date],
        t.[Description],
        t.[Amount],
        tt.[Type] AS TransactionType
    FROM 
        [AccountDb].[dbo].[Transactions] t
    INNER JOIN 
        [AccountDb].[dbo].[TransactionTypes] tt ON t.[TransactionTypeId] = tt.[Id]
    WHERE 
        t.[Date] >= @CurrentMonthStart AND t.[Date] < @NextMonthStart
    ORDER BY 
        t.[Date] DESC;
END
```


