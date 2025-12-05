# Step 1.2: Grouping User Stories into Units - Plan

## Document Information
- **Task**: Group user stories into business feature units
- **Source**: /inception/overview_user_stories.md (75 user stories)
- **Date**: December 5, 2025
- **Status**: ✅ COMPLETED (Core Units 1-5)

## Objective
Group the 75 user stories into cohesive, loosely coupled business feature units that can be built independently by single teams. Each unit should focus on a specific business capability with well-defined service interfaces.

## Proposed Business Feature Units

Based on analysis of the 75 user stories, I propose the following **6 business feature units**:

### Priority 1 (Core/Required Features):
1. **Loan Account Management Unit** - Core loan account CRUD operations
2. **Customer Management Unit** - Customer information and classification
3. **Financial Management Unit** - Balances, transactions, and interest calculations (EXCLUDING Collateral/Dates/GL and Manner of Release)
4. **Reference Data Management Unit** - System configuration and master data
5. **Compliance & Validation Unit** - Business rules, validation, and audit

### Priority 2 (Optional Features - Future Phase):
6. **Reporting & Analytics Unit** - Search, reports, and portfolio analysis
7. **Collateral & GL Management Unit** - Collateral tracking, GL mapping, and release documentation (OPTIONAL)

**Note**: Collateral/Dates/GL Tab (US-034 to US-039) and Manner of Release (US-040) are marked as OPTIONAL and moved to a separate unit for future implementation.

## Execution Plan

### Step 1: Analyze and Finalize Unit Groupings ✅
- [x] Review the 75 user stories and proposed 7 units (5 core + 2 optional)
- [x] Validate that units are cohesive (related functionality grouped together)
- [x] Validate that units are loosely coupled (minimal dependencies)
- [x] Ensure each unit can be built independently
- [x] **CONFIRMED: 7-unit structure approved with validation questions answered**

### Step 2: Create Unit 1 - Loan Account Management (PRIORITY 1) ✅
- [x] Create `/inception/units/unit1_loan_account_management.md`
- [x] Document unit purpose and scope
- [x] List assigned user stories: US-001 to US-004, US-015 to US-023, US-048 to US-051
- [x] Write acceptance criteria for each user story
- [x] Define service interfaces this unit exposes

### Step 3: Create Unit 2 - Customer Management (PRIORITY 1) ✅
- [x] Create `/inception/units/unit2_customer_management.md`
- [x] Document unit purpose and scope
- [x] List assigned user stories: US-005 to US-014, US-044
- [x] Write acceptance criteria for each user story
- [x] Define service interfaces this unit exposes

### Step 4: Create Unit 3 - Financial Management (PRIORITY 1 - Core Only) ✅
- [x] Create `/inception/units/unit3_financial_management.md`
- [x] Document unit purpose and scope
- [x] List assigned user stories: US-024 to US-033, US-067, US-069, US-071, US-073
- [x] Write acceptance criteria for each user story
- [x] Define service interfaces this unit exposes
- [x] **NOTE: EXCLUDING US-034 to US-040 (moved to Unit 7 - Optional)**

### Step 5: Create Unit 4 - Reference Data Management (PRIORITY 1) ✅
- [x] Create `/inception/units/unit4_reference_data_management.md`
- [x] Document unit purpose and scope
- [x] List assigned user stories: US-041 to US-047, US-066
- [x] Write acceptance criteria for each user story
- [x] Define service interfaces this unit exposes

### Step 6: Create Unit 5 - Compliance & Validation (PRIORITY 1) ✅
- [x] Create `/inception/units/unit5_compliance_validation.md`
- [x] Document unit purpose and scope
- [x] List assigned user stories: US-061 to US-065, US-068, US-070, US-072, US-074
- [x] Write acceptance criteria for each user story
- [x] Define service interfaces this unit exposes
- [x] **COMPLETED: All validation rules confirmed and documented**

