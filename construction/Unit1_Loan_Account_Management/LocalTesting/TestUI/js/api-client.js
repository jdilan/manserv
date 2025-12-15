/*
================================================================================
MANSERV Local Test UI - API Client
================================================================================
Purpose: Handle all API calls to the local test API
Author: System Generated
Date: December 6, 2025
================================================================================
*/

// API Configuration
const API_BASE_URL = 'http://localhost:5000/api';
const DEFAULT_USER_ID = 'admin1';

// ============================================================================
// UTILITY FUNCTIONS
// ============================================================================

async function apiCall(endpoint, method = 'GET', body = null, userId = DEFAULT_USER_ID) {
    try {
        const url = new URL(`${API_BASE_URL}${endpoint}`);
        
        // Add userId as query parameter
        if (userId) {
            url.searchParams.append('userId', userId);
        }

        const options = {
            method,
            headers: {
                'Content-Type': 'application/json'
            }
        };

        if (body && (method === 'POST' || method === 'PUT')) {
            options.body = JSON.stringify(body);
        }

        const response = await fetch(url, options);
        const data = await response.json();

        displayResponse(response.status, data);

        return { status: response.status, data };
    } catch (error) {
        displayResponse(0, { error: error.message });
        return { status: 0, data: { error: error.message } };
    }
}

// ============================================================================
// TAB NAVIGATION
// ============================================================================

function showTab(tabName) {
    // Hide all tabs
    document.querySelectorAll('.tab-content').forEach(tab => {
        tab.classList.remove('active');
    });

    // Remove active class from all buttons
    document.querySelectorAll('.tab-button').forEach(btn => {
        btn.classList.remove('active');
    });

    // Show selected tab
    document.getElementById(`${tabName}-tab`).classList.add('active');

    // Add active class to clicked button
    event.target.classList.add('active');
}

// ============================================================================
// HEALTH CHECK
// ============================================================================

async function checkApiHealth() {
    const statusElement = document.getElementById('apiStatus');
    statusElement.innerHTML = '<span class="loading"></span> Checking...';

    try {
        const response = await fetch('http://localhost:5000/api/health');
        const data = await response.json();

        if (data.status === 'healthy') {
            statusElement.innerHTML = `✅ API Online | ${data.accountCount} accounts in database`;
            statusElement.style.color = '#4ec9b0';
        } else {
            statusElement.innerHTML = `⚠️ API Unhealthy: ${data.error}`;
            statusElement.style.color = '#f48771';
        }
    } catch (error) {
        statusElement.innerHTML = '❌ API Offline - Make sure the API server is running';
        statusElement.style.color = '#f48771';
    }
}

// ============================================================================
// SEARCH OPERATIONS
// ============================================================================

async function searchAccounts(event) {
    event.preventDefault();

    const refNo = document.getElementById('search_refNo').value;
    const customerName = document.getElementById('search_customerName').value;
    const centerCode = document.getElementById('search_centerCode').value;
    const status = document.getElementById('search_status').value;

    const params = new URLSearchParams();
    if (refNo) params.append('referenceNumber', refNo);
    if (customerName) params.append('customerName', customerName);
    if (centerCode) params.append('centerCode', centerCode);
    if (status) params.append('status', status);

    const result = await apiCall(`/accounts/search?${params.toString()}`);

    if (result.status === 200 && result.data.success) {
        displaySearchResults(result.data.data);
    }
}

async function getAllAccounts() {
    const result = await apiCall('/accounts');

    if (result.status === 200 && result.data.success) {
        displaySearchResults(result.data.data);
    }
}

function displaySearchResults(accounts) {
    const container = document.getElementById('searchResultsContent');
    const header = document.getElementById('searchResultsHeader');

    if (!accounts || accounts.length === 0) {
        container.innerHTML = '<p>No accounts found.</p>';
        header.style.display = 'none';
        return;
    }

    // Store data globally for PDF export
    window.currentSearchResults = accounts;

    let html = `<h3>Found ${accounts.length} account(s)</h3>`;
    html += '<table id="searchResultsTable"><thead><tr>';
    html += '<th>ID</th><th>Reference Number</th><th>Customer Name</th>';
    html += '<th>Center</th><th>Status</th><th>Actions</th>';
    html += '</tr></thead><tbody>';

    accounts.forEach(account => {
        html += `<tr>
            <td>${account.accountId}</td>
            <td>${account.referenceNumber}</td>
            <td>${account.customerName}</td>
            <td>${account.centerCode}</td>
            <td>${account.status}</td>
            <td>
                <button onclick="viewAccountById(${account.accountId})">View</button>
                <button onclick="loadAccountForUpdateById(${account.accountId})">Edit</button>
            </td>
        </tr>`;
    });

    html += '</tbody></table>';
    container.innerHTML = html;
    header.style.display = 'block';
}

