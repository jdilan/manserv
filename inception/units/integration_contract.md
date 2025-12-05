# Integration Contract

## Document Information
- **Project**: Manserv Loan Account Management System
- **Version**: 1.0
- **Date**: December 5, 2025
- **Status**: Ready for Implementation

## Purpose

This document defines the integration contract between all business feature units in the Loan Account Management System. It specifies the service interfaces each unit exposes, the dependencies between units, and the communication patterns used for inter-unit integration.

## Integration Principles

1. **Loose Coupling**: Units communicate through well-defined service interfaces
2. **Service-Oriented**: Each unit exposes services that other units can consume
3. **Data Encapsulation**: Units own their data and expose it through services
4. **Synchronous Communication**: Primary communication pattern is synchronous API calls
5. **Error Handling**: All services return standardized error responses
6. **Authentication**: All service calls include user context for authorization
7. **Audit Trail**: All cross-unit operations are logged for audit purposes

## Unit Overview

### Priority 1 (Core/Required Units)
1. **Unit 1: Loan Account Management** - Core loan account CRUD operations
2. **Unit 2: Customer Management** - Customer information and classification
3. **Unit 3: Financial Management** - Balances, transactions, and interest calculations
4. **Unit 4: Reference Data Management** - System configuration and master data
5. **Unit 5: Compliance & Validation** - Business rules, validation, and audit

### Priority 2 (Optional Units - Future Phase)
6. **Unit 6: Reporting & Analytics** - Search, reports, and portfolio analysis (OPTIONAL)
7. **Unit 7: Collateral & GL Management** - Collateral tracking and GL mapping (OPTIONAL)

---

## Unit 1: Loan Account Management

### Exposed Services

#### Account Management Service
```
createAccount(accountData): AccountId
getAccount(accountId): AccountDetails
updateAccount(accountId, accountData): Success/Failure
deleteAccount(accountId): Success/Failure
searchAccounts(searchCriteria): AccountList
copyAccount(sourceAccountId): NewAccountId
closeAccount(accountId): Success/Failure
```

#### Account Query Service
```
getAccountSummary(accountId): AccountSummary
getAccountStatus(accountId): AccountStatus
getAccountsByCustomer(customerId): AccountList
getAccountsByCenter(centerCode): AccountList
validateAccountExists(accountId): Boolean
```

#### Account Lifecycle Service
```
archiveAccount(accountId): Success/Failure
reopenAccount(accountId): Success/Failure
getArchivedAccounts(searchCriteria): AccountList
```

### Consumed Services

**From Unit 2 (Customer Management)**:
- `getCustomerInfo(customerId)`: Get customer details
- `validateCustomerExists(customerId)`: Validate customer

**From Unit 4 (Reference Data Management)**:
- `getReferenceData(groupNumber)`: Get dropdown values
- `getAccountTypes()`: Get account types
- `getEconomicActivities()`: Get economic activities
- `getCenters()`: Get center codes
- `validateReferenceCode(code, type)`: Validate reference codes

