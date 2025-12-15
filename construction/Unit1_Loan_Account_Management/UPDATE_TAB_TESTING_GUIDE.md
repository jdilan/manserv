# Update Tab Testing Guide

## Overview
This guide will walk you through testing the new Update Tab functionality, including the "Load Account" feature and restricted field editing capabilities.

## Prerequisites

### 1. Start the Local Testing Environment

First, make sure the local API server is running:

```bash
# Navigate to the API directory
cd construction/Unit1_Loan_Account_Management/LocalTesting/API

# Restore packages (first time only)
dotnet restore

# Start the API server
dotnet run
```

You should see:
```
================================================================================
MANSERV Local Test API - Unit 1: Loan Account Management
================================================================================
API Server: http://localhost:5000
Swagger UI: http://localhost:5000/swagger
Database: Data Source=manserv_test.db;Version=3;
================================================================================
```

### 2. Set Up the Web Application

Since we're testing ASP.NET Web Forms pages, you'll need to:

1. **Copy the updated files** to your web application project
2. **Build the web application** 
3. **Deploy to IIS or run in Visual Studio**

**Files to copy:**
- `UI_UpdateAccount.aspx`
- `UI_UpdateAccount.aspx.cs`

## Testing Scenarios

### Scenario 1: Load Account by Reference Number

**Objective**: Test the new search functionality using Reference Number

**Steps:**
1. Open the Update Account page (`UpdateAccount.aspx`)
2. You should see the "Load Account" section at the top
3. In the "Reference Number" field, enter: `LA-2025-0001`
4. Leave the "Account ID" field empty
5. Click "🔍 Load Account" button

**Expected Results:**
- Account information panel should appear below
- All fields should be populated with account data
- Customer Name and Long Name fields should be editable (white background)
- All other fields should be disabled (gray background)
- Guarantee and Litigation section should be editable
- Search fields should be cleared

**Verification Points:**
- ✅ Account loads successfully
- ✅ Editable fields are enabled
- ✅ Read-only fields are disabled
- ✅ Search fields are cleared after load

### Scenario 2: Load Account by Account ID

**Objective**: Test search functionality using Account ID

**Steps:**
1. Clear any loaded account by clicking "🗑️ Clear" button
2. In the "Account ID" field, enter: `1`
3. Leave the "Reference Number" field empty
4. Click "🔍 Load Account" button

**Expected Results:**
- Same as Scenario 1, but using Account ID search
- Account with ID 1 should be loaded

**Verification Points:**
- ✅ Account loads by ID
- ✅ Correct account information is displayed
- ✅ Field states are correct (editable vs read-only)

### Scenario 3: Test Search Validation

**Objective**: Test error handling for invalid search criteria

**Steps:**
1. Clear any loaded account
2. Leave both search fields empty
3. Click "🔍 Load Account" button

**Expected Results:**
- Error message: "Please enter either a Reference Number or Account ID to search."
- No account information should be displayed

**Additional Tests:**
- Enter invalid Account ID (e.g., "abc"): Should show "Invalid Account ID format"
- Enter non-existent Reference Number (e.g., "INVALID-REF"): Should show account not found error
- Enter non-existent Account ID (e.g., "99999"): Should show account not found error

### Scenario 4: Test Field Editability

**Objective**: Verify that only specified fields can be edited

**Steps:**
1. Load any account (use Scenario 1 or 2)
2. Try to modify each field type:

**Editable Fields (should work):**
- Customer Name: Change to "Test Customer Updated"
- Long Name: Change to "Test Customer Long Name Updated"
- Is Guaranteed: Check/uncheck the checkbox
- Guaranteed By: Enter "Test Guarantor" (only when Is Guaranteed is checked)
- Is Under Litigation: Check/uncheck the checkbox
- Litigation Date: Select a date (only when Is Under Litigation is checked)

**Read-only Fields (should be disabled):**
- Reference Number (already a label, not editable)
- Previous Reference Number
- CRIB ID Number
- NIDSS Account Number
- All Account Identification fields (Center Code, Budget Unit, etc.)
- All Loan Dates fields
- All Account Type and Funding fields

**Expected Results:**
- Editable fields should accept changes
- Read-only fields should be grayed out and not accept input
- Dependent fields (Guaranteed By, Litigation Date) should enable/disable based on checkboxes

### Scenario 5: Test Guarantee Functionality

**Objective**: Test the guarantee checkbox and dependent field behavior

**Steps:**
1. Load any account
2. **Test enabling guarantee:**
   - Uncheck "Is Guaranteed" if checked
   - Verify "Guaranteed By" field is disabled and grayed out
   - Check "Is Guaranteed"
   - Verify "Guaranteed By" field becomes enabled (white background)
   - Enter "ABC Bank" in "Guaranteed By" field

3. **Test disabling guarantee:**
   - Uncheck "Is Guaranteed"
   - Verify "Guaranteed By" field becomes disabled and text is cleared

**Expected Results:**
- Checkbox controls the enabled state of the dependent field
- Field is cleared when checkbox is unchecked
- Field styling changes appropriately (gray when disabled, white when enabled)

### Scenario 6: Test Litigation Functionality

**Objective**: Test the litigation checkbox and dependent field behavior