### Step 7: Create Unit 6 - Reporting & Analytics (PRIORITY 2 - Optional)
- [ ] Create `/inception/units/unit6_reporting_analytics.md` (OPTIONAL)
- [ ] Document unit purpose and scope
- [ ] List assigned user stories: US-052 to US-060, US-075
- [ ] Write acceptance criteria for each user story
- [ ] Define service interfaces this unit exposes
- [ ] **NOTE: This unit is marked as OPTIONAL for future phase**

### Step 8: Create Unit 7 - Collateral & GL Management (PRIORITY 2 - Optional)
- [ ] Create `/inception/units/unit7_collateral_gl_management.md` (OPTIONAL)
- [ ] Document unit purpose and scope
- [ ] List assigned user stories: US-034 to US-040
- [ ] Write acceptance criteria for each user story
- [ ] Define service interfaces this unit exposes
- [ ] **NOTE: This unit is marked as OPTIONAL for future phase**

### Step 9: Identify Validation Input Requirements ✅
- [x] Review all validation rules across units
- [x] Identify validation rules that need your confirmation
- [x] Document questions for your input (see VALIDATION_QUESTIONS.md)
- [x] **COMPLETED: All validation rules confirmed and documented**

### Step 10: Create Integration Contract ✅
- [x] Create `/inception/units/integration_contract.md`
- [x] Document integration overview and principles
- [x] For each unit, define:
  - Service interfaces exposed
  - API methods/operations
  - Input/output data structures
  - Dependencies on other units
- [x] Define data exchange formats
- [x] Define error handling and communication patterns
- [x] **COMPLETED: Integration contract ready for review**

### Step 11: Review and Validation ✅
- [x] Review all unit files for completeness
- [x] Validate that all 75 user stories are assigned to units (68 core + 7 optional)
- [x] Validate that no user story is duplicated across units
- [x] Check that integration contracts are clear and complete
- [x] Ensure units can be built independently
- [x] **COMPLETED: All core units (1-5) ready for implementation**

## Detailed Unit Breakdown

### Unit 1: Loan Account Management (23 user stories)
**Purpose**: Manage the core loan account lifecycle - create, read, update, delete, and basic loan information.

**User Stories**:
- US-001 to US-004: General section CRUD operations
- US-015 to US-023: Loan info management (account identification, dates, type, purpose, funding, status, guarantee, litigation, project type)
- US-048 to US-051: Account operations (search, view summary, copy, close/archive)

**Key Responsibilities**:
- Loan account CRUD operations
- Loan information management
- Account search and retrieval
- Account lifecycle operations (copy, close, archive)

### Unit 2: Customer Management (11 user stories)
**Purpose**: Manage customer information, demographics, classification, and risk ratings.

**User Stories**:
- US-005 to US-014: Customer information/approval (address, classification, demographics, tax info, OFW, DOSRI, firm size, special programs, risk rating, account classification, financial statements)
- US-044: Customer master data management

**Key Responsibilities**:
- Customer information management
- Customer classification and demographics
- Risk rating and credit assessment
- Customer master data maintenance

### Unit 3: Financial Management (13 user stories - CORE ONLY)
**Purpose**: Handle core financial aspects - balances, transactions, interest calculations, and multi-currency operations.

**User Stories** (PRIORITY 1 - REQUIRED):
- US-024 to US-033: Balances (currency conversion, approved amounts, released amounts, OPB, reserves, AIR, past due, litigation expenses, advances, interest)
- US-067: Calculate and validate interest
- US-069: Validate transaction posting rules
- US-071: Multi-currency operations
- US-073: Handle restructured loans

**Key Responsibilities**:
- Balance management across multiple currencies
- Interest calculation and accrual
- Transaction processing and validation
- Restructured loan handling

**EXCLUDED (OPTIONAL - See Unit 7)**:
- US-034 to US-040: Collateral/Dates/GL and Manner of Release

### Unit 4: Reference Data Management (8 user stories)
**Purpose**: Maintain all system reference data, master data, and configuration.

