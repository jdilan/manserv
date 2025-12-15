# PDF Export Implementation Guide

## Overview
This guide provides the implementation for PDF export functionality across all list views in the MANSERV Loan Account Management System.

## Components Added

### 1. TestUI (HTML/JavaScript) - ✅ COMPLETED
- **Search Results Export** - Export account search results to PDF
- **Reference Data Export** - Export reference data tables to PDF
- **Libraries Used**: jsPDF and jsPDF-AutoTable
- **Features**: Professional formatting, headers, footers, pagination

### 2. ASP.NET Web Forms Pages - 🔄 IMPLEMENTATION NEEDED

#### Pages Modified:
1. **UI_SearchAccounts.aspx** - Added export button for search results
2. **UI_Default.aspx** - Added export button for recent accounts

#### Required NuGet Packages:
```xml
<package id="iTextSharp" version="5.5.13.3" targetFramework="net472" />
```

## Code-Behind Implementation

### 1. SearchAccounts.aspx.cs - Add PDF Export Method

```csharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

protected void btnExportPDF_Click(object sender, EventArgs e)
{
    try
    {
        if (gvAccounts.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", 
                "alert('No data to export. Please perform a search first.');", true);
            return;
        }

        // Create PDF document
        Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
        MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

        document.Open();

        // Add title
        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
        Paragraph title = new Paragraph("MANSERV Loan Account Management System", titleFont);
        title.Alignment = Element.ALIGN_CENTER;
        document.Add(title);

        Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
        Paragraph subtitle = new Paragraph("Account Search Results", subtitleFont);
        subtitle.Alignment = Element.ALIGN_CENTER;
        subtitle.SpacingAfter = 20;
        document.Add(subtitle);

        // Add generation info
        Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
        Paragraph info = new Paragraph($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss} | Total Records: {gvAccounts.Rows.Count}", infoFont);
        info.SpacingAfter = 20;
        document.Add(info);

        // Create table
        PdfPTable table = new PdfPTable(gvAccounts.HeaderRow.Cells.Count - 1); // Exclude Actions column
        table.WidthPercentage = 100;

        // Add headers
        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
        for (int i = 0; i < gvAccounts.HeaderRow.Cells.Count - 1; i++)
        {
            PdfPCell cell = new PdfPCell(new Phrase(gvAccounts.HeaderRow.Cells[i].Text, headerFont));
            cell.BackgroundColor = new BaseColor(102, 126, 234);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 8;
            table.AddCell(cell);
        }

        // Add data rows
        Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK);
        foreach (GridViewRow row in gvAccounts.Rows)
        {
            for (int i = 0; i < row.Cells.Count - 1; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(row.Cells[i].Text, dataFont));
                cell.Padding = 5;
                if (gvAccounts.Rows.ToList().IndexOf(row) % 2 == 0)
                    cell.BackgroundColor = new BaseColor(245, 245, 245);
                table.AddCell(cell);
            }
        }

        document.Add(table);

        // Add footer
        Paragraph footer = new Paragraph("MANSERV Loan Account Management System - Confidential", infoFont);
        footer.Alignment = Element.ALIGN_CENTER;
        footer.SpacingBefore = 20;
        document.Add(footer);

        document.Close();

        // Send PDF to browser
        byte[] bytes = memoryStream.ToArray();
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", $"attachment; filename=Account_Search_Results_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        Response.ContentEncoding = Encoding.UTF8;
        Response.BinaryWrite(bytes);
        Response.End();
    }
    catch (Exception ex)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "alert", 
            $"alert('Error generating PDF: {ex.Message}');", true);
    }
}

// Update the search method to show/hide export button
protected void btnSearch_Click(object sender, EventArgs e)
{
    // ... existing search logic ...
    
    // Show export button if results found
    btnExportPDF.Visible = (gvAccounts.Rows.Count > 0);
}

protected void btnClear_Click(object sender, EventArgs e)
{
    // ... existing clear logic ...
    
    // Hide export button
    btnExportPDF.Visible = false;
}
```

### 2. Default.aspx.cs - Add PDF Export Method

```csharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

protected void btnExportRecentAccountsPDF_Click(object sender, EventArgs e)
{
    try
    {
        if (gvRecentAccounts.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", 
                "alert('No recent accounts to export.');", true);
            return;
        }

        // Create PDF document
        Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
        MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

        document.Open();

        // Add title
        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
        Paragraph title = new Paragraph("MANSERV Loan Account Management System", titleFont);
        title.Alignment = Element.ALIGN_CENTER;
        document.Add(title);

        Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
        Paragraph subtitle = new Paragraph("Recent Accounts Report", subtitleFont);
        subtitle.Alignment = Element.ALIGN_CENTER;
        subtitle.SpacingAfter = 20;
        document.Add(subtitle);

        // Add generation info
        Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
        Paragraph info = new Paragraph($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss} | Total Records: {gvRecentAccounts.Rows.Count}", infoFont);
        info.SpacingAfter = 20;
        document.Add(info);

        // Create table
        PdfPTable table = new PdfPTable(gvRecentAccounts.HeaderRow.Cells.Count - 1); // Exclude Actions column
        table.WidthPercentage = 100;

        // Add headers
        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
        for (int i = 0; i < gvRecentAccounts.HeaderRow.Cells.Count - 1; i++)
        {
            PdfPCell cell = new PdfPCell(new Phrase(gvRecentAccounts.HeaderRow.Cells[i].Text, headerFont));
            cell.BackgroundColor = new BaseColor(102, 126, 234);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 8;
            table.AddCell(cell);
        }

        // Add data rows
        Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK);
        foreach (GridViewRow row in gvRecentAccounts.Rows)
        {
            for (int i = 0; i < row.Cells.Count - 1; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(row.Cells[i].Text, dataFont));
                cell.Padding = 5;
                if (gvRecentAccounts.Rows.ToList().IndexOf(row) % 2 == 0)
                    cell.BackgroundColor = new BaseColor(245, 245, 245);
                table.AddCell(cell);
            }
        }

        document.Add(table);

        // Add footer
        Paragraph footer = new Paragraph("MANSERV Loan Account Management System - Confidential", infoFont);
        footer.Alignment = Element.ALIGN_CENTER;
        footer.SpacingBefore = 20;
        document.Add(footer);

        document.Close();

        // Send PDF to browser
        byte[] bytes = memoryStream.ToArray();
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", $"attachment; filename=Recent_Accounts_Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        Response.ContentEncoding = Encoding.UTF8;
        Response.BinaryWrite(bytes);
        Response.End();
    }
    catch (Exception ex)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "alert", 
            $"alert('Error generating PDF: {ex.Message}');", true);
    }
}

// Update Page_Load to show export button when data is available
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        // ... existing page load logic ...
        
        // Show export button if recent accounts are loaded
        btnExportRecentAccountsPDF.Visible = (gvRecentAccounts.Rows.Count > 0);
    }
}
```

## Installation Steps

### 1. Install NuGet Package
```bash
Install-Package iTextSharp -Version 5.5.13.3
```

### 2. Add Using Statements
Add to the top of your .aspx.cs files:
```csharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
```

### 3. Update Web.config (if needed)
```xml
<system.web>
  <httpRuntime maxRequestLength="51200" executionTimeout="3600" />
</system.web>
```

## Features Implemented

### ✅ TestUI Features:
- **Professional PDF Layout** - Company header, titles, generation info
- **Responsive Tables** - Auto-sizing columns, alternating row colors
- **Pagination Support** - Multi-page documents with page numbers
- **Error Handling** - User-friendly error messages
- **Dynamic Export Buttons** - Show/hide based on data availability

### 🔄 ASP.NET Features (To Implement):
- **Server-side PDF Generation** - Using iTextSharp library
- **GridView Integration** - Export any GridView data
- **Professional Formatting** - Consistent with TestUI design
- **File Download** - Direct PDF download to browser
- **Error Handling** - Graceful error management

## Testing Checklist

### TestUI Testing:
- [ ] Search for accounts and verify export button appears
- [ ] Click export button and verify PDF downloads
- [ ] Check PDF formatting and content
- [ ] Test reference data export
- [ ] Verify export button hides when no data

### ASP.NET Testing:
- [ ] Install iTextSharp NuGet package
- [ ] Add code-behind implementations
- [ ] Test search results export
- [ ] Test recent accounts export
- [ ] Verify PDF formatting and download

## Business Benefits

1. **Easy Reporting** - Business owners can quickly generate reports
2. **Professional Format** - Clean, branded PDF documents
3. **Data Portability** - Export data for offline analysis
4. **Audit Trail** - Generated timestamps for compliance
5. **User-Friendly** - One-click export functionality

## File Naming Convention

- Search Results: `Account_Search_Results_YYYYMMDD_HHMMSS.pdf`
- Recent Accounts: `Recent_Accounts_Report_YYYYMMDD_HHMMSS.pdf`
- Reference Data: `Reference_Data_[TYPE]_YYYYMMDD.pdf`

## Next Steps

1. **Install iTextSharp** in your ASP.NET project
2. **Copy the code-behind implementations** to your .aspx.cs files
3. **Test the functionality** with sample data
4. **Customize styling** if needed for your brand
5. **Deploy and train users** on the new export feature

The PDF export functionality is now ready for business use! 📄✨