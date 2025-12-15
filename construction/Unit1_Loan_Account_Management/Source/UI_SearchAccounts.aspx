<%@ Page Title="Search Accounts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchAccounts.aspx.cs" Inherits="ManservLoanSystem.Web.Features.AccountOperations.SearchAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>🔍 Search Loan Accounts</h2>
    <p>Search for loan accounts using one or more criteria below.</p>

    <!-- Search Form -->
    <div style="border: 1px solid #ddd; padding: 20px; border-radius: 4px; margin-bottom: 20px; background-color: #f9f9f9;">
        <h3>Search Criteria</h3>
        
        <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 15px;">
            <div class="form-group">
                <label for="txtSearchRefNo">Reference Number</label>
                <asp:TextBox ID="txtSearchRefNo" runat="server" placeholder="e.g., LA-2025"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtSearchCustomerName">Customer Name</label>
                <asp:TextBox ID="txtSearchCustomerName" runat="server" placeholder="e.g., Juan"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ddlSearchCenterCode">Center Code</label>
                <asp:DropDownList ID="ddlSearchCenterCode" runat="server">
                    <asp:ListItem Value="">-- All Centers --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlSearchStatus">Status</label>
                <asp:DropDownList ID="ddlSearchStatus" runat="server">
                    <asp:ListItem Value="">-- All Statuses --</asp:ListItem>
                    <asp:ListItem Value="Active">Active</asp:ListItem>
                    <asp:ListItem Value="Closed">Closed</asp:ListItem>
                    <asp:ListItem Value="Archived">Archived</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlSearchAccountType">Account Type</label>
                <asp:DropDownList ID="ddlSearchAccountType" runat="server">
                    <asp:ListItem Value="">-- All Types --</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div style="margin-top: 20px;">
            <asp:Button ID="btnSearch" runat="server" Text="🔍 Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="🔄 Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
        </div>
    </div>

    <!-- Results -->
    <div>
        <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 15px;">
            <div>
                <h3>Search Results</h3>
                <asp:Label ID="lblResultCount" runat="server" Text="" style="font-weight: bold; color: #003366;"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnExportPDF" runat="server" Text="📄 Export to PDF" 
                    CssClass="btn btn-danger" OnClick="btnExportPDF_Click" 
                    Visible="false" style="background-color: #dc3545; border-color: #dc3545;" />
            </div>
        </div>
        
        <div class="grid-container">
            <asp:GridView ID="gvAccounts" runat="server" 
                AutoGenerateColumns="False" 
                EmptyDataText="No accounts found. Try different search criteria."
                OnRowCommand="gvAccounts_RowCommand"
                DataKeyNames="AccountId">
                <Columns>
                    <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference No" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                    <asp:BoundField DataField="AccountType" HeaderText="Type" />
                    <asp:BoundField DataField="CenterCode" HeaderText="Center" />
                    <asp:BoundField DataField="LoanStatus" HeaderText="Loan Status" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="Currency" HeaderText="Currency" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnView" runat="server" 
                                CommandName="ViewAccount" 
                                CommandArgument='<%# Eval("AccountId") %>'
                                Text="👁️ View" 
                                style="color: #003366; text-decoration: none; margin-right: 10px;">
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnEdit" runat="server" 
                                CommandName="EditAccount" 
                                CommandArgument='<%# Eval("AccountId") %>'
                                Text="✏️ Edit" 
                                style="color: #0059b3; text-decoration: none;">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
