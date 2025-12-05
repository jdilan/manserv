/*
================================================================================
MANSERV Loan Account Management System
Service Implementation: AccountManagementService
================================================================================
Purpose: Implement account management business operations
Pattern: Service-Oriented Architecture with Repository Pattern
Author: System Generated
Date: December 5, 2025

IMPLEMENTATION NOTES:
- Orchestrates business logic and validation
- Uses repository for data access
- Creates audit trail for all operations
- Returns standardized ServiceResponse<T>
- Handles errors gracefully
================================================================================
*/

using System;
using System.Linq;
using ManservLoanSystem.Data;
using ManservLoanSystem.Models.Common;
using ManservLoanSystem.Models.DTOs;
using ManservLoanSystem.Models.Entities;
using ManservLoanSystem.Repositories;

namespace ManservLoanSystem.Services
{
    /// <summary>
    /// Service implementation for account management operations
    /// </summary>
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        public AccountManagementService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        /// <summary>
        /// Create a new loan account
        /// </summary>
        public ServiceResponse<int> CreateAccount(AccountDTO accountDTO, string userId)
        {
            try
            {
                // Validate mandatory fields
                var validationErrors = ValidateMandatoryFields(accountDTO);
                if (validationErrors.Any())
                    return ServiceResponse<int>.Failure(validationErrors);

                // Validate date relationships
                var dateValidation = ValidateDateRelationships(accountDTO);
                if (!dateValidation.IsSuccess)
                    return ServiceResponse<int>.Failure(dateValidation.Errors);

                // Check for duplicate reference number (warning only)
                if (_accountRepository.ExistsByReferenceNumber(accountDTO.ReferenceNumber))
                {
                    // Log warning but allow creation
                    // TODO: Add logging
                    // Log.Warning($"Duplicate reference number: {accountDTO.ReferenceNumber}");
                }

                // Map DTO to Entity
                var account = MapDTOToEntity(accountDTO);
                account.CreatedBy = userId;
                account.CreatedDate = DateTime.Now;
                account.Status = accountDTO.IsDraft ? "Active" : "Active";

                // Auto-populate fields
                AutoPopulateFields(account);

                // Create account
                int accountId = _accountRepository.Create(account);

                // Create audit log
                CreateAuditLog(accountId, accountDTO.ReferenceNumber, "Create", userId, null, "Account created");

                return ServiceResponse<int>.Success(accountId);
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                // Log.Error($"Error creating account: {ex.Message}", ex);
                return ServiceResponse<int>.Failure(ErrorCodes.SYSTEM_ERROR, $"Error creating account: {ex.Message}");
            }
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        public ServiceResponse<AccountDTO> GetAccount(int accountId, string userId)
        {
            try
            {
                var account = _accountRepository.GetById(accountId);
                
                if (account == null)
                    return ServiceResponse<AccountDTO>.Failure(ErrorCodes.NOT_FOUND, "Account not found");

                // TODO: Check user access to center/branch
                // if (!HasAccessToCenter(userId, account.CenterCode))
                //     return ServiceResponse<AccountDTO>.Failure(ErrorCodes.UNAUTHORIZED, "Access denied");

                var accountDTO = MapEntityToDTO(account);
                return ServiceResponse<AccountDTO>.Success(accountDTO);
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return ServiceResponse<AccountDTO>.Failure(ErrorCodes.SYSTEM_ERROR, $"Error retrieving account: {ex.Message}");
            }
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        public ServiceResponse<bool> UpdateAccount(int accountId, AccountDTO accountDTO, string userId)
        {
            try
            {
                // Get existing account
                var existingAccount = _accountRepository.GetById(accountId);
                if (existingAccount == null)
                    return ServiceResponse<bool>.Failure(ErrorCodes.NOT_FOUND, "Account not found");

                // Validate mandatory fields
                var validationErrors = ValidateMandatoryFields(accountDTO);
                if (validationErrors.Any())
                    return ServiceResponse<bool>.Failure(validationErrors);

                // Validate date relationships
                var dateValidation = ValidateDateRelationships(accountDTO);
                if (!dateValidation.IsSuccess)
                    return ServiceResponse<bool>.Failure(dateValidation.Errors);

                // Map DTO to existing entity
                UpdateEntityFromDTO(existingAccount, accountDTO);
                existingAccount.ModifiedBy = userId;
                existingAccount.ModifiedDate = DateTime.Now;

                // Auto-populate fields
                AutoPopulateFields(existingAccount);

                // Update account
                _accountRepository.Update(existingAccount);

                // Create audit log
                CreateAuditLog(accountId, accountDTO.ReferenceNumber, "Update", userId, null, "Account updated");

                return ServiceResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return ServiceResponse<bool>.Failure(ErrorCodes.SYSTEM_ERROR, $"Error updating account: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete account (soft delete)
        /// </summary>
        public ServiceResponse<bool> DeleteAccount(int accountId, string userId)
        {
            try
            {
                // Get existing account
                var account = _accountRepository.GetById(accountId);
                if (account == null)
                    return ServiceResponse<bool>.Failure(ErrorCodes.NOT_FOUND, "Account not found");

                // TODO: Check for dependencies (transactions, balances, collateral)
                // This would require integration with Financial Management Unit

                // Soft delete
                bool deleted = _accountRepository.Delete(accountId, userId);

                if (deleted)
                {
                    // Create audit log
                    CreateAuditLog(accountId, account.ReferenceNumber, "Delete", userId, null, "Account deleted");
                    return ServiceResponse<bool>.Success(true);
                }

                return ServiceResponse<bool>.Failure(ErrorCodes.SYSTEM_ERROR, "Failed to delete account");
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return ServiceResponse<bool>.Failure(ErrorCodes.SYSTEM_ERROR, $"Error deleting account: {ex.Message}");
            }
        }

        /// <summary>
        /// Check for duplicate reference number
        /// </summary>
        public ServiceResponse<bool> CheckDuplicateReferenceNumber(string referenceNumber, int? excludeAccountId = null)
        {
            try
            {
                bool exists = _accountRepository.ExistsByReferenceNumber(referenceNumber, excludeAccountId);
                return ServiceResponse<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return ServiceResponse<bool>.Failure(ErrorCodes.SYSTEM_ERROR, $"Error checking duplicate: {ex.Message}");
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// Validate mandatory fields
        /// </summary>
        private System.Collections.Generic.List<ServiceError> ValidateMandatoryFields(AccountDTO accountDTO)
        {
            var errors = new System.Collections.Generic.List<ServiceError>();

            if (string.IsNullOrWhiteSpace(accountDTO.ReferenceNumber))
                errors.Add(new ServiceError { ErrorCode = ErrorCodes.MANDATORY_FIELD, Message = "Reference Number is required", Field = "ReferenceNumber" });

            if (string.IsNullOrWhiteSpace(accountDTO.PreviousReferenceNumber))
                errors.Add(new ServiceError { ErrorCode = ErrorCodes.MANDATORY_FIELD, Message = "Previous Reference Number is required", Field = "PreviousReferenceNumber" });

            if (string.IsNullOrWhiteSpace(accountDTO.CustomerName))
                errors.Add(new ServiceError { ErrorCode = ErrorCodes.MANDATORY_FIELD, Message = "Customer Name is required", Field = "CustomerName" });

            if (string.IsNullOrWhiteSpace(accountDTO.LongName))
                errors.Add(new ServiceError { ErrorCode = ErrorCodes.MANDATORY_FIELD, Message = "Long Name is required", Field = "LongName" });

            // Validate conditional fields
            if (IsAccountTypeThatRequiresPurpose(accountDTO.AccountType) && string.IsNullOrWhiteSpace(accountDTO.Purpose))
                errors.Add(new ServiceError { ErrorCode = ErrorCodes.CONDITIONAL_REQUIRED, Message = "Purpose is required for this account type", Field = "Purpose" });

            return errors;
        }

        /// <summary>
        /// Validate date relationships
        /// </summary>
        private ServiceResponse<bool> ValidateDateRelationships(AccountDTO accountDTO)
        {
            // Business Rule: StartOfTerm must equal OriginalReleaseDate
            if (accountDTO.StartOfTerm.Date != accountDTO.OriginalReleaseDate.Date)
                return ServiceResponse<bool>.Failure(ErrorCodes.INVALID_DATE_RANGE, "Start of Term must equal Original Release Date");

            // Business Rule: MaturityDate must be greater than StartOfTerm
            if (accountDTO.MaturityDate <= accountDTO.StartOfTerm)
                return ServiceResponse<bool>.Failure(ErrorCodes.INVALID_DATE_RANGE, "Maturity Date must be after Start of Term");

            return ServiceResponse<bool>.Success(true);
        }

        /// <summary>
        /// Check if account type requires purpose field
        /// </summary>
        private bool IsAccountTypeThatRequiresPurpose(string accountType)
        {
            var typesRequiringPurpose = new[] { "AA", "AI", "R", "RDC", "RDE", "RDH" };
            return typesRequiringPurpose.Contains(accountType);
        }

        /// <summary>
        /// Auto-populate fields based on business rules
        /// </summary>
        private void AutoPopulateFields(Account account)
        {
            // Auto-populate TypeOfCredit based on account status
            if (account.IsUnderLitigation)
                account.TypeOfCredit = "LITIG";
            else if (account.LoanStatus == "PDO")
                account.TypeOfCredit = "PDO";
            else
                account.TypeOfCredit = "CUR";

            // Auto-populate PurposeOfCredit based on AccountType
            // This would normally come from reference data
            account.PurposeOfCredit = "P"; // Default value
        }

        /// <summary>
        /// Map DTO to Entity
        /// </summary>
        private Account MapDTOToEntity(AccountDTO dto)
        {
            return new Account
            {
                ReferenceNumber = dto.ReferenceNumber,
                PreviousReferenceNumber = dto.PreviousReferenceNumber,
                CRIBIDNumber = dto.CRIBIDNumber,
                CustomerName = dto.CustomerName,
                NIDSSAccountNumber = dto.NIDSSAccountNumber,
                LongName = dto.LongName,
                CenterCode = dto.CenterCode,
                BudgetUnit = dto.BudgetUnit,
                Corporation = dto.Corporation,
                BookCode = dto.BookCode,
                EconomicActivityCode = dto.EconomicActivityCode,
                OriginalReleaseDate = dto.OriginalReleaseDate,
                StartOfTerm = dto.StartOfTerm,
                MaturityDate = dto.MaturityDate,
                AccountType = dto.AccountType,
                Purpose = dto.Purpose,
                FundSource = dto.FundSource,
                LendingProgram = dto.LendingProgram,
                Area = dto.Area,
                IsRestructured = dto.IsRestructured,
                TypeOfCredit = dto.TypeOfCredit,
                MaturityCode = dto.MaturityCode,
                PurposeOfCredit = dto.PurposeOfCredit,
                NumberOfRecords = dto.NumberOfRecords,
                IsGuaranteed = dto.IsGuaranteed,
                GuaranteedBy = dto.GuaranteedBy,
                IsUnderLitigation = dto.IsUnderLitigation,
                LitigationDate = dto.LitigationDate,
                LoanStatus = dto.LoanStatus,
                LoanProjectType = dto.LoanProjectType,
                Currency = dto.Currency,
                Status = dto.Status,
                IsDraft = dto.IsDraft,
                ClosureDate = dto.ClosureDate
            };
        }

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

        /// <summary>
        /// Update entity from DTO
        /// </summary>
        private void UpdateEntityFromDTO(Account entity, AccountDTO dto)
        {
            // Don't update AccountId or ReferenceNumber (primary keys)
            entity.PreviousReferenceNumber = dto.PreviousReferenceNumber;
            entity.CRIBIDNumber = dto.CRIBIDNumber;
            entity.CustomerName = dto.CustomerName;
            entity.NIDSSAccountNumber = dto.NIDSSAccountNumber;
            entity.LongName = dto.LongName;
            entity.CenterCode = dto.CenterCode;
            entity.BudgetUnit = dto.BudgetUnit;
            entity.Corporation = dto.Corporation;
            entity.BookCode = dto.BookCode;
            entity.EconomicActivityCode = dto.EconomicActivityCode;
            entity.OriginalReleaseDate = dto.OriginalReleaseDate;
            entity.StartOfTerm = dto.StartOfTerm;
            entity.MaturityDate = dto.MaturityDate;
            entity.AccountType = dto.AccountType;
            entity.Purpose = dto.Purpose;
            entity.FundSource = dto.FundSource;
            entity.LendingProgram = dto.LendingProgram;
            entity.Area = dto.Area;
            entity.IsRestructured = dto.IsRestructured;
            entity.MaturityCode = dto.MaturityCode;
            entity.NumberOfRecords = dto.NumberOfRecords;
            entity.IsGuaranteed = dto.IsGuaranteed;
            entity.GuaranteedBy = dto.GuaranteedBy;
            entity.IsUnderLitigation = dto.IsUnderLitigation;
            entity.LitigationDate = dto.LitigationDate;
            entity.LoanStatus = dto.LoanStatus;
            entity.LoanProjectType = dto.LoanProjectType;
            entity.Currency = dto.Currency;
            entity.IsDraft = dto.IsDraft;
        }

        /// <summary>
        /// Create audit log entry
        /// </summary>
        private void CreateAuditLog(int accountId, string referenceNumber, string action, string userId, string fieldName, string comments)
        {
            try
            {
                var audit = new AccountAudit
                {
                    AccountId = accountId,
                    ReferenceNumber = referenceNumber,
                    Action = action,
                    FieldName = fieldName,
                    ChangedBy = userId,
                    ChangedDate = DateTime.Now,
                    UserRole = "User", // TODO: Get actual role
                    Comments = comments
                };

                _accountRepository.CreateAudit(audit);
            }
            catch (Exception ex)
            {
                // Don't throw - audit failures shouldn't break main operations
                // TODO: Add logging
                // Log.Error($"Error creating audit log: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