**Steps:**
1. Load any account
2. **Test enabling litigation:**
   - Uncheck "Is Under Litigation" if checked
   - Verify "Litigation Date" field is disabled and grayed out
   - Check "Is Under Litigation"
   - Verify "Litigation Date" field becomes enabled
   - Select a date (e.g., today's date)

3. **Test disabling litigation:**
   - Uncheck "Is Under Litigation"
   - Verify "Litigation Date" field becomes disabled and date is cleared

**Expected Results:**
- Same behavior as guarantee functionality but for litigation fields

### Scenario 7: Test Update Functionality

**Objective**: Test saving changes to editable fields

**Steps:**
1. Load account with Reference Number "LA-2025-0001"
2. Make the following changes:
   - Customer Name: "Updated Customer Name"
   - Long Name: "Updated Long Name"
   - Check "Is Guaranteed"
   - Guaranteed By: "Test Bank"
   - Check "Is Under Litigation"
   - Litigation Date: Select today's date
3. Click "💾 Update Account" button

**Expected Results:**
- Success message should appear: "Account updated successfully"
- Changes should be saved to the database

**Verification:**
1. Click "🗑️ Clear" to clear the form
2. Load the same account again
3. Verify all changes were saved:
   - Customer Name shows "Updated Customer Name"
   - Long Name shows "Updated Long Name"
   - Is Guaranteed is checked
   - Guaranteed By shows "Test Bank"
   - Is Under Litigation is checked
   - Litigation Date shows the selected date
4. Verify read-only fields were NOT changed

### Scenario 8: Test Update Without Loading Account

**Objective**: Test validation when trying to update without loading an account

**Steps:**
1. Open fresh Update Account page (or click Clear)
2. Don't load any account
3. Click "💾 Update Account" button

**Expected Results:**
- Error message: "Please load an account first before updating."
- No update should be performed

### Scenario 9: Test Clear Functionality

**Objective**: Test the Clear button behavior

**Steps:**
1. Load any account
2. Make some changes to editable fields
3. Click "🗑️ Clear" button

**Expected Results:**
- Search fields should be cleared
- Account information panel should be hidden
- All form fields should be reset
- Any error/success messages should be cleared

### Scenario 10: Test Cancel Button

**Objective**: Test navigation behavior of Cancel button

**Steps:**
1. **Test with loaded account:**
   - Load an account
   - Click "❌ Cancel" button
   - Should redirect to ViewAccount.aspx with the account ID

2. **Test without loaded account:**
   - Clear the form (no account loaded)
   - Click "❌ Cancel" button
   - Should redirect to Default.aspx

### Scenario 11: Test Direct URL Access

**Objective**: Test backward compatibility with direct URL access

**Steps:**
1. Access the page directly with an account ID: `UpdateAccount.aspx?id=1`

**Expected Results:**
- Account should load automatically
- Account information panel should be visible
- All functionality should work as normal
- This maintains backward compatibility

## API Testing (Optional)

If you want to test the API endpoints directly:

### 1. Test Search Endpoint

```bash
# Search by Reference Number
curl "http://localhost:5000/api/accounts/search?referenceNumber=LA-2025-0001&userId=SYSTEM"

# Get account by ID
curl "http://localhost:5000/api/accounts/1?userId=SYSTEM"
```

### 2. Test Update Endpoint

```bash
# Update account (you'll need to construct the full JSON payload)
curl -X PUT "http://localhost:5000/api/accounts/1?userId=SYSTEM" \
  -H "Content-Type: application/json" \
  -d '{
    "accountId": 1,
    "customerName": "Updated Name",
    "longName": "Updated Long Name",
    "isGuaranteed": true,
    "guaranteedBy": "Test Bank",
    "isUnderLitigation": false,
    "litigationDate": null,
    ... (other required fields)
  }'
```

## Troubleshooting

### Common Issues

1. **"Account not found" errors:**
   - Check that the API server is running
   - Verify the database has sample data
   - Try using known Reference Numbers: LA-2025-0001, LA-2025-0002, etc.

2. **Fields not enabling/disabling:**
   - Check browser console for JavaScript errors
   - Verify AutoPostBack is working (requires ViewState)

3. **Update not working:**
   - Check that all required fields in AccountDTO are being set
   - Verify the API endpoint is accessible
   - Check server logs for validation errors

4. **Page not loading:**
   - Ensure all referenced services are available
   - Check that the Master Page (Site.Master) exists
   - Verify all using statements are correct

### Browser Developer Tools

Use F12 Developer Tools to:
- Check Console for JavaScript errors
- Monitor Network tab for API calls
- Inspect form elements and their states

## Test Data

The local testing environment includes these sample accounts:

- **LA-2025-0001** (Account ID: 1) - Juan Dela Cruz
- **LA-2025-0002** (Account ID: 2) - Maria Santos  
- **LA-2025-0003** (Account ID: 3) - Pedro Gonzales
- And more...

You can use any of these for testing.

## Success Criteria

✅ **Search Functionality:**
- Can search by Reference Number
- Can search by Account ID
- Proper error handling for invalid searches
- Clear functionality works

✅ **Field Control:**
- Only specified fields are editable
- Read-only fields are properly disabled
- Visual styling clearly indicates field states

✅ **Dependent Fields:**
- Guarantee fields work correctly
- Litigation fields work correctly
- Checkboxes control dependent field states

✅ **Update Functionality:**
- Changes are saved correctly
- Read-only fields are preserved
- Proper validation and error handling

✅ **Navigation:**
- Cancel button works correctly
- Direct URL access still works
- Proper redirects after operations

## Next Steps

After successful testing:
1. Deploy to staging environment
2. Conduct user acceptance testing
3. Update user documentation
4. Plan production deployment

---

**Happy Testing!** 🚀

If you encounter any issues during testing, refer to the troubleshooting section or check the API server logs for detailed error information.