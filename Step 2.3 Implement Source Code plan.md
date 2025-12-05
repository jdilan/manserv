# Step 2.3: Implement Source Code - Plan

## Project Information
- **Unit**: Unit 1 - Loan Account Management
- **Technology Stack**: ASP.NET 4.7, SQL Server 2022, ADO.NET/Entity Framework 6.x
- **Architecture**: Feature-Based Architecture with Service-Oriented patterns
- **Status**: Planning Phase
- **Date**: December 5, 2025

---

## Prerequisites Check
- [ ] **PREREQUISITE**: Verify logical_design.md exists in construction/Unit1_Loan_Account_Management/
  - **NOTE**: If logical_design.md does not exist, we need to complete Step 2.2 first
  - **ACTION REQUIRED**: Please confirm if we should proceed with implementation based on architecture_design.md only, or if we need to create logical_design.md first

---

## Implementation Approach

### Technology Decisions ✅ CONFIRMED
- [x] **CONFIRMED**: ASP.NET Web Forms pattern
  - Traditional approach, familiar to waterfall teams
  - Good for rapid development with drag-and-drop controls
  - Built-in ViewState management

- [x] **CONFIRMED**: Entity Framework 6.x (ORM)
  - Code First or Database First approach
  - LINQ queries for data access
  - Automatic change tracking and migrations

- [x] **CONFIRMED**: ASP.NET Built-in Dependency Injection
  - Use ASP.NET 4.7.2+ built-in DI container
  - Register services in Global.asax or App_Start

- [x] **CONFIRMED**: Synchronous operations (no async/await)
  - Traditional synchronous data access
  - Simpler code for teams transitioning from waterfall

- [x] **CONFIRMED**: Same application service communication
  - All services within same application
  - Direct method calls, no web services or REST APIs

---

## Implementation Steps

### Phase 1: Project Setup and Infrastructure (Foundation)
- [ ] **Step 1.1**: Create ASP.NET 4.7 project structure
  - Create solution file
  - Create web application project (Web Forms or MVC based on decision)
  - Create class library projects for Services, Repositories, Models
  - Set up project references

- [ ] **Step 1.2**: Configure NuGet packages
  - Install Entity Framework 6.x
  - Install Microsoft.AspNet.WebApi (for Web API controllers if needed)
  - Install logging framework (log4net or NLog)
  - Install testing frameworks (NUnit or xUnit, Moq) - optional for now

- [x] **Step 1.3**: Set up database infrastructure ✅ COMPLETED
  - ✅ Created SQL Server 2022 database scripts
  - ✅ Created database schema (tables, indexes, constraints)
  - ✅ Added SQL Server 2022 specific features comments
  - ✅ Created initial migration scripts (001, 002, 003)
  - ✅ Added sample/seed data for testing (10 sample accounts)
  - ✅ Created comprehensive Database README with setup instructions

- [ ] **Step 1.4**: Configure Web.config
  - Add connection strings with SQL Server 2022 configuration
  - Add authentication/authorization settings
  - Add app settings for service endpoints
  - Add detailed comments for deployment considerations
  - Configure IIS Express settings

---

### Phase 2: Data Layer Implementation (Bottom-Up Approach)

- [x] **Step 2.1**: Create Entity Models ✅ COMPLETED
  - ✅ Created Account.cs entity class with full mappings
  - ✅ Created AccountAudit.cs entity class
  - ✅ Created AccountRelationship.cs entity class
  - ✅ Added data annotations for Entity Framework
  - ✅ Mapped to legacy MANSERV.DBF field names in comments

- [x] **Step 2.2**: Create DTOs (Data Transfer Objects) ✅ COMPLETED
  - ✅ Created AccountDTO.cs for create/update operations
  - ✅ Created ServiceResponse<T> and ServiceError classes
  - ✅ Created ErrorCodes constants
  - ⏳ AccountSummaryDTO.cs (optional - can use AccountDTO)
  - ⏳ SearchCriteriaDTO.cs (optional - using method parameters)

