/*
================================================================================
MANSERV Loan Account Management System
Repository Implementation: AccountRepository
================================================================================
Purpose: Implement account data access operations using Entity Framework 6.x
Pattern: Repository Pattern
Target: SQL Server 2022
Author: System Generated
Date: December 5, 2025

IMPLEMENTATION NOTES:
- Uses Entity Framework 6.x for data access
- Synchronous operations (no async/await as per requirements)
- Includes error handling and logging placeholders
- SQL Server 2022 optimized queries
================================================================================
*/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ManservLoanSystem.Data;
using ManservLoanSystem.Models.Entities;

namespace ManservLoanSystem.Repositories
{
    /// <summary>
    /// Repository implementation for Account entity
    /// Uses Entity Framework 6.x for data access
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly ManservDbContext _context;
        private bool _disposed = false;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        public AccountRepository(ManservDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region CRUD Operations

        /// <summary>
        /// Create a new account
        /// </summary>
        public int Create(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return account.AccountId;
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                // Log.Error($"Error creating account: {ex.Message}", ex);
                throw new Exception($"Error creating account: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get account by internal ID
        /// </summary>
        public Account GetById(int accountId)
        {
            try
            {
                return _context.Accounts
                    .FirstOrDefault(a => a.AccountId == accountId && a.Status != "Deleted");
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error retrieving account: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get account by reference number
        /// </summary>
        public Account GetByReferenceNumber(string referenceNumber)
        {
            try
            {
                return _context.Accounts
                    .FirstOrDefault(a => a.ReferenceNumber == referenceNumber && a.Status != "Deleted");
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error retrieving account: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        public int Update(Account account)
        {
            try
            {
                var existing = _context.Accounts.Find(account.AccountId);
                if (existing == null)
                    throw new Exception("Account not found");

                // Update properties
                _context.Entry(existing).CurrentValues.SetValues(account);
                
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error updating account: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Soft delete account
        /// </summary>
        public bool Delete(int accountId, string userId)
        {
            try
            {
                var account = _context.Accounts.Find(accountId);
                if (account == null)
                    return false;

                // Soft delete - mark as deleted
                account.Status = "Deleted";
                account.DeletedBy = userId;
                account.DeletedDate = DateTime.Now;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error deleting account: {ex.Message}", ex);
            }
        }

        #endregion

        #region Query Operations

        /// <summary>
        /// Get all accounts
        /// </summary>
        public List<Account> GetAll(bool includeDeleted = false)
        {
            try
            {
                var query = _context.Accounts.AsQueryable();

                if (!includeDeleted)
                    query = query.Where(a => a.Status != "Deleted");

                return query.OrderByDescending(a => a.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error retrieving accounts: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Search accounts by criteria
        /// </summary>
        public List<Account> Search(
            string referenceNumber = null,
            string customerName = null,
            string centerCode = null,
            string status = null,
            string accountType = null)
        {
            try
            {
                var query = _context.Accounts.AsQueryable();

                // Exclude deleted accounts by default
                query = query.Where(a => a.Status != "Deleted");

                // Apply filters
                if (!string.IsNullOrEmpty(referenceNumber))
                    query = query.Where(a => a.ReferenceNumber.Contains(referenceNumber));

                if (!string.IsNullOrEmpty(customerName))
                    query = query.Where(a => a.CustomerName.Contains(customerName));

                if (!string.IsNullOrEmpty(centerCode))
                    query = query.Where(a => a.CenterCode == centerCode);

                if (!string.IsNullOrEmpty(status))
                    query = query.Where(a => a.Status == status);

                if (!string.IsNullOrEmpty(accountType))
                    query = query.Where(a => a.AccountType == accountType);

                return query.OrderByDescending(a => a.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error searching accounts: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Check if reference number exists
        /// </summary>
        public bool ExistsByReferenceNumber(string referenceNumber, int? excludeAccountId = null)
        {
            try
            {
                var query = _context.Accounts
                    .Where(a => a.ReferenceNumber == referenceNumber && a.Status != "Deleted");

                if (excludeAccountId.HasValue)
                    query = query.Where(a => a.AccountId != excludeAccountId.Value);

                return query.Any();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error checking reference number: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get accounts by center code
        /// </summary>
        public List<Account> GetByCenterCode(string centerCode)
        {
            try
            {
                return _context.Accounts
                    .Where(a => a.CenterCode == centerCode && a.Status != "Deleted")
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error retrieving accounts by center: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get accounts by status
        /// </summary>
        public List<Account> GetByStatus(string status)
        {
            try
            {
                return _context.Accounts
                    .Where(a => a.Status == status)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error retrieving accounts by status: {ex.Message}", ex);
            }
        }

        #endregion

        #region Audit Operations

        /// <summary>
        /// Create audit log entry
        /// </summary>
        public void CreateAudit(AccountAudit audit)
        {
            try
            {
                _context.AccountAudits.Add(audit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                // Don't throw - audit failures shouldn't break main operations
                // Log.Error($"Error creating audit entry: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get audit history for an account
        /// </summary>
        public List<AccountAudit> GetAuditHistory(int accountId)
        {
            try
            {
                return _context.AccountAudits
                    .Where(a => a.AccountId == accountId)
                    .OrderByDescending(a => a.ChangedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error retrieving audit history: {ex.Message}", ex);
            }
        }

        #endregion

        #region Relationship Operations

        /// <summary>
        /// Create account relationship
        /// </summary>
        public void CreateRelationship(AccountRelationship relationship)
        {
            try
            {
                _context.AccountRelationships.Add(relationship);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error creating relationship: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get relationships for an account
        /// </summary>
        public List<AccountRelationship> GetRelationships(int accountId)
        {
            try
            {
                return _context.AccountRelationships
                    .Where(r => r.SourceAccountId == accountId || r.TargetAccountId == accountId)
                    .OrderByDescending(r => r.CreatedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                throw new Exception($"Error retrieving relationships: {ex.Message}", ex);
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Dispose of resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected dispose method
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