**From Unit 5 (Compliance & Validation)**:
- `validateMandatoryFields(accountData)`: Validate required fields
- `validateCrossFieldRules(accountData)`: Validate business rules
- `validateFieldFormats(accountData)`: Validate field formats
- `auditLog(action, userId, accountId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

### Integration Points

- **Consumers**: Unit 3 (Financial Management), Unit 6 (Reporting), Unit 7 (Collateral & GL)
- **Dependencies**: Unit 2, Unit 4, Unit 5

---

## Unit 2: Customer Management

### Exposed Services

#### Customer Management Service
```
createCustomer(customerData): CustomerId
getCustomer(customerId): CustomerDetails
updateCustomer(customerId, customerData): Success/Failure
deleteCustomer(customerId): Success/Failure
searchCustomers(searchCriteria): CustomerList
mergeCustomers(sourceCustomerId, targetCustomerId): Success/Failure
```

#### Customer Query Service
```
getCustomerByBorrowerNumber(borrowerNumber): CustomerDetails
getCustomersByName(name): CustomerList
getCustomersByTIN(tin): CustomerList
getCustomersByCRIBID(cribId): CustomerList
validateCustomerExists(customerId): Boolean
```

#### Customer Classification Service
```
getCustomerRiskRating(customerId): RiskRating
updateRiskRating(customerId, rating, worksheetDate): Success/Failure
getCustomerClassification(customerId): Classification
updateClassification(customerId, classification): Success/Failure
getRiskRatingHistory(customerId): RatingHistory
```

#### Customer Financial Service
```
getFinancialStatements(customerId): FinancialData
updateFinancialStatements(customerId, financialData): Success/Failure
calculateFinancialRatios(customerId): FinancialRatios
```

### Consumed Services

**From Unit 4 (Reference Data Management)**:
- `getReferenceData(groupNumber)`: Get dropdown values
- `getGroupList()`: Get group names
- `validateReferenceCode(code, type)`: Validate reference codes

**From Unit 5 (Compliance & Validation)**:
- `validateMandatoryFields(customerData)`: Validate required fields
- `validateFieldFormats(customerData)`: Validate field formats
- `auditLog(action, userId, customerId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

### Integration Points

- **Consumers**: Unit 1 (Loan Account Management), Unit 3 (Financial Management), Unit 6 (Reporting)
- **Dependencies**: Unit 4, Unit 5

---

## Unit 3: Financial Management

### Exposed Services

#### Balance Management Service
```
getBalances(accountId): BalanceDetails
updateBalance(accountId, balanceType, amount, currency): Success/Failure
getBalanceHistory(accountId, dateRange): BalanceHistory
validateBalanceRules(accountId, balanceData): ValidationResult
```

#### Currency Conversion Service
```
convertCurrency(amount, fromCurrency, toCurrency, exchangeRate): ConvertedAmount
getExchangeRate(fromCurrency, toCurrency, date): ExchangeRate
updateExchangeRate(fromCurrency, toCurrency, rate, date): Success/Failure
```

#### Interest Calculation Service
```
calculateInterest(accountId, fromDate, toDate): InterestAmount
calculateAIR(accountId, asOfDate): AIRAmount
accrueInterest(accountId): Success/Failure
reverseAIR(accountId, amount): Success/Failure
```

#### Transaction Validation Service
```
validateTransaction(transactionData): ValidationResult
postTransaction(transactionData): TransactionId
reverseTransaction(transactionId): Success/Failure
```

#### Restructured Loan Service
```
restructureLoan(accountId, newTerms): NewAccountId
getRestructuredLoans(searchCriteria): LoanList
getRestructuringHistory(accountId): RestructuringHistory
```

### Consumed Services

**From Unit 1 (Loan Account Management)**:
- `getAccount(accountId)`: Get account details
- `updateAccount(accountId, accountData)`: Update account information
- `createAccount(accountData)`: Create new account (for restructuring)

**From Unit 4 (Reference Data Management)**:
- `getReferenceData(groupNumber)`: Get dropdown values
- `validateReferenceCode(code, type)`: Validate reference codes

**From Unit 5 (Compliance & Validation)**:
- `validateCrossFieldRules(accountData)`: Validate business rules
- `validateFieldFormats(accountData)`: Validate field formats
- `auditLog(action, userId, accountId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

### Integration Points

- **Consumers**: Unit 6 (Reporting), Unit 7 (Collateral & GL)
- **Dependencies**: Unit 1, Unit 4, Unit 5

---

## Unit 4: Reference Data Management

### Exposed Services

#### Reference Data Query Service
```
getReferenceData(groupNumber): ReferenceDataList
getReferenceDataByCode(groupNumber, code): ReferenceDataItem
searchReferenceData(groupNumber, searchTerm): ReferenceDataList
getAllGroups(): GroupList
```

#### Account Type Service
```
getAccountTypes(): AccountTypeList
getAccountType(code): AccountTypeDetails
getGLMappings(accountTypeCode, economicActivityCode): GLAccounts
searchAccountTypes(searchTerm): AccountTypeList
```

#### Economic Activity Service
```
getEconomicActivities(): EconomicActivityList
getEconomicActivity(code): EconomicActivityDetails
searchEconomicActivities(searchTerm): EconomicActivityList
getEconomicGroups(): EconomicGroupList
```

#### GL Account Service
```
getGLAccounts(): GLAccountList
getGLAccount(accountNumber): GLAccountDetails
searchGLAccounts(searchTerm): GLAccountList
validateGLAccount(accountNumber): Boolean
```

#### Center Service
```
getCenters(): CenterList
getCenter(code): CenterDetails
getBudgetUnits(centerCode): BudgetUnitList
validateCenter(code): Boolean
```

#### Group Service
```
getGroups(): GroupList
getGroup(groupId): GroupDetails
getGroupMembers(groupId): CustomerList
calculateGroupExposure(groupId): ExposureAmount
```

#### Validation Service
```
validateReferenceCode(code, type): ValidationResult
validateAccountType(code): Boolean
validateEconomicActivity(code): Boolean
validateGLAccount(accountNumber): Boolean
validateCenter(code): Boolean
validateGroup(groupId): Boolean
```

### Consumed Services

**From Unit 5 (Compliance & Validation)**:
- `auditLog(action, userId, resourceId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

### Integration Points

- **Consumers**: All units (Unit 1, 2, 3, 6, 7)
- **Dependencies**: Unit 5

---

## Unit 5: Compliance & Validation

### Exposed Services

#### Validation Service
```
validateMandatoryFields(entityData): ValidationResult
validateFieldFormats(entityData): ValidationResult
validateCrossFieldRules(entityData): ValidationResult
validateConditionalFields(entityData): ValidationResult
validateBusinessRules(entityData): ValidationResult
```

#### Auto-Population Service
```
autoPopulateFields(entityData): UpdatedEntityData
calculateDependentFields(entityData): CalculatedFields
updateDependentFields(entityId, sourceField, sourceValue): Success/Failure
```

#### Status Management Service
```
validateStatusTransition(currentStatus, newStatus): ValidationResult
updateAccountStatus(accountId, newStatus, reason): Success/Failure
checkStatusRules(accountId): StatusCheckResult
getStatusHistory(accountId): StatusHistory
```

#### Audit Service
```
auditLog(action, userId, resourceId, changes): Success/Failure
getAuditLog(resourceId, dateRange): AuditLogEntries
searchAuditLog(searchCriteria): AuditLogEntries
generateAuditReport(reportCriteria): AuditReport
```

#### Batch Processing Service
```
executeBatch(batchType): BatchResult
scheduleBatch(batchType, schedule): Success/Failure
getBatchStatus(batchId): BatchStatus
getBatchHistory(batchType, dateRange): BatchHistory
```

#### Access Control Service
```
authenticateUser(username, password): AuthToken
checkUserPermission(userId, action, resourceId): Boolean
getUserRole(userId): Role
getUserPermissions(userId): PermissionList
restrictByCenter(userId, query): FilteredQuery
```

### Consumed Services

None (foundational unit)

### Integration Points

- **Consumers**: All units (Unit 1, 2, 3, 4, 6, 7)
- **Dependencies**: None

---

## Unit 6: Reporting & Analytics (OPTIONAL - Future Phase)

### Exposed Services

#### Report Generation Service
```
generateReport(reportType, parameters): ReportData
exportReport(reportId, format): ExportedFile
scheduleReport(reportType, schedule): ScheduleId
getReportHistory(reportType, dateRange): ReportHistory
```

#### Analytics Service
```
getPortfolioSummary(filters): PortfolioSummary
getAccountAnalytics(accountId): AnalyticsData
getTrendAnalysis(metric, dateRange): TrendData
getConcentrationAnalysis(dimension): ConcentrationData
```

#### Data Export Service
```
exportData(entityType, filters, format): ExportedFile
importData(entityType, file): ImportResult
validateImportData(entityType, file): ValidationResult
```

### Consumed Services

**From Unit 1 (Loan Account Management)**:
- `searchAccounts(searchCriteria)`: Get account data
- `getAccountSummary(accountId)`: Get account summary

**From Unit 2 (Customer Management)**:
- `searchCustomers(searchCriteria)`: Get customer data
- `getCustomerClassification(customerId)`: Get classification

**From Unit 3 (Financial Management)**:
- `getBalances(accountId)`: Get balance data
- `getBalanceHistory(accountId, dateRange)`: Get balance history

**From Unit 4 (Reference Data Management)**:
- `getReferenceData(groupNumber)`: Get reference data for filtering
- `getAccountTypes()`: Get account types for grouping

**From Unit 5 (Compliance & Validation)**:
- `auditLog(action, userId, resourceId, changes)`: Log report generation
- `checkUserPermission(userId, action, resourceId)`: Check access rights

### Integration Points

- **Consumers**: End users (no other units consume this)
- **Dependencies**: All core units (Unit 1, 2, 3, 4, 5)

---

## Unit 7: Collateral & GL Management (OPTIONAL - Future Phase)

### Exposed Services

#### Collateral Management Service
```
addCollateral(accountId, collateralData): CollateralId
updateCollateral(collateralId, collateralData): Success/Failure
deleteCollateral(collateralId): Success/Failure
getCollateral(accountId): CollateralList
calculateCoverageRatio(accountId): CoverageRatio
```

#### GL Posting Service
```
postToGL(accountId, transactionData): GLEntryId
reverseGLEntry(glEntryId): Success/Failure
getGLEntries(accountId, dateRange): GLEntryList
reconcileGLAccounts(accountId): ReconciliationResult
```

#### Amortization Service
```
calculateAmortization(accountId): AmortizationSchedule
updateAmortizationArrears(accountId, arrears): Success/Failure
getAmortizationSchedule(accountId): Schedule
```

### Consumed Services

**From Unit 1 (Loan Account Management)**:
- `getAccount(accountId)`: Get account details
- `updateAccount(accountId, accountData)`: Update account information

**From Unit 3 (Financial Management)**:
- `getBalances(accountId)`: Get balance data for GL posting

**From Unit 4 (Reference Data Management)**:
- `getGLAccounts()`: Get GL account list
- `validateGLAccount(accountNumber)`: Validate GL accounts

**From Unit 5 (Compliance & Validation)**:
- `validateCrossFieldRules(accountData)`: Validate collateral rules
- `auditLog(action, userId, accountId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

### Integration Points

- **Consumers**: Unit 6 (Reporting)
- **Dependencies**: Unit 1, Unit 3, Unit 4, Unit 5

---

## Data Exchange Formats

### Standard Request Format
```json
{
  "requestId": "unique-request-id",
  "timestamp": "2025-12-05T14:30:00Z",
  "userId": "user-id",
  "action": "action-name",
  "parameters": {
    // Action-specific parameters
  }
}
```

### Standard Response Format
```json
{
  "requestId": "unique-request-id",
  "timestamp": "2025-12-05T14:30:01Z",
  "status": "SUCCESS|FAILURE",
  "data": {
    // Response data
  },
  "errors": [
    {
      "code": "error-code",
      "message": "error-message",
      "field": "field-name"
    }
  ]
}
```

### Error Codes
- `VALIDATION_ERROR`: Validation failure
- `NOT_FOUND`: Resource not found
- `UNAUTHORIZED`: User not authorized
- `DUPLICATE`: Duplicate record
- `BUSINESS_RULE_VIOLATION`: Business rule violation
- `SYSTEM_ERROR`: System error

---

## Communication Patterns

### Synchronous API Calls
- Primary communication pattern
- Request-response model
- Timeout: 30 seconds default
- Retry: 3 attempts with exponential backoff

### Error Handling
- All services return standardized error responses
- Errors include error code, message, and field (if applicable)
- Validation errors include all validation failures
- System errors are logged and alerted

### Authentication & Authorization
- All service calls include user context (userId, role, center)
- Access control service validates permissions before execution
- Center/branch restrictions applied to data queries
- Audit trail logs all service calls

### Transaction Management
- Each unit manages its own transactions
- Cross-unit operations use compensating transactions for rollback
- Audit trail tracks all transaction steps

---

## Dependency Graph

```
Unit 5 (Compliance & Validation)
    ↑
    |
Unit 4 (Reference Data Management)
    ↑
    |
    ├─→ Unit 1 (Loan Account Management) ←─┐
    |       ↑                               |
    |       |                               |
    ├─→ Unit 2 (Customer Management)       |
    |                                       |
    └─→ Unit 3 (Financial Management) ─────┘
            ↑
            |
    ┌───────┴───────┐
    |               |
Unit 6 (Reporting)  Unit 7 (Collateral & GL)
(OPTIONAL)          (OPTIONAL)
```

### Build Order Recommendation
1. **Phase 1**: Unit 5 (Compliance & Validation) - Foundational
2. **Phase 2**: Unit 4 (Reference Data Management) - Foundational
3. **Phase 3**: Unit 2 (Customer Management) - Core
4. **Phase 4**: Unit 1 (Loan Account Management) - Core
5. **Phase 5**: Unit 3 (Financial Management) - Core
6. **Phase 6**: Unit 6 (Reporting & Analytics) - Optional
7. **Phase 7**: Unit 7 (Collateral & GL Management) - Optional

---

## Testing Strategy

### Unit Testing
- Each unit tests its own services independently
- Mock dependencies from other units
- Test all business rules and validation logic

### Integration Testing
- Test service calls between units
- Test error handling and retry logic
- Test authentication and authorization
- Test audit trail logging

### End-to-End Testing
- Test complete business workflows across units
- Test user scenarios from UI to database
- Test performance and scalability

---

## Deployment Strategy

### Independent Deployment
- Each unit can be deployed independently
- Service versioning for backward compatibility
- Rolling deployment to minimize downtime

### Configuration Management
- Centralized configuration for service endpoints
- Environment-specific configurations
- Feature flags for optional units

---

**Status**: Ready for Implementation
**Next Steps**: Begin technical design for each unit following the build order recommendation
