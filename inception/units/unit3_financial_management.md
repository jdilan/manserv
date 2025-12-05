# Unit 3: Financial Management

## Unit Information
- **Unit Name**: Financial Management
- **Priority**: 1 (Core/Required)
- **Team Assignment**: Single team
- **Dependencies**: Loan Account Management Unit, Reference Data Management Unit, Compliance & Validation Unit
- **Status**: Ready for Implementation

## Purpose and Scope

This unit manages all core financial aspects of loan accounts including balances across multiple currencies, interest calculations, transaction processing, and restructured loan handling. It excludes collateral management and GL account mapping which are moved to the optional Collateral & GL Management Unit.

## Business Capabilities

1. **Balance Management**: Track and manage loan balances across multiple currencies (Peso, USD, Others)
2. **Currency Conversion**: Handle multi-currency operations with manual exchange rate entry
3. **Interest Calculation**: Calculate and accrue interest based on various frequencies and rates
4. **Transaction Processing**: Validate and process loan transactions
5. **Restructured Loan Handling**: Manage loan restructuring and track restructured balances

## Assigned User Stories (13 total)

### Balances Section (10 user stories)

#### US-024: Manage Currency Conversion Rates
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
- Exchange rates are entered manually per transaction
- Exchange rates are updated manually (no automatic schedule)

**Business Rules**:
- Exchange rates: Manual entry per transaction
- Exchange rate updates: Manual (no automatic schedule)
- Conversions: Automatic based on entered rates

---

#### US-025: Manage Approved Amounts
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

---

#### US-026: Manage Released Amounts
**As a** loan officer  
**I want to** record released loan amounts per release memo  
**So that** I can track actual disbursements against approved amounts

