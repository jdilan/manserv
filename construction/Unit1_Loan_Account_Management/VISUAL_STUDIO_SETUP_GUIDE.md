# Visual Studio Project Setup Guide
## Unit 1: Loan Account Management

**Date**: December 5, 2025  
**Technology**: ASP.NET 4.7 Web Forms, Entity Framework 6.x, SQL Server 2022  
**Estimated Time**: 30-45 minutes

---

## Prerequisites

- [ ] Visual Studio 2019 or later installed
- [ ] .NET Framework 4.7.2 or later installed
- [ ] SQL Server 2022 (or 2019) installed and running
- [ ] IIS Express (included with Visual Studio)
- [ ] All source files from `Source/` folder available

---

## Step 1: Create New Web Forms Project (10 minutes)

### 1.1 Create Project
1. Open Visual Studio
2. Click **File** > **New** > **Project**
3. Search for "ASP.NET Web Application (.NET Framework)"
4. Click **Next**

### 1.2 Configure Project
- **Project name**: `ManservLoanSystem.Web`
- **Location**: `construction/Unit1_Loan_Account_Management/Source/`
- **Framework**: `.NET Framework 4.7.2`
- Click **Create**

### 1.3 Select Template
- Select **Web Forms** template
- Check **Web API** (optional, for future API endpoints)
- Authentication: **No Authentication** (we'll configure Forms Auth manually)
- Click **Create**

Visual Studio will create the project with default structure.

---

## Step 2: Organize Project Structure (5 minutes)

### 2.1 Create Folders
Right-click project > **Add** > **New Folder** for each:

```
ManservLoanSystem.Web/
├── Models/
│   ├── Entities/
│   ├── DTOs/
│   └── Common/
├── Data/
├── Repositories/
├── Services/
├── ExternalServices/
├── Features/
│   ├── GeneralAccountManagement/
│   ├── LoanInformationManagement/
│   └── AccountOperations/
└── App_Start/
```

### 2.2 Delete Unnecessary Files (Optional)
- Delete `About.aspx`, `Contact.aspx` if not needed
- Keep `Default.aspx`, `Site.Master`

---

## Step 3: Copy Source Files (10 minutes)

### 3.1 Copy Entity Models
Copy these files to `Models/Entities/`:
- `Models_Entities_Account.cs` → `Account.cs`
- `Models_Entities_AccountAudit.cs` → `AccountAudit.cs`
- `Models_Entities_AccountRelationship.cs` → `AccountRelationship.cs`

### 3.2 Copy DTOs
Copy these files to `Models/DTOs/`:
- `Models_DTOs_AccountDTO.cs` → `AccountDTO.cs`

### 3.3 Copy Common Classes
Copy these files to `Models/Common/`:
- `Models_Common_ServiceResponse.cs` → `ServiceResponse.cs`

### 3.4 Copy Data Layer
Copy these files to `Data/`:
- `Data_ManservDbContext.cs` → `ManservDbContext.cs`

### 3.5 Copy Repositories
Copy these files to `Repositories/`:
- `Repositories_IAccountRepository.cs` → `IAccountRepository.cs`
- `Repositories_AccountRepository.cs` → `AccountRepository.cs`

### 3.6 Copy Services
Copy these files to `Services/`:
- `Services_IAccountManagementService.cs` → `IAccountManagementService.cs`
- `Services_AccountManagementService.cs` → `AccountManagementService.cs`
- `Services_IAccountQueryService.cs` → `IAccountQueryService.cs`
- `Services_AccountQueryService.cs` → `AccountQueryService.cs`

### 3.7 Copy External Services
Copy these files to `ExternalServices/`:
- `ExternalServices_ReferenceDataServiceStub.cs` → `ReferenceDataServiceStub.cs`

**Important**: After copying, right-click each file in Solution Explorer and select **Include in Project** if not automatically included.

---

## Step 4: Install NuGet Packages (5 minutes)

### 4.1 Open Package Manager Console
**Tools** > **NuGet Package Manager** > **Package Manager Console**

### 4.2 Install Entity Framework 6.x
```powershell
Install-Package EntityFramework -Version 6.4.4
```

### 4.3 Install Optional Packages
```powershell
# For logging (optional)
Install-Package log4net -Version 2.0.15

# For dependency injection (optional)
Install-Package Unity.AspNet.WebForms -Version 5.11.1
```

---

## Step 5: Configure Web.config (5 minutes)

### 5.1 Copy Template
1. Copy `Web.config.template` content
2. Replace existing `Web.config` in project root
3. Or merge the connection strings and entityFramework sections

### 5.2 Update Connection String
Find this section and update with your SQL Server details:

```xml
<connectionStrings>
  <add name="ManservLoanDB" 
       connectionString="Data Source=localhost;Initial Catalog=ManservLoanDB;Integrated Security=True;TrustServerCertificate=True;Application Name=ManservLoanSystem;MultipleActiveResultSets=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Change**:
- `Data Source=localhost` → Your SQL Server instance name
- `Integrated Security=True` → Or use SQL Authentication

---

## Step 6: Fix Namespace References (5 minutes)

### 6.1 Update Namespaces
Open each copied file and update the namespace from:
```csharp
namespace ManservLoanSystem.Models.Entities
```

To match your project structure (if different).

### 6.2 Add Using Statements
Ensure all files have necessary using statements:
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
```

---

## Step 7: Build and Test (5 minutes)

### 7.1 Build Solution
1. Click **Build** > **Build Solution** (or press `Ctrl+Shift+B`)
2. Fix any compilation errors
3. Common issues:
   - Missing using statements
   - Namespace mismatches
   - Missing NuGet packages

### 7.2 Test Database Connection
Add this code to `Global.asax.cs` in `Application_Start`:

```csharp
protected void Application_Start(object sender, EventArgs e)
{
    // Test database connection
    using (var context = new ManservLoanSystem.Data.ManservDbContext())
    {
        if (context.Database.Exists())
        {
            System.Diagnostics.Debug.WriteLine("✓ Database connection successful!");
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("✗ Database not found. Run schema scripts first.");
        }
    }
}
```

### 7.3 Run Application
1. Press **F5** to run in debug mode
2. Check Output window for database connection message
3. Default.aspx should load successfully

---

## Step 8: Create Simple Test Page (Optional - 10 minutes)

### 8.1 Create TestRepository.aspx
1. Right-click project > **Add** > **Web Form**
2. Name: `TestRepository.aspx`
3. Add this code to code-behind:

```csharp
using System;
using ManservLoanSystem.Data;
using ManservLoanSystem.Repositories;

namespace ManservLoanSystem.Web
{
    public partial class TestRepository : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TestDatabaseConnection();
            }
        }

        private void TestDatabaseConnection()
        {
            try
            {
                using (var context = new ManservDbContext())
                {
                    var repository = new AccountRepository(context);
                    var accounts = repository.GetAll();
                    
                    Response.Write($"<h2>Database Test Results</h2>");
                    Response.Write($"<p>✓ Connection successful!</p>");
                    Response.Write($"<p>Found {accounts.Count} accounts</p>");
                    
                    Response.Write("<h3>Accounts:</h3><ul>");
                    foreach (var account in accounts)
                    {
                        Response.Write($"<li>{account.ReferenceNumber}: {account.CustomerName}</li>");
                    }
                    Response.Write("</ul>");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<h2>Error</h2>");
                Response.Write($"<p style='color:red'>{ex.Message}</p>");
            }
        }
    }
}
```

4. Run and navigate to `/TestRepository.aspx`
5. Should display list of accounts from database

---

## Step 9: Set Up Dependency Injection (Optional - 10 minutes)

### 9.1 Create DependencyConfig.cs
Create file in `App_Start/DependencyConfig.cs`:

```csharp
using System.Web;
using ManservLoanSystem.Data;
using ManservLoanSystem.Repositories;
using ManservLoanSystem.Services;

namespace ManservLoanSystem.Web.App_Start
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            // Simple factory pattern for now
            // TODO: Replace with proper DI container (Unity, Autofac)
        }
        
        // Helper methods to get instances
        public static ManservDbContext GetDbContext()
        {
            return new ManservDbContext();
        }
        
        public static IAccountRepository GetAccountRepository()
        {
            return new AccountRepository(GetDbContext());
        }
        
        public static IAccountManagementService GetAccountManagementService()
        {
            return new AccountManagementService(GetAccountRepository());
        }
        
        public static IAccountQueryService GetAccountQueryService()
        {
            return new AccountQueryService(GetAccountRepository());
        }
    }
}
```

### 9.2 Use in Pages
In your page code-behind:

```csharp
private IAccountManagementService _accountService;

protected void Page_Load(object sender, EventArgs e)
{
    _accountService = DependencyConfig.GetAccountManagementService();
}
```

---

## Troubleshooting

### Issue: "Could not load file or assembly 'EntityFramework'"
**Solution**: 
1. Right-click project > **Manage NuGet Packages**
2. Reinstall EntityFramework package
3. Clean and rebuild solution

### Issue: "Cannot open database 'ManservLoanDB'"
**Solution**:
1. Verify SQL Server is running
2. Run database schema scripts (001, 002, 003)
3. Check connection string in Web.config
4. Test connection in SQL Server Management Studio

### Issue: "The type or namespace name 'ManservLoanSystem' could not be found"
**Solution**:
1. Check all files are included in project
2. Verify namespaces match project structure
3. Rebuild solution

### Issue: Compilation errors in copied files
**Solution**:
1. Add missing using statements
2. Install required NuGet packages
3. Check .NET Framework version (should be 4.7.2)

---

## Next Steps

After completing this setup:

1. ✅ **Verify** - Run TestRepository.aspx to confirm everything works
2. ⏳ **Create UI** - Build Web Forms pages for CRUD operations
3. ⏳ **Add Features** - Implement search, validation, etc.
4. ⏳ **Test** - Test all functionality end-to-end

---

## Quick Reference

### Project Structure
```
ManservLoanSystem.Web/
├── Models/Entities/          (3 files)
├── Models/DTOs/              (1 file)
├── Models/Common/            (1 file)
├── Data/                     (1 file)
├── Repositories/             (2 files)
├── Services/                 (4 files)
├── ExternalServices/         (1 file)
├── Features/                 (UI pages - to be created)
├── App_Start/                (DI config)
└── Web.config                (configuration)
```

### Key Files
- **Web.config** - Connection strings and configuration
- **Global.asax.cs** - Application startup
- **ManservDbContext.cs** - Entity Framework context
- **AccountRepository.cs** - Data access
- **AccountManagementService.cs** - Business logic

### Useful Commands
```powershell
# Build solution
Ctrl+Shift+B

# Run with debugging
F5

# Run without debugging
Ctrl+F5

# Clean solution
Build > Clean Solution

# Rebuild solution
Build > Rebuild Solution
```

---

**Setup Complete!** 🎉

You now have a working ASP.NET Web Forms project with:
- ✅ Entity Framework 6.x configured
- ✅ Repository pattern implemented
- ✅ Service layer ready
- ✅ Database connection working

Next: Create Web Forms UI pages for account management!

---

**Document Status**: Complete  
**Last Updated**: December 5, 2025  
**Version**: 1.0
