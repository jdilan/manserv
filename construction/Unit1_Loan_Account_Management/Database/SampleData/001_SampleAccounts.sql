/*
================================================================================
MANSERV Loan Account Management System
Sample Data Script - Loan Accounts
================================================================================
Target: SQL Server 2022
Purpose: Insert sample loan accounts for testing and demo
Author: System Generated
Date: December 5, 2025

SAMPLE DATA INCLUDES:
- 10 sample loan accounts with various statuses
- Different account types, centers, and loan characteristics
- Realistic data for demo purposes

NOTE: This is sample data for development and testing only
      Do NOT use in production environment
================================================================================
*/

USE ManservLoanDB;
GO

PRINT 'Inserting sample loan accounts...';
GO

-- Clear existing sample data (optional - comment out if you want to keep existing data)
-- DELETE FROM [dbo].[AccountRelationship];
-- DELETE FROM [dbo].[AccountAudit];
-- DELETE FROM [dbo].[Account];
-- DBCC CHECKIDENT ('[dbo].[Account]', RESEED, 0);
-- GO

-- ============================================================================
-- SAMPLE ACCOUNTS
-- ============================================================================

-- Account 1: Active Retail Loan
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2025-0001')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2025-0001', 'LA-2024-9999', '1234567890', 'Juan Dela Cruz',
        '1234567890123', 'Juan Dela Cruz Manufacturing Corporation', '01', '001', 'RTL',
        '11', 'IND001', '2025-01-15', '2025-01-15',
        '2030-01-15', 'IND', NULL, 'BSP', 'DBP', 'PA',
        0, 'CUR', 'B', 'P', 1,
        0, NULL, 0, NULL,
        'CUR', 'C', 'PHP', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2025-0001 (Juan Dela Cruz)';
END
GO

-- Account 2: Active Agricultural Loan
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2025-0002')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2025-0002', 'LA-2024-8888', '2345678901', 'Maria Santos',
        '2345678901234', 'Maria Santos Agricultural Farm', '01', '001', 'RTL',
        '11', 'AGR001', '2025-02-01', '2025-02-01',
        '2028-02-01', 'AA', 'A', 'LBP', 'ALF', 'PA',
        0, 'CUR', 'C', 'A', 1,
        1, 'SBGFC', 0, NULL,
        'CUR', 'D', 'PHP', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2025-0002 (Maria Santos)';
END
GO

-- Account 3: Past Due Real Estate Loan
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2024-0500')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2024-0500', 'LA-2023-0500', '3456789012', 'Pedro Reyes',
        '3456789012345', 'Pedro Reyes Real Estate Development', '02', '002', 'RTL',
        '11', 'REL001', '2024-06-01', '2024-06-01',
        '2029-06-01', 'R', 'H', 'DBP', 'CLF', 'NPA',
        0, 'PDO', 'C', 'R', 2,
        0, NULL, 0, NULL,
        'PDO', 'C', 'PHP', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2024-0500 (Pedro Reyes - Past Due)';
END
GO

-- Account 4: Restructured Loan
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2024-0750')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2024-0750', 'LA-2023-0750', '4567890123', 'Ana Garcia',
        '4567890123456', 'Ana Garcia Trading Company', '01', '001', 'RTL',
        '11', 'COM001', '2024-09-01', '2024-09-01',
        '2027-09-01', 'IND', NULL, 'WB', 'DBP', 'PA',
        1, 'CUR', 'B', 'P', 1,
        0, NULL, 0, NULL,
        'CUR', 'C', 'PHP', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2024-0750 (Ana Garcia - Restructured)';
END
GO

-- Account 5: Foreign Currency Loan (USD)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2025-0100')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2025-0100', 'LA-2024-0100', '5678901234', 'Global Exports Inc',
        '5678901234567', 'Global Exports Incorporated', '03', '003', 'FCDU',
        '20', 'EXP001', '2025-03-01', '2025-03-01',
        '2030-03-01', 'IND', NULL, 'ACPC', 'DBP', 'PA',
        0, 'CUR', 'C', 'P', 1,
        1, 'GFSME', 0, NULL,
        'CUR', 'C', 'USD', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2025-0100 (Global Exports - USD)';
END
GO

-- Account 6: Closed Loan
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2023-0001')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2023-0001', 'LA-2022-0001', '6789012345', 'Roberto Cruz',
        '6789012345678', 'Roberto Cruz Small Business', '01', '001', 'RTL',
        '11', 'SER001', '2023-01-15', '2023-01-15',
        '2025-01-15', 'IND', NULL, 'BSP', 'DBP', 'PA',
        0, 'CUR', 'B', 'P', 1,
        0, NULL, 0, NULL,
        'CUR', 'C', 'PHP', 'Closed',
        0, '2024-12-01', 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2023-0001 (Roberto Cruz - Closed)';