function clearSearch() {
    document.getElementById('searchForm').reset();
    document.getElementById('searchResultsContent').innerHTML = '';
    document.getElementById('searchResultsHeader').style.display = 'none';
    window.currentSearchResults = null;
}

// ============================================================================
// CREATE OPERATION
// ============================================================================

async function createAccount(event) {
    event.preventDefault();

    const account = {
        referenceNumber: document.getElementById('create_refNo').value,
        previousReferenceNumber: document.getElementById('create_prevRefNo').value,
        cribidNumber: document.getElementById('create_cribId').value || null,
        customerName: document.getElementById('create_customerName').value,
        nidssAccountNumber: document.getElementById('create_nidss').value || null,
        longName: document.getElementById('create_longName').value,
        centerCode: document.getElementById('create_centerCode').value,
        budgetUnit: document.getElementById('create_budgetUnit').value,
        corporation: document.getElementById('create_corporation').value,
        bookCode: document.getElementById('create_bookCode').value,
        economicActivityCode: document.getElementById('create_economicActivity').value,
        originalReleaseDate: document.getElementById('create_originalReleaseDate').value,
        startOfTerm: document.getElementById('create_startOfTerm').value,
        maturityDate: document.getElementById('create_maturityDate').value,
        accountType: document.getElementById('create_accountType').value,
        purpose: document.getElementById('create_purpose').value || null,
        fundSource: document.getElementById('create_fundSource').value,
        lendingProgram: document.getElementById('create_lendingProgram').value,
        area: document.getElementById('create_area').value,
        isRestructured: document.getElementById('create_isRestructured').checked,
        typeOfCredit: document.getElementById('create_typeOfCredit').value,
        maturityCode: document.getElementById('create_maturityCode').value,
        purposeOfCredit: document.getElementById('create_purposeOfCredit').value,
        numberOfRecords: parseInt(document.getElementById('create_numberOfRecords').value) || null,
        isGuaranteed: document.getElementById('create_isGuaranteed').checked,
        guaranteedBy: document.getElementById('create_guaranteedBy').value || null,
        isUnderLitigation: document.getElementById('create_isUnderLitigation').checked,
        litigationDate: document.getElementById('create_litigationDate').value || null,
        loanStatus: document.getElementById('create_loanStatus').value,
        loanProjectType: document.getElementById('create_loanProjectType').value,
        currency: document.getElementById('create_currency').value,
        status: 'Active',
        isDraft: false
    };

    const result = await apiCall('/accounts', 'POST', account);

    if (result.status === 201 && result.data.success) {
        alert(`Account created successfully! Account ID: ${result.data.accountId}`);
        clearForm('createForm');
    } else {
        alert('Failed to create account. Check the response viewer for details.');
    }
}

function clearForm(formId) {
    document.getElementById(formId).reset();
}

// ============================================================================
// VIEW OPERATION
// ============================================================================

async function viewAccount() {
    const identifier = document.getElementById('view_identifier').value;

    if (!identifier) {
        alert('Please enter an Account ID or Reference Number');
        return;
    }

    // Check if it's a number (ID) or string (Reference Number)
    const isId = /^\d+$/.test(identifier);
    const endpoint = isId 
        ? `/accounts/${identifier}`
        : `/accounts/by-reference/${encodeURIComponent(identifier)}`;

    const result = await apiCall(endpoint);

    if (result.status === 200 && result.data.success) {
        displayAccountDetails(result.data.data);
    } else {
        document.getElementById('viewResults').innerHTML = '<p>Account not found.</p>';
    }
}

async function viewAccountById(accountId) {
    document.getElementById('view_identifier').value = accountId;
    showTab('view');
    await viewAccount();
}

