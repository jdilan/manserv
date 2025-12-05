/*
================================================================================
MANSERV Loan Account Management System - Local Testing
SQLite Repository Implementation
================================================================================
Purpose: SQLite-based repository for local API testing
Technology: System.Data.SQLite with ADO.NET
Author: System Generated
Date: December 6, 2025

USAGE:
- Install NuGet package: System.Data.SQLite
- Connection string: "Data Source=manserv_test.db;Version=3;"
- Enable foreign keys: PRAGMA foreign_keys = ON
================================================================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using ManservLoanSystem.Models.Entities;
using ManservLoanSystem.Repositories;

namespace ManservLoanSystem.LocalTesting.DataAccess
{
    /// <summary>
    /// SQLite implementation of IAccountRepository for local testing
    /// </summary>
    public class SqliteAccountRepository : IAccountRepository
    {
        private readonly string _connectionString;
        private SQLiteConnection _connection;

        /// <summary>
        /// Constructor with connection string
        /// </summary>
        public SqliteAccountRepository(string connectionString)
        {
            _connectionString = connectionString;
            InitializeConnection();
        }

        /// <summary>
        /// Initialize database connection and enable foreign keys
        /// </summary>
        private void InitializeConnection()
        {
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
            
            // Enable foreign key constraints (required for SQLite)
            using (var cmd = new SQLiteCommand("PRAGMA foreign_keys = ON", _connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        #region CRUD Operations

        /// <summary>
        /// Create a new account
        /// </summary>
        public int Create(Account account)
        {
            const string sql = @"
                INSERT INTO Account (
                    ReferenceNumber, PreviousReferenceNumber, CRIBIDNumber, CustomerName,
                    NIDSSAccountNumber, LongName, CenterCode, BudgetUnit, Corporation,
                    BookCode, EconomicActivityCode, OriginalReleaseDate, StartOfTerm,
                    MaturityDate, AccountType, Purpose, FundSource, LendingProgram,
                    Area, IsRestructured, TypeOfCredit, MaturityCode, PurposeOfCredit,
                    NumberOfRecords, IsGuaranteed, GuaranteedBy, IsUnderLitigation,
                    LitigationDate, LoanStatus, LoanProjectType, Currency, Status,
                    IsDraft, ClosureDate, CreatedBy, CreatedDate
                )
                VALUES (
                    @ReferenceNumber, @PreviousReferenceNumber, @CRIBIDNumber, @CustomerName,
                    @NIDSSAccountNumber, @LongName, @CenterCode, @BudgetUnit, @Corporation,
                    @BookCode, @EconomicActivityCode, @OriginalReleaseDate, @StartOfTerm,
                    @MaturityDate, @AccountType, @Purpose, @FundSource, @LendingProgram,
                    @Area, @IsRestructured, @TypeOfCredit, @MaturityCode, @PurposeOfCredit,
                    @NumberOfRecords, @IsGuaranteed, @GuaranteedBy, @IsUnderLitigation,
                    @LitigationDate, @LoanStatus, @LoanProjectType, @Currency, @Status,
                    @IsDraft, @ClosureDate, @CreatedBy, @CreatedDate
                );
                SELECT last_insert_rowid();";

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                AddAccountParameters(cmd, account);
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        /// <summary>
        /// Get account by internal ID
        /// </summary>
        public Account GetById(int accountId)
        {
            const string sql = "SELECT * FROM Account WHERE AccountId = @AccountId";

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapReaderToAccount(reader);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get account by reference number
        /// </summary>
        public Account GetByReferenceNumber(string referenceNumber)
        {
            const string sql = "SELECT * FROM Account WHERE ReferenceNumber = @ReferenceNumber";

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapReaderToAccount(reader);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        public int Update(Account account)
        {
            const string sql = @"
                UPDATE Account SET
                    ReferenceNumber = @ReferenceNumber,
                    PreviousReferenceNumber = @PreviousReferenceNumber,
                    CRIBIDNumber = @CRIBIDNumber,
                    CustomerName = @CustomerName,
                    NIDSSAccountNumber = @NIDSSAccountNumber,
                    LongName = @LongName,
                    CenterCode = @CenterCode,
                    BudgetUnit = @BudgetUnit,
                    Corporation = @Corporation,
                    BookCode = @BookCode,
                    EconomicActivityCode = @EconomicActivityCode,
                    OriginalReleaseDate = @OriginalReleaseDate,
                    StartOfTerm = @StartOfTerm,
                    MaturityDate = @MaturityDate,
                    AccountType = @AccountType,
                    Purpose = @Purpose,
                    FundSource = @FundSource,
                    LendingProgram = @LendingProgram,
                    Area = @Area,
                    IsRestructured = @IsRestructured,
                    TypeOfCredit = @TypeOfCredit,
                    MaturityCode = @MaturityCode,
                    PurposeOfCredit = @PurposeOfCredit,
                    NumberOfRecords = @NumberOfRecords,
                    IsGuaranteed = @IsGuaranteed,
                    GuaranteedBy = @GuaranteedBy,
                    IsUnderLitigation = @IsUnderLitigation,
                    LitigationDate = @LitigationDate,
                    LoanStatus = @LoanStatus,
                    LoanProjectType = @LoanProjectType,
                    Currency = @Currency,
                    Status = @Status,
                    IsDraft = @IsDraft,
                    ClosureDate = @ClosureDate,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE AccountId = @AccountId";

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                AddAccountParameters(cmd, account);
                cmd.Parameters.AddWithValue("@AccountId", account.AccountId);
                cmd.Parameters.AddWithValue("@ModifiedBy", account.ModifiedBy);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Soft delete account
        /// </summary>
        public bool Delete(int accountId, string userId)
        {
            const string sql = @"
                UPDATE Account SET
                    Status = 'Deleted',
                    DeletedBy = @DeletedBy,
                    DeletedDate = @DeletedDate
                WHERE AccountId = @AccountId";

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                cmd.Parameters.AddWithValue("@DeletedBy", userId);
                cmd.Parameters.AddWithValue("@DeletedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        #endregion

        #region Query Operations

        /// <summary>
        /// Get all accounts
        /// </summary>
        public List<Account> GetAll(bool includeDeleted = false)
        {
            var sql = "SELECT * FROM Account";
            if (!includeDeleted)
            {
                sql += " WHERE Status != 'Deleted'";
            }
            sql += " ORDER BY CreatedDate DESC";

            var accounts = new List<Account>();

            using (var cmd = new SQLiteCommand(sql, _connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    accounts.Add(MapReaderToAccount(reader));
                }
            }

            return accounts;
        }

        /// <summary>
        /// Search accounts by criteria
        /// </summary>
        public List<Account> Search(
            string referenceNumber = null,
            string customerName = null,
            string centerCode = null,
            string status = null,
            string accountType = null)
        {
            var sql = "SELECT * FROM Account WHERE 1=1";
            var parameters = new List<SQLiteParameter>();

            if (!string.IsNullOrEmpty(referenceNumber))
            {
                sql += " AND ReferenceNumber LIKE @ReferenceNumber";
                parameters.Add(new SQLiteParameter("@ReferenceNumber", $"%{referenceNumber}%"));
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                sql += " AND CustomerName LIKE @CustomerName";
                parameters.Add(new SQLiteParameter("@CustomerName", $"%{customerName}%"));
            }

            if (!string.IsNullOrEmpty(centerCode))
            {
                sql += " AND CenterCode = @CenterCode";
                parameters.Add(new SQLiteParameter("@CenterCode", centerCode));
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += " AND Status = @Status";
                parameters.Add(new SQLiteParameter("@Status", status));
            }

            if (!string.IsNullOrEmpty(accountType))
            {
                sql += " AND AccountType = @AccountType";
                parameters.Add(new SQLiteParameter("@AccountType", accountType));
            }

            sql += " ORDER BY CreatedDate DESC";

            var accounts = new List<Account>();

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                
                using (var reader = cmd.ExecuteReader())      
          {
                    while (reader.Read())
                    {
                        accounts.Add(MapReaderToAccount(reader));
                    }
                }
            }

            return accounts;
        }

        /// <summary>
        /// Check if reference number exists
        /// </summary>
        public bool ExistsByReferenceNumber(string referenceNumber, int? excludeAccountId = null)
        {
            var sql = "SELECT COUNT(*) FROM Account WHERE ReferenceNumber = @ReferenceNumber";
            
            if (excludeAccountId.HasValue)
            {
                sql += " AND AccountId != @ExcludeAccountId";
            }

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                
                if (excludeAccountId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ExcludeAccountId", excludeAccountId.Value);
                }
                
                var count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        /// <summary>
        /// Get accounts by center code
        /// </summary>
        public List<Account> GetByCenterCode(string centerCode)
        {
            const string sql = "SELECT * FROM Account WHERE CenterCode = @CenterCode AND Status != 'Deleted' ORDER BY CreatedDate DESC";

            var accounts = new List<Account>();

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@CenterCode", centerCode);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(MapReaderToAccount(reader));
                    }
                }
            }

            return accounts;
        }

        /// <summary>
        /// Get accounts by status
        /// </summary>
        public List<Account> GetByStatus(string status)
        {
            const string sql = "SELECT * FROM Account WHERE Status = @Status ORDER BY CreatedDate DESC";

            var accounts = new List<Account>();

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(MapReaderToAccount(reader));
                    }
                }
            }

            return accounts;
        }

        #endregion

        #region Audit Operations

        /// <summary>
        /// Create audit record
        /// </summary>
        public void CreateAudit(AccountAudit audit)
        {
            const string sql = @"
                INSERT INTO AccountAudit (
                    AccountId, ReferenceNumber, Action, FieldName, OldValue, NewValue,
                    ChangedBy, ChangedDate, UserRole, IPAddress, Comments
                )
                VALUES (
                    @AccountId, @ReferenceNumber, @Action, @FieldName, @OldValue, @NewValue,
                    @ChangedBy, @ChangedDate, @UserRole, @IPAddress, @Comments
                )";

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@AccountId", audit.AccountId);
                cmd.Parameters.AddWithValue("@ReferenceNumber", audit.ReferenceNumber);
                cmd.Parameters.AddWithValue("@Action", audit.Action);
                cmd.Parameters.AddWithValue("@FieldName", audit.FieldName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@OldValue", audit.OldValue ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NewValue", audit.NewValue ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ChangedBy", audit.ChangedBy);
                cmd.Parameters.AddWithValue("@ChangedDate", audit.ChangedDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@UserRole", audit.UserRole);
                cmd.Parameters.AddWithValue("@IPAddress", audit.IPAddress ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Comments", audit.Comments ?? (object)DBNull.Value);
                
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get audit history for account
        /// </summary>
        public List<AccountAudit> GetAuditHistory(int accountId)
        {
            const string sql = "SELECT * FROM AccountAudit WHERE AccountId = @AccountId ORDER BY ChangedDate DESC";

            var audits = new List<AccountAudit>();

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        audits.Add(new AccountAudit
                        {
                            AuditId = reader.GetInt32(reader.GetOrdinal("AuditId")),
                            AccountId = reader.GetInt32(reader.GetOrdinal("AccountId")),
                            ReferenceNumber = reader.GetString(reader.GetOrdinal("ReferenceNumber")),
                            Action = reader.GetString(reader.GetOrdinal("Action")),
                            FieldName = reader.IsDBNull(reader.GetOrdinal("FieldName")) ? null : reader.GetString(reader.GetOrdinal("FieldName")),
                            OldValue = reader.IsDBNull(reader.GetOrdinal("OldValue")) ? null : reader.GetString(reader.GetOrdinal("OldValue")),
                            NewValue = reader.IsDBNull(reader.GetOrdinal("NewValue")) ? null : reader.GetString(reader.GetOrdinal("NewValue")),
                            ChangedBy = reader.GetString(reader.GetOrdinal("ChangedBy")),
                            ChangedDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("ChangedDate"))),
                            UserRole = reader.GetString(reader.GetOrdinal("UserRole")),
                            IPAddress = reader.IsDBNull(reader.GetOrdinal("IPAddress")) ? null : reader.GetString(reader.GetOrdinal("IPAddress")),
                            Comments = reader.IsDBNull(reader.GetOrdinal("Comments")) ? null : reader.GetString(reader.GetOrdinal("Comments"))
                        });
                    }
                }
            }

            return audits;
        }

        #endregion

        #region Relationship Operations

        /// <summary>
        /// Create account relationship
        /// </summary>
        public void CreateRelationship(AccountRelationship relationship)
        {
            const string sql = @"
                INSERT INTO AccountRelationship (
                    SourceAccountId, TargetAccountId, RelationshipType, CreatedBy, CreatedDate
                )
                VALUES (
                    @SourceAccountId, @TargetAccountId, @RelationshipType, @CreatedBy, @CreatedDate
                )";

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@SourceAccountId", relationship.SourceAccountId);
                cmd.Parameters.AddWithValue("@TargetAccountId", relationship.TargetAccountId);
                cmd.Parameters.AddWithValue("@RelationshipType", relationship.RelationshipType);
                cmd.Parameters.AddWithValue("@CreatedBy", relationship.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", relationship.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"));
                
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get relationships for account
        /// </summary>
        public List<AccountRelationship> GetRelationships(int accountId)
        {
            const string sql = @"
                SELECT * FROM AccountRelationship 
                WHERE SourceAccountId = @AccountId OR TargetAccountId = @AccountId
                ORDER BY CreatedDate DESC";

            var relationships = new List<AccountRelationship>();

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        relationships.Add(new AccountRelationship
                        {
                            RelationshipId = reader.GetInt32(reader.GetOrdinal("RelationshipId")),
                            SourceAccountId = reader.GetInt32(reader.GetOrdinal("SourceAccountId")),
                            TargetAccountId = reader.GetInt32(reader.GetOrdinal("TargetAccountId")),
                            RelationshipType = reader.GetString(reader.GetOrdinal("RelationshipType")),
                            CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                            CreatedDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("CreatedDate")))
                        });
                    }
                }
            }

            return relationships;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Add account parameters to command
        /// </summary>
        private void AddAccountParameters(SQLiteCommand cmd, Account account)
        {
            cmd.Parameters.AddWithValue("@ReferenceNumber", account.ReferenceNumber);
            cmd.Parameters.AddWithValue("@PreviousReferenceNumber", account.PreviousReferenceNumber);
            cmd.Parameters.AddWithValue("@CRIBIDNumber", account.CRIBIDNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CustomerName", account.CustomerName);
            cmd.Parameters.AddWithValue("@NIDSSAccountNumber", account.NIDSSAccountNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LongName", account.LongName);
            cmd.Parameters.AddWithValue("@CenterCode", account.CenterCode);
            cmd.Parameters.AddWithValue("@BudgetUnit", account.BudgetUnit);
            cmd.Parameters.AddWithValue("@Corporation", account.Corporation);
            cmd.Parameters.AddWithValue("@BookCode", account.BookCode);
            cmd.Parameters.AddWithValue("@EconomicActivityCode", account.EconomicActivityCode);
            cmd.Parameters.AddWithValue("@OriginalReleaseDate", account.OriginalReleaseDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@StartOfTerm", account.StartOfTerm.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@MaturityDate", account.MaturityDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@AccountType", account.AccountType);
            cmd.Parameters.AddWithValue("@Purpose", account.Purpose ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FundSource", account.FundSource);
            cmd.Parameters.AddWithValue("@LendingProgram", account.LendingProgram);
            cmd.Parameters.AddWithValue("@Area", account.Area);
            cmd.Parameters.AddWithValue("@IsRestructured", account.IsRestructured ? 1 : 0);
            cmd.Parameters.AddWithValue("@TypeOfCredit", account.TypeOfCredit);
            cmd.Parameters.AddWithValue("@MaturityCode", account.MaturityCode);
            cmd.Parameters.AddWithValue("@PurposeOfCredit", account.PurposeOfCredit);
            cmd.Parameters.AddWithValue("@NumberOfRecords", account.NumberOfRecords ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IsGuaranteed", account.IsGuaranteed ? 1 : 0);
            cmd.Parameters.AddWithValue("@GuaranteedBy", account.GuaranteedBy ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IsUnderLitigation", account.IsUnderLitigation ? 1 : 0);
            cmd.Parameters.AddWithValue("@LitigationDate", account.LitigationDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LoanStatus", account.LoanStatus);
            cmd.Parameters.AddWithValue("@LoanProjectType", account.LoanProjectType);
            cmd.Parameters.AddWithValue("@Currency", account.Currency);
            cmd.Parameters.AddWithValue("@Status", account.Status);
            cmd.Parameters.AddWithValue("@IsDraft", account.IsDraft ? 1 : 0);
            cmd.Parameters.AddWithValue("@ClosureDate", account.ClosureDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedBy", account.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", account.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// Map data reader to Account entity
        /// </summary>
        private Account MapReaderToAccount(SQLiteDataReader reader)
        {
            return new Account
            {
                AccountId = reader.GetInt32(reader.GetOrdinal("AccountId")),
                ReferenceNumber = reader.GetString(reader.GetOrdinal("ReferenceNumber")),
                PreviousReferenceNumber = reader.GetString(reader.GetOrdinal("PreviousReferenceNumber")),
                CRIBIDNumber = reader.IsDBNull(reader.GetOrdinal("CRIBIDNumber")) ? null : reader.GetString(reader.GetOrdinal("CRIBIDNumber")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                NIDSSAccountNumber = reader.IsDBNull(reader.GetOrdinal("NIDSSAccountNumber")) ? null : reader.GetString(reader.GetOrdinal("NIDSSAccountNumber")),
                LongName = reader.GetString(reader.GetOrdinal("LongName")),
                CenterCode = reader.GetString(reader.GetOrdinal("CenterCode")),
                BudgetUnit = reader.GetString(reader.GetOrdinal("BudgetUnit")),
                Corporation = reader.GetString(reader.GetOrdinal("Corporation")),
                BookCode = reader.GetString(reader.GetOrdinal("BookCode")),
                EconomicActivityCode = reader.GetString(reader.GetOrdinal("EconomicActivityCode")),
                OriginalReleaseDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("OriginalReleaseDate"))),
                StartOfTerm = DateTime.Parse(reader.GetString(reader.GetOrdinal("StartOfTerm"))),
                MaturityDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("MaturityDate"))),
                AccountType = reader.GetString(reader.GetOrdinal("AccountType")),
                Purpose = reader.IsDBNull(reader.GetOrdinal("Purpose")) ? null : reader.GetString(reader.GetOrdinal("Purpose")),
                FundSource = reader.GetString(reader.GetOrdinal("FundSource")),
                LendingProgram = reader.GetString(reader.GetOrdinal("LendingProgram")),
                Area = reader.GetString(reader.GetOrdinal("Area")),
                IsRestructured = reader.GetInt32(reader.GetOrdinal("IsRestructured")) == 1,
                TypeOfCredit = reader.GetString(reader.GetOrdinal("TypeOfCredit")),
                MaturityCode = reader.GetString(reader.GetOrdinal("MaturityCode")),
                PurposeOfCredit = reader.GetString(reader.GetOrdinal("PurposeOfCredit")),
                NumberOfRecords = reader.IsDBNull(reader.GetOrdinal("NumberOfRecords")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("NumberOfRecords")),
                IsGuaranteed = reader.GetInt32(reader.GetOrdinal("IsGuaranteed")) == 1,
                GuaranteedBy = reader.IsDBNull(reader.GetOrdinal("GuaranteedBy")) ? null : reader.GetString(reader.GetOrdinal("GuaranteedBy")),
                IsUnderLitigation = reader.GetInt32(reader.GetOrdinal("IsUnderLitigation")) == 1,
                LitigationDate = reader.IsDBNull(reader.GetOrdinal("LitigationDate")) ? (DateTime?)null : DateTime.Parse(reader.GetString(reader.GetOrdinal("LitigationDate"))),
                LoanStatus = reader.GetString(reader.GetOrdinal("LoanStatus")),
                LoanProjectType = reader.GetString(reader.GetOrdinal("LoanProjectType")),
                Currency = reader.GetString(reader.GetOrdinal("Currency")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                IsDraft = reader.GetInt32(reader.GetOrdinal("IsDraft")) == 1,
                ClosureDate = reader.IsDBNull(reader.GetOrdinal("ClosureDate")) ? (DateTime?)null : DateTime.Parse(reader.GetString(reader.GetOrdinal("ClosureDate"))),
                CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                CreatedDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("CreatedDate"))),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? null : reader.GetString(reader.GetOrdinal("ModifiedBy")),
                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : DateTime.Parse(reader.GetString(reader.GetOrdinal("ModifiedDate"))),
                DeletedBy = reader.IsDBNull(reader.GetOrdinal("DeletedBy")) ? null : reader.GetString(reader.GetOrdinal("DeletedBy")),
                DeletedDate = reader.IsDBNull(reader.GetOrdinal("DeletedDate")) ? (DateTime?)null : DateTime.Parse(reader.GetString(reader.GetOrdinal("DeletedDate")))
            };
        }

        #endregion
    }
}
