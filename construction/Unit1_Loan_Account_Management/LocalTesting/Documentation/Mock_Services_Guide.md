# Mock Services Guide - Unit 1: Loan Account Management

## Document Information
- **Purpose**: Guide to using mock services in local testing environment
- **Date**: December 6, 2025
- **Status**: Complete

---

## Overview

This guide explains how to use and configure the mock services for local testing of Unit 1 APIs. All external dependencies are mocked to allow isolated testing without requiring other units to be implemented.

---

## Available Mock Services

### 1. ReferenceDataServiceMock

**Implements**: IReferenceDataService, IAccountTypeService, IEconomicActivityService, ICenterService

**Purpose**: Provides hardcoded dropdown values and reference data

**Hardcoded Data**:
- **Corporations**: RTL, WBG, FCDU
- **Book Codes**: 11, 20
- **Fund Sources**: BSP, LBP, DBP, WB, ACPC
- **Lending Programs**: DBP, ALF, CLF
- **Areas**: PA, NPA
- **Maturity Codes**: A, B, C, D, E
- **Guaranteed By**: SBGFC, GFSME, PHILGUARANTEE
- **Currencies**: PHP, USD, EUR, JPY
- **Account Types**: IND, AA, AI, R, RDC, RDE, RDH
- **Economic Activities**: IND001, AGR001, REL001, COM001, SER001, EXP001, MAN001, DEV001
- **Centers**: 01 (Head Office), 02 (Branch A), 03 (Branch B)

**Usage Example**:
```csharp
var mockService = new ReferenceDataServiceMock();

// Get all corporations
var corporations = mockService.GetReferenceData("CORPORATION");

// Validate a code
var isValid = mockService.ValidateReferenceCode("RTL", "CORPORATION");

// Get account types
var accountTypes = mockService.GetAccountTypes();

// Get GL mappings
var glAccounts = mockService.GetGLMappings("IND", "IND001");
```

**Test Scenarios**:
- ✅ Valid codes return success
- ❌ Invalid codes return "not found" error
- ✅ All dropdown lists populated
- ✅ GL mappings return dummy accounts

---

### 2. ValidationServiceMock

**Implements**: IValidationService

**Purpose**: Validates account data according to business rules

**Validation Rules**:
1. **Mandatory Fields**: ReferenceNumber, PreviousReferenceNumber, CustomerName, LongName
2. **Field Lengths**: ReferenceNumber (17), CustomerName (40), LongName (100), CRIBID (10)
3. **Date Relationships**: StartOfTerm = OriginalReleaseDate, MaturityDate > StartOfTerm
4. **Conditional Fields**: Purpose required for AA, AI, R, RDC, RDE, RDH account types
5. **Guarantee Rules**: GuaranteedBy required when IsGuaranteed = true
6. **Litigation Rules**: LitigationDate required when IsUnderLitigation = true

**Configuration**:
```csharp
// Lenient mode (default)
var mockService = new ValidationServiceMock(strictMode: false);

// Strict mode
var mockService = new ValidationServiceMock(strictMode: true);
```

**Usage Example**:
```csharp
var mockService = new ValidationServiceMock();

// Validate mandatory fields
var result = mockService.ValidateMandatoryFields(accountDTO);

// Validate all rules at once
var allResults = mockService.ValidateAll(accountDTO);

if (!allResults.Data.IsValid)
{
    foreach (var error in allResults.Data.Errors)
    {
        Console.WriteLine($"{error.Field}: {error.Message}");
    }
}
```

**Test Scenarios**:
- ✅ Valid account passes all validations
- ❌ Missing mandatory fields trigger errors
- ❌ Invalid date relationships trigger errors
- ❌ Missing conditional fields trigger errors

---

### 3. AccessControlServiceMock

**Implements**: IAccessControlService

**Purpose**: Manages user permissions and center restrictions

