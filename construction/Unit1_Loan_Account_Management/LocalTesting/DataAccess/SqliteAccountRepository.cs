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