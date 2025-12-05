/*
================================================================================
MANSERV Loan Account Management System
Entity Model: AccountRelationship
================================================================================
Purpose: Track relationships between accounts (copy, restructure, renewal)
Maps to: AccountRelationship table in SQL Server 2022
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
    /// Represents a relationship between two accounts
    /// Used for tracking copied accounts, restructured loans, and renewals
    /// </summary>
    [Table("AccountRelationship")]
    public class AccountRelationship
    {
        /// <summary>
        /// Relationship ID (auto-generated)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelationshipId { get; set; }

        /// <summary>
        /// Source account ID (the original account)
        /// </summary>
        [Required]
        public int SourceAccountId { get; set; }

        /// <summary>
        /// Target account ID (the new/related account)
        /// </summary>
        [Required]
        public int TargetAccountId { get; set; }

        /// <summary>
        /// Type of relationship: Copy, Restructure, Renewal
        /// </summary>
        [Required]
        [StringLength(20)]
        public string RelationshipType { get; set; }

        /// <summary>
        /// User who created the relationship
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Date and time when relationship was created
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Navigation property to source account
        /// </summary>
        [ForeignKey("SourceAccountId")]
        public virtual Account SourceAccount { get; set; }

        /// <summary>
        /// Navigation property to target account
        /// </summary>
        [ForeignKey("TargetAccountId")]
        public virtual Account TargetAccount { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountRelationship()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
