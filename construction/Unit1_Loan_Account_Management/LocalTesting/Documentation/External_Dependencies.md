# External Dependencies - Unit 1: Loan Account Management

## Document Information
- **Purpose**: Document all external service dependencies for Unit 1
- **Date**: December 6, 2025
- **Status**: Complete

---

## Overview

Unit 1 (Loan Account Management) depends on services from three other units. This document maps all external dependencies and their usage within Unit 1.

---

## Dependency Map

```
Unit 1: Loan Account Management
├── Unit 2: Customer Management
│   └── ICustomerQueryService
├── Unit 4: Reference Data Management
│   ├── IReferenceDataService
│   ├── IAccountTypeService
│   ├── IEconomicActivityService
│   └── ICenterService
└── Unit 5: Compliance & Validation
    ├── IValidationService
    ├── IAuditService
    └── IAccessControlService
```

---

## Unit 2: Customer Management

### ICustomerQueryService

**Purpose**: Validate customer existence and retrieve customer information

**Methods Used**:
```csharp
ServiceResponse<CustomerDetails> GetCustomerInfo(int customerId)
ServiceResponse<bool> ValidateCustomerExists(int customerId)
```

**Usage in Unit 1**:
- **Account Creation**: Validate customer exists before creating account
- **Account Summary**: Retrieve customer details for display
- **Account Linking**: Link accounts to customers

**Mock Requirements**:
- Return sample customer data for valid customer IDs
- Return "not found" for invalid customer IDs
- Support at least 10 sample customers

---

## Unit 4: Reference Data Management

### IReferenceDataService

**Purpose**: Provide dropdown values and validate reference codes

**Methods Used**:
```csharp
ServiceResponse<List<ReferenceDataItem>> GetReferenceData(string groupNumber)
ServiceResponse<ReferenceDataItem> GetReferenceDataByCode(string groupNumber, string code)
ServiceResponse<bool> ValidateReferenceCode(string code, string type)
```

**Reference Data Groups Needed**:
- **Corporation** (RTL, WBG, FCDU, etc.)
- **BookCode** (11, 20, etc.)
- **FundSource** (BSP, LBP, DBP, WB, ACPC, etc.)
- **LendingProgram** (DBP, ALF, CLF, etc.)
- **Area** (PA, NPA)
- **MaturityCode** (A, B, C, D, E)
- **GuaranteedBy** (SBGFC, GFSME, PHILGUARANTEE, etc.)
- **Currency** (PHP, USD, EUR, JPY, etc.)

**Mock Requirements**:
- Return hardcoded lists for each group
- Validate codes against hardcoded lists
- Support at least 5-10 values per group

### IAccountTypeService

**Purpose**: Provide account type information and GL mappings

**Methods Used**:
```csharp
ServiceResponse<List<AccountType>> GetAccountTypes()
ServiceResponse<AccountType> GetAccountType(string code)
ServiceResponse<GLAccounts> GetGLMappings(string accountTypeCode, string economicActivityCode)
```

**Account Types Needed**:
- **IND** - Industrial
- **AA** - Agri-Agra (requires Purpose)
- **AI** - Agri-Industrial (requires Purpose)
- **R** - Real Estate (requires Purpose)
- **RDC** - Real Estate Commercial (requires Purpose)
- **RDE** - Real Estate Residential (requires Purpose)
- **RDH** - Real Estate Housing (requires Purpose)

**Mock Requirements**:
- Return list of account types with descriptions
- Return GL account mappings (can be dummy values for testing)
- Indicate which account types require Purpose field

### IEconomicActivityService

**Purpose**: Provide economic activity codes and descriptions

**Methods Used**:
```csharp
ServiceResponse<List<EconomicActivity>> GetEconomicActivities()
ServiceResponse<EconomicActivity> GetEconomicActivity(string code)
```

**Economic Activities Needed** (sample):
- **IND001** - Manufacturing
- **AGR001** - Agriculture
- **REL001** - Real Estate
- **COM001** - Commerce/Trading
- **SER001** - Services
- **EXP001** - Export
- **MAN001** - Manufacturing (Large Scale)
- **DEV001** - Development Projects

