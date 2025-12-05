# Unit 1: Loan Account Management

## Unit Information
- **Unit Name**: Loan Account Management
- **Priority**: 1 (Core/Required)
- **Team Assignment**: Single team
- **Dependencies**: Reference Data Management Unit, Compliance & Validation Unit
- **Status**: Ready for Implementation

## Purpose and Scope

This unit manages the core loan account lifecycle including create, read, update, delete operations, and basic loan information management. It handles account identification, loan terms, dates, funding sources, and account operations like search, copy, and archival.

## Business Capabilities

1. **Loan Account CRUD Operations**: Create, view, update, and delete loan accounts
2. **Loan Information Management**: Manage account identification, dates, types, purposes, funding sources, and status
3. **Account Search and Retrieval**: Search and retrieve loan accounts by various criteria
4. **Account Lifecycle Operations**: Copy accounts, close/archive accounts

## Assigned User Stories (23 total)

### General Section (4 user stories)

#### US-001: Create New Loan Account with General Information
**As a** loan officer  
**I want to** create a new loan account with general customer information  
**So that** I can initiate the loan account setup process

**Acceptance Criteria:**
- System displays a New Account form with GENERAL section as the default view
- User can enter Reference Number (Ref. No) - mandatory field, max 17 characters
- User can enter Previous Reference Number (Prev. Ref. No.) - mandatory field, max 17 characters
- User can enter CRIB ID No. - optional field, max 10 characters
- User can enter Customer Name - mandatory field, max 40 characters
- User can enter NIDSS Account No. - optional field, max 13 characters
- User can enter Long Name - mandatory field, max 100 characters
- System validates that all mandatory fields are filled before allowing save
- System displays warning for duplicate Reference Numbers but allows override
- System stores data in MANSERV.DBF table with appropriate field mappings
- System displays validation error messages for invalid or missing mandatory data
- User can save as "Draft" with incomplete data

**Validation Rules**:
- Ref. No: Mandatory, max 17 characters, duplicate warning but allow override
- Prev. Ref. No.: Mandatory, max 17 characters
- Customer Name: Mandatory, max 40 characters
- Long Name: Mandatory, max 100 characters
- CRIB ID No.: Optional, max 10 characters, no specific format
- NIDSS Account No.: Optional, max 13 characters, no specific format

---

#### US-002: View Existing Loan Account General Information
**As a** loan officer  
**I want to** view the general information of an existing loan account  
**So that** I can review customer basic details

**Acceptance Criteria:**
- User can search for an account by Reference Number
- System displays the GENERAL section with all populated fields
- All fields are displayed in read-only mode initially
- User can see: Ref. No, Prev. Ref. No., CRIB ID No., Customer Name, NIDSS Account No., Long Name
- System retrieves data from MANSERV.DBF table
- System displays appropriate message if account not found
- Access restricted by center/branch based on user role

---

#### US-003: Update Loan Account General Information
**As a** loan officer  
**I want to** update the general information of an existing loan account  
**So that** I can correct or modify customer basic details

**Acceptance Criteria:**
- User can enable edit mode for an existing account
- User can modify all fields except Reference Number (primary key)
- System validates mandatory fields: Ref. No, Prev. Ref. No., Customer Name, Long Name
- System validates field lengths and data types
- User can save changes or cancel modifications
- System updates MANSERV.DBF table with modified data
- System displays confirmation message upon successful update
- System maintains audit trail of changes (user ID, timestamp)
- Only User and Authorizer roles can update
- Access restricted by center/branch

---

#### US-004: Delete Loan Account
**As a** loan officer with appropriate permissions  
**I want to** delete a loan account  
**So that** I can remove erroneous or cancelled accounts

**Acceptance Criteria:**
- User can select an existing account for deletion
- System displays confirmation dialog with account details before deletion
- System checks for dependencies (transactions, collateral, etc.) before allowing deletion
- System prevents deletion if account has transaction history
- System prevents deletion if account has active balances or collateral
- System performs soft delete (marks as deleted) rather than hard delete
- System logs deletion action with user ID and timestamp
- System displays success message after deletion
- Deleted accounts are excluded from standard searches but can be viewed in audit reports
- Deleted accounts cannot be restored
- Only Administrator role can delete accounts

**Validation Rules**:
- Soft delete only (mark as deleted, not physical deletion)
- Prevent deletion if transaction history exists
- Prevent deletion if active balances exist
- Prevent deletion if collateral exists
- Administrator role only

