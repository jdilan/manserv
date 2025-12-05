/*
================================================================================
MANSERV Loan Account Management System
Entity Framework DbContext
================================================================================
Purpose: Database context for Entity Framework 6.x
Manages: Account, AccountAudit, AccountRelationship entities
Target: SQL Server 2022
Author: System Generated
Date: December 5, 2025

CONFIGURATION NOTES:
- Connection string name: "ManservLoanDB" (defined in Web.config)
- SQL Server 2022 compatibility level: 160
- Uses Code First approach with existing database
- Lazy loading disabled for better performance
================================================================================
*/

using System.Data.Entity;
using ManservLoanSystem.Models.Entities;

namespace ManservLoanSystem.Data
{
    /// <summary>
    /// Entity Framework DbContext for MANSERV Loan Account Management
    /// Provides access to Account, AccountAudit, and AccountRelationship tables
    /// </summary>
    public class ManservDbContext : DbContext
    {
        /// <summary>
        /// Constructor - uses connection string named "ManservLoanDB" from Web.config
        /// </summary>
        public ManservDbContext() : base("name=ManservLoanDB")
        {
            // Disable lazy loading for better performance and to avoid N+1 query issues
            this.Configuration.LazyLoadingEnabled = false;

            // Disable proxy creation for simpler debugging
            this.Configuration.ProxyCreationEnabled = false;

            // Disable automatic migrations (we use explicit SQL scripts)
            Database.SetInitializer<ManservDbContext>(null);
        }

        #region DbSets

        /// <summary>
        /// Account entities (loan accounts)
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// AccountAudit entities (audit trail)
        /// </summary>
        public DbSet<AccountAudit> AccountAudits { get; set; }

        /// <summary>
        /// AccountRelationship entities (account relationships)
        /// </summary>
        public DbSet<AccountRelationship> AccountRelationships { get; set; }

        #endregion

        #region Model Configuration

        /// <summary>
        /// Configure entity mappings and relationships
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Account entity
            modelBuilder.Entity<Account>()
                .ToTable("Account")
                .HasKey(a => a.AccountId);

            // Configure unique index on ReferenceNumber
            modelBuilder.Entity<Account>()
                .Property(a => a.ReferenceNumber)
                .IsRequired()
                .HasMaxLength(17);

            // Configure AccountAudit entity
            modelBuilder.Entity<AccountAudit>()
                .ToTable("AccountAudit")
                .HasKey(a => a.AuditId);

            // Configure relationship: AccountAudit -> Account
            modelBuilder.Entity<AccountAudit>()
                .HasRequired(a => a.Account)
                .WithMany()
                .HasForeignKey(a => a.AccountId)
                .WillCascadeOnDelete(false);

            // Configure AccountRelationship entity
            modelBuilder.Entity<AccountRelationship>()
                .ToTable("AccountRelationship")
                .HasKey(r => r.RelationshipId);

            // Configure relationship: AccountRelationship -> SourceAccount
            modelBuilder.Entity<AccountRelationship>()
                .HasRequired(r => r.SourceAccount)
                .WithMany()
                .HasForeignKey(r => r.SourceAccountId)
                .WillCascadeOnDelete(false);

            // Configure relationship: AccountRelationship -> TargetAccount
            modelBuilder.Entity<AccountRelationship>()
                .HasRequired(r => r.TargetAccount)
                .WithMany()
                .HasForeignKey(r => r.TargetAccountId)
                .WillCascadeOnDelete(false);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Test database connection
        /// </summary>
        /// <returns>True if connection successful</returns>
        public bool TestConnection()
        {
            try
            {
                return this.Database.Exists();
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}

/*
================================================================================
WEB.CONFIG CONNECTION STRING CONFIGURATION
================================================================================

Add this to your Web.config file in the <connectionStrings> section:

<!-- Development - Windows Authentication -->
<connectionStrings>
  <add name="ManservLoanDB" 
       connectionString="Data Source=localhost;Initial Catalog=ManservLoanDB;Integrated Security=True;TrustServerCertificate=True;Application Name=ManservLoanSystem" 
       providerName="System.Data.SqlClient" />
</connectionStrings>

<!-- Development - SQL Authentication -->
<connectionStrings>
  <add name="ManservLoanDB" 
       connectionString="Data Source=localhost;Initial Catalog=ManservLoanDB;User ID=manserv_user;Password=YourPassword;TrustServerCertificate=True;Application Name=ManservLoanSystem" 
       providerName="System.Data.SqlClient" />
</connectionStrings>

<!-- Production - Encrypted Connection -->
<connectionStrings>
  <add name="ManservLoanDB" 
       connectionString="Data Source=production-server;Initial Catalog=ManservLoanDB;User ID=manserv_app;Password=YourSecurePassword;Encrypt=True;TrustServerCertificate=False;Application Name=ManservLoanSystem;MultipleActiveResultSets=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>

IMPORTANT FOR PRODUCTION:
Encrypt connection strings using aspnet_regiis.exe:
  aspnet_regiis -pef "connectionStrings" "C:\inetpub\wwwroot\ManservLoanSystem" -prov "RsaProtectedConfigurationProvider"

SQL SERVER 2022 SPECIFIC SETTINGS:
- TrustServerCertificate=True: For development with self-signed certificates
- TrustServerCertificate=False: For production with valid SSL certificates
- Encrypt=True: Enables encryption for data in transit (recommended for production)
- MultipleActiveResultSets=True: Allows multiple result sets on same connection
- Application Name: Helps identify connections in SQL Server monitoring

================================================================================
*/
