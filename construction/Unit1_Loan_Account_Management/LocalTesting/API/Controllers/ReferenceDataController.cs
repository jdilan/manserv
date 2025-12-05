/*
================================================================================
MANSERV Loan Account Management System - Local Testing API
Reference Data Controller
================================================================================
Purpose: REST API endpoints for reference data (dropdowns, lookups)
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using Microsoft.AspNetCore.Mvc;
using ManservLoanSystem.LocalTesting.Mocks;

namespace ManservLoanSystem.LocalTesting.API.Controllers
{
    [ApiController]
    [Route("api/reference")]
    public class ReferenceDataController : ControllerBase
    {
        private readonly ReferenceDataServiceMock _referenceDataService;
        private readonly CustomerServiceMock _customerService;

        public ReferenceDataController(
            ReferenceDataServiceMock referenceDataService,
            CustomerServiceMock customerService)
        {
            _referenceDataService = referenceDataService;
            _customerService = customerService;
        }

        /// <summary>
        /// Get all reference data groups
        /// </summary>
        [HttpGet]
        public IActionResult GetAllGroups()
        {
            try
            {
                var groups = new[]
                {
                    "CORPORATION",
                    "BOOKCODE",
                    "FUNDSOURCE",
                    "LENDINGPROGRAM",
                    "AREA",
                    "MATURITYCODE",
                    "GUARANTEEDBY",
                    "CURRENCY",
                    "BUDGETUNIT"
                };

                return Ok(new
                {
                    success = true,
                    groups
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
        /// Get reference data by group
        /// </summary>
        [HttpGet("{groupNumber}")]
        public IActionResult GetByGroup(string groupNumber)
        {
            try
            {
                var result = _referenceDataService.GetReferenceData(groupNumber);

                if (!result.Status.Equals("Success", StringComparison.OrdinalIgnoreCase))
                {
                    return NotFound(new
                    {
                        success = false,
                        error = result.Errors?.FirstOrDefault()?.Message ?? "Group not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    group = groupNumber,
                    count = result.Data.Count,
                    data = result.Data
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
        /// Get account types
        /// </summary>
        [HttpGet("account-types")]
        public IActionResult GetAccountTypes()
        {
            try
            {
                var result = _referenceDataService.GetAccountTypes();

                return Ok(new
                {
                    success = true,
                    count = result.Data.Count,
                    data = result.Data
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
        /// Get economic activities
        /// </summary>
        [HttpGet("economic-activities")]
        public IActionResult GetEconomicActivities()
        {
            try
            {
                var result = _referenceDataService.GetEconomicActivities();

                return Ok(new
                {
                    success = true,
                    count = result.Data.Count,
                    data = result.Data
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
        /// Get centers
        /// </summary>
        [HttpGet("centers")]
        public IActionResult GetCenters()
        {
            try
            {
                var result = _referenceDataService.GetCenters();

                return Ok(new
                {
                    success = true,
                    count = result.Data.Count,
                    data = result.Data
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
        /// Get GL mappings for account type and economic activity
        /// </summary>
        [HttpGet("gl-mappings")]
        public IActionResult GetGLMappings(
            [FromQuery] string accountTypeCode,
            [FromQuery] string economicActivityCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accountTypeCode) || string.IsNullOrWhiteSpace(economicActivityCode))
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Both accountTypeCode and economicActivityCode are required"
                    });
                }

                var result = _referenceDataService.GetGLMappings(accountTypeCode, economicActivityCode);

                return Ok(new
                {
                    success = true,
                    accountTypeCode,
                    economicActivityCode,
                    data = result.Data
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
        /// Get all customers
        /// </summary>
        [HttpGet("customers")]
        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();

                return Ok(new
                {
                    success = true,
                    count = customers.Count,
                    data = customers
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
        /// Get customer by ID
        /// </summary>
        [HttpGet("customers/{customerId}")]
        public IActionResult GetCustomer(int customerId)
        {
            try
            {
                var result = _customerService.GetCustomerInfo(customerId);

                if (!result.Status.Equals("Success", StringComparison.OrdinalIgnoreCase))
                {
                    return NotFound(new
                    {
                        success = false,
                        error = result.Errors?.FirstOrDefault()?.Message ?? "Customer not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = result.Data
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