**Mock Requirements**:
- Return list of economic activities
- Support lookup by code
- Include description for each code

### ICenterService

**Purpose**: Validate center codes and retrieve center information

**Methods Used**:
```csharp
ServiceResponse<List<Center>> GetCenters()
ServiceResponse<Center> GetCenter(string code)
ServiceResponse<bool> ValidateCenter(string code)
```

**Centers Needed** (sample):
- **01** - Head Office
- **02** - Branch A
- **03** - Branch B

**Mock Requirements**:
- Return list of centers
- Validate center codes
- Include center name and location

---

## Unit 5: Compliance & Validation

### IValidationService

**Purpose**: Perform complex business rule validation

**Methods Used**:
```csharp
ServiceResponse<ValidationResult> ValidateMandatoryFields(object entityData)
ServiceResponse<ValidationResult> ValidateFieldFormats(object entityData)
ServiceResponse<ValidationResult> ValidateCrossFieldRules(object entityData)
ServiceResponse<ValidationResult> ValidateConditionalFields(object entityData)
```

**Validation Rules**:
- **Mandatory Fields**: Reference Number, Customer Name, Long Name, etc.
- **Field Formats**: Reference number format, date formats, numeric ranges
- **Cross-Field Rules**: Start of Term = Original Release Date, Maturity Date > Start of Term
- **Conditional Fields**: Purpose required for certain account types

**Mock Requirements**:
- Return success for valid data
- Return specific error messages for invalid data
- Support configurable strict/lenient modes

### IAuditService

**Purpose**: Log all operations for audit trail

**Methods Used**:
```csharp
ServiceResponse<bool> AuditLog(string action, string userId, int resourceId, Dictionary<string, string> changes)
```

**Actions to Log**:
- Create, Update, Delete, Close, Archive, Reopen

**Mock Requirements**:
- Log to console/debug output
- Store in AccountAudit table (via repository)
- Return success always

### IAccessControlService

**Purpose**: Check user permissions and apply center/branch restrictions

**Methods Used**:
```csharp
ServiceResponse<bool> CheckUserPermission(string userId, string action, int resourceId)
ServiceResponse<string> GetUserRole(string userId)
ServiceResponse<FilteredQuery> RestrictByCenter(string userId, object query)
```

**Roles**:
- **User**: Can create and view accounts
- **Authorizer**: Can update accounts
- **Administrator**: Can delete and archive accounts

**Mock Requirements**:
- Return configurable permissions based on user ID
- Support role-based access control
- Apply center restrictions (optional for testing)

---

## Mock Service Configuration

### Configuration File Structure

```json
{
  "mockMode": "lenient",
  "users": [
    {
      "userId": "user1",
      "role": "User",
      "centers": ["01"]
    },
    {
      "userId": "auth1",
      "role": "Authorizer",
      "centers": ["01", "02"]
    },
    {
      "userId": "admin1",
      "role": "Administrator",
      "centers": ["01", "02", "03"]
    }
  ],
  "validation": {
    "strictMode": false,
    "allowDuplicateRefNo": true
  }
}
```

---

## Testing Scenarios

### Scenario 1: Happy Path
- All services return success
- Valid data passes all validations
- User has all required permissions

### Scenario 2: Validation Failures
- Invalid reference number format
- Missing mandatory fields
- Date relationship violations
- Conditional field violations

### Scenario 3: Permission Denied
- User lacks required role
- User restricted to different center
- Resource not accessible

### Scenario 4: External Service Failures
- Customer not found
- Invalid reference data code
- Center code not found

---

## Implementation Priority

1. **High Priority** (Required for basic testing):
   - IReferenceDataService (dropdowns)
   - IValidationService (basic validation)
   - IAccessControlService (permissions)

2. **Medium Priority** (Required for full testing):
   - IAccountTypeService (GL mappings)
   - IEconomicActivityService (economic codes)
   - ICenterService (center validation)

3. **Low Priority** (Nice to have):
   - ICustomerQueryService (customer lookup)
   - IAuditService (audit logging)

---

## Summary

Unit 1 has **8 external service dependencies** across 3 units. All dependencies will be mocked for local testing with configurable responses to support various test scenarios.

