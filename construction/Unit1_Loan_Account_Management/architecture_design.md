# Architecture Design - Unit 1: Loan Account Management

## Document Information
- **Project**: Manserv Loan Account Management System
- **Unit**: Unit 1 - Loan Account Management
- **Version**: 1.0
- **Date**: December 5, 2025
- **Status**: Design Complete
- **Technology Stack**: ASP.NET 4.7, SQL Server 2022

---

## Table of Contents
1. Architecture Overview
2. Feature Components
3. Business Services Layer
4. Data Models
5. Repository Pattern
6. Service Interfaces
7. Validation and Business Rules
8. Access Control and Security
9. Integration Points
10. Component Interactions
11. Non-Functional Considerations
12. Architecture Diagrams
13. Implementation Guidelines

---

## 1. Architecture Overview

### 1.1 Architecture Style
This unit implements a **Feature-Based Architecture with Service-Oriented patterns**, designed to be straightforward for teams transitioning from traditional monolithic waterfall development.

**Key Characteristics:**
- Features organized by business capability
- Clear separation of concerns without complex domain modeling
- Service-oriented communication between components
- Repository pattern for data access abstraction
- Straightforward data models with clear relationships

### 1.2 Technology Stack
- **Framework**: ASP.NET 4.7 (Web Forms or MVC)
- **Database**: SQL Server 2022
- **Data Access**: ADO.NET with Repository Pattern
- **Service Layer**: C# Business Services
- **Authentication**: ASP.NET Forms Authentication with custom authorization

### 1.3 Architectural Principles
1. **Separation of Concerns**: Clear boundaries between presentation, business logic, and data access
2. **Single Responsibility**: Each component has one well-defined purpose
3. **Dependency Inversion**: Depend on abstractions (interfaces) not concrete implementations
4. **Loose Coupling**: Features communicate through well-defined service interfaces
5. **High Cohesion**: Related functionality grouped together within features
6. **Testability**: Design supports unit and integration testing

### 1.4 Layering Strategy


```
┌─────────────────────────────────────────────────────────┐
│         Presentation Layer (ASP.NET Web Forms/MVC)      │
│  - Account Management UI                                │
│  - Loan Information UI                                  │
│  - Account Operations UI                                │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│              Business Services Layer                     │
│  - Account Management Service                           │
│  - Account Query Service                                │
│  - Account Lifecycle Service                            │
│  - Loan Information Service                             │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│           Validation & Business Rules Layer             │
│  - Local Validators (basic field validation)            │
│  - Business Rule Validators                             │
│  - Integration with Compliance Unit (complex rules)     │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│              Data Access Layer (Repositories)           │
│  - IAccountRepository / AccountRepository               │
│  - ILoanInfoRepository / LoanInfoRepository             │
│  - IAuditRepository / AuditRepository                   │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│                SQL Server 2022 Database                  │
│  - Account Tables                                       │
│  - Audit Tables                                         │
└─────────────────────────────────────────────────────────┘
```

---

## 2. Feature Components

This unit is organized into three main feature components based on business capabilities:

### 2.1 Feature 1: General Account Management

**Purpose**: Handle core CRUD operations for loan accounts

**User Stories Covered**: US-001, US-002, US-003, US-004

**Responsibilities**:
- Create new loan accounts with general information
- View existing loan account details
- Update loan account general information
- Delete loan accounts (soft delete)
- Validate mandatory fields (Ref. No, Prev. Ref. No., Customer Name, Long Name)
- Handle duplicate reference number warnings
- Maintain audit trail

**Key Operations**:
- CreateAccount(accountData)
- GetAccount(accountId)
- UpdateAccount(accountId, accountData)
- DeleteAccount(accountId)
- ValidateDuplicateRefNo(refNo)

**Data Managed**:
- Reference Number, Previous Reference Number
- CRIB ID, NIDSS Account Number
- Customer Name, Long Name
- Account status and timestamps



### 2.2 Feature 2: Loan Information Management

**Purpose**: Manage detailed loan classification, dates, and attributes

**User Stories Covered**: US-015, US-016, US-017, US-018, US-019, US-020, US-021, US-022, US-023

**Responsibilities**:
- Manage account identification (Center Code, Budget Unit, Corporation, Book Code)
- Manage economic activity classification
- Manage loan dates (Original Release Date, Start of Term, Maturity Date)
- Manage account type and purpose
- Manage funding source and lending program
- Manage loan status and classification
- Manage guarantee information
- Manage litigation status
- Manage loan project type and currency
- Validate date relationships and conditional fields
- Auto-populate dependent fields

**Key Operations**:
- UpdateAccountIdentification(accountId, identificationData)
- UpdateEconomicActivity(accountId, economicActivityCode)
- UpdateLoanDates(accountId, datesData)
- UpdateAccountType(accountId, accountType, purpose)
- UpdateFundingSource(accountId, fundSource, program, area)
- UpdateLoanStatus(accountId, statusData)
- UpdateGuaranteeInfo(accountId, guaranteeData)
- UpdateLitigationStatus(accountId, isUnderLitigation)
- UpdateProjectTypeAndCurrency(accountId, projectType, currency)
- ValidateDateRules(datesData)
- ValidateConditionalFields(accountData)

**Data Managed**:
- Center Code, Budget Unit, Corporation, Book Code
- Economic Activity Code
- Original Release Date, Start of Term, Maturity Date
- Account Type, Purpose
- Fund Source, Lending Program, Area
- Restructured indicator, Type of Credit, Maturity Code, Purpose of Credit
- Guarantee indicator, Guaranteed By
- Litigation status and date
- Loan Status, Loan Project Type, Currency

### 2.3 Feature 3: Account Operations

**Purpose**: Provide search, copy, close, and archive operations

**User Stories Covered**: US-048, US-049, US-050, US-051

**Responsibilities**:
- Search accounts by multiple criteria
- Display account summary with key metrics
- Copy existing accounts for renewals
- Close and archive fully paid accounts
- Validate closure conditions
- Maintain account relationships (original to copied)

**Key Operations**:
- SearchAccounts(searchCriteria)
- GetAccountSummary(accountId)
- CopyAccount(sourceAccountId, newRefNo)
- CloseAccount(accountId)
- ArchiveAccount(accountId)
- ReopenAccount(accountId)
- GetArchivedAccounts(searchCriteria)
- ValidateClosureConditions(accountId)

**Data Managed**:
- Search indexes and filters
- Account summary aggregations
- Account relationships (original-to-copy links)
- Archive status and dates

---

## 3. Business Services Layer

The Business Services Layer encapsulates business operations and orchestrates interactions between features, repositories, and external services.



### 3.1 Account Management Service

**Interface**: IAccountManagementService

**Responsibilities**:
- Orchestrate account creation, update, and deletion
- Coordinate validation with local validators and Compliance Unit
- Manage transactions across multiple repositories
- Enforce business rules and access control
- Maintain audit trail

**Key Methods**:

**CreateAccount(accountData, userId)**
- Validates mandatory fields locally
- Calls Compliance Unit for complex validation
- Checks for duplicate reference numbers (warning only)
- Validates user permissions via Compliance Unit
- Creates account record via repository
- Logs audit trail
- Returns AccountId or validation errors

**GetAccount(accountId, userId)**
- Validates user has access to account's center/branch
- Retrieves account from repository
- Returns AccountDetails or NotFound error

**UpdateAccount(accountId, accountData, userId)**
- Validates user permissions (User or Authorizer role)
- Validates center/branch access
- Validates mandatory fields and business rules
- Updates account via repository
- Logs audit trail with changes
- Returns Success or validation errors

**DeleteAccount(accountId, userId)**
- Validates user permissions (Administrator role only)
- Checks for dependencies (transactions, balances, collateral)
- Performs soft delete (marks as deleted)
- Logs audit trail
- Returns Success or validation errors

**ValidateDuplicateRefNo(refNo)**
- Checks if reference number exists
- Returns warning message but allows override
- Used during account creation

### 3.2 Account Query Service

**Interface**: IAccountQueryService

**Responsibilities**:
- Provide read-only query operations
- Support complex search scenarios
- Generate account summaries
- Validate account existence

**Key Methods**:

**SearchAccounts(searchCriteria, userId)**
- Applies center/branch restrictions based on user
- Supports multiple search criteria (RefNo, CustomerName, CRIBID, TIN, AccountType, CenterCode, Status, DateRanges)
- Combines criteria with AND logic
- Returns paginated results
- Supports sorting and filtering

**GetAccountSummary(accountId, userId)**
- Validates user access
- Retrieves account with all related data
- Calculates key metrics (OPB, AIR, Past Due - from Financial Unit)
- Returns comprehensive summary