function displayAccountDetails(account) {
    const container = document.getElementById('viewResults');

    let html = '<div class="account-card">';
    html += `<h4>Account Details - ${account.referenceNumber}</h4>`;

    const fields = [
        { label: 'Account ID', value: account.accountId },
        { label: 'Reference Number', value: account.referenceNumber },
        { label: 'Previous Reference Number', value: account.previousReferenceNumber },
        { label: 'Customer Name', value: account.customerName },
        { label: 'Long Name', value: account.longName },
        { label: 'CRIB ID', value: account.cribidNumber || 'N/A' },
        { label: 'NIDSS Account Number', value: account.nidssAccountNumber || 'N/A' },
        { label: 'Center Code', value: account.centerCode },
        { label: 'Budget Unit', value: account.budgetUnit },
        { label: 'Corporation', value: account.corporation },
        { label: 'Book Code', value: account.bookCode },
        { label: 'Economic Activity', value: account.economicActivityCode },
        { label: 'Original Release Date', value: account.originalReleaseDate },
        { label: 'Start of Term', value: account.startOfTerm },
        { label: 'Maturity Date', value: account.maturityDate },
        { label: 'Account Type', value: account.accountType },
        { label: 'Purpose', value: account.purpose || 'N/A' },
        { label: 'Fund Source', value: account.fundSource },
        { label: 'Lending Program', value: account.lendingProgram },
        { label: 'Area', value: account.area },
        { label: 'Is Restructured', value: account.isRestructured ? 'Yes' : 'No' },
        { label: 'Type of Credit', value: account.typeOfCredit },
        { label: 'Maturity Code', value: account.maturityCode },
        { label: 'Purpose of Credit', value: account.purposeOfCredit },
        { label: 'Number of Records', value: account.numberOfRecords || 'N/A' },
        { label: 'Is Guaranteed', value: account.isGuaranteed ? 'Yes' : 'No' },
        { label: 'Guaranteed By', value: account.guaranteedBy || 'N/A' },
        { label: 'Is Under Litigation', value: account.isUnderLitigation ? 'Yes' : 'No' },
        { label: 'Litigation Date', value: account.litigationDate || 'N/A' },
        { label: 'Loan Status', value: account.loanStatus },
        { label: 'Loan Project Type', value: account.loanProjectType },
        { label: 'Currency', value: account.currency },
        { label: 'Status', value: account.status },
        { label: 'Created By', value: account.createdBy },
        { label: 'Created Date', value: account.createdDate }
    ];

    fields.forEach(field => {
        html += `<div class="field">
            <div class="field-label">${field.label}:</div>
            <div class="field-value">${field.value}</div>
        </div>`;
    });

    html += '</div>';
    container.innerHTML = html;
}

// ============================================================================
// UPDATE OPERATION
// ============================================================================

let currentUpdateAccount = null;

async function loadAccountForUpdate() {
    // Clear any previous messages
    clearUpdateMessages();

    const referenceNumber = document.getElementById('update_searchRefNo').value.trim();
    const accountIdText = document.getElementById('update_searchAccountId').value.trim();

    if (!referenceNumber && !accountIdText) {
        showUpdateError('Please enter either a Reference Number or Account ID to search.');
        return;
    }

    let result;

    // Search by Account ID first if provided
    if (accountIdText) {
        const accountId = parseInt(accountIdText);
        if (isNaN(accountId)) {
            showUpdateError('Invalid Account ID format. Please enter a valid number.');
            return;
        }

        result = await apiCall(`/accounts/${accountId}`);
        if (result.status !== 200 || !result.data.success) {
            showUpdateError(`Account with ID ${accountId} not found.`);
            return;
        }
    }
    // Search by Reference Number
    else if (referenceNumber) {
        result = await apiCall(`/accounts/by-reference/${encodeURIComponent(referenceNumber)}`);
        if (result.status !== 200 || !result.data.success) {
            showUpdateError(`Account with Reference Number '${referenceNumber}' not found.`);
            return;
        }
    }

    if (result.data.success) {
        currentUpdateAccount = result.data.data;
        displayUpdateForm(currentUpdateAccount);
        
        // Clear search fields and show form
        document.getElementById('update_searchRefNo').value = '';
        document.getElementById('update_searchAccountId').value = '';
        document.getElementById('updateFormContainer').style.display = 'block';
    }
}

