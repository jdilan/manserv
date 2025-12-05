# Unit 2: Customer Management

## Unit Information
- **Unit Name**: Customer Management
- **Priority**: 1 (Core/Required)
- **Team Assignment**: Single team
- **Dependencies**: Reference Data Management Unit, Compliance & Validation Unit
- **Status**: Ready for Implementation

## Purpose and Scope

This unit manages all customer-related information including demographics, classification, risk ratings, financial statements, and regulatory compliance data. It provides a centralized customer master data repository that supports loan account management and regulatory reporting.

## Business Capabilities

1. **Customer Information Management**: Manage customer addresses, contact information, and basic demographics
2. **Customer Classification**: Classify customers by type, size, nationality, and other regulatory categories
3. **Risk Assessment**: Manage borrower risk ratings and credit assessments
4. **Financial Analysis**: Track customer financial statements and ratios
5. **Regulatory Compliance**: Manage tax information, DOSRI classification, and special program indicators
6. **Customer Master Data**: Maintain comprehensive customer master records

## Assigned User Stories (11 total)

### Customer Information/Approval Section (10 user stories)

#### US-005: Manage Customer Address Information
**As a** loan officer  
**I want to** enter and manage customer address information  
**So that** I can maintain accurate contact details for the borrower

**Acceptance Criteria:**
- User can access Customer Information/Approval tab
- User can enter Business/Residence Address - mandatory field, max 80 characters
- User can enter Project Address - mandatory field, max 80 characters
- User can select Preferred Mailing Address - radio button with options: Business/Residence (1) or Project (2)
- System stores addresses in MANSERV.CUS_BADDR and MANSERV.CUS_PADDR fields
- System stores mailing preference in MANSERV.CUS_MAIL field
- System validates that at least one address is provided
- System allows multi-line text entry for addresses
- User can save as "Draft" with incomplete data

**Validation Rules**:
- Business/Residence Address: Mandatory, max 80 characters
- Project Address: Mandatory, max 80 characters
- At least one address must be provided

---

#### US-006: Manage Customer Classification and Demographics
**As a** loan officer  
**I want to** enter customer classification and demographic information  
**So that** I can properly categorize the borrower

**Acceptance Criteria:**
- User can select Affiliate from dropdown - mandatory field, populated from distinct CUS_NM in MANSERV table
- User can select Location from dropdown - populated from RLSSYS where SYS_GNO = '0002', max 4 characters
- User can select Type of Borrower from dropdown - populated from RLSSYS where SYS_GNO = '0006'
- User can select New Type of Borrower from dropdown - populated from RLSSYS where SYS_GNO = '0016'
- User can select Sex from dropdown with options: 1-Male, 2-Female, 3-N/A
- User can select Nationality from dropdown - populated from RLSSYS where SYS_GNO = '0007'
- User can select Group Name from dropdown - populated from RLSGROUP table (GRP_NAME, GRP_ID)
- System stores all selections in appropriate MANSERV fields
- All dropdowns display user-friendly titles (SYS_UTITLE) but store codes (SYS_UCODE)

**Validation Rules**:
- Affiliate: Mandatory

---

#### US-007: Manage Tax and Regulatory Information
**As a** loan officer  
**I want to** enter tax and regulatory information for the customer  
**So that** I can ensure compliance with tax regulations

**Acceptance Criteria:**
- User can select RDO (Revenue District Office) from dropdown - mandatory field, populated from RLSSYS where SYS_GNO = '0040'
- User can enter TIN (Tax Identification Number) - mandatory field, max 17 characters
- User can select V/N (VATable/Non-VAT) - mandatory field, single character: V or N
- User can select LGU Salary Loan indicator - Y/N field
- System validates TIN format (standard format, no specific pattern)
- System stores RDO in MANSERV.CUS_RDO field
- System stores TIN in MANSERV.CUS_TIN field
- System stores VAT indicator in MANSERV.CUS_VAT field
- System stores LGU Salary Loan in MANSERV.CNT_LGUSS field

**Validation Rules**:
- RDO: Mandatory
- TIN: Mandatory, max 17 characters, standard format
- V/N: Mandatory, single character (V or N)

---

#### US-008: Manage OFW (Overseas Foreign Worker) Information
**As a** loan officer  
**I want to** record OFW status and count  
**So that** I can track loans to overseas workers

