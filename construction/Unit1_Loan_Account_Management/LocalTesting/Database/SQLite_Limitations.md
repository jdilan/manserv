# SQLite vs SQL Server 2022 - Limitations and Workarounds

## Document Information
- **Purpose**: Document differences between SQL Server 2022 and SQLite for testing
- **Unit**: Unit 1 - Loan Account Management
- **Date**: December 6, 2025

---

## Overview

This document outlines the key differences between the production SQL Server 2022 database and the SQLite testing database, along with workarounds for testing purposes.

---

## Data Type Conversions

### SQL Server → SQLite Mappings

| SQL Server Type | SQLite Type | Notes |
|----------------|-------------|-------|
| INT, BIGINT, SMALLINT | INTEGER | SQLite uses dynamic typing |
| VARCHAR(n), NVARCHAR(n), CHAR(n) | TEXT | No length enforcement in SQLite |
| BIT | INTEGER | Use 0 for false, 1 for true |
| DATE, DATETIME, DATETIME2 | TEXT | Store as ISO8601: 'YYYY-MM-DD HH:MM:SS' |
| DECIMAL, NUMERIC, MONEY | REAL or TEXT | Use TEXT for exact precision |
| UNIQUEIDENTIFIER (GUID) | TEXT | Store as string representation |

### Workarounds

**Date Handling:**
```sql
-- SQL Server
WHERE CreatedDate >= '2025-01-01' AND CreatedDate < '2025-02-01'

-- SQLite (same syntax works)
WHERE CreatedDate >= '2025-01-01' AND CreatedDate < '2025-02-01'

-- SQLite date functions
WHERE date(CreatedDate) = '2025-01-01'
WHERE datetime(CreatedDate) >= datetime('now', '-7 days')
```

**Boolean Fields:**
```sql
-- SQL Server
WHERE IsRestructured = 1

-- SQLite (same syntax works)
WHERE IsRestructured = 1
-- Or more explicit:
WHERE IsRestructured = CAST(1 AS INTEGER)
```

---

## Identity Columns

### SQL Server
```sql
CREATE TABLE Account (
    AccountId INT IDENTITY(1,1) PRIMARY KEY
);
```

### SQLite
```sql
CREATE TABLE Account (
    AccountId INTEGER PRIMARY KEY AUTOINCREMENT
);
```

### Limitations
- SQLite AUTOINCREMENT is similar but not identical to SQL Server IDENTITY
- SQLite reuses deleted IDs unless AUTOINCREMENT is specified
- No IDENTITY_INSERT equivalent in SQLite

### Workarounds
- Always use AUTOINCREMENT for primary keys
- Don't rely on specific ID values in tests
- Use ReferenceNumber (business key) instead of AccountId where possible

---

## Indexes

### SQL Server Features NOT Available in SQLite
- ❌ Clustered vs Non-clustered distinction
- ❌ INCLUDE columns in indexes
- ❌ Filtered indexes
- ❌ Index options (PAD_INDEX, FILLFACTOR, etc.)
- ❌ Columnstore indexes
- ❌ Online index operations

### SQLite Index Syntax
```sql
-- Simple index
CREATE INDEX IX_Account_CustomerName ON Account(CustomerName);

-- Composite index
CREATE INDEX IX_Account_Search ON Account(Status, CenterCode, AccountType);

-- Unique index
CREATE UNIQUE INDEX UK_Account_ReferenceNumber ON Account(ReferenceNumber);
```

### Workarounds
- Simplified indexes are sufficient for testing
- SQLite automatically creates indexes for PRIMARY KEY and UNIQUE constraints
- Query performance is acceptable for test data volumes (< 10,000 records)

---

## Constraints

### Supported in SQLite ✅
- PRIMARY KEY
- FOREIGN KEY (must enable: `PRAGMA foreign_keys = ON`)
- UNIQUE
- NOT NULL
- CHECK
- DEFAULT

### NOT Supported in SQLite ❌
- Named constraints for NOT NULL
- ALTER TABLE to add/drop constraints (limited support)
- Deferred constraint checking (available but not used)

### Workarounds
- Define all constraints during table creation
- Use CHECK constraints for business rules
- Enable foreign keys at connection level: `PRAGMA foreign_keys = ON`

---

## Transactions

### SQL Server Features NOT Available
- ❌ Distributed transactions
- ❌ Savepoints (SQLite has limited support)
- ❌ Transaction isolation levels (SQLite uses SERIALIZABLE only)
- ❌ READ UNCOMMITTED, READ COMMITTED hints

### SQLite Transaction Syntax
```sql
BEGIN TRANSACTION;
-- SQL statements
COMMIT;
-- or
ROLLBACK;
```

### Workarounds
- Use simple BEGIN/COMMIT/ROLLBACK for testing
- SQLite locks entire database during writes (acceptable for testing)
- Avoid concurrent write tests

---

## Functions and Operators

### Date/Time Functions

