/*
================================================================================
MANSERV Loan Account Management System - Local Testing
Customer Service Mock
================================================================================
Purpose: Mock implementation of ICustomerQueryService for local testing
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using ManservLoanSystem.Models.Common;
using ManservLoanSystem.ExternalServices;

namespace ManservLoanSystem.LocalTesting.Mocks
{
    /// <summary>
    /// Mock implementation of customer query service
    /// Provides sample customer data for testing
    /// </summary>
    public class CustomerServiceMock : ICustomerQueryService
    {
        private List<CustomerDetails> _customers;

        public CustomerServiceMock()
        {
            InitializeCustomers();
        }

        #region ICustomerQueryService Implementation

        public ServiceResponse<CustomerDetails> GetCustomerInfo(int customerId)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
            
            if (customer != null)
            {
                return ServiceResponse<CustomerDetails>.Success(customer);
            }

            return ServiceResponse<CustomerDetails>.Failure("CUSTOMER_NOT_FOUND", $"Customer with ID {customerId} not found");
        }

        public ServiceResponse<bool> ValidateCustomerExists(int customerId)
        {
            var exists = _customers.Any(c => c.CustomerId == customerId);
            return ServiceResponse<bool>.Success(exists);
        }

        public ServiceResponse<List<CustomerDetails>> SearchCustomers(string searchTerm)
        {
            var results = _customers
                .Where(c => c.CustomerName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           c.TIN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return ServiceResponse<List<CustomerDetails>>.Success(results);
        }

        #endregion

        #region Data Initialization

        private void InitializeCustomers()
        {
            _customers = new List<CustomerDetails>
            {
                new CustomerDetails
                {
                    CustomerId = 1,
                    CustomerName = "Juan Dela Cruz",
                    TIN = "123-456-789-000",
                    Address = "123 Main Street, Manila",
                    ContactNumber = "+63-2-1234-5678",
                    Email = "juan.delacruz@example.com",
                    CustomerType = "Individual"
                },
                new CustomerDetails
                {
                    CustomerId = 2,
                    CustomerName = "Maria Santos",
                    TIN = "234-567-890-000",
                    Address = "456 Farm Road, Bulacan",
                    ContactNumber = "+63-917-123-4567",
                    Email = "maria.santos@example.com",
                    CustomerType = "Individual"
                },
                new CustomerDetails
                {
                    CustomerId = 3,
                    CustomerName = "Pedro Reyes",
                    TIN = "345-678-901-000",
                    Address = "789 Business Ave, Makati",
                    ContactNumber = "+63-2-8765-4321",
                    Email = "pedro.reyes@example.com",
                    CustomerType = "Individual"
                },
                new CustomerDetails
                {
                    CustomerId = 4,
                    CustomerName = "Ana Garcia",
                    TIN = "456-789-012-000",
                    Address = "321 Trade Street, Quezon City",
                    ContactNumber = "+63-918-234-5678",
                    Email = "ana.garcia@example.com",
                    CustomerType = "Individual"
                },
                new CustomerDetails
                {
                    CustomerId = 5,
                    CustomerName = "Global Exports Inc",
                    TIN = "567-890-123-000",
                    Address = "555 Export Blvd, Pasay City",
                    ContactNumber = "+63-2-5555-1234",
                    Email = "info@globalexports.com",
                    CustomerType = "Corporation"
                },
                new CustomerDetails
                {
                    CustomerId = 6,
                    CustomerName = "Roberto Cruz",
                    TIN = "678-901-234-000",
                    Address = "111 Small Business St, Pasig",
                    ContactNumber = "+63-919-345-6789",
                    Email = "roberto.cruz@example.com",
                    CustomerType = "Individual"
                },
                new CustomerDetails
                {
                    CustomerId = 7,
                    CustomerName = "Draft Customer",
                    TIN = "789-012-345-000",
                    Address = "999 Draft Street, Manila",
                    ContactNumber = "+63-2-9999-9999",
                    Email = "draft@example.com",
                    CustomerType = "Individual"
                },
                new CustomerDetails
                {
                    CustomerId = 8,
                    CustomerName = "Litigation Case Corp",
                    TIN = "890-123-456-000",
                    Address = "777 Legal Ave, BGC",
                    ContactNumber = "+63-2-7777-7777",
                    Email = "legal@litigationcorp.com",
                    CustomerType = "Corporation"
                },
                new CustomerDetails
                {
                    CustomerId = 9,
                    CustomerName = "Development Foundation",
                    TIN = "901-234-567-000",
                    Address = "888 Progress Road, Ortigas",
                    ContactNumber = "+63-2-8888-8888",
                    Email = "info@devfoundation.org",
                    CustomerType = "Foundation"
                },
                new CustomerDetails
                {
                    CustomerId = 10,
                    CustomerName = "Mega Corporation",
                    TIN = "012-345-678-000",
                    Address = "1000 Corporate Plaza, Bonifacio Global City",
                    ContactNumber = "+63-2-1000-1000",
                    Email = "contact@megacorp.com",
                    CustomerType = "Corporation"
                }
            };
        }

        /// <summary>
        /// Add a test customer dynamically
        /// </summary>
        public void AddTestCustomer(CustomerDetails customer)
        {
            _customers.Add(customer);
        }

        /// <summary>
        /// Get all customers (for testing/debugging)
        /// </summary>
        public List<CustomerDetails> GetAllCustomers()
        {
            return _customers;
        }

        #endregion
    }

    #region Supporting Classes

    public class CustomerDetails
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TIN { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string CustomerType { get; set; }
    }

    #endregion
}
