/*
================================================================================
MANSERV Loan Account Management System
Database Indexes Creation Script
================================================================================
Target: SQL Server 2022
Purpose: Create indexes for optimal query performance
Author: System Generated
Date: December 5, 2025

SQL SERVER 2022 SPECIFIC FEATURES:
- Intelligent Query Processing automatically optimizes queries
- Columnstore indexes for analytical queries (optional)
- Memory-optimized tables support (can be enabled for high-performance scenarios)
- Resumable index operations for large tables

PERFORMANCE NOTES:
- Indexes improve SELECT performance but slow down INSERT/UPDATE/DELETE
- Monitor index usage with sys.dm_db_index_usage_stats
- Rebuild indexes regularly in production
- Consider filtered indexes for specific query patterns
================================================================================
*/

USE ManservLoanDB;
GO

PRINT 'Creating indexes for Account table...';
GO

-- ============================================================================
-- ACCOUNT TABLE INDEXES
-- ============================================================================

-- Unique index on ReferenceNumber (business key)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UK_Account_ReferenceNumber' AND object_id = OBJECT_ID('Account'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UK_Account_ReferenceNumber]
    ON [dbo].[Account] ([ReferenceNumber] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, 
          IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, 
          ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created UK_Account_ReferenceNumber';
END
ELSE
    PRINT '  - UK_Account_ReferenceNumber already exists';
GO

-- Index on CustomerName for search
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Account_CustomerName' AND object_id = OBJECT_ID('Account'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Account_CustomerName]
    ON [dbo].[Account] ([CustomerName] ASC)
    INCLUDE ([ReferenceNumber], [AccountType], [Status])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_Account_CustomerName';
END
ELSE
    PRINT '  - IX_Account_CustomerName already exists';
GO

-- Index on CenterCode for filtering by center
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Account_CenterCode' AND object_id = OBJECT_ID('Account'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Account_CenterCode]
    ON [dbo].[Account] ([CenterCode] ASC)
    INCLUDE ([ReferenceNumber], [CustomerName], [Status])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_Account_CenterCode';
END
ELSE
    PRINT '  - IX_Account_CenterCode already exists';
GO

-- Index on Status for filtering active/closed accounts
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Account_Status' AND object_id = OBJECT_ID('Account'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Account_Status]
    ON [dbo].[Account] ([Status] ASC)
    INCLUDE ([ReferenceNumber], [CustomerName], [CenterCode])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_Account_Status';
END
ELSE
    PRINT '  - IX_Account_Status already exists';
GO

-- Index on AccountType for filtering by account type
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Account_AccountType' AND object_id = OBJECT_ID('Account'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Account_AccountType]
    ON [dbo].[Account] ([AccountType] ASC)
    INCLUDE ([ReferenceNumber], [CustomerName])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_Account_AccountType';
END
ELSE
    PRINT '  - IX_Account_AccountType already exists';
GO

-- Composite index for common search scenarios
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Account_Search' AND object_id = OBJECT_ID('Account'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Account_Search]
    ON [dbo].[Account] ([Status] ASC, [CenterCode] ASC, [AccountType] ASC)
    INCLUDE ([ReferenceNumber], [CustomerName], [CreatedDate])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_Account_Search';
END
ELSE
    PRINT '  - IX_Account_Search already exists';
GO

PRINT '';
PRINT 'Creating indexes for AccountAudit table...';
GO

-- ============================================================================
-- ACCOUNTAUDIT TABLE INDEXES
-- ============================================================================

-- Index on AccountId for audit history retrieval
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AccountAudit_AccountId' AND object_id = OBJECT_ID('AccountAudit'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AccountAudit_AccountId]
    ON [dbo].[AccountAudit] ([AccountId] ASC, [ChangedDate] DESC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_AccountAudit_AccountId';
END
ELSE
    PRINT '  - IX_AccountAudit_AccountId already exists';
GO

-- Index on ReferenceNumber for audit lookup
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AccountAudit_ReferenceNumber' AND object_id = OBJECT_ID('AccountAudit'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AccountAudit_ReferenceNumber]
    ON [dbo].[AccountAudit] ([ReferenceNumber] ASC, [ChangedDate] DESC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_AccountAudit_ReferenceNumber';
END
ELSE
    PRINT '  - IX_AccountAudit_ReferenceNumber already exists';
GO

-- Index on ChangedDate for time-based queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AccountAudit_ChangedDate' AND object_id = OBJECT_ID('AccountAudit'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AccountAudit_ChangedDate]
    ON [dbo].[AccountAudit] ([ChangedDate] DESC)
    INCLUDE ([AccountId], [ReferenceNumber], [Action], [ChangedBy])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_AccountAudit_ChangedDate';
END
ELSE
    PRINT '  - IX_AccountAudit_ChangedDate already exists';
GO

PRINT '';
PRINT 'Creating indexes for AccountRelationship table...';
GO

-- ============================================================================
-- ACCOUNTRELATIONSHIP TABLE INDEXES
-- ============================================================================

-- Index on SourceAccountId
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AccountRelationship_SourceAccountId' AND object_id = OBJECT_ID('AccountRelationship'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AccountRelationship_SourceAccountId]
    ON [dbo].[AccountRelationship] ([SourceAccountId] ASC)
    INCLUDE ([TargetAccountId], [RelationshipType])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_AccountRelationship_SourceAccountId';
END
ELSE
    PRINT '  - IX_AccountRelationship_SourceAccountId already exists';
GO

-- Index on TargetAccountId
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AccountRelationship_TargetAccountId' AND object_id = OBJECT_ID('AccountRelationship'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AccountRelationship_TargetAccountId]
    ON [dbo].[AccountRelationship] ([TargetAccountId] ASC)
    INCLUDE ([SourceAccountId], [RelationshipType])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,
          DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
    
    PRINT '  ✓ Created IX_AccountRelationship_TargetAccountId';
END
ELSE
    PRINT '  - IX_AccountRelationship_TargetAccountId already exists';
GO

PRINT '';
PRINT '================================================================================';
PRINT 'All indexes created successfully!';
PRINT '================================================================================';
PRINT '';
PRINT 'INDEX MAINTENANCE NOTES:';
PRINT '- Monitor index fragmentation with sys.dm_db_index_physical_stats';
PRINT '- Rebuild indexes when fragmentation > 30%: ALTER INDEX ALL ON [Table] REBUILD';
PRINT '- Reorganize indexes when fragmentation 10-30%: ALTER INDEX ALL ON [Table] REORGANIZE';
PRINT '- Update statistics regularly: UPDATE STATISTICS [Table]';
PRINT '- SQL Server 2022 automatic tuning can help optimize index usage';
PRINT '';
PRINT 'NEXT STEPS:';
PRINT '1. Run 003_CreateConstraints.sql to create foreign keys';
PRINT '2. Run sample data scripts';
PRINT '================================================================================';
GO