async function loadAccountForUpdateById(accountId) {
    document.getElementById('update_searchAccountId').value = accountId;
    showTab('update');
    await loadAccountForUpdate();
}

function displayUpdateForm(account) {
    // Populate read-only fields
    document.getElementById('update_refNo').value = account.referenceNumber || '';
    document.getElementById('update_prevRefNo').value = account.previousReferenceNumber || '';
    document.getElementById('update_cribId').value = account.cribidNumber || '';
    document.getElementById('update_nidss').value = account.nidssAccountNumber || '';
    document.getElementById('update_centerCode').value = account.centerCode || '';
    document.getElementById('update_budgetUnit').value = account.budgetUnit || '';
    document.getElementById('update_corporation').value = account.corporation || '';
    document.getElementById('update_bookCode').value = account.bookCode || '';
    document.getElementById('update_economicActivity').value = account.economicActivityCode || '';
    document.getElementById('update_originalReleaseDate').value = account.originalReleaseDate ? account.originalReleaseDate.split('T')[0] : '';
    document.getElementById('update_maturityDate').value = account.maturityDate ? account.maturityDate.split('T')[0] : '';
    document.getElementById('update_accountType').value = account.accountType || '';
    document.getElementById('update_fundSource').value = account.fundSource || '';
    document.getElementById('update_lendingProgram').value = account.lendingProgram || '';
    document.getElementById('update_currency').value = account.currency || '';

    // Populate editable fields
    document.getElementById('update_customerName').value = account.customerName || '';
    document.getElementById('update_longName').value = account.longName || '';
    document.getElementById('update_isGuaranteed').checked = account.isGuaranteed || false;
    document.getElementById('update_guaranteedBy').value = account.guaranteedBy || '';
    document.getElementById('update_isUnderLitigation').checked = account.isUnderLitigation || false;
    document.getElementById('update_litigationDate').value = account.litigationDate ? account.litigationDate.split('T')[0] : '';

    // Update dependent field states
    toggleGuaranteedBy('update');
    toggleLitigationDate('update');
}

async function updateAccount() {
    if (!currentUpdateAccount) {
        showUpdateError('Please load an account first before updating.');
        return;
    }

    // Validate required fields
    const customerName = document.getElementById('update_customerName').value.trim();
    const longName = document.getElementById('update_longName').value.trim();

    if (!customerName || !longName) {
        showUpdateError('Customer Name and Long Name are required.');
        return;
    }

    // Prepare update data - only include editable fields
    const updateData = {
        accountId: currentUpdateAccount.accountId,
        referenceNumber: currentUpdateAccount.referenceNumber,
        previousReferenceNumber: currentUpdateAccount.previousReferenceNumber,
        
        // Editable fields
        customerName: customerName,
        longName: longName,
        isGuaranteed: document.getElementById('update_isGuaranteed').checked,
        guaranteedBy: document.getElementById('update_isGuaranteed').checked ? 
            document.getElementById('update_guaranteedBy').value.trim() || null : null,
        isUnderLitigation: document.getElementById('update_isUnderLitigation').checked,
        litigationDate: document.getElementById('update_isUnderLitigation').checked && 
            document.getElementById('update_litigationDate').value ? 
            document.getElementById('update_litigationDate').value : null,

        // Preserve all other fields from current account
        cribidNumber: currentUpdateAccount.cribidNumber,
        nidssAccountNumber: currentUpdateAccount.nidssAccountNumber,
        centerCode: currentUpdateAccount.centerCode,
        budgetUnit: currentUpdateAccount.budgetUnit,
        corporation: currentUpdateAccount.corporation,
        bookCode: currentUpdateAccount.bookCode,
        economicActivityCode: currentUpdateAccount.economicActivityCode,
        originalReleaseDate: currentUpdateAccount.originalReleaseDate,
        startOfTerm: currentUpdateAccount.startOfTerm,
        maturityDate: currentUpdateAccount.maturityDate,
        accountType: currentUpdateAccount.accountType,
        purpose: currentUpdateAccount.purpose,
        fundSource: currentUpdateAccount.fundSource,
        lendingProgram: currentUpdateAccount.lendingProgram,
        area: currentUpdateAccount.area,
        isRestructured: currentUpdateAccount.isRestructured,
        typeOfCredit: currentUpdateAccount.typeOfCredit,
        maturityCode: currentUpdateAccount.maturityCode,
        purposeOfCredit: currentUpdateAccount.purposeOfCredit,
        numberOfRecords: currentUpdateAccount.numberOfRecords,
        loanStatus: currentUpdateAccount.loanStatus,
        loanProjectType: currentUpdateAccount.loanProjectType,
        currency: currentUpdateAccount.currency,
        status: currentUpdateAccount.status,
        isDraft: currentUpdateAccount.isDraft
    };

    const result = await apiCall(`/accounts/${currentUpdateAccount.accountId}`, 'PUT', updateData);

    if (result.status === 200 && result.data.success) {
        showUpdateSuccess('Account updated successfully!');
        // Reload the account to show updated data
        currentUpdateAccount = result.data.data || updateData;
        displayUpdateForm(currentUpdateAccount);
    } else {
        showUpdateError('Failed to update account. Check the response viewer for details.');
    }
}

