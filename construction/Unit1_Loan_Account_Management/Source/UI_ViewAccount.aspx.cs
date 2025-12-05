/*
================================================================================
MANSERV Loan Account Management System
Page Code-Behind: ViewAccount.aspx.cs
================================================================================
Purpose: Display account details in read-only mode
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;
using System.Web.UI;
using ManservLoanSystem.Data;
using ManservLoanSystem.Repositories;
using ManservLoanSystem.Services;

namespace ManservLoanSystem.Web.Features.GeneralAccountManagement
{
    public partial class ViewAccount : Page
    {
        private IAccountManagementService _accountService;
        private int _accountId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize services
            InitializeServices();

            // Get account ID from query string
            if (!int.TryParse(Request.QueryString["id"], out _accountId))
            {
                ShowError("Invalid account ID");
                return;
            }

            if (!IsPostBack)
            {
                LoadAccount();
            }
        }

        private void InitializeServices()
        {
            var context = new ManservDbContext();
            var repository = new AccountRepository(context);
            _accountService = new AccountManagementService(repository);
        }

        private void LoadAccount()
        {
            try
            {
                var response = _accountService.GetAccount(_accountId, "SYSTEM"); // TODO: Get actual user ID

                if (response.IsSuccess)
                {
                    var account = response.Data;
                    
                    // General Information
                    lblAccountId.Text = account.AccountId.ToString();
                    lblStatus.Text = account.Status;
                    lblStatus.ForeColor = account.Status == "Active" ? System.Drawing.Color.Green : System.Drawing.Color.Gray;
                    lblReferenceNumber.Text = account.ReferenceNumber;
                    lblPreviousReferenceNumber.Text = account.PreviousReferenceNumber;
                    lblCustomerName.Text = account.CustomerName;
                    lblLongName.Text = account.LongName;
                    lblCRIBIDNumber.Text = string.IsNullOrEmpty(account.CRIBIDNumber) ? "N/A" : account.CRIBIDNumber;
                    lblNIDSSAccountNumber.Text = string.IsNullOrEmpty(account.NIDSSAccountNumber) ? "N/A" : account.NIDSSAccountNumber;

                    // Account Identification
                    lblCenterCode.Text = account.CenterCode;
                    lblBudgetUnit.Text = account.BudgetUnit;
                    lblCorporation.Text = account.Corporation;
                    lblBookCode.Text = account.BookCode;
                    lblEconomicActivityCode.Text = account.EconomicActivityCode;

                    // Loan Dates
                    lblOriginalReleaseDate.Text = account.OriginalReleaseDate.ToString("yyyy-MM-dd");
                    lblStartOfTerm.Text = account.StartOfTerm.ToString("yyyy-MM-dd");
                    lblMaturityDate.Text = account.MaturityDate.ToString("yyyy-MM-dd");

                    // Account Type and Funding
                    lblAccountType.Text = account.AccountType;
                    lblPurpose.Text = string.IsNullOrEmpty(account.Purpose) ? "N/A" : account.Purpose;
                    lblFundSource.Text = account.FundSource;
                    lblLendingProgram.Text = account.LendingProgram;
                    lblArea.Text = account.Area;
                    lblMaturityCode.Text = account.MaturityCode;
                    lblCurrency.Text = account.Currency;
                    lblLoanProjectType.Text = account.LoanProjectType == "C" ? "C - Commercial" : "D - Developmental";

                    // Status and Classification
                    lblLoanStatus.Text = account.LoanStatus == "CUR" ? "CUR - Current" : "PDO - Past Due";
                    lblTypeOfCredit.Text = account.TypeOfCredit;
                    lblPurposeOfCredit.Text = account.PurposeOfCredit;
                    lblIsRestructured.Text = account.IsRestructured ? "Yes" : "No";
                    lblIsGuaranteed.Text = account.IsGuaranteed ? "Yes" : "No";
                    lblGuaranteedBy.Text = string.IsNullOrEmpty(account.GuaranteedBy) ? "N/A" : account.GuaranteedBy;
                    lblIsUnderLitigation.Text = account.IsUnderLitigation ? "Yes" : "No";
                    lblLitigationDate.Text = account.LitigationDate.HasValue ? account.LitigationDate.Value.ToString("yyyy-MM-dd") : "N/A";

                    // Audit Information (from entity, not in DTO - would need to add to DTO or get from entity)
                    lblCreatedBy.Text = "SYSTEM"; // TODO: Get from entity
                    lblCreatedDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); // TODO: Get from entity
                    lblModifiedBy.Text = "N/A"; // TODO: Get from entity
                    lblModifiedDate.Text = "N/A"; // TODO: Get from entity

                    pnlAccountDetails.Visible = true;
                }
                else
                {
                    ShowError(response.GetErrorMessage());
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error loading account: {ex.Message}");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect($"UpdateAccount.aspx?id={_accountId}");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var response = _accountService.DeleteAccount(_accountId, "SYSTEM"); // TODO: Get actual user ID

                if (response.IsSuccess)
                {
                    Response.Redirect("~/Features/AccountOperations/SearchAccounts.aspx?deleted=true");
                }
                else
                {
                    ShowError(response.GetErrorMessage());
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error deleting account: {ex.Message}");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Features/AccountOperations/SearchAccounts.aspx");
        }

        private void ShowError(string message)
        {
            pnlError.Visible = true;
            lblError.Text = message;
            pnlAccountDetails.Visible = false;
        }
    }
}
