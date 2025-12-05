/*
================================================================================
MANSERV Loan Account Management System - Local Testing API
Account Controller
================================================================================
Purpose: REST API endpoints for account CRUD operations
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using Microsoft.AspNetCore.Mvc;
using ManservLoanSystem.LocalTesting.DataAccess;
using ManservLoanSystem.LocalTesting.Mocks;
using ManservLoanSystem.Models.Entities;
using ManservLoanSystem.Models.DTOs;

namespace ManservLoanSystem.LocalTesting.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly SqliteAccountRepository _repository;
        private readonly ValidationServiceMock _validationService;
        private readonly AccessControlServiceMock _accessControlService;
        private readonly ReferenceDataServiceMock _referenceDataService;

        public AccountController(
            SqliteAccountRepository repository,
            ValidationServiceMock validationService,
            AccessControlServiceMock accessControlService,
            ReferenceDataServiceMock referenceDataService)
        {
            _repository = repository;
            _validationService = validationService;
            _accessControlService = accessControlService;
            _referenceDataService = referenceDataService;
        }

        /// <summary>
        /// Get all accounts
        /// </summary>
        [HttpGet]
        public IActionResult GetAll([FromQuery] bool includeDeleted = false)
        {
            try
            {
                var accounts = _repository.GetAll(includeDeleted);
                return Ok(new
                {
                    success = true,
                    count = accounts.Count,
                    data = accounts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var account = _repository.GetById(id);
                
                if (account == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = $"Account with ID {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = account
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Get account by reference number
        /// </summary>
        [HttpGet("by-reference/{referenceNumber}")]
        public IActionResult GetByReferenceNumber(string referenceNumber)
        {
            try
            {
                var account = _repository.GetByReferenceNumber(referenceNumber);
                
                if (account == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = $"Account with reference number '{referenceNumber}' not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = account
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Create new account
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] Account account, [FromQuery] string userId = "SYSTEM")
        {
            try
            {
                // Check permissions
                var permissionCheck = _accessControlService.CheckUserPermission(userId, "CREATE", 0);
                if (!permissionCheck.Data)
                {
                    return Forbid();
                }

                // Validate account data
                var validationResult = _validationService.ValidateAll(MapToDTO(account));
                if (!validationResult.Data.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        errors = validationResult.Data.Errors
                    });
                }

                // Check for duplicate reference number
                if (_repository.ExistsByReferenceNumber(account.ReferenceNumber))
                {
                    return Conflict(new
                    {
                        success = false,
                        error = $"Account with reference number '{account.ReferenceNumber}' already exists"
                    });
                }

                // Set audit fields
                account.CreatedBy = userId;
                account.CreatedDate = DateTime.Now;

                // Create account
                var accountId = _repository.Create(account);

                // Create audit record
                _repository.CreateAudit(new AccountAudit
                {
                    AccountId = accountId,
                    ReferenceNumber = account.ReferenceNumber,
                    Action = "Create",
                    ChangedBy = userId,
                    ChangedDate = DateTime.Now,
                    UserRole = _accessControlService.GetUserRole(userId).Data ?? "Unknown",
                    Comments = "Account created via API"
                });

                return CreatedAtAction(nameof(GetById), new { id = accountId }, new
                {
                    success = true,
                    accountId = accountId,
                    message = "Account created successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Account account, [FromQuery] string userId = "SYSTEM")
        {
            try
            {
                // Check permissions
                var permissionCheck = _accessControlService.CheckUserPermission(userId, "UPDATE", id);
                if (!permissionCheck.Data)
                {
                    return Forbid();
                }

                // Check if account exists
                var existingAccount = _repository.GetById(id);
                if (existingAccount == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = $"Account with ID {id} not found"
                    });
                }

                // Validate account data
                var validationResult = _validationService.ValidateAll(MapToDTO(account));
                if (!validationResult.Data.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        errors = validationResult.Data.Errors
                    });
                }

                // Set audit fields
                account.AccountId = id;
                account.ModifiedBy = userId;
                account.ModifiedDate = DateTime.Now;

                // Update account
                var rowsAffected = _repository.Update(account);

                if (rowsAffected == 0)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        error = "Failed to update account"
                    });
                }

                // Create audit record
                _repository.CreateAudit(new AccountAudit
                {
                    AccountId = id,
                    ReferenceNumber = account.ReferenceNumber,
                    Action = "Update",
                    ChangedBy = userId,
                    ChangedDate = DateTime.Now,
                    UserRole = _accessControlService.GetUserRole(userId).Data ?? "Unknown",
                    Comments = "Account updated via API"
                });

                return Ok(new
                {
                    success = true,
                    message = "Account updated successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete account (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] string userId = "SYSTEM")
        {
            try
            {
                // Check permissions
                var permissionCheck = _accessControlService.CheckUserPermission(userId, "DELETE", id);
                if (!permissionCheck.Data)
                {
                    return Forbid();
                }

                // Check if account exists
                var account = _repository.GetById(id);
                if (account == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = $"Account with ID {id} not found"
                    });
                }

                // Soft delete
                var success = _repository.Delete(id, userId);

                if (!success)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        error = "Failed to delete account"
                    });
                }

                // Create audit record
                _repository.CreateAudit(new AccountAudit
                {
                    AccountId = id,
                    ReferenceNumber = account.ReferenceNumber,
                    Action = "Delete",
                    ChangedBy = userId,
                    ChangedDate = DateTime.Now,
                    UserRole = _accessControlService.GetUserRole(userId).Data ?? "Unknown",
                    Comments = "Account deleted via API"
                });

                return Ok(new
                {
                    success = true,
                    message = "Account deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Get audit history for an account
        /// </summary>
        [HttpGet("{id}/audit")]
        public IActionResult GetAuditHistory(int id)
        {
            try
            {
                var auditHistory = _repository.GetAuditHistory(id);
                
                return Ok(new
                {
                    success = true,
                    count = auditHistory.Count,
                    data = auditHistory
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        // Helper method to map Account to AccountDTO
        private AccountDTO MapToDTO(Account account)
        {
            return new AccountDTO
            {
                ReferenceNumber = account.ReferenceNumber,
                PreviousReferenceNumber = account.PreviousReferenceNumber,
                CRIBIDNumber = account.CRIBIDNumber,
                CustomerName = account.CustomerName,
                NIDSSAccountNumber = account.NIDSSAccountNumber,
                LongName = account.LongName,
                CenterCode = account.CenterCode,
                BudgetUnit = account.BudgetUnit,
                Corporation = account.Corporation,
                BookCode = account.BookCode,
                EconomicActivityCode = account.EconomicActivityCode,
                OriginalReleaseDate = account.OriginalReleaseDate,
                StartOfTerm = account.StartOfTerm,
                MaturityDate = account.MaturityDate,
                AccountType = account.AccountType,
                Purpose = account.Purpose,
                FundSource = account.FundSource,
                LendingProgram = account.LendingProgram,
                Area = account.Area,
                IsRestructured = account.IsRestructured,
                TypeOfCredit = account.TypeOfCredit,
                MaturityCode = account.MaturityCode,
                PurposeOfCredit = account.PurposeOfCredit,
                NumberOfRecords = account.NumberOfRecords,
                IsGuaranteed = account.IsGuaranteed,
                GuaranteedBy = account.GuaranteedBy,
                IsUnderLitigation = account.IsUnderLitigation,
                LitigationDate = account.LitigationDate,
                LoanStatus = account.LoanStatus,
                LoanProjectType = account.LoanProjectType,
                Currency = account.Currency
            };
        }
    }
}
