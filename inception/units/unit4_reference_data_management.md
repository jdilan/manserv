# Unit 4: Reference Data Management

## Unit Information
- **Unit Name**: Reference Data Management
- **Priority**: 1 (Core/Required)
- **Team Assignment**: Single team
- **Dependencies**: Compliance & Validation Unit
- **Status**: Ready for Implementation

## Purpose and Scope

This unit manages all system reference data, master data, and configuration that supports the loan account management system. It provides centralized management of dropdown values, lookup tables, account types, economic activities, GL accounts, centers, groups, and other system-wide reference data.

## Business Capabilities

1. **Reference Data Management**: Maintain system-wide reference data for dropdowns and lookups
2. **Account Type Configuration**: Configure loan products with GL mappings
3. **Economic Activity Management**: Maintain industry sector classifications
4. **GL Account Management**: Maintain chart of accounts
5. **Organizational Structure**: Manage centers, budget units, and organizational hierarchy
6. **Group Management**: Maintain customer group definitions
7. **Data Integrity Validation**: Ensure reference data integrity across the system

## Assigned User Stories (8 total)

### Reference Data Management (7 user stories)

#### US-041: Manage Account Type Reference Data
**As a** system administrator  
**I want to** create and maintain account type definitions  
**So that** I can configure loan products with appropriate GL mappings

**Acceptance Criteria:**
- User can access Account Type management interface
- User can create new account type with code (3 characters) and description (40 characters)
- User can enter short name (20 characters)
- User can map GL accounts for each economic sector (63 fields total)
- System stores data in RLSACCT.DBF table
- System validates unique account type codes
- System prevents deletion of account types in use
- User can search and filter account types
- User can export account type list
- System maintains audit trail of changes
- Only Administrator role can manage account types

---

#### US-042: Manage Economic Activity Codes
**As a** system administrator  
**I want to** maintain economic activity classifications  
**So that** I can properly categorize loans by industry

**Acceptance Criteria:**
- User can access Economic Activity management interface
- User can create new economic activity with code (6 characters) and description (40 characters)
- User can assign economic group (6 characters)
- User can set purpose indicator (1 character)
- User can enter old/legacy code for migration (6 characters)
- System stores data in RLSECON.DBF table
- System validates unique economic activity codes
- System prevents deletion of codes in use
- User can search by code or description
- System supports bulk import/export
- System maintains version history
- Only Administrator role can manage economic activities

---

#### US-043: Manage General Ledger Master Accounts
**As a** system administrator  
**I want to** maintain the chart of accounts  
**So that** I can ensure proper accounting structure

**Acceptance Criteria:**
- User can access GL Account management interface
- User can create new GL account with account number (8 characters) and description (40 characters)
- User can set GL type (1 character)
- User can set balance indicator (1 character)
- User can assign account alias (9 characters)
- User can link to master key (18 characters)
- User can assign ward/subsidiary account (18 characters)
- System stores data in GLMACCT.DBF table
- System validates unique account numbers
- System enforces chart of accounts hierarchy
- System prevents deletion of accounts with balances
- User can search and filter accounts
- System supports account mapping and consolidation
- Only Administrator role can manage GL accounts

---

#### US-045: Manage Center/Budget Unit Data
**As a** system administrator  
**I want to** maintain organizational structure data  
**So that** I can properly categorize accounts by center and budget unit

**Acceptance Criteria:**
- User can access Center/Budget Unit management interface
- User can create new center with code (2 characters)
- User can assign budget unit (3 characters)
- User can enter description (20 characters)
- User can assign division code (1 character)
- System stores data in RLSCTR.DBF table
- System validates unique center codes
- System prevents deletion of centers in use
- User can view hierarchy of centers and budget units
- System supports organizational reporting
- Only Administrator role can manage centers

---

#### US-046: Manage System Reference Data (RLSSYS)
**As a** system administrator  
**I want to** maintain system-wide reference data  
**So that** I can configure dropdown lists and lookup values