---

### Loan Info Section (9 user stories)

#### US-015: Manage Account Identification
**As a** loan officer  
**I want to** assign organizational codes to the loan account  
**So that** I can properly categorize and track the account within the bank structure

**Acceptance Criteria:**
- User can access Loan Info tab
- User can enter Center Code - 2 character field
- User can enter Budget Unit - 3 character field
- User can select Corporation from dropdown - populated from RLSSYS where SYS_GNO = '0100'
- Corporation options: RTL (Retail), FCDU (Foreign Currency), FCDW (Foreign Currency Wholesale), WBG (Wholesale)
- User can select Book Code from dropdown - populated from RLSSYS where SYS_GNO = '0110'
- Book Code options:
  - 11: PESO REGULAR ACCOUNTS
  - 12: FOREIGN REGULAR ACCOUNTS
  - 20: FCDU ACCOUNTS
  - 30: FOREIGN OFFICE ACCOUNTS
  - 41: TRUST DEPT ACCT - PESO ACCT
  - 42: TRUST DEPT ACCT - FCDU ACCT
- System stores Center Code in MANSERV.CNT_CENTER field
- System stores Budget Unit in MANSERV.BUNIT field
- System stores Corporation in MANSERV.CORP field
- System stores Book Code in MANSERV.BOOKCDE field
- System validates Center Code against RLSCTR.DBF table

---

#### US-016: Manage Economic Activity Classification
**As a** loan officer  
**I want to** classify the loan by economic activity  
**So that** I can track lending by industry sector

**Acceptance Criteria:**
- User can select Economic Activity from dropdown - populated from RLSECON.DBF table
- Dropdown displays ECO_DESC and stores ECO_CODE (6 characters)
- System stores selection in MANSERV.CNT_ECON field
- System links to economic sector GL accounts in RLSACCT.DBF
- System uses economic activity for regulatory reporting
- Dropdown is searchable by code or description

---

#### US-017: Manage Loan Dates
**As a** loan officer  
**I want to** record critical loan dates  
**So that** I can track loan lifecycle and calculate interest properly

**Acceptance Criteria:**
- User can enter Orig Release Date (Original Release Date) - date field
- User can enter Start of Term - date field
- User can enter Maturity Date - date field
- System stores Orig Release Date in MANSERV.ORELDAT field
- System stores Start of Term in MANSERV.CNT_STERM field
- System stores Maturity Date in MANSERV.CNT_MATD field
- System validates that Start of Term = Orig Release Date (must be equal)
- System validates that Maturity Date > Start of Term
- System calculates loan term based on dates
- System uses Start of Term as reckoning date for interest computation

**Validation Rules**:
- Start of Term = Orig Release Date (must be equal, not >=)
- Maturity Date > Start of Term (strict enforcement)

---

#### US-018: Manage Account Type and Purpose
**As a** loan officer  
**I want to** classify the loan by type and purpose  
**So that** I can apply appropriate lending policies and track loan usage

**Acceptance Criteria:**
- User can select Account Type from dropdown - populated from RLSACCT.DBF (ACT_CODE, ACT_DESC)
- Common account types include: Agri, REL (Real Estate), IND (Industrial)
- User can select Purpose from dropdown - conditional field
- Purpose field is mandatory when Account Type is AA, AI, R, RDC, RDE, or RDH
- Purpose dropdown populated from RLSSYS where SYS_GNO = '0030' (for RE) or '0031' (for AA/AI)
- System stores Account Type in MANSERV.CNT_ATYPE field (3 characters)
- System stores Purpose in MANSERV.CNT_PURP field (1 character)
- System auto-populates related GL accounts based on Account Type from RLSACCT.DBF
- System validates Purpose selection matches Account Type requirements

**Validation Rules**:
- Purpose is mandatory when Account Type is AA, AI, R, RDC, RDE, or RDH

---

#### US-019: Manage Funding Source and Program
**As a** loan officer  
**I want to** record the funding source and lending program  
**So that** I can track fund utilization and program performance

