/*
================================================================================
MANSERV Loan Account Management System
Database Schema Creation Script
================================================================================
Target: SQL Server 2022
Purpose: Create tables for Unit 1 - Loan Account Management
Author: System Generated
Date: December 5, 2025

SQL SERVER 2022 SPECIFIC FEATURES USED:
- Enhanced security features (Always Encrypted support ready)
- Improved query performance with intelligent query processing
- JSON support for audit logging
- Temporal tables support (can be enabled for history tracking)
- UTF-8 support for international characters

DEPLOYMENT NOTES:
- Run this script on SQL Server 2022 instance
- Requires CREATE TABLE permissions
- Database must exist before running this script
- For production: Review and adjust file groups for performance
- For production: Consider partitioning for large tables
================================================================================
*/

USE ManservLoanDB;  -- Change to your database name
GO

-- ============================================================================
-- TABLE: Account
-- Purpose: Core loan account information
-- Maps to legacy: MANSERV.DBF
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Account]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Account]
    (
        -- Primary Keys
        [AccountId] INT IDENTITY(1,1) NOT NULL,
        [ReferenceNumber] VARCHAR(17) NOT NULL,
        
        -- General Information (GENERAL Section)
        [PreviousReferenceNumber] VARCHAR(17) NOT NULL,
        [CRIBIDNumber] VARCHAR(10) NULL,
        [CustomerName] VARCHAR(40) NOT NULL,
        [NIDSSAccountNumber] VARCHAR(13) NULL,
        [LongName] VARCHAR(100) NOT NULL,
        
        -- Account Identification (LOAN INFO Section)
        [CenterCode] VARCHAR(2) NOT NULL,
        [BudgetUnit] VARCHAR(3) NOT NULL,
        [Corporation] VARCHAR(10) NOT NULL,
        [BookCode] VARCHAR(2) NOT NULL,
        
        -- Economic Classification
        [EconomicActivityCode] VARCHAR(6) NOT NULL,
        
        -- Loan Dates
        [OriginalReleaseDate] DATE NOT NULL,
        [StartOfTerm] DATE NOT NULL,
        [MaturityDate] DATE NOT NULL,
        
        -- Account Type and Purpose
        [AccountType] VARCHAR(3) NOT NULL,
        [Purpose] VARCHAR(1) NULL,  -- Conditional: mandatory for specific account types
        
        -- Funding and Program
        [FundSource] VARCHAR(3) NOT NULL,
        [LendingProgram] VARCHAR(3) NOT NULL,
        [Area] VARCHAR(3) NOT NULL,
        
        -- Status and Classification
        [IsRestructured] BIT NOT NULL DEFAULT 0,
        [TypeOfCredit] VARCHAR(6) NOT NULL,  -- Auto-populated
        [MaturityCode] VARCHAR(1) NOT NULL,
        [PurposeOfCredit] VARCHAR(1) NOT NULL,  -- Auto-populated
        [NumberOfRecords] INT NULL,
        
        -- Guarantee Information
        [IsGuaranteed] BIT NOT NULL DEFAULT 0,
        [GuaranteedBy] VARCHAR(10) NULL,
        
        -- Litigation
        [IsUnderLitigation] BIT NOT NULL DEFAULT 0,
        [LitigationDate] DATE NULL,
        
        -- Loan Project and Currency
        [LoanStatus] VARCHAR(3) NOT NULL,  -- CUR or PDO
        [LoanProjectType] VARCHAR(1) NOT NULL,  -- C or D
        [Currency] VARCHAR(3) NOT NULL,
        
        -- Account Status
        [Status] VARCHAR(20) NOT NULL DEFAULT 'Active',  -- Active, Closed, Archived, Deleted
        [IsDraft] BIT NOT NULL DEFAULT 0,
        [ClosureDate] DATE NULL,
        
        -- Audit Fields
        [CreatedBy] VARCHAR(50) NOT NULL,
        [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
        [ModifiedBy] VARCHAR(50) NULL,
        [ModifiedDate] DATETIME NULL,
        [DeletedBy] VARCHAR(50) NULL,
        [DeletedDate] DATETIME NULL,
        
        -- Primary Key Constraint
        CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountId] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
                  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    PRINT 'Table [Account] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [Account] already exists.';
END
GO

-- ============================================================================
-- TABLE: AccountAudit
-- Purpose: Audit trail for all account changes
-- SQL Server 2022 Feature: Can store JSON data in nvarchar(max) columns
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountAudit]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[AccountAudit]
    (
        [AuditId] INT IDENTITY(1,1) NOT NULL,
        [AccountId] INT NOT NULL,
        [ReferenceNumber] VARCHAR(17) NOT NULL,
        [Action] VARCHAR(20) NOT NULL,  -- Create, Update, Delete, Close, Archive, Reopen
        [FieldName] VARCHAR(100) NULL,  -- For Update actions
        [OldValue] NVARCHAR(MAX) NULL,  -- Can store JSON for complex objects
        [NewValue] NVARCHAR(MAX) NULL,  -- Can store JSON for complex objects
        [ChangedBy] VARCHAR(50) NOT NULL,
        [ChangedDate] DATETIME NOT NULL DEFAULT GETDATE(),
        [UserRole] VARCHAR(50) NOT NULL,
        [IPAddress] VARCHAR(50) NULL,
        [Comments] NVARCHAR(500) NULL,
        
        -- Primary Key Constraint
        CONSTRAINT [PK_AccountAudit] PRIMARY KEY CLUSTERED ([AuditId] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
                  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
    
    PRINT 'Table [AccountAudit] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [AccountAudit] already exists.';
END
GO

-- ============================================================================
-- TABLE: AccountRelationship
-- Purpose: Track relationships between accounts (copy, restructure, renewal)
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountRelationship]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[AccountRelationship]
    (
        [RelationshipId] INT IDENTITY(1,1) NOT NULL,
        [SourceAccountId] INT NOT NULL,
        [TargetAccountId] INT NOT NULL,
        [RelationshipType] VARCHAR(20) NOT NULL,  -- Copy, Restructure, Renewal
        [CreatedBy] VARCHAR(50) NOT NULL,
        [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
        
        -- Primary Key Constraint
        CONSTRAINT [PK_AccountRelationship] PRIMARY KEY CLUSTERED ([RelationshipId] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
                  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    PRINT 'Table [AccountRelationship] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [AccountRelationship] already exists.';
END
GO

PRINT '';
PRINT '================================================================================';
PRINT 'All tables created successfully!';
PRINT '================================================================================';
PRINT '';
PRINT 'NEXT STEPS:';
PRINT '1. Run 002_CreateIndexes.sql to create indexes';
PRINT '2. Run 003_CreateConstraints.sql to create foreign keys and constraints';
PRINT '3. Run sample data scripts in Database/SampleData folder';
PRINT '';
PRINT 'SQL SERVER 2022 DEPLOYMENT NOTES:';
PRINT '- Ensure SQL Server 2022 is installed and running';
PRINT '- Database collation: SQL_Latin1_General_CP1_CI_AS (or as per requirements)';
PRINT '- For production: Configure backup strategy';
PRINT '- For production: Configure maintenance plans';
PRINT '- For production: Review security and permissions';
PRINT '================================================================================';
GO
