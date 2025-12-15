<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SIMPLE_UpdateAccount.aspx.cs" Inherits="SIMPLE_UpdateAccount" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Update Account - Simple Version</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; }
        .form-group { margin-bottom: 15px; }
        .form-group label { display: block; font-weight: bold; margin-bottom: 5px; }
        .form-group input, .form-group select { width: 300px; padding: 8px; border: 1px solid #ccc; border-radius: 4px; }
        .editable-section { border: 2px solid #28a745; background-color: #f8fff9; padding: 20px; margin: 20px 0; border-radius: 4px; }
        .readonly-section { border: 1px solid #ddd; background-color: #f9f9f9; padding: 20px; margin: 20px 0; border-radius: 4px; }
        .search-section { border: 2px solid #007bff; background-color: #f8f9ff; padding: 20px; margin: 20px 0; border-radius: 4px; }
        .btn { padding: 8px 16px; margin: 5px; border: none; border-radius: 4px; cursor: pointer; }
        .btn-primary { background-color: #007bff; color: white; }
        .btn-info { background-color: #17a2b8; color: white; }
        .btn-secondary { background-color: #6c757d; color: white; }
        .alert { padding: 15px; margin: 20px 0; border-radius: 4px; }
        .alert-success { background-color: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .alert-error { background-color: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .disabled { background-color: #f0f0f0 !important; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>✏️ Update Loan Account (Simple Version)</h2>
        <p>This is a simplified version for testing the Update Tab functionality.</p>

        <!-- Success/Error Messages -->
        <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
            <strong>Success!</strong> Account updated successfully.
        </asp:Panel>
        
        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-error">
            <strong>Error!</strong> <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:Panel>

        <!-- Load Account Section -->
        <div class="search-section">
            <h3>🔍 Load Account</h3>
            <div class="form-group">
                <label for="txtSearchReferenceNumber">Reference Number</label>
                <asp:TextBox ID="txtSearchReferenceNumber" runat="server" placeholder="e.g., LA-2025-0001"></asp:TextBox>
            </div>
            
            <div style="text-align: center; margin: 15px 0;"><strong>OR</strong></div>
            
            <div class="form-group">
                <label for="txtSearchAccountId">Account ID</label>
                <asp:TextBox ID="txtSearchAccountId" runat="server" placeholder="e.g., 1"></asp:TextBox>
            </div>
            
            <div>
                <asp:Button ID="btnLoadAccount" runat="server" Text="🔍 Load Account" CssClass="btn btn-info" OnClick="btnLoadAccount_Click" />
                <asp:Button ID="btnClearSearch" runat="server" Text="🗑️ Clear" CssClass="btn btn-secondary" OnClick="btnClearSearch_Click" />
            </div>
        </div>

        <!-- Hidden field for Account ID -->
        <asp:HiddenField ID="hfAccountId" runat="server" />

        <!-- Account Information Panel (Initially Hidden) -->
        <asp:Panel ID="pnlAccountInfo" runat="server" Visible="false">

            <!-- General Information Section -->
            <div class="editable-section">
                <h3>General Information (Customer Name and Long Name are editable)</h3>
                
                <div class="form-group">
                    <label>Reference Number (Cannot be changed)</label>
                    <asp:Label ID="lblReferenceNumber" runat="server" style="display: block; padding: 8px; background-color: #f0f0f0; border: 1px solid #ccc; border-radius: 4px; width: 300px;"></asp:Label>
                </div>

                <div class="form-group">
                    <label>Previous Reference Number (Read-only)</label>
                    <asp:TextBox ID="txtPreviousReferenceNumber" runat="server" Enabled="false" CssClass="disabled"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtCustomerName">Customer Name *</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtLongName">Long Name *</label>
                    <asp:TextBox ID="txtLongName" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>CRIB ID Number (Read-only)</label>
                    <asp:TextBox ID="txtCRIBIDNumber" runat="server" Enabled="false" CssClass="disabled"></asp:TextBox>
                </div>
            </div>

            <!-- Guarantee and Litigation Section -->
            <div class="editable-section">
                <h3>Guarantee and Litigation Information (Editable)</h3>
                
                <div class="form-group">
                    <asp:CheckBox ID="chkIsGuaranteed" runat="server" Text="Is Guaranteed" AutoPostBack="true" OnCheckedChanged="chkIsGuaranteed_CheckedChanged" />
                </div>

                <div class="form-group">
                    <label for="txtGuaranteedBy">Guaranteed By</label>
                    <asp:TextBox ID="txtGuaranteedBy" runat="server" placeholder="Enter guarantor name"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:CheckBox ID="chkIsUnderLitigation" runat="server" Text="Is Under Litigation" AutoPostBack="true" OnCheckedChanged="chkIsUnderLitigation_CheckedChanged" />
                </div>

                <div class="form-group">
                    <label for="txtLitigationDate">Litigation Date</label>
                    <asp:TextBox ID="txtLitigationDate" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <!-- Read-only Section (Sample) -->
            <div class="readonly-section">
                <h3>Other Information (Read-only)</h3>
                <div class="form-group">
                    <label>Center Code</label>
                    <asp:TextBox ID="txtCenterCode" runat="server" Enabled="false" CssClass="disabled"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Account Type</label>
                    <asp:TextBox ID="txtAccountType" runat="server" Enabled="false" CssClass="disabled"></asp:TextBox>
                </div>
            </div>

            <!-- Buttons -->
            <div style="margin-top: 30px;">
                <asp:Button ID="btnUpdate" runat="server" Text="💾 Update Account" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="❌ Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
            </div>

        </asp:Panel>

    </form>
</body>
</html>