**Predefined Users**:
| User ID | Username | Role | Centers | Permissions |
|---------|----------|------|---------|-------------|
| user1 | John User | User | 01 | Create, View |
| auth1 | Jane Authorizer | Authorizer | 01, 02 | Create, View, Update |
| admin1 | Admin User | Administrator | 01, 02, 03 | All permissions |
| restricted1 | Restricted User | User | (none) | Limited access |
| SYSTEM | System | Administrator | 01, 02, 03 | All permissions |

**Permission Matrix**:
| Action | User | Authorizer | Administrator |
|--------|------|------------|---------------|
| CREATE | ✅ | ✅ | ✅ |
| VIEW | ✅ | ✅ | ✅ |
| UPDATE | ❌ | ✅ | ✅ |
| DELETE | ❌ | ❌ | ✅ |
| CLOSE | ❌ | ❌ | ✅ |
| ARCHIVE | ❌ | ❌ | ✅ |
| REOPEN | ❌ | ❌ | ✅ |

**Usage Example**:
```csharp
var mockService = new AccessControlServiceMock();

// Check permission
var canUpdate = mockService.CheckUserPermission("user1", "UPDATE", 123);

// Get user role
var role = mockService.GetUserRole("admin1");

// Check center access
var hasAccess = mockService.CheckCenterAccess("user1", "01");

// Add test user dynamically
mockService.AddTestUser("test1", "Test User", "User", new List<string> { "01" });
```

**Test Scenarios**:
- ✅ User can create and view accounts
- ❌ User cannot update or delete accounts
- ✅ Authorizer can update accounts
- ✅ Administrator has all permissions
- ❌ Restricted user has no center access

---

### 4. CustomerServiceMock

**Implements**: ICustomerQueryService

**Purpose**: Provides sample customer data

**Predefined Customers** (10 sample customers):
| ID | Name | TIN | Type |
|----|------|-----|------|
| 1 | Juan Dela Cruz | 123-456-789-000 | Individual |
| 2 | Maria Santos | 234-567-890-000 | Individual |
| 3 | Pedro Reyes | 345-678-901-000 | Individual |
| 4 | Ana Garcia | 456-789-012-000 | Individual |
| 5 | Global Exports Inc | 567-890-123-000 | Corporation |
| 6 | Roberto Cruz | 678-901-234-000 | Individual |
| 7 | Draft Customer | 789-012-345-000 | Individual |
| 8 | Litigation Case Corp | 890-123-456-000 | Corporation |
| 9 | Development Foundation | 901-234-567-000 | Foundation |
| 10 | Mega Corporation | 012-345-678-000 | Corporation |

**Usage Example**:
```csharp
var mockService = new CustomerServiceMock();

// Get customer info
var customer = mockService.GetCustomerInfo(1);

// Validate customer exists
var exists = mockService.ValidateCustomerExists(5);

// Search customers
var results = mockService.SearchCustomers("Juan");

// Add test customer dynamically
mockService.AddTestCustomer(new CustomerDetails
{
    CustomerId = 11,
    CustomerName = "Test Customer",
    TIN = "111-222-333-000"
});
```

**Test Scenarios**:
- ✅ Valid customer IDs return customer details
- ❌ Invalid customer IDs return "not found" error
- ✅ Search by name or TIN works
- ✅ Can add test customers dynamically

---

## Mock Configuration

### Configuration File: MockConfig.json

Located at: `/LocalTesting/Mocks/MockConfig.json`

**Structure**:
```json
{
  "mockMode": "lenient",
  "validation": {
    "strictMode": false,
    "allowDuplicateRefNo": true,
    "enforceAllBusinessRules": false
  },
  "users": [
    {
      "userId": "user1",
      "username": "John User",
      "role": "User",
      "centers": ["01"]
    }
  ],
  "testScenarios": {
    "happyPath": { "enabled": true },
    "validationFailures": { "enabled": false },
    "permissionDenied": { "enabled": false },
    "externalServiceFailures": { "enabled": false }
  },
  "logging": {
    "logToConsole": true,
    "logToFile": false,
    "logLevel": "Info"
  }
}
```

### Using MockConfigManager