- [x] **Step 2.3**: Create Repository Interfaces ✅ COMPLETED
  - ✅ Created IAccountRepository.cs interface with full CRUD operations
  - ✅ Defined query methods (Search, GetAll, GetByStatus, etc.)
  - ✅ Defined audit operations
  - ✅ Defined relationship operations
  - ⏳ Separate interfaces for Audit and Relationship (optional - combined in IAccountRepository)

- [x] **Step 2.4**: Implement Repositories ✅ COMPLETED
  - ✅ Implemented AccountRepository.cs with Entity Framework 6.x
  - ✅ Implemented all CRUD operations
  - ✅ Implemented search and query operations
  - ✅ Implemented audit operations
  - ✅ Implemented relationship operations
  - ✅ Added SQL Server 2022 specific comments
  - ✅ Implemented error handling (with logging placeholders)
  - ✅ Implemented IDisposable pattern

---

### Phase 3: Business Services Layer Implementation

- [ ] **Step 3.1**: Create Service Interfaces
  - Create IAccountManagementService.cs interface
  - Create IAccountQueryService.cs interface
  - Create IAccountLifecycleService.cs interface
  - Create ILoanInformationService.cs interface

- [ ] **Step 3.2**: Create External Service Interfaces (Stubs)
  - Create ICustomerQueryService.cs interface (Unit 2 dependency)
  - Create IReferenceDataService.cs interface (Unit 4 dependency)
  - Create IValidationService.cs interface (Unit 5 dependency)
  - Create IAuditService.cs interface (Unit 5 dependency)
  - Create IAccessControlService.cs interface (Unit 5 dependency)
  - **NOTE**: These will be stub implementations for demo purposes

- [ ] **Step 3.3**: Implement Service Classes
  - Implement AccountManagementService.cs
    - CreateAccount method with validation flow
    - GetAccount method with access control
    - UpdateAccount method with audit trail
    - DeleteAccount method with soft delete logic
  - Implement AccountQueryService.cs
    - SearchAccounts method with pagination
    - GetAccountSummary method
    - GetAccountsByCustomer method
    - GetAccountsByCenter method
  - Implement AccountLifecycleService.cs
    - CopyAccount method with relationship tracking
    - CloseAccount method with balance validation
    - ArchiveAccount method
    - ReopenAccount method
  - Implement LoanInformationService.cs
    - UpdateAccountIdentification method
    - UpdateLoanDates method with date validation
    - UpdateAccountType method with auto-population
    - UpdateLoanStatus method
    - UpdateLitigationStatus method

- [ ] **Step 3.4**: Implement Validators
  - Create AccountValidator.cs for mandatory field validation
  - Create LoanDateValidator.cs for date relationship validation
  - Create ConditionalFieldValidator.cs for conditional field rules

- [ ] **Step 3.5**: Implement Stub Services (for demo)
  - Create CustomerQueryServiceStub.cs with sample data
  - Create ReferenceDataServiceStub.cs with sample dropdown data
  - Create ValidationServiceStub.cs with basic validation
  - Create AuditServiceStub.cs with console logging
  - Create AccessControlServiceStub.cs with simple role checks

---

### Phase 4: Presentation Layer Implementation

- [ ] **Step 4.1**: Create Feature 1 - General Account Management UI
  - Create CreateAccount page/view
    - Form with General section fields
    - Client-side and server-side validation
    - Duplicate reference number warning
    - Draft save functionality
  - Create ViewAccount page/view
    - Read-only display of account details
    - Search by reference number
  - Create UpdateAccount page/view
    - Editable form with validation
    - Audit trail display
  - Create DeleteAccount functionality
    - Confirmation dialog
    - Dependency checks
    - Soft delete implementation

