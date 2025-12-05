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
        private ReferenceDataServiceStub _referenceDataService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize services
            InitializeServices();

            if (!IsPostBack)
            {
                // Get account ID from query string
                int accountId;
                if (!int.TryParse(Request.QueryString["id"], out accountId))
                {
                    ShowError("Invalid account ID");
                    return;
                }

                hfAccountId.Value = accountId.ToString();

                // Populate dropdowns
                PopulateDropdowns();

                // Load account data
                LoadAccount(accountId);
            }
        }

        private void InitializeServices()
        {
            var context = new ManservDbContext();
            var repository = new AccountRepository(context);
            _accountService = new AccountManagementService(repository);
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
                    var account = response.Data;

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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                int accountId = int.Parse(hfAccountId.Value);

                // Create AccountDTO from form data
                var accountDTO = new AccountDTO
                {
                    AccountId = accountId,
                    ReferenceNumber = lblReferenceNumber.Text, // Cannot be changed
                    PreviousReferenceNumber = txtPreviousReferenceNumber.Text.Trim(),
                    CustomerName = txtCustomerName.Text.Trim(),
                    LongName = txtLongName.Text.Trim(),
                    CRIBIDNumber = txtCRIBIDNumber.Text.Trim(),
                    NIDSSAccountNumber = txtNIDSSAccountNumber.Text.Trim(),
                    CenterCode = ddlCenterCode.SelectedValue,
                    BudgetUnit = txtBudgetUnit.Text.Trim(),
                    Corporation = ddlCorporation.SelectedValue,
                    BookCode = ddlBookCode.SelectedValue,
                    EconomicActivityCode = ddlEconomicActivity.SelectedValue,
                    OriginalReleaseDate = DateTime.Parse(txtOriginalReleaseDate.Text),
                    StartOfTerm = DateTime.Parse(txtOriginalReleaseDate.Text),
                    MaturityDate = DateTime.Parse(txtMaturityDate.Text),
                    AccountType = ddlAccountType.SelectedValue,
                    FundSource = ddlFundSource.SelectedValue,
                    LendingProgram = ddlLendingProgram.SelectedValue,
                    Area = ddlArea.SelectedValue,
                    MaturityCode = ddlMaturityCode.SelectedValue,
                    Currency = ddlCurrency.SelectedValue,
                    LoanProjectType = ddlLoanProjectType.SelectedValue,
                    LoanStatus = "CUR",
                    Status = "Active",
                    IsDraft = false,
                    IsRestructured = false,
                    IsGuaranteed = false,
                    IsUnderLitigation = false,
                    TypeOfCredit = "CUR",
                    PurposeOfCredit = "P"
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
            int accountId = int.Parse(hfAccountId.Value);
            Response.Redirect($"ViewAccount.aspx?id={accountId}");
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
