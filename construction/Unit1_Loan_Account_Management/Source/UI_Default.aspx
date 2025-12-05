<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ManservLoanSystem.Web._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div style="text-align: center; padding: 40px 0;">
        <h1 style="color: #003366; font-size: 36px; margin-bottom: 10px;">🏦 Welcome to MANSERV</h1>
        <h2 style="color: #666; font-size: 24px; font-weight: normal;">Loan Account Management System</h2>
        <p style="color: #888; font-size: 16px; margin-top: 20px;">Unit 1: Loan Account Management</p>
    </div>

    <!-- Quick Stats -->
    <h3>📊 Quick Statistics</h3>
    <div style="display: grid; grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); gap: 20px; margin-bottom: 40px;">
        
        <div style="border: 2px solid #003366; padding: 30px; border-radius: 8px; text-align: center; background-color: #f0f8ff;">
            <div style="font-size: 48px; font-weight: bold; color: #003366;">
                <asp:Label ID="lblTotalAccounts" runat="server" Text="0"></asp:Label>
            </div>
            <div style="font-size: 18px; color: #666; margin-top: 10px;">Total Accounts</div>
        </div>

        <div style="border: 2px solid #28a745; padding: 30px; border-radius: 8px; text-align: center; background-color: #f0fff4;">
            <div style="font-size: 48px; font-weight: bold; color: #28a745;">
                <asp:Label ID="lblActiveAccounts" runat="server" Text="0"></asp:Label>
            </div>
            <div style="font-size: 18px; color: #666; margin-top: 10px;">Active Accounts</div>
        </div>

        <div style="border: 2px solid #ffc107; padding: 30px; border-radius: 8px; text-align: center; background-color: #fffef0;">
            <div style="font-size: 48px; font-weight: bold; color: #ffc107;">
                <asp:Label ID="lblPastDueAccounts" runat="server" Text="0"></asp:Label>
            </div>
            <div style="font-size: 18px; color: #666; margin-top: 10px;">Past Due</div>
        </div>

        <div style="border: 2px solid #6c757d; padding: 30px; border-radius: 8px; text-align: center; background-color: #f8f9fa;">
            <div style="font-size: 48px; font-weight: bold; color: #6c757d;">
                <asp:Label ID="lblClosedAccounts" runat="server" Text="0"></asp:Label>
            </div>
            <div style="font-size: 18px; color: #666; margin-top: 10px;">Closed Accounts</div>
        </div>

    </div>

    <!-- Quick Actions -->
    <h3>⚡ Quick Actions</h3>
    <div style="display: grid; grid-template-columns: repeat(auto-fit, minmax(300px, 1fr)); gap: 20px; margin-bottom: 40px;">
        
        <a href="~/Features/GeneralAccountManagement/CreateAccount.aspx" runat="server" style="text-decoration: none;">
            <div style="border: 1px solid #ddd; padding: 30px; border-radius: 8px; background-color: white; transition: all 0.3s; cursor: pointer;"
                 onmouseover="this.style.boxShadow='0 4px 8px rgba(0,0,0,0.1)'; this.style.transform='translateY(-2px)';"
                 onmouseout="this.style.boxShadow='none'; this.style.transform='translateY(0)';">
                <div style="font-size: 48px; margin-bottom: 15px;">➕</div>
                <div style="font-size: 20px; font-weight: bold; color: #003366; margin-bottom: 10px;">Create New Account</div>
                <div style="color: #666;">Set up a new loan account with customer information</div>
            </div>
        </a>

        <a href="~/Features/AccountOperations/SearchAccounts.aspx" runat="server" style="text-decoration: none;">
            <div style="border: 1px solid #ddd; padding: 30px; border-radius: 8px; background-color: white; transition: all 0.3s; cursor: pointer;"
                 onmouseover="this.style.boxShadow='0 4px 8px rgba(0,0,0,0.1)'; this.style.transform='translateY(-2px)';"
                 onmouseout="this.style.boxShadow='none'; this.style.transform='translateY(0)';">
                <div style="font-size: 48px; margin-bottom: 15px;">🔍</div>
                <div style="font-size: 20px; font-weight: bold; color: #003366; margin-bottom: 10px;">Search Accounts</div>
                <div style="color: #666;">Find and manage existing loan accounts</div>
            </div>
        </a>

        <a href="~/Features/GeneralAccountManagement/ViewAccount.aspx" runat="server" style="text-decoration: none;">
            <div style="border: 1px solid #ddd; padding: 30px; border-radius: 8px; background-color: white; transition: all 0.3s; cursor: pointer;"
                 onmouseover="this.style.boxShadow='0 4px 8px rgba(0,0,0,0.1)'; this.style.transform='translateY(-2px)';"
                 onmouseout="this.style.boxShadow='none'; this.style.transform='translateY(0)';">
                <div style="font-size: 48px; margin-bottom: 15px;">👁️</div>
                <div style="font-size: 20px; font-weight: bold; color: #003366; margin-bottom: 10px;">View Account</div>
                <div style="color: #666;">View detailed account information</div>
            </div>
        </a>

    </div>

    <!-- Recent Accounts -->
    <h3>📋 Recent Accounts</h3>
    <div class="grid-container">
        <asp:GridView ID="gvRecentAccounts" runat="server" 
            AutoGenerateColumns="False" 
            EmptyDataText="No accounts found.">
            <Columns>
                <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference No" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                <asp:BoundField DataField="AccountType" HeaderText="Type" />
                <asp:BoundField DataField="LoanStatus" HeaderText="Loan Status" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkView" runat="server" 
                            NavigateUrl='<%# "~/Features/GeneralAccountManagement/ViewAccount.aspx?id=" + Eval("AccountId") %>'
                            Text="View" 
                            style="color: #003366; text-decoration: none;">
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- System Information -->
    <div style="margin-top: 40px; padding: 20px; background-color: #f9f9f9; border-radius: 8px;">
        <h3>ℹ️ System Information</h3>
        <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px;">
            <div>
                <strong>Version:</strong> 1.0<br />
                <strong>Framework:</strong> ASP.NET 4.7 Web Forms<br />
                <strong>ORM:</strong> Entity Framework 6.x
            </div>
            <div>
                <strong>Database:</strong> SQL Server 2022<br />
                <strong>Architecture:</strong> Feature-Based with Service-Oriented patterns<br />
                <strong>Status:</strong> <span style="color: green; font-weight: bold;">✓ Operational</span>
            </div>
        </div>
    </div>

</asp:Content>
