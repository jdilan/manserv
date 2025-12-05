/*
================================================================================
MANSERV Loan Account Management System
Service Interface: IAccountQueryService
================================================================================
Purpose: Define contract for account query operations (read-only)
Pattern: Service-Oriented Architecture
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System.Collections.Generic;
using ManservLoanSystem.Models.DTOs;
using ManservLoanSystem.Models.Common;

namespace ManservLoanSystem.Services
{
    /// <summary>
    /// Service interface for account query operations
    /// Provides read-only access to account data
    /// </summary>
    public interface IAccountQueryService
    {
        /// <summary>
        /// Search accounts by multiple criteria
        /// </summary>
        ServiceResponse<List<AccountDTO>> SearchAccounts(
            string referenceNumber = null,
            string customerName = null,
            string centerCode = null,
            string status = null,
            string accountType = null,
            string userId = null);

        /// <summary>
        /// Get all accounts (with optional status filter)
        /// </summary>
        ServiceResponse<List<AccountDTO>> GetAllAccounts(string status = null, string userId = null);

        /// <summary>
        /// Check if account exists
        /// </summary>
        ServiceResponse<bool> AccountExists(int accountId);
    }
}
