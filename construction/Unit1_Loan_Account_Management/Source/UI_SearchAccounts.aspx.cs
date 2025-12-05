/*
================================================================================
MANSERV Loan Account Management System
Page Code-Behind: SearchAccounts.aspx.cs
================================================================================
Purpose: Handle search accounts page logic
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManservLoanSystem.Data;
using ManservLoanSystem.ExternalServices;
using ManservLoanSystem.Repositories;
using ManservLoanSystem.Services;

namespace ManservLoanSystem.Web.Features.AccountOperations
{
    public partial class SearchAccounts : Page
    {
        private IAccountQueryService _queryService;
        private ReferenceDataServiceStub _referenceDataService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize services
            InitializeServices();

            if (!IsPostBack)
            {
                // Populate dropdowns
                PopulateDropdowns();
                
                // Load all accounts by default
                LoadAccounts();
            }
        }

        private void InitializeServices()
        {
            var context = new ManservDbContext();
            var repository = new AccountRepository(context);
            _queryService = new AccountQueryService(repository);
            _referenceDataService = new ReferenceDataServiceStub();
        }

        private void PopulateDropdowns()
        {
            // Centers
            ddlSearchCenterCode.Items.Clear();
            ddlSearchCenterCode.Items.Add(new ListItem("-- All Centers --", ""));
            foreach (var item in _referenceDataService.GetCenters())
            {
                ddlSearchCenterCode.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }

            // Account Types
            ddlSearchAccountType.Items.Clear();
            ddlSearchAccountType.Items.Add(new ListItem("-- All Types --", ""));
            foreach (var item in _referenceDataService.GetAccountTypes())
            {
                ddlSearchAccountType.Items.Add(new ListItem($"{item.Code} - {item.Description}", item.Code));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear search fields
            txtSearchRefNo.Text = string.Empty;
            txtSearchCustomerName.Text = string.Empty;
            ddlSearchCenterCode.SelectedIndex = 0;
            ddlSearchStatus.SelectedIndex = 0;
            ddlSearchAccountType.SelectedIndex = 0;

            // Reload all accounts
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            try
            {
                // Get search criteria
                string refNo = txtSearchRefNo.Text.Trim();
                string customerName = txtSearchCustomerName.Text.Trim();
                string centerCode = ddlSearchCenterCode.SelectedValue;
                string status = ddlSearchStatus.SelectedValue;
                string accountType = ddlSearchAccountType.SelectedValue;

                // Call service
                var response = _queryService.SearchAccounts(
                    refNo,
                    customerName,
                    centerCode,
                    status,
                    accountType,
                    "SYSTEM"); // TODO: Get actual user ID

                if (response.IsSuccess)
                {
                    // Bind to grid
                    gvAccounts.DataSource = response.Data;
                    gvAccounts.DataBind();

                    // Show result count
                    lblResultCount.Text = $"Found {response.Data.Count} account(s)";
                }
                else
                {
                    lblResultCount.Text = $"Error: {response.GetErrorMessage()}";
                    gvAccounts.DataSource = null;
                    gvAccounts.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblResultCount.Text = $"Error: {ex.Message}";
                gvAccounts.DataSource = null;
                gvAccounts.DataBind();
            }
        }

        protected void gvAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int accountId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "ViewAccount")
            {
                // Redirect to view page
                Response.Redirect($"~/Features/GeneralAccountManagement/ViewAccount.aspx?id={accountId}");
            }
            else if (e.CommandName == "EditAccount")
            {
                // Redirect to edit page
                Response.Redirect($"~/Features/GeneralAccountManagement/UpdateAccount.aspx?id={accountId}");
            }
        }
    }
}
