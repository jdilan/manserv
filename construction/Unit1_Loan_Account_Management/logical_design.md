# Logical Design - Unit 1: Loan Account Management

## Document Information
- **Project**: MANSERV Loan Account Management System
- **Unit**: Unit 1 - Loan Account Management
- **Version**: 1.0
- **Date**: December 5, 2025
- **Status**: Design Complete
- **Technology Stack**: ASP.NET 4.7 Web Forms, Entity Framework 6.x, SQL Server 2022

---

## Table of Contents
1. [Overview](#overview)
2. [Namespace and Project Structure](#namespace-and-project-structure)
3. [Data Layer Design](#data-layer-design)
4. [Business Services Layer Design](#business-services-layer-design)
5. [Validation Layer Design](#validation-layer-design)
6. [External Service Integration Design](#external-service-integration-design)
7. [Presentation Layer Design](#presentation-layer-design)
8. [Security and Access Control Design](#security-and-access-control-design)
9. [Configuration and Dependency Injection](#configuration-and-dependency-injection)
10. [Error Handling and Logging Design](#error-handling-and-logging-design)
11. [Sequence Diagrams](#sequence-diagrams)
12. [Class Diagrams](#class-diagrams)
13. [Implementation Guidelines](#implementation-guidelines)

---

## 1. Overview

### 1.1 Purpose
This logical design document provides detailed specifications for implementing Unit 1: Loan Account Management. It defines the complete class structure, interfaces, method signatures, and interaction patterns without providing actual code implementation.

### 1.2 Scope
- **Features Covered**: 23 user stories across 3 feature components
- **Architecture Pattern**: Feature-Based Architecture with Service-Oriented patterns
- **Technology**: ASP.NET 4.7 Web Forms, Entity Framework 6.x, SQL Server 2022
- **Integration**: Interfaces with Units 2, 4, and 5

### 1.3 Design Principles
- **Separation of Concerns**: Clear boundaries between layers
- **Dependency Inversion**: Depend on abstractions (interfaces)
- **Single Responsibility**: Each class has one well-defined purpose
- **Repository Pattern**: Abstract data access from business logic
- **Service-Oriented**: Business logic encapsulated in services

---

## 2. Namespace and Project Structure

### 2.1 Solution Structure

```
ManservLoanSystem.sln
├── ManservLoanSystem.Web (ASP.NET Web Forms Application)
│   ├── Features/
│   │   ├── GeneralAccountManagement/
│   │   ├── LoanInformationManagement/
│   │   └── AccountOperations/
│   ├── Models/
│   ├── Services/
│   ├── Repositories/
│   ├── Data/
│   ├── Validators/
│   ├── ExternalServices/
│   └── Common/
└── ManservLoanSystem.Tests (Optional - Unit Tests)
```

### 2.2 Namespace Hierarchy

**Root Namespace**: `ManservLoanSystem`

#### Core Namespaces:
- `ManservLoanSystem.Models.Entities` - Entity classes (Account, AccountAudit, AccountRelationship)
- `ManservLoanSystem.Models.DTOs` - Data Transfer Objects
- `ManservLoanSystem.Models.Common` - Common models (ServiceResponse, ServiceError)
- `ManservLoanSystem.Data` - DbContext and EF configuration
- `ManservLoanSystem.Repositories` - Repository interfaces and implementations
- `ManservLoanSystem.Services` - Business service interfaces and implementations
- `ManservLoanSystem.Validators` - Validation logic
- `ManservLoanSystem.ExternalServices` - External service interfaces and stubs
- `ManservLoanSystem.Features.GeneralAccountManagement` - UI for general account CRUD
- `ManservLoanSystem.Features.LoanInformationManagement` - UI for loan info management
- `ManservLoanSystem.Features.AccountOperations` - UI for search, copy, close operations

### 2.3 Assembly References

**Required NuGet Packages**:
- EntityFramework (6.4.4)
- Microsoft.AspNet.Web.Optimization.WebForms
- log4net or NLog (for logging)
- Unity.AspNet.WebForms or Autofac.WebForms (for DI - optional)

**Framework References**:
- System.Data.Entity
- System.Web
- System.Web.UI
- System.ComponentModel.DataAnnotations

---

## 3. Data Layer Design

### 3.1 Entity Classes

#### 3.1.1 Account Entity
**Namespace**: `ManservLoanSystem.Models.Entities`  
**Table**: Account  
**Purpose**: Represents a loan account

**Properties**:
```
Primary Keys:
- AccountId: int (Identity, Primary Key)
- ReferenceNumber: string(17) (Unique, Business Key)

General Information:
- PreviousReferenceNumber: string(17) (Required)
- CRIBIDNumber: string(10) (Optional)
- CustomerName: string(40) (Required, Indexed)
- NIDSSAccountNumber: string(13) (Optional)
- LongName: string(100) (Required)

Account Identification:
- CenterCode: string(2) (Required, Indexed)
- BudgetUnit: string(3) (Required)
- Corporation: string(10) (Required)
- BookCode: string(2) (Required)

Economic Classification:
- EconomicActivityCode: string(6) (Required)

Loan Dates:
- OriginalReleaseDate: DateTime (Required)
- StartOfTerm: DateTime (Required)
- MaturityDate: DateTime (Required)

Account Type and Purpose:
- AccountType: string(3) (Required, Indexed)
- Purpose: string(1) (Optional, Conditional)

Funding and Program:
- FundSource: string(3) (Required)
- LendingProgram: string(3) (Required)
- Area: string(3) (Required)

Status and Classification:
- IsRestructured: bool (Required, Default: false)
- TypeOfCredit: string(6) (Required, Auto-populated)
- MaturityCode: string(1) (Required)
- PurposeOfCredit: string(1) (Required, Auto-populated)
- NumberOfRecords: int? (Optional)

Guarantee Information:
- IsGuaranteed: bool (Required, Default: false)
- GuaranteedBy: string(10) (Optional)

Litigation:
- IsUnderLitigation: bool (Required, Default: false)
- LitigationDate: DateTime? (Optional)

Loan Project and Currency:
- LoanStatus: string(3) (Required)
- LoanProjectType: string(1) (Required)
- Currency: string(3) (Required)

Account Status:
- Status: string(20) (Required, Indexed, Default: "Active")
- IsDraft: bool (Required, Default: false)
- ClosureDate: DateTime? (Optional)

Audit Fields:
- CreatedBy: string(50) (Required)
- CreatedDate: DateTime (Required)
- ModifiedBy: string(50) (Optional)
- ModifiedDate: DateTime? (Optional)
- DeletedBy: string(50) (Optional)
- DeletedDate: DateTime? (Optional)
```

**Data Annotations**:
- [Table("Account")]
- [Key] on AccountId
- [Required] on mandatory fields
- [StringLength(n)] on all string fields
- [Index] on ReferenceNumber (unique), CustomerName, CenterCode, Status, AccountType

**Navigation Properties**: None (for performance)

#### 3.1.2 AccountAudit Entity
**Properties**: AuditId (PK), AccountId (FK), ReferenceNumber, Action, FieldName, OldValue, NewValue, ChangedBy, ChangedDate, UserRole, IPAddress, Comments

#### 3.1.3 AccountRelationship Entity
**Properties**: RelationshipId (PK), SourceAccountId (FK), TargetAccountId (FK), RelationshipType, CreatedBy, CreatedDate

### 3.2 Data Transfer Objects (DTOs)

#### 3.2.1 AccountDTO
**Purpose**: Transfer account data between layers
**Properties**: All Account entity properties except audit fields

#### 3.2.2 ServiceResponse<T>
**Purpose**: Standardized response wrapper
**Properties**: Status (Success/Failure), Data (T), Errors (List<ServiceError>)
**Methods**: Success(T data), Failure(errorCode, message), GetErrorMessage()

#### 3.2.3 ServiceError
**Properties**: ErrorCode, Message, Field

### 3.3 Repository Interfaces

#### 3.3.1 IAccountRepository
**Methods**:
- int Create(Account account)
- Account GetById(int accountId)
- Account GetByReferenceNumber(string referenceNumber)
- int Update(Account account)
- bool Delete(int accountId, string userId)
- List<Account> GetAll(bool includeDeleted = false)
- List<Account> Search(string refNo, string customerName, string centerCode, string status, string accountType)
- bool ExistsByReferenceNumber(string referenceNumber, int? excludeAccountId = null)
- List<Account> GetByCenterCode(string centerCode)
- List<Account> GetByStatus(string status)
- void CreateAudit(AccountAudit audit)
- List<AccountAudit> GetAuditHistory(int accountId)
- void CreateRelationship(AccountRelationship relationship)
- List<AccountRelationship> GetRelationships(int accountId)

### 3.4 DbContext

#### ManservDbContext
**Inherits**: DbContext
**DbSets**: Accounts, AccountAudits, AccountRelationships
**Configuration**: LazyLoadingEnabled = false, ProxyCreationEnabled = false
**Connection String**: "ManservLoanDB" from Web.config

---

## 4. Business Services Layer Design

### 4.1 Service Interfaces

#### 4.1.1 IAccountManagementService
**Methods**:
- ServiceResponse<int> CreateAccount(AccountDTO accountDTO, string userId)
- ServiceResponse<AccountDTO> GetAccount(int accountId, string userId)
- ServiceResponse<bool> UpdateAccount(int accountId, AccountDTO accountDTO, string userId)
- ServiceResponse<bool> DeleteAccount(int accountId, string userId)
- ServiceResponse<bool> ValidateDuplicateRefNo(string refNo)

#### 4.1.2 IAccountQueryService
**Methods**:
- ServiceResponse<List<AccountDTO>> SearchAccounts(string refNo, string customerName, string centerCode, string status, string accountType, string userId)
- ServiceResponse<AccountDTO> GetAccountSummary(int accountId, string userId)
- ServiceResponse<List<AccountDTO>> GetAccountsByCenter(string centerCode, string userId)
- ServiceResponse<bool> ValidateAccountExists(int accountId)

#### 4.1.3 IAccountLifecycleService
**Methods**:
- ServiceResponse<int> CopyAccount(int sourceAccountId, string newRefNo, string userId)
- ServiceResponse<bool> CloseAccount(int accountId, string userId)
- ServiceResponse<bool> ArchiveAccount(int accountId, string userId)
- ServiceResponse<bool> ReopenAccount(int accountId, string userId)

### 4.2 Service Implementation Pattern

**Dependencies**: IAccountRepository, IValidationService, IAuditService, IAccessControlService
**Pattern**: Constructor injection of dependencies
**Error Handling**: Try-catch with ServiceResponse<T> return
**Validation**: Local validation + external validation service
**Audit**: Log all operations via IAuditService

---

## 5. Validation Layer Design

### 5.1 Validator Classes

#### AccountValidator
**Methods**:
- ValidationResult ValidateMandatoryFields(AccountDTO account)
- ValidationResult ValidateFieldLengths(AccountDTO account)
- ValidationResult ValidateDataTypes(AccountDTO account)

#### LoanDateValidator
**Methods**:
- ValidationResult ValidateDateRelationships(DateTime originalReleaseDate, DateTime startOfTerm, DateTime maturityDate)

#### ConditionalFieldValidator
**Methods**:
- ValidationResult ValidatePurposeField(string accountType, string purpose)
- ValidationResult ValidateGuaranteedBy(bool isGuaranteed, string guaranteedBy)

---

## 6. External Service Integration Design

### 6.1 External Service Interfaces

#### From Unit 4: Reference Data Management
- IReferenceDataService: GetReferenceData(groupNumber), ValidateReferenceCode(code, type)
- IAccountTypeService: GetAccountTypes(), GetGLMappings(accountTypeCode, economicActivityCode)

#### From Unit 5: Compliance & Validation
- IValidationService: ValidateCrossFieldRules(accountData), ValidateConditionalFields(accountData)
- IAuditService: AuditLog(action, userId, accountId, changes)
- IAccessControlService: CheckUserPermission(userId, action, resourceId), GetUserRole(userId)

### 6.2 Stub Implementations (for demo)
- ReferenceDataServiceStub: Returns hardcoded dropdown values
- ValidationServiceStub: Returns success for all validations
- AuditServiceStub: Logs to console/debug output
- AccessControlServiceStub: Returns true for all permission checks

---

## 7. Presentation Layer Design

### 7.1 Web Forms Pages

#### CreateAccount.aspx
**Controls**: TextBoxes for all fields, DropDownLists for reference data, RequiredFieldValidators, CustomValidators, Button (Save, Cancel)
**Code-Behind**: Inject IAccountManagementService, populate dropdowns, handle Save click, display validation errors

#### ViewAccount.aspx
**Controls**: Labels for all fields (read-only), Buttons (Edit, Delete, Back)
**Code-Behind**: Inject IAccountQueryService, load account data, handle button clicks

#### UpdateAccount.aspx
**Controls**: Same as CreateAccount but pre-populated
**Code-Behind**: Inject IAccountManagementService, load existing data, handle Save click

#### SearchAccounts.aspx
**Controls**: Search form (TextBoxes, DropDownLists), GridView for results, Pagination controls
**Code-Behind**: Inject IAccountQueryService, handle search, bind results to GridView

---

## 8. Security and Access Control Design

### 8.1 Authentication
**Method**: ASP.NET Forms Authentication
**Configuration**: Web.config <authentication mode="Forms">
**Login**: Custom login page validates credentials, creates FormsAuthenticationTicket

### 8.2 Authorization
**Roles**: User, Authorizer, Administrator
**Implementation**: [Authorize(Roles="User,Authorizer")] attributes on pages
**Center Restrictions**: IAccessControlService.RestrictByCenter(userId, query)

### 8.3 Audit Trail
**Capture Points**: All Create, Update, Delete operations
**Data**: Action, User, Timestamp, Old/New Values
**Storage**: AccountAudit table

---

## 9. Configuration and Dependency Injection

### 9.1 Web.config Structure
```xml
<connectionStrings>
  <add name="ManservLoanDB" connectionString="..." />
</connectionStrings>
<appSettings>
  <add key="ValidationMode" value="Strict" />
</appSettings>
```

### 9.2 Dependency Injection Setup
**Container**: ASP.NET Built-in DI or Unity
**Registration**: Global.asax Application_Start
**Lifetimes**: 
- DbContext: Scoped (per request)
- Repositories: Scoped
- Services: Scoped
- External service stubs: Singleton

---

## 10. Error Handling and Logging Design

### 10.1 Exception Hierarchy
- ValidationException
- NotFoundException
- UnauthorizedException
- DataAccessException

### 10.2 Logging Strategy
**Framework**: log4net or NLog
**Levels**: Debug, Info, Warning, Error
**Format**: [Timestamp] [Level] [Logger] Message
**Storage**: File (rolling), Database (optional)

---

## 11. Sequence Diagrams

### 11.1 Create Account Flow
```
User → CreateAccount.aspx → AccountManagementService → AccountValidator
  → ValidationService (external) → AccountRepository → Database
  → AuditService → Return Success/Errors
```

### 11.2 Search Accounts Flow
```
User → SearchAccounts.aspx → AccountQueryService → AccessControlService
  → AccountRepository → Database → Return Results
```

---

## 12. Class Diagrams

### 12.1 Entity Model
```
Account (1) ←→ (Many) AccountAudit
Account (1) ←→ (Many) AccountRelationship (as Source)
Account (1) ←→ (Many) AccountRelationship (as Target)
```

### 12.2 Service Layer Dependencies
```
AccountManagementService
  ↓ depends on
IAccountRepository, IValidationService, IAuditService, IAccessControlService
```

---

## 13. Implementation Guidelines

### 13.1 Coding Standards
- Use PascalCase for classes, methods, properties
- Use camelCase for parameters, local variables
- Prefix interfaces with "I"
- Add XML comments to all public members
- Use meaningful names (no abbreviations)

### 13.2 Best Practices
- Always use using statements for IDisposable
- Validate inputs at service layer
- Use parameterized queries (EF handles this)
- Log all errors
- Return ServiceResponse<T> from all service methods
- Use async/await for I/O operations (optional)

### 13.3 Testing Approach
- Unit test services with mocked repositories
- Integration test repositories with test database
- UI test critical workflows

---

## Document Status
**Status**: Complete  
**Date**: December 5, 2025  
**Version**: 1.0  
**Implementation Status**: 40% complete (database, entities, DTOs, repositories)

