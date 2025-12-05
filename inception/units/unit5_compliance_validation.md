# Unit 5: Compliance & Validation

## Unit Information
- **Unit Name**: Compliance & Validation
- **Priority**: 1 (Core/Required)
- **Team Assignment**: Single team
- **Dependencies**: None (foundational unit)
- **Status**: Ready for Implementation

## Purpose and Scope

This unit provides system-wide validation, business rules enforcement, audit trail management, batch processing, and user access control. It serves as a foundational unit that ensures data integrity, regulatory compliance, and security across the entire loan account management system.

## Business Capabilities

1. **Data Validation**: Enforce mandatory fields, field formats, and data type validations
2. **Business Rules Enforcement**: Validate cross-field rules and business logic
3. **Auto-Population**: Automatically populate dependent fields based on business rules
4. **Conditional Logic**: Enforce conditional field requirements
5. **Account Status Management**: Manage account status transitions and rules
6. **Audit Trail**: Maintain comprehensive audit logs for compliance
7. **Batch Processing**: Execute scheduled batch jobs for system maintenance
8. **User Access Control**: Manage authentication, authorization, and role-based access

## Assigned User Stories (13 total)

### Data Validation (5 user stories)

#### US-061: Validate Mandatory Fields
**As a** system  
**I want to** enforce mandatory field requirements  
**So that** data integrity is maintained

**Acceptance Criteria:**
- System validates mandatory fields before save:
  - **General**: Ref. No, Prev. Ref. No., Customer Name, Long Name
  - **Customer Info**: Business/Residence Address, Project Address, Affiliate, RDO, TIN, V/N, OFW
  - **Loan Info**: (varies by account type)
  - **Balances**: OPB (Peso)
  - **Customer**: BRR/CAMP
- System displays clear error messages identifying missing fields
- System highlights missing fields in red or with error icon
- System prevents save until all mandatory fields are completed
- System allows save as draft with incomplete data (with warning)
- System validates mandatory fields on each tab before allowing navigation
- Users can save as "Draft" with incomplete data

**Mandatory Fields Summary**:
- General: Ref. No, Prev. Ref. No., Customer Name, Long Name
- Customer Info: Business/Residence Address, Project Address, Affiliate, RDO, TIN, V/N, OFW, BRR/CAMP
- Balances: OPB (Peso)

---

#### US-062: Validate Field Formats and Data Types
**As a** system  
**I want to** validate field formats and data types  
**So that** data quality is maintained

**Acceptance Criteria:**
- System validates character field lengths (e.g., Ref. No max 17 characters)
- System validates numeric fields accept only numbers
- System validates date fields accept only valid dates
- System validates TIN format (17 characters, standard format, no specific pattern)
- System validates email format if email field is added
- System validates percentage fields (0-100 range)
- System validates currency amounts (positive numbers with 2 decimal places)
- System displays format-specific error messages
- System prevents entry of invalid characters in numeric fields
- System auto-formats fields where appropriate (e.g., currency with thousand separators)

**Format Validations**:
- TIN: Max 17 characters, standard format (no specific pattern)
- Reference Number: Max 17 characters (no specific pattern or prefix)
- CRIB ID: Max 10 characters (no specific format)
- NIDSS Account No: Max 13 characters (no specific format)
- Interest Rate: 0-100% range

---

#### US-063: Validate Cross-Field Business Rules
**As a** system  
**I want to** enforce cross-field validation rules  
**So that** data consistency is maintained

**Acceptance Criteria:**
- System validates: Released Amount <= Approved Amount (strict enforcement)
- System validates: OPB <= Released Amount (strict enforcement)
- System validates: Start of Term = Orig Release Date (must be equal)
- System validates: Maturity Date > Start of Term (strict enforcement)
- System validates: Total Assets = Total Liabilities + Stockholder Equity (OPTIONAL - not enforced)
- System displays specific error messages for each validation failure
- System prevents save when cross-field validations fail (except optional validations)

**Cross-Field Validation Rules**:
- Released Amount <= Approved Amount: Strict enforcement
- OPB <= Released Amount: Strict enforcement
- Start of Term = Orig Release Date: Must be equal (not >=)
- Maturity Date > Start of Term: Strict enforcement
- Total Assets = Total Liabilities + Stockholder Equity: OPTIONAL (not enforced)

**Note**: Amount Secured + Amount Unsecured = OPB validation is moved to Collateral & GL Management Unit (optional)

