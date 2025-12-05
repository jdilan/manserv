/*
================================================================================
MANSERV Loan Account Management System
Service Interface: IAccountManagementService
================================================================================
Purpose: Define contract for account management business operations
Pattern: Service-Oriented Architecture
Author: System Generated
Date: December 5, 2025

BUSINESS OPERATIONS:
- Create new loan accounts with validation
- Retrieve account details
- Update account information with audit trail
- Delete accounts (soft delete)
- Validate duplicate reference numbers
================================================================================
*/

using ManservLoanSystem.Models.DTOs;
using ManservLoanSystem.Models.Common;

namespace ManservLoanSystem.Services
{
    /// <summary>
    /// Service interface for account management operations
    /// Handles core CRUD operations with business logic and validation
    /// </summary>
    public interface IAccountManagementService
    {
        /// <summary>
        /// Create a new loan account
        /// </summary>
        /// <param name="accountDTO">Account data</param>
        /// <param name="userId">User creating the account</param>
        /// <returns>ServiceResponse with new AccountId or errors</returns>
        ServiceResponse<int> CreateAccount(AccountDTO accountDTO, string userId);

        /// <summary>
        /// Get account by ID
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="userId">User requesting the account</param>
        /// <returns>ServiceResponse with AccountDTO or errors</returns>
        ServiceResponse<AccountDTO> GetAccount(int accountId, string userId);

        /// <summary>
        /// Update existing account
        /// </summary>
        /// <param name="accountId">Account ID to update</param>
        /// <param name="accountDTO">Updated account data</param>
        /// <param name="userId">User updating the account</param>
        /// <returns>ServiceResponse with success/failure</returns>
        ServiceResponse<bool> UpdateAccount(int accountId, AccountDTO accountDTO, string userId);

        /// <summary>
        /// Delete account (soft delete)
        /// </summary>
        /// <param name="accountId">Account ID to delete</param>
        /// <param name="userId">User deleting the account</param>
        /// <returns>ServiceResponse with success/failure</returns>
        ServiceResponse<bool> DeleteAccount(int accountId, string userId);

        /// <summary>
        /// Check if reference number already exists (for duplicate warning)
        /// </summary>
        /// <param name="referenceNumber">Reference number to check</param>
        /// <param name="excludeAccountId">Account ID to exclude (for updates)</param>
        /// <returns>ServiceResponse with true if duplicate exists</returns>
        ServiceResponse<bool> CheckDuplicateReferenceNumber(string referenceNumber, int? excludeAccountId = null);
    }
}