**GetAccountsByCustomer(customerId, userId)**
- Retrieves all accounts for a customer
- Applies center/branch restrictions
- Returns account list

**GetAccountsByCenter(centerCode, userId)**
- Validates user has access to center
- Retrieves all accounts for center
- Returns account list

**ValidateAccountExists(accountId)**
- Quick existence check
- Returns Boolean
- Used by other units for validation



### 3.3 Account Lifecycle Service

**Interface**: IAccountLifecycleService

**Responsibilities**:
- Handle account lifecycle operations (copy, close, archive, reopen)
- Validate lifecycle transition rules
- Maintain account relationships

**Key Methods**:

**CopyAccount(sourceAccountId, newRefNo, userId)**
- Validates source account exists
- Validates user permissions
- Creates new account with new reference number
- Copies customer information and loan structure
- Does NOT copy balances or transaction history
- Sets Prev. Ref. No. to source account's reference number
- Maintains link between original and copied account
- Returns NewAccountId

**CloseAccount(accountId, userId)**
- Validates user permissions
- Validates all balances are zero (OPB, AIR, Past Due)
- Marks account as closed with closure date
- Moves to archived status
- Generates closure report
- Returns Success or validation errors

**ArchiveAccount(accountId, userId)**
- Validates account is closed
- Moves account to archive storage
- Maintains full history
- Returns Success

**ReopenAccount(accountId, userId)**
- Validates user permissions (requires approval)
- Validates account is closed
- Reactivates account
- Logs audit trail
- Returns Success

**GetArchivedAccounts(searchCriteria, userId)**
- Searches archived accounts
- Applies center/branch restrictions
- Returns archived account list

### 3.4 Loan Information Service

**Interface**: ILoanInformationService

**Responsibilities**:
- Manage loan-specific attributes and classifications
- Validate date relationships and conditional fields
- Auto-populate dependent fields
- Integrate with Reference Data Unit for dropdown values

**Key Methods**:

**UpdateAccountIdentification(accountId, identificationData, userId)**
- Validates Center Code against Reference Data Unit
- Validates Corporation and Book Code selections
- Updates account identification fields
- Logs audit trail
- Returns Success or validation errors

**UpdateLoanDates(accountId, datesData, userId)**
- Validates Start of Term = Original Release Date
- Validates Maturity Date > Start of Term
- Updates date fields
- Returns Success or validation errors

**UpdateAccountType(accountId, accountType, purpose, userId)**
- Validates account type exists in Reference Data Unit
- Validates purpose is mandatory for specific account types (AA, AI, R, RDC, RDE, RDH)
- Auto-populates Type of Credit and Purpose of Credit (cannot override)
- Auto-populates GL accounts from Reference Data Unit
- Returns Success or validation errors

**UpdateLoanStatus(accountId, statusData, userId)**
- Updates restructured indicator, maturity code, number of records
- Auto-updates Type of Credit based on account status
- Validates status transitions
- Returns Success or validation errors

**UpdateLitigationStatus(accountId, isUnderLitigation, userId)**
- Updates litigation flag
- Auto-updates Type of Credit to LITIG when true
- Records litigation date
- Returns Success or validation errors

---

## 4. Data Models

The data model uses a hybrid approach: normalized key entities for clarity while maintaining practical flat structures where appropriate for teams familiar with traditional database design.



### 4.1 Account Entity (Primary)

**Table Name**: Account

**Purpose**: Core account information combining general and loan information

**Fields**:

**Primary Key**:
- AccountId (int, identity) - Internal surrogate key
- ReferenceNumber (varchar(17), unique) - Business key

**General Information**:
- PreviousReferenceNumber (varchar(17), not null)
- CRIBIDNumber (varchar(10), nullable)
- CustomerName (varchar(40), not null)
- NIDSSAccountNumber (varchar(13), nullable)
- LongName (varchar(100), not null)

**Account Identification**:
- CenterCode (varchar(2), not null)
- BudgetUnit (varchar(3), not null)
- Corporation (varchar(10), not null) - FK to ReferenceData
- BookCode (varchar(2), not null) - FK to ReferenceData

**Economic Classification**:
- EconomicActivityCode (varchar(6), not null) - FK to EconomicActivity

**Loan Dates**:
- OriginalReleaseDate (date, not null)
- StartOfTerm (date, not null)
- MaturityDate (date, not null)

**Account Type and Purpose**:
- AccountType (varchar(3), not null) - FK to AccountType
- Purpose (varchar(1), nullable) - Conditional based on AccountType

**Funding and Program**:
- FundSource (varchar(3), not null) - FK to ReferenceData
- LendingProgram (varchar(3), not null) - FK to ReferenceData
- Area (varchar(3), not null) - FK to ReferenceData

**Status and Classification**:
- IsRestructured (bit, not null, default 0)
- TypeOfCredit (varchar(6), not null) - Auto-populated
- MaturityCode (varchar(1), not null) - FK to ReferenceData
- PurposeOfCredit (varchar(1), not null) - Auto-populated
- NumberOfRecords (int, nullable)

**Guarantee Information**:
- IsGuaranteed (bit, not null, default 0)
- GuaranteedBy (varchar(10), nullable) - FK to ReferenceData

**Litigation**:
- IsUnderLitigation (bit, not null, default 0)
- LitigationDate (date, nullable)

**Loan Project and Currency**:
- LoanStatus (varchar(3), not null) - CUR or PDO
- LoanProjectType (varchar(1), not null) - C or D
- Currency (varchar(3), not null) - FK to ReferenceData

**Account Status**:
- Status (varchar(20), not null) - Active, Closed, Archived, Deleted
- IsDraft (bit, not null, default 0)
- ClosureDate (date, nullable)

**Audit Fields**:
- CreatedBy (varchar(50), not null)
- CreatedDate (datetime, not null)
- ModifiedBy (varchar(50), nullable)
- ModifiedDate (datetime, nullable)
- DeletedBy (varchar(50), nullable)
- DeletedDate (datetime, nullable)

**Indexes**:
- PK_Account_AccountId (Clustered)
- UK_Account_ReferenceNumber (Unique)
- IX_Account_CustomerName (Non-clustered)
- IX_Account_CenterCode (Non-clustered)
- IX_Account_Status (Non-clustered)
- IX_Account_AccountType (Non-clustered)

**Legacy Mapping**: Maps to MANSERV.DBF table



### 4.2 AccountAudit Entity

**Table Name**: AccountAudit

**Purpose**: Track all changes to account records for compliance and audit trail

**Fields**:
- AuditId (int, identity, PK)
- AccountId (int, not null, FK to Account)
- ReferenceNumber (varchar(17), not null)
- Action (varchar(20), not null) - Create, Update, Delete, Close, Archive, Reopen
- FieldName (varchar(100), nullable) - For Update actions
- OldValue (nvarchar(max), nullable)
- NewValue (nvarchar(max), nullable)
- ChangedBy (varchar(50), not null)
- ChangedDate (datetime, not null)
- UserRole (varchar(50), not null)
- IPAddress (varchar(50), nullable)
- Comments (nvarchar(500), nullable)

**Indexes**:
- PK_AccountAudit_AuditId (Clustered)
- IX_AccountAudit_AccountId (Non-clustered)
- IX_AccountAudit_ReferenceNumber (Non-clustered)
- IX_AccountAudit_ChangedDate (Non-clustered)

### 4.3 AccountRelationship Entity

**Table Name**: AccountRelationship

**Purpose**: Track relationships between accounts (e.g., original to copied accounts)

**Fields**:
- RelationshipId (int, identity, PK)
- SourceAccountId (int, not null, FK to Account)
- TargetAccountId (int, not null, FK to Account)
- RelationshipType (varchar(20), not null) - Copy, Restructure, Renewal
- CreatedBy (varchar(50), not null)
- CreatedDate (datetime, not null)

**Indexes**:
- PK_AccountRelationship_RelationshipId (Clustered)
- IX_AccountRelationship_SourceAccountId (Non-clustered)
- IX_AccountRelationship_TargetAccountId (Non-clustered)

### 4.4 Entity Relationships

```
Account (1) ←→ (Many) AccountAudit
Account (1) ←→ (Many) AccountRelationship (as Source)
Account (1) ←→ (Many) AccountRelationship (as Target)
Account (Many) → (1) ReferenceData (Corporation, BookCode, FundSource, etc.)
Account (Many) → (1) AccountType
Account (Many) → (1) EconomicActivity
```

**Note**: ReferenceData, AccountType, and EconomicActivity tables are owned by Unit 4 (Reference Data Management) and accessed read-only by this unit.

### 4.5 Field Mapping to Legacy MANSERV.DBF