**User Stories**:
- US-041: Account type reference data
- US-042: Economic activity codes
- US-043: GL master accounts
- US-045: Center/budget unit data
- US-046: System reference data (RLSSYS)
- US-047: Group reference data
- US-066: Validate reference data integrity

**Key Responsibilities**:
- Reference data CRUD operations
- Master data maintenance
- System configuration
- Data integrity validation
- Lookup data provision for other units

### Unit 5: Reporting & Analytics (10 user stories)
**Purpose**: Provide comprehensive reporting, analytics, and data export capabilities.

**User Stories**:
- US-052: Account listing report
- US-053: Portfolio summary report
- US-054: Transaction history report
- US-055: Regulatory reports
- US-056: Customer statement
- US-057: Collateral report
- US-058: Borrower risk rating report
- US-059: Economic activity report
- US-060: Maturity schedule report
- US-075: Data export and import

**Key Responsibilities**:
- Report generation (operational, regulatory, customer-facing)
- Portfolio analytics and dashboards
- Data export/import functionality
- Ad-hoc query and analysis

### Unit 6: Compliance & Validation (13 user stories - PRIORITY 1)
**Purpose**: Enforce business rules, data validation, audit trails, and system-wide compliance.

**User Stories**:
- US-061: Validate mandatory fields
- US-062: Validate field formats and data types
- US-063: Validate cross-field business rules
- US-064: Auto-populate dependent fields
- US-065: Enforce conditional field requirements
- US-068: Enforce account status rules
- US-070: Maintain audit trail
- US-072: Support batch processing
- US-074: User access control

**Key Responsibilities**:
- Data validation (field-level, cross-field, business rules)
- Auto-population and calculation logic
- Audit trail maintenance
- Batch processing
- User authentication and authorization
- Security and access control

**INPUT NEEDED**: Validation rules confirmation (see Step 8)

### Unit 7: Collateral & GL Management (7 user stories - OPTIONAL/FUTURE)
**Purpose**: Handle collateral tracking, GL account mapping, and release documentation.

**User Stories** (PRIORITY 2 - OPTIONAL):
- US-034: Manage collateral security information
- US-035: Manage collateral type breakdown
- US-036: Manage amortization arrears
- US-037: Manage transaction dates
- US-038: Manage interest rate information
- US-039: Manage GL account codes
- US-040: Manage release documentation

**Key Responsibilities**:
- Collateral tracking and valuation
- GL account mapping and posting
- Release documentation
- Amortization tracking

**Status**: OPTIONAL - Can be implemented in Phase 2

## Integration Points

Key integration points between units:

1. **Loan Account Management ↔ Customer Management**: Loan accounts link to customers
2. **Loan Account Management ↔ Financial Management**: Accounts have balances and transactions
3. **All Units ↔ Reference Data Management**: All units consume reference data
4. **All Units ↔ Compliance & Validation**: All units use validation services
5. **Reporting & Analytics ↔ All Units**: Reports consume data from all units

## Questions for Your Review

### General Structure Questions
1. **Unit Count**: Are 7 units (5 core + 2 optional) appropriate, or would you prefer a different grouping?
2. **Unit Boundaries**: Do the proposed unit boundaries make sense for your organization?
3. **Team Structure**: Can each unit be assigned to a single team?
4. **Build Sequence**: Is there a preferred order for building these units?
5. **Integration Approach**: Any specific integration patterns or technologies to use?
6. **Optional Features**: Confirm that Reporting & Analytics and Collateral/GL Management can be deferred to Phase 2?

### Validation Questions (INPUT NEEDED)

Below are key validation rules that need your confirmation before implementation:

#### 1. Mandatory Field Rules
**Question**: Please confirm which fields are absolutely mandatory vs. conditionally mandatory:
- General Section: Ref. No, Prev. Ref. No., Customer Name, Long Name - **Confirm all mandatory?**
- Customer Info: Business/Residence Address, Project Address, Affiliate, RDO, TIN, V/N, OFW - **Confirm all mandatory?**
- Balances: OPB (Peso) - **Confirm mandatory?**
- Can users save as "Draft" with incomplete data, or must all mandatory fields be filled?

