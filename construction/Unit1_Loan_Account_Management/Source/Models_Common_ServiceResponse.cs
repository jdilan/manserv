/*
================================================================================
MANSERV Loan Account Management System
Common Classes: ServiceResponse and ServiceError
================================================================================
Purpose: Standardized response format for all service operations
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System.Collections.Generic;
using System.Linq;

namespace ManservLoanSystem.Models.Common
{
    /// <summary>
    /// Standardized response wrapper for all service operations
    /// Provides consistent error handling and status reporting
    /// </summary>
    /// <typeparam name="T">Type of data being returned</typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Status of the operation
        /// </summary>
        public ServiceStatus Status { get; set; }

        /// <summary>
        /// Data returned by the operation (if successful)
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// List of errors (if operation failed)
        /// </summary>
        public List<ServiceError> Errors { get; set; }

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess => Status == ServiceStatus.Success;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceResponse()
        {
            Errors = new List<ServiceError>();
        }

        /// <summary>
        /// Creates a successful response with data
        /// </summary>
        public static ServiceResponse<T> Success(T data)
        {
            return new ServiceResponse<T>
            {
                Status = ServiceStatus.Success,
                Data = data
            };
        }

        /// <summary>
        /// Creates a failure response with a single error
        /// </summary>
        public static ServiceResponse<T> Failure(string errorCode, string message, string field = null)
        {
            return new ServiceResponse<T>
            {
                Status = ServiceStatus.Failure,
                Errors = new List<ServiceError>
                {
                    new ServiceError
                    {
                        ErrorCode = errorCode,
                        Message = message,
                        Field = field
                    }
                }
            };
        }

        /// <summary>
        /// Creates a failure response with multiple errors
        /// </summary>
        public static ServiceResponse<T> Failure(List<ServiceError> errors)
        {
            return new ServiceResponse<T>
            {
                Status = ServiceStatus.Failure,
                Errors = errors
            };
        }

        /// <summary>
        /// Gets a formatted error message string
        /// </summary>
        public string GetErrorMessage()
        {
            if (Errors == null || !Errors.Any())
                return string.Empty;

            return string.Join("; ", Errors.Select(e => 
                string.IsNullOrEmpty(e.Field) ? e.Message : $"{e.Field}: {e.Message}"));
        }
    }

    /// <summary>
    /// Represents a service error
    /// </summary>
    public class ServiceError
    {
        /// <summary>
        /// Error code for programmatic handling
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Human-readable error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Field name associated with the error (optional)
        /// </summary>
        public string Field { get; set; }
    }

    /// <summary>
    /// Service operation status
    /// </summary>
    public enum ServiceStatus
    {
        Success,
        Failure
    }

    /// <summary>
    /// Standard error codes used throughout the application
    /// </summary>
    public static class ErrorCodes
    {
        public const string MANDATORY_FIELD = "MANDATORY_FIELD";
        public const string INVALID_LENGTH = "INVALID_LENGTH";
        public const string INVALID_FORMAT = "INVALID_FORMAT";
        public const string DUPLICATE_REFNO = "DUPLICATE_REFNO";
        public const string INVALID_DATE_RANGE = "INVALID_DATE_RANGE";
        public const string CONDITIONAL_REQUIRED = "CONDITIONAL_REQUIRED";
        public const string INVALID_STATUS_TRANSITION = "INVALID_STATUS_TRANSITION";
        public const string HAS_DEPENDENCIES = "HAS_DEPENDENCIES";
        public const string UNAUTHORIZED = "UNAUTHORIZED";
        public const string INVALID_CENTER = "INVALID_CENTER";
        public const string NOT_FOUND = "NOT_FOUND";
        public const string SYSTEM_ERROR = "SYSTEM_ERROR";
    }
}
