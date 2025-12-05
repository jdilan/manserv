/*
================================================================================
MANSERV Local Test UI - Sample Data
================================================================================
Purpose: Provide sample data for quick testing
Author: System Generated
Date: December 6, 2025
================================================================================
*/

function loadSampleData(formPrefix) {
    const today = new Date().toISOString().split('T')[0];
    const futureDate = new Date();
    futureDate.setFullYear(futureDate.getFullYear() + 5);
    const maturityDate = futureDate.toISOString().split('T')[0];

    // Generate random reference number
    const randomNum = Math.floor(Math.random() * 9999) + 1;
    const refNo = `LA-2025-${String(randomNum).padStart(4, '0')}`;

    // Sample data
    const sampleData = {
        refNo: refNo,
        prevRefNo: `LA-2024-${String(randomNum).padStart(4, '0')}`,
        cribId: `${Math.floor(Math.random() * 10000000000)}`,
        customerName: 'Test Customer ' + randomNum,
        nidss: `${Math.floor(Math.random() * 10000000000000)}`,
        longName: 'Test Customer ' + randomNum + ' Corporation',
        centerCode: '01',
        budgetUnit: '001',
        corporation: 'RTL',
        bookCode: '11',
        economicActivity: 'IND001',
        originalReleaseDate: today,
        startOfTerm: today,
        maturityDate: maturityDate,
        accountType: 'IND',
        purpose: '',
        fundSource: 'BSP',
        lendingProgram: 'DBP',
        area: 'PA',
        isRestructured: false,
        typeOfCredit: 'CUR',
        maturityCode: 'B',
        purposeOfCredit: 'P',
        numberOfRecords: 1,
        isGuaranteed: false,
        guaranteedBy: '',
        isUnderLitigation: false,
        litigationDate: '',
        loanStatus: 'CUR',
        loanProjectType: 'C',
        currency: 'PHP'
    };

    // Populate form fields
    document.getElementById(`${formPrefix}_refNo`).value = sampleData.refNo;
    document.getElementById(`${formPrefix}_prevRefNo`).value = sampleData.prevRefNo;
    document.getElementById(`${formPrefix}_cribId`).value = sampleData.cribId;
    document.getElementById(`${formPrefix}_customerName`).value = sampleData.customerName;
    document.getElementById(`${formPrefix}_nidss`).value = sampleData.nidss;
    document.getElementById(`${formPrefix}_longName`).value = sampleData.longName;
    document.getElementById(`${formPrefix}_centerCode`).value = sampleData.centerCode;
    document.getElementById(`${formPrefix}_budgetUnit`).value = sampleData.budgetUnit;
    document.getElementById(`${formPrefix}_corporation`).value = sampleData.corporation;
    document.getElementById(`${formPrefix}_bookCode`).value = sampleData.bookCode;
    document.getElementById(`${formPrefix}_economicActivity`).value = sampleData.economicActivity;
    document.getElementById(`${formPrefix}_originalReleaseDate`).value = sampleData.originalReleaseDate;
    document.getElementById(`${formPrefix}_startOfTerm`).value = sampleData.startOfTerm;
    document.getElementById(`${formPrefix}_maturityDate`).value = sampleData.maturityDate;
    document.getElementById(`${formPrefix}_accountType`).value = sampleData.accountType;
    document.getElementById(`${formPrefix}_purpose`).value = sampleData.purpose;
    document.getElementById(`${formPrefix}_fundSource`).value = sampleData.fundSource;
    document.getElementById(`${formPrefix}_lendingProgram`).value = sampleData.lendingProgram;
    document.getElementById(`${formPrefix}_area`).value = sampleData.area;
    document.getElementById(`${formPrefix}_isRestructured`).checked = sampleData.isRestructured;
    document.getElementById(`${formPrefix}_typeOfCredit`).value = sampleData.typeOfCredit;
    document.getElementById(`${formPrefix}_maturityCode`).value = sampleData.maturityCode;
    document.getElementById(`${formPrefix}_purposeOfCredit`).value = sampleData.purposeOfCredit;
    document.getElementById(`${formPrefix}_numberOfRecords`).value = sampleData.numberOfRecords;
    document.getElementById(`${formPrefix}_isGuaranteed`).checked = sampleData.isGuaranteed;
    document.getElementById(`${formPrefix}_guaranteedBy`).value = sampleData.guaranteedBy;
    document.getElementById(`${formPrefix}_isUnderLitigation`).checked = sampleData.isUnderLitigation;
    document.getElementById(`${formPrefix}_litigationDate`).value = sampleData.litigationDate;
    document.getElementById(`${formPrefix}_loanStatus`).value = sampleData.loanStatus;
    document.getElementById(`${formPrefix}_loanProjectType`).value = sampleData.loanProjectType;
    document.getElementById(`${formPrefix}_currency`).value = sampleData.currency;

    alert('Sample data loaded! You can now submit the form.');
}

// Additional sample data scenarios
const sampleScenarios = {
    agricultural: {
        accountType: 'AA',
        purpose: 'A',
        economicActivity: 'AGR001',
        fundSource: 'LBP',
        lendingProgram: 'ALF',
        isGuaranteed: true,
        guaranteedBy: 'SBGFC'
    },
    
    realEstate: {
        accountType: 'R',
        purpose: 'H',
        economicActivity: 'REL001',
        fundSource: 'DBP',
        lendingProgram: 'CLF',
        maturityCode: 'D'
    },
    
    foreignCurrency: {
        currency: 'USD',
        bookCode: '20',
        corporation: 'FCDU',
        economicActivity: 'EXP001'
    },
    
    pastDue: {
        loanStatus: 'PDO',
        area: 'NPA',
        isRestructured: true
    },
    
    underLitigation: {
        isUnderLitigation: true,
        litigationDate: new Date().toISOString().split('T')[0],
        typeOfCredit: 'LITIG',
        area: 'NPA'
    }
};

function loadScenario(scenarioName) {
    const scenario = sampleScenarios[scenarioName];
    if (!scenario) {
        alert('Scenario not found');
        return;
    }

    // First load base sample data
    loadSampleData('create');

    // Then override with scenario-specific data
    Object.keys(scenario).forEach(key => {
        const elementId = `create_${key}`;
        const element = document.getElementById(elementId);
        
        if (element) {
            if (element.type === 'checkbox') {
                element.checked = scenario[key];
            } else {
                element.value = scenario[key];
            }
        }
    });

    alert(`${scenarioName} scenario loaded!`);
}
