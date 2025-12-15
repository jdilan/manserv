# Update Tab Testing Checklist

## Quick Setup
- [ ] API server running (`cd LocalTesting/API && dotnet run`)
- [ ] Web application deployed with updated files
- [ ] Browser ready for testing

## Core Functionality Tests

### Search & Load
- [ ] Search by Reference Number works (try: LA-2025-0001)
- [ ] Search by Account ID works (try: 1)
- [ ] Error handling for empty search criteria
- [ ] Error handling for invalid/non-existent accounts
- [ ] Clear button resets form and hides account info

### Field Editability
- [ ] **Editable fields** (white background, can type):
  - [ ] Customer Name
  - [ ] Long Name
  - [ ] Is Guaranteed checkbox
  - [ ] Guaranteed By (when Is Guaranteed checked)
  - [ ] Is Under Litigation checkbox
  - [ ] Litigation Date (when Is Under Litigation checked)

- [ ] **Read-only fields** (gray background, disabled):
  - [ ] Reference Number (label)
  - [ ] Previous Reference Number
  - [ ] CRIB ID Number
  - [ ] NIDSS Account Number
  - [ ] All Account Identification fields
  - [ ] All Loan Dates fields
  - [ ] All Account Type and Funding fields

### Dependent Field Behavior
- [ ] **Guarantee fields:**
  - [ ] Guaranteed By disabled when Is Guaranteed unchecked
  - [ ] Guaranteed By enabled when Is Guaranteed checked
  - [ ] Guaranteed By cleared when Is Guaranteed unchecked

- [ ] **Litigation fields:**
  - [ ] Litigation Date disabled when Is Under Litigation unchecked
  - [ ] Litigation Date enabled when Is Under Litigation checked
  - [ ] Litigation Date cleared when Is Under Litigation unchecked

### Update Operations
- [ ] Can update Customer Name and Long Name
- [ ] Can update guarantee information
- [ ] Can update litigation information
- [ ] Success message appears after update
- [ ] Changes persist when reloading account
- [ ] Read-only fields remain unchanged after update
- [ ] Error when trying to update without loading account

### Navigation
- [ ] Cancel with loaded account → redirects to ViewAccount.aspx
- [ ] Cancel without loaded account → redirects to Default.aspx
- [ ] Direct URL access (UpdateAccount.aspx?id=1) still works

## Test Data to Use
- Reference Numbers: LA-2025-0001, LA-2025-0002, LA-2025-0003
- Account IDs: 1, 2, 3
- Invalid data: "INVALID-REF", 99999, "abc"

## Quick Test Sequence
1. Load account by Reference Number → ✅
2. Verify field states → ✅
3. Toggle guarantee checkbox → ✅
4. Toggle litigation checkbox → ✅
5. Update Customer Name → ✅
6. Save changes → ✅
7. Clear and reload → ✅
8. Verify changes saved → ✅

## Issues Found
- [ ] Issue 1: ________________________________
- [ ] Issue 2: ________________________________
- [ ] Issue 3: ________________________________

## Sign-off
- [ ] All tests passed
- [ ] Ready for user acceptance testing
- [ ] Documentation updated

**Tester:** _________________ **Date:** _________