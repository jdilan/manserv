/*
================================================================================
MANSERV Loan Account Management System
Page Code-Behind: CreateAccount.aspx.cs
================================================================================
Purpose: Handle create account page logic
Author: System Generated
Date: December 5, 2025

INSTRUCTIONS FOR INTEGRATION:
1. Copy this file to Features/GeneralAccountManagement/ folder
2. Update namespace to match your project
3. Ensure all services are registered in DI container
4. Update using statements as needed
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
    public partial class CreateAccount : Page
    {
        // Services (in real app, these would be injected via DI)
        private IAccountManagementService _accountService;
        private ReferenceDataServiceStub _referenceDataService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize services
            InitializeServices();

            if (!IsPostBack)
            {
                // Populate dropdowns
                PopulateDropdowns();
            }
        }

        /// <summary>
        /// Initialize services (simple factory pattern for demo)
        /// In production, use proper DI container
        /// </summary>
        private void InitializeServices()
        {
            var context = new ManservDbContext();
            var repository = new AccountRepository(context);
            _accountService = new AccountManagementService(repository);
            _referenceDataService = new ReferenceDataServiceStub();
        }

        /// <summary>
        /// Populate all dropdown lists with reference data
        /// </summary>
        private void PopulateDropdowns()
        {
            try
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
            catch (Exception ex)
            {
                ShowError($"Error loading dropdown data: {ex.Message}");
            }
        }

        /// <summary>
        /// Handle Save button click
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                // Create AccountDTO from form data
                var accountDTO = new AccountDTO
                {
                    // General Information
                    ReferenceNumber = txtReferenceNumber.Text.Trim(),
                    PreviousReferenceNumber = txtPreviousReferenceNumber.Text.Trim(),
                    CustomerName = txtCustomerName.Text.Trim(),
                    LongName = txtLongName.Text.Trim(),
                    CRIBIDNumber = txtCRIBIDNumber.Text.Trim(),
                    NIDSSAccountNumber = txtNIDSSAccountNumber.Text.Trim(),

                    // Account Identification
                    CenterCode = ddlCenterCode.SelectedValue,
                    BudgetUnit = txtBudgetUnit.Text.Trim(),
                    Corporation = ddlCorporation.SelectedValue,
                    BookCode = ddlBookCode.SelectedValue,
                    EconomicActivityCode = ddlEconomicActivity.SelectedValue,

                    // Loan Dates
                    OriginalReleaseDate = DateTime.Parse(txtOriginalReleaseDate.Text),
                    StartOfTerm = DateTime.Parse(txtOriginalReleaseDate.Text), // Must be equal
                    MaturityDate = DateTime.Parse(txtMaturityDate.Text),

                    // Account Type and Funding
                    AccountType = ddlAccountType.SelectedValue,
                    FundSource = ddlFundSource.SelectedValue,
                    LendingProgram = ddlLendingProgram.SelectedValue,
                    Area = ddlArea.SelectedValue,
                    MaturityCode = ddlMaturityCode.SelectedValue,
                    Currency = ddlCurrency.SelectedValue,
                    LoanProjectType = ddlLoanProjectType.SelectedValue,

                    // Default values
                    LoanStatus = "CUR", // Current
                    Status = "Active",
                    IsDraft = false,
                    IsRestructured = false,
                    IsGuaranteed = false,
                    IsUnderLitigation = false,
                    TypeOfCredit = "CUR", // Will be auto-populated by service
                    PurposeOfCredit = "P" // Will be auto-populated by service
                };

                // Call service to create account
                var response = _accountService.CreateAccount(accountDTO, "SYSTEM"); // TODO: Get actual user ID

                if (response.IsSuccess)
                {
                    // Show success message
                    ShowSuccess(response.Data);
                    
                    // Clear form
                    ClearForm();
                }
                else
                {
                    // Show error messages
                    ShowError(response.GetErrorMessage());
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error creating account: {ex.Message}");
            }
        }

        /// <summary>
        /// Handle Cancel button click
        /// </summary>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        /// <summary>
        /// Show success message
        /// </summary>
        private void ShowSuccess(int accountId)
        {
            pnlSuccess.Visible = true;
            pnlError.Visible = false;
            lblNewAccountId.Text = accountId.ToString();
        }

        /// <summary>
        /// Show error message
        /// </summary>
        private void ShowError(string message)
        {
            pnlError.Visible = true;
            pnlSuccess.Visible = false;
            lblError.Text = message;
        }

        /// <summary>
        /// Clear form fields
        /// </summary>
        private void ClearForm()
        {
            txtReferenceNumber.Text = string.Empty;
            txtPreviousReferenceNumber.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtLongName.Text = string.Empty;
            txtCRIBIDNumber.Text = string.Empty;
            txtNIDSSAccountNumber.Text = string.Empty;
            txtBudgetUnit.Text = string.Empty;
            txtOriginalReleaseDate.Text = string.Empty;
            txtMaturityDate.Text = string.Empty;

            // Reset dropdowns
            ddlCenterCode.SelectedIndex = 0;
            ddlCorporation.SelectedIndex = 0;
            ddlBookCode.SelectedIndex = 0;
            ddlEconomicActivity.SelectedIndex = 0;
            ddlAccountType.SelectedIndex = 0;
            ddlFundSource.SelectedIndex = 0;
            ddlLendingProgram.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlMaturityCode.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            ddlLoanProjectType.SelectedIndex = 0;
        }
    }
}