**Acceptance Criteria:**
- User can select Fund Source from dropdown - populated from RLSSYS where SYS_GNO = '0024'
- Fund Source includes options like BSP/CB, DBP, LBP, WB, ACPC, etc.
- User can select Lending Program from dropdown - populated from RLSSYS where SYS_GNO = '0025'
- Lending Program includes various programs (DBP Internal Financing, ALF, CLF, etc.)
- User can select Area from dropdown - populated from RLSSYS where SYS_GNO = '0014'
- Area options: PA (Performing), NPA (Non-Performing)
- System stores Fund Source in MANSERV.CNT_FUND field (3 characters)
- System stores Lending Program in MANSERV.CNT_PROG field (3 characters)
- System stores Area in MANSERV.AREA field (3 characters)
- System uses these fields for fund tracking and program reporting

---

#### US-020: Manage Loan Status and Classification
**As a** loan officer  
**I want to** track loan status and classification  
**So that** I can monitor loan performance and apply appropriate accounting treatment

**Acceptance Criteria:**
- User can select Restructured indicator - Y/N field
- User can view Type of Credit - auto-populated field based on Account Type and status
- Type of Credit populated from RLSSYS where SYS_GNO = '0020'
- Type of Credit includes: CUR (Current), PDO (Past Due), LITIG (Litigation)
- User can select Maturity Code from dropdown - populated from RLSSYS where SYS_GNO = '0022'
- Maturity Code options: A (Demand), B (Short-term), C-E (Intermediate to Long-term)
- User can view Purpose of Credit - auto-populated based on Account Type
- Purpose of Credit populated from RLSSYS where SYS_GNO = '0021'
- User can enter No. of Record - numeric field, max 5 digits
- System stores Restructured in MANSERV.REST field
- System stores Type of Credit in MANSERV.CNT_CRTYPE field (6 characters)
- System stores Maturity Code in MANSERV.CNT_MAT field (1 character)
- System stores Purpose of Credit in MANSERV.CNT_CRPURP field (1 character)
- System stores No. of Record in MANSERV.CNT_REC field
- System auto-updates Type of Credit when account status changes
- Type of Credit and Purpose of Credit cannot be overridden by users

**Auto-Population Rules**:
- Type of Credit: Auto-populated based on Account Type and Status (cannot override)
- Purpose of Credit: Auto-populated based on Account Type (cannot override)

---

#### US-021: Manage Guarantee Information
**As a** loan officer  
**I want to** record guarantee information for the loan  
**So that** I can track guaranteed loans and their guarantors

**Acceptance Criteria:**
- User can select Guaranteed indicator - Y/N field
- User can select Guaranteed By from dropdown - conditional field
- Guaranteed By field is enabled only when Guaranteed = Y
- Guaranteed By field is NOT mandatory when Guaranteed = Y
- Guaranteed By dropdown populated from RLSSYS where SYS_GNO = '0019'
- Options include: SBGFC, GFSME, PHILGUARANTEE
- System stores Guaranteed in MANSERV.GUAR field
- System stores Guaranteed By in MANSERV.CNT_GBY field
- System disables and clears Guaranteed By when Guaranteed = N
- System applies special reporting for guaranteed loans

**Validation Rules**:
- Guaranteed By is NOT mandatory even when Guaranteed = Y

---

#### US-022: Manage Litigation Status
**As a** loan officer  
**I want to** mark accounts under litigation  
**So that** I can track legal proceedings and apply appropriate accounting treatment

**Acceptance Criteria:**
- User can select Under Litig (Under Litigation) indicator - Y/N field
- System stores value in MANSERV.LITIG field
- System automatically updates Type of Credit to LITIG when Under Litig = Y
- System prevents certain transactions when account is under litigation
- System includes litigation accounts in special monitoring reports
- System tracks litigation date in MANSERV.ITL field
- Litigation status is manual only (create separate Reclass Module for Litigation)

**Business Rules**:
- Litigation status is set manually only
- Create a separate Reclass Module for Litigation processing

---

#### US-023: Manage Loan Project Type and Currency
**As a** loan officer  
**I want to** classify the loan project type and currency  
**So that** I can apply appropriate policies and track multi-currency loans

**Acceptance Criteria:**
- User can select Loan Status from dropdown - options: CUR (Current), PDO (Past Due)
- User can select Loan Project Type - radio button or dropdown with options: C (Commercial), D (Developmental)
- User can select Currency from dropdown - populated from RLSSYS where SYS_GNO = '0090'
- Currency dropdown includes: PHP, USD, JPY, EUR, GBP, and other major currencies
- System stores Loan Status in MANSERV.CNT_LSTAT field (3 characters)
- System stores Loan Project Type in MANSERV.CNT_LPTYPE field (1 character)
- System stores Currency in MANSERV.RELCURR field (3 characters)
- System uses currency selection to determine which balance columns to use:
  - PHP → Peso column
  - USD → Dollar column
  - Others (JPY, EUR) → Others column