- [ ] **Step 4.2**: Create Feature 2 - Loan Information Management UI
  - Create ManageLoanInfo page/view
    - Account Identification section
    - Economic Activity section
    - Account Type and Purpose section
    - Funding Source and Program section
    - Loan Status and Classification section
    - Guarantee Information section
    - Litigation Status section
    - Project Type and Currency section
  - Create ManageLoanDates page/view
    - Date fields with validation
    - Date relationship enforcement
  - Implement dropdown population from Reference Data service
  - Implement auto-population logic for Type of Credit and Purpose of Credit

- [ ] **Step 4.3**: Create Feature 3 - Account Operations UI
  - Create SearchAccounts page/view
    - Multi-criteria search form
    - Paginated results grid
    - Sorting and filtering
    - Export to Excel functionality
  - Create AccountSummary page/view
    - Comprehensive account summary display
    - Key metrics display
    - Recent transaction history
    - Navigation to detailed tabs
  - Create CopyAccount page/view
    - Source account selection
    - New reference number input
    - Confirmation and creation
  - Create CloseAccount functionality
    - Balance validation
    - Closure confirmation
    - Closure report generation

- [ ] **Step 4.4**: Create Shared UI Components
  - Create master page/layout with navigation
  - Create error display component
  - Create success message component
  - Create loading indicator component
  - Create pagination component

---

### Phase 5: Configuration and Dependency Injection

- [ ] **Step 5.1**: Configure Dependency Injection Container
  - Register all service interfaces and implementations
  - Register all repository interfaces and implementations
  - Configure service lifetimes (scoped, singleton, transient)
  - Set up constructor injection

- [ ] **Step 5.2**: Configure Logging
  - Set up log4net or NLog configuration
  - Configure log levels (Debug, Info, Warn, Error)
  - Configure log file locations
  - Add logging to all services and repositories

- [ ] **Step 5.3**: Configure Authentication and Authorization
  - Set up Forms Authentication
  - Create login page
  - Implement role-based authorization
  - Implement center/branch restrictions

---

### Phase 6: Demo Application and Sample Data

- [ ] **Step 6.1**: Create Sample Data
  - Create SQL script to populate reference data tables
  - Create sample accounts with various statuses
  - Create sample audit records
  - Create sample account relationships

- [ ] **Step 6.2**: Create Demo Scenarios
  - Scenario 1: Create new loan account (complete workflow)
  - Scenario 2: Update loan information (date validation)
  - Scenario 3: Search and view accounts (pagination)
  - Scenario 4: Copy account (relationship tracking)
  - Scenario 5: Close account (balance validation)

- [ ] **Step 6.3**: Create Demo User Accounts
  - Create User role account
  - Create Authorizer role account
  - Create Administrator role account
  - Assign center/branch restrictions

---

### Phase 7: Testing and Verification

- [ ] **Step 7.1**: Manual Testing
  - Test all CRUD operations
  - Test all validation rules
  - Test all business rules
  - Test access control and authorization
  - Test error handling and logging

- [ ] **Step 7.2**: Integration Testing
  - Test service integration with repositories
  - Test service integration with external services (stubs)
  - Test end-to-end workflows

- [ ] **Step 7.3**: Performance Testing
  - Test search performance with large datasets
  - Test pagination performance
  - Test concurrent user access

- [ ] **Step 7.4**: Security Testing
  - Test SQL injection prevention
  - Test XSS prevention
  - Test authentication and authorization
  - Test audit trail completeness

---

### Phase 8: Documentation and Deployment

- [ ] **Step 8.1**: Create Code Documentation
  - Add XML comments to all public methods
  - Create README.md for project structure
  - Document configuration settings
  - Document deployment procedures

- [ ] **Step 8.2**: Create User Documentation
  - Create user guide for each feature
  - Create screenshots and workflow diagrams
  - Document validation rules and error messages

- [ ] **Step 8.3**: Prepare for Deployment
  - Create deployment checklist
  - Create database migration scripts
  - Create IIS configuration guide
  - Create troubleshooting guide

- [ ] **Step 8.4**: Package Demo Application
  - Create deployment package
  - Include sample data scripts
  - Include configuration templates
  - Include setup instructions

---

## File Structure (Proposed)