```csharp
// Get configuration instance
var config = MockConfigManager.Instance.Config;

// Check mock mode
if (config.MockMode == "lenient")
{
    // Use lenient validation
}

// Get validation settings
bool strictMode = config.Validation.StrictMode;

// Get user configuration
var users = config.Users;

// Reload configuration from file
MockConfigManager.Instance.ReloadConfiguration();

// Save configuration changes
MockConfigManager.Instance.SaveConfiguration();
```

---

## Test Scenarios

### Scenario 1: Happy Path (Default)

**Configuration**:
```json
{
  "mockMode": "lenient",
  "testScenarios": {
    "happyPath": { "enabled": true }
  }
}
```

**Behavior**:
- All validations pass for valid data
- All permissions granted for appropriate roles
- All reference data lookups succeed
- All customers found

**Use Case**: Basic functional testing

---

### Scenario 2: Validation Failures

**Configuration**:
```json
{
  "mockMode": "strict",
  "validation": {
    "strictMode": true,
    "allowDuplicateRefNo": false,
    "enforceAllBusinessRules": true
  },
  "testScenarios": {
    "validationFailures": { "enabled": true }
  }
}
```

**Behavior**:
- Strict validation enforced
- Duplicate reference numbers rejected
- All business rules enforced
- Field format validation strict

**Use Case**: Testing error handling and validation messages

---

### Scenario 3: Permission Denied

**Configuration**:
```json
{
  "testScenarios": {
    "permissionDenied": { "enabled": true }
  }
}
```

**Behavior**:
- Use restricted user (restricted1)
- Permissions denied for most operations
- Center access restricted

**Use Case**: Testing access control and authorization

---

### Scenario 4: External Service Failures

**Configuration**:
```json
{
  "testScenarios": {
    "externalServiceFailures": { "enabled": true }
  }
}
```

**Behavior**:
- Reference data lookups fail
- Customer not found errors
- Validation service unavailable

**Use Case**: Testing error handling for external dependencies

---

## Switching Between Scenarios

### Method 1: Edit MockConfig.json

1. Open `/LocalTesting/Mocks/MockConfig.json`
2. Change `mockMode` or enable/disable scenarios
3. Save file
4. Restart API server (configuration reloads automatically)

### Method 2: Programmatically

```csharp
var config = MockConfigManager.Instance.Config;
config.MockMode = "strict";
config.TestScenarios.ValidationFailures.Enabled = true;
MockConfigManager.Instance.SaveConfiguration();
```

---

## Adding Custom Mock Data

### Add Custom Reference Data

```csharp
var mockService = new ReferenceDataServiceMock();
// Reference data is hardcoded, modify ReferenceDataServiceMock.cs to add more
```

### Add Custom User

```csharp
var mockService = new AccessControlServiceMock();
mockService.AddTestUser("custom1", "Custom User", "User", new List<string> { "01", "02" });
```

### Add Custom Customer

```csharp
var mockService = new CustomerServiceMock();
mockService.AddTestCustomer(new CustomerDetails
{
    CustomerId = 100,
    CustomerName = "Custom Customer",
    TIN = "999-999-999-000",
    CustomerType = "Individual"
});
```

---

## Troubleshooting

### Issue: Mock services not returning data

**Solution**: Check that mock services are properly initialized in dependency injection container

### Issue: Configuration changes not taking effect

**Solution**: Restart API server or call `MockConfigManager.Instance.ReloadConfiguration()`

### Issue: Validation always passes/fails

**Solution**: Check `strictMode` setting in MockConfig.json

### Issue: Permission denied for all users

**Solution**: Verify user exists in MockConfig.json and has appropriate role

---

## Summary

The mock services provide a complete testing environment for Unit 1 without requiring external dependencies. All services are configurable through MockConfig.json and support multiple test scenarios.

**Key Benefits**:
- ✅ Isolated testing without external dependencies
- ✅ Configurable test scenarios
- ✅ Realistic mock data
- ✅ Easy to extend with custom data
- ✅ Supports all validation and permission rules

