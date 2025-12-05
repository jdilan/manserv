# MANSERV Local Testing Environment - Unit 1: Loan Account Management

## Overview

This is a self-contained local testing environment for Unit 1 (Loan Account Management) APIs. It includes:

- ✅ SQLite database (no SQL Server required)
- ✅ Mock services for all external dependencies
- ✅ Local API server (.NET Core 6.0)
- ✅ Browser-based test UI
- ✅ Complete documentation

## Quick Start

### Prerequisites

- .NET 6.0 SDK or later ([Download](https://dotnet.microsoft.com/download/dotnet/6.0))
- Modern web browser (Chrome, Firefox, Edge)
- Text editor (VS Code, Notepad++, etc.)

### Step 1: Navigate to API Directory

```bash
cd construction/Unit1_Loan_Account_Management/LocalTesting/API
```

### Step 2: Restore NuGet Packages

```bash
dotnet restore
```

### Step 3: Run the API Server

```bash
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

### Step 4: Open the Test UI

Open `TestUI/index.html` in your web browser:

**Windows:**
```bash
start ../TestUI/index.html
```

**Mac/Linux:**
```bash
open ../TestUI/index.html
# or
xdg-open ../TestUI/index.html
```

### Step 5: Start Testing!

The Test UI will automatically connect to the API server. You can:

- 🔍 Search for accounts
- ➕ Create new accounts
- 👁️ View account details
- ✏️ Update accounts
- 🗑️ Delete accounts
- 📚 Browse reference data

## What's Included

### 1. SQLite Database

- **Location**: `API/manserv_test.db` (auto-created on first run)
- **Schema**: Converted from SQL Server 2022
- **Sample Data**: 12 pre-loaded test accounts
- **Documentation**: `Database/SQLite_Limitations.md`

### 2. Mock Services

All external dependencies are mocked:

- **Reference Data Service**: Dropdowns, account types, economic activities
- **Validation Service**: Business rule validation
- **Access Control Service**: User permissions and roles
- **Customer Service**: Sample customer data

**Configuration**: `Mocks/MockConfig.json`  
**Documentation**: `Documentation/Mock_Services_Guide.md`

### 3. Local API Server

- **Technology**: ASP.NET Core 6.0 Minimal API
- **Port**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger
- **Endpoints**: Full CRUD + Search + Reference Data

### 4. Test UI

- **Technology**: HTML + CSS + JavaScript (no frameworks)
- **Features**: All CRUD operations, search, reference data viewer
- **Sample Data**: One-click form population
- **Response Viewer**: Formatted JSON with syntax highlighting

## Directory Structure

```
LocalTesting/
├── README.md                    (this file)
├── QUICKSTART.md               (one-page quick start)
├── config.json                 (main configuration)
├── Database/
│   ├── SQLite_Schema.sql       (database schema)
│   ├── SQLite_SampleData.sql   (sample data)
│   └── SQLite_Limitations.md   (SQL Server vs SQLite)
├── DataAccess/
│   └── SqliteAccountRepository.cs
├── Mocks/
│   ├── ReferenceDataServiceMock.cs
│   ├── ValidationServiceMock.cs
│   ├── AccessControlServiceMock.cs
│   ├── CustomerServiceMock.cs
│   ├── MockConfig.json
│   └── MockConfigManager.cs
├── API/
│   ├── Program.cs
│   ├── ManservLocalTestAPI.csproj
│   ├── Controllers/
│   │   ├── AccountController.cs
│   │   ├── AccountQueryController.cs
│   │   └── ReferenceDataController.cs
│   ├── Middleware/
│   │   └── RequestLoggingMiddleware.cs
│   └── Properties/
│       └── launchSettings.json
├── TestUI/
│   ├── index.html
│   ├── css/
│   │   └── styles.css
│   └── js/
│       ├── api-client.js
│       ├── sample-data.js
│       └── response-viewer.js
└── Documentation/
    ├── External_Dependencies.md
    ├── Mock_Services_Guide.md
    ├── API_Reference.md
    ├── Troubleshooting.md
    └── Test_Scenarios.md
```

## API Endpoints

### Account Management

- `GET /api/accounts` - Get all accounts
- `GET /api/accounts/{id}` - Get account by ID
- `GET /api/accounts/by-reference/{refNo}` - Get account by reference number
- `POST /api/accounts` - Create new account
- `PUT /api/accounts/{id}` - Update account
- `DELETE /api/accounts/{id}` - Delete account (soft delete)
- `GET /api/accounts/{id}/audit` - Get audit history

### Account Query

- `GET /api/accounts/search` - Search accounts
- `GET /api/accounts/by-center/{centerCode}` - Get accounts by center
- `GET /api/accounts/by-status/{status}` - Get accounts by status
- `GET /api/accounts/exists/{refNo}` - Check if reference number exists
- `GET /api/accounts/statistics` - Get account statistics

### Reference Data

- `GET /api/reference` - Get all reference data groups
- `GET /api/reference/{groupNumber}` - Get reference data by group
- `GET /api/reference/account-types` - Get account types
- `GET /api/reference/economic-activities` - Get economic activities
- `GET /api/reference/centers` - Get centers
- `GET /api/reference/customers` - Get customers
- `GET /api/reference/customers/{id}` - Get customer by ID
- `GET /api/reference/gl-mappings` - Get GL account mappings

### Health Check

- `GET /api/health` - Check API and database status

## Configuration

### Change API Port

Edit `API/Properties/launchSettings.json`:

```json
{
  "applicationUrl": "http://localhost:YOUR_PORT"
}
```

Also update `TestUI/js/api-client.js`:

```javascript
const API_BASE_URL = 'http://localhost:YOUR_PORT/api';
```

### Change Mock Behavior

Edit `Mocks/MockConfig.json`:

```json
{
  "mockMode": "lenient",  // or "strict"
  "validation": {
    "strictMode": false,
    "allowDuplicateRefNo": true
  }
}
```

### Change Database Location

Edit `API/Program.cs`:

```csharp
var connectionString = "Data Source=YOUR_PATH/manserv_test.db;Version=3;";
```

## Testing Scenarios

### Scenario 1: Create a New Account

1. Open Test UI
2. Click "Create" tab
3. Click "Load Sample Data"
4. Click "Create Account"
5. Check response viewer for success

### Scenario 2: Search Accounts

1. Click "Search" tab
2. Enter search criteria (e.g., Customer Name: "Juan")
3. Click "Search"
4. View results in table

### Scenario 3: View Account Details

1. Search for an account
2. Click "View" button in results
3. See full account details

### Scenario 4: Update an Account

1. Search for an account
2. Click "Edit" button
3. Modify fields
4. Click "Update Account"

### Scenario 5: Test Validation

1. Create tab → Load Sample Data
2. Clear "Customer Name" field
3. Try to create → See validation error

## Troubleshooting

### API Won't Start

**Problem**: Port 5000 already in use

**Solution**: Change port in `launchSettings.json` and `api-client.js`

---

**Problem**: Missing .NET SDK

**Solution**: Install .NET 6.0 SDK from https://dotnet.microsoft.com/download

### Database Issues

**Problem**: Database file not found

**Solution**: Delete `manserv_test.db` and restart API (will auto-recreate)

---

**Problem**: Sample data not loading

**Solution**: Check that SQL scripts are in `Database/` folder

### Test UI Issues

**Problem**: API Status shows "Offline"

**Solution**: Make sure API server is running on http://localhost:5000

---

**Problem**: Dropdowns not populating

**Solution**: Check browser console for errors, verify API is accessible

### CORS Issues

**Problem**: Browser blocks API calls

**Solution**: CORS is already enabled in API. If still blocked, try:
- Use same origin (open HTML from http://localhost:5000/TestUI/index.html)
- Check browser console for specific CORS error

## Advanced Usage

### Using Swagger UI

1. Start API server
2. Open http://localhost:5000/swagger
3. Explore and test endpoints directly
4. See request/response schemas

### Using curl

```bash
# Get all accounts
curl http://localhost:5000/api/accounts

# Create account
curl -X POST http://localhost:5000/api/accounts \
  -H "Content-Type: application/json" \
  -d @account.json

# Search accounts
curl "http://localhost:5000/api/accounts/search?customerName=Juan"
```

### Using Postman

1. Import API endpoints from Swagger
2. Set base URL: http://localhost:5000
3. Add `userId` query parameter to requests
4. Test all endpoints

## Next Steps

### Transition to Production

1. Replace SQLite with SQL Server
2. Replace mock services with real implementations
3. Add authentication/authorization
4. Deploy to production environment

### Add More Test Data

Edit `Database/SQLite_SampleData.sql` and add more INSERT statements, then:

```bash
# Delete existing database
rm API/manserv_test.db

# Restart API (will recreate with new data)
dotnet run
```

### Customize Mock Responses

Edit mock service files in `Mocks/` directory to add/modify test data.

## Support

For issues or questions:

1. Check `Documentation/Troubleshooting.md`
2. Review `Documentation/API_Reference.md`
3. Check API logs in console output
4. Review browser console for UI errors

## License

Internal use only - MANSERV Loan Account Management System

---

**Version**: 1.0  
**Last Updated**: December 6, 2025  
**Status**: Ready for Testing