---

#### US-064: Auto-populate Dependent Fields
**As a** system  
**I want to** automatically populate dependent fields  
**So that** data entry is efficient and consistent

**Acceptance Criteria:**
- System auto-populates Type of Credit based on Account Type and Status (cannot override)
- System auto-populates Purpose of Credit based on Account Type (cannot override)
- System auto-populates GL account codes based on Account Type from RLSACCT.DBF (can override)
- System auto-populates Date of Last Transaction from most recent transaction in MNSHTRAN.DBF
- System auto-calculates No. of Days Past Due based on Past Due Date and current date
- System auto-calculates collateral coverage ratio (if collateral module is implemented)
- System auto-calculates financial ratios from financial statement data
- System updates dependent fields when source fields change
- System logs auto-population actions for audit
- GL account codes can be overridden by users
- Other auto-populated fields cannot be overridden

**Auto-Population Rules**:
- Type of Credit: Auto-populated based on Account Type and Status (cannot override)
- Purpose of Credit: Auto-populated based on Account Type (cannot override)
- GL Account Codes: Auto-populated from RLSACCT.DBF (can override)
- Date of Last Transaction: Auto-populated from MNSHTRAN.DBF
- No auto-populated fields can be overridden except GL codes

---

#### US-065: Enforce Conditional Field Requirements
**As a** system  
**I want to** enforce conditional field requirements  
**So that** appropriate data is collected based on context

**Acceptance Criteria:**
- System enables No. of OFW field only when OFW = Y
- System requires No. of OFW when OFW = Y (must be > 0)
- System disables and clears No. of OFW when OFW = N
- System enables Guaranteed By field only when Guaranteed = Y
- Guaranteed By is NOT mandatory when Guaranteed = Y
- System requires Purpose when Account Type is AA, AI, R, RDC, RDE, or RDH
- System adjusts field requirements dynamically as user enters data
- System provides clear indication of which fields are required in current context

**Conditional Field Rules**:
- No. of OFW: Required when OFW = Y, must be > 0
- Guaranteed By: NOT required when Guaranteed = Y
- Purpose: Required for Account Types AA, AI, R, RDC, RDE, RDH
- Amount Secured/Unsecured: NOT required (moved to optional Collateral module)

---

### Account Status Management (1 user story)

#### US-068: Enforce Account Status Rules
**As a** system  
**I want to** enforce account status transition rules  
**So that** account status changes follow business logic

**Acceptance Criteria:**
- System automatically changes status to PDO (Past Due) when account is 90 days past due
- System automatically changes Type of Credit to LITIG when Under Litig = Y
- System updates Account Classification based on days past due and payment history
- System prevents certain transactions on closed accounts
- System prevents certain transactions on accounts under litigation
- System validates status transitions follow allowed paths
- System logs all status changes with reason, user, and timestamp
- System sends alerts when accounts change to adverse status
- System applies appropriate reserve requirements based on status
- System updates Area field (PA/NPA) based on account status
- Status can be manually overridden
- Litigation status is manual only (separate Reclass Module for Litigation)

**Account Status Transition Rules**:
- Current to Past Due: Automatic after 90 days
- Litigation Status: Manual only (create separate Reclass Module)
- Status can be manually overridden

**Account Classification Rules**:
- Classification based on days past due only
- Classification triggers: Non-payment, number of days past due, and upon payment
- Classification thresholds:
  - Current: 0-30 days
  - Watchlisted: 31-60 days
  - Substandard: 61-90 days
  - Doubtful: 91-180 days
  - Loss: 180+ days
- Classification can be manually overridden

---

### Audit and Compliance (1 user story)

#### US-070: Maintain Audit Trail
**As a** system  
**I want to** maintain comprehensive audit trail  
**So that** all changes are tracked for compliance and troubleshooting

**Acceptance Criteria:**
- System logs all create, update, delete operations
- System captures: User ID, Timestamp, Action, Old Value, New Value
- System logs field-level changes (which fields were modified)
- System logs transaction postings with full details
- System logs status changes with reason codes
- System logs report generation and exports
- System logs user login/logout activities
- Audit logs are immutable (cannot be modified or deleted)
- Audit logs are retained per regulatory requirements
- User can search and view audit logs (with appropriate permissions)
- System generates audit reports for compliance reviews
- System alerts on suspicious activities or patterns

---

### Batch Processing (1 user story)

