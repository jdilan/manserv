<%@ Page Title="View Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAccount.aspx.cs" Inherits="ManservLoanSystem.Web.Features.GeneralAccountManagement.ViewAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>👁️ View Loan Account</h2>

    <!-- Error Message -->
    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-error">
        <strong>Error!</strong> <asp:Label ID="lblError" runat="server"></asp:Label>
    </asp:Panel>

    <!-- Account Details -->
    <asp:Panel ID="pnlAccountDetails" runat="server" Visible="false">
        
        <!-- Action Buttons -->
        <div style="margin-bottom: 20px;">
            <asp:Button ID="btnEdit" runat="server" Text="✏️ Edit Account" CssClass="btn btn-primary" OnClick="btnEdit_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="🗑️ Delete Account" CssClass="btn btn-danger" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this account?');" />
            <asp:Button ID="btnBack" runat="server" Text="⬅️ Back to Search" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>

        <!-- General Information Section -->
        <h3>General Information</h3>
        <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px; background-color: #f9f9f9;">
            <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px;">
                <div>
                    <strong>Account ID:</strong><br />
                    <asp:Label ID="lblAccountId" runat="server" style="font-size: 16px;"></asp:Label>
                </div>
                <div>
                    <strong>Status:</strong><br />
                    <asp:Label ID="lblStatus" runat="server" style="font-size: 16px; font-weight: bold;"></asp:Label>
                </div>
                <div>
                    <strong>Reference Number:</strong><br />
                    <asp:Label ID="lblReferenceNumber" runat="server" style="font-size: 16px;"></asp:Label>
                </div>
                <div>
                    <strong>Previous Reference Number:</strong><br />
                    <asp:Label ID="lblPreviousReferenceNumber" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Customer Name:</strong><br />
                    <asp:Label ID="lblCustomerName" runat="server" style="font-size: 16px; color: #003366;"></asp:Label>
                </div>
                <div>
                    <strong>Long Name:</strong><br />
                    <asp:Label ID="lblLongName" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>CRIB ID Number:</strong><br />
                    <asp:Label ID="lblCRIBIDNumber" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>NIDSS Account Number:</strong><br />
                    <asp:Label ID="lblNIDSSAccountNumber" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Account Identification Section -->
        <h3>Account Identification</h3>
        <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px; background-color: #f9f9f9;">
            <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px;">
                <div>
                    <strong>Center Code:</strong><br />
                    <asp:Label ID="lblCenterCode" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Budget Unit:</strong><br />
                    <asp:Label ID="lblBudgetUnit" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Corporation:</strong><br />
                    <asp:Label ID="lblCorporation" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Book Code:</strong><br />
                    <asp:Label ID="lblBookCode" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Economic Activity:</strong><br />
                    <asp:Label ID="lblEconomicActivityCode" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Loan Dates Section -->
        <h3>Loan Dates</h3>
        <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px; background-color: #f9f9f9;">
            <div style="display: grid; grid-template-columns: 1fr 1fr 1fr; gap: 20px;">
                <div>
                    <strong>Original Release Date:</strong><br />
                    <asp:Label ID="lblOriginalReleaseDate" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Start of Term:</strong><br />
                    <asp:Label ID="lblStartOfTerm" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Maturity Date:</strong><br />
                    <asp:Label ID="lblMaturityDate" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Account Type and Funding Section -->
        <h3>Account Type and Funding</h3>
        <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px; background-color: #f9f9f9;">
            <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px;">
                <div>
                    <strong>Account Type:</strong><br />
                    <asp:Label ID="lblAccountType" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Purpose:</strong><br />
                    <asp:Label ID="lblPurpose" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Fund Source:</strong><br />
                    <asp:Label ID="lblFundSource" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Lending Program:</strong><br />
                    <asp:Label ID="lblLendingProgram" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Area:</strong><br />
                    <asp:Label ID="lblArea" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Maturity Code:</strong><br />
                    <asp:Label ID="lblMaturityCode" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Currency:</strong><br />
                    <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Loan Project Type:</strong><br />
                    <asp:Label ID="lblLoanProjectType" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Status and Classification Section -->
        <h3>Status and Classification</h3>
        <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px; background-color: #f9f9f9;">
            <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px;">
                <div>
                    <strong>Loan Status:</strong><br />
                    <asp:Label ID="lblLoanStatus" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Type of Credit:</strong><br />
                    <asp:Label ID="lblTypeOfCredit" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Purpose of Credit:</strong><br />
                    <asp:Label ID="lblPurposeOfCredit" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Is Restructured:</strong><br />
                    <asp:Label ID="lblIsRestructured" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Is Guaranteed:</strong><br />
                    <asp:Label ID="lblIsGuaranteed" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Guaranteed By:</strong><br />
                    <asp:Label ID="lblGuaranteedBy" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Is Under Litigation:</strong><br />
                    <asp:Label ID="lblIsUnderLitigation" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Litigation Date:</strong><br />
                    <asp:Label ID="lblLitigationDate" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Audit Information Section -->
        <h3>Audit Information</h3>
        <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px; background-color: #f9f9f9;">
            <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px;">
                <div>
                    <strong>Created By:</strong><br />
                    <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Created Date:</strong><br />
                    <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Modified By:</strong><br />
                    <asp:Label ID="lblModifiedBy" runat="server"></asp:Label>
                </div>
                <div>
                    <strong>Modified Date:</strong><br />
                    <asp:Label ID="lblModifiedDate" runat="server"></asp:Label>
                </div>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