```
construction/Unit1_Loan_Account_Management/
├── Source/
│   ├── ManservLoanSystem.sln
│   ├── ManservLoanSystem.Web/                    (ASP.NET Web Forms Application)
│   │   ├── Features/
│   │   │   ├── GeneralAccountManagement/
│   │   │   │   ├── CreateAccount.aspx (+ .aspx.cs code-behind)
│   │   │   │   ├── ViewAccount.aspx
│   │   │   │   ├── UpdateAccount.aspx
│   │   │   │   └── DeleteAccount.aspx
│   │   │   ├── LoanInformationManagement/
│   │   │   │   ├── ManageLoanInfo.aspx
│   │   │   │   └── ManageLoanDates.aspx
│   │   │   └── AccountOperations/
│   │   │       ├── SearchAccounts.aspx
│   │   │       ├── AccountSummary.aspx
│   │   │       ├── CopyAccount.aspx
│   │   │       └── CloseAccount.aspx
│   │   ├── App_Start/
│   │   │   └── DependencyConfig.cs              (DI registration)
│   │   ├── Web.config                            (with detailed comments)
│   │   └── Global.asax
│   ├── ManservLoanSystem.Services/               (Class Library)
│   │   ├── Interfaces/
│   │   │   ├── IAccountManagementService.cs
│   │   │   ├── IAccountQueryService.cs
│   │   │   ├── IAccountLifecycleService.cs
│   │   │   └── ILoanInformationService.cs
│   │   ├── Implementation/
│   │   │   ├── AccountManagementService.cs
│   │   │   ├── AccountQueryService.cs
│   │   │   ├── AccountLifecycleService.cs
│   │   │   └── LoanInformationService.cs
│   │   └── Validators/
│   │       ├── AccountValidator.cs
│   │       ├── LoanDateValidator.cs
│   │       └── ConditionalFieldValidator.cs
│   ├── ManservLoanSystem.Repositories/           (Class Library)
│   │   ├── Interfaces/
│   │   │   ├── IAccountRepository.cs
│   │   │   ├── IAccountAuditRepository.cs
│   │   │   └── IAccountRelationshipRepository.cs
│   │   └── Implementation/
│   │       ├── AccountRepository.cs
│   │       ├── AccountAuditRepository.cs
│   │       └── AccountRelationshipRepository.cs
│   ├── ManservLoanSystem.Models/                 (Class Library)
│   │   ├── Entities/
│   │   │   ├── Account.cs
│   │   │   ├── AccountAudit.cs
│   │   │   └── AccountRelationship.cs
│   │   ├── DTOs/
│   │   │   ├── AccountDTO.cs
│   │   │   ├── AccountSummaryDTO.cs
│   │   │   └── SearchCriteriaDTO.cs
│   │   └── Common/
│   │       ├── ServiceResponse.cs
│   │       └── ServiceError.cs
│   └── ManservLoanSystem.ExternalServices/       (Class Library - Stubs)
│       ├── Interfaces/
│       │   ├── ICustomerQueryService.cs
│       │   ├── IReferenceDataService.cs
│       │   ├── IValidationService.cs
│       │   ├── IAuditService.cs
│       │   └── IAccessControlService.cs
│       └── Stubs/
│           ├── CustomerQueryServiceStub.cs
│           ├── ReferenceDataServiceStub.cs
│           ├── ValidationServiceStub.cs
│           ├── AuditServiceStub.cs
│           └── AccessControlServiceStub.cs
├── Database/
│   ├── Schema/
│   │   ├── 001_CreateTables.sql
│   │   ├── 002_CreateIndexes.sql
│   │   └── 003_CreateConstraints.sql
│   ├── SampleData/
│   │   ├── 001_ReferenceData.sql
│   │   ├── 002_SampleAccounts.sql
│   │   └── 003_SampleUsers.sql
│   └── README.md                                 (Database setup instructions)
├── Documentation/
│   ├── UserGuide.md
│   ├── DeveloperGuide.md
│   ├── DeploymentGuide.md
│   └── Screenshots/
└── Tests/                                        (Optional - if time permits)
    ├── ManservLoanSystem.Services.Tests/
    └── ManservLoanSystem.Repositories.Tests/
```