| SQL Server | SQLite Equivalent | Notes |
|-----------|------------------|-------|
| GETDATE() | datetime('now') | Current timestamp |
| DATEADD(day, 7, date) | datetime(date, '+7 days') | Add days |
| DATEDIFF(day, date1, date2) | julianday(date2) - julianday(date1) | Date difference |
| YEAR(date) | strftime('%Y', date) | Extract year |
| MONTH(date) | strftime('%m', date) | Extract month |

### String Functions

| SQL Server | SQLite Equivalent | Notes |
|-----------|------------------|-------|
| LEN(string) | LENGTH(string) | String length |
| SUBSTRING(string, start, length) | SUBSTR(string, start, length) | Substring |
| UPPER(string) | UPPER(string) | ✅ Same |
| LOWER(string) | LOWER(string) | ✅ Same |
| CONCAT(str1, str2) | str1 \|\| str2 | Concatenation |
| ISNULL(value, default) | IFNULL(value, default) | Null handling |

### Aggregate Functions

| SQL Server | SQLite Equivalent | Notes |
|-----------|------------------|-------|
| COUNT(*) | COUNT(*) | ✅ Same |
| SUM(column) | SUM(column) | ✅ Same |
| AVG(column) | AVG(column) | ✅ Same |
| MIN(column) | MIN(column) | ✅ Same |
| MAX(column) | MAX(column) | ✅ Same |

---

## Features NOT Available in SQLite

### Database Objects
- ❌ Stored Procedures
- ❌ User-Defined Functions (UDF) - except via extensions
- ❌ Triggers (available but not used in this implementation)
- ❌ Views (available but not used in this implementation)
- ❌ Schemas (SQLite has single schema)

### Advanced Features
- ❌ Full-Text Search (available via FTS5 extension, not used)
- ❌ JSON support (available in SQLite 3.38+, limited)
- ❌ XML support
- ❌ Spatial data types
- ❌ Temporal tables
- ❌ Always Encrypted
- ❌ Row-level security
- ❌ Dynamic data masking

### Workarounds
- Implement business logic in application layer (C# services)
- Use simple SQL queries only
- Complex operations handled by repository layer

---

## Concurrency and Locking

### SQL Server
- Row-level locking
- Multiple concurrent readers and writers
- Optimistic concurrency with row versioning

### SQLite
- Database-level locking
- Multiple readers OR single writer
- No row-level locking

### Workarounds for Testing
- ✅ Single-user testing (no concurrency issues)
- ✅ Sequential test execution
- ❌ Cannot test concurrent write scenarios
- ❌ Cannot test deadlock scenarios

---

## Performance Considerations

### SQLite Strengths
- ✅ Fast for small to medium datasets (< 100,000 records)
- ✅ Zero configuration
- ✅ Single file database
- ✅ ACID compliant

### SQLite Limitations
- ⚠️ Slower for large datasets (> 1,000,000 records)
- ⚠️ Limited concurrent writes
- ⚠️ No query optimizer hints
- ⚠️ No execution plan analysis

### Recommendations for Testing
- Keep test data under 10,000 records
- Focus on functional testing, not performance testing
- Use SQL Server for performance and load testing

---

## Connection Strings

### SQL Server
```
Data Source=localhost;Initial Catalog=ManservLoanDB;Integrated Security=True;
```

### SQLite
```
Data Source=manserv_test.db;Version=3;
```

---

## Migration Path

### From SQLite (Testing) to SQL Server (Production)

1. **Schema**: SQL Server schema is already defined in `/Database/Schema/`
2. **Data**: Export SQLite data and import to SQL Server using ETL tools
3. **Code**: Repository layer abstracts database differences
4. **Testing**: Validate all functionality on SQL Server before production

### Compatibility Notes
- SQL queries should be database-agnostic where possible
- Use parameterized queries (works in both)
- Avoid database-specific functions in application code
- Repository pattern isolates database-specific code

---

## Testing Recommendations

### What CAN Be Tested with SQLite ✅
- CRUD operations
- Business logic validation
- Data integrity constraints
- Foreign key relationships
- Transaction rollback
- Search and filtering
- Audit trail logging
- Account lifecycle operations

### What CANNOT Be Tested with SQLite ❌
- Concurrent write operations
- Deadlock scenarios
- Performance under load
- SQL Server specific features
- Distributed transactions
- Advanced indexing strategies

---

## Summary

SQLite is an excellent choice for local testing and development because:
- ✅ Zero configuration required
- ✅ Single file database (easy to reset)
- ✅ Fast for typical test scenarios
- ✅ Supports essential SQL features
- ✅ Cross-platform compatibility

However, always perform final testing on SQL Server 2022 before production deployment to ensure compatibility with production environment.

---

## References

- SQLite Documentation: https://www.sqlite.org/docs.html
- SQLite Data Types: https://www.sqlite.org/datatype3.html
- SQLite Date/Time Functions: https://www.sqlite.org/lang_datefunc.html
- SQL Server to SQLite Migration: https://www.sqlite.org/different.html