**Acceptance Criteria:**
- User can select OFW indicator - mandatory field, Y/N
- User can enter No. of OFW - numeric field, max 3 digits
- No. of OFW field is enabled only when OFW = Y
- No. of OFW field is mandatory when OFW = Y and cannot be zero
- No. of OFW field is disabled and cleared when OFW = N
- System stores OFW indicator in MANSERV.CNT_OFW field
- System stores OFW count in MANSERV.CNT_OFWN field
- System validates that No. of OFW is a positive integer (> 0)

**Validation Rules**:
- OFW: Mandatory
- No. of OFW: Required when OFW = Y, must be > 0

---

#### US-009: Manage DOSRI Classification
**As a** loan officer  
**I want to** classify borrowers according to DOSRI regulations  
**So that** I can comply with banking regulations on related party transactions

**Acceptance Criteria:**
- User can select DOSRI from dropdown - populated from RLSSYS where SYS_GNO = '0008'
- Dropdown includes options: DIR (Director), OFF (Officer), RIN (Related Interest), STK (Stockholder), N/A (Not Applicable)
- System stores selection in MANSERV.CUS_DOSRI field (max 3 characters)
- System displays warning message when DOSRI is not N/A
- System applies special validation rules for DOSRI accounts per banking regulations

---

#### US-010: Manage Firm Size Classification
**As a** loan officer  
**I want to** classify the borrower's firm size  
**So that** I can apply appropriate lending policies

**Acceptance Criteria:**
- User can select Size of Firm from dropdown - populated from RLSSYS where SYS_GNO = '0004'
- Dropdown includes options:
  - 1: MICRO (UP TO 3M)
  - 3: SMALL-SCALE INDUSTRIES (ABOVE 3M TO 15M)
  - 4: MEDIUM-SCALE INDUSTRIES (ABOVE 15M-100M)
  - 5: LARGE-SCALE INDUSTRIES (ABOVE 100M)
- System stores selection in MANSERV.CUS_SIZE field (1 character)
- System uses firm size for credit limit calculations and reporting

---

#### US-011: Manage Special Program Indicators
**As a** loan officer  
**I want to** mark accounts for special lending programs  
**So that** I can track participation in government and special programs

**Acceptance Criteria:**
- User can select Sulong indicator - Y/N field
- User can select BMBE (Barangay Micro Business Enterprise) indicator - Y/N field
- System stores Sulong in MANSERV.CNT_SULONG field
- System stores BMBE in MANSERV.CNT_BMBE field
- System applies special interest rates or terms based on program indicators
- System includes these accounts in program-specific reports

---

#### US-012: Manage Borrower Risk Rating
**As a** loan officer  
**I want to** assign and track borrower risk ratings  
**So that** I can assess credit risk and set appropriate terms

**Acceptance Criteria:**
- User can select BRR/CAMP (Branch Risk Rating) from dropdown - mandatory field, populated from RLSSYS where SYS_GNO = '0068'
- Dropdown includes ratings from N/A to 15 (Loss)
- User can enter Worksheet Date - date field for rating date
- System stores BRR in MANSERV.CUS_BRR field (max 3 characters)
- System stores worksheet date in MANSERV.CUS_BRRWDT field
- System validates that worksheet date is not in the future
- System tracks rating history in MNSHBRR.DBF table
- System displays rating description alongside code

**Validation Rules**:
- BRR/CAMP: Mandatory
- Worksheet Date: Cannot be in the future

---

#### US-013: Manage Account Classification
**As a** loan officer  
**I want to** classify accounts by performance and interest category  
**So that** I can apply appropriate provisioning and pricing

**Acceptance Criteria:**
- User can select Account Class from dropdown - populated from RLSSYS where SYS_GNO = '0003'
- Dropdown includes classifications:
  - 1I: UNCLASSIFIED/CURRENT
  - 2IW: WATCHLISTED
  - 3IA: SPECIALLY MENTIONED
  - 4II-6II: SUBSTANDARD (various)
  - 7III: DOUBTFUL
  - 8IV: LOSS
- User can select Account Tag from dropdown - populated from RLSSYS where SYS_GNO = '0015'
- Account Tag includes: A, AA, AAA, AAAA, N/A, NP (Non Prime)
- System stores Account Class in MANSERV.CNT_ACLASS field
- System stores Account Tag in MANSERV.CNT_ATAG field
- System applies appropriate reserve requirements based on classification
- System uses Account Tag for interest rate determination
- Classification can be manually overridden
- Classification changes triggered by: non-payment, number of days past due, and upon payment
- Classification thresholds:
  - Current: 0-30 days
  - Watchlisted: 31-60 days
  - Substandard: 61-90 days
  - Doubtful: 91-180 days
  - Loss: 180+ days