#### US-072: Support Batch Processing
**As a** system administrator  
**I want to** run batch processes for routine calculations  
**So that** system data is updated automatically

**Acceptance Criteria:**
- System runs daily batch to calculate AIR for all accounts at 11:59 PM
- System runs daily batch to update days past due at 11:59 PM
- System runs daily batch to update account classifications
- System runs monthly batch to generate statements at end of month
- System logs batch execution results
- System sends alerts on batch failures
- System allows manual triggering of batch processes
- System prevents concurrent batch execution
- System maintains batch execution history
- Batch failure handling: Retry logic and alerts

**Batch Processing Schedule**:
- Daily AIR calculation: 11:59 PM
- Daily days past due update: 11:59 PM
- Account classification update: Daily
- Statement generation: End of month
- Batch failure handling: Retry logic and alerts

---

### User Access Control (1 user story)

#### US-074: Support User Access Control
**As a** system administrator  
**I want to** control user access to functions and data  
**So that** security and segregation of duties are maintained

**Acceptance Criteria:**
- System authenticates users with username and password
- System supports role-based access control (RBAC)
- System defines roles: User, Authorizer, Administrator
- System restricts functions based on user role:
  - **User**: Can create and update
  - **Authorizer**: Can create and update
  - **Administrator**: Can create, update, and delete
- System restricts data access based on center/branch assignment
- System logs all user activities
- System enforces password complexity requirements
- System supports password expiration and reset
- System locks accounts after failed login attempts
- System supports single sign-on (SSO) if available
- System displays user's role and permissions
- System allows administrators to manage user accounts and permissions
- Standard approval workflows required for certain operations

**User Roles and Permissions**:
- **User**: Create and update (no delete)
- **Authorizer**: Create and update (no delete)
- **Administrator**: Create, update, and delete
- **Data Access**: By center/branch
- **Approval Workflows**: Standard approval required

---

## Service Interfaces

### Exposed Services

This unit exposes the following service interfaces for consumption by other units:

#### 1. Validation Service
```
- validateMandatoryFields(entityData): ValidationResult
- validateFieldFormats(entityData): ValidationResult
- validateCrossFieldRules(entityData): ValidationResult
- validateConditionalFields(entityData): ValidationResult
- validateBusinessRules(entityData): ValidationResult
```

#### 2. Auto-Population Service
```
- autoPopulateFields(entityData): UpdatedEntityData
- calculateDependentFields(entityData): CalculatedFields
- updateDependentFields(entityId, sourceField, sourceValue): Success/Failure
```

#### 3. Status Management Service
```
- validateStatusTransition(currentStatus, newStatus): ValidationResult
- updateAccountStatus(accountId, newStatus, reason): Success/Failure
- checkStatusRules(accountId): StatusCheckResult
- getStatusHistory(accountId): StatusHistory
```

#### 4. Audit Service
```
- auditLog(action, userId, resourceId, changes): Success/Failure
- getAuditLog(resourceId, dateRange): AuditLogEntries
- searchAuditLog(searchCriteria): AuditLogEntries
- generateAuditReport(reportCriteria): AuditReport
```

#### 5. Batch Processing Service
```
- executeBatch(batchType): BatchResult
- scheduleBatch(batchType, schedule): Success/Failure
- getBatchStatus(batchId): BatchStatus
- getBatchHistory(batchType, dateRange): BatchHistory
```

#### 6. Access Control Service
```
- authenticateUser(username, password): AuthToken
- checkUserPermission(userId, action, resourceId): Boolean
- getUserRole(userId): Role
- getUserPermissions(userId): PermissionList
- restrictByCenter(userId, query): FilteredQuery
```

### Consumed Services

This unit is a foundational unit and does not consume services from other units. It provides services to all other units.

---

## Data Model

### Primary Tables

#### Audit Log Table (New)
- AUDIT_ID: Unique audit log ID
- USER_ID: User who performed the action
- TIMESTAMP: Date and time of action
- ACTION: Type of action (CREATE, UPDATE, DELETE, etc.)
- RESOURCE_TYPE: Type of resource (ACCOUNT, CUSTOMER, etc.)
- RESOURCE_ID: ID of the resource
- FIELD_NAME: Name of field changed
- OLD_VALUE: Previous value
- NEW_VALUE: New value
- REASON: Reason for change (if applicable)

