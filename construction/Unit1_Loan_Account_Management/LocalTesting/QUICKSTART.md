# MANSERV Local Testing - Quick Start Guide

## 🚀 Get Started in 5 Minutes

### Step 1: Check Prerequisites ✅

- [ ] .NET 6.0 SDK installed ([Download](https://dotnet.microsoft.com/download/dotnet/6.0))
- [ ] Modern web browser (Chrome, Firefox, Edge)

**Verify .NET installation:**
```bash
dotnet --version
```
Should show `6.0.x` or later.

### Step 2: Start the API Server 🖥️

```bash
cd construction/Unit1_Loan_Account_Management/LocalTesting/API
dotnet restore
dotnet run
```

**Wait for this message:**
```
API Server: http://localhost:5000
Swagger UI: http://localhost:5000/swagger
```

✅ API is ready!

### Step 3: Open the Test UI 🌐

**Option A - Double-click:**
- Navigate to `LocalTesting/TestUI/`
- Double-click `index.html`

**Option B - Command line:**
```bash
# Windows
start ../TestUI/index.html

# Mac
open ../TestUI/index.html

# Linux
xdg-open ../TestUI/index.html
```

### Step 4: Test It! 🎯

1. **Check API Status** - Should show "✅ API Online"
2. **Click "Search" tab** → Click "Get All" → See 12 sample accounts
3. **Click "Create" tab** → Click "Load Sample Data" → Click "Create Account"
4. **Success!** 🎉

---

## Common Commands

### Start API Server
```bash
cd LocalTesting/API
dotnet run
```

### Stop API Server
Press `Ctrl+C` in the terminal

### Reset Database
```bash
# Delete database file
rm manserv_test.db  # Mac/Linux
del manserv_test.db  # Windows

# Restart API (will recreate database)
dotnet run
```

### View API Documentation
Open browser: http://localhost:5000/swagger

---

## Quick Test Scenarios

### ✅ Create an Account
1. Test UI → "Create" tab
2. "Load Sample Data" button
3. "Create Account" button
4. Check response viewer for success

### ✅ Search Accounts
1. "Search" tab
2. Enter "Juan" in Customer Name
3. "Search" button
4. See results

### ✅ View Account Details
1. Search for accounts
2. Click "View" on any result
3. See full details

---

## Troubleshooting

### ❌ "API Offline"
- Make sure API server is running
- Check it's on port 5000
- Try: http://localhost:5000/api/health

### ❌ "Port 5000 already in use"
- Stop other applications using port 5000
- Or change port in `API/Properties/launchSettings.json`

### ❌ ".NET SDK not found"
- Install from: https://dotnet.microsoft.com/download/dotnet/6.0
- Restart terminal after installation

---

## What's Next?

📖 **Full Documentation**: See `README.md`  
🔧 **API Reference**: See `Documentation/API_Reference.md`  
🧪 **Test Scenarios**: See `Documentation/Test_Scenarios.md`  
❓ **Problems?**: See `Documentation/Troubleshooting.md`

---

**That's it! You're ready to test Unit 1 APIs locally.** 🚀

