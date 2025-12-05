# MANSERV Loan Account Management - Database Setup

## Overview
This folder contains SQL Server 2022 database scripts for Unit 1: Loan Account Management.

## Prerequisites
- SQL Server 2022 (or SQL Server 2019 with compatibility level 150+)
- SQL Server Management Studio (SSMS) or Azure Data Studio
- Database creation permissions
- Windows Authentication or SQL Server Authentication configured

## SQL Server 2022 Specific Features Used
- Enhanced security features (Always Encrypted support ready)
- Improved query performance with intelligent query processing
- JSON support for audit logging
- Temporal tables support (can be enabled for history tracking)
- UTF-8 support for international characters

## Database Setup Instructions

### Step 1: Create Database
```sql
CREATE DATABASE ManservLoanDB
ON PRIMARY 
(
    NAME = N'ManservLoanDB_Data',
    FILENAME = N'C:\SQLData\ManservLoanDB_Data.mdf',  -- Adjust path as needed
    SIZE = 100MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10MB
)
LOG ON 
(
    NAME = N'ManservLoanDB_Log',
    FILENAME = N'C:\SQLData\ManservLoanDB_Log.ldf',  -- Adjust path as needed
    SIZE = 50MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10MB
);
GO

-- Set compatibility level to SQL Server 2022
ALTER DATABASE ManservLoanDB SET COMPATIBILITY_LEVEL = 160;
GO
```

### Step 2: Run Schema Scripts (in order)
1. **001_CreateTables.sql** - Creates Account, AccountAudit, and AccountRelationship tables
2. **002_CreateIndexes.sql** - Creates indexes for optimal query performance
3. **003_CreateConstraints.sql** - Creates foreign keys and check constraints

```powershell
# Using sqlcmd (Windows Command Line)
sqlcmd -S localhost -d ManservLoanDB -E -i "Schema\001_CreateTables.sql"
sqlcmd -S localhost -d ManservLoanDB -E -i "Schema\002_CreateIndexes.sql"
sqlcmd -S localhost -d ManservLoanDB -E -i "Schema\003_CreateConstraints.sql"
```

Or run each script manually in SSMS.

### Step 3: Load Sample Data
1. **001_SampleAccounts.sql** - Inserts 10 sample loan accounts for testing

```powershell
sqlcmd -S localhost -d ManservLoanDB -E -i "SampleData\001_SampleAccounts.sql"
```

## Connection String Configuration

### For Development (Windows Authentication)
```xml
<connectionStrings>
  <add name="ManservLoanDB" 
       connectionString="Data Source=localhost;Initial Catalog=ManservLoanDB;Integrated Security=True;TrustServerCertificate=True;Application Name=ManservLoanSystem" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### For Development (SQL Authentication)
```xml
<connectionStrings>
  <add name="ManservLoanDB" 
       connectionString="Data Source=localhost;Initial Catalog=ManservLoanDB;User ID=manserv_user;Password=YourSecurePassword;TrustServerCertificate=True;Application Name=ManservLoanSystem" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### For Production (with encryption)
```xml
<connectionStrings>
  <add name="ManservLoanDB" 
       connectionString="Data Source=production-server;Initial Catalog=ManservLoanDB;User ID=manserv_app;Password=YourSecurePassword;Encrypt=True;TrustServerCertificate=False;Application Name=ManservLoanSystem;MultipleActiveResultSets=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Important**: Encrypt connection strings in production using `aspnet_regiis.exe`:
```powershell
aspnet_regiis -pef "connectionStrings" "C:\inetpub\wwwroot\ManservLoanSystem" -prov "RsaProtectedConfigurationProvider"
```

## Database Tables

### Account Table
- **Purpose**: Core loan account information
- **Primary Key**: AccountId (identity)
- **Business Key**: ReferenceNumber (unique)
- **Maps to Legacy**: MANSERV.DBF

### AccountAudit Table
- **Purpose**: Audit trail for all account changes
- **Primary Key**: AuditId (identity)
- **Foreign Key**: AccountId → Account.AccountId

### AccountRelationship Table
- **Purpose**: Track relationships between accounts (copy, restructure, renewal)
- **Primary Key**: RelationshipId (identity)
- **Foreign Keys**: SourceAccountId, TargetAccountId → Account.AccountId

## Sample Data Summary
- 10 loan accounts with various characteristics
- Account types: Industrial, Agricultural, Real Estate
- Statuses: Active, Past Due, Closed, Draft, Under Litigation
- Currencies: PHP, USD
- Includes guaranteed and restructured loans

## Verification Queries

### Check table creation
```sql
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
```

### Check indexes
```sql
SELECT 
    t.name AS TableName,
    i.name AS IndexName,
    i.type_desc AS IndexType
FROM sys.indexes i
INNER JOIN sys.tables t ON i.object_id = t.object_id
WHERE t.name IN ('Account', 'AccountAudit', 'AccountRelationship')
ORDER BY t.name, i.name;
```

### Check constraints
```sql
SELECT 
    t.name AS TableName,
    c.name AS ConstraintName,
    c.type_desc AS ConstraintType
FROM sys.objects c
INNER JOIN sys.tables t ON c.parent_object_id = t.object_id
WHERE c.type IN ('F', 'C', 'D')
  AND t.name IN ('Account', 'AccountAudit', 'AccountRelationship')
ORDER BY t.name, c.type_desc, c.name;
```

### Check sample data
```sql
SELECT 
    Status,
    COUNT(*) AS AccountCount
FROM Account
GROUP BY Status;

SELECT * FROM Account ORDER BY CreatedDate DESC;
```

## Troubleshooting

### Issue: Cannot create database
**Solution**: Ensure you have CREATE DATABASE permission and sufficient disk space.

### Issue: Scripts fail with syntax errors
**Solution**: Ensure you're using SQL Server 2022 or set compatibility level to 160.

### Issue: Connection string not working
**Solution**: 
- Verify SQL Server is running: `services.msc` → SQL Server (MSSQLSERVER)
- Check firewall allows SQL Server port 1433
- Verify authentication mode (Windows vs SQL)
- Test connection in SSMS first

### Issue: Sample data violates constraints
**Solution**: Run schema scripts first (001, 002, 003) before sample data.

## Production Deployment Considerations

### Security
- [ ] Use SQL Authentication with strong passwords
- [ ] Encrypt connection strings in Web.config
- [ ] Grant minimum required permissions to application user
- [ ] Enable SQL Server auditing
- [ ] Configure firewall rules

### Performance
- [ ] Configure appropriate file growth settings
- [ ] Set up maintenance plans (backup, index rebuild, statistics update)
- [ ] Monitor query performance with Query Store
- [ ] Consider partitioning for large tables
- [ ] Configure appropriate memory settings

### Backup and Recovery
- [ ] Configure full backup schedule (daily)
- [ ] Configure transaction log backup schedule (every 15 minutes)
- [ ] Test restore procedures
- [ ] Document recovery time objective (RTO) and recovery point objective (RPO)
- [ ] Store backups off-site

### Monitoring
- [ ] Set up SQL Server Agent alerts
- [ ] Monitor disk space
- [ ] Monitor query performance
- [ ] Monitor blocking and deadlocks
- [ ] Set up performance baseline

## Support
For issues or questions, contact the development team.

---
**Last Updated**: December 5, 2025
**Version**: 1.0
