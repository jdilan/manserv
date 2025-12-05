# Step 2.1: Architecture Design Plan - Unit 1: Loan Account Management

## Overview
Design a Feature-Based Architecture with Service-Oriented patterns for Unit 1: Loan Account Management using ASP.NET 4.7 and SQL Server 2022. The architecture will be straightforward for teams transitioning from traditional monolithic waterfall development.

## Prerequisites
- ✅ Unit 1 requirements document reviewed (inception/units/unit1_loan_account_management.md)
- ✅ Integration contracts reviewed (inception/units/integration_contract.md)
- ✅ User stories analyzed (23 user stories across General, Loan Info, and Account Operations)

---

## Plan Steps

### Step 1: Create Project Structure
- [x] Create `/construction/` folder in root directory
- [x] Create `/construction/Unit1_Loan_Account_Management/` subfolder
- [x] Create `architecture_design.md` file in the unit folder

### Step 2: Define Architecture Overview
- [x] Document architecture style (Feature-Based with Service-Oriented patterns)
- [x] Define technology stack (ASP.NET 4.7, SQL Server 2022)
- [x] Outline architectural principles and design goals
- [x] Document separation of concerns approach
- [x] Define layering strategy (Presentation, Business Services, Data Access)

### Step 3: Identify and Document Feature Components
- [x] Map 23 user stories to feature components organized by business capability
- [x] Define Feature 1: General Account Management (US-001 to US-004)
- [x] Define Feature 2: Loan Information Management (US-015 to US-023)
- [x] Define Feature 3: Account Operations (US-048 to US-051)
- [x] Document responsibilities for each feature component
- [x] Define feature boundaries and interfaces

### Step 4: Design Business Services Layer
- [x] Design Account Management Service (create, read, update, delete operations)
- [x] Design Account Query Service (search, summary, validation operations)
- [x] Design Account Lifecycle Service (copy, close, archive operations)
- [x] Design Loan Information Service (dates, classification, status management)
- [x] Document service contracts and method signatures
- [x] Define service dependencies and integration points

### Step 5: Design Data Models
- [x] Map MANSERV.DBF fields to data model entities
- [x] Design AccountGeneral entity (General section fields)
- [x] Design LoanInformation entity (Loan Info section fields)
- [x] Design AccountStatus entity (status tracking)
- [x] Design AccountAudit entity (audit trail)
- [x] Define entity relationships and foreign keys
- [x] Document field mappings to legacy DBF structure
- [x] **Decision: Hybrid approach - normalized key entities with practical flat structures**

### Step 6: Design Repository Pattern for Data Access
- [x] Design IAccountRepository interface
- [x] Design ILoanInfoRepository interface
- [x] Design IAuditRepository interface
- [x] Define repository methods (CRUD operations, queries)
- [x] Document SQL Server 2022 data access approach
- [x] Define connection management strategy
- [x] Document transaction management approach

### Step 7: Define Service Interfaces for Integration
- [x] Document exposed services (Account Management, Query, Lifecycle)
- [x] Document consumed services from other units:
  - Reference Data Management Unit
  - Compliance & Validation Unit
  - Customer Management Unit
- [x] Define service contract formats (request/response)
- [x] Define error handling and response patterns
- [x] Document authentication and authorization requirements

### Step 8: Design Validation and Business Rules Layer
- [x] Document mandatory field validations
- [x] Document cross-field validation rules (dates, conditional fields)
- [x] Document business rules (status transitions, deletion rules)
- [x] Document auto-population rules
- [x] Define validation service integration points
- [x] **Decision: Hybrid - basic validations local, complex rules via Compliance Unit**

### Step 9: Design Access Control and Security
- [x] Document role-based access control (User, Authorizer, Administrator)
- [x] Document center/branch data access restrictions
- [x] Define permission checks for operations (create, update, delete)
- [x] Document audit trail requirements
- [x] Define integration with Compliance & Validation Unit for access control

### Step 10: Document Integration Points and Dependencies
- [x] Document dependencies on Reference Data Management Unit
- [x] Document dependencies on Compliance & Validation Unit
- [x] Document dependencies on Customer Management Unit
- [x] Document how Financial Management Unit will consume this unit
- [x] Define synchronous API call patterns
- [x] Document error handling and retry logic

