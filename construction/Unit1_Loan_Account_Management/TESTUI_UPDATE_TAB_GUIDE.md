# TestUI Update Tab - Testing Guide

## What I Fixed

I've enhanced the Update Tab in the TestUI (`index.html`) to include the functionality you requested:

### ✅ **New Features Added:**
1. **Search by Reference Number OR Account ID**
2. **Load Account button that actually works**
3. **Visual distinction between editable and read-only fields**
4. **Only Customer Name, Long Name, and Guarantee/Litigation fields are editable**
5. **All other fields are read-only (grayed out)**
6. **Proper error handling and success messages**

## How to Test

### Step 1: Start the API Server

Open a **new Command Prompt** and run:

```bash
cd C:\Kiro\construction\Unit1_Loan_Account_Management\LocalTesting\API
dotnet run --urls "http://localhost:5000"
```

**Wait for this message:**
```
================================================================================
MANSERV Local Test API - Unit 1: Loan Account Management
================================================================================
API Server: http://localhost:5000
Swagger UI: http://localhost:5000/swagger
Database: Data Source=manserv_test.db;Version=3;
================================================================================
```

### Step 2: Open the TestUI

Double-click on:
```
C:\Kiro\construction\Unit1_Loan_Account_Management\LocalTesting\TestUI\index.html
```

### Step 3: Test the Update Tab

1. **Click on the "✏️ Update" tab**

2. **Test Search by Account ID:**
   - Enter `1` in the "Account ID" field
   - Click "🔍 Load Account"
   - Account information should appear below

3. **Test Search by Reference Number:**
   - Click "🗑️ Clear" to reset
   - Enter `LA-2025-0001` in the "Reference Number" field  
   - Click "🔍 Load Account"
   - Same account should load

4. **Verify Field States:**
   - ✅ **Editable fields** (white background):
     - Customer Name
     - Long Name
     - Is Guaranteed checkbox
     - Guaranteed By (when checkbox is checked)
     - Is Under Litigation checkbox
     - Litigation Date (when checkbox is checked)
   
   - ✅ **Read-only fields** (gray background):
     - Reference Number
     - Previous Reference Number
     - CRIB ID Number
     - All Account Identification fields
     - All Loan Dates fields
     - All Account Type and Funding fields

5. **Test Checkbox Functionality:**
   - Check "Is Guaranteed" → "Guaranteed By" field should become enabled
   - Uncheck "Is Guaranteed" → "Guaranteed By" field should become disabled and cleared
   - Same behavior for "Is Under Litigation" and "Litigation Date"

6. **Test Update:**
   - Change "Customer Name" to "Updated Customer Name"
   - Change "Long Name" to "Updated Long Name"  
   - Check "Is Guaranteed" and enter "Test Bank"
   - Click "💾 Update Account"
   - Should see success message

7. **Test Error Handling:**
   - Click "🗑️ Clear"
   - Click "🔍 Load Account" without entering anything
   - Should see error: "Please enter either a Reference Number or Account ID to search"

## Available Test Data

The API includes these sample accounts:

| Account ID | Reference Number | Customer Name |
|------------|------------------|---------------|
| 1 | LA-2025-0001 | Juan Dela Cruz |
| 2 | LA-2025-0002 | Maria Santos |
| 3 | LA-2025-0003 | Pedro Gonzales |

## Troubleshooting

### ❌ "API Offline" message
- Make sure the API server is running on port 5001
- Check that you see the startup message in the command prompt

### ❌ "Account not found" errors  
- Use the test data above (Account IDs 1, 2, 3)
- Use Reference Numbers: LA-2025-0001, LA-2025-0002, LA-2025-0003

### ❌ Load Account button does nothing
- Check browser console (F12) for JavaScript errors
- Make sure the API server is running
- Try refreshing the page

### ❌ Fields not enabling/disabling
- This should work automatically when you check/uncheck the checkboxes
- If not working, check browser console for errors

## What You Should See

### ✅ **Success Indicators:**
- Green sections for editable fields
- Gray sections for read-only fields  
- Search loads account information
- Only specified fields can be edited
- Checkboxes control dependent fields
- Update shows success message
- Clear button resets everything

### ❌ **Issues to Report:**
- Fields that should be read-only are editable
- Checkboxes don't control dependent fields
- Search doesn't find accounts
- Update doesn't save changes
- Error messages don't appear

## Visual Guide

**Search Section (Blue border):**
- Reference Number field
- Account ID field
- Load Account and Clear buttons

**Editable Sections (Green border):**
- General Information (Customer Name, Long Name only)
- Guarantee and Litigation Information

**Read-only Sections (Gray border):**
- Account Identification
- Loan Dates  
- Account Type and Funding

The Update Tab now works exactly like the ASP.NET version I created, but in the HTML TestUI interface!

## Next Steps

Once this works in the TestUI, you can:
1. Use the same logic in your ASP.NET application
2. Test with real database connections
3. Deploy to your production environment

**The TestUI is perfect for testing the functionality before integrating into your main application!**