function cancelUpdate() {
    if (currentUpdateAccount) {
        // In a real application, this might redirect to ViewAccount
        showUpdateError(`Cancel clicked - would redirect to ViewAccount.aspx?id=${currentUpdateAccount.accountId}`);
    } else {
        // In a real application, this might redirect to Default page
        showUpdateError('Cancel clicked - would redirect to Default.aspx');
    }
}

function clearUpdateSearch() {
    document.getElementById('update_searchRefNo').value = '';
    document.getElementById('update_searchAccountId').value = '';
    document.getElementById('updateFormContainer').style.display = 'none';
    currentUpdateAccount = null;
    clearUpdateMessages();
}

function showUpdateSuccess(message) {
    // Remove any existing messages
    clearUpdateMessages();
    
    // Create success message
    const successDiv = document.createElement('div');
    successDiv.id = 'updateSuccessMessage';
    successDiv.style.cssText = 'background: #d4edda; color: #155724; border: 1px solid #c3e6cb; padding: 15px; margin: 20px 0; border-radius: 4px;';
    successDiv.innerHTML = `<strong>Success!</strong> ${message}`;
    
    // Insert after the search section
    const searchSection = document.querySelector('#update-tab fieldset');
    searchSection.parentNode.insertBefore(successDiv, searchSection.nextSibling);
}

function showUpdateError(message) {
    // Remove any existing messages
    clearUpdateMessages();
    
    // Create error message
    const errorDiv = document.createElement('div');
    errorDiv.id = 'updateErrorMessage';
    errorDiv.style.cssText = 'background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; padding: 15px; margin: 20px 0; border-radius: 4px;';
    errorDiv.innerHTML = `<strong>Error!</strong> ${message}`;
    
    // Insert after the search section
    const searchSection = document.querySelector('#update-tab fieldset');
    searchSection.parentNode.insertBefore(errorDiv, searchSection.nextSibling);
}

function clearUpdateMessages() {
    const successMsg = document.getElementById('updateSuccessMessage');
    const errorMsg = document.getElementById('updateErrorMessage');
    if (successMsg) successMsg.remove();
    if (errorMsg) errorMsg.remove();
}

// ============================================================================
// DELETE OPERATION
// ============================================================================

async function deleteAccount() {
    const accountId = document.getElementById('delete_accountId').value;

    if (!accountId) {
        alert('Please enter an Account ID');
        return;
    }

    if (!confirm(`Are you sure you want to delete account ID ${accountId}?`)) {
        return;
    }

    const result = await apiCall(`/accounts/${accountId}`, 'DELETE');

    const container = document.getElementById('deleteResults');
    if (result.status === 200 && result.data.success) {
        container.innerHTML = '<p style="color: green;">✅ Account deleted successfully</p>';
    } else {
        container.innerHTML = '<p style="color: red;">❌ Failed to delete account</p>';
    }
}

// ============================================================================
// REFERENCE DATA
// ============================================================================

async function loadReferenceData() {
    const dataType = document.getElementById('refDataType').value;

    if (!dataType) return;

    let endpoint;
    if (dataType === 'account-types' || dataType === 'economic-activities' || 
        dataType === 'centers' || dataType === 'customers') {
        endpoint = `/reference/${dataType}`;
    } else {
        endpoint = `/reference/${dataType}`;
    }

    const result = await apiCall(endpoint);

    if (result.status === 200 && result.data.success) {
        displayReferenceData(result.data.data, dataType);
    }
}

