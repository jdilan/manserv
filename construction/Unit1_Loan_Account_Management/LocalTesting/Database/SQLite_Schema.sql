/*
================================================================================
MANSERV Loan Account Management System - SQLite Version
Database Schema Creation Script
================================================================================
Target: SQLite 3
Purpose: Create tables for Unit 1 - Loan Account Management (Testing)
Converted from: SQL Server 2022 schema
Date: December 6, 2025

SQLITE CONVERSION NOTES:
- IDENTITY → AUTOINCREMENT
- VARCHAR/NVARCHAR → TEXT
- BIT → INTEGER (0/1)
- DATETIME/DATE → TEXT (ISO8601 format: YYYY-MM-DD HH:MM:SS)
- Simplified indexes (no INCLUDE clause, no PAD_INDEX options)
- Check constraints supported
- Foreign keys supported (must enable: PRAGMA foreign_keys = ON)

LIMITATIONS vs SQL Server:
- No clustered/nonclustered index distinction
- No filegroup support
- No advanced features (temporal tables, Always Encrypted, etc.)
- Limited concurrent write access
- No stored procedures or triggers in this version

USAGE:
sqlite3 manserv_test.db < SQLite_Schema.sql
================================================================================
*/

-- Enable foreign key constraints (required for SQLite)
PRAGMA foreign_keys = ON;

-- ============================================================================
-- TABLE: Account
-- Purpose: Core loan account information
-- ============================================================================
CREATE TABLE IF NOT EXISTS Account (
    -- Primary Keys
    AccountId INTEGER PRIMARY KEY AUTOINCREMENT,
    ReferenceNumber TEXT NOT NULL UNIQUE,
    
    -- General Information
    PreviousReferenceNumber TEXT NOT NULL,
    CRIBIDNumber TEXT,
    CustomerName TEXT NOT NULL,
    NIDSSAccountNumber TEXT,
    LongName TEXT NOT NULL,
    
    -- Account Identification
    CenterCode TEXT NOT NULL,
    BudgetUnit TEXT NOT NULL,
    Corporation TEXT NOT NULL,
    BookCode TEXT NOT NULL,
    
    -- Economic Classification
    EconomicActivityCode TEXT NOT NULL,
    
    -- Loan Dates (stored as TEXT in ISO8601 format)
    OriginalReleaseDate TEXT NOT NULL,
    StartOfTerm TEXT NOT NULL,
    MaturityDate TEXT NOT NULL,
    
    -- Account Type and Purpose
    AccountType TEXT NOT NULL,
    Purpose TEXT,
    
    -- Funding and Program
    FundSource TEXT NOT NULL,
    LendingProgram TEXT NOT NULL,
    Area TEXT NOT NULL,
    
    -- Status and Classification
    IsRestructured INTEGER NOT NULL DEFAULT 0,
    TypeOfCredit TEXT NOT NULL,
    MaturityCode TEXT NOT NULL,
    PurposeOfCredit TEXT NOT NULL,
    NumberOfRecords INTEGER,
    
    -- Guarantee Information
    IsGuaranteed INTEGER NOT NULL DEFAULT 0,
    GuaranteedBy TEXT,
    
    -- Litigation
    IsUnderLitigation INTEGER NOT NULL DEFAULT 0,
    LitigationDate TEXT,
    
    -- Loan Project and Currency
    LoanStatus TEXT NOT NULL,
    LoanProjectType TEXT NOT NULL,
    Currency TEXT NOT NULL,
    
    -- Account Status
    Status TEXT NOT NULL DEFAULT 'Active',
    IsDraft INTEGER NOT NULL DEFAULT 0,
    ClosureDate TEXT,
    
    -- Audit Fields
    CreatedBy TEXT NOT NULL,
    CreatedDate TEXT NOT NULL DEFAULT (datetime('now')),
    ModifiedBy TEXT,
    ModifiedDate TEXT,
    DeletedBy TEXT,
    DeletedDate TEXT,
    
    -- Check Constraints
    CHECK (Status IN ('Active', 'Closed', 'Archived', 'Deleted')),
    CHECK (LoanStatus IN ('CUR', 'PDO')),
    CHECK (LoanProjectType IN ('C', 'D')),
    CHECK (MaturityDate > StartOfTerm),
    CHECK (StartOfTerm = OriginalReleaseDate),
    CHECK (IsRestructured IN (0, 1)),
    CHECK (IsGuaranteed IN (0, 1)),
    CHECK (IsUnderLitigation IN (0, 1)),
    CHECK (IsDraft IN (0, 1))
);