**Business Rules**:
- Classification based on days past due only
- Classification can be manually overridden
- Automatic classification changes triggered by: non-payment, days past due, and payment

---

#### US-014: Manage Financial Statement Information
**As a** loan officer  
**I want to** record borrower financial statement data  
**So that** I can assess creditworthiness and financial capacity

**Acceptance Criteria:**
- User can enter Total Assets - numeric field, 15 digits with 2 decimal places
- User can enter Total Liabilities - numeric field, 15 digits with 2 decimal places
- User can enter Stockholder Equity - numeric field, 15 digits with 2 decimal places
- User can enter Gross Revenue - numeric field, 15 digits with 2 decimal places
- User can enter Total Expenses - numeric field, 15 digits with 2 decimal places
- User can enter Interest Expense - numeric field, 15 digits with 2 decimal places
- User can enter Net Inc/Loss - numeric field, 15 digits with 2 decimal places
- User can enter Date Audited - date field
- System stores values in MANSERV fields: TOTASSET, TOTLIABS, STCKHLEQ, GROSSREV, TOTEXPNS, INTEXPNS, NETINCOM, FINSDTE
- System validates that Total Assets = Total Liabilities + Stockholder Equity (OPTIONAL - not required to balance)
- System calculates financial ratios for credit analysis
- System formats currency fields with appropriate separators

**Validation Rules**:
- Total Assets = Total Liabilities + Stockholder Equity is OPTIONAL (not enforced)

---

### Customer Master Data Management (1 user story)

#### US-044: Manage Customer Master Data
**As a** loan officer  
**I want to** maintain comprehensive customer information  
**So that** I can have a single source of truth for customer data

**Acceptance Criteria:**
- User can access Customer management interface
- User can create new customer with borrower number (10 characters)
- User can enter all customer fields from RLSCUST.DBF structure (41 fields)
- System validates unique borrower numbers
- System validates CRIB ID format (no specific format required)
- System validates TIN format (standard format)
- System stores financial information with proper data types
- System links customer to loan accounts
- User can search customers by multiple criteria
- System prevents deletion of customers with active loans
- System maintains customer history and changes
- System supports customer merge functionality
- Access restricted by center/branch

**Validation Rules**:
- Borrower Number: Unique, 10 characters
- CRIB ID: No specific format
- TIN: Standard format, max 17 characters

---

## Service Interfaces

### Exposed Services

This unit exposes the following service interfaces for consumption by other units:

#### 1. Customer Management Service
```
- createCustomer(customerData): CustomerId
- getCustomer(customerId): CustomerDetails
- updateCustomer(customerId, customerData): Success/Failure
- deleteCustomer(customerId): Success/Failure
- searchCustomers(searchCriteria): CustomerList
- mergeCustomers(sourceCustomerId, targetCustomerId): Success/Failure
```

#### 2. Customer Query Service
```
- getCustomerByBorrowerNumber(borrowerNumber): CustomerDetails
- getCustomersByName(name): CustomerList
- getCustomersByTIN(tin): CustomerList
- getCustomersByCRIBID(cribId): CustomerList
- validateCustomerExists(customerId): Boolean
```

#### 3. Customer Classification Service
```
- getCustomerRiskRating(customerId): RiskRating
- updateRiskRating(customerId, rating, worksheetDate): Success/Failure
- getCustomerClassification(customerId): Classification
- updateClassification(customerId, classification): Success/Failure
- getRiskRatingHistory(customerId): RatingHistory
```

#### 4. Customer Financial Service
```
- getFinancialStatements(customerId): FinancialData
- updateFinancialStatements(customerId, financialData): Success/Failure
- calculateFinancialRatios(customerId): FinancialRatios
```

### Consumed Services

This unit consumes services from:

#### From Reference Data Management Unit:
- `getReferenceData(groupNumber)`: Get dropdown values from RLSSYS
- `getGroupList()`: Get group names from RLSGROUP
- `validateReferenceCode(code, type)`: Validate reference data codes