#### Batch Execution Log Table (New)
- BATCH_ID: Unique batch execution ID
- BATCH_TYPE: Type of batch (AIR_CALC, PAST_DUE_UPDATE, etc.)
- START_TIME: Batch start time
- END_TIME: Batch end time
- STATUS: Execution status (SUCCESS, FAILURE, RUNNING)
- RECORDS_PROCESSED: Number of records processed
- RECORDS_FAILED: Number of records failed
- ERROR_MESSAGE: Error message if failed

#### User Table (New)
- USER_ID: Unique user ID
- USERNAME: Login username
- PASSWORD_HASH: Hashed password
- ROLE: User role (USER, AUTHORIZER, ADMINISTRATOR)
- CENTER_CODE: Assigned center/branch
- ACTIVE: Active status
- LAST_LOGIN: Last login timestamp
- FAILED_ATTEMPTS: Failed login attempts

---

## Business Rules Summary

### Mandatory Field Rules
- General: Ref. No, Prev. Ref. No., Customer Name, Long Name
- Customer Info: Business/Residence Address, Project Address, Affiliate, RDO, TIN, V/N, OFW, BRR/CAMP
- Balances: OPB (Peso)
- Draft save allowed with incomplete data

### Field Format Rules
- TIN: Max 17 characters, standard format
- Reference Number: Max 17 characters, duplicate warning but allow override
- CRIB ID: Max 10 characters, no specific format
- NIDSS Account No: Max 13 characters, no specific format
- Interest Rate: 0-100% range

### Cross-Field Validation Rules
- Released Amount <= Approved Amount: Strict enforcement
- OPB <= Released Amount: Strict enforcement
- Start of Term = Orig Release Date: Must be equal
- Maturity Date > Start of Term: Strict enforcement
- Total Assets = Total Liabilities + Stockholder Equity: OPTIONAL

### Conditional Field Rules
- No. of OFW: Required when OFW = Y, must be > 0
- Guaranteed By: NOT required when Guaranteed = Y
- Purpose: Required for Account Types AA, AI, R, RDC, RDE, RDH

### Auto-Population Rules
- Type of Credit: Auto-populated, cannot override
- Purpose of Credit: Auto-populated, cannot override
- GL Account Codes: Auto-populated, can override
- Date of Last Transaction: Auto-populated
- No. of Days Past Due: Auto-calculated

### Account Status Rules
- Current to Past Due: Automatic after 90 days
- Litigation: Manual only (separate Reclass Module)
- Classification based on days past due:
  - Current: 0-30 days
  - Watchlisted: 31-60 days
  - Substandard: 61-90 days
  - Doubtful: 91-180 days
  - Loss: 180+ days
- Status and classification can be manually overridden

### Batch Processing Rules
- Daily AIR calculation: 11:59 PM
- Daily days past due update: 11:59 PM
- Account classification update: Daily
- Statement generation: End of month
- Batch failure: Retry logic and alerts

### Access Control Rules
- Roles: User, Authorizer, Administrator
- User & Authorizer: Create and update
- Administrator: Create, update, and delete
- Data access: By center/branch
- Approval workflows: Standard approval required

---

## Integration Points

### Dependencies
None (foundational unit)

### Consumers
- **All Units**: All units consume validation, audit, and access control services from this unit

---

## Implementation Notes

1. **Validation Framework**: Implement a flexible validation framework that can be easily extended
2. **Audit Trail**: Design immutable audit log with efficient querying capabilities
3. **Batch Processing**: Implement robust batch processing with error handling and retry logic
4. **Access Control**: Implement role-based access control with center/branch restrictions
5. **Performance**: Optimize validation and audit logging for performance
6. **Alerts**: Implement alert system for batch failures and suspicious activities
7. **Draft Save**: Support draft save functionality with incomplete data

---

## Testing Considerations

1. **Unit Tests**: Test all validation rules, auto-population logic, and business rules
2. **Integration Tests**: Test integration with all consuming units
3. **Access Control Tests**: Verify role-based access and center/branch restrictions
4. **Batch Tests**: Test batch processing with various scenarios and failure conditions
5. **Audit Tests**: Verify audit trail completeness and immutability
6. **Performance Tests**: Test validation and audit logging performance
7. **Edge Cases**: Test conditional logic, status transitions, and complex validation rules

---

**Status**: Ready for Implementation
**Next Steps**: Begin technical design and implementation planning

**Note**: This is a foundational unit that all other units depend on for validation, audit, and access control.