| SQL Server Field | DBF Field | Notes |
|-----------------|-----------|-------|
| ReferenceNumber | REFNO | Primary business key |
| PreviousReferenceNumber | PREVREF | |
| CRIBIDNumber | CRIBID | |
| CustomerName | CUSNAME | |
| NIDSSAccountNumber | NIDSS | |
| LongName | LONGNAME | |
| CenterCode | CNT_CENTER | |
| BudgetUnit | BUNIT | |
| Corporation | CORP | |
| BookCode | BOOKCDE | |
| EconomicActivityCode | CNT_ECON | |
| OriginalReleaseDate | ORELDAT | |
| StartOfTerm | CNT_STERM | |
| MaturityDate | CNT_MATD | |
| AccountType | CNT_ATYPE | |
| Purpose | CNT_PURP | |
| FundSource | CNT_FUND | |
| LendingProgram | CNT_PROG | |
| Area | AREA | |
| IsRestructured | REST | Y/N → bit |
| TypeOfCredit | CNT_CRTYPE | |
| MaturityCode | CNT_MAT | |
| PurposeOfCredit | CNT_CRPURP | |
| NumberOfRecords | CNT_REC | |
| IsGuaranteed | GUAR | Y/N → bit |
| GuaranteedBy | CNT_GBY | |
| IsUnderLitigation | LITIG | Y/N → bit |
| LitigationDate | ITL | |
| LoanStatus | CNT_LSTAT | |
| LoanProjectType | CNT_LPTYPE | |
| Currency | RELCURR | |

---

## 5. Repository Pattern

The Repository Pattern abstracts data access logic and provides a clean separation between business logic and data access.



### 5.1 IAccountRepository Interface

**Purpose**: Define contract for account data access operations

**Methods**:

**Create(account)**
- Inserts new account record
- Returns generated AccountId
- Throws exception on constraint violations

**GetById(accountId)**
- Retrieves account by internal ID
- Returns Account entity or null

**GetByReferenceNumber(referenceNumber)**
- Retrieves account by business key
- Returns Account entity or null

**Update(account)**
- Updates existing account record
- Returns number of rows affected
- Throws exception on concurrency conflicts

**Delete(accountId, userId)**
- Performs soft delete (sets Status = 'Deleted', DeletedBy, DeletedDate)
- Returns success boolean

**Search(searchCriteria)**
- Executes complex search with multiple criteria
- Returns list of Account entities
- Supports pagination (skip, take)
- Supports sorting

**ExistsByReferenceNumber(referenceNumber)**
- Quick existence check
- Returns boolean

**GetByCustomerId(customerId)**
- Retrieves all accounts for a customer
- Returns list of Account entities

**GetByCenterCode(centerCode)**
- Retrieves all accounts for a center
- Returns list of Account entities

**GetArchivedAccounts(searchCriteria)**
- Retrieves archived accounts
- Returns list of Account entities

### 5.2 IAccountAuditRepository Interface

**Purpose**: Define contract for audit trail operations

**Methods**:

**Create(auditEntry)**
- Inserts audit record
- Returns generated AuditId

**GetByAccountId(accountId)**
- Retrieves all audit entries for an account
- Returns list of AccountAudit entities
- Ordered by ChangedDate descending

**GetByReferenceNumber(referenceNumber)**
- Retrieves all audit entries for a reference number
- Returns list of AccountAudit entities

**Search(searchCriteria)**
- Searches audit log with filters
- Returns list of AccountAudit entities

### 5.3 IAccountRelationshipRepository Interface

**Purpose**: Define contract for account relationship operations

**Methods**:

**Create(relationship)**
- Inserts relationship record
- Returns generated RelationshipId

**GetBySourceAccountId(sourceAccountId)**
- Retrieves all relationships where account is source
- Returns list of AccountRelationship entities

**GetByTargetAccountId(targetAccountId)**
- Retrieves all relationships where account is target
- Returns list of AccountRelationship entities

**GetRelationshipChain(accountId)**
- Retrieves full chain of related accounts
- Returns list of Account entities

### 5.4 Repository Implementation Guidelines

**Connection Management**:
- Use connection pooling (default in ADO.NET)
- Open connections late, close early
- Use using statements for automatic disposal
- Connection string stored in web.config

**Transaction Management**:
- Use TransactionScope for distributed transactions
- Use SqlTransaction for single-database transactions
- Repositories do not manage transactions (handled by services)
- Support ambient transactions

**Error Handling**:
- Catch SqlException and translate to domain exceptions
- Log all database errors
- Provide meaningful error messages
- Don't expose SQL details to upper layers

**Performance Considerations**:
- Use parameterized queries to prevent SQL injection
- Use stored procedures for complex operations
- Implement efficient indexing strategy
- Use async/await for I/O operations (if ASP.NET 4.7 supports)
- Cache reference data lookups

**Example Repository Structure** (Conceptual):

```
AccountRepository : IAccountRepository
{
    - connectionString (from configuration)
    
    + Create(account) : int
    + GetById(accountId) : Account
    + GetByReferenceNumber(referenceNumber) : Account
    + Update(account) : int
    + Delete(accountId, userId) : bool
    + Search(searchCriteria) : List<Account>
    + ExistsByReferenceNumber(referenceNumber) : bool
    
    - ExecuteQuery(sql, parameters) : DataTable
    - ExecuteNonQuery(sql, parameters) : int
    - MapDataRowToAccount(dataRow) : Account
}
```

---

## 6. Service Interfaces

This section defines the service interfaces exposed by this unit for consumption by other units, and the interfaces consumed from other units.



### 6.1 Exposed Service Interfaces

These interfaces are exposed for consumption by other units (Unit 3: Financial Management, Unit 6: Reporting, Unit 7: Collateral & GL).

#### 6.1.1 IAccountManagementService (Public Interface)

**Service Contract**:

```
CreateAccount(accountData, userId) : ServiceResponse<int>
GetAccount(accountId, userId) : ServiceResponse<AccountDetails>
UpdateAccount(accountId, accountData, userId) : ServiceResponse<bool>
DeleteAccount(accountId, userId) : ServiceResponse<bool>
ValidateDuplicateRefNo(refNo) : ServiceResponse<bool>
```

**Request/Response Format**:
- All methods return ServiceResponse<T> with Status, Data, Errors
- accountData is AccountDTO (Data Transfer Object)
- userId for authentication and audit trail

#### 6.1.2 IAccountQueryService (Public Interface)

**Service Contract**:

```
SearchAccounts(searchCriteria, userId) : ServiceResponse<PagedResult<AccountSummary>>
GetAccountSummary(accountId, userId) : ServiceResponse<AccountSummary>
GetAccountsByCustomer(customerId, userId) : ServiceResponse<List<AccountSummary>>
GetAccountsByCenter(centerCode, userId) : ServiceResponse<List<AccountSummary>>
ValidateAccountExists(accountId) : ServiceResponse<bool>
GetAccountStatus(accountId) : ServiceResponse<string>
```

#### 6.1.3 IAccountLifecycleService (Public Interface)

**Service Contract**:

```
CopyAccount(sourceAccountId, newRefNo, userId) : ServiceResponse<int>
CloseAccount(accountId, userId) : ServiceResponse<bool>
ArchiveAccount(accountId, userId) : ServiceResponse<bool>
ReopenAccount(accountId, userId) : ServiceResponse<bool>
GetArchivedAccounts(searchCriteria, userId) : ServiceResponse<List<AccountSummary>>
```

### 6.2 Consumed Service Interfaces

These interfaces are consumed from other units.

#### 6.2.1 From Unit 2: Customer Management

**ICustomerQueryService**:

```
GetCustomerInfo(customerId) : ServiceResponse<CustomerDetails>
ValidateCustomerExists(customerId) : ServiceResponse<bool>
```

**Usage**:
- Validate customer exists before creating account
- Retrieve customer details for account summary
- Link accounts to customers

#### 6.2.2 From Unit 4: Reference Data Management

**IReferenceDataService**:

```
GetReferenceData(groupNumber) : ServiceResponse<List<ReferenceDataItem>>
GetReferenceDataByCode(groupNumber, code) : ServiceResponse<ReferenceDataItem>
ValidateReferenceCode(code, type) : ServiceResponse<bool>
```

**IAccountTypeService**:

```
GetAccountTypes() : ServiceResponse<List<AccountType>>
GetAccountType(code) : ServiceResponse<AccountType>
GetGLMappings(accountTypeCode, economicActivityCode) : ServiceResponse<GLAccounts>
```

**IEconomicActivityService**:

```
GetEconomicActivities() : ServiceResponse<List<EconomicActivity>>
GetEconomicActivity(code) : ServiceResponse<EconomicActivity>
```

**ICenterService**:

```
GetCenters() : ServiceResponse<List<Center>>
GetCenter(code) : ServiceResponse<Center>
ValidateCenter(code) : ServiceResponse<bool>
```

**Usage**:
- Populate dropdowns (Corporation, Book Code, Fund Source, etc.)
- Validate user-selected codes
- Auto-populate GL accounts based on account type
- Validate center codes

#### 6.2.3 From Unit 5: Compliance & Validation

**IValidationService**:

```
ValidateMandatoryFields(entityData) : ServiceResponse<ValidationResult>
ValidateFieldFormats(entityData) : ServiceResponse<ValidationResult>
ValidateCrossFieldRules(entityData) : ServiceResponse<ValidationResult>
ValidateConditionalFields(entityData) : ServiceResponse<ValidationResult>
```

**IAuditService**:

```
AuditLog(action, userId, resourceId, changes) : ServiceResponse<bool>
```

**IAccessControlService**:

```
CheckUserPermission(userId, action, resourceId) : ServiceResponse<bool>
GetUserRole(userId) : ServiceResponse<string>
RestrictByCenter(userId, query) : ServiceResponse<FilteredQuery>
```

**Usage**:
- Validate complex business rules
- Log all operations for audit trail
- Check user permissions before operations
- Apply center/branch restrictions to queries

### 6.3 Service Communication Pattern

**Dependency Injection**:
- Services depend on interfaces, not concrete implementations
- Interfaces injected via constructor injection
- Supports unit testing with mock implementations

**Error Handling**:
- All service calls return ServiceResponse<T>
- ServiceResponse includes Status (Success/Failure), Data, Errors
- Errors include ErrorCode, Message, Field
- Calling service handles errors appropriately

**Timeout and Retry**:
- Default timeout: 30 seconds
- Retry logic: 3 attempts with exponential backoff
- Circuit breaker pattern for external service failures

**Example Service Response Structure**:

```
ServiceResponse<T>
{
    Status: Success | Failure
    Data: T (generic type)
    Errors: List<ServiceError>
    {
        ErrorCode: string
        Message: string
        Field: string (optional)
    }
}
```

---

## 7. Validation and Business Rules

This unit implements a hybrid validation approach: basic validations performed locally for performance, complex business rules delegated to Compliance & Validation Unit.



### 7.1 Local Validation (Performed in this Unit)

**Mandatory Field Validation**:
- Reference Number (max 17 characters)
- Previous Reference Number (max 17 characters)
- Customer Name (max 40 characters)
- Long Name (max 100 characters)
- Performed before service call to Compliance Unit

**Field Length Validation**:
- All varchar fields validated against max length
- Performed locally for immediate feedback

**Data Type Validation**:
- Date fields validated as valid dates
- Numeric fields validated as valid numbers
- Boolean fields validated as true/false
- Performed locally

**Duplicate Reference Number Check**:
- Check if reference number already exists
- Return warning but allow override
- Performed locally via repository

### 7.2 Business Rules Validation (Delegated to Compliance Unit)

**Date Relationship Rules**:
- Start of Term = Original Release Date (must be equal)
- Maturity Date > Start of Term (strict enforcement)
- Validated via IValidationService.ValidateCrossFieldRules()

**Conditional Field Rules**:
- Purpose is mandatory when Account Type is AA, AI, R, RDC, RDE, or RDH
- Guaranteed By is enabled only when Guaranteed = Y (but not mandatory)
- Validated via IValidationService.ValidateConditionalFields()

**Deletion Rules**:
- Prevent deletion if transaction history exists (check with Financial Unit)
- Prevent deletion if active balances exist (check with Financial Unit)
- Prevent deletion if collateral exists (check with Collateral Unit)
- Validated via business logic in DeleteAccount service method

**Status Transition Rules**:
- Current to Past Due: Automatic after 90 days (handled by batch process in Compliance Unit)
- Litigation status: Manual only
- Validated via IValidationService.ValidateBusinessRules()

### 7.3 Auto-Population Rules

**Type of Credit**:
- Auto-populated based on Account Type and Status
- Cannot be overridden by users
- Logic implemented in UpdateAccountType service method
- Mapping maintained in Reference Data Unit

**Purpose of Credit**:
- Auto-populated based on Account Type
- Cannot be overridden by users
- Logic implemented in UpdateAccountType service method
- Mapping maintained in Reference Data Unit

**GL Account Codes**:
- Auto-populated from RLSACCT.DBF based on Account Type and Economic Activity
- Can be overridden by users if needed
- Retrieved via IReferenceDataService.GetGLMappings()

### 7.4 Validation Flow

```
User Input → Local Validation (immediate feedback)
    ↓
Service Layer → Compliance Unit Validation (business rules)
    ↓
Repository Layer → Database Constraints (final enforcement)
```

**Validation Response**:
- All validations return ValidationResult with list of errors
- Each error includes: ErrorCode, Message, Field
- UI displays all validation errors to user
- No partial saves (all-or-nothing)

### 7.5 Validation Error Codes

| Error Code | Description | Severity |
|-----------|-------------|----------|
| MANDATORY_FIELD | Required field is missing | Error |
| INVALID_LENGTH | Field exceeds max length | Error |
| INVALID_FORMAT | Field format is invalid | Error |
| DUPLICATE_REFNO | Reference number already exists | Warning |
| INVALID_DATE_RANGE | Date relationship violated | Error |
| CONDITIONAL_REQUIRED | Conditional field is required | Error |
| INVALID_STATUS_TRANSITION | Status transition not allowed | Error |
| HAS_DEPENDENCIES | Cannot delete due to dependencies | Error |
| UNAUTHORIZED | User not authorized for action | Error |
| INVALID_CENTER | User not authorized for center | Error |

---

## 8. Access Control and Security

This unit implements role-based access control (RBAC) with center/branch restrictions.

### 8.1 User Roles

**User**:
- Can create accounts
- Can view accounts in authorized centers
- Can update accounts in authorized centers
- Cannot delete accounts

**Authorizer**:
- All User permissions
- Can authorize/approve account changes
- Can update accounts in authorized centers
- Cannot delete accounts

**Administrator**:
- All Authorizer permissions
- Can delete accounts (soft delete)
- Can access all centers
- Can reopen closed accounts

### 8.2 Center/Branch Restrictions

**Data Access Rules**:
- Users can only access accounts in their assigned centers/branches
- Center assignments stored in user profile (managed by Compliance Unit)
- All queries filtered by user's authorized centers
- Administrators have access to all centers

**Implementation**:
- IAccessControlService.RestrictByCenter(userId, query) applies filters
- Repository queries include center filter
- Service layer validates center access before operations

### 8.3 Operation Permissions

| Operation | User | Authorizer | Administrator |
|-----------|------|------------|---------------|
| Create Account | ✓ | ✓ | ✓ |
| View Account | ✓ (own centers) | ✓ (own centers) | ✓ (all centers) |
| Update Account | ✓ (own centers) | ✓ (own centers) | ✓ (all centers) |
| Delete Account | ✗ | ✗ | ✓ |
| Close Account | ✓ (own centers) | ✓ (own centers) | ✓ (all centers) |
| Reopen Account | ✗ | ✗ | ✓ |
| Copy Account | ✓ (own centers) | ✓ (own centers) | ✓ (all centers) |

### 8.4 Audit Trail

**All Operations Logged**:
- Create, Update, Delete, Close, Archive, Reopen
- User ID, Role, Timestamp, IP Address
- Field-level changes for Update operations (old value → new value)
- Stored in AccountAudit table

**Audit Log Access**:
- Users can view audit log for accounts they can access
- Administrators can view all audit logs
- Audit logs cannot be modified or deleted

### 8.5 Security Implementation

**Authentication**:
- ASP.NET Forms Authentication
- User credentials validated against user database
- Session management with timeout

**Authorization**:
- Role-based authorization via IAccessControlService
- Permission checks before every operation
- Center/branch restrictions applied to all queries

**Data Protection**:
- Parameterized queries to prevent SQL injection
- Input validation to prevent XSS attacks
- Sensitive data encrypted in database (if applicable)
- Connection strings encrypted in web.config

---

## 9. Integration Points

This section details how this unit integrates with other units.



### 9.1 Dependencies (Services Consumed)

#### 9.1.1 Unit 2: Customer Management

**Integration Points**:
- Validate customer exists before creating account
- Retrieve customer details for account summary display
- Link accounts to customer records

**Service Calls**:
- ICustomerQueryService.ValidateCustomerExists(customerId)
- ICustomerQueryService.GetCustomerInfo(customerId)

**Error Handling**:
- If customer doesn't exist, return validation error
- If customer service unavailable, log error and retry

#### 9.1.2 Unit 4: Reference Data Management