#### From Compliance & Validation Unit:
- `validateMandatoryFields(customerData)`: Validate required fields
- `validateFieldFormats(customerData)`: Validate field formats
- `auditLog(action, userId, customerId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

---

## Data Model

### Primary Tables

#### MANSERV.DBF (Customer Fields)
Key fields managed by this unit:
- **Address**: CUS_BADDR, CUS_PADDR, CUS_MAIL
- **Classification**: CUS_NM (Affiliate), CUS_LOC, CUS_TYPE, CUS_NTYPE, CUS_SEX, CUS_NAT
- **Tax/Regulatory**: CUS_RDO, CUS_TIN, CUS_VAT, CNT_LGUSS
- **OFW**: CNT_OFW, CNT_OFWN
- **DOSRI**: CUS_DOSRI
- **Firm Size**: CUS_SIZE
- **Special Programs**: CNT_SULONG, CNT_BMBE
- **Risk Rating**: CUS_BRR, CUS_BRRWDT
- **Classification**: CNT_ACLASS, CNT_ATAG
- **Financial**: TOTASSET, TOTLIABS, STCKHLEQ, GROSSREV, TOTEXPNS, INTEXPNS, NETINCOM, FINSDTE

#### RLSCUST.DBF (Customer Master Table)
Comprehensive customer master data (41 fields)

#### MNSHBRR.DBF (Risk Rating History Table)
Tracks borrower risk rating changes over time

### Reference Tables (Read-Only)
- **RLSSYS.DBF**: System reference data for dropdowns
- **RLSGROUP.DBF**: Customer group definitions

---

## Business Rules Summary

### Validation Rules
1. **Mandatory Fields**: 
   - Address: Business/Residence Address, Project Address
   - Classification: Affiliate
   - Tax: RDO, TIN, V/N
   - OFW: OFW indicator
   - Risk Rating: BRR/CAMP
2. **Conditional Fields**:
   - No. of OFW: Required when OFW = Y, must be > 0
3. **Format Validations**:
   - TIN: Max 17 characters, standard format
   - Borrower Number: 10 characters, unique
4. **Date Validations**:
   - Worksheet Date: Cannot be in the future
5. **Financial Statement**:
   - Total Assets = Total Liabilities + Stockholder Equity is OPTIONAL

### Classification Rules
1. **Account Classification Thresholds**:
   - Current: 0-30 days past due
   - Watchlisted: 31-60 days past due
   - Substandard: 61-90 days past due
   - Doubtful: 91-180 days past due
   - Loss: 180+ days past due
2. **Classification Triggers**: Non-payment, days past due, payment received
3. **Manual Override**: Classification can be manually overridden

### Reserve Requirements
Based on Account Classification:
- Unclassified: 0%
- Watchlisted: 5%
- Substandard: 25%
- Doubtful: 50%
- Loss: 100%

**Note**: Reserves are entered manually (not auto-calculated)

### Access Control
1. **Roles**: User, Authorizer, Administrator
2. **Data Access**: By center/branch
3. **Create/Update**: User and Authorizer roles
4. **Delete**: Administrator role only (prevented if active loans exist)

---

## Integration Points

### Dependencies
- **Reference Data Management Unit**: For dropdown values and validation
- **Compliance & Validation Unit**: For validation rules and audit logging

### Consumers
- **Loan Account Management Unit**: Uses customer data for loan accounts
- **Financial Management Unit**: Uses customer classification for risk assessment
- **Reporting & Analytics Unit**: Uses customer data for reports

---

## Implementation Notes

1. **Draft Save**: Allow users to save customer data with incomplete information as "Draft" status
2. **Audit Trail**: Log all create, update, delete operations with user ID and timestamp
3. **Risk Rating History**: Maintain historical risk ratings in MNSHBRR.DBF table
4. **Customer Merge**: Support merging duplicate customer records
5. **DOSRI Warning**: Display warning when DOSRI is not N/A
6. **Financial Ratios**: Calculate and display key financial ratios for credit analysis
7. **Group Management**: Link customers to groups for related party tracking

---

## Testing Considerations

1. **Unit Tests**: Test all CRUD operations, validation rules, and business logic
2. **Integration Tests**: Test integration with Reference Data and Compliance units
3. **Access Control Tests**: Verify role-based access and center/branch restrictions
4. **Validation Tests**: Test mandatory fields, conditional fields, and format validations
5. **Classification Tests**: Test automatic classification changes and manual overrides
6. **Edge Cases**: Test OFW conditional logic, DOSRI warnings, financial statement validations

---

**Status**: Ready for Implementation
**Next Steps**: Begin technical design and implementation planning
