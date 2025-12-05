<%@ Page Title="Create Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="ManservLoanSystem.Web.Features.GeneralAccountManagement.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>➕ Create New Loan Account</h2>
    <p>Enter the loan account information below. Fields marked with <span style="color:red;">*</span> are required.</p>

    <!-- Success/Error Messages -->
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
        <strong>Success!</strong> Account created successfully. Account ID: <asp:Label ID="lblNewAccountId" runat="server"></asp:Label>
    </asp:Panel>
    
    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-error">
        <strong>Error!</strong> <asp:Label ID="lblError" runat="server"></asp:Label>
    </asp:Panel>

    <!-- General Information Section -->
    <h3>General Information</h3>
    <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="txtReferenceNumber">Reference Number <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtReferenceNumber" runat="server" MaxLength="17" placeholder="e.g., LA-2025-0001"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvReferenceNumber" runat="server" 
                ControlToValidate="txtReferenceNumber" 
                ErrorMessage="Reference Number is required" 
                ForeColor="Red" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="txtPreviousReferenceNumber">Previous Reference Number <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtPreviousReferenceNumber" runat="server" MaxLength="17" placeholder="e.g., LA-2024-9999"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPreviousReferenceNumber" runat="server" 
                ControlToValidate="txtPreviousReferenceNumber" 
                ErrorMessage="Previous Reference Number is required" 
                ForeColor="Red" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="txtCustomerName">Customer Name <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtCustomerName" runat="server" MaxLength="40" placeholder="e.g., Juan Dela Cruz"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" 
                ControlToValidate="txtCustomerName" 
                ErrorMessage="Customer Name is required" 
                ForeColor="Red" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="txtLongName">Long Name <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtLongName" runat="server" MaxLength="100" placeholder="e.g., Juan Dela Cruz Manufacturing Corporation"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLongName" runat="server" 
                ControlToValidate="txtLongName" 
                ErrorMessage="Long Name is required" 
                ForeColor="Red" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="txtCRIBIDNumber">CRIB ID Number</label>
            <asp:TextBox ID="txtCRIBIDNumber" runat="server" MaxLength="10" placeholder="Optional"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtNIDSSAccountNumber">NIDSS Account Number</label>
            <asp:TextBox ID="txtNIDSSAccountNumber" runat="server" MaxLength="13" placeholder="Optional"></asp:TextBox>
        </div>
    </div>

    <!-- Account Identification Section -->
    <h3>Account Identification</h3>
    <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="ddlCenterCode">Center Code <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlCenterCode" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="txtBudgetUnit">Budget Unit <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtBudgetUnit" runat="server" MaxLength="3" placeholder="e.g., 001"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlCorporation">Corporation <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlCorporation" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlBookCode">Book Code <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlBookCode" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlEconomicActivity">Economic Activity <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlEconomicActivity" runat="server"></asp:DropDownList>
        </div>
    </div>

    <!-- Loan Dates Section -->
    <h3>Loan Dates</h3>
    <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="txtOriginalReleaseDate">Original Release Date <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtOriginalReleaseDate" runat="server" TextMode="Date"></asp:TextBox>
            <small style="color: #666;">Note: Start of Term will be set equal to this date</small>
        </div>

        <div class="form-group">
            <label for="txtMaturityDate">Maturity Date <span style="color:red;">*</span></label>
            <asp:TextBox ID="txtMaturityDate" runat="server" TextMode="Date"></asp:TextBox>
            <small style="color: #666;">Must be after Original Release Date</small>
        </div>
    </div>

    <!-- Account Type and Funding Section -->
    <h3>Account Type and Funding</h3>
    <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px;">
        
        <div class="form-group">
            <label for="ddlAccountType">Account Type <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlAccountType" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlFundSource">Fund Source <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlFundSource" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlLendingProgram">Lending Program <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlLendingProgram" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlArea">Area <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlMaturityCode">Maturity Code <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlMaturityCode" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlCurrency">Currency <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlCurrency" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlLoanProjectType">Loan Project Type <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlLoanProjectType" runat="server">
                <asp:ListItem Value="">-- Select --</asp:ListItem>
                <asp:ListItem Value="C">C - Commercial</asp:ListItem>
                <asp:ListItem Value="D">D - Developmental</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <!-- Buttons -->
    <div style="margin-top: 30px;">
        <asp:Button ID="btnSave" runat="server" Text="💾 Save Account" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="❌ Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
    </div>

</asp:Content>