### Step 11: Design Component Interaction Patterns
- [x] Document presentation layer to service layer interaction
- [x] Document service layer to repository layer interaction
- [x] Document cross-feature communication patterns
- [x] Document cross-unit service call patterns
- [x] Define transaction boundaries and management

### Step 12: Document Non-Functional Considerations
- [x] Document performance considerations (search, large datasets)
- [x] Document scalability approach
- [x] Document error handling and logging strategy
- [x] Document transaction management approach
- [x] Document deployment considerations

### Step 13: Create Architecture Diagrams (Text-Based)
- [x] Create high-level architecture diagram (layers and components)
- [x] Create feature component diagram
- [x] Create service dependency diagram
- [x] Create data model diagram
- [x] Create integration flow diagram
- [x] **Diagrams created in ASCII format in markdown**

### Step 14: Document Implementation Guidelines
- [x] Provide guidance for teams transitioning from monolithic development
- [x] Document coding standards and conventions
- [x] Document naming conventions for services, repositories, entities
- [x] Provide examples of typical workflows
- [x] Document testing strategy recommendations

### Step 15: Review and Finalize
- [x] Review completeness of architecture design
- [x] Ensure all 23 user stories are covered
- [x] Verify alignment with integration contracts
- [x] Verify no code snippets are included (design only)
- [x] Final review and polish

---

## Questions Requiring Clarification

1. **Data Model Structure**: Should we normalize the data model into multiple related entities, or keep a flatter structure that closely mirrors the legacy MANSERV.DBF table structure? (Step 5)
   - Option A: Normalized (AccountGeneral, LoanInformation, AccountStatus as separate entities)
   - Option B: Flat (Single Account entity with all fields)

2. **Validation Strategy**: Should validation logic be:
   - Option A: Centralized entirely in Compliance & Validation Unit (service calls for all validations)
   - Option B: Duplicated locally for performance with sync to Compliance Unit for audit
   - Option C: Hybrid (basic validations local, complex business rules in Compliance Unit)

3. **Service Communication**: For consumed services from other units, should we:
   - Option A: Direct service-to-service calls (tightly coupled)
   - Option B: Service interfaces with dependency injection (loosely coupled)
   - Option C: Message-based communication (most loosely coupled but more complex)

4. **Legacy DBF Integration**: Should the architecture:
   - Option A: Directly access DBF files through OLEDB/ODBC
   - Option B: Migrate DBF data to SQL Server tables first
   - Option C: Hybrid approach with gradual migration

---

## Deliverable
- `/construction/Unit1_Loan_Account_Management/architecture_design.md` - Comprehensive architecture design document without code snippets

---

## Estimated Effort
- Planning: 30 minutes (completed)
- Execution: 3-4 hours
- Review: 30 minutes

---

**Status**: ✅ COMPLETED
**Completion Date**: December 5, 2025
**Next Action**: Proceed to Step 2.2 - Create Logical Design

## Execution Summary

All 15 steps completed successfully. The architecture design document has been created at:
`/construction/Unit1_Loan_Account_Management/architecture_design.md`

**Key Design Decisions Made**:
1. **Data Model**: Hybrid approach - normalized key entities (Account, AccountAudit, AccountRelationship) with practical flat structure for main Account table
2. **Validation Strategy**: Hybrid - basic validations performed locally for performance, complex business rules delegated to Compliance Unit
3. **Service Communication**: Dependency injection with service interfaces for loose coupling and testability
4. **Legacy Integration**: Migrate DBF data to SQL Server 2022 tables for modern, maintainable architecture

**Deliverable**: Comprehensive 14-section architecture design document covering:
- Architecture overview and principles
- 3 feature components (23 user stories)
- 4 business services with detailed contracts
- 3 data entities with field mappings
- Repository pattern with 3 repositories
- Service interfaces (exposed and consumed)
- Validation and business rules
- Access control and security
- Integration points and dependencies
- Component interactions
- Non-functional considerations
- 5 architecture diagrams (ASCII format)
- Implementation guidelines for teams
