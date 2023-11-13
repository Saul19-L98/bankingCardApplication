using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bankingApp.Restapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BonusInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumBalancePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BonifiableInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumPaymentDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCashAmountPayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCashAmountToPayWithInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Card",
                columns: new[] { "Id", "CardNumber", "CreditLimit", "ExpirationDate", "Name", "SecurityCode" },
                values: new object[] { new Guid("a747daa4-1b84-4ee7-98cf-17ebc2ff7469"), "4550030500456732", 5000m, new DateTime(2028, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saúl Laínez", "123" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("648b70c1-8f13-45c9-a7bd-50c1f5b8e358"), "Purchase" },
                    { new Guid("f9a01834-7689-4609-963f-b7666724f42a"), "Payment" }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "AvailableBalance", "BonifiableInterest", "BonusInterest", "CardId", "CurrentBalance", "MinimumBalancePercentage", "MinimumPaymentDue", "TotalCashAmountPayable", "TotalCashAmountToPayWithInterest" },
                values: new object[] { new Guid("44de30e9-6e02-4ca4-b3ad-f75f950dead6"), 2000m, 0m, 0.05m, new Guid("a747daa4-1b84-4ee7-98cf-17ebc2ff7469"), 3000m, 0.10m, 0m, 0m, 0m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "Amount", "Date", "Description", "TransactionTypeId" },
                values: new object[,]
                {
                    { new Guid("4faf703c-5121-49da-befb-8a52ee46debd"), new Guid("44de30e9-6e02-4ca4-b3ad-f75f950dead6"), 3000.00m, new DateTime(2023, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell Laptop", new Guid("648b70c1-8f13-45c9-a7bd-50c1f5b8e358") },
                    { new Guid("8b90ebcc-0295-48de-97ec-5e6dc543dfba"), new Guid("44de30e9-6e02-4ca4-b3ad-f75f950dead6"), 3150.00m, new DateTime(2023, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("f9a01834-7689-4609-963f-b7666724f42a") },
                    { new Guid("cfc3ddbb-c6d0-4749-bc83-c64eb0a50c42"), new Guid("44de30e9-6e02-4ca4-b3ad-f75f950dead6"), 3000.00m, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Car reparations.", new Guid("648b70c1-8f13-45c9-a7bd-50c1f5b8e358") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CardId",
                table: "Account",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");

            var spGetCardAccountDetails = @"
                CREATE PROCEDURE GetCardAccountDetails
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT TOP 1
                        c.[Name],
                        c.[CardNumber],
                        c.[CreditLimit],
                        a.[CurrentBalance],
                        ROUND(c.[CreditLimit] - a.[CurrentBalance], 2) AS [AvailableBalance],
                        ROUND(a.[CurrentBalance] * a.[BonusInterest], 2) AS [BonifiableInterest],
                        ROUND(a.[CurrentBalance] * a.[MinimumBalancePercentage], 2) AS [MinimumPaymentDue],
                        ROUND(a.[CurrentBalance], 2) AS [TotalCashAmountPayable],
                        ROUND(a.[CurrentBalance] + (a.[CurrentBalance] * a.[BonusInterest]), 2) AS [TotalCashAmountToPayWithInterest]
                    FROM 
                        [AccountDb].[dbo].[Card] c
                    INNER JOIN 
                        [AccountDb].[dbo].[Account] a ON c.[Id] = a.[CardId];
                END
            ";
            var spGetPurchasesForCurrentMonth = @"
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
            ";
            var spGetTotalPurchasesCurrentAndPreviousMonth = @"
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
            ";
            var spAddPurchase = @"
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
            ";
            var spMakePayment = @"
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
            ";
            var spGetSingleAccountId = @"
                CREATE PROCEDURE GetSingleAccountId
                AS
                BEGIN
                    SET NOCOUNT ON;

                    -- Retrieve the first account ID
                    SELECT TOP 1 Id
                    FROM [AccountDb].[dbo].[Account];
                END
            ";
            var spGetTransactionsForCurrentMonth = @"
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
            ";
            migrationBuilder.Sql(spGetCardAccountDetails);
            migrationBuilder.Sql(spGetPurchasesForCurrentMonth);
            migrationBuilder.Sql(spGetTotalPurchasesCurrentAndPreviousMonth);
            migrationBuilder.Sql(spAddPurchase);
            migrationBuilder.Sql(spMakePayment);
            migrationBuilder.Sql(spGetSingleAccountId);
            migrationBuilder.Sql(spGetTransactionsForCurrentMonth);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "Card");

            // Drop stored procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCardAccountDetails");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spGetPurchasesForCurrentMonth");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spGetTotalPurchasesCurrentAndPreviousMonth");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spAddPurchase");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spMakePayment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spGetSingleAccountId");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spGetTransactionsForCurrentMonth");
        }
    }
}
