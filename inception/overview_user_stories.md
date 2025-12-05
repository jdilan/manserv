# Loan Account Management System - User Stories

## Document Information
- **Project**: Manserv Loan Account Management System
- **Version**: 1.0
- **Date**: December 5, 2025
- **Status**: Draft

## Table of Contents
1. [General Section User Stories](#general-section-user-stories)
2. [Customer Information/Approval User Stories](#customer-informationapproval-user-stories)
3. [Loan Info User Stories](#loan-info-user-stories)
4. [Balances User Stories](#balances-user-stories)
5. [Collateral/Dates/GL User Stories](#collateraldatesgl-user-stories)
6. [Manner of Release User Stories](#manner-of-release-user-stories)
7. [Reference Data Management User Stories](#reference-data-management-user-stories)
8. [Account Operations User Stories](#account-operations-user-stories)
9. [Search and Reporting User Stories](#search-and-reporting-user-stories)
10. [Data Validation and Business Rules User Stories](#data-validation-and-business-rules-user-stories)

---

## General Section User Stories

### US-001: Create New Loan Account with General Information
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
- System prevents duplicate Reference Numbers
- System stores data in MANSERV.DBF table with appropriate field mappings
- System displays validation error messages for invalid or missing mandatory data

### US-002: View Existing Loan Account General Information
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

### US-003: Update Loan Account General Information
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

### US-004: Delete Loan Account
**As a** loan officer with appropriate permissions  
**I want to** delete a loan account  
**So that** I can remove erroneous or cancelled accounts

**Acceptance Criteria:**
- User can select an existing account for deletion
- System displays confirmation dialog with account details before deletion
- System checks for dependencies (transactions, collateral, etc.) before allowing deletion
- System prevents deletion if account has transaction history
- System performs soft delete (marks as deleted) rather than hard delete
- System logs deletion action with user ID and timestamp
- System displays success message after deletion
- Deleted accounts are excluded from standard searches but can be viewed in audit reports

---

## Customer Information/Approval User Stories

### US-005: Manage Customer Address Information
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

### US-006: Manage Customer Classification and Demographics
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


### US-007: Manage Tax and Regulatory Information
**As a** loan officer  
**I want to** enter tax and regulatory information for the customer  
**So that** I can ensure compliance with tax regulations

**Acceptance Criteria:**
- User can select RDO (Revenue District Office) from dropdown - mandatory field, populated from RLSSYS where SYS_GNO = '0040'
- User can enter TIN (Tax Identification Number) - mandatory field, max 17 characters
- User can select V/N (VATable/Non-VAT) - mandatory field, single character: V or N
- User can select LGU Salary Loan indicator - Y/N field
- System validates TIN format
- System stores RDO in MANSERV.CUS_RDO field
- System stores TIN in MANSERV.CUS_TIN field
- System stores VAT indicator in MANSERV.CUS_VAT field
- System stores LGU Salary Loan in MANSERV.CNT_LGUSS field

### US-008: Manage OFW (Overseas Foreign Worker) Information
**As a** loan officer  
**I want to** record OFW status and count  
**So that** I can track loans to overseas workers

**Acceptance Criteria:**
- User can select OFW indicator - mandatory field, Y/N
- User can enter No. of OFW - numeric field, max 3 digits
- No. of OFW field is enabled only when OFW = Y
- No. of OFW field is mandatory when OFW = Y
- No. of OFW field is disabled and cleared when OFW = N
- System stores OFW indicator in MANSERV.CNT_OFW field
- System stores OFW count in MANSERV.CNT_OFWN field
- System validates that No. of OFW is a positive integer

### US-009: Manage DOSRI Classification
**As a** loan officer  
**I want to** classify borrowers according to DOSRI regulations  
**So that** I can comply with banking regulations on related party transactions

**Acceptance Criteria:**
- User can select DOSRI from dropdown - populated from RLSSYS where SYS_GNO = '0008'
- Dropdown includes options: DIR (Director), OFF (Officer), RIN (Related Interest), STK (Stockholder), N/A (Not Applicable)
- System stores selection in MANSERV.CUS_DOSRI field (max 3 characters)
- System displays warning message when DOSRI is not N/A
- System applies special validation rules for DOSRI accounts per banking regulations

### US-010: Manage Firm Size Classification
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

### US-011: Manage Special Program Indicators
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

### US-012: Manage Borrower Risk Rating
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

### US-013: Manage Account Classification
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

### US-014: Manage Financial Statement Information
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
- System validates that Total Assets = Total Liabilities + Stockholder Equity
- System calculates financial ratios for credit analysis
- System formats currency fields with appropriate separators

---

## Loan Info User Stories

### US-015: Manage Account Identification
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

### US-016: Manage Economic Activity Classification
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

### US-017: Manage Loan Dates
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
- System validates that Start of Term >= Orig Release Date
- System validates that Maturity Date > Start of Term
- System calculates loan term based on dates
- System uses Start of Term as reckoning date for interest computation

### US-018: Manage Account Type and Purpose
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

### US-019: Manage Funding Source and Program
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

### US-020: Manage Loan Status and Classification
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


### US-021: Manage Guarantee Information
**As a** loan officer  
**I want to** record guarantee information for the loan  
**So that** I can track guaranteed loans and their guarantors

**Acceptance Criteria:**
- User can select Guaranteed indicator - Y/N field
- User can select Guaranteed By from dropdown - conditional field
- Guaranteed By field is enabled only when Guaranteed = Y
- Guaranteed By field is mandatory when Guaranteed = Y
- Guaranteed By dropdown populated from RLSSYS where SYS_GNO = '0019'
- Options include: SBGFC, GFSME, PHILGUARANTEE
- System stores Guaranteed in MANSERV.GUAR field
- System stores Guaranteed By in MANSERV.CNT_GBY field
- System disables and clears Guaranteed By when Guaranteed = N
- System applies special reporting for guaranteed loans

### US-022: Manage Litigation Status
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

### US-023: Manage Loan Project Type and Currency
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
- System uses currency selection to determine which balance columns to use
- System applies different interest rates based on project type

---

## Balances User Stories

### US-024: Manage Currency Conversion Rates
**As a** loan officer  
**I want to** enter currency conversion rates  
**So that** I can accurately convert multi-currency loan amounts

**Acceptance Criteria:**
- User can access Balances tab
- User can enter "From Other Currency to USD" conversion rate - numeric field, 14 digits with 10 decimal places
- User can enter "From USD to PHP" conversion rate - numeric field, 14 digits with 10 decimal places
- System stores "Other to USD" rate in MANSERV.HIS_O2D field
- System stores "USD to PHP" rate in MANSERV.HIS_D2P field
- System validates that rates are positive numbers
- System uses rates for automatic currency conversions
- System displays current exchange rates as reference
- Rates are specific to each transaction date

### US-025: Manage Approved Amounts
**As a** loan officer  
**I want to** record approved loan amounts in multiple currencies  
**So that** I can track credit limits across different currencies

**Acceptance Criteria:**
- User can enter Approved Amount (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Approved Amount (USD) - numeric field, 15 digits with 2 decimal places
- User can enter Approved Amount (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.APPORIG, MANSERV.APPDOLR, MANSERV.APPPESO fields
- System validates that at least one approved amount is entered
- System formats amounts with currency symbols and thousand separators
- System uses approved amounts for credit limit checking
- System tracks approval date in MANSERV.CNT_APRDAT field

### US-026: Manage Released Amounts
**As a** loan officer  
**I want to** record released loan amounts per release memo  
**So that** I can track actual disbursements against approved amounts

**Acceptance Criteria:**
- User can enter Released Amount (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Released Amount (USD) - numeric field, 15 digits with 2 decimal places
- User can enter Released Amount (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.RELAMORIG, MANSERV.RELAMDOLR, MANSERV.RELAMPESO fields
- System validates that released amounts do not exceed approved amounts
- System updates released amounts with each release transaction
- System tracks cumulative releases across multiple release memos
- System validates that released amount matches currency selection

### US-027: Manage Outstanding Principal Balance (OPB)
**As a** loan officer  
**I want to** track outstanding principal balances in multiple currencies  
**So that** I can monitor loan exposure and calculate interest

**Acceptance Criteria:**
- User can enter OPB (Others) - numeric field, 15 digits with 2 decimal places
- User can enter OPB (USD) - numeric field, 15 digits with 2 decimal places
- User can enter OPB (Peso) - mandatory field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.OSORIG, MANSERV.OSDOLR, MANSERV.OSPESO fields
- System validates that OPB (Peso) = Amount Secured + Amount Unsecured
- System updates OPB automatically with principal payments and releases
- System uses OPB for interest calculations
- System validates that OPB does not exceed released amount
- OPB (Peso) is mandatory and cannot be blank

### US-028: Manage Reserve Amounts
**As a** loan officer  
**I want to** record reserve amounts for loan loss provisioning  
**So that** I can comply with regulatory reserve requirements

**Acceptance Criteria:**
- User can enter Reserves (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Reserves (Dollar) - numeric field, 15 digits with 2 decimal places
- User can enter Reserves (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.RESVORIG, MANSERV.RESVDOLR, MANSERV.RESVPESO fields
- System calculates required reserves based on account classification
- System validates that reserves meet minimum regulatory requirements
- System updates reserve GL accounts automatically
- System tracks reserve changes over time

### US-029: Manage Accrued Interest Receivable (AIR)
**As a** loan officer  
**I want to** track accrued interest receivable in multiple currencies  
**So that** I can properly account for interest income

**Acceptance Criteria:**
- User can enter AIR (Others) - numeric field, 15 digits with 2 decimal places
- User can enter AIR (USD) - numeric field, 15 digits with 2 decimal places
- User can enter AIR (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.AIRORIG, MANSERV.AIRDOLR, MANSERV.AIRPESO fields
- System calculates AIR automatically based on interest rate and days elapsed
- System updates AIR daily through batch processes
- System reverses AIR when interest is paid
- System tracks both regular AIR and restructured AIR (RAIR fields)

### US-030: Manage Past Due Amounts
**As a** loan officer  
**I want to** track actual past due amounts in multiple currencies  
**So that** I can monitor delinquent accounts

**Acceptance Criteria:**
- User can enter Actual Past Due (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Actual Past Due (Dollar) - numeric field, 15 digits with 2 decimal places
- User can enter Actual Past Due (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.PDORIG, MANSERV.PDDOLR, MANSERV.PDPESO fields
- System calculates past due amounts automatically based on payment schedule
- System updates past due status when payments are missed
- System uses past due amounts for account classification
- System tracks number of days past due in MANSERV.CNT_PDDAY field

### US-031: Manage Litigation Expenses
**As a** loan officer  
**I want to** record litigation expenses in multiple currencies  
**So that** I can track legal costs associated with problem loans

**Acceptance Criteria:**
- User can enter LT Exp (Others) - numeric field, 15 digits with 2 decimal places
- User can enter LT Exp (USD) - numeric field, 15 digits with 2 decimal places
- User can enter LT Exp (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in fields corresponding to litigation expenses
- System allows entry only when Under Litig = Y
- System tracks litigation expenses separately from principal and interest
- System updates litigation expense GL accounts
- System includes litigation expenses in total exposure calculations

### US-032: Manage Advances
**As a** loan officer  
**I want to** record borrower advances in multiple currencies  
**So that** I can track prepayments and advance payments

**Acceptance Criteria:**
- User can enter Advances (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Advances (USD) - numeric field, 15 digits with 2 decimal places
- User can enter Advances (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.ADVRORIG, MANSERV.ADVRDOLR, MANSERV.ADVRPESO fields
- System applies advances to future payments
- System tracks advance balances separately from principal
- System calculates interest on advances if applicable
- System allows application of advances to principal or interest

### US-033: Manage Interest Amounts
**As a** loan officer  
**I want to** track interest amounts in multiple currencies  
**So that** I can monitor interest income and receivables

**Acceptance Criteria:**
- User can enter Interest (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Interest (USD) - numeric field, 15 digits with 2 decimal places
- User can enter Interest (Peso) - numeric field, 15 digits with 2 decimal places
- System stores interest amounts in appropriate MANSERV fields
- System calculates interest based on interest rate and principal balance
- System tracks both paid and unpaid interest
- System distinguishes between regular interest and past due interest
- System updates interest income GL accounts

---

## Collateral/Dates/GL User Stories

### US-034: Manage Collateral Security Information
**As a** loan officer  
**I want to** record collateral security details  
**So that** I can track loan security and calculate risk exposure

**Acceptance Criteria:**
- User can access Collateral/Dates/GL tab
- User can select Security Code from dropdown - populated from RLSSYS where SYS_GNO = '0038'
- Security Code options include: 01 (Unsecured), 21-22 (REM), 31 (CHM), 41-51 (Holdout/Guarantee), 61-66 (Shares/Assignment), 90 (Others)
- User can enter Amount Secured - numeric field, 15 digits with 2 decimal places
- Amount Secured is mandatory if OPB is not blank and not equal to Amount Unsecured
- User can enter Amount Unsecured - numeric field, 15 digits with 2 decimal places
- Amount Unsecured is mandatory if OPB is not blank and not equal to Amount Secured
- System stores Security Code in MANSERV.SECCODE field (2 characters)
- System stores Amount Secured in MANSERV.AMTSEC field
- System stores Amount Unsecured in MANSERV.AMTUNSEC field
- System validates that Amount Secured + Amount Unsecured = OPB
- System displays validation error if amounts don't balance

### US-035: Manage Collateral Type Breakdown
**As a** loan officer  
**I want to** categorize collateral by type  
**So that** I can analyze collateral composition and risk

**Acceptance Criteria:**
- User can enter Real Estate - numeric field, 15 digits with 2 decimal places
- User can enter Machinery/Equipment - numeric field, 15 digits with 2 decimal places
- User can enter Others/Unsecured - numeric field, 15 digits with 2 decimal places
- User can enter Others/Secured - numeric field, 15 digits with 2 decimal places
- System stores collateral type amounts in MANSERV.CNT_COLA1, CNT_COLA2, CNT_COLA3, CNT_COLA4 fields
- System validates that sum of collateral types equals total collateral value
- System uses collateral types for risk weighting calculations
- System tracks collateral values over time for revaluation

### US-036: Manage Amortization Arrears
**As a** loan officer  
**I want to** track the number of amortizations in arrears  
**So that** I can monitor payment delinquency

**Acceptance Criteria:**
- User can enter No. of Amort in Arrears - numeric field, max 3 digits
- System stores value in MANSERV.CNT_ARREAR field
- System calculates arrears automatically based on payment schedule
- System updates arrears count when payments are missed or made
- System uses arrears count for account classification
- System displays arrears prominently for delinquent accounts


### US-037: Manage Transaction Dates
**As a** loan officer  
**I want to** track critical transaction dates  
**So that** I can monitor loan activity and calculate aging

**Acceptance Criteria:**
- User can view Date of Last Transaction - date field, auto-populated from MNSHTRAN.DBF
- System displays the most recent transaction date from HIS_TDATE field
- User can enter Past Due Date - date field
- System stores Past Due Date in MANSERV.PDL field
- System calculates No. of Days Past Due automatically
- System stores calculated days in MANSERV.CNT_PDDAY field
- System updates dates automatically with each transaction
- System uses dates for aging analysis and reporting

### US-038: Manage Interest Rate Information
**As a** loan officer  
**I want to** record and track interest rate details  
**So that** I can calculate interest charges accurately

**Acceptance Criteria:**
- User can select RI Indicator (Regular Interest Rate Indicator) from dropdown - populated from RLSSYS where SYS_GNO = '0120'
- RI Indicator options: 1 (Fixed), 2 (Variable)
- User can enter Interest Rate - numeric field, 7 digits with 4 decimal places (percentage)
- System stores RI Indicator in MANSERV.RIIND field
- System stores Interest Rate in MANSERV.CNT_RI field
- System validates that interest rate is within acceptable range (0-100%)
- System uses interest rate for daily interest calculations
- System tracks interest rate changes over time
- System supports base rate + spread for variable rates (CNT_BASE + CNT_SPRD fields)

### US-039: Manage General Ledger Account Codes
**As a** loan officer  
**I want to** assign GL account codes for various transaction types  
**So that** I can ensure proper accounting treatment

**Acceptance Criteria:**
- User can enter Principal GL Account Code - text field, max 21 characters
- User can enter Interest Income GL Account Code - text field, max 21 characters
- User can enter Advances GL Account Code - text field, max 21 characters
- User can enter Guarantee Fee GL Account Code - text field, max 21 characters
- User can enter GL AIR (Accrued Interest Receivable) Account Code - text field, max 21 characters
- User can enter AI/PC GL Account Code - text field, max 21 characters
- User can enter Exp. Litigation GL Account Code - text field, max 21 characters
- User can enter Reserves GL Account Code - text field, max 21 characters
- System stores GL codes in MANSERV fields: CNT_GLPRIN, CNT_GLINT, CNT_GLADV, CNT_GLGF, CNT_GLAIR, CNT_GLAIPC, CNT_GLLIT, CNT_GLRESV
- System validates GL codes against GLMACCT.DBF table (CACCTNO field)
- System auto-populates GL codes based on Account Type from RLSACCT.DBF
- System uses GL codes for automatic journal entry generation
- System displays GL account descriptions alongside codes
- System allows override of auto-populated GL codes with user confirmation

---

## Manner of Release User Stories

### US-040: Manage Release Documentation
**As a** loan officer  
**I want to** record release documentation details  
**So that** I can maintain proper documentation for loan disbursements

**Acceptance Criteria:**
- User can access Manner of Release tab
- User can enter Description 2 (DR) - text field for debit entry remarks
- User can enter Description 2 (CR) - text field for credit entry remarks
- User can enter Credit Alias - mandatory field, max 9 characters
- System stores DR description in appropriate MANSERV field
- System stores CR description in appropriate MANSERV field
- System stores Credit Alias in MANSERV field
- System validates that Credit Alias is not blank
- System uses descriptions for release memo generation
- System links Credit Alias to GL account for credit entry

---

## Reference Data Management User Stories

### US-041: Manage Account Type Reference Data
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

### US-042: Manage Economic Activity Codes
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

### US-043: Manage General Ledger Master Accounts
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

### US-044: Manage Customer Master Data
**As a** loan officer  
**I want to** maintain comprehensive customer information  
**So that** I can have a single source of truth for customer data

**Acceptance Criteria:**
- User can access Customer management interface
- User can create new customer with borrower number (10 characters)
- User can enter all customer fields from RLSCUST.DBF structure (41 fields)
- System validates unique borrower numbers
- System validates CRIB ID format
- System validates TIN format
- System stores financial information with proper data types
- System links customer to loan accounts
- User can search customers by multiple criteria
- System prevents deletion of customers with active loans
- System maintains customer history and changes
- System supports customer merge functionality

### US-045: Manage Center/Budget Unit Data
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

### US-046: Manage System Reference Data (RLSSYS)
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

### US-047: Manage Group Reference Data (RLSGROUP)
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

---

## Account Operations User Stories

### US-048: Search Loan Accounts
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
- System limits results to user's authorized centers (if applicable)
- Search performs efficiently even with large datasets

### US-049: View Account Summary
**As a** loan officer  
**I want to** view a comprehensive summary of a loan account  
**So that** I can quickly assess account status and key metrics

**Acceptance Criteria:**
- User can access account summary from search results or by entering Reference Number
- System displays summary with all tabs: General, Customer Info/Approval, Loan Info, Balances, Collateral/Dates/GL, Manner of Release
- Summary shows key metrics: OPB, AIR, Past Due Amount, Days Past Due, Account Classification
- Summary shows customer information: Name, TIN, Address, Contact
- Summary shows loan terms: Interest Rate, Maturity Date, Payment Frequency
- Summary shows collateral summary: Security Code, Amount Secured/Unsecured
- Summary shows recent transaction history (last 10 transactions)
- User can navigate to detailed tabs from summary
- System displays alerts for: Past due accounts, Expiring documents, Classification changes
- Summary is read-only; user must enter edit mode to modify

### US-050: Copy Loan Account
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

### US-051: Close/Archive Loan Account
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

## Search and Reporting User Stories

### US-052: Generate Account Listing Report
**As a** loan officer  
**I want to** generate a list of accounts based on various criteria  
**So that** I can analyze portfolio composition and performance

**Acceptance Criteria:**
- User can access Account Listing report
- User can filter by: Center Code, Account Type, Account Classification, Status, Date Range
- User can select columns to include in report
- Report includes: Ref No, Customer Name, Account Type, OPB, AIR, Past Due, Days Past Due, Classification, Status
- User can sort by any column
- User can group by: Center, Account Type, Classification
- Report shows subtotals and grand totals for numeric fields
- User can export to Excel, PDF, or CSV
- System generates report efficiently for large datasets
- Report includes generation date, time, and user
- User can save report parameters as template for reuse


### US-053: Generate Portfolio Summary Report
**As a** management user  
**I want to** view portfolio summary statistics  
**So that** I can monitor overall portfolio health and performance

**Acceptance Criteria:**
- User can access Portfolio Summary report
- Report shows total counts by: Account Type, Classification, Status
- Report shows total amounts by: OPB, AIR, Past Due, Reserves
- Report shows portfolio quality metrics: NPL Ratio, Reserve Coverage Ratio
- Report shows aging analysis: Current, 1-30 days, 31-60 days, 61-90 days, 90+ days
- Report shows concentration by: Economic Activity, Customer Group, Geographic Location
- User can filter by date range and center
- Report includes trend analysis (comparison to previous period)
- Report displays charts and graphs for visual analysis
- User can drill down from summary to detailed account list
- User can export report to Excel or PDF
- Report updates in real-time or shows as-of date

### US-054: Generate Transaction History Report
**As a** loan officer  
**I want to** view transaction history for an account or portfolio  
**So that** I can analyze account activity and audit transactions

**Acceptance Criteria:**
- User can access Transaction History report
- User can filter by: Reference Number, Date Range, Transaction Type, User ID
- Report retrieves data from MNSHTRAN.DBF table
- Report shows: Transaction Date, Value Date, Transaction Code, Amount, Balance After Transaction, User ID
- Report shows all balance fields: OPB, Reserves, AIR, Advances, Past Due
- Report shows both Peso, Dollar, and Other currency columns
- User can sort by any column
- Report shows running balances
- User can export to Excel or CSV
- Report includes transaction descriptions and GL entries
- System retrieves transactions efficiently even for high-volume accounts

### US-055: Generate Regulatory Reports
**As a** compliance officer  
**I want to** generate regulatory reports required by BSP and other regulators  
**So that** I can ensure compliance with reporting requirements

**Acceptance Criteria:**
- User can access Regulatory Reports menu
- System supports multiple report formats: BSP, SEC, BIR
- Reports include: Loan Portfolio Report, DOSRI Report, Past Due Report, Restructured Loans Report
- Reports follow prescribed regulatory formats
- System validates data completeness before report generation
- Reports include all required fields and classifications
- User can select reporting period (monthly, quarterly, annual)
- System generates reports in required file formats (Excel, CSV, XML)
- Reports include certification and sign-off section
- System maintains archive of submitted reports
- System alerts user of upcoming report deadlines

### US-056: Generate Customer Statement
**As a** loan officer  
**I want to** generate customer account statements  
**So that** I can provide customers with their account information

**Acceptance Criteria:**
- User can generate statement for specific Reference Number
- User can select statement period (date range)
- Statement includes customer information: Name, Address, Account Number
- Statement shows opening balance, transactions, closing balance
- Statement shows: Principal, Interest, Advances, Payments
- Statement shows payment due dates and amounts
- Statement shows past due information if applicable
- Statement includes interest rate and terms
- Statement is formatted for printing or email
- User can generate statements in batch for multiple accounts
- Statement includes bank contact information
- System generates statement in PDF format

### US-057: Generate Collateral Report
**As a** credit officer  
**I want to** view collateral coverage across the portfolio  
**So that** I can assess security risk and identify under-collateralized loans

**Acceptance Criteria:**
- User can access Collateral Report
- Report shows: Reference Number, Customer Name, OPB, Amount Secured, Amount Unsecured, Security Code
- Report calculates collateral coverage ratio (Amount Secured / OPB)
- Report shows collateral type breakdown: Real Estate, Machinery, Others
- User can filter by: Security Code, Coverage Ratio threshold, Center
- Report highlights under-collateralized accounts (coverage < 100%)
- Report shows collateral valuation dates
- User can sort by coverage ratio or amount
- Report includes summary statistics
- User can export to Excel or PDF
- Report supports drill-down to account details

### US-058: Generate Borrower Risk Rating Report
**As a** risk officer  
**I want to** analyze borrower risk ratings across the portfolio  
**So that** I can monitor credit risk and identify deteriorating credits

**Acceptance Criteria:**
- User can access BRR Report
- Report retrieves data from RLSCUST.DBF and MNSHBRR.DBF tables
- Report shows: Customer Name, Reference Number, BRR Code, Rating Date, OPB
- Report shows distribution of accounts by BRR rating
- Report shows migration analysis (rating changes over time)
- User can filter by: BRR range, Date range, Center
- Report highlights recent downgrades
- Report shows concentration in high-risk ratings (10-15)
- Report includes summary statistics and charts
- User can drill down to customer details
- User can export to Excel or PDF
- Report shows rating validity periods (start date, end date)

### US-059: Generate Economic Activity Report
**As a** portfolio manager  
**I want to** analyze loan distribution by economic activity  
**So that** I can monitor sector concentration and diversification

**Acceptance Criteria:**
- User can access Economic Activity Report
- Report groups accounts by Economic Activity Code
- Report shows: Economic Activity Description, Number of Accounts, Total OPB, Percentage of Portfolio
- Report shows top 10 economic activities by exposure
- Report includes concentration risk indicators
- User can filter by: Date range, Center, Account Status
- Report shows trend analysis (comparison to previous periods)
- Report displays pie chart or bar chart for visualization
- User can drill down to account list by economic activity
- User can export to Excel or PDF
- Report includes regulatory concentration limits and compliance status

### US-060: Generate Maturity Schedule Report
**As a** treasury officer  
**I want to** view loan maturity schedule  
**So that** I can forecast cash flows and plan funding

**Acceptance Criteria:**
- User can access Maturity Schedule Report
- Report shows loans maturing by period: Current month, Next 3 months, Next 6 months, Next 12 months, Beyond 12 months
- Report shows: Reference Number, Customer Name, Maturity Date, OPB, Payment Frequency
- Report calculates expected cash inflows by period
- User can filter by: Center, Account Type, Currency
- Report shows both principal and interest components
- Report includes amortization schedule for selected accounts
- User can export to Excel for cash flow modeling
- Report updates dynamically based on current balances
- Report highlights loans with balloon payments

---

## Data Validation and Business Rules User Stories

### US-061: Validate Mandatory Fields
**As a** system  
**I want to** enforce mandatory field requirements  
**So that** data integrity is maintained

**Acceptance Criteria:**
- System validates mandatory fields before save:
  - General: Ref. No, Prev. Ref. No., Customer Name, Long Name
  - Customer Info: Business/Residence Address, Project Address, Affiliate, RDO, TIN, V/N, OFW
  - Loan Info: (varies by account type)
  - Balances: OPB (Peso)
  - Collateral: Amount Secured OR Amount Unsecured (if OPB not blank)
  - Manner of Release: Credit Alias
- System displays clear error messages identifying missing fields
- System highlights missing fields in red or with error icon
- System prevents save until all mandatory fields are completed
- System allows save as draft with incomplete data (with warning)
- System validates mandatory fields on each tab before allowing navigation

### US-062: Validate Field Formats and Data Types
**As a** system  
**I want to** validate field formats and data types  
**So that** data quality is maintained

**Acceptance Criteria:**
- System validates character field lengths (e.g., Ref. No max 17 characters)
- System validates numeric fields accept only numbers
- System validates date fields accept only valid dates
- System validates TIN format (17 characters with proper format)
- System validates email format if email field is added
- System validates percentage fields (0-100 range)
- System validates currency amounts (positive numbers with 2 decimal places)
- System displays format-specific error messages
- System prevents entry of invalid characters in numeric fields
- System auto-formats fields where appropriate (e.g., currency with thousand separators)

### US-063: Validate Cross-Field Business Rules
**As a** system  
**I want to** enforce cross-field validation rules  
**So that** data consistency is maintained

**Acceptance Criteria:**
- System validates: Amount Secured + Amount Unsecured = OPB (Peso)
- System validates: Released Amount <= Approved Amount
- System validates: OPB <= Released Amount
- System validates: Start of Term >= Orig Release Date
- System validates: Maturity Date > Start of Term
- System validates: Total Assets = Total Liabilities + Stockholder Equity
- System validates: No. of OFW is required when OFW = Y
- System validates: Guaranteed By is required when Guaranteed = Y
- System validates: Purpose is required when Account Type is AA, AI, R, RDC, RDE, or RDH
- System displays specific error messages for each validation failure
- System prevents save when cross-field validations fail

### US-064: Auto-populate Dependent Fields
**As a** system  
**I want to** automatically populate dependent fields  
**So that** data entry is efficient and consistent

**Acceptance Criteria:**
- System auto-populates Type of Credit based on Account Type and Status
- System auto-populates Purpose of Credit based on Account Type
- System auto-populates GL account codes based on Account Type from RLSACCT.DBF
- System auto-populates Date of Last Transaction from most recent transaction in MNSHTRAN.DBF
- System auto-calculates No. of Days Past Due based on Past Due Date and current date
- System auto-calculates collateral coverage ratio
- System auto-calculates financial ratios from financial statement data
- System allows user override of auto-populated fields (with warning)
- System updates dependent fields when source fields change
- System logs auto-population actions for audit

### US-065: Enforce Conditional Field Requirements
**As a** system  
**I want to** enforce conditional field requirements  
**So that** appropriate data is collected based on context

**Acceptance Criteria:**
- System enables No. of OFW field only when OFW = Y
- System requires No. of OFW when OFW = Y
- System disables and clears No. of OFW when OFW = N
- System enables Guaranteed By field only when Guaranteed = Y
- System requires Guaranteed By when Guaranteed = Y
- System requires Purpose when Account Type is AA, AI, R, RDC, RDE, or RDH
- System requires Amount Secured when OPB is not blank and not equal to Amount Unsecured
- System requires Amount Unsecured when OPB is not blank and not equal to Amount Secured
- System adjusts field requirements dynamically as user enters data
- System provides clear indication of which fields are required in current context

### US-066: Validate Reference Data Integrity
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

### US-067: Calculate and Validate Interest
**As a** system  
**I want to** calculate interest accurately and validate interest-related fields  
**So that** interest charges are correct

**Acceptance Criteria:**
- System calculates daily interest based on: OPB × Interest Rate × Days / 365
- System calculates AIR (Accrued Interest Receivable) daily
- System uses Start of Term as reckoning date for interest calculation
- System handles different interest rate types: Fixed vs Variable
- System calculates variable rate as Base Rate + Spread
- System validates interest rate is within acceptable range (0-100%)
- System handles different day count conventions if applicable
- System calculates interest separately for Peso, Dollar, and Other currencies
- System updates AIR automatically through batch process
- System reverses AIR when interest is paid
- System tracks both regular interest and past due interest separately

### US-068: Enforce Account Status Rules
**As a** system  
**I want to** enforce account status transition rules  
**So that** account status changes follow business logic

**Acceptance Criteria:**
- System automatically changes status to PDO (Past Due) when payment is missed beyond grace period
- System automatically changes Type of Credit to LITIG when Under Litig = Y
- System updates Account Classification based on days past due and payment history
- System prevents certain transactions on closed accounts
- System prevents certain transactions on accounts under litigation
- System validates status transitions follow allowed paths
- System logs all status changes with reason, user, and timestamp
- System sends alerts when accounts change to adverse status
- System applies appropriate reserve requirements based on status
- System updates Area field (PA/NPA) based on account status

### US-069: Validate Transaction Posting Rules
**As a** system  
**I want to** validate transaction posting rules  
**So that** transactions are posted correctly

**Acceptance Criteria:**
- System validates transaction date is not in the future
- System validates value date is >= transaction date
- System validates sufficient balance for payment transactions
- System validates release amount does not exceed approved amount
- System validates payment amount does not exceed outstanding balance
- System validates GL accounts are valid and active
- System validates debit and credit amounts balance
- System validates currency matches account currency
- System prevents duplicate transaction posting
- System validates user has authority to post transaction type
- System validates transaction is posted to correct accounting period

### US-070: Maintain Audit Trail
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

## Additional Cross-Cutting User Stories

### US-071: Manage Multi-Currency Operations
**As a** loan officer  
**I want to** handle loans in multiple currencies  
**So that** I can support foreign currency lending

**Acceptance Criteria:**
- System supports three currency columns: Others, USD, Peso
- System stores currency selection in MANSERV.RELCURR field
- System uses appropriate currency column based on selection
- System converts between currencies using entered exchange rates
- System tracks exchange rates by transaction date
- System calculates gains/losses on currency fluctuations
- System displays amounts in selected currency
- System supports reporting in multiple currencies
- System validates currency-specific business rules
- System handles currency rounding appropriately

### US-072: Support Batch Processing
**As a** system administrator  
**I want to** run batch processes for routine calculations  
**So that** system data is updated automatically

**Acceptance Criteria:**
- System runs daily batch to calculate AIR for all accounts
- System runs daily batch to update days past due
- System runs daily batch to update account classifications
- System runs monthly batch to generate statements
- System runs batch to update exchange rates from external source
- System logs batch execution results
- System sends alerts on batch failures
- System allows manual triggering of batch processes
- System prevents concurrent batch execution
- System maintains batch execution history

### US-073: Handle Restructured Loans
**As a** loan officer  
**I want to** process loan restructuring  
**So that** I can help borrowers in financial difficulty

**Acceptance Criteria:**
- User can mark account as Restructured (Y/N)
- System creates new account record for restructured loan
- System links restructured account to original account via Prev. Ref. No.
- System transfers balances from original to restructured account
- System applies new terms: Interest rate, Maturity date, Payment schedule
- System tracks restructured principal separately (RESA, RESB, RESC codes)
- System tracks restructured AIR separately (RAIR fields)
- System applies special classification rules for restructured loans
- System includes restructured loans in regulatory reports
- System maintains history of restructuring events

### US-074: Support User Access Control
**As a** system administrator  
**I want to** control user access to functions and data  
**So that** security and segregation of duties are maintained

**Acceptance Criteria:**
- System authenticates users with username and password
- System supports role-based access control (RBAC)
- System defines roles: Loan Officer, Credit Officer, Manager, Administrator, Read-Only
- System restricts functions based on user role
- System restricts data access based on center/branch assignment
- System logs all user activities
- System enforces password complexity requirements
- System supports password expiration and reset
- System locks accounts after failed login attempts
- System supports single sign-on (SSO) if available
- System displays user's role and permissions
- System allows administrators to manage user accounts and permissions

### US-075: Provide Data Export and Import
**As a** user  
**I want to** export and import data  
**So that** I can integrate with other systems and perform bulk operations

**Acceptance Criteria:**
- User can export account data to Excel, CSV, or XML
- User can export transaction history to Excel or CSV
- User can export reference data for backup
- User can import reference data from Excel or CSV
- System validates imported data before committing
- System provides import error report with specific issues
- System supports bulk account creation via import
- System maintains data integrity during import/export
- System logs all import/export operations
- System allows scheduling of automated exports
- Exported files include timestamp and user information

---

## Summary

This document contains **75 user stories** organized into the following categories:

1. **General Section**: 4 user stories (US-001 to US-004)
2. **Customer Information/Approval**: 10 user stories (US-005 to US-014)
3. **Loan Info**: 9 user stories (US-015 to US-023)
4. **Balances**: 10 user stories (US-024 to US-033)
5. **Collateral/Dates/GL**: 6 user stories (US-034 to US-039)
6. **Manner of Release**: 1 user story (US-040)
7. **Reference Data Management**: 7 user stories (US-041 to US-047)
8. **Account Operations**: 4 user stories (US-048 to US-051)
9. **Search and Reporting**: 9 user stories (US-052 to US-060)
10. **Data Validation and Business Rules**: 10 user stories (US-061 to US-070)
11. **Cross-Cutting Concerns**: 5 user stories (US-071 to US-075)

Each user story follows the standard format:
- **As a** [role]
- **I want to** [action]
- **So that** [benefit]
- **Acceptance Criteria**: Detailed, testable criteria

All user stories are derived from the Manserv New Account documentation and additional database table specifications, covering:
- Complete CRUD operations for loan accounts
- All six tabs of the New Account interface
- Reference data management for all supporting tables
- Search and reporting capabilities
- Data validation and business rules
- Multi-currency support
- Audit and compliance requirements

---

**Document Status**: Complete and ready for review
**Next Steps**: Review and approval, then proceed to Step 1.2 (Grouping User Stories into Units)