-- Indexes for Account table (simplified for testing)
CREATE INDEX IF NOT EXISTS IX_Account_CustomerName ON Account(CustomerName);
CREATE INDEX IF NOT EXISTS IX_Account_CenterCode ON Account(CenterCode);
CREATE INDEX IF NOT EXISTS IX_Account_Status ON Account(Status);
CREATE INDEX IF NOT EXISTS IX_Account_AccountType ON Account(AccountType);
CREATE INDEX IF NOT EXISTS IX_Account_Search ON Account(Status, CenterCode, AccountType);

-- ============================================================================
-- TABLE: AccountAudit
-- Purpose: Audit trail for all account changes
-- ============================================================================
CREATE TABLE IF NOT EXISTS AccountAudit (
    AuditId INTEGER PRIMARY KEY AUTOINCREMENT,
    AccountId INTEGER NOT NULL,
    ReferenceNumber TEXT NOT NULL,
    Action TEXT NOT NULL,
    FieldName TEXT,
    OldValue TEXT,
    NewValue TEXT,
    ChangedBy TEXT NOT NULL,
    ChangedDate TEXT NOT NULL DEFAULT (datetime('now')),
    UserRole TEXT NOT NULL,
    IPAddress TEXT,
    Comments TEXT,
    
    -- Check Constraint
    CHECK (Action IN ('Create', 'Update', 'Delete', 'Close', 'Archive', 'Reopen')),
    
    -- Foreign Key
    FOREIGN KEY (AccountId) REFERENCES Account(AccountId) ON DELETE NO ACTION
);

-- Indexes for AccountAudit table
CREATE INDEX IF NOT EXISTS IX_AccountAudit_AccountId ON AccountAudit(AccountId, ChangedDate DESC);
CREATE INDEX IF NOT EXISTS IX_AccountAudit_ReferenceNumber ON AccountAudit(ReferenceNumber, ChangedDate DESC);
CREATE INDEX IF NOT EXISTS IX_AccountAudit_ChangedDate ON AccountAudit(ChangedDate DESC);

-- ============================================================================
-- TABLE: AccountRelationship
-- Purpose: Track relationships between accounts
-- ============================================================================
CREATE TABLE IF NOT EXISTS AccountRelationship (
    RelationshipId INTEGER PRIMARY KEY AUTOINCREMENT,
    SourceAccountId INTEGER NOT NULL,
    TargetAccountId INTEGER NOT NULL,
    RelationshipType TEXT NOT NULL,
    CreatedBy TEXT NOT NULL,
    CreatedDate TEXT NOT NULL DEFAULT (datetime('now')),
    
    -- Check Constraints
    CHECK (RelationshipType IN ('Copy', 'Restructure', 'Renewal')),
    CHECK (SourceAccountId != TargetAccountId),
    
    -- Foreign Keys
    FOREIGN KEY (SourceAccountId) REFERENCES Account(AccountId) ON DELETE NO ACTION,
    FOREIGN KEY (TargetAccountId) REFERENCES Account(AccountId) ON DELETE NO ACTION
);

-- Indexes for AccountRelationship table
CREATE INDEX IF NOT EXISTS IX_AccountRelationship_SourceAccountId ON AccountRelationship(SourceAccountId);
CREATE INDEX IF NOT EXISTS IX_AccountRelationship_TargetAccountId ON AccountRelationship(TargetAccountId);

-- ============================================================================
-- VERIFICATION QUERIES
-- ============================================================================
-- Uncomment to verify schema creation:
-- SELECT name, type FROM sqlite_master WHERE type IN ('table', 'index') ORDER BY type, name;

-- ============================================================================
-- SCHEMA CREATION COMPLETE
-- ============================================================================
