/*
================================================================================
MANSERV Loan Account Management System
Page Code-Behind: Default.aspx.cs
================================================================================
Purpose: Dashboard/home page with statistics and recent accounts
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System;
using System.Linq;
using System.Web.UI;
using ManservLoanSystem.Data;
using ManservLoanSystem.Repositories;
using ManservLoanSystem.Services;

namespace ManservLoanSystem.Web
{
    public partial class _Default : Page
    {
        private IAccountQueryService _queryService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize services
            InitializeServices();

            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        private void InitializeServices()
        {
            var context = new ManservDbContext();
            var repository = new AccountRepository(context);
            _queryService = new AccountQueryService(repository);
        }

        private void LoadDashboardData()
        {
            try
            {
                // Get all accounts
                var response = _queryService.GetAllAccounts(null, "SYSTEM"); // TODO: Get actual user ID

                if (response.IsSuccess)
                {
                    var accounts = response.Data;

                    // Calculate statistics
                    lblTotalAccounts.Text = accounts.Count.ToString();
                    lblActiveAccounts.Text = accounts.Count(a => a.Status == "Active" && a.LoanStatus == "CUR").ToString();
                    lblPastDueAccounts.Text = accounts.Count(a => a.LoanStatus == "PDO").ToString();
                    lblClosedAccounts.Text = accounts.Count(a => a.Status == "Closed").ToString();

                    // Load recent accounts (last 10)
                    var recentAccounts = accounts
                        .OrderByDescending(a => a.AccountId)
                        .Take(10)
                        .ToList();

                    gvRecentAccounts.DataSource = recentAccounts;
                    gvRecentAccounts.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log error (in production, use proper logging)
                // For now, just show 0 for all stats
                lblTotalAccounts.Text = "Error";
                lblActiveAccounts.Text = "Error";
                lblPastDueAccounts.Text = "Error";
                lblClosedAccounts.Text = "Error";
            }
        }
    }
}