END
GO

-- Account 7: Draft Account (Incomplete)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2025-DRAFT-001')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2025-DRAFT-001', 'LA-2024-DRAFT-001', NULL, 'Draft Customer',
        NULL, 'Draft Customer Long Name', '01', '001', 'RTL',
        '11', 'IND001', '2025-01-01', '2025-01-01',
        '2030-01-01', 'IND', NULL, 'BSP', 'DBP', 'PA',
        0, 'CUR', 'B', 'P', 1,
        0, NULL, 0, NULL,
        'CUR', 'C', 'PHP', 'Active',
        1, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2025-DRAFT-001 (Draft Account)';
END
GO

-- Account 8: Under Litigation
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2023-0999')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2023-0999', 'LA-2022-0999', '7890123456', 'Litigation Case Corp',
        '7890123456789', 'Litigation Case Corporation', '02', '002', 'RTL',
        '11', 'IND001', '2023-06-01', '2023-06-01',
        '2028-06-01', 'IND', NULL, 'DBP', 'DBP', 'NPA',
        0, 'LITIG', 'C', 'P', 3,
        0, NULL, 1, '2024-06-01',
        'PDO', 'C', 'PHP', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2023-0999 (Under Litigation)';
END
GO

-- Account 9: Developmental Project
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2025-0200')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2025-0200', 'LA-2024-0200', '8901234567', 'Development Foundation',
        '8901234567890', 'Development Foundation for Progress', '01', '001', 'RTL',
        '11', 'DEV001', '2025-04-01', '2025-04-01',
        '2035-04-01', 'AI', 'I', 'WB', 'ALF', 'PA',
        0, 'CUR', 'E', 'A', 1,
        1, 'PHILGUARANTEE', 0, NULL,
        'CUR', 'D', 'PHP', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2025-0200 (Developmental Project)';
END
GO

-- Account 10: Wholesale Banking
IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [ReferenceNumber] = 'LA-2025-0300')
BEGIN
    INSERT INTO [dbo].[Account] (
        [ReferenceNumber], [PreviousReferenceNumber], [CRIBIDNumber], [CustomerName],
        [NIDSSAccountNumber], [LongName], [CenterCode], [BudgetUnit], [Corporation],
        [BookCode], [EconomicActivityCode], [OriginalReleaseDate], [StartOfTerm],
        [MaturityDate], [AccountType], [Purpose], [FundSource], [LendingProgram],
        [Area], [IsRestructured], [TypeOfCredit], [MaturityCode], [PurposeOfCredit],
        [NumberOfRecords], [IsGuaranteed], [GuaranteedBy], [IsUnderLitigation],
        [LitigationDate], [LoanStatus], [LoanProjectType], [Currency], [Status],
        [IsDraft], [ClosureDate], [CreatedBy], [CreatedDate]
    )
    VALUES (
        'LA-2025-0300', 'LA-2024-0300', '9012345678', 'Mega Corporation',
        '9012345678901', 'Mega Corporation International', '03', '003', 'WBG',
        '11', 'MAN001', '2025-05-01', '2025-05-01',
        '2030-05-01', 'IND', NULL, 'BSP', 'DBP', 'PA',
        0, 'CUR', 'C', 'P', 1,
        0, NULL, 0, NULL,
        'CUR', 'C', 'PHP', 'Active',
        0, NULL, 'SYSTEM', GETDATE()
    );
    PRINT '  ✓ Inserted Account: LA-2025-0300 (Wholesale Banking)';
END
GO

PRINT '';
PRINT '================================================================================';
PRINT 'Sample accounts inserted successfully!';
PRINT '================================================================================';
PRINT '';
PRINT 'SAMPLE DATA SUMMARY:';
PRINT '✓ 10 loan accounts created';
PRINT '✓ Various account types: Industrial, Agricultural, Real Estate';
PRINT '✓ Various statuses: Active, Past Due, Closed, Draft, Under Litigation';
PRINT '✓ Various currencies: PHP, USD';
PRINT '✓ Various corporations: RTL, FCDU, WBG';
PRINT '✓ Includes guaranteed and restructured loans';
PRINT '';
PRINT 'NEXT STEPS:';
PRINT '1. Query accounts: SELECT * FROM [dbo].[Account]';
PRINT '2. Test search functionality with various criteria';
PRINT '3. Proceed with application development';
PRINT '================================================================================';
GO
