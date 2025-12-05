/*
================================================================================
MANSERV Loan Account Management System
Entity Model: AccountAudit
================================================================================
Purpose: Audit trail entity for tracking all account changes
Maps to: AccountAudit table in SQL Server 2022
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManservLoanSystem.Models.Entities
{
    /// <summary>
    /// Represents an audit log entry for account changes
    /// Tracks all Create, Update, Delete, Close, Archive, and Reopen operations
    /// </summary>
    [Table("AccountAudit")]
    public class AccountAudit
    {
        /// <summary>
        /// Audit entry ID (auto-generated)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditId { get; set; }

        /// <summary>
        /// Foreign key to Account table
        /// </summary>
        [Required]
        public int AccountId { get; set; }

        /// <summary>
        /// Reference number of the account (for quick lookup)
        /// </summary>
        [Required]
        [StringLength(17)]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Action performed: Create, Update, Delete, Close, Archive, Reopen
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Action { get; set; }

        /// <summary>
        /// Field name that was changed (for Update actions)
        /// </summary>
        [StringLength(100)]
        public string FieldName { get; set; }

        /// <summary>
        /// Old value before change (can store JSON for complex objects)
        /// SQL Server 2022 supports JSON functions for querying
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string OldValue { get; set; }

        /// <summary>
        /// New value after change (can store JSON for complex objects)
        /// SQL Server 2022 supports JSON functions for querying
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string NewValue { get; set; }

        /// <summary>
        /// User who made the change
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ChangedBy { get; set; }

        /// <summary>
        /// Date and time of the change
        /// </summary>
        [Required]
        public DateTime ChangedDate { get; set; }

        /// <summary>
        /// Role of the user who made the change (User, Authorizer, Administrator)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserRole { get; set; }

        /// <summary>
        /// IP address of the user (for security tracking)
        /// </summary>
        [StringLength(50)]
        public string IPAddress { get; set; }

        /// <summary>
        /// Optional comments about the change
        /// </summary>
        [StringLength(500)]
        public string Comments { get; set; }

        /// <summary>
        /// Navigation property to Account
        /// </summary>
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountAudit()
        {
            ChangedDate = DateTime.Now;
        }
    }
}