**Integration Points**:
- Populate dropdowns for all reference data fields
- Validate user-selected codes against reference data
- Retrieve GL account mappings for auto-population
- Validate center codes

**Service Calls**:
- IReferenceDataService.GetReferenceData(groupNumber) - for dropdowns
- IAccountTypeService.GetAccountTypes() - for account type dropdown
- IAccountTypeService.GetGLMappings(accountTypeCode, economicActivityCode) - for GL auto-population
- IEconomicActivityService.GetEconomicActivities() - for economic activity dropdown
- ICenterService.ValidateCenter(code) - for center validation

**Caching Strategy**:
- Cache reference data locally for 1 hour
- Refresh cache on demand or when data changes
- Reduces service calls and improves performance

**Error Handling**:
- If reference data service unavailable, use cached data
- If no cached data, display error to user
- Log all service failures

#### 9.1.3 Unit 5: Compliance & Validation

**Integration Points**:
- Validate business rules and cross-field validations
- Log all operations for audit trail
- Check user permissions before operations
- Apply center/branch restrictions to queries

**Service Calls**:
- IValidationService.ValidateMandatoryFields(accountData)
- IValidationService.ValidateCrossFieldRules(accountData)
- IValidationService.ValidateConditionalFields(accountData)
- IAuditService.AuditLog(action, userId, accountId, changes)
- IAccessControlService.CheckUserPermission(userId, action, resourceId)
- IAccessControlService.GetUserRole(userId)
- IAccessControlService.RestrictByCenter(userId, query)

**Error Handling**:
- If validation service unavailable, perform local validation only and log warning
- If audit service unavailable, queue audit log for later processing
- If access control service unavailable, deny operation (fail-safe)

### 9.2 Consumers (Units that Consume this Unit's Services)

#### 9.2.1 Unit 3: Financial Management

**Services Consumed**:
- IAccountManagementService.GetAccount(accountId, userId)
- IAccountManagementService.UpdateAccount(accountId, accountData, userId)
- IAccountQueryService.ValidateAccountExists(accountId)
- IAccountQueryService.GetAccountStatus(accountId)

**Use Cases**:
- Validate account exists before posting transactions
- Retrieve account details for balance calculations
- Update account status based on payment history
- Check account status before allowing transactions

#### 9.2.2 Unit 6: Reporting & Analytics (Optional)

**Services Consumed**:
- IAccountQueryService.SearchAccounts(searchCriteria, userId)
- IAccountQueryService.GetAccountSummary(accountId, userId)
- IAccountQueryService.GetAccountsByCenter(centerCode, userId)

**Use Cases**:
- Generate portfolio reports
- Analyze account distribution by center, type, status
- Export account data for external reporting

#### 9.2.3 Unit 7: Collateral & GL Management (Optional)

**Services Consumed**:
- IAccountManagementService.GetAccount(accountId, userId)
- IAccountQueryService.ValidateAccountExists(accountId)

**Use Cases**:
- Link collateral to accounts
- Post GL entries for account transactions
- Validate account exists before adding collateral

### 9.3 Integration Patterns

**Synchronous API Calls**:
- Primary communication pattern
- Request-response model
- Timeout: 30 seconds default
- Retry: 3 attempts with exponential backoff (1s, 2s, 4s)

**Service Discovery**:
- Service endpoints configured in web.config
- Supports multiple environments (dev, test, prod)
- Can be updated without recompilation

**Error Handling**:
- All service calls wrapped in try-catch
- Log all service failures
- Return standardized error responses
- Graceful degradation where possible

**Transaction Coordination**:
- Each unit manages its own transactions
- No distributed transactions across units
- Compensating transactions for rollback scenarios
- Audit trail tracks all transaction steps

### 9.4 Integration Flow Examples

**Example 1: Create Account Flow**

```
User → Presentation Layer → AccountManagementService
    ↓
Local Validation (mandatory fields, lengths)
    ↓
IAccessControlService.CheckUserPermission(userId, "CreateAccount")
    ↓
ICustomerQueryService.ValidateCustomerExists(customerId)
    ↓
IReferenceDataService.ValidateReferenceCode(centerCode, "Center")
    ↓
IValidationService.ValidateCrossFieldRules(accountData)
    ↓
AccountRepository.Create(account)
    ↓
IAuditService.AuditLog("Create", userId, accountId, accountData)
    ↓
Return ServiceResponse<AccountId>
```

**Example 2: Update Account Type Flow**

```
User → Presentation Layer → LoanInformationService.UpdateAccountType()
    ↓
IAccessControlService.CheckUserPermission(userId, "UpdateAccount")
    ↓
AccountRepository.GetById(accountId)
    ↓
IAccountTypeService.GetAccountType(accountTypeCode)
    ↓
IValidationService.ValidateConditionalFields(accountData) [Purpose validation]
    ↓
Auto-populate Type of Credit and Purpose of Credit
    ↓
IAccountTypeService.GetGLMappings(accountTypeCode, economicActivityCode)
    ↓
AccountRepository.Update(account)
    ↓
IAuditService.AuditLog("Update", userId, accountId, changes)
    ↓
Return ServiceResponse<bool>
```

---

## 10. Component Interactions

This section describes how components within this unit interact with each other.



### 10.1 Presentation Layer to Service Layer

**Interaction Pattern**:
- Presentation layer (Web Forms/MVC) calls service layer methods
- Passes Data Transfer Objects (DTOs) to services
- Receives ServiceResponse<T> from services
- Handles success and error responses
- Displays validation errors to user

**Example Interaction**:

```
AccountManagementPage (Presentation)
    ↓
    Calls: accountManagementService.CreateAccount(accountDTO, userId)
    ↓
AccountManagementService (Business Service)
    ↓
    Returns: ServiceResponse<int> with AccountId or Errors
    ↓
AccountManagementPage displays success message or validation errors
```

**Data Transfer Objects (DTOs)**:
- AccountDTO: Contains all account fields for create/update
- AccountSummaryDTO: Contains summary information for display
- SearchCriteriaDTO: Contains search parameters
- DTOs are simple data containers, no business logic

### 10.2 Service Layer to Repository Layer

**Interaction Pattern**:
- Service layer calls repository methods
- Passes entity objects to repositories
- Receives entity objects or primitive types from repositories
- Handles database exceptions
- Manages transactions (if needed)

**Example Interaction**:

```
AccountManagementService (Business Service)
    ↓
    Calls: accountRepository.Create(accountEntity)
    ↓
AccountRepository (Data Access)
    ↓
    Executes SQL INSERT
    ↓
    Returns: AccountId (int)
    ↓
AccountManagementService returns ServiceResponse<int>
```

**Transaction Management**:
- Services manage transaction boundaries
- Use TransactionScope for operations spanning multiple repositories
- Repositories participate in ambient transactions
- Rollback on any exception

### 10.3 Service Layer to External Services

**Interaction Pattern**:
- Service layer calls external service interfaces
- Passes request objects to external services
- Receives ServiceResponse<T> from external services
- Handles service failures gracefully
- Implements retry logic

**Example Interaction**:

```
AccountManagementService (Business Service)
    ↓
    Calls: validationService.ValidateCrossFieldRules(accountData)
    ↓
ValidationService (External - Compliance Unit)
    ↓
    Returns: ServiceResponse<ValidationResult>
    ↓
AccountManagementService checks ValidationResult
    ↓
    If valid: proceed with create
    If invalid: return errors to presentation layer
```

### 10.4 Cross-Feature Communication

**Within Unit 1**:
- Features communicate through shared services
- AccountManagementService used by all features
- AccountQueryService provides read-only access
- AccountLifecycleService handles lifecycle operations

**Example**:

```
Account Operations Feature (Copy Account)
    ↓
    Calls: accountLifecycleService.CopyAccount(sourceAccountId, newRefNo, userId)
    ↓
AccountLifecycleService
    ↓
    Calls: accountQueryService.GetAccount(sourceAccountId, userId)
    ↓
    Calls: accountManagementService.CreateAccount(newAccountData, userId)
    ↓
    Calls: accountRelationshipRepository.Create(relationship)
    ↓
    Returns: ServiceResponse<int> with NewAccountId
```

### 10.5 Dependency Injection Configuration

**Service Registration** (Conceptual):

```
Container Registration:
- IAccountManagementService → AccountManagementService
- IAccountQueryService → AccountQueryService
- IAccountLifecycleService → AccountLifecycleService
- ILoanInformationService → LoanInformationService
- IAccountRepository → AccountRepository
- IAccountAuditRepository → AccountAuditRepository
- IAccountRelationshipRepository → AccountRelationshipRepository

External Service Registration:
- ICustomerQueryService → CustomerQueryServiceProxy
- IReferenceDataService → ReferenceDataServiceProxy
- IValidationService → ValidationServiceProxy
- IAuditService → AuditServiceProxy
- IAccessControlService → AccessControlServiceProxy
```

