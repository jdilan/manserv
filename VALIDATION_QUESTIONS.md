# Validation Questions - Input Required

## Document Information
- **Purpose**: Collect business rule confirmations before implementation
- **Date**: December 5, 2025
- **Status**: Awaiting Your Input

---

## Quick Reference: What I Need From You

### IMMEDIATE (Before Creating Unit Files)
✅ **CONFIRMED**: 7-unit structure (5 core + 2 optional) is acceptable
✅ **CONFIRMED**: Collateral/Dates/GL Tab (US-034 to US-039) is OPTIONAL
✅ **CONFIRMED**: Manner of Release (US-040) is OPTIONAL
✅ **CONFIRMED**: Reporting & Analytics (US-052 to US-060, US-075) is OPTIONAL

### DURING UNIT CREATION (Step 9)
✅ **COMPLETED**: Validation questions answered below

---

## Validation Questions

### 1. Mandatory Field Rules ✅
**Current Understanding**: These fields are mandatory:
- General: Ref. No, Prev. Ref. No., Customer Name, Long Name
- Customer Info: Business/Residence Address, Project Address, Affiliate, RDO, TIN, V/N, OFW
- Balances: OPB (Peso)

**Answers**:
- ✅ All fields above are mandatory
- ✅ Users CAN save as "Draft" with incomplete data
- ❌ No additional mandatory fields

---

### 2. Cross-Field Validation Rules ✅
**Answers**:
- ✅ **Amount Secured + Amount Unsecured = OPB (Peso)** - Must balance exactly
- ✅ **Released Amount <= Approved Amount** - Strict enforcement
- ✅ **OPB <= Released Amount** - Strict enforcement
- ✅ **Start of Term = Orig Release Date** - Must be equal (not >=)
- ✅ **Maturity Date > Start of Term** - Strict enforcement
- ✅ **Total Assets = Total Liabilities + Stockholder Equity** - Optional (not required to balance)

---

### 3. Duplicate Reference Number Handling ✅
**Answers**:
- ✅ **Warning but allow override**
- ✅ Hard block (prevent save)
- ✅ Check across entire system
- ❌ Not limited to center/branch

---

### 4. Account Status Transition Rules ✅
**Answers**:
- ✅ **90 Days** - Account changes from Current to Past Due after 90 days
- ✅ **Manual only** - Litigation status is manual, but create a separate Reclass Module for Litigation
- ✅ Status can be manually overridden

---

### 5. Interest Calculation Method ✅
**Current Understanding**: OPB × Interest Rate × Days / 365

**Answers**:
- ✅ Formula is correct
- ✅ Day count convention: Actual/365
- ✅ Reckoning date: Start of Term is the base date
- ✅ **Simple interest** (not compound)
- ✅ Variable rate: Base Rate + Spread
- ✅ **Accrual frequency**: Daily, Monthly, Quarterly, Semi-Annual, Annual (multiple options supported)

---

### 6. Past Due Calculation ✅
**Answers**:
- ✅ "Past Due Date" is the first missed payment date
- ✅ "No. of Days Past Due" = Current Date - Past Due Date
- ✅ **No payment** triggers past due status
- ❌ **No grace period** before marking as past due

---

### 7. Account Classification Rules ✅
**Answers**:
- ✅ **Triggers**: Non-payment, number of days past due, and upon payment
- ✅ Classification based on days past due only
- ✅ **Classification thresholds confirmed**:
  - Current: 0-30 days
  - Watchlisted: 31-60 days
  - Substandard: 61-90 days
  - Doubtful: 91-180 days
  - Loss: 180+ days
- ✅ Classification can be manually overridden

---

### 8. Reserve Calculation ✅
**Answers**:
- ✅ Reserves calculated as % of OPB based on classification
- ✅ **Reserve percentages confirmed**:
  - Unclassified: 0%
  - Watchlisted: 5%
  - Substandard: 25%
  - Doubtful: 50%
  - Loss: 100%
- ✅ **Manual entry** (not automatic calculation)

---

### 9. Conditional Field Requirements ✅
**Answers**:
- ✅ **No. of OFW**: Required when OFW = Y, cannot be zero
- ❌ **Guaranteed By**: NOT required when Guaranteed = Y
- ✅ **Purpose**: Required for Account Types AA, AI, R, RDC, RDE, RDH
- ❌ **Amount Secured/Unsecured**: NOT required (even when OPB > 0)

---

### 10. Field Format Validations ✅
**Answers**:
- ✅ **TIN format**: Standard format (no specific pattern specified)
- ❌ **Reference Number**: No specific pattern or prefix required
- ❌ **CRIB ID**: No specific format
- ❌ **NIDSS Account No**: No specific format
- ✅ **Interest Rate**: Standard limits (0-100%)

---

### 11. Currency Conversion Rules ✅
**Answers**:
- ✅ **Manual entry** per transaction (not fetched from rate table)
- ✅ **Manual update** (no automatic schedule)
- ✅ **Currency column mapping confirmed**:
  - PHP → Peso column
  - USD → Dollar column
  - Others (JPY, EUR) → Others column
- ✅ Conversions are automatic based on entered rates

---

### 12. Deletion Rules ✅
**Answers**:
- ✅ **Soft delete** (mark as deleted)
- ✅ Prevent deletion if transaction history exists
- ✅ Prevent deletion if active balances or collateral exist
- ❌ Deleted accounts **cannot** be restored

---

### 13. Auto-Population Rules ✅
**Answers**:
- ✅ **Type of Credit**: Auto-populated based on Account Type and Status
- ✅ **Purpose of Credit**: Auto-populated based on Account Type
- ✅ **GL Account Codes**: Auto-populated from RLSACCT.DBF, users can override
- ✅ **Date of Last Transaction**: Auto-populated from MNSHTRAN.DBF
- ✅ **None** - No auto-populated fields can be overridden (except GL codes)

---

### 14. Batch Processing Rules ✅
**Answers**:
- ✅ **Daily AIR calculation**: Run at **11:59 PM**
- ✅ **Daily days past due update**: Run at **11:59 PM**
- ✅ **Account classification update**: **Daily**
- ✅ **Statement generation**: **End of month**
- ✅ **Batch failure handling**: Retry logic and alerts

---

### 15. User Access Control ✅
**Answers**:
- ✅ **Roles**: **User, Authorizer, Administrator only** (simplified from original proposal)
- ✅ **Data access**: **By center/branch**
- ✅ **Function restrictions**:
  - **Administrator**: Can delete
  - **User & Authorizer**: Can create and update
  - **User & Authorizer**: Cannot delete
- ✅ **Approval workflows**: Standard approval required

---

## How to Provide Input

Please review each question and provide your answers. You can:
1. Reply with answers inline in this document
2. Provide answers verbally and I'll document them
3. Provide answers in phases as we work through each unit

**Note**: I can proceed with creating the unit structure files now, and we can fill in the validation details during Step 9 when we review the Compliance & Validation unit.

---

**Status**: ✅ COMPLETED - All validation questions answered
**Next Step**: Proceed with creating unit files based on confirmed requirements