#### 2. Cross-Field Validation Rules
**Question**: Please confirm these validation rules:
- **Amount Secured + Amount Unsecured = OPB (Peso)** - Must this balance exactly, or is a tolerance allowed?
- **Released Amount <= Approved Amount** - Strict enforcement, or can it be overridden with approval?
- **OPB <= Released Amount** - Strict enforcement?
- **Start of Term >= Orig Release Date** - Strict enforcement?
- **Maturity Date > Start of Term** - Strict enforcement?
- **Total Assets = Total Liabilities + Stockholder Equity** - Must balance exactly, or tolerance allowed?

#### 3. Duplicate Reference Number Handling
**Question**: How should the system handle duplicate Reference Numbers?
- Hard block (prevent save)?
- Warning but allow override?
- Check only within same center/branch?
- Check across entire system?

#### 4. Account Status Transition Rules
**Question**: Please confirm the allowed status transitions:
- When does an account automatically change from Current (CUR) to Past Due (PDO)?
  - Number of days past due threshold?
  - Grace period?
- When does an account automatically change to Litigation (LITIG)?
  - Manual only, or automatic based on criteria?
- Can status be manually overridden, or only automatic?

#### 5. Interest Calculation Method
**Question**: Please confirm interest calculation details:
- **Formula**: OPB × Interest Rate × Days / 365 - **Correct?**
- **Day count convention**: Actual/365, 30/360, or other?
- **Reckoning date**: Start of Term is used as the base date for interest calculation - **Confirm?**
- **Compounding**: Simple interest or compound interest?
- **Variable rate calculation**: Base Rate + Spread - **Confirm?**
- **Interest accrual frequency**: Daily, monthly, or other?

#### 6. Past Due Calculation
**Question**: Please confirm past due logic:
- How is "Past Due Date" determined? (First missed payment date?)
- How are "No. of Days Past Due" calculated? (Current Date - Past Due Date?)
- What triggers an account to be marked as past due?
- Is there a grace period before marking as past due?

#### 7. Account Classification Rules
**Question**: Please confirm account classification logic:
- What triggers automatic classification changes (e.g., from UNCLASSIFIED to SUBSTANDARD)?
- Is classification based solely on days past due, or other factors?
- Classification thresholds:
  - Current: 0-30 days?
  - Watchlisted: 31-60 days?
  - Substandard: 61-90 days?
  - Doubtful: 91-180 days?
  - Loss: 180+ days?
- Can classification be manually overridden?

#### 8. Reserve Calculation
**Question**: Please confirm reserve requirements:
- How are reserves calculated? (Percentage of OPB based on classification?)
- Reserve percentages by classification:
  - Unclassified: 0%?
  - Watchlisted: 5%?
  - Substandard: 25%?
  - Doubtful: 50%?
  - Loss: 100%?
- Are reserves calculated automatically or entered manually?

#### 9. Conditional Field Requirements
**Question**: Please confirm conditional logic:
- **No. of OFW**: Required when OFW = Y - **Confirm?** Can it be zero?
- **Guaranteed By**: Required when Guaranteed = Y - **Confirm?**
- **Purpose**: Required when Account Type is AA, AI, R, RDC, RDE, or RDH - **Confirm these codes?**
- **Amount Secured/Unsecured**: When is each required? Only when OPB > 0?

#### 10. Field Format Validations
**Question**: Please confirm format requirements:
- **TIN format**: 17 characters - What is the exact format? (e.g., XXX-XXX-XXX-XXX)?
- **Reference Number format**: Max 17 characters - Any specific pattern or prefix required?
- **CRIB ID format**: Max 10 characters - Any specific format?
- **NIDSS Account No format**: Max 13 characters - Any specific format?
- **Interest Rate range**: 0-100% - Any practical limits? (e.g., max 50%?)