**Lifetime Management**:
- Services: Scoped (per request)
- Repositories: Scoped (per request)
- External service proxies: Singleton (stateless)

---

## 11. Non-Functional Considerations

### 11.1 Performance

**Database Performance**:
- Implement proper indexing on frequently queried columns
- Use parameterized queries for query plan caching
- Implement pagination for large result sets
- Use stored procedures for complex operations
- Monitor slow queries and optimize

**Caching Strategy**:
- Cache reference data locally (1 hour TTL)
- Cache user permissions (session lifetime)
- Cache dropdown values (1 hour TTL)
- Use ASP.NET Cache or distributed cache (Redis)

**Search Performance**:
- Implement full-text search for customer name searches
- Use indexed views for complex queries
- Limit result sets to 1000 records max
- Provide pagination controls

**Expected Performance Targets**:
- Account creation: < 2 seconds
- Account retrieval: < 500ms
- Search with filters: < 3 seconds
- Update operations: < 2 seconds

### 11.2 Scalability

**Horizontal Scalability**:
- Stateless service layer supports multiple web servers
- Use load balancer for web tier
- Database connection pooling
- Distributed caching for session state

**Vertical Scalability**:
- SQL Server 2022 supports large databases
- Optimize queries for large datasets
- Partition large tables if needed
- Archive old data to separate tables

**Data Growth**:
- Expected growth: 10,000 new accounts per year
- Audit table growth: 100,000 records per year
- Implement data archival strategy
- Purge old audit logs after retention period

### 11.3 Reliability

**Error Handling**:
- All exceptions caught and logged
- User-friendly error messages displayed
- Technical details logged for troubleshooting
- No sensitive information in error messages

**Logging**:
- Log all service calls (request/response)
- Log all database operations
- Log all external service calls
- Log all errors with stack traces
- Use structured logging (log4net or NLog)

**Monitoring**:
- Monitor service response times
- Monitor database performance
- Monitor external service availability
- Alert on errors and performance degradation

**Backup and Recovery**:
- Daily database backups
- Transaction log backups every 15 minutes
- Test restore procedures regularly
- Document recovery procedures

### 11.4 Maintainability

**Code Organization**:
- Clear folder structure by feature
- Consistent naming conventions
- Separation of concerns
- Single responsibility principle

**Documentation**:
- XML comments for all public methods
- README files for each feature
- Architecture decision records (ADRs)
- API documentation for service interfaces

**Testing**:
- Unit tests for business logic
- Integration tests for repositories
- Service integration tests with mocks
- UI tests for critical workflows
- Target: 80% code coverage

### 11.5 Security

**Data Security**:
- Encrypt sensitive data at rest (if applicable)
- Use SSL/TLS for data in transit
- Parameterized queries prevent SQL injection
- Input validation prevents XSS attacks

**Authentication & Authorization**:
- Strong password policies
- Session timeout after inactivity
- Role-based access control
- Center/branch restrictions enforced

**Audit Trail**:
- All operations logged
- Audit logs immutable
- Audit logs retained per compliance requirements
- Regular audit log reviews

### 11.6 Deployment

**Deployment Strategy**:
- Deploy to test environment first
- Smoke tests after deployment
- Rollback plan for failed deployments
- Blue-green deployment for zero downtime

**Configuration Management**:
- Environment-specific configurations
- Connection strings in web.config
- Service endpoints configurable
- Feature flags for optional features

**Database Deployment**:
- Database migration scripts
- Version control for schema changes
- Test migrations in lower environments
- Rollback scripts for failed migrations

---

## 12. Architecture Diagrams

### 12.1 High-Level Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                        PRESENTATION LAYER                        │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │   General    │  │  Loan Info   │  │   Account    │          │
│  │  Account UI  │  │  Management  │  │ Operations   │          │
│  │              │  │      UI      │  │      UI      │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
└─────────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────────┐
│                      BUSINESS SERVICES LAYER                     │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │   Account    │  │   Account    │  │   Account    │          │
│  │  Management  │  │    Query     │  │  Lifecycle   │          │
│  │   Service    │  │   Service    │  │   Service    │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
│  ┌──────────────┐                                               │
│  │  Loan Info   │                                               │
│  │   Service    │                                               │
│  └──────────────┘                                               │
└─────────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────────┐
│                   VALIDATION & BUSINESS RULES                    │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │    Local     │  │  Compliance  │  │   Access     │          │
│  │  Validators  │  │     Unit     │  │   Control    │          │
│  │              │  │  Integration │  │              │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
└─────────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────────┐
│                      DATA ACCESS LAYER                           │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │   Account    │  │    Audit     │  │ Relationship │          │
│  │  Repository  │  │  Repository  │  │  Repository  │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
└─────────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────────┐
│                      SQL SERVER 2022                             │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │   Account    │  │ AccountAudit │  │   Account    │          │
│  │    Table     │  │    Table     │  │ Relationship │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
└─────────────────────────────────────────────────────────────────┘
```



### 12.2 Feature Component Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                  UNIT 1: LOAN ACCOUNT MANAGEMENT                 │
│                                                                  │
│  ┌────────────────────────────────────────────────────────┐    │
│  │  FEATURE 1: GENERAL ACCOUNT MANAGEMENT                 │    │
│  │  - Create Account (US-001)                             │    │
│  │  - View Account (US-002)                               │    │
│  │  - Update Account (US-003)                             │    │
│  │  - Delete Account (US-004)                             │    │
│  └────────────────────────────────────────────────────────┘    │
│                           ↓                                      │
│  ┌────────────────────────────────────────────────────────┐    │
│  │  FEATURE 2: LOAN INFORMATION MANAGEMENT                │    │
│  │  - Account Identification (US-015)                     │    │
│  │  - Economic Activity (US-016)                          │    │
│  │  - Loan Dates (US-017)                                 │    │
│  │  - Account Type & Purpose (US-018)                     │    │
│  │  - Funding Source & Program (US-019)                   │    │
│  │  - Loan Status & Classification (US-020)               │    │
│  │  - Guarantee Information (US-021)                      │    │
│  │  - Litigation Status (US-022)                          │    │
│  │  - Project Type & Currency (US-023)                    │    │
│  └────────────────────────────────────────────────────────┘    │
│                           ↓                                      │
│  ┌────────────────────────────────────────────────────────┐    │
│  │  FEATURE 3: ACCOUNT OPERATIONS                         │    │
│  │  - Search Accounts (US-048)                            │    │
│  │  - View Account Summary (US-049)                       │    │
│  │  - Copy Account (US-050)                               │    │
│  │  - Close/Archive Account (US-051)                      │    │
│  └────────────────────────────────────────────────────────┘    │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

### 12.3 Service Dependency Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│              UNIT 1: LOAN ACCOUNT MANAGEMENT                     │
│                                                                  │
│  ┌──────────────────────────────────────────────────────┐      │
│  │         EXPOSED SERVICES (Public Interfaces)         │      │
│  │  - IAccountManagementService                         │      │
│  │  - IAccountQueryService                              │      │
│  │  - IAccountLifecycleService                          │      │
│  └──────────────────────────────────────────────────────┘      │
│                           ↑                                      │
│                           │ Consumed by                          │
│                           │                                      │
│  ┌────────────────────────┴──────────────────────────────┐     │
│  │  Unit 3: Financial Management                         │     │
│  │  Unit 6: Reporting & Analytics (Optional)             │     │
│  │  Unit 7: Collateral & GL Management (Optional)        │     │
│  └───────────────────────────────────────────────────────┘     │
│                                                                  │
│  ┌──────────────────────────────────────────────────────┐      │
│  │         CONSUMED SERVICES (Dependencies)             │      │
│  │                                                       │      │
│  │  From Unit 2: Customer Management                    │      │
│  │  - ICustomerQueryService                             │      │
│  │                                                       │      │
│  │  From Unit 4: Reference Data Management              │      │
│  │  - IReferenceDataService                             │      │
│  │  - IAccountTypeService                               │      │
│  │  - IEconomicActivityService                          │      │
│  │  - ICenterService                                    │      │
│  │                                                       │      │
│  │  From Unit 5: Compliance & Validation                │      │
│  │  - IValidationService                                │      │
│  │  - IAuditService                                     │      │
│  │  - IAccessControlService                             │      │
│  └──────────────────────────────────────────────────────┘      │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

### 12.4 Data Model Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                         ACCOUNT TABLE                            │
│  PK: AccountId (int, identity)                                  │
│  UK: ReferenceNumber (varchar(17))                              │
│                                                                  │
│  General Information:                                            │
│  - PreviousReferenceNumber, CRIBIDNumber, CustomerName          │
│  - NIDSSAccountNumber, LongName                                 │
│                                                                  │
│  Account Identification:                                         │
│  - CenterCode, BudgetUnit, Corporation, BookCode                │
│                                                                  │
│  Loan Information:                                               │
│  - EconomicActivityCode, OriginalReleaseDate, StartOfTerm       │
│  - MaturityDate, AccountType, Purpose, FundSource               │
│  - LendingProgram, Area, IsRestructured, TypeOfCredit           │
│  - MaturityCode, PurposeOfCredit, NumberOfRecords               │
│  - IsGuaranteed, GuaranteedBy, IsUnderLitigation                │
│  - LitigationDate, LoanStatus, LoanProjectType, Currency        │
│                                                                  │
│  Status & Audit:                                                 │
│  - Status, IsDraft, ClosureDate                                 │
│  - CreatedBy, CreatedDate, ModifiedBy, ModifiedDate             │
│  - DeletedBy, DeletedDate                                       │
└─────────────────────────────────────────────────────────────────┘
                              │
                              │ 1:N
                              ↓
┌─────────────────────────────────────────────────────────────────┐
│                      ACCOUNTAUDIT TABLE                          │
│  PK: AuditId (int, identity)                                    │
│  FK: AccountId → Account.AccountId                              │
│                                                                  │
│  - ReferenceNumber, Action, FieldName                           │
│  - OldValue, NewValue                                           │
│  - ChangedBy, ChangedDate, UserRole, IPAddress, Comments        │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                   ACCOUNTRELATIONSHIP TABLE                      │
│  PK: RelationshipId (int, identity)                             │
│  FK: SourceAccountId → Account.AccountId                        │
│  FK: TargetAccountId → Account.AccountId                        │
│                                                                  │
│  - RelationshipType (Copy, Restructure, Renewal)                │
│  - CreatedBy, CreatedDate                                       │
└─────────────────────────────────────────────────────────────────┘

REFERENCE TABLES (Owned by Unit 4 - Read-Only):
┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│  ReferenceData   │  │   AccountType    │  │ EconomicActivity │
│  - GroupNumber   │  │   - Code         │  │   - Code         │
│  - Code          │  │   - Description  │  │   - Description  │
│  - Description   │  │   - GLMappings   │  │   - Group        │
└──────────────────┘  └──────────────────┘  └──────────────────┘
```