- System applies different interest rates based on project type
- Account changes from Current to Past Due after 90 days

**Business Rules**:
- Account status changes from Current (CUR) to Past Due (PDO) after 90 days

---

### Account Operations (4 user stories)

#### US-048: Search Loan Accounts
**As a** loan officer  
**I want to** search for loan accounts using various criteria  
**So that** I can quickly find and access account information

**Acceptance Criteria:**
- User can access account search interface
- User can search by Reference Number (exact match)
- User can search by Customer Name (partial match, case-insensitive)
- User can search by CRIB ID
- User can search by TIN
- User can search by Account Type
- User can search by Center Code
- User can search by Account Status (Current, Past Due, Litigation)
- User can search by date ranges (creation date, maturity date)
- User can combine multiple search criteria (AND logic)
- System displays search results in paginated list
- System shows key fields in results: Ref No, Customer Name, Account Type, OPB, Status
- User can sort results by any column
- User can export search results to Excel/CSV
- System limits results to user's authorized centers/branches
- Search performs efficiently even with large datasets

**Access Control**:
- Results limited to user's authorized centers/branches

---

#### US-049: View Account Summary
**As a** loan officer  
**I want to** view a comprehensive summary of a loan account  
**So that** I can quickly assess account status and key metrics

**Acceptance Criteria:**
- User can access account summary from search results or by entering Reference Number
- System displays summary with all tabs: General, Customer Info/Approval, Loan Info, Balances (Collateral/Dates/GL and Manner of Release are optional)
- Summary shows key metrics: OPB, AIR, Past Due Amount, Days Past Due, Account Classification
- Summary shows customer information: Name, TIN, Address, Contact
- Summary shows loan terms: Interest Rate, Maturity Date, Payment Frequency
- Summary shows recent transaction history (last 10 transactions)
- User can navigate to detailed tabs from summary
- System displays alerts for: Past due accounts, Expiring documents, Classification changes
- Summary is read-only; user must enter edit mode to modify
- Access restricted by center/branch

---

#### US-050: Copy Loan Account
**As a** loan officer  
**I want to** create a new loan account by copying an existing one  
**So that** I can quickly set up renewal or additional loans for existing customers

**Acceptance Criteria:**
- User can select "Copy Account" option from existing account
- System creates new account with new Reference Number
- System copies customer information from original account
- System copies loan structure (Account Type, Economic Activity, GL codes)
- System does NOT copy balances (OPB, AIR, etc.) - these start at zero
- System does NOT copy transaction history
- System sets Prev. Ref. No. to the original account's Reference Number
- User can modify any field before saving
- System validates all mandatory fields
- System saves new account as separate record
- System maintains link between original and copied account

---

#### US-051: Close/Archive Loan Account
**As a** loan officer  
**I want to** close or archive fully paid loan accounts  
**So that** I can maintain clean active account lists

**Acceptance Criteria:**
- User can select "Close Account" option for accounts with zero balances
- System validates that OPB = 0, AIR = 0, Past Due = 0
- System prevents closure if any balance is outstanding
- System marks account as closed with closure date
- System moves account to archived status
- Closed accounts are excluded from active account searches by default
- User can search archived accounts separately
- System maintains full history of closed accounts
- System allows reopening of closed accounts if needed (with approval)
- System generates closure report with final balances and transaction summary

---

## Service Interfaces

### Exposed Services

This unit exposes the following service interfaces for consumption by other units:

#### 1. Account Management Service
```
- createAccount(accountData): AccountId
- getAccount(accountId): AccountDetails
- updateAccount(accountId, accountData): Success/Failure
- deleteAccount(accountId): Success/Failure
- searchAccounts(searchCriteria): AccountList
- copyAccount(sourceAccountId): NewAccountId
- closeAccount(accountId): Success/Failure
```

#### 2. Account Query Service
```
- getAccountSummary(accountId): AccountSummary
- getAccountStatus(accountId): AccountStatus
- getAccountsByCustomer(customerId): AccountList
- getAccountsByCenter(centerCode): AccountList
- validateAccountExists(accountId): Boolean
```

#### 3. Account Lifecycle Service
```
- archiveAccount(accountId): Success/Failure
- reopenAccount(accountId): Success/Failure
- getArchivedAccounts(searchCriteria): AccountList
```

