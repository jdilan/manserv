/*
================================================================================
MANSERV Loan Account Management System - Local Testing
Validation Service Mock
================================================================================
Purpose: Mock implementation of IValidationService for local testing
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using System;
using System.Collections.Generic;
using ManservLoanSystem.Models.Common;
using ManservLoanSystem.Models.DTOs;
using ManservLoanSystem.ExternalServices;

namespace ManservLoanSystem.LocalTesting.Mocks
{
    /// <summary>
    /// Mock implementation of validation service
    /// Supports configurable strict/lenient validation modes
    /// </summary>
    public class ValidationServiceMock : IValidationService
    {
        private readonly bool _strictMode;

        public ValidationServiceMock(bool strictMode = false)
        {
            _strictMode = strictMode;
        }

        #region IValidationService Implementation

        public ServiceResponse<ValidationResult> ValidateMandatoryFields(object entityData)
        {
            var account = entityData as AccountDTO;
            if (account == null)
            {
                return ServiceResponse<ValidationResult>.Failure("INVALID_ENTITY", "Entity data is not an AccountDTO");
            }

            var result = new ValidationResult { IsValid = true };

            // Check mandatory fields
            if (string.IsNullOrWhiteSpace(account.ReferenceNumber))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "ReferenceNumber",
                    ErrorCode = "REQUIRED",
                    Message = "Reference Number is required"
                });
            }

            if (string.IsNullOrWhiteSpace(account.PreviousReferenceNumber))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "PreviousReferenceNumber",
                    ErrorCode = "REQUIRED",
                    Message = "Previous Reference Number is required"
                });
            }

            if (string.IsNullOrWhiteSpace(account.CustomerName))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "CustomerName",
                    ErrorCode = "REQUIRED",
                    Message = "Customer Name is required"
                });
            }

            if (string.IsNullOrWhiteSpace(account.LongName))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "LongName",
                    ErrorCode = "REQUIRED",
                    Message = "Long Name is required"
                });
            }

            return ServiceResponse<ValidationResult>.Success(result);
        }

        public ServiceResponse<ValidationResult> ValidateFieldFormats(object entityData)
        {
            var account = entityData as AccountDTO;
            if (account == null)
            {
                return ServiceResponse<ValidationResult>.Failure("INVALID_ENTITY", "Entity data is not an AccountDTO");
            }

            var result = new ValidationResult { IsValid = true };

            // Reference Number format (max 17 characters)
            if (!string.IsNullOrWhiteSpace(account.ReferenceNumber) && account.ReferenceNumber.Length > 17)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "ReferenceNumber",
                    ErrorCode = "INVALID_LENGTH",
                    Message = "Reference Number cannot exceed 17 characters"
                });
            }

            // Customer Name format (max 40 characters)
            if (!string.IsNullOrWhiteSpace(account.CustomerName) && account.CustomerName.Length > 40)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "CustomerName",
                    ErrorCode = "INVALID_LENGTH",
                    Message = "Customer Name cannot exceed 40 characters"
                });
            }

            // Long Name format (max 100 characters)
            if (!string.IsNullOrWhiteSpace(account.LongName) && account.LongName.Length > 100)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "LongName",
                    ErrorCode = "INVALID_LENGTH",
                    Message = "Long Name cannot exceed 100 characters"
                });
            }

            // CRIB ID format (max 10 characters)
            if (!string.IsNullOrWhiteSpace(account.CRIBIDNumber) && account.CRIBIDNumber.Length > 10)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "CRIBIDNumber",
                    ErrorCode = "INVALID_LENGTH",
                    Message = "CRIB ID cannot exceed 10 characters"
                });
            }

            return ServiceResponse<ValidationResult>.Success(result);
        }

        public ServiceResponse<ValidationResult> ValidateCrossFieldRules(object entityData)
        {
            var account = entityData as AccountDTO;
            if (account == null)
            {
                return ServiceResponse<ValidationResult>.Failure("INVALID_ENTITY", "Entity data is not an AccountDTO");
            }

            var result = new ValidationResult { IsValid = true };

            // Rule: Start of Term must equal Original Release Date
            if (account.StartOfTerm != account.OriginalReleaseDate)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "StartOfTerm",
                    ErrorCode = "INVALID_DATE_RELATIONSHIP",
                    Message = "Start of Term must equal Original Release Date"
                });
            }

            // Rule: Maturity Date must be greater than Start of Term
            if (account.MaturityDate <= account.StartOfTerm)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "MaturityDate",
                    ErrorCode = "INVALID_DATE_RELATIONSHIP",
                    Message = "Maturity Date must be greater than Start of Term"
                });
            }

            // Rule: If guaranteed, GuaranteedBy must be specified
            if (account.IsGuaranteed && string.IsNullOrWhiteSpace(account.GuaranteedBy))
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "GuaranteedBy",
                    ErrorCode = "REQUIRED_CONDITIONAL",
                    Message = "Guaranteed By is required when loan is guaranteed"
                });
            }

            // Rule: If under litigation, LitigationDate must be specified
            if (account.IsUnderLitigation && !account.LitigationDate.HasValue)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError
                {
                    Field = "LitigationDate",
                    ErrorCode = "REQUIRED_CONDITIONAL",
                    Message = "Litigation Date is required when loan is under litigation"
                });
            }

            return ServiceResponse<ValidationResult>.Success(result);
        }

        public ServiceResponse<ValidationResult> ValidateConditionalFields(object entityData)
        {
            var account = entityData as AccountDTO;
            if (account == null)
            {
                return ServiceResponse<ValidationResult>.Failure("INVALID_ENTITY", "Entity data is not an AccountDTO");
            }

            var result = new ValidationResult { IsValid = true };

            // Account types that require Purpose field
            var accountTypesRequiringPurpose = new[] { "AA", "AI", "R", "RDC", "RDE", "RDH" };

            if (Array.Exists(accountTypesRequiringPurpose, t => t == account.AccountType))
            {
                if (string.IsNullOrWhiteSpace(account.Purpose))
                {
                    result.IsValid = false;
                    result.Errors.Add(new ValidationError
                    {
                        Field = "Purpose",
                        ErrorCode = "REQUIRED_CONDITIONAL",
                        Message = $"Purpose is required for account type '{account.AccountType}'"
                    });
                }
            }

            return ServiceResponse<ValidationResult>.Success(result);
        }

        #endregion

        #region Additional Validation Methods

        /// <summary>
        /// Validate all rules at once
        /// </summary>
        public ServiceResponse<ValidationResult> ValidateAll(AccountDTO account)
        {
            var combinedResult = new ValidationResult { IsValid = true };

            // Run all validations
            var mandatoryResult = ValidateMandatoryFields(account);
            var formatResult = ValidateFieldFormats(account);
            var crossFieldResult = ValidateCrossFieldRules(account);
            var conditionalResult = ValidateConditionalFields(account);

            // Combine results
            if (!mandatoryResult.Data.IsValid)
            {
                combinedResult.IsValid = false;
                combinedResult.Errors.AddRange(mandatoryResult.Data.Errors);
            }

            if (!formatResult.Data.IsValid)
            {
                combinedResult.IsValid = false;
                combinedResult.Errors.AddRange(formatResult.Data.Errors);
            }

            if (!crossFieldResult.Data.IsValid)
            {
                combinedResult.IsValid = false;
                combinedResult.Errors.AddRange(crossFieldResult.Data.Errors);
            }

            if (!conditionalResult.Data.IsValid)
            {
                combinedResult.IsValid = false;
                combinedResult.Errors.AddRange(conditionalResult.Data.Errors);
            }

            return ServiceResponse<ValidationResult>.Success(combinedResult);
        }

        #endregion
    }

    #region Supporting Classes

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }

    public class ValidationError
    {
        public string Field { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }

    #endregion
}