### 12.5 Integration Flow Diagram

```
CREATE ACCOUNT FLOW:

User Input
    ↓
[Presentation Layer]
    ↓
AccountManagementService.CreateAccount()
    ↓
┌─────────────────────────────────────────────────────────────┐
│ 1. Local Validation (mandatory fields, lengths)             │
│    ↓                                                         │
│ 2. IAccessControlService.CheckUserPermission()              │
│    ↓                                                         │
│ 3. ICustomerQueryService.ValidateCustomerExists()           │
│    ↓                                                         │
│ 4. IReferenceDataService.ValidateReferenceCode()            │
│    ↓                                                         │
│ 5. IValidationService.ValidateCrossFieldRules()             │
│    ↓                                                         │
│ 6. AccountRepository.ExistsByReferenceNumber()              │
│    (Duplicate check - warning only)                         │
│    ↓                                                         │
│ 7. AccountRepository.Create()                               │
│    ↓                                                         │
│ 8. IAuditService.AuditLog()                                 │
│    ↓                                                         │
│ 9. Return ServiceResponse<AccountId>                        │
└─────────────────────────────────────────────────────────────┘
    ↓
[Presentation Layer]
    ↓
Display Success or Errors to User


UPDATE ACCOUNT TYPE FLOW:

User Input (Account Type, Purpose)
    ↓
[Presentation Layer]
    ↓
LoanInformationService.UpdateAccountType()
    ↓
┌─────────────────────────────────────────────────────────────┐
│ 1. IAccessControlService.CheckUserPermission()              │
│    ↓                                                         │
│ 2. AccountRepository.GetById()                              │
│    ↓                                                         │
│ 3. IAccountTypeService.GetAccountType()                     │
│    ↓                                                         │
│ 4. IValidationService.ValidateConditionalFields()           │
│    (Purpose mandatory for specific account types)           │
│    ↓                                                         │
│ 5. Auto-populate Type of Credit and Purpose of Credit       │
│    (Cannot be overridden)                                   │
│    ↓                                                         │
│ 6. IAccountTypeService.GetGLMappings()                      │
│    (Auto-populate GL accounts)                              │
│    ↓                                                         │
│ 7. AccountRepository.Update()                               │
│    ↓                                                         │
│ 8. IAuditService.AuditLog()                                 │
│    ↓                                                         │
│ 9. Return ServiceResponse<bool>                             │
└─────────────────────────────────────────────────────────────┘
    ↓
[Presentation Layer]
    ↓
Display Success or Errors to User
```

---

## 13. Implementation Guidelines

This section provides guidance for development teams implementing this architecture.



### 13.1 For Teams Transitioning from Monolithic Development

**Key Differences from Traditional Approach**:

1. **Separation of Concerns**:
   - Traditional: Business logic mixed with UI and data access
   - Feature-Based: Clear layers (Presentation → Services → Repositories)
   - Benefit: Easier to test, maintain, and modify

2. **Service-Oriented Communication**:
   - Traditional: Direct database calls from UI
   - Feature-Based: UI calls services, services call repositories
   - Benefit: Business logic centralized, reusable across features

3. **Dependency Injection**:
   - Traditional: Direct instantiation of dependencies
   - Feature-Based: Dependencies injected via interfaces
   - Benefit: Loose coupling, easier to test with mocks

4. **Feature Organization**:
   - Traditional: Organized by technical layer (all UIs together, all DAL together)
   - Feature-Based: Organized by business capability (all account management together)
   - Benefit: Easier to understand, modify, and deploy features independently

**Migration Strategy**:
- Start with one feature (e.g., General Account Management)
- Implement full stack for that feature
- Learn patterns and practices
- Apply to remaining features
- Gradually refactor existing code to new architecture

### 13.2 Coding Standards and Conventions

**Naming Conventions**:

**Interfaces**:
- Prefix with "I": IAccountManagementService, IAccountRepository
- Use descriptive names: IAccountQueryService (not IAccountService2)

**Classes**:
- Suffix with type: AccountManagementService, AccountRepository
- Use PascalCase: AccountManagementService (not accountManagementService)

**Methods**:
- Use verbs: CreateAccount, GetAccount, UpdateAccount, DeleteAccount
- Use PascalCase: CreateAccount (not createAccount)
- Be specific: GetAccountByReferenceNumber (not GetAccount)

**Properties**:
- Use nouns: ReferenceNumber, CustomerName, AccountType
- Use PascalCase: ReferenceNumber (not referenceNumber)

**Variables**:
- Use camelCase: accountId, userId, searchCriteria
- Be descriptive: accountRepository (not repo)

**Constants**:
- Use UPPER_CASE: MAX_SEARCH_RESULTS, DEFAULT_TIMEOUT

**File Organization**:

```
Unit1_Loan_Account_Management/
├── Presentation/
│   ├── GeneralAccountManagement/
│   │   ├── CreateAccount.aspx
│   │   ├── ViewAccount.aspx
│   │   ├── UpdateAccount.aspx
│   │   └── DeleteAccount.aspx
│   ├── LoanInformationManagement/
│   │   ├── ManageLoanInfo.aspx
│   │   └── ManageLoanDates.aspx
│   └── AccountOperations/
│       ├── SearchAccounts.aspx
│       ├── AccountSummary.aspx
│       └── CopyAccount.aspx
├── Services/
│   ├── Interfaces/
│   │   ├── IAccountManagementService.cs
│   │   ├── IAccountQueryService.cs
│   │   ├── IAccountLifecycleService.cs
│   │   └── ILoanInformationService.cs
│   └── Implementation/
│       ├── AccountManagementService.cs
│       ├── AccountQueryService.cs
│       ├── AccountLifecycleService.cs
│       └── LoanInformationService.cs
├── Repositories/
│   ├── Interfaces/
│   │   ├── IAccountRepository.cs
│   │   ├── IAccountAuditRepository.cs
│   │   └── IAccountRelationshipRepository.cs
│   └── Implementation/
│       ├── AccountRepository.cs
│       ├── AccountAuditRepository.cs
│       └── AccountRelationshipRepository.cs
├── Models/
│   ├── Entities/
│   │   ├── Account.cs
│   │   ├── AccountAudit.cs
│   │   └── AccountRelationship.cs
│   └── DTOs/
│       ├── AccountDTO.cs
│       ├── AccountSummaryDTO.cs
│       └── SearchCriteriaDTO.cs
├── Validators/
│   ├── AccountValidator.cs
│   ├── LoanDateValidator.cs
│   └── ConditionalFieldValidator.cs
└── Common/
    ├── ServiceResponse.cs
    ├── ServiceError.cs
    └── Constants.cs
```

