using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SIMPLE_UpdateAccount : Page
{
    // Sample data to simulate database
    private static readonly string[,] SampleAccounts = {
        {"1", "LA-2025-0001", "LA-2024-9999", "Juan Dela Cruz", "Juan Dela Cruz Manufacturing Corp", "1234567890", "001", "LOAN"},
        {"2", "LA-2025-0002", "LA-2024-9998", "Maria Santos", "Maria Santos Trading Inc", "2345678901", "002", "LOAN"},
        {"3", "LA-2025-0003", "LA-2024-9997", "Pedro Gonzales", "Pedro Gonzales Construction", "3456789012", "003", "LOAN"}
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Check if account ID is provided in query string (for direct access)
            string accountIdParam = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(accountIdParam))
            {
                int accountId;
                if (int.TryParse(accountIdParam, out accountId))
                {
                    hfAccountId.Value = accountId.ToString();
                    LoadAccountById(accountId);
                    pnlAccountInfo.Visible = true;
                }
            }
            else
            {
                // Show only the search section initially
                pnlAccountInfo.Visible = false;
            }
        }
    }

    protected void btnLoadAccount_Click(object sender, EventArgs e)
    {
        try
        {
            // Clear any previous messages
            pnlError.Visible = false;
            pnlSuccess.Visible = false;

            string referenceNumber = txtSearchReferenceNumber.Text.Trim();
            string accountIdText = txtSearchAccountId.Text.Trim();

            if (string.IsNullOrEmpty(referenceNumber) && string.IsNullOrEmpty(accountIdText))
            {
                ShowError("Please enter either a Reference Number or Account ID to search.");
                return;
            }

            // Search by Account ID first if provided
            if (!string.IsNullOrEmpty(accountIdText))
            {
                int accountId;
                if (!int.TryParse(accountIdText, out accountId))
                {
                    ShowError("Invalid Account ID format. Please enter a valid number.");
                    return;
                }

                if (LoadAccountById(accountId))
                {
                    hfAccountId.Value = accountId.ToString();
                    pnlAccountInfo.Visible = true;
                    ClearSearchFields();
                }
                else
                {
                    ShowError($"Account with ID {accountId} not found.");
                }
            }
            // Search by Reference Number
            else if (!string.IsNullOrEmpty(referenceNumber))
            {
                int foundAccountId = FindAccountByReferenceNumber(referenceNumber);
                if (foundAccountId > 0)
                {
                    LoadAccountById(foundAccountId);
                    hfAccountId.Value = foundAccountId.ToString();
                    pnlAccountInfo.Visible = true;
                    ClearSearchFields();
                }
                else
                {
                    ShowError($"Account with Reference Number '{referenceNumber}' not found.");
                }
            }
        }
        catch (Exception ex)
        {
            ShowError($"Error searching for account: {ex.Message}");
        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        ClearSearchFields();
        pnlAccountInfo.Visible = false;
        pnlError.Visible = false;
        pnlSuccess.Visible = false;
        hfAccountId.Value = string.Empty;
    }

    protected void chkIsGuaranteed_CheckedChanged(object sender, EventArgs e)
    {
        UpdateGuaranteeFields();
    }

    protected void chkIsUnderLitigation_CheckedChanged(object sender, EventArgs e)
    {
        UpdateLitigationFields();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfAccountId.Value))
        {
            ShowError("Please load an account first before updating.");
            return;
        }

        if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()) || string.IsNullOrEmpty(txtLongName.Text.Trim()))
        {
            ShowError("Customer Name and Long Name are required.");
            return;
        }

        try
        {
            // Simulate update operation
            ShowSuccess();
            
            // In a real application, you would save to database here
            // For demo purposes, we just show success message
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
            // In real app: Response.Redirect($"ViewAccount.aspx?id={hfAccountId.Value}");
            ShowError("Cancel clicked - would redirect to ViewAccount.aspx");
        }
        else
        {
            // In real app: Response.Redirect("~/Default.aspx");
            ShowError("Cancel clicked - would redirect to Default.aspx");
        }
    }

    private bool LoadAccountById(int accountId)
    {
        // Search in sample data
        for (int i = 0; i < SampleAccounts.GetLength(0); i++)
        {
            if (SampleAccounts[i, 0] == accountId.ToString())
            {
                // Load account data
                lblReferenceNumber.Text = SampleAccounts[i, 1];
                txtPreviousReferenceNumber.Text = SampleAccounts[i, 2];
                txtCustomerName.Text = SampleAccounts[i, 3];
                txtLongName.Text = SampleAccounts[i, 4];
                txtCRIBIDNumber.Text = SampleAccounts[i, 5];
                txtCenterCode.Text = SampleAccounts[i, 6];
                txtAccountType.Text = SampleAccounts[i, 7];

                // Set default values for guarantee/litigation
                chkIsGuaranteed.Checked = false;
                txtGuaranteedBy.Text = string.Empty;
                chkIsUnderLitigation.Checked = false;
                txtLitigationDate.Text = string.Empty;

                UpdateGuaranteeFields();
                UpdateLitigationFields();

                return true;
            }
        }
        return false;
    }

    private int FindAccountByReferenceNumber(string referenceNumber)
    {
        for (int i = 0; i < SampleAccounts.GetLength(0); i++)
        {
            if (SampleAccounts[i, 1].Equals(referenceNumber, StringComparison.OrdinalIgnoreCase))
            {
                return int.Parse(SampleAccounts[i, 0]);
            }
        }
        return 0;
    }

    private void ClearSearchFields()
    {
        txtSearchReferenceNumber.Text = string.Empty;
        txtSearchAccountId.Text = string.Empty;
    }

    private void UpdateGuaranteeFields()
    {
        txtGuaranteedBy.Enabled = chkIsGuaranteed.Checked;
        if (!chkIsGuaranteed.Checked)
        {
            txtGuaranteedBy.Text = string.Empty;
            txtGuaranteedBy.CssClass = "disabled";
        }
        else
        {
            txtGuaranteedBy.CssClass = "";
        }
    }

    private void UpdateLitigationFields()
    {
        txtLitigationDate.Enabled = chkIsUnderLitigation.Checked;
        if (!chkIsUnderLitigation.Checked)
        {
            txtLitigationDate.Text = string.Empty;
            txtLitigationDate.CssClass = "disabled";
        }
        else
        {
            txtLitigationDate.CssClass = "";
        }
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