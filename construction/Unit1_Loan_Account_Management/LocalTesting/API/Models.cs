/*
================================================================================
MANSERV Loan Account Management System - Local Testing
Model Classes (Stub Implementation)
================================================================================
Purpose: Stub model classes for local testing
Note: These are simplified versions for testing purposes
================================================================================
*/

using System;
using System.Collections.Generic;

namespace ManservLoanSystem.Models.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string ReferenceNumber { get; set; }
        public string PreviousReferenceNumber { get; set; }
        public string CRIBIDNumber { get; set; }
        public string CustomerName { get; set; }
        public string NIDSSAccountNumber { get; set; }
        public string LongName { get; set; }
        public string CenterCode { get; set; }
        public string BudgetUnit { get; set; }
        public string Corporation { get; set; }
        public string BookCode { get; set; }
        public string EconomicActivityCode { get; set; }
        public DateTime OriginalReleaseDate { get; set; }
        public DateTime StartOfTerm { get; set; }
        public DateTime MaturityDate { get; set; }
        public string AccountType { get; set; }
        public string Purpose { get; set; }
        public string FundSource { get; set; }
        public string LendingProgram { get; set; }
        public string Area { get; set; }
        public bool IsRestructured { get; set; }
        public string TypeOfCredit { get; set; }
        public string MaturityCode { get; set; }
        public string PurposeOfCredit { get; set; }
        public int? NumberOfRecords { get; set; }
        public bool IsGuaranteed { get; set; }
        public string GuaranteedBy { get; set; }
        public bool IsUnderLitigation { get; set; }
        public DateTime? LitigationDate { get; set; }
        public string LoanStatus { get; set; }
        public string LoanProjectType { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public bool IsDraft { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    public class AccountAudit
    {
        public int AuditId { get; set; }
        public int AccountId { get; set; }
        public string ReferenceNumber { get; set; }
        public string Action { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }
        public string UserRole { get; set; }
        public string IPAddress { get; set; }
        public string Comments { get; set; }
        public Account Account { get; set; }
    }

    public class AccountRelationship
    {
        public int RelationshipId { get; set; }
        public int SourceAccountId { get; set; }
        public int TargetAccountId { get; set; }
        public string RelationshipType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Account SourceAccount { get; set; }
        public Account TargetAccount { get; set; }
    }
}

namespace ManservLoanSystem.Models.Common
{
    public class ServiceResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public List<ServiceError> Errors { get; set; }

        public static ServiceResponse<T> Success(T data)
        {
            return new ServiceResponse<T>
            {
                Status = "Success",
                Data = data,
                Errors = new List<ServiceError>()
            };
        }

        public static ServiceResponse<T> Failure(string errorCode, string message)
        {
            return new ServiceResponse<T>
            {
                Status = "Failure",
                Data = default(T),
                Errors = new List<ServiceError>
                {
                    new ServiceError { ErrorCode = errorCode, Message = message }
                }
            };
        }
    }

    public class ServiceError
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Field { get; set; }
    }
}

namespace ManservLoanSystem.Repositories
{
    public interface IAccountRepository
    {
        int Create(ManservLoanSystem.Models.Entities.Account account);
        ManservLoanSystem.Models.Entities.Account GetById(int accountId);
        ManservLoanSystem.Models.Entities.Account GetByReferenceNumber(string referenceNumber);
        int Update(ManservLoanSystem.Models.Entities.Account account);
        bool Delete(int accountId, string userId);
        List<ManservLoanSystem.Models.Entities.Account> GetAll(bool includeDeleted = false);
        List<ManservLoanSystem.Models.Entities.Account> Search(string referenceNumber = null, string customerName = null, string centerCode = null, string status = null, string accountType = null);
        bool ExistsByReferenceNumber(string referenceNumber, int? excludeAccountId = null);
        List<ManservLoanSystem.Models.Entities.Account> GetByCenterCode(string centerCode);
        List<ManservLoanSystem.Models.Entities.Account> GetByStatus(string status);
        void CreateAudit(ManservLoanSystem.Models.Entities.AccountAudit audit);
        List<ManservLoanSystem.Models.Entities.AccountAudit> GetAuditHistory(int accountId);
        void CreateRelationship(ManservLoanSystem.Models.Entities.AccountRelationship relationship);
        List<ManservLoanSystem.Models.Entities.AccountRelationship> GetRelationships(int accountId);
    }
}

namespace ManservLoanSystem.ExternalServices
{
    public interface ICustomerQueryService { }
    public interface IReferenceDataService { }
    public interface IAccountTypeService { }
    public interface IEconomicActivityService { }
    public interface ICenterService { }
    public interface IValidationService { }
    public interface IAccessControlService { }
}

namespace ManservLoanSystem.Models.DTOs
{
    public class AccountDTO
    {
        public string ReferenceNumber { get; set; }
        public string PreviousReferenceNumber { get; set; }
        public string CRIBIDNumber { get; set; }
        public string CustomerName { get; set; }
        public string NIDSSAccountNumber { get; set; }
        public string LongName { get; set; }
        public string CenterCode { get; set; }
        public string BudgetUnit { get; set; }
        public string Corporation { get; set; }
        public string BookCode { get; set; }
        public string EconomicActivityCode { get; set; }
        public DateTime OriginalReleaseDate { get; set; }
        public DateTime StartOfTerm { get; set; }
        public DateTime MaturityDate { get; set; }
        public string AccountType { get; set; }
        public string Purpose { get; set; }
        public string FundSource { get; set; }
        public string LendingProgram { get; set; }
        public string Area { get; set; }
        public bool IsRestructured { get; set; }
        public string TypeOfCredit { get; set; }
        public string MaturityCode { get; set; }
        public string PurposeOfCredit { get; set; }
        public int? NumberOfRecords { get; set; }
        public bool IsGuaranteed { get; set; }
        public string GuaranteedBy { get; set; }
        public bool IsUnderLitigation { get; set; }
        public DateTime? LitigationDate { get; set; }
        public string LoanStatus { get; set; }
        public string LoanProjectType { get; set; }
        public string Currency { get; set; }
    }
}