**Acceptance Criteria:**
- User can access System Reference Data management interface
- User can create new reference data entry with group number (4 characters)
- User can enter group title (15 characters)
- User can enter user code (4 characters)
- User can enter user title (40 characters)
- User can enter additional info (4 characters)
- System stores data in RLSSYS.DBF table
- System groups entries by SYS_GNO for dropdown population
- System validates unique codes within each group
- User can search by group or code
- System prevents deletion of codes in use
- User can reorder entries within a group
- System supports bulk import/export
- System maintains effective dates for time-sensitive codes
- Only Administrator role can manage system reference data

**Key Reference Data Groups (SYS_GNO)**:
- 0002: Location
- 0003: Account Class
- 0004: Size of Firm
- 0006: Type of Borrower
- 0007: Nationality
- 0008: DOSRI
- 0014: Area (PA/NPA)
- 0015: Account Tag
- 0016: New Type of Borrower
- 0019: Guaranteed By
- 0020: Type of Credit
- 0021: Purpose of Credit
- 0022: Maturity Code
- 0024: Fund Source
- 0025: Lending Program
- 0030: Purpose (RE)
- 0031: Purpose (AA/AI)
- 0038: Security Code
- 0040: RDO
- 0068: BRR/CAMP
- 0090: Currency
- 0100: Corporation
- 0110: Book Code
- 0120: RI Indicator

---

#### US-047: Manage Group Reference Data (RLSGROUP)
**As a** system administrator  
**I want to** maintain customer group definitions  
**So that** I can track related party transactions and group exposures

**Acceptance Criteria:**
- User can access Group management interface
- User can create new group with ID (10 characters) and name (50 characters)
- System stores data in RLSGROUP table
- System validates unique group IDs
- User can assign customers to groups
- System calculates total group exposure
- System prevents deletion of groups with assigned customers
- User can search and filter groups
- System supports group hierarchy (parent-child relationships)
- Only Administrator role can manage groups

---

### Data Integrity Validation (1 user story)

#### US-066: Validate Reference Data Integrity
**As a** system  
**I want to** validate that entered codes exist in reference tables  
**So that** data integrity is maintained

**Acceptance Criteria:**
- System validates Account Type exists in RLSACCT.DBF
- System validates Economic Activity exists in RLSECON.DBF
- System validates GL Account Codes exist in GLMACCT.DBF
- System validates Center Code exists in RLSCTR.DBF
- System validates all dropdown selections exist in RLSSYS.DBF with correct SYS_GNO
- System validates Group Name exists in RLSGROUP table
- System validates Customer Number exists in RLSCUST.DBF when linking
- System displays error message when invalid code is entered
- System provides lookup functionality to help users find valid codes
- System prevents save when invalid reference codes are used

---

## Service Interfaces

### Exposed Services

This unit exposes the following service interfaces for consumption by other units:

#### 1. Reference Data Query Service
```
- getReferenceData(groupNumber): ReferenceDataList
- getReferenceDataByCode(groupNumber, code): ReferenceDataItem
- searchReferenceData(groupNumber, searchTerm): ReferenceDataList
- getAllGroups(): GroupList
```

#### 2. Account Type Service
```
- getAccountTypes(): AccountTypeList
- getAccountType(code): AccountTypeDetails
- getGLMappings(accountTypeCode, economicActivityCode): GLAccounts
- searchAccountTypes(searchTerm): AccountTypeList
```

#### 3. Economic Activity Service
```
- getEconomicActivities(): EconomicActivityList
- getEconomicActivity(code): EconomicActivityDetails
- searchEconomicActivities(searchTerm): EconomicActivityList
- getEconomicGroups(): EconomicGroupList
```

#### 4. GL Account Service
```
- getGLAccounts(): GLAccountList
- getGLAccount(accountNumber): GLAccountDetails
- searchGLAccounts(searchTerm): GLAccountList
- validateGLAccount(accountNumber): Boolean
```

#### 5. Center Service
```
- getCenters(): CenterList
- getCenter(code): CenterDetails
- getBudgetUnits(centerCode): BudgetUnitList
- validateCenter(code): Boolean
```

#### 6. Group Service
```
- getGroups(): GroupList
- getGroup(groupId): GroupDetails
- getGroupMembers(groupId): CustomerList
- calculateGroupExposure(groupId): ExposureAmount
```

#### 7. Validation Service
```
- validateReferenceCode(code, type): ValidationResult
- validateAccountType(code): Boolean
- validateEconomicActivity(code): Boolean
- validateGLAccount(accountNumber): Boolean
- validateCenter(code): Boolean
- validateGroup(groupId): Boolean
```

