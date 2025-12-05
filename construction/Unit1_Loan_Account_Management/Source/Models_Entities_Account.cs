/*
================================================================================
MANSERV Loan Account Management System
Entity Model: Account
================================================================================
Purpose: Core loan account entity class
Maps to: Account table in SQL Server 2022
Legacy Mapping: MANSERV.DBF
Author: System Generated
Date: December 5, 2025

ENTITY FRAMEWORK 6.X CONFIGURATION:
- This class can be used with Code First or Database First approach
- Data annotations define database mappings
- Navigation properties for related entities
================================================================================
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManservLoanSystem.Models.Entities
{
    /// <summary>
    /// Represents a loan account in the system
    /// Maps to Account table and legacy MANSERV.DBF
    /// </summary>
    [Table("Account")]
    public class Account
    {
        #region Primary Keys

        /// <summary>
        /// Internal surrogate key (auto-generated)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        /// <summary>
        /// Business key - Reference Number (max 17 characters)
        /// Maps to: MANSERV.REFNO
        /// </summary>
        [Required]
        [StringLength(17)]
        [Index("UK_Account_ReferenceNumber", IsUnique = true)]
        public string ReferenceNumber { get; set; }

        #endregion

        #region General Information (GENERAL Section)

        /// <summary>
        /// Previous Reference Number (max 17 characters)
        /// Maps to: MANSERV.PREVREF
        /// </summary>
        [Required]
        [StringLength(17)]
        public string PreviousReferenceNumber { get; set; }

        /// <summary>
        /// CRIB ID Number (max 10 characters, optional)
        /// Maps to: MANSERV.CRIBID
        /// </summary>
        [StringLength(10)]
        public string CRIBIDNumber { get; set; }

        /// <summary>
        /// Customer Name (max 40 characters)
        /// Maps to: MANSERV.CUSNAME
        /// </summary>
        [Required]
        [StringLength(40)]
        [Index("IX_Account_CustomerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// NIDSS Account Number (max 13 characters, optional)
        /// Maps to: MANSERV.NIDSS
        /// </summary>
        [StringLength(13)]
        public string NIDSSAccountNumber { get; set; }

        /// <summary>
        /// Long Name (max 100 characters)
        /// Maps to: MANSERV.LONGNAME
        /// </summary>
        [Required]
        [StringLength(100)]
        public string LongName { get; set; }

        #endregion

        #region Account Identification (LOAN INFO Section)

        /// <summary>
        /// Center Code (2 characters)
        /// Maps to: MANSERV.CNT_CENTER
        /// </summary>
        [Required]
        [StringLength(2)]
        [Index("IX_Account_CenterCode")]
        public string CenterCode { get; set; }

        /// <summary>
        /// Budget Unit (3 characters)
        /// Maps to: MANSERV.BUNIT
        /// </summary>
        [Required]
        [StringLength(3)]
        public string BudgetUnit { get; set; }

        /// <summary>
        /// Corporation (10 characters)
        /// Values: RTL, FCDU, FCDW, WBG
        /// Maps to: MANSERV.CORP
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Corporation { get; set; }

        /// <summary>
        /// Book Code (2 characters)
        /// Values: 11, 12, 20, 30, 41, 42
        /// Maps to: MANSERV.BOOKCDE
        /// </summary>
        [Required]
        [StringLength(2)]
        public string BookCode { get; set; }

        #endregion

        #region Economic Classification

        /// <summary>
        /// Economic Activity Code (6 characters)
        /// Maps to: MANSERV.CNT_ECON
        /// </summary>
        [Required]
        [StringLength(6)]
        public string EconomicActivityCode { get; set; }

        #endregion

        #region Loan Dates

        /// <summary>
        /// Original Release Date
        /// Maps to: MANSERV.ORELDAT
        /// Business Rule: Must equal StartOfTerm
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime OriginalReleaseDate { get; set; }

        /// <summary>
        /// Start of Term (reckoning date for interest computation)
        /// Maps to: MANSERV.CNT_STERM
        /// Business Rule: Must equal OriginalReleaseDate
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime StartOfTerm { get; set; }

        /// <summary>
        /// Maturity Date
        /// Maps to: MANSERV.CNT_MATD
        /// Business Rule: Must be greater than StartOfTerm
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime MaturityDate { get; set; }

        #endregion

        #region Account Type and Purpose

        /// <summary>
        /// Account Type (3 characters)
        /// Examples: Agri, REL, IND, AA, AI, R, RDC, RDE, RDH
        /// Maps to: MANSERV.CNT_ATYPE
        /// </summary>
        [Required]
        [StringLength(3)]
        [Index("IX_Account_AccountType")]
        public string AccountType { get; set; }

        /// <summary>
        /// Purpose (1 character, conditional)
        /// Mandatory when AccountType is AA, AI, R, RDC, RDE, or RDH
        /// Maps to: MANSERV.CNT_PURP
        /// </summary>
        [StringLength(1)]
        public string Purpose { get; set; }

        #endregion

        #region Funding and Program

        /// <summary>
        /// Fund Source (3 characters)
        /// Examples: BSP, DBP, LBP, WB, ACPC
        /// Maps to: MANSERV.CNT_FUND
        /// </summary>
        [Required]
        [StringLength(3)]
        public string FundSource { get; set; }

        /// <summary>
        /// Lending Program (3 characters)
        /// Examples: DBP, ALF, CLF
        /// Maps to: MANSERV.CNT_PROG
        /// </summary>
        [Required]
        [StringLength(3)]
        public string LendingProgram { get; set; }

        /// <summary>
        /// Area (3 characters)
        /// Values: PA (Performing), NPA (Non-Performing)
        /// Maps to: MANSERV.AREA
        /// </summary>
        [Required]
        [StringLength(3)]
        public string Area { get; set; }

        #endregion

        #region Status and Classification

        /// <summary>
        /// Is Restructured flag
        /// Maps to: MANSERV.REST (Y/N → bit)
        /// </summary>
        [Required]
        public bool IsRestructured { get; set; }

        /// <summary>
        /// Type of Credit (6 characters, auto-populated)
        /// Examples: CUR, PDO, LITIG
        /// Maps to: MANSERV.CNT_CRTYPE
        /// Business Rule: Auto-populated based on AccountType and Status, cannot override
        /// </summary>
        [Required]
        [StringLength(6)]
        public string TypeOfCredit { get; set; }

        /// <summary>
        /// Maturity Code (1 character)
        /// Values: A (Demand), B (Short-term), C-E (Intermediate to Long-term)
        /// Maps to: MANSERV.CNT_MAT
        /// </summary>
        [Required]
        [StringLength(1)]
        public string MaturityCode { get; set; }

        /// <summary>
        /// Purpose of Credit (1 character, auto-populated)
        /// Maps to: MANSERV.CNT_CRPURP
        /// Business Rule: Auto-populated based on AccountType, cannot override
        /// </summary>
        [Required]
        [StringLength(1)]
        public string PurposeOfCredit { get; set; }

        /// <summary>
        /// Number of Records (max 5 digits)
        /// Maps to: MANSERV.CNT_REC
        /// </summary>
        public int? NumberOfRecords { get; set; }

        #endregion

        #region Guarantee Information

        /// <summary>
        /// Is Guaranteed flag
        /// Maps to: MANSERV.GUAR (Y/N → bit)
        /// </summary>
        [Required]
        public bool IsGuaranteed { get; set; }

        /// <summary>
        /// Guaranteed By (10 characters, conditional)
        /// Examples: SBGFC, GFSME, PHILGUARANTEE
        /// Maps to: MANSERV.CNT_GBY
        /// Business Rule: Enabled only when IsGuaranteed = true, but not mandatory
        /// </summary>
        [StringLength(10)]
        public string GuaranteedBy { get; set; }

        #endregion

        #region Litigation

        /// <summary>
        /// Is Under Litigation flag
        /// Maps to: MANSERV.LITIG (Y/N → bit)
        /// Business Rule: Manual only, separate Reclass Module for Litigation
        /// </summary>
        [Required]
        public bool IsUnderLitigation { get; set; }

        /// <summary>
        /// Litigation Date
        /// Maps to: MANSERV.ITL
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? LitigationDate { get; set; }

        #endregion

        #region Loan Project and Currency

        /// <summary>
        /// Loan Status (3 characters)
        /// Values: CUR (Current), PDO (Past Due)
        /// Maps to: MANSERV.CNT_LSTAT
        /// Business Rule: Changes from CUR to PDO after 90 days
        /// </summary>
        [Required]
        [StringLength(3)]
        public string LoanStatus { get; set; }

        /// <summary>
        /// Loan Project Type (1 character)
        /// Values: C (Commercial), D (Developmental)
        /// Maps to: MANSERV.CNT_LPTYPE
        /// </summary>
        [Required]
        [StringLength(1)]
        public string LoanProjectType { get; set; }

        /// <summary>
        /// Currency (3 characters)
        /// Examples: PHP, USD, JPY, EUR, GBP
        /// Maps to: MANSERV.RELCURR
        /// Business Rule: Determines which balance column to use
        /// </summary>
        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        #endregion

        #region Account Status

        /// <summary>
        /// Account Status (20 characters)
        /// Values: Active, Closed, Archived, Deleted
        /// </summary>
        [Required]
        [StringLength(20)]
        [Index("IX_Account_Status")]
        public string Status { get; set; }

        /// <summary>
        /// Is Draft flag (allows incomplete data)
        /// </summary>
        [Required]
        public bool IsDraft { get; set; }

        /// <summary>
        /// Closure Date (for closed accounts)
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? ClosureDate { get; set; }

        #endregion

        #region Audit Fields

        /// <summary>
        /// User who created the account
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Date and time when account was created
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// User who last modified the account
        /// </summary>
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Date and time when account was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// User who deleted the account (soft delete)
        /// </summary>
        [StringLength(50)]
        public string DeletedBy { get; set; }

        /// <summary>
        /// Date and time when account was deleted (soft delete)
        /// </summary>
        public DateTime? DeletedDate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor - initializes default values
        /// </summary>
        public Account()
        {
            // Set default values
            Status = "Active";
            IsDraft = false;
            IsRestructured = false;
            IsGuaranteed = false;
            IsUnderLitigation = false;
            CreatedDate = DateTime.Now;
        }

        #endregion
    }
}