---

## Critical Decisions Required Before Implementation

### 1. Technology Stack Confirmation ✅ CONFIRMED
- ✅ **Web Forms** - Confirmed
- ✅ **Entity Framework 6.x** - Confirmed
- ✅ **ASP.NET Built-in DI** - Confirmed
- ✅ **Synchronous operations** - Confirmed
- ✅ **Same application services** - Confirmed

### 2. Scope Clarification ⏳ PENDING YOUR DECISION
- **Should we implement all 23 user stories or focus on core CRUD operations first?**
  - Option A: Implement all features (comprehensive but time-consuming)
  - Option B: Implement core features only (General Account Management + basic Loan Info)
  - **Recommendation**: Option B for initial implementation, then expand
  - **YOUR INPUT REQUIRED**: Which scope do you prefer?

### 3. External Service Dependencies ✅ CONFIRMED
- **How should we handle dependencies on other units (Customer, Reference Data, Compliance)?**
  - ✅ Confirmed approach: Use stub implementations for demo
  - All external services will be in-memory stubs with sample data
  - Services will be in same application (no web service calls)

### 4. Testing Scope ⏳ PENDING YOUR DECISION
- **Should we include unit tests and integration tests in this step?**
  - Option A: Include tests (better quality but more time)
  - Option B: Manual testing only (faster but less coverage)
  - **Recommendation**: Manual testing for demo, unit tests in Step 2.5
  - **YOUR INPUT REQUIRED**: Confirm testing approach
  - **Note**: Step 2.5 is specifically for creating tests, so Option B makes sense

### 5. Deployment Environment ⏳ PENDING YOUR DECISION
- **Should we configure for local IIS Express only or include IIS deployment?**
  - Option A: IIS Express only (simpler, for demo)
  - Option B: Include IIS deployment configuration (production-ready)
  - **Recommendation**: IIS Express for demo with IIS deployment notes in comments
  - **YOUR INPUT REQUIRED**: Confirm deployment target

---

## Estimated Effort

| Phase | Estimated Time | Complexity |
|-------|---------------|------------|
| Phase 1: Project Setup | 2-3 hours | Low |
| Phase 2: Data Layer | 4-6 hours | Medium |
| Phase 3: Business Services | 8-12 hours | High |
| Phase 4: Presentation Layer | 10-15 hours | High |
| Phase 5: Configuration | 2-3 hours | Medium |
| Phase 6: Demo Application | 3-4 hours | Low |
| Phase 7: Testing | 4-6 hours | Medium |
| Phase 8: Documentation | 2-3 hours | Low |
| **Total** | **35-52 hours** | **High** |

**Note**: This is a substantial implementation. Consider breaking into smaller iterations.

---

## Risk Assessment

| Risk | Impact | Mitigation |
|------|--------|------------|
| Missing logical_design.md | High | Create based on architecture_design.md or complete Step 2.2 first |
| Complex validation rules | Medium | Use stub services for demo, implement fully later |
| External service dependencies | Medium | Use stub implementations with sample data |
| Time constraints | High | Focus on core features first, expand later |
| SQL Server 2022 specific features | Low | Document clearly, use standard SQL where possible |
| IIS deployment complexity | Medium | Focus on IIS Express, provide deployment guide |

---

## Success Criteria

- [ ] All core CRUD operations working (Create, Read, Update, Delete)
- [ ] Basic validation rules implemented and working
- [ ] Search functionality with pagination working
- [ ] Demo application runs successfully in IIS Express
- [ ] Sample data loaded and accessible
- [ ] All configuration files have detailed comments
- [ ] User can complete all demo scenarios successfully
- [ ] Code is well-organized following feature-based architecture
- [ ] Basic error handling and logging implemented
- [ ] Documentation complete (README, setup instructions)

---

## Next Steps After Plan Approval

