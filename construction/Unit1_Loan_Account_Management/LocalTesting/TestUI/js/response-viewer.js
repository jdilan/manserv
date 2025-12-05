/*
================================================================================
MANSERV Local Test UI - Response Viewer
================================================================================
Purpose: Display and format API responses
Author: System Generated
Date: December 6, 2025
================================================================================
*/

function displayResponse(status, data) {
    const statusElement = document.getElementById('responseStatus');
    const bodyElement = document.getElementById('responseBody');

    // Format status
    if (status >= 200 && status < 300) {
        statusElement.textContent = `✅ ${status} - Success`;
        statusElement.className = 'success';
    } else if (status === 0) {
        statusElement.textContent = `❌ Network Error`;
        statusElement.className = 'error';
    } else {
        statusElement.textContent = `❌ ${status} - Error`;
        statusElement.className = 'error';
    }

    // Format body
    bodyElement.textContent = JSON.stringify(data, null, 2);

    // Syntax highlighting (simple)
    highlightJSON(bodyElement);
}

function highlightJSON(element) {
    let json = element.textContent;
    
    // Simple syntax highlighting
    json = json.replace(/"([^"]+)":/g, '<span style="color: #9cdcfe;">"$1"</span>:');
    json = json.replace(/: "([^"]*)"/g, ': <span style="color: #ce9178;">"$1"</span>');
    json = json.replace(/: (\d+)/g, ': <span style="color: #b5cea8;">$1</span>');
    json = json.replace(/: (true|false|null)/g, ': <span style="color: #569cd6;">$1</span>');
    
    element.innerHTML = json;
}

function copyResponse() {
    const bodyElement = document.getElementById('responseBody');
    const text = bodyElement.textContent;

    navigator.clipboard.writeText(text).then(() => {
        alert('Response copied to clipboard!');
    }).catch(err => {
        alert('Failed to copy: ' + err);
    });
}

function clearResponse() {
    document.getElementById('responseStatus').textContent = '';
    document.getElementById('responseBody').textContent = '';
}

// Auto-scroll to response viewer when response is displayed
function scrollToResponse() {
    const responseViewer = document.getElementById('responseViewer');
    responseViewer.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}
