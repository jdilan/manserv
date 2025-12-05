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
    const container = document.getElementById('searchResults');

    if (!accounts || accounts.length === 0) {
        container.innerHTML = '<p>No accounts found.</p>';
        return;
    }

    let html = `<h3>Found ${accounts.length} account(s)</h3>`;
    html += '<table><thead><tr>';
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
}

function clearSearch() {
    document.getElementById('searchForm').reset();
    document.getElementById('searchResults').innerHTML = '';
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

async function loadAccountForUpdate() {
    const accountId = document.getElementById('update_accountId').value;

    if (!accountId) {
        alert('Please enter an Account ID');
        return;
    }

    const result = await apiCall(`/accounts/${accountId}`);

    if (result.status === 200 && result.data.success) {
        displayUpdateForm(result.data.data);
    } else {
        alert('Account not found');
    }
}

async function loadAccountForUpdateById(accountId) {
    document.getElementById('update_accountId').value = accountId;
    showTab('update');
    await loadAccountForUpdate();
}

function displayUpdateForm(account) {
    // Implementation similar to create form but pre-populated
    // For brevity, showing simplified version
    const container = document.getElementById('updateFormContainer');
    container.innerHTML = `
        <p>Update form for Account ID: ${account.accountId}</p>
        <p>Reference Number: ${account.referenceNumber}</p>
        <p><em>Full update form implementation would go here...</em></p>
        <button onclick="alert('Update functionality - to be implemented')">Update Account</button>
    `;
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
    const container = document.getElementById('refDataResults');

    if (!data || data.length === 0) {
        container.innerHTML = '<p>No data found.</p>';
        return;
    }

    let html = `<h3>${dataType}</h3>`;
    html += '<table><thead><tr>';

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