### Consumed Services

This unit consumes services from:

#### From Reference Data Management Unit:
- `getReferenceData(groupNumber)`: Get dropdown values from RLSSYS
- `getAccountTypes()`: Get account types from RLSACCT
- `getEconomicActivities()`: Get economic activities from RLSECON
- `getCenters()`: Get center codes from RLSCTR
- `validateReferenceCode(code, type)`: Validate reference data codes

#### From Compliance & Validation Unit:
- `validateMandatoryFields(accountData)`: Validate required fields
- `validateCrossFieldRules(accountData)`: Validate business rules
- `validateFieldFormats(accountData)`: Validate field formats
- `auditLog(action, userId, accountId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

#### From Customer Management Unit:
- `getCustomerInfo(customerId)`: Get customer details
- `validateCustomerExists(customerId)`: Validate customer

---

## Data Model

### Primary Tables

#### MANSERV.DBF (Main Account Table)
Key fields managed by this unit:
- **General Section**: REFNO, PREVREF, CRIBID, CUSNAME, NIDSS, LONGNAME
- **Loan Info Section**: CNT_CENTER, BUNIT, CORP, BOOKCDE, CNT_ECON, ORELDAT, CNT_STERM, CNT_MATD, CNT_ATYPE, CNT_PURP, CNT_FUND, CNT_PROG, AREA, REST, CNT_CRTYPE, CNT_MAT, CNT_CRPURP, CNT_REC, GUAR, CNT_GBY, LITIG, ITL, CNT_LSTAT, CNT_LPTYPE, RELCURR

### Reference Tables (Read-Only)
- **RLSSYS.DBF**: System reference data for dropdowns
- **RLSACCT.DBF**: Account types and GL mappings
- **RLSECON.DBF**: Economic activity codes
- **RLSCTR.DBF**: Center/budget unit codes

---

## Business Rules Summary

### Validation Rules
1. **Mandatory Fields**: Ref. No, Prev. Ref. No., Customer Name, Long Name
2. **Duplicate Reference Numbers**: Warning but allow override
3. **Date Validations**: Start of Term = Orig Release Date, Maturity Date > Start of Term
4. **Conditional Fields**: Purpose mandatory for specific Account Types (AA, AI, R, RDC, RDE, RDH)
5. **Deletion Rules**: Soft delete only, prevent if transaction history or active balances exist

### Auto-Population Rules
1. **Type of Credit**: Auto-populated based on Account Type and Status (cannot override)
2. **Purpose of Credit**: Auto-populated based on Account Type (cannot override)
3. **GL Account Codes**: Auto-populated from RLSACCT.DBF (can override)

### Status Transition Rules
1. **Current to Past Due**: Automatic after 90 days
2. **Litigation Status**: Manual only (separate Reclass Module)

### Access Control
1. **Roles**: User, Authorizer, Administrator
2. **Data Access**: By center/branch
3. **Create/Update**: User and Authorizer roles
4. **Delete**: Administrator role only

---

## Integration Points

### Dependencies
- **Reference Data Management Unit**: For dropdown values and validation
- **Compliance & Validation Unit**: For validation rules and audit logging
- **Customer Management Unit**: For customer information

### Consumers
- **Financial Management Unit**: Uses account data for balance and transaction management
- **Reporting & Analytics Unit**: Uses account data for reports
- **Collateral & GL Management Unit**: Uses account data for collateral and GL operations

---

## Implementation Notes

1. **Draft Save**: Allow users to save accounts with incomplete data as "Draft" status
2. **Audit Trail**: Log all create, update, delete operations with user ID and timestamp
3. **Soft Delete**: Mark accounts as deleted rather than physical deletion
4. **Search Performance**: Implement efficient indexing for search operations
5. **Currency Handling**: Use currency selection to determine which balance columns to use
6. **Reclass Module**: Create separate module for litigation reclassification

---

## Testing Considerations

1. **Unit Tests**: Test all CRUD operations, validation rules, and business logic
2. **Integration Tests**: Test integration with Reference Data, Compliance, and Customer Management units
3. **Access Control Tests**: Verify role-based access and center/branch restrictions
4. **Performance Tests**: Test search performance with large datasets
5. **Edge Cases**: Test duplicate reference numbers, date validations, conditional fields

---

**Status**: Ready for Implementation
**Next Steps**: Begin technical design and implementation planning