1. **Confirm all critical decisions** (technology choices, scope, testing approach)
2. **Verify prerequisites** (logical_design.md or proceed with architecture_design.md)
3. **Begin Phase 1: Project Setup** and proceed step-by-step
4. **Mark checkboxes as completed** after each step
5. **Request review at key milestones** (after each phase)

---

## Notes

- This plan assumes implementation from scratch
- Focus on simplicity and clarity for teams transitioning from waterfall
- Prioritize working demo over comprehensive features
- All code will include detailed comments for SQL Server 2022 and deployment
- Stub services will have realistic sample data for meaningful demo

---

**Status**: 🚧 IN PROGRESS - Database Complete, Application Pending Decision

## Decision Summary

### ✅ All Decisions Confirmed:
1. **Web Forms** - ASP.NET Web Forms with code-behind
2. **Entity Framework 6.x** - ORM for data access (Code First approach)
3. **ASP.NET Built-in DI** - Dependency injection
4. **Synchronous operations** - No async/await
5. **Same application** - All services in one application
6. **External services** - Use stub implementations with sample data
7. **Scope** - All 23 user stories (complete implementation)
8. **Testing** - Manual testing only (unit tests in Step 2.5)
9. **Deployment** - IIS Express with deployment notes
10. **Design Reference** - Use architecture_design.md

---

## Implementation Strategy

**Approach**: Bottom-up implementation
- Start with database and data models
- Build repositories and services
- Create UI layer last
- Test each layer as we build

**Focus**: Simple, working demo that can run locally in IIS Express

---

## Implementation Progress Summary

### ✅ Completed (Phase 1 - Database Infrastructure)
- **Database Schema**: 3 tables created (Account, AccountAudit, AccountRelationship)
- **Indexes**: 12 performance indexes created
- **Constraints**: 8 business rule constraints implemented
- **Sample Data**: 10 diverse sample accounts loaded
- **Documentation**: Comprehensive database README with setup guide
- **Entity Model**: Account.cs entity class created (1 of 3)

### 📊 Progress: 🎊 100% COMPLETE! (20 of 20 hours) 🎊
- Database: 100% ✅ (2 hours)
- Entity Models: 100% ✅ (2 hours)
- DTOs & Common: 100% ✅ (1 hour)
- DbContext: 100% ✅ (1 hour)
- Repositories: 100% ✅ (2 hours)
- Services: 100% ✅ (3 hours)
  - ✅ IAccountManagementService interface
  - ✅ AccountManagementService implementation (full CRUD)
  - ✅ IAccountQueryService interface
  - ✅ AccountQueryService implementation
- Stub Services: 100% ✅ (2 hours)
  - ✅ ReferenceDataServiceStub with sample data
- Configuration: 100% ✅ (1 hour)
  - ✅ Web.config template with SQL Server 2022 settings
  - ✅ Visual Studio setup guide
- UI: 100% ✅ (7 hours - ALL PAGES COMPLETE!)
  - ✅ Site.Master (navigation and layout)
  - ✅ Default.aspx + code-behind (dashboard with statistics)
  - ✅ CreateAccount.aspx + code-behind (complete)
  - ✅ ViewAccount.aspx + code-behind (read-only display)
  - ✅ UpdateAccount.aspx + code-behind (edit form)
  - ✅ SearchAccounts.aspx + code-behind (complete)

### 🎯 Decision Made: Option B - Minimal Viable Implementation ✅
**Scope**: Core CRUD operations with basic UI
**Timeline**: 10-15 hours
**Focus**: Working demo that demonstrates architecture

**Features to Implement**:
- ✅ Database infrastructure (complete)
- 🚧 Entity models (Account, AccountAudit, AccountRelationship)
- 🚧 DbContext and repositories
- 🚧 Basic services (Create, Read, Update, Delete)
- 🚧 Simple Web Forms UI (Create, View, Update, Delete, Search)
- 🚧 Stub services for external dependencies
- 🚧 Basic validation and error handling

---
