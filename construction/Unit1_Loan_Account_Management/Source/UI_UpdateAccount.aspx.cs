/*
================================================================================
MANSERV Loan Account Management System
Page Code-Behind: UpdateAccount.aspx.cs
================================================================================
Purpose: Handle update account page logic
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManservLoanSystem.Data;
using ManservLoanSystem.ExternalServices;
using ManservLoanSystem.Models.DTOs;
using ManservLoanSystem.Repositories;
using ManservLoanSystem.Services;

namespace ManservLoanSystem.Web.Features.GeneralAccountManagement
{
    public partial class UpdateAccount : Page
    {
        private IAccountManagementService _accountService;
        private IAccountQueryService _accountQueryService;
        private ReferenceDataServiceStub _referenceDataService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize services
            InitializeServices();

            if (!IsPostBack)
            {
                // Populate dropdowns
                PopulateDropdowns();

                // Check if account ID is provided in query string (for direct access)
                int accountId;
                if (int.TryParse(Request.QueryString["id"], out accountId))
                {
                    hfAccountId.Value = accountId.ToString();
                    LoadAccount(accountId);
                    pnlAccountInfo.Visible = true;
                }
                else
                {
                    // Show only the search section initially
                    pnlAccountInfo.Visible = false;
                }
            }
        }

        private void InitializeServices()
        {
            var context = new ManservDbContext();
            var repository = new AccountRepository(context);
            _accountService = new AccountManagementService(repository);
            _accountQueryService = new AccountQueryService(repository);
            _referenceDataService = new ReferenceDataServiceStub();
        }

        private void PopulateDropdowns()
        {
            // Centers
            ddlCenterCode.Items.Clear();
            ddlCenterCode.Items.Add(new ListItem("-- Select Center --", ""));
            foreach (var item in _referenceDataService.GetCenters())
            {
                ddlCenterCode.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Corporation
            ddlCorporation.Items.Clear();
            ddlCorporation.Items.Add(new ListItem("-- Select Corporation --", ""));
            foreach (var item in _referenceDataService.GetReferenceData("0100"))
            {
                ddlCorporation.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Book Code
            ddlBookCode.Items.Clear();
            ddlBookCode.Items.Add(new ListItem("-- Select Book Code --", ""));
            foreach (var item in _referenceDataService.GetReferenceData("0110"))
            {
                ddlBookCode.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Economic Activity
            ddlEconomicActivity.Items.Clear();
            ddlEconomicActivity.Items.Add(new ListItem("-- Select Economic Activity --", ""));
            foreach (var item in _referenceDataService.GetEconomicActivities())
            {
                ddlEconomicActivity.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Account Type
            ddlAccountType.Items.Clear();
            ddlAccountType.Items.Add(new ListItem("-- Select Account Type --", ""));
            foreach (var item in _referenceDataService.GetAccountTypes())
            {
                ddlAccountType.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Fund Source
            ddlFundSource.Items.Clear();
            ddlFundSource.Items.Add(new ListItem("-- Select Fund Source --", ""));
            foreach (var item in _referenceDataService.GetReferenceData("0024"))
            {
                ddlFundSource.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Lending Program
            ddlLendingProgram.Items.Clear();
            ddlLendingProgram.Items.Add(new ListItem("-- Select Lending Program --", ""));
            foreach (var item in _referenceDataService.GetReferenceData("0025"))
            {
                ddlLendingProgram.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Area
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem("-- Select Area --", ""));
            foreach (var item in _referenceDataService.GetReferenceData("0014"))
            {
                ddlArea.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Maturity Code
            ddlMaturityCode.Items.Clear();
            ddlMaturityCode.Items.Add(new ListItem("-- Select Maturity Code --", ""));
            foreach (var item in _referenceDataService.GetReferenceData("0022"))
            {
                ddlMaturityCode.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Currency
            ddlCurrency.Items.Clear();
            ddlCurrency.Items.Add(new ListItem("-- Select Currency --", ""));
            foreach (var item in _referenceDataService.GetReferenceData("0090"))
            {
                ddlCurrency.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }
        }

        private void LoadAccount(int accountId)
        {
            try
            {
                var response = _accountService.GetAccount(accountId, "SYSTEM"); // TODO: Get actual user ID

                if (response.IsSuccess)
                {
                    LoadAccountData(response.Data);
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

        private void LoadAccountData(AccountDTO account)
        {
            // General Information
            lblReferenceNumber.Text = account.ReferenceNumber;
            txtPreviousReferenceNumber.Text = account.PreviousReferenceNumber;
            txtCustomerName.Text = account.CustomerName;
            txtLongName.Text = account.LongName;
            txtCRIBIDNumber.Text = account.CRIBIDNumber;
            txtNIDSSAccountNumber.Text = account.NIDSSAccountNumber;

            // Account Identification
            SetDropDownValue(ddlCenterCode, account.CenterCode);
            txtBudgetUnit.Text = account.BudgetUnit;
            SetDropDownValue(ddlCorporation, account.Corporation);
            SetDropDownValue(ddlBookCode, account.BookCode);
            SetDropDownValue(ddlEconomicActivity, account.EconomicActivityCode);

            // Loan Dates
            txtOriginalReleaseDate.Text = account.OriginalReleaseDate.ToString("yyyy-MM-dd");
            txtMaturityDate.Text = account.MaturityDate.ToString("yyyy-MM-dd");

            // Account Type and Funding
            SetDropDownValue(ddlAccountType, account.AccountType);
            SetDropDownValue(ddlFundSource, account.FundSource);
            SetDropDownValue(ddlLendingProgram, account.LendingProgram);
            SetDropDownValue(ddlArea, account.Area);
            SetDropDownValue(ddlMaturityCode, account.MaturityCode);
            SetDropDownValue(ddlCurrency, account.Currency);
            SetDropDownValue(ddlLoanProjectType, account.LoanProjectType);

            // Guarantee and Litigation Information
            chkIsGuaranteed.Checked = account.IsGuaranteed;
            txtGuaranteedBy.Text = account.GuaranteedBy ?? string.Empty;
            chkIsUnderLitigation.Checked = account.IsUnderLitigation;
            txtLitigationDate.Text = account.LitigationDate?.ToString("yyyy-MM-dd") ?? string.Empty;

            // Enable/disable dependent fields
            UpdateGuaranteeFields();
            UpdateLitigationFields();
        }

        protected void chkIsGuaranteed_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuaranteeFields();
        }

        protected void chkIsUnderLitigation_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLitigationFields();
        }

        protected void btnLoadAccount_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear any previous error messages
                pnlError.Visible = false;
                pnlSuccess.Visible = false;

                // Validate that at least one search criteria is provided
                string referenceNumber = txtSearchReferenceNumber.Text.Trim();
                string accountIdText = txtSearchAccountId.Text.Trim();

                if (string.IsNullOrEmpty(referenceNumber) && string.IsNullOrEmpty(accountIdText))
                {
                    ShowError("Please enter either a Reference Number or Account ID to search.");
                    return;
                }

                AccountDTO foundAccount = null;

                // Search by Account ID first if provided
                if (!string.IsNullOrEmpty(accountIdText))
                {
                    int accountId;
                    if (!int.TryParse(accountIdText, out accountId))
                    {
                        ShowError("Invalid Account ID format. Please enter a valid number.");
                        return;
                    }

                    var response = _accountService.GetAccount(accountId, "SYSTEM");
                    if (response.IsSuccess)
                    {
                        foundAccount = response.Data;
                    }
                    else
                    {
                        ShowError($"Account with ID {accountId} not found.");
                        return;
                    }
                }
                // Search by Reference Number if Account ID not provided
                else if (!string.IsNullOrEmpty(referenceNumber))
                {
                    var searchResponse = _accountQueryService.SearchAccounts(
                        referenceNumber: referenceNumber,
                        userId: "SYSTEM");

                    if (searchResponse.IsSuccess && searchResponse.Data.Count > 0)
                    {
                        foundAccount = searchResponse.Data[0]; // Take the first match
                    }
                    else
                    {
                        ShowError($"Account with Reference Number '{referenceNumber}' not found.");
                        return;
                    }
                }

                if (foundAccount != null)
                {
                    // Store the account ID and load the account
                    hfAccountId.Value = foundAccount.AccountId.ToString();
                    LoadAccountData(foundAccount);
                    pnlAccountInfo.Visible = true;

                    // Clear search fields
                    txtSearchReferenceNumber.Text = string.Empty;
                    txtSearchAccountId.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error searching for account: {ex.Message}");
            }
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            // Clear search fields
            txtSearchReferenceNumber.Text = string.Empty;
            txtSearchAccountId.Text = string.Empty;
            
            // Hide account information panel
            pnlAccountInfo.Visible = false;
            
            // Clear any messages
            pnlError.Visible = false;
            pnlSuccess.Visible = false;
            
            // Clear hidden field
            hfAccountId.Value = string.Empty;
        }

        private void UpdateGuaranteeFields()
        {
            txtGuaranteedBy.Enabled = chkIsGuaranteed.Checked;
            if (!chkIsGuaranteed.Checked)
            {
                txtGuaranteedBy.Text = string.Empty;
                txtGuaranteedBy.BackColor = System.Drawing.Color.FromName("#f0f0f0");
            }
            else
            {
                txtGuaranteedBy.BackColor = System.Drawing.Color.White;
            }
        }

        private void UpdateLitigationFields()
        {
            txtLitigationDate.Enabled = chkIsUnderLitigation.Checked;
            if (!chkIsUnderLitigation.Checked)
            {
                txtLitigationDate.Text = string.Empty;
                txtLitigationDate.BackColor = System.Drawing.Color.FromName("#f0f0f0");
            }
            else
            {
                txtLitigationDate.BackColor = System.Drawing.Color.White;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            // Check if an account is loaded
            if (string.IsNullOrEmpty(hfAccountId.Value))
            {
                ShowError("Please load an account first before updating.");
                return;
            }

            try
            {
                int accountId = int.Parse(hfAccountId.Value);

                // Get current account data first to preserve non-editable fields
                var currentResponse = _accountService.GetAccount(accountId, "SYSTEM");
                if (!currentResponse.IsSuccess)
                {
                    ShowError("Error retrieving current account data: " + currentResponse.GetErrorMessage());
                    return;
                }

                var currentAccount = currentResponse.Data;

                // Create AccountDTO with only editable fields updated
                var accountDTO = new AccountDTO
                {
                    AccountId = accountId,
                    ReferenceNumber = currentAccount.ReferenceNumber,
                    PreviousReferenceNumber = currentAccount.PreviousReferenceNumber,
                    
                    // Editable fields
                    CustomerName = txtCustomerName.Text.Trim(),
                    LongName = txtLongName.Text.Trim(),
                    IsGuaranteed = chkIsGuaranteed.Checked,
                    GuaranteedBy = chkIsGuaranteed.Checked ? txtGuaranteedBy.Text.Trim() : null,
                    IsUnderLitigation = chkIsUnderLitigation.Checked,
                    LitigationDate = chkIsUnderLitigation.Checked && !string.IsNullOrEmpty(txtLitigationDate.Text) 
                        ? DateTime.Parse(txtLitigationDate.Text) : (DateTime?)null,

                    // Preserve existing non-editable fields
                    CRIBIDNumber = currentAccount.CRIBIDNumber,
                    NIDSSAccountNumber = currentAccount.NIDSSAccountNumber,
                    CenterCode = currentAccount.CenterCode,
                    BudgetUnit = currentAccount.BudgetUnit,
                    Corporation = currentAccount.Corporation,
                    BookCode = currentAccount.BookCode,
                    EconomicActivityCode = currentAccount.EconomicActivityCode,
                    OriginalReleaseDate = currentAccount.OriginalReleaseDate,
                    StartOfTerm = currentAccount.StartOfTerm,
                    MaturityDate = currentAccount.MaturityDate,
                    AccountType = currentAccount.AccountType,
                    FundSource = currentAccount.FundSource,
                    LendingProgram = currentAccount.LendingProgram,
                    Area = currentAccount.Area,
                    MaturityCode = currentAccount.MaturityCode,
                    Currency = currentAccount.Currency,
                    LoanProjectType = currentAccount.LoanProjectType,
                    LoanStatus = currentAccount.LoanStatus,
                    Status = currentAccount.Status,
                    IsDraft = currentAccount.IsDraft,
                    IsRestructured = currentAccount.IsRestructured,
                    TypeOfCredit = currentAccount.TypeOfCredit,
                    PurposeOfCredit = currentAccount.PurposeOfCredit
                };

                // Call service to update account
                var response = _accountService.UpdateAccount(accountId, accountDTO, "SYSTEM"); // TODO: Get actual user ID

                if (response.IsSuccess)
                {
                    ShowSuccess();
                }
                else
                {
                    ShowError(response.GetErrorMessage());
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error updating account: {ex.Message}");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfAccountId.Value))
            {
                int accountId = int.Parse(hfAccountId.Value);
                Response.Redirect($"ViewAccount.aspx?id={accountId}");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void SetDropDownValue(DropDownList ddl, string value)
        {
            var item = ddl.Items.FindByValue(value);
            if (item != null)
                ddl.SelectedValue = value;
        }

        private void ShowSuccess()
        {
            pnlSuccess.Visible = true;
            pnlError.Visible = false;
        }

        private void ShowError(string message)
        {
            pnlError.Visible = true;
            pnlSuccess.Visible = false;
            lblError.Text = message;
        }
    }
}
