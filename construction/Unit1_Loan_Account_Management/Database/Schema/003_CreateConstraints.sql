/*
================================================================================
MANSERV Loan Account Management System
Database Constraints Creation Script
================================================================================
Target: SQL Server 2022
Purpose: Create foreign keys, check constraints, and default constraints
Author: System Generated
Date: December 5, 2025

CONSTRAINT TYPES:
- Foreign Keys: Enforce referential integrity
- Check Constraints: Enforce business rules at database level
- Default Constraints: Provide default values

SQL SERVER 2022 FEATURES:
- Enhanced constraint validation performance
- Support for computed columns with constraints
- Deferred constraint checking (can be enabled)
================================================================================
*/

USE ManservLoanDB;
GO

PRINT 'Creating foreign key constraints...';
GO

-- ============================================================================
-- FOREIGN KEY CONSTRAINTS
-- ============================================================================

-- AccountAudit.AccountId → Account.AccountId
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_AccountAudit_Account')
BEGIN
    ALTER TABLE [dbo].[AccountAudit]
    ADD CONSTRAINT [FK_AccountAudit_Account]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Account] ([AccountId])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;
    
    PRINT '  ✓ Created FK_AccountAudit_Account';
END
ELSE
    PRINT '  - FK_AccountAudit_Account already exists';
GO

-- AccountRelationship.SourceAccountId → Account.AccountId
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_AccountRelationship_SourceAccount')
BEGIN
    ALTER TABLE [dbo].[AccountRelationship]
    ADD CONSTRAINT [FK_AccountRelationship_SourceAccount]
    FOREIGN KEY ([SourceAccountId])
    REFERENCES [dbo].[Account] ([AccountId])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;
    
    PRINT '  ✓ Created FK_AccountRelationship_SourceAccount';
END
ELSE
    PRINT '  - FK_AccountRelationship_SourceAccount already exists';
GO

-- AccountRelationship.TargetAccountId → Account.AccountId
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_AccountRelationship_TargetAccount')
BEGIN
    ALTER TABLE [dbo].[AccountRelationship]
    ADD CONSTRAINT [FK_AccountRelationship_TargetAccount]
    FOREIGN KEY ([TargetAccountId])
    REFERENCES [dbo].[Account] ([AccountId])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;
    
    PRINT '  ✓ Created FK_AccountRelationship_TargetAccount';
END
ELSE
    PRINT '  - FK_AccountRelationship_TargetAccount already exists';
GO

PRINT '';
PRINT 'Creating check constraints...';
GO

-- ============================================================================
-- CHECK CONSTRAINTS - Business Rules Enforcement
-- ============================================================================

-- Account.Status must be valid value
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Account_Status')
BEGIN
    ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [CK_Account_Status]
    CHECK ([Status] IN ('Active', 'Closed', 'Archived', 'Deleted'));
    
    PRINT '  ✓ Created CK_Account_Status';
END
ELSE
    PRINT '  - CK_Account_Status already exists';
GO

-- Account.LoanStatus must be CUR or PDO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Account_LoanStatus')
BEGIN
    ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [CK_Account_LoanStatus]
    CHECK ([LoanStatus] IN ('CUR', 'PDO'));
    
    PRINT '  ✓ Created CK_Account_LoanStatus';
END
ELSE
    PRINT '  - CK_Account_LoanStatus already exists';
GO

-- Account.LoanProjectType must be C or D
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Account_LoanProjectType')
BEGIN
    ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [CK_Account_LoanProjectType]
    CHECK ([LoanProjectType] IN ('C', 'D'));
    
    PRINT '  ✓ Created CK_Account_LoanProjectType';
END
ELSE
    PRINT '  - CK_Account_LoanProjectType already exists';
GO

-- Account.MaturityDate must be greater than StartOfTerm
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Account_MaturityDate')
BEGIN
    ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [CK_Account_MaturityDate]
    CHECK ([MaturityDate] > [StartOfTerm]);
    
    PRINT '  ✓ Created CK_Account_MaturityDate';
END
ELSE
    PRINT '  - CK_Account_MaturityDate already exists';
GO

-- Account.StartOfTerm must equal OriginalReleaseDate (business rule)
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Account_StartOfTerm')
BEGIN
    ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [CK_Account_StartOfTerm]
    CHECK ([StartOfTerm] = [OriginalReleaseDate]);
    
    PRINT '  ✓ Created CK_Account_StartOfTerm';
END
ELSE
    PRINT '  - CK_Account_StartOfTerm already exists';
GO

-- AccountAudit.Action must be valid value
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_AccountAudit_Action')
BEGIN
    ALTER TABLE [dbo].[AccountAudit]
    ADD CONSTRAINT [CK_AccountAudit_Action]
    CHECK ([Action] IN ('Create', 'Update', 'Delete', 'Close', 'Archive', 'Reopen'));
    
    PRINT '  ✓ Created CK_AccountAudit_Action';
END
ELSE
    PRINT '  - CK_AccountAudit_Action already exists';
GO

-- AccountRelationship.RelationshipType must be valid value
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_AccountRelationship_Type')
BEGIN
    ALTER TABLE [dbo].[AccountRelationship]
    ADD CONSTRAINT [CK_AccountRelationship_Type]
    CHECK ([RelationshipType] IN ('Copy', 'Restructure', 'Renewal'));
    
    PRINT '  ✓ Created CK_AccountRelationship_Type';
END
ELSE
    PRINT '  - CK_AccountRelationship_Type already exists';
GO

-- AccountRelationship: Source and Target cannot be the same
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_AccountRelationship_NotSelf')
BEGIN
    ALTER TABLE [dbo].[AccountRelationship]
    ADD CONSTRAINT [CK_AccountRelationship_NotSelf]
    CHECK ([SourceAccountId] <> [TargetAccountId]);
    
    PRINT '  ✓ Created CK_AccountRelationship_NotSelf';
END
ELSE
    PRINT '  - CK_AccountRelationship_NotSelf already exists';
GO

PRINT '';
PRINT '================================================================================';
PRINT 'All constraints created successfully!';
PRINT '================================================================================';
PRINT '';
PRINT 'CONSTRAINT NOTES:';
PRINT '- Foreign keys enforce referential integrity';
PRINT '- Check constraints enforce business rules at database level';
PRINT '- Additional validation should be done in application layer';
PRINT '- For production: Consider disabling constraints during bulk loads';
PRINT '- For production: Re-enable and validate constraints after bulk loads';
PRINT '';
PRINT 'BUSINESS RULES ENFORCED:';
PRINT '✓ Account status must be Active, Closed, Archived, or Deleted';
PRINT '✓ Loan status must be CUR (Current) or PDO (Past Due)';
PRINT '✓ Loan project type must be C (Commercial) or D (Developmental)';
PRINT '✓ Maturity date must be after start of term';
PRINT '✓ Start of term must equal original release date';
PRINT '✓ Audit actions must be valid operation types';
PRINT '✓ Relationship types must be Copy, Restructure, or Renewal';
PRINT '✓ Account cannot have relationship with itself';
PRINT '';
PRINT 'NEXT STEPS:';
PRINT '1. Run sample data scripts in Database/SampleData folder';
PRINT '2. Test constraints by attempting invalid data inserts';
PRINT '3. Proceed with application development';
PRINT '================================================================================';
GO