function displayReferenceData(data, dataType) {
    const container = document.getElementById('refDataResultsContent');
    const header = document.getElementById('refDataResultsHeader');

    if (!data || data.length === 0) {
        container.innerHTML = '<p>No data found.</p>';
        header.style.display = 'none';
        return;
    }

    // Store data globally for PDF export
    window.currentReferenceData = { data: data, type: dataType };

    let html = `<h3>${dataType}</h3>`;
    html += '<table id="refDataTable"><thead><tr>';

    // Determine columns based on data type
    const firstItem = data[0];
    const keys = Object.keys(firstItem);

    keys.forEach(key => {
        html += `<th>${key}</th>`;
    });

    html += '</tr></thead><tbody>';

    data.forEach(item => {
        html += '<tr>';
        keys.forEach(key => {
            html += `<td>${item[key] !== null && item[key] !== undefined ? item[key] : 'N/A'}</td>`;
        });
        html += '</tr>';
    });

    html += '</tbody></table>';
    container.innerHTML = html;
    header.style.display = 'block';
}

// ============================================================================
// DROPDOWN POPULATION
// ============================================================================

async function loadDropdowns() {
    // Load all reference data for dropdowns
    await populateDropdown('create_centerCode', '/reference/centers', 'code', 'name');
    await populateDropdown('create_budgetUnit', '/reference/BUDGETUNIT', 'code', 'description');
    await populateDropdown('create_corporation', '/reference/CORPORATION', 'code', 'description');
    await populateDropdown('create_bookCode', '/reference/BOOKCODE', 'code', 'description');
    await populateDropdown('create_economicActivity', '/reference/economic-activities', 'code', 'description');
    await populateDropdown('create_accountType', '/reference/account-types', 'code', 'description');
    await populateDropdown('create_fundSource', '/reference/FUNDSOURCE', 'code', 'description');
    await populateDropdown('create_lendingProgram', '/reference/LENDINGPROGRAM', 'code', 'description');
    await populateDropdown('create_area', '/reference/AREA', 'code', 'description');
    await populateDropdown('create_maturityCode', '/reference/MATURITYCODE', 'code', 'description');
    await populateDropdown('create_guaranteedBy', '/reference/GUARANTEEDBY', 'code', 'description');
    await populateDropdown('create_currency', '/reference/CURRENCY', 'code', 'description');
}

async function populateDropdown(elementId, endpoint, valueKey, textKey) {
    const result = await fetch(`${API_BASE_URL}${endpoint}`);
    const response = await result.json();

    if (response.success && response.data) {
        const select = document.getElementById(elementId);
        select.innerHTML = '<option value="">-- Select --</option>';

        response.data.forEach(item => {
            const option = document.createElement('option');
            option.value = item[valueKey];
            option.textContent = `${item[valueKey]} - ${item[textKey]}`;
            select.appendChild(option);
        });
    }
}

// ============================================================================
// CONDITIONAL FIELD TOGGLES
// ============================================================================

function togglePurposeField(prefix) {
    const accountType = document.getElementById(`${prefix}_accountType`).value;
    const purposeField = document.getElementById(`${prefix}_purpose`);
    const requiresPurpose = ['AA', 'AI', 'R', 'RDC', 'RDE', 'RDH'].includes(accountType);
    
    purposeField.required = requiresPurpose;
    purposeField.disabled = !requiresPurpose;
}

function toggleGuaranteedBy(prefix) {
    const isGuaranteed = document.getElementById(`${prefix}_isGuaranteed`).checked;
    const guaranteedByField = document.getElementById(`${prefix}_guaranteedBy`);
    
    guaranteedByField.disabled = !isGuaranteed;
    guaranteedByField.required = isGuaranteed;
}

function toggleLitigationDate(prefix) {
    const isUnderLitigation = document.getElementById(`${prefix}_isUnderLitigation`).checked;
    const litigationDateField = document.getElementById(`${prefix}_litigationDate`);
    
    litigationDateField.disabled = !isUnderLitigation;
    litigationDateField.required = isUnderLitigation;
}

// ============================================================================
// PDF EXPORT FUNCTIONS
// ============================================================================