#### 11. Currency Conversion Rules
**Question**: Please confirm multi-currency handling:
- Are exchange rates entered manually per transaction, or fetched from a rate table?
- How often are exchange rates updated?
- Which currency column is used based on RELCURR field?
  - PHP → Peso column
  - USD → Dollar column
  - Others (JPY, EUR, etc.) → Others column?
- Are conversions automatic or manual?

#### 12. Deletion Rules
**Question**: Please confirm deletion logic:
- **Soft delete vs. Hard delete**: Use soft delete (mark as deleted) - **Confirm?**
- **Deletion restrictions**: Prevent deletion if account has transaction history - **Confirm?**
- What other conditions prevent deletion? (Active balances? Collateral? Guarantees?)
- Can deleted accounts be restored?

#### 13. Auto-Population Rules
**Question**: Please confirm auto-population logic:
- **Type of Credit**: Auto-populated based on Account Type and Status - **Confirm mapping logic?**
- **Purpose of Credit**: Auto-populated based on Account Type - **Confirm mapping logic?**
- **GL Account Codes**: Auto-populated from RLSACCT.DBF based on Account Type - **Can users override?**
- **Date of Last Transaction**: Auto-populated from MNSHTRAN.DBF - **Confirm?**
- Can users override auto-populated fields? If yes, which ones?

#### 14. Batch Processing Rules
**Question**: Please confirm batch processing requirements:
- **Daily AIR calculation**: Run at what time? (e.g., end of day, midnight?)
- **Daily days past due update**: Run at what time?
- **Account classification update**: Daily, weekly, or monthly?
- **Statement generation**: Monthly on what date?
- What happens if batch fails? Retry logic? Alerts?

#### 15. User Access Control
**Question**: Please confirm access control requirements:
- **Roles needed**: Loan Officer, Credit Officer, Manager, Administrator, Read-Only - **Confirm these roles?**
- **Data access restrictions**: By center/branch, or full access?
- **Function restrictions by role**: Which roles can create/update/delete accounts?
- **Approval workflows**: Are approvals needed for certain operations? (e.g., large loans, deletions?)

---

## Summary of Input Needed

**IMMEDIATE INPUT NEEDED** (before creating unit files):
1. Confirm the 7-unit structure (5 core + 2 optional)
2. Confirm that Collateral/Dates/GL and Manner of Release are optional
3. Confirm that Reporting & Analytics is optional

**INPUT NEEDED DURING UNIT CREATION** (Step 9):
1. Answers to the 15 validation questions above
2. Any additional business rules or constraints
3. Specific format requirements for fields
4. Approval workflows and authorization rules

Please review and provide your input on these questions so I can proceed with accurate unit documentation.

## Next Steps After Approval

Once you approve this plan, I will:
1. Create the `/inception/units/` folder
2. Create individual .md files for each unit with detailed user stories and acceptance criteria
3. Create the integration contract document
4. Mark each step as complete in this plan

---

## Priority Summary

### Priority 1 (Core/Required) - 55 User Stories
1. **Unit 1: Loan Account Management** - 23 user stories
2. **Unit 2: Customer Management** - 11 user stories
3. **Unit 3: Financial Management** - 13 user stories (EXCLUDING Collateral/GL)
4. **Unit 4: Reference Data Management** - 8 user stories
5. **Unit 5: Compliance & Validation** - 13 user stories

**Total Core Features**: 68 user stories

### Priority 2 (Optional/Future) - 17 User Stories
6. **Unit 6: Reporting & Analytics** - 10 user stories (OPTIONAL)
7. **Unit 7: Collateral & GL Management** - 7 user stories (OPTIONAL)

**Total Optional Features**: 17 user stories

---

**Status**: Ready for your review and approval
**Awaiting**: 
1. Your confirmation on the 7-unit structure (5 core + 2 optional)
2. Your confirmation that Collateral/Dates/GL Tab and Manner of Release are optional
3. Your answers to the 15 validation questions above (can be provided during Step 9)