### Consumed Services

This unit consumes services from:

#### From Compliance & Validation Unit:
- `auditLog(action, userId, resourceId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

---

## Data Model

### Primary Tables

#### RLSACCT.DBF (Account Type Table)
- ACT_CODE: Account type code (3 characters)
- ACT_DESC: Description (40 characters)
- ACT_SNAME: Short name (20 characters)
- 63 GL account mapping fields for different economic sectors

#### RLSECON.DBF (Economic Activity Table)
- ECO_CODE: Economic activity code (6 characters)
- ECO_DESC: Description (40 characters)
- ECO_GRP: Economic group (6 characters)
- ECO_PURP: Purpose indicator (1 character)
- ECO_OLD: Old/legacy code (6 characters)

#### GLMACCT.DBF (GL Master Account Table)
- CACCTNO: Account number (8 characters)
- CACCTDESC: Description (40 characters)
- CGLTYPE: GL type (1 character)
- CBALIND: Balance indicator (1 character)
- CACTALIAS: Account alias (9 characters)
- CMKEY: Master key (18 characters)
- CWARD: Ward/subsidiary account (18 characters)

#### RLSCTR.DBF (Center/Budget Unit Table)
- CTR_CODE: Center code (2 characters)
- CTR_BUNIT: Budget unit (3 characters)
- CTR_DESC: Description (20 characters)
- CTR_DIV: Division code (1 character)

#### RLSSYS.DBF (System Reference Data Table)
- SYS_GNO: Group number (4 characters)
- SYS_GTITLE: Group title (15 characters)
- SYS_UCODE: User code (4 characters)
- SYS_UTITLE: User title (40 characters)
- SYS_INFO: Additional info (4 characters)

#### RLSGROUP (Group Table)
- GRP_ID: Group ID (10 characters)
- GRP_NAME: Group name (50 characters)

---

## Business Rules Summary

### Validation Rules
1. **Unique Codes**: All codes must be unique within their respective tables
2. **Deletion Prevention**: Prevent deletion of reference data in use
3. **Code Format**: Validate code lengths and formats
4. **Hierarchy**: Enforce chart of accounts hierarchy for GL accounts

### Access Control
1. **Roles**: Only Administrator role can manage reference data
2. **Audit Trail**: Log all create, update, delete operations
3. **Version History**: Maintain version history for reference data changes

### Data Integrity
1. **Referential Integrity**: Validate all foreign key references
2. **Lookup Validation**: Provide lookup functionality for code selection
3. **Error Messages**: Display clear error messages for invalid codes

---

## Integration Points

### Dependencies
- **Compliance & Validation Unit**: For audit logging and access control

### Consumers
- **Loan Account Management Unit**: Uses reference data for dropdowns and validation
- **Customer Management Unit**: Uses reference data for dropdowns and validation
- **Financial Management Unit**: Uses reference data for dropdowns and validation
- **Reporting & Analytics Unit**: Uses reference data for report filtering and grouping
- **Collateral & GL Management Unit** (Optional): Uses GL account data

---

## Implementation Notes

1. **Caching**: Implement caching for frequently accessed reference data
2. **Bulk Operations**: Support bulk import/export for reference data
3. **Search**: Implement efficient search functionality for large reference data sets
4. **Dropdown Population**: Optimize dropdown population for performance
5. **Version Control**: Maintain version history for reference data changes
6. **Effective Dates**: Support effective dates for time-sensitive reference data
7. **Audit Trail**: Log all reference data changes with user ID and timestamp

---

## Testing Considerations

1. **Unit Tests**: Test all CRUD operations for each reference data type
2. **Integration Tests**: Test integration with all consuming units
3. **Validation Tests**: Test referential integrity validation
4. **Access Control Tests**: Verify only Administrator role can manage reference data
5. **Performance Tests**: Test dropdown population and search performance
6. **Bulk Operations**: Test bulk import/export functionality
7. **Edge Cases**: Test deletion prevention, unique code validation, hierarchy enforcement

---

**Status**: Ready for Implementation
**Next Steps**: Begin technical design and implementation planning
