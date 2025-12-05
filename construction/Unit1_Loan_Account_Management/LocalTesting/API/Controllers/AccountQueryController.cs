/*
================================================================================
MANSERV Loan Account Management System - Local Testing API
Account Query Controller
================================================================================
Purpose: REST API endpoints for account search and query operations
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using Microsoft.AspNetCore.Mvc;
using ManservLoanSystem.LocalTesting.DataAccess;
using ManservLoanSystem.LocalTesting.Mocks;

namespace ManservLoanSystem.LocalTesting.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountQueryController : ControllerBase
    {
        private readonly SqliteAccountRepository _repository;
        private readonly AccessControlServiceMock _accessControlService;

        public AccountQueryController(
            SqliteAccountRepository repository,
            AccessControlServiceMock accessControlService)
        {
            _repository = repository;
            _accessControlService = accessControlService;
        }

        /// <summary>
        /// Search accounts by multiple criteria
        /// </summary>
        [HttpGet("search")]
        public IActionResult Search(
            [FromQuery] string referenceNumber = null,
            [FromQuery] string customerName = null,
            [FromQuery] string centerCode = null,
            [FromQuery] string status = null,
            [FromQuery] string accountType = null,
            [FromQuery] string userId = "SYSTEM")
        {
            try
            {
                // Check permissions
                var permissionCheck = _accessControlService.CheckUserPermission(userId, "VIEW", 0);
                if (!permissionCheck.Data)
                {
                    return Forbid();
                }

                // Apply center restrictions
                var userCenters = _accessControlService.GetUserCenters(userId);
                
                // Perform search
                var results = _repository.Search(
                    referenceNumber,
                    customerName,
                    centerCode,
                    status,
                    accountType
                );

                // Filter by user's centers if applicable
                if (userCenters.Data != null && userCenters.Data.Any())
                {
                    results = results.Where(a => userCenters.Data.Contains(a.CenterCode)).ToList();
                }

                return Ok(new
                {
                    success = true,
                    count = results.Count,
                    searchCriteria = new
                    {
                        referenceNumber,
                        customerName,
                        centerCode,
                        status,
                        accountType
                    },
                    data = results
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
        /// Get accounts by center code
        /// </summary>
        [HttpGet("by-center/{centerCode}")]
        public IActionResult GetByCenter(string centerCode, [FromQuery] string userId = "SYSTEM")
        {
            try
            {
                // Check permissions
                var permissionCheck = _accessControlService.CheckUserPermission(userId, "VIEW", 0);
                if (!permissionCheck.Data)
                {
                    return Forbid();
                }

                // Check center access
                var centerAccess = _accessControlService.CheckCenterAccess(userId, centerCode);
                if (!centerAccess.Data)
                {
                    return Forbid();
                }

                var accounts = _repository.GetByCenterCode(centerCode);

                return Ok(new
                {
                    success = true,
                    centerCode,
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
        /// Get accounts by status
        /// </summary>
        [HttpGet("by-status/{status}")]
        public IActionResult GetByStatus(string status, [FromQuery] string userId = "SYSTEM")
        {
            try
            {
                // Check permissions
                var permissionCheck = _accessControlService.CheckUserPermission(userId, "VIEW", 0);
                if (!permissionCheck.Data)
                {
                    return Forbid();
                }

                var accounts = _repository.GetByStatus(status);

                // Apply center restrictions
                var userCenters = _accessControlService.GetUserCenters(userId);
                if (userCenters.Data != null && userCenters.Data.Any())
                {
                    accounts = accounts.Where(a => userCenters.Data.Contains(a.CenterCode)).ToList();
                }

                return Ok(new
                {
                    success = true,
                    status,
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
        /// Check if reference number exists
        /// </summary>
        [HttpGet("exists/{referenceNumber}")]
        public IActionResult CheckExists(string referenceNumber)
        {
            try
            {
                var exists = _repository.ExistsByReferenceNumber(referenceNumber);

                return Ok(new
                {
                    success = true,
                    referenceNumber,
                    exists
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
        /// Get account statistics
        /// </summary>
        [HttpGet("statistics")]
        public IActionResult GetStatistics([FromQuery] string userId = "SYSTEM")
        {
            try
            {
                var allAccounts = _repository.GetAll(includeDeleted: false);

                // Apply center restrictions
                var userCenters = _accessControlService.GetUserCenters(userId);
                if (userCenters.Data != null && userCenters.Data.Any())
                {
                    allAccounts = allAccounts.Where(a => userCenters.Data.Contains(a.CenterCode)).ToList();
                }

                var statistics = new
                {
                    totalAccounts = allAccounts.Count,
                    byStatus = allAccounts.GroupBy(a => a.Status)
                        .Select(g => new { status = g.Key, count = g.Count() })
                        .ToList(),
                    byCenter = allAccounts.GroupBy(a => a.CenterCode)
                        .Select(g => new { centerCode = g.Key, count = g.Count() })
                        .ToList(),
                    byAccountType = allAccounts.GroupBy(a => a.AccountType)
                        .Select(g => new { accountType = g.Key, count = g.Count() })
                        .ToList(),
                    byCurrency = allAccounts.GroupBy(a => a.Currency)
                        .Select(g => new { currency = g.Key, count = g.Count() })
                        .ToList()
                };

                return Ok(new
                {
                    success = true,
                    data = statistics
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
    }
}