### 13.3 Error Handling Patterns

**Service Layer Error Handling**:

```
Pattern:
try
{
    // Validate inputs
    // Call repositories or external services
    // Return success response
}
catch (ValidationException ex)
{
    // Return validation errors
    return ServiceResponse<T>.Failure(ex.Errors);
}
catch (NotFoundException ex)
{
    // Return not found error
    return ServiceResponse<T>.Failure("NOT_FOUND", ex.Message);
}
catch (UnauthorizedException ex)
{
    // Return unauthorized error
    return ServiceResponse<T>.Failure("UNAUTHORIZED", ex.Message);
}
catch (Exception ex)
{
    // Log unexpected errors
    logger.Error(ex, "Unexpected error in CreateAccount");
    // Return generic error
    return ServiceResponse<T>.Failure("SYSTEM_ERROR", "An unexpected error occurred");
}
```

**Repository Layer Error Handling**:

```
Pattern:
try
{
    // Execute database operation
    // Return result
}
catch (SqlException ex)
{
    // Log SQL error
    logger.Error(ex, "Database error in AccountRepository.Create");
    // Translate to domain exception
    if (ex.Number == 2627) // Unique constraint violation
        throw new DuplicateException("Account with this reference number already exists");
    else
        throw new DataAccessException("Database error occurred", ex);
}
```

**Presentation Layer Error Handling**:

```
Pattern:
var response = accountManagementService.CreateAccount(accountDTO, userId);

if (response.Status == ServiceStatus.Success)
{
    // Display success message
    ShowSuccessMessage($"Account created successfully. Account ID: {response.Data}");
    // Redirect or refresh
}
else
{
    // Display validation errors
    foreach (var error in response.Errors)
    {
        ShowErrorMessage($"{error.Field}: {error.Message}");
    }
}
```

### 13.4 Testing Strategy

**Unit Testing**:

**Test Services with Mocked Dependencies**:
```
Test: CreateAccount_WithValidData_ReturnsAccountId
- Mock IAccountRepository
- Mock IValidationService
- Mock IAuditService
- Call AccountManagementService.CreateAccount()
- Assert: Returns success with AccountId
- Verify: Repository.Create() called once
- Verify: AuditService.AuditLog() called once
```

**Test Validators**:
```
Test: ValidateLoanDates_StartOfTermNotEqualToOriginalReleaseDate_ReturnsError
- Create account data with StartOfTerm != OriginalReleaseDate
- Call LoanDateValidator.Validate()
- Assert: Returns validation error
```

**Integration Testing**:

**Test Repository with Real Database**:
```
Test: AccountRepository_Create_InsertsRecordAndReturnsId
- Use test database
- Create account entity
- Call AccountRepository.Create()
- Assert: Returns AccountId > 0
- Assert: Record exists in database
- Cleanup: Delete test record
```

**Test Service Integration**:
```
Test: AccountManagementService_CreateAccount_IntegratesWithComplianceUnit
- Use real services (not mocks)
- Create account data
- Call AccountManagementService.CreateAccount()
- Assert: Validation service called
- Assert: Audit service called
- Assert: Account created in database
```

**UI Testing**:

**Test Critical Workflows**:
```
Test: CreateAccount_CompleteWorkflow_CreatesAccountSuccessfully
- Navigate to Create Account page
- Fill in all mandatory fields
- Click Save button
- Assert: Success message displayed
- Assert: Account appears in search results
```

**Test Coverage Targets**:
- Services: 90% code coverage
- Repositories: 80% code coverage
- Validators: 95% code coverage
- Overall: 80% code coverage

### 13.5 Typical Workflow Examples

**Example 1: Implementing a New Service Method**

**Requirement**: Add method to get accounts by economic activity

**Steps**:
1. Add method signature to IAccountQueryService interface
2. Implement method in AccountQueryService class
3. Add repository method to IAccountRepository interface
4. Implement repository method in AccountRepository class
5. Write unit tests for service method
6. Write integration test for repository method
7. Update API documentation

**Example 2: Adding a New Validation Rule**

**Requirement**: Validate that maturity date is not more than 30 years from start of term

**Steps**:
1. Create LoanDateValidator class (if not exists)
2. Add ValidateMaturityDateRange method
3. Call validator from LoanInformationService.UpdateLoanDates()
4. Write unit tests for validator
5. Update validation error codes documentation
6. Update UI to display new validation error

**Example 3: Integrating with a New External Service**

**Requirement**: Integrate with new Credit Bureau service for CRIB validation

**Steps**:
1. Define ICreditBureauService interface
2. Create CreditBureauServiceProxy implementation
3. Register service in dependency injection container
4. Inject service into AccountManagementService
5. Call service in CreateAccount method
6. Handle service failures gracefully
7. Write integration tests with mock service
8. Update configuration for service endpoint

### 13.6 Common Pitfalls to Avoid

**1. Mixing Business Logic in Presentation Layer**:
- ❌ Don't: Validate business rules in code-behind
- ✅ Do: Call service methods that encapsulate business logic

**2. Direct Database Access from Services**:
- ❌ Don't: Execute SQL queries directly in services
- ✅ Do: Call repository methods for all data access

**3. Ignoring Error Handling**:
- ❌ Don't: Let exceptions bubble up unhandled
- ✅ Do: Catch, log, and translate exceptions appropriately

**4. Not Using Transactions**:
- ❌ Don't: Execute multiple database operations without transaction
- ✅ Do: Use TransactionScope for operations that must succeed or fail together

**5. Hardcoding Configuration**:
- ❌ Don't: Hardcode connection strings or service endpoints
- ✅ Do: Store configuration in web.config or database

**6. Not Logging Errors**:
- ❌ Don't: Swallow exceptions without logging
- ✅ Do: Log all errors with sufficient context for troubleshooting

**7. Returning Entities to Presentation Layer**:
- ❌ Don't: Return entity objects directly to UI
- ✅ Do: Use DTOs to decouple presentation from data model

**8. Not Testing**:
- ❌ Don't: Skip unit and integration tests
- ✅ Do: Write tests for all business logic and critical workflows

### 13.7 Performance Optimization Tips

**1. Use Pagination for Large Result Sets**:
- Always limit search results to reasonable page size (e.g., 50 records)
- Implement skip/take pattern in repository queries

**2. Cache Reference Data**:
- Cache dropdown values locally for 1 hour
- Refresh cache on demand or when data changes

**3. Use Async/Await for I/O Operations**:
- Use async methods for database calls (if ASP.NET 4.7 supports)
- Improves scalability by freeing up threads

**4. Optimize Database Queries**:
- Use indexes on frequently queried columns
- Avoid SELECT * - select only needed columns
- Use stored procedures for complex queries

**5. Minimize Service Calls**:
- Batch validation calls when possible
- Cache validation results for session lifetime

**6. Use Connection Pooling**:
- Enable connection pooling (default in ADO.NET)
- Don't hold connections open longer than necessary

---

## 14. Summary

This architecture design provides a comprehensive blueprint for implementing Unit 1: Loan Account Management using Feature-Based Architecture with Service-Oriented patterns.

**Key Highlights**:

1. **Three Feature Components**: General Account Management, Loan Information Management, Account Operations
2. **Four Business Services**: Account Management, Account Query, Account Lifecycle, Loan Information
3. **Three Data Entities**: Account, AccountAudit, AccountRelationship
4. **Repository Pattern**: Clean separation between business logic and data access
5. **Service Interfaces**: Well-defined contracts for integration with other units
6. **Hybrid Validation**: Local validation for performance, complex rules via Compliance Unit
7. **Role-Based Access Control**: User, Authorizer, Administrator roles with center/branch restrictions
8. **Comprehensive Audit Trail**: All operations logged for compliance

**Design Decisions**:

1. **Data Model**: Hybrid approach - normalized key entities with practical flat structures
2. **Validation**: Hybrid - basic validations local, complex rules via Compliance Unit
3. **Service Communication**: Dependency injection with interfaces for loose coupling
4. **Legacy Integration**: Migrate DBF data to SQL Server tables

**Next Steps**:

1. Review and approve this architecture design
2. Proceed to Step 2.2: Create Logical Design (detailed class diagrams, sequence diagrams)
3. Proceed to Step 2.3: Implement Source Code
4. Proceed to Step 2.4: Debugging and Testing
5. Proceed to Step 2.5: Create Tests

---

**Document Status**: Complete and Ready for Review
**Date**: December 5, 2025
**Version**: 1.0

