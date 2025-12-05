/*
================================================================================
MANSERV Loan Account Management System
Repository Interface: IAccountRepository
================================================================================
Purpose: Define contract for account data access operations
Pattern: Repository Pattern for data access abstraction
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;
using System.Collections.Generic;
using ManservLoanSystem.Models.Entities;

namespace ManservLoanSystem.Repositories
{
    /// <summary>
    /// Repository interface for Account entity
    /// Abstracts data access logic from business logic
    /// </summary>
    public interface IAccountRepository : IDisposable
    {
        #region CRUD Operations

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="account">Account entity to create</param>
        /// <returns>Generated AccountId</returns>
        int Create(Account account);

        /// <summary>
        /// Get account by internal ID
        /// </summary>
        /// <param name="accountId">Internal account ID</param>
        /// <returns>Account entity or null if not found</returns>
        Account GetById(int accountId);

        /// <summary>
        /// Get account by reference number (business key)
        /// </summary>
        /// <param name="referenceNumber">Reference number</param>
        /// <returns>Account entity or null if not found</returns>
        Account GetByReferenceNumber(string referenceNumber);

        /// <summary>
        /// Update existing account
        /// </summary>
        /// <param name="account">Account entity with updated values</param>
        /// <returns>Number of rows affected</returns>
        int Update(Account account);

        /// <summary>
        /// Soft delete account (marks as deleted)
        /// </summary>
        /// <param name="accountId">Account ID to delete</param>
        /// <param name="userId">User performing the deletion</param>
        /// <returns>True if successful</returns>
        bool Delete(int accountId, string userId);

        #endregion

        #region Query Operations

        /// <summary>
        /// Get all accounts (with optional filtering)
        /// </summary>
        /// <param name="includeDeleted">Include soft-deleted accounts</param>
        /// <returns>List of accounts</returns>
        List<Account> GetAll(bool includeDeleted = false);

        /// <summary>
        /// Search accounts by criteria
        /// </summary>
        /// <param name="referenceNumber">Reference number (partial match)</param>
        /// <param name="customerName">Customer name (partial match)</param>
        /// <param name="centerCode">Center code (exact match)</param>
        /// <param name="status">Account status (exact match)</param>
        /// <param name="accountType">Account type (exact match)</param>
        /// <returns>List of matching accounts</returns>
        List<Account> Search(
            string referenceNumber = null,
            string customerName = null,
            string centerCode = null,
            string status = null,
            string accountType = null);

        /// <summary>
        /// Check if reference number already exists
        /// </summary>
        /// <param name="referenceNumber">Reference number to check</param>
        /// <param name="excludeAccountId">Account ID to exclude from check (for updates)</param>
        /// <returns>True if exists</returns>
        bool ExistsByReferenceNumber(string referenceNumber, int? excludeAccountId = null);

        /// <summary>
        /// Get accounts by center code
        /// </summary>
        /// <param name="centerCode">Center code</param>
        /// <returns>List of accounts</returns>
        List<Account> GetByCenterCode(string centerCode);

        /// <summary>
        /// Get accounts by status
        /// </summary>
        /// <param name="status">Account status</param>
        /// <returns>List of accounts</returns>
        List<Account> GetByStatus(string status);

        #endregion

        #region Audit Operations

        /// <summary>
        /// Create audit log entry
        /// </summary>
        /// <param name="audit">Audit entry to create</param>
        void CreateAudit(AccountAudit audit);

        /// <summary>
        /// Get audit history for an account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>List of audit entries</returns>
        List<AccountAudit> GetAuditHistory(int accountId);

        #endregion

        #region Relationship Operations

        /// <summary>
        /// Create account relationship
        /// </summary>
        /// <param name="relationship">Relationship to create</param>
        void CreateRelationship(AccountRelationship relationship);

        /// <summary>
        /// Get relationships for an account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>List of relationships</returns>
        List<AccountRelationship> GetRelationships(int accountId);

        #endregion
    }
}