**Acceptance Criteria:**
- User can enter Released Amount (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Released Amount (USD) - numeric field, 15 digits with 2 decimal places
- User can enter Released Amount (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.RELAMORIG, MANSERV.RELAMDOLR, MANSERV.RELAMPESO fields
- System validates that released amounts do not exceed approved amounts (strict enforcement)
- System updates released amounts with each release transaction
- System tracks cumulative releases across multiple release memos
- System validates that released amount matches currency selection

**Validation Rules**:
- Released Amount <= Approved Amount (strict enforcement)

---

#### US-027: Manage Outstanding Principal Balance (OPB)
**As a** loan officer  
**I want to** track outstanding principal balances in multiple currencies  
**So that** I can monitor loan exposure and calculate interest

**Acceptance Criteria:**
- User can enter OPB (Others) - numeric field, 15 digits with 2 decimal places
- User can enter OPB (USD) - numeric field, 15 digits with 2 decimal places
- User can enter OPB (Peso) - mandatory field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.OSORIG, MANSERV.OSDOLR, MANSERV.OSPESO fields
- System validates that OPB <= Released Amount (strict enforcement)
- System updates OPB automatically with principal payments and releases
- System uses OPB for interest calculations
- OPB (Peso) is mandatory and cannot be blank

**Validation Rules**:
- OPB (Peso): Mandatory
- OPB <= Released Amount (strict enforcement)

**Note**: Amount Secured + Amount Unsecured validation is moved to Collateral & GL Management Unit (optional)

---

#### US-028: Manage Reserve Amounts
**As a** loan officer  
**I want to** record reserve amounts for loan loss provisioning  
**So that** I can comply with regulatory reserve requirements

**Acceptance Criteria:**
- User can enter Reserves (Others) - numeric field, 15 digits with 2 decimal places
- User can enter Reserves (Dollar) - numeric field, 15 digits with 2 decimal places
- User can enter Reserves (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.RESVORIG, MANSERV.RESVDOLR, MANSERV.RESVPESO fields
- Reserves are entered manually (not auto-calculated)
- Reserve percentages by classification:
  - Unclassified: 0%
  - Watchlisted: 5%
  - Substandard: 25%
  - Doubtful: 50%
  - Loss: 100%
- System validates that reserves meet minimum regulatory requirements
- System updates reserve GL accounts automatically
- System tracks reserve changes over time

**Business Rules**:
- Reserves: Manual entry (not automatic calculation)
- Reserve percentages based on account classification

---

#### US-029: Manage Accrued Interest Receivable (AIR)
**As a** loan officer  
**I want to** track accrued interest receivable in multiple currencies  
**So that** I can properly account for interest income

**Acceptance Criteria:**
- User can enter AIR (Others) - numeric field, 15 digits with 2 decimal places
- User can enter AIR (USD) - numeric field, 15 digits with 2 decimal places
- User can enter AIR (Peso) - numeric field, 15 digits with 2 decimal places
- System stores amounts in MANSERV.AIRORIG, MANSERV.AIRDOLR, MANSERV.AIRPESO fields
- System calculates AIR automatically based on interest rate and days elapsed
- System updates AIR daily through batch processes at 11:59 PM
- System reverses AIR when interest is paid
- System tracks both regular AIR and restructured AIR (RAIR fields)

**Business Rules**:
- Daily AIR calculation: Run at 11:59 PM
- AIR calculation: OPB × Interest Rate × Days / 365

---

#### US-030: Manage Past Due Amounts
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
- Past Due Date is the first missed payment date
- No. of Days Past Due = Current Date - Past Due Date
- No payment triggers past due status
- No grace period before marking as past due

**Business Rules**:
- Past Due Date: First missed payment date
- Days Past Due: Current Date - Past Due Date
- Trigger: No payment
- Grace Period: None

---

#### US-031: Manage Litigation Expenses
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

---

#### US-032: Manage Advances
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

---

#### US-033: Manage Interest Amounts
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

### Interest and Transaction Validation (3 user stories)

#### US-067: Calculate and Validate Interest
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
- Day count convention: Actual/365
- Compounding: Simple interest (not compound)
- Interest accrual frequency: Daily, Monthly, Quarterly, Semi-Annual, Annual (multiple options supported)
- System calculates interest separately for Peso, Dollar, and Other currencies
- System updates AIR automatically through batch process at 11:59 PM
- System reverses AIR when interest is paid
- System tracks both regular interest and past due interest separately

**Business Rules**:
- Formula: OPB × Interest Rate × Days / 365
- Day count: Actual/365
- Reckoning date: Start of Term
- Compounding: Simple interest
- Variable rate: Base Rate + Spread
- Accrual frequency: Daily, Monthly, Quarterly, Semi-Annual, Annual
- Batch time: 11:59 PM daily

---

#### US-069: Validate Transaction Posting Rules
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

---

#### US-071: Manage Multi-Currency Operations
**As a** loan officer  
**I want to** handle loans in multiple currencies  
**So that** I can support foreign currency lending

**Acceptance Criteria:**
- System supports three currency columns: Others, USD, Peso
- System stores currency selection in MANSERV.RELCURR field
- System uses appropriate currency column based on selection:
  - PHP → Peso column
  - USD → Dollar column
  - Others (JPY, EUR, etc.) → Others column
- System converts between currencies using entered exchange rates
- System tracks exchange rates by transaction date
- System calculates gains/losses on currency fluctuations
- System displays amounts in selected currency
- System supports reporting in multiple currencies
- System validates currency-specific business rules
- System handles currency rounding appropriately
- Conversions are automatic based on entered rates

**Business Rules**:
- Currency column mapping:
  - PHP → Peso column
  - USD → Dollar column
  - Others (JPY, EUR) → Others column
- Conversions: Automatic based on entered rates

---

### Restructured Loans (1 user story)

#### US-073: Handle Restructured Loans
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

---

## Service Interfaces

### Exposed Services

This unit exposes the following service interfaces for consumption by other units:

#### 1. Balance Management Service
```
- getBalances(accountId): BalanceDetails
- updateBalance(accountId, balanceType, amount, currency): Success/Failure
- getBalanceHistory(accountId, dateRange): BalanceHistory
- validateBalanceRules(accountId, balanceData): ValidationResult
```

#### 2. Currency Conversion Service
```
- convertCurrency(amount, fromCurrency, toCurrency, exchangeRate): ConvertedAmount
- getExchangeRate(fromCurrency, toCurrency, date): ExchangeRate
- updateExchangeRate(fromCurrency, toCurrency, rate, date): Success/Failure
```

#### 3. Interest Calculation Service
```
- calculateInterest(accountId, fromDate, toDate): InterestAmount
- calculateAIR(accountId, asOfDate): AIRAmount
- accrueInterest(accountId): Success/Failure
- reverseAIR(accountId, amount): Success/Failure
```

#### 4. Transaction Validation Service
```
- validateTransaction(transactionData): ValidationResult
- postTransaction(transactionData): TransactionId
- reverseTransaction(transactionId): Success/Failure
```

#### 5. Restructured Loan Service
```
- restructureLoan(accountId, newTerms): NewAccountId
- getRestructuredLoans(searchCriteria): LoanList
- getRestructuringHistory(accountId): RestructuringHistory
```

### Consumed Services

This unit consumes services from:

#### From Loan Account Management Unit:
- `getAccount(accountId)`: Get account details
- `updateAccount(accountId, accountData)`: Update account information
- `createAccount(accountData)`: Create new account (for restructuring)

#### From Reference Data Management Unit:
- `getReferenceData(groupNumber)`: Get dropdown values
- `validateReferenceCode(code, type)`: Validate reference data codes

#### From Compliance & Validation Unit:
- `validateCrossFieldRules(accountData)`: Validate business rules
- `validateFieldFormats(accountData)`: Validate field formats
- `auditLog(action, userId, accountId, changes)`: Log audit trail
- `checkUserPermission(userId, action, resourceId)`: Check access rights

---

## Data Model

### Primary Tables

#### MANSERV.DBF (Balance Fields)
Key fields managed by this unit:
- **Currency Conversion**: HIS_O2D, HIS_D2P
- **Approved Amounts**: APPORIG, APPDOLR, APPPESO, CNT_APRDAT
- **Released Amounts**: RELAMORIG, RELAMDOLR, RELAMPESO
- **OPB**: OSORIG, OSDOLR, OSPESO
- **Reserves**: RESVORIG, RESVDOLR, RESVPESO
- **AIR**: AIRORIG, AIRDOLR, AIRPESO
- **Past Due**: PDORIG, PDDOLR, PDPESO, CNT_PDDAY, PDL
- **Advances**: ADVRORIG, ADVRDOLR, ADVRPESO
- **Interest**: Interest amount fields
- **Currency**: RELCURR
- **Restructured**: RESA, RESB, RESC, RAIR fields

#### MNSHTRAN.DBF (Transaction History Table)
Transaction records with balance updates

---

## Business Rules Summary

### Validation Rules
1. **Mandatory Fields**:
   - OPB (Peso): Mandatory
2. **Cross-Field Validations**:
   - Released Amount <= Approved Amount (strict enforcement)
   - OPB <= Released Amount (strict enforcement)
3. **Transaction Validations**:
   - Transaction date not in future
   - Value date >= transaction date
   - Sufficient balance for payments
   - Currency matches account currency
4. **Interest Rate**: 0-100% range

### Interest Calculation Rules
1. **Formula**: OPB × Interest Rate × Days / 365
2. **Day Count**: Actual/365
3. **Reckoning Date**: Start of Term
4. **Compounding**: Simple interest
5. **Variable Rate**: Base Rate + Spread
6. **Accrual Frequency**: Daily, Monthly, Quarterly, Semi-Annual, Annual
7. **Batch Processing**: Daily at 11:59 PM

### Currency Rules
1. **Currency Columns**:
   - PHP → Peso column
   - USD → Dollar column
   - Others (JPY, EUR) → Others column
2. **Exchange Rates**: Manual entry per transaction
3. **Conversions**: Automatic based on entered rates

### Past Due Rules
1. **Past Due Date**: First missed payment date
2. **Days Past Due**: Current Date - Past Due Date
3. **Trigger**: No payment
4. **Grace Period**: None

### Reserve Rules
1. **Entry**: Manual (not auto-calculated)
2. **Percentages by Classification**:
   - Unclassified: 0%
   - Watchlisted: 5%
   - Substandard: 25%
   - Doubtful: 50%
   - Loss: 100%

### Access Control
1. **Roles**: User, Authorizer, Administrator
2. **Data Access**: By center/branch
3. **Create/Update**: User and Authorizer roles
4. **Delete**: Administrator role only

---

## Integration Points

### Dependencies
- **Loan Account Management Unit**: For account data and updates
- **Reference Data Management Unit**: For dropdown values and validation
- **Compliance & Validation Unit**: For validation rules and audit logging

### Consumers
- **Reporting & Analytics Unit**: Uses balance and transaction data for reports
- **Collateral & GL Management Unit** (Optional): Uses balance data for GL posting

---

## Batch Processing

### Daily Batch Jobs (11:59 PM)
1. **AIR Calculation**: Calculate and update accrued interest receivable for all accounts
2. **Days Past Due Update**: Update number of days past due for all accounts
3. **Account Classification Update**: Update account classification based on days past due

### Batch Failure Handling
- Retry logic: Automatic retry on failure
- Alerts: Send alerts to administrators on batch failure

---

## Implementation Notes

1. **Multi-Currency Support**: Implement robust currency handling with proper rounding
2. **Interest Calculation**: Support multiple accrual frequencies (daily, monthly, quarterly, semi-annual, annual)
3. **Batch Processing**: Implement efficient batch processing for daily AIR calculation
4. **Transaction Validation**: Implement comprehensive validation before posting
5. **Restructured Loans**: Track restructured balances separately with proper linking
6. **Exchange Rates**: Store exchange rates by transaction date for historical accuracy
7. **Audit Trail**: Log all balance changes and transactions

---

## Testing Considerations

1. **Unit Tests**: Test interest calculations, currency conversions, balance validations
2. **Integration Tests**: Test integration with Loan Account Management and Compliance units
3. **Batch Tests**: Test daily batch processing for AIR calculation and past due updates
4. **Currency Tests**: Test multi-currency operations and conversions
5. **Validation Tests**: Test all transaction validation rules
6. **Performance Tests**: Test batch processing performance with large datasets
7. **Edge Cases**: Test restructured loans, litigation expenses, advance payments

---

**Status**: Ready for Implementation
**Next Steps**: Begin technical design and implementation planning

**Note**: Collateral/Dates/GL management (US-034 to US-040) is moved to Unit 7 (Optional)