function exportSearchResultsToPDF() {
    if (!window.currentSearchResults || window.currentSearchResults.length === 0) {
        alert('No search results to export');
        return;
    }

    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    // Add title
    doc.setFontSize(20);
    doc.text('MANSERV Loan Account Management System', 20, 20);
    
    doc.setFontSize(16);
    doc.text('Account Search Results', 20, 35);
    
    doc.setFontSize(12);
    doc.text(`Generated on: ${new Date().toLocaleString()}`, 20, 45);
    doc.text(`Total Records: ${window.currentSearchResults.length}`, 20, 55);

    // Prepare table data
    const tableColumns = [
        'Account ID',
        'Reference Number', 
        'Customer Name',
        'Long Name',
        'Center Code',
        'Status',
        'Created Date'
    ];

    const tableRows = window.currentSearchResults.map(account => [
        account.accountId || 'N/A',
        account.referenceNumber || 'N/A',
        account.customerName || 'N/A',
        account.longName || 'N/A',
        account.centerCode || 'N/A',
        account.status || 'N/A',
        account.createdDate ? new Date(account.createdDate).toLocaleDateString() : 'N/A'
    ]);

    // Add table
    doc.autoTable({
        head: [tableColumns],
        body: tableRows,
        startY: 65,
        styles: {
            fontSize: 8,
            cellPadding: 3
        },
        headStyles: {
            fillColor: [102, 126, 234],
            textColor: 255,
            fontStyle: 'bold'
        },
        alternateRowStyles: {
            fillColor: [245, 245, 245]
        },
        margin: { top: 65, left: 10, right: 10 }
    });

    // Add footer
    const pageCount = doc.internal.getNumberOfPages();
    for (let i = 1; i <= pageCount; i++) {
        doc.setPage(i);
        doc.setFontSize(10);
        doc.text(`Page ${i} of ${pageCount}`, doc.internal.pageSize.width - 30, doc.internal.pageSize.height - 10);
        doc.text('MANSERV Loan Account Management System', 20, doc.internal.pageSize.height - 10);
    }

    // Save the PDF
    const fileName = `Account_Search_Results_${new Date().toISOString().split('T')[0]}.pdf`;
    doc.save(fileName);
}

function exportReferenceDataToPDF() {
    if (!window.currentReferenceData || !window.currentReferenceData.data || window.currentReferenceData.data.length === 0) {
        alert('No reference data to export');
        return;
    }

    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();
    const data = window.currentReferenceData.data;
    const dataType = window.currentReferenceData.type;

    // Add title
    doc.setFontSize(20);
    doc.text('MANSERV Loan Account Management System', 20, 20);
    
    doc.setFontSize(16);
    doc.text(`Reference Data: ${dataType}`, 20, 35);
    
    doc.setFontSize(12);
    doc.text(`Generated on: ${new Date().toLocaleString()}`, 20, 45);
    doc.text(`Total Records: ${data.length}`, 20, 55);

    // Prepare table data
    const firstItem = data[0];
    const tableColumns = Object.keys(firstItem).map(key => 
        key.charAt(0).toUpperCase() + key.slice(1).replace(/([A-Z])/g, ' $1')
    );

    const tableRows = data.map(item => 
        Object.values(item).map(value => 
            value !== null && value !== undefined ? String(value) : 'N/A'
        )
    );

    // Add table
    doc.autoTable({
        head: [tableColumns],
        body: tableRows,
        startY: 65,
        styles: {
            fontSize: 8,
            cellPadding: 3
        },
        headStyles: {
            fillColor: [102, 126, 234],
            textColor: 255,
            fontStyle: 'bold'
        },
        alternateRowStyles: {
            fillColor: [245, 245, 245]
        },
        margin: { top: 65, left: 10, right: 10 }
    });

    // Add footer
    const pageCount = doc.internal.getNumberOfPages();
    for (let i = 1; i <= pageCount; i++) {
        doc.setPage(i);
        doc.setFontSize(10);
        doc.text(`Page ${i} of ${pageCount}`, doc.internal.pageSize.width - 30, doc.internal.pageSize.height - 10);
        doc.text('MANSERV Loan Account Management System', 20, doc.internal.pageSize.height - 10);
    }

    // Save the PDF
    const fileName = `Reference_Data_${dataType.replace(/\s+/g, '_')}_${new Date().toISOString().split('T')[0]}.pdf`;
    doc.save(fileName);
}
