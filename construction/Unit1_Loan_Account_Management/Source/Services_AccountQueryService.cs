/*
================================================================================
MANSERV Loan Account Management System
Service Implementation: AccountQueryService
================================================================================
Purpose: Implement account query operations (read-only)
Pattern: Service-Oriented Architecture
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using ManservLoanSystem.Models.Common;
using ManservLoanSystem.Models.DTOs;
using ManservLoanSystem.Models.Entities;
using ManservLoanSystem.Repositories;

namespace ManservLoanSystem.Services
{
    /// <summary>
    /// Service implementation for account query operations
    /// Provides read-only access to account data
    /// </summary>
    public class AccountQueryService : IAccountQueryService
    {
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        public AccountQueryService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        /// <summary>
        /// Search accounts by multiple criteria
        /// </summary>
        public ServiceResponse<List<AccountDTO>> SearchAccounts(
            string referenceNumber = null,
            string customerName = null,
            string centerCode = null,
            string status = null,
            string accountType = null,
            string userId = null)
        {
            try
            {
                // TODO: Apply center/branch restrictions based on userId
                // var authorizedCenters = GetUserAuthorizedCenters(userId);

                var accounts = _accountRepository.Search(
                    referenceNumber,
                    customerName,
                    centerCode,
                    status,
                    accountType);

                var accountDTOs = accounts.Select(MapEntityToDTO).ToList();

                return ServiceResponse<List<AccountDTO>>.Success(accountDTOs);
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return ServiceResponse<List<AccountDTO>>.Failure(
                    ErrorCodes.SYSTEM_ERROR,
                    $"Error searching accounts: {ex.Message}");
            }
        }

        /// <summary>
        /// Get all accounts (with optional status filter)
        /// </summary>
        public ServiceResponse<List<AccountDTO>> GetAllAccounts(string status = null, string userId = null)
        {
            try
            {
                List<Account> accounts;

                if (!string.IsNullOrEmpty(status))
                    accounts = _accountRepository.GetByStatus(status);
                else
                    accounts = _accountRepository.GetAll(includeDeleted: false);

                var accountDTOs = accounts.Select(MapEntityToDTO).ToList();

                return ServiceResponse<List<AccountDTO>>.Success(accountDTOs);
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return ServiceResponse<List<AccountDTO>>.Failure(
                    ErrorCodes.SYSTEM_ERROR,
                    $"Error retrieving accounts: {ex.Message}");
            }
        }

        /// <summary>
        /// Check if account exists
        /// </summary>
        public ServiceResponse<bool> AccountExists(int accountId)
        {
            try
            {
                var account = _accountRepository.GetById(accountId);
                return ServiceResponse<bool>.Success(account != null);
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return ServiceResponse<bool>.Failure(
                    ErrorCodes.SYSTEM_ERROR,
                    $"Error checking account existence: {ex.Message}");
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// Map Entity to DTO
        /// </summary>
        private AccountDTO MapEntityToDTO(Account entity)
        {
            return new AccountDTO
            {
                AccountId = entity.AccountId,
                ReferenceNumber = entity.ReferenceNumber,
                PreviousReferenceNumber = entity.PreviousReferenceNumber,
                CRIBIDNumber = entity.CRIBIDNumber,
                CustomerName = entity.CustomerName,
                NIDSSAccountNumber = entity.NIDSSAccountNumber,
                LongName = entity.LongName,
                CenterCode = entity.CenterCode,
                BudgetUnit = entity.BudgetUnit,
                Corporation = entity.Corporation,
                BookCode = entity.BookCode,
                EconomicActivityCode = entity.EconomicActivityCode,
                OriginalReleaseDate = entity.OriginalReleaseDate,
                StartOfTerm = entity.StartOfTerm,
                MaturityDate = entity.MaturityDate,
                AccountType = entity.AccountType,
                Purpose = entity.Purpose,
                FundSource = entity.FundSource,
                LendingProgram = entity.LendingProgram,
                Area = entity.Area,
                IsRestructured = entity.IsRestructured,
                TypeOfCredit = entity.TypeOfCredit,
                MaturityCode = entity.MaturityCode,
                PurposeOfCredit = entity.PurposeOfCredit,
                NumberOfRecords = entity.NumberOfRecords,
                IsGuaranteed = entity.IsGuaranteed,
                GuaranteedBy = entity.GuaranteedBy,
                IsUnderLitigation = entity.IsUnderLitigation,
                LitigationDate = entity.LitigationDate,
                LoanStatus = entity.LoanStatus,
                LoanProjectType = entity.LoanProjectType,
                Currency = entity.Currency,
                Status = entity.Status,
                IsDraft = entity.IsDraft,
                ClosureDate = entity.ClosureDate
            };
        }

        #endregion
    }
}
