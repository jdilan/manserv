<%@ Page Title="Update Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateAccount.aspx.cs" Inherits="ManservLoanSystem.Web.Features.GeneralAccountManagement.UpdateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }
        .form-group input[type="text"], .form-group input[type="date"], .form-group select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .editable-section {
            border: 2px solid #28a745;
            background-color: #f8fff9;
        }
        .readonly-section {
            border: 1px solid #ddd;
            background-color: #f9f9f9;
        }
        .section-header {
            color: #28a745;
            font-weight: bold;
        }
        .search-section {
            border: 2px solid #007bff;
            background-color: #f8f9ff;
        }
        .btn {
            padding: 8px 16px;
            margin: 5px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            text-decoration: none;
            display: inline-block;
        }
        .btn-info {
            background-color: #17a2b8;
            color: white;
        }
        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }
        .btn-primary {
            background-color: #007bff;
            color: white;
        }
    </style>
    <h2>✏️ Update Loan Account</h2>
    <p>Search for an account to update, then modify the Customer Name, Long Name, and Guarantee/Litigation information.</p>

    <!-- Success/Error Messages -->
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
        <strong>Success!</strong> Account updated successfully.
    </asp:Panel>
    
    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-error">
        <strong>Error!</strong> <asp:Label ID="lblError" runat="server"></asp:Label>
    </asp:Panel>

    <!-- Load Account Section -->
    <h3>🔍 Load Account</h3>
    <div class="search-section" style="padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        <div class="form-group">
            <label for="txtSearchReferenceNumber">Reference Number</label>
            <asp:TextBox ID="txtSearchReferenceNumber" runat="server" MaxLength="17" placeholder="e.g., LA-2025-0001"></asp:TextBox>
        </div>
        
        <div class="form-group" style="text-align: center; margin: 15px 0;">
            <strong>OR</strong>
        </div>
        
        <div class="form-group">
            <label for="txtSearchAccountId">Account ID</label>
            <asp:TextBox ID="txtSearchAccountId" runat="server" placeholder="e.g., 12345"></asp:TextBox>
        </div>
        
        <div style="margin-top: 15px;">
            <asp:Button ID="btnLoadAccount" runat="server" Text="🔍 Load Account" CssClass="btn btn-info" OnClick="btnLoadAccount_Click" CausesValidation="false" />
            <asp:Button ID="btnClearSearch" runat="server" Text="🗑️ Clear" CssClass="btn btn-secondary" OnClick="btnClearSearch_Click" CausesValidation="false" />
        </div>
    </div>

    <!-- Hidden field for Account ID -->
    <asp:HiddenField ID="hfAccountId" runat="server" />

    <!-- Account Information Panel (Initially Hidden) -->
    <asp:Panel ID="pnlAccountInfo" runat="server" Visible="false">

    <!-- General Information Section -->
    <h3 class="section-header">General Information (Customer Name and Long Name are editable)</h3>
    <div class="editable-section" style="padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label>Reference Number (Cannot be changed)</label>
            <asp:Label ID="lblReferenceNumber" runat="server" style="display: block; padding: 8px; background-color: #f0f0f0; border: 1px solid #ccc; border-radius: 4px;"></asp:Label>
        </div>

        <div class="form-group">
            <label for="txtPreviousReferenceNumber">Previous Reference Number</label>
            <asp:TextBox ID="txtPreviousReferenceNumber" runat="server" MaxLength="17" Enabled="false" BackColor="#f0f0f0"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtCustomerName">Customer Name <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtCustomerName" runat="server" MaxLength="40"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" 
                ControlToValidate="txtCustomerName" 
                ErrorMessage="Customer Name is required" 
                ForeColor="Red" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="txtLongName">Long Name <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtLongName" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLongName" runat="server" 
                ControlToValidate="txtLongName" 
                ErrorMessage="Long Name is required" 
                ForeColor="Red" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="txtCRIBIDNumber">CRIB ID Number</label>
            <asp:TextBox ID="txtCRIBIDNumber" runat="server" MaxLength="10" Enabled="false" BackColor="#f0f0f0"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtNIDSSAccountNumber">NIDSS Account Number</label>
            <asp:TextBox ID="txtNIDSSAccountNumber" runat="server" MaxLength="13" Enabled="false" BackColor="#f0f0f0"></asp:TextBox>
        </div>
    </div>

    <!-- Account Identification Section -->
    <h3>Account Identification (Read-only)</h3>
    <div class="readonly-section" style="padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="ddlCenterCode">Center Code</label>
            <asp:DropDownList ID="ddlCenterCode" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="txtBudgetUnit">Budget Unit</label>
            <asp:TextBox ID="txtBudgetUnit" runat="server" MaxLength="3" Enabled="false" BackColor="#f0f0f0"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlCorporation">Corporation</label>
            <asp:DropDownList ID="ddlCorporation" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlBookCode">Book Code</label>
            <asp:DropDownList ID="ddlBookCode" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlEconomicActivity">Economic Activity</label>
            <asp:DropDownList ID="ddlEconomicActivity" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>
    </div>

    <!-- Loan Dates Section -->
    <h3>Loan Dates (Read-only)</h3>
    <div class="readonly-section" style="padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="txtOriginalReleaseDate">Original Release Date</label>
            <asp:TextBox ID="txtOriginalReleaseDate" runat="server" TextMode="Date" Enabled="false" BackColor="#f0f0f0"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtMaturityDate">Maturity Date</label>
            <asp:TextBox ID="txtMaturityDate" runat="server" TextMode="Date" Enabled="false" BackColor="#f0f0f0"></asp:TextBox>
        </div>
    </div>

    <!-- Account Type and Funding Section -->
    <h3>Account Type and Funding (Read-only)</h3>
    <div class="readonly-section" style="padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="ddlAccountType">Account Type</label>
            <asp:DropDownList ID="ddlAccountType" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlFundSource">Fund Source</label>
            <asp:DropDownList ID="ddlFundSource" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlLendingProgram">Lending Program</label>
            <asp:DropDownList ID="ddlLendingProgram" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlArea">Area</label>
            <asp:DropDownList ID="ddlArea" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlMaturityCode">Maturity Code</label>
            <asp:DropDownList ID="ddlMaturityCode" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlCurrency">Currency</label>
            <asp:DropDownList ID="ddlCurrency" runat="server" Enabled="false" BackColor="#f0f0f0"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlLoanProjectType">Loan Project Type</label>
            <asp:DropDownList ID="ddlLoanProjectType" runat="server" Enabled="false" BackColor="#f0f0f0">
                <asp:ListItem Value="">-- Select --</asp:ListItem>
                <asp:ListItem Value="C">C - Commercial</asp:ListItem>
                <asp:ListItem Value="D">D - Developmental</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <!-- Guarantee and Litigation Section -->
    <h3 class="section-header">Guarantee and Litigation Information (Editable)</h3>
    <div class="editable-section" style="padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="chkIsGuaranteed">Is Guaranteed</label>
            <asp:CheckBox ID="chkIsGuaranteed" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsGuaranteed_CheckedChanged" />
        </div>

        <div class="form-group">
            <label for="txtGuaranteedBy">Guaranteed By</label>
            <asp:TextBox ID="txtGuaranteedBy" runat="server" MaxLength="100" placeholder="Enter guarantor name"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="chkIsUnderLitigation">Is Under Litigation</label>
            <asp:CheckBox ID="chkIsUnderLitigation" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsUnderLitigation_CheckedChanged" />
        </div>

        <div class="form-group">
            <label for="txtLitigationDate">Litigation Date</label>
            <asp:TextBox ID="txtLitigationDate" runat="server" TextMode="Date"></asp:TextBox>
        </div>
    </div>

    <!-- Buttons -->
    <div style="margin-top: 30px;">
        <asp:Button ID="btnUpdate" runat="server" Text="💾 Update Account" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="❌ Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
    </div>

    </asp:Panel> <!-- End of pnlAccountInfo -->

</asp:Content>
