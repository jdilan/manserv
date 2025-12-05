/*
================================================================================
MANSERV Loan Account Management System
Data Transfer Object: AccountDTO
================================================================================
Purpose: DTO for transferring account data between layers
Used for: Create and Update operations
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;

namespace ManservLoanSystem.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object for Account entity
    /// Used to transfer data between presentation and service layers
    /// Decouples UI from entity model
    /// </summary>
    public class AccountDTO
    {
        // Primary Keys
        public int AccountId { get; set; }
        public string ReferenceNumber { get; set; }

        // General Information
        public string PreviousReferenceNumber { get; set; }
        public string CRIBIDNumber { get; set; }
        public string CustomerName { get; set; }
        public string NIDSSAccountNumber { get; set; }
        public string LongName { get; set; }

        // Account Identification
        public string CenterCode { get; set; }
        public string BudgetUnit { get; set; }
        public string Corporation { get; set; }
        public string BookCode { get; set; }

        // Economic Classification
        public string EconomicActivityCode { get; set; }

        // Loan Dates
        public DateTime OriginalReleaseDate { get; set; }
        public DateTime StartOfTerm { get; set; }
        public DateTime MaturityDate { get; set; }

        // Account Type and Purpose
        public string AccountType { get; set; }
        public string Purpose { get; set; }

        // Funding and Program
        public string FundSource { get; set; }
        public string LendingProgram { get; set; }
        public string Area { get; set; }

        // Status and Classification
        public bool IsRestructured { get; set; }
        public string TypeOfCredit { get; set; }
        public string MaturityCode { get; set; }
        public string PurposeOfCredit { get; set; }
        public int? NumberOfRecords { get; set; }

        // Guarantee Information
        public bool IsGuaranteed { get; set; }
        public string GuaranteedBy { get; set; }

        // Litigation
        public bool IsUnderLitigation { get; set; }
        public DateTime? LitigationDate { get; set; }

        // Loan Project and Currency
        public string LoanStatus { get; set; }
        public string LoanProjectType { get; set; }
        public string Currency { get; set; }

        // Account Status
        public string Status { get; set; }
        public bool IsDraft { get; set; }
        public DateTime? ClosureDate { get; set; }
    }
}
