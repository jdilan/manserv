/*
================================================================================
MANSERV Loan Account Management System - Local Testing
Reference Data Service Mock
================================================================================
Purpose: Mock implementation of IReferenceDataService for local testing
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using ManservLoanSystem.Models.Common;
using ManservLoanSystem.ExternalServices;

namespace ManservLoanSystem.LocalTesting.Mocks
{
    /// <summary>
    /// Mock implementation of reference data services
    /// Provides hardcoded dropdown values for testing
    /// </summary>
    public class ReferenceDataServiceMock : IReferenceDataService, IAccountTypeService, IEconomicActivityService, ICenterService
    {
        private Dictionary<string, List<ReferenceDataItem>> _referenceData;
        private List<AccountType> _accountTypes;
        private List<EconomicActivity> _economicActivities;
        private List<Center> _centers;

        public ReferenceDataServiceMock()
        {
            InitializeReferenceData();
            InitializeAccountTypes();
            InitializeEconomicActivities();
            InitializeCenters();
        }

        #region IReferenceDataService Implementation

        public ServiceResponse<List<ReferenceDataItem>> GetReferenceData(string groupNumber)
        {
            if (_referenceData.ContainsKey(groupNumber))
            {
                return ServiceResponse<List<ReferenceDataItem>>.Success(_referenceData[groupNumber]);
            }

            return ServiceResponse<List<ReferenceDataItem>>.Failure("REF_NOT_FOUND", $"Reference data group '{groupNumber}' not found");
        }

        public ServiceResponse<ReferenceDataItem> GetReferenceDataByCode(string groupNumber, string code)
        {
            if (_referenceData.ContainsKey(groupNumber))
            {
                var item = _referenceData[groupNumber].FirstOrDefault(x => x.Code == code);
                if (item != null)
                {
                    return ServiceResponse<ReferenceDataItem>.Success(item);
                }
            }

            return ServiceResponse<ReferenceDataItem>.Failure("REF_CODE_NOT_FOUND", $"Reference code '{code}' not found in group '{groupNumber}'");
        }

        public ServiceResponse<bool> ValidateReferenceCode(string code, string type)
        {
            if (_referenceData.ContainsKey(type))
            {
                var exists = _referenceData[type].Any(x => x.Code == code);
                return ServiceResponse<bool>.Success(exists);
            }

            return ServiceResponse<bool>.Failure("REF_TYPE_NOT_FOUND", $"Reference type '{type}' not found");
        }

        #endregion

        #region IAccountTypeService Implementation

        public ServiceResponse<List<AccountType>> GetAccountTypes()
        {
            return ServiceResponse<List<AccountType>>.Success(_accountTypes);
        }

        public ServiceResponse<AccountType> GetAccountType(string code)
        {
            var accountType = _accountTypes.FirstOrDefault(x => x.Code == code);
            if (accountType != null)
            {
                return ServiceResponse<AccountType>.Success(accountType);
            }

            return ServiceResponse<AccountType>.Failure("ACCT_TYPE_NOT_FOUND", $"Account type '{code}' not found");
        }

        public ServiceResponse<GLAccounts> GetGLMappings(string accountTypeCode, string economicActivityCode)
        {
            // Return dummy GL accounts for testing
            var glAccounts = new GLAccounts
            {
                PrincipalAccount = $"1010-{accountTypeCode}-{economicActivityCode}",
                InterestAccount = $"4010-{accountTypeCode}-{economicActivityCode}",
                PenaltyAccount = $"4020-{accountTypeCode}-{economicActivityCode}"
            };

            return ServiceResponse<GLAccounts>.Success(glAccounts);
        }

        #endregion

        #region IEconomicActivityService Implementation

        public ServiceResponse<List<EconomicActivity>> GetEconomicActivities()
        {
            return ServiceResponse<List<EconomicActivity>>.Success(_economicActivities);
        }

        public ServiceResponse<EconomicActivity> GetEconomicActivity(string code)
        {
            var activity = _economicActivities.FirstOrDefault(x => x.Code == code);
            if (activity != null)
            {
                return ServiceResponse<EconomicActivity>.Success(activity);
            }

            return ServiceResponse<EconomicActivity>.Failure("ECON_ACT_NOT_FOUND", $"Economic activity '{code}' not found");
        }

        #endregion

        #region ICenterService Implementation

        public ServiceResponse<List<Center>> GetCenters()
        {
            return ServiceResponse<List<Center>>.Success(_centers);
        }

        public ServiceResponse<Center> GetCenter(string code)
        {
            var center = _centers.FirstOrDefault(x => x.Code == code);
            if (center != null)
            {
                return ServiceResponse<Center>.Success(center);
            }

            return ServiceResponse<Center>.Failure("CENTER_NOT_FOUND", $"Center '{code}' not found");
        }

        public ServiceResponse<bool> ValidateCenter(string code)
        {
            var exists = _centers.Any(x => x.Code == code);
            return ServiceResponse<bool>.Success(exists);
        }

        #endregion

        #region Data Initialization

        private void InitializeReferenceData()
        {
            _referenceData = new Dictionary<string, List<ReferenceDataItem>>();

            // Corporation
            _referenceData["CORPORATION"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "RTL", Description = "Retail Banking" },
                new ReferenceDataItem { Code = "WBG", Description = "Wholesale Banking" },
                new ReferenceDataItem { Code = "FCDU", Description = "Foreign Currency Deposit Unit" }
            };

            // Book Code
            _referenceData["BOOKCODE"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "11", Description = "Regular Loans" },
                new ReferenceDataItem { Code = "20", Description = "Foreign Currency Loans" }
            };

            // Fund Source
            _referenceData["FUNDSOURCE"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "BSP", Description = "Bangko Sentral ng Pilipinas" },
                new ReferenceDataItem { Code = "LBP", Description = "Land Bank of the Philippines" },
                new ReferenceDataItem { Code = "DBP", Description = "Development Bank of the Philippines" },
                new ReferenceDataItem { Code = "WB", Description = "World Bank" },
                new ReferenceDataItem { Code = "ACPC", Description = "ACPC Funds" }
            };

            // Lending Program
            _referenceData["LENDINGPROGRAM"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "DBP", Description = "DBP Regular Program" },
                new ReferenceDataItem { Code = "ALF", Description = "Agricultural Lending Facility" },
                new ReferenceDataItem { Code = "CLF", Description = "Commercial Lending Facility" }
            };

            // Area
            _referenceData["AREA"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "PA", Description = "Performing Assets" },
                new ReferenceDataItem { Code = "NPA", Description = "Non-Performing Assets" }
            };

            // Maturity Code
            _referenceData["MATURITYCODE"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "A", Description = "Short Term (< 1 year)" },
                new ReferenceDataItem { Code = "B", Description = "Medium Term (1-5 years)" },
                new ReferenceDataItem { Code = "C", Description = "Long Term (5-10 years)" },
                new ReferenceDataItem { Code = "D", Description = "Very Long Term (10-20 years)" },
                new ReferenceDataItem { Code = "E", Description = "Ultra Long Term (> 20 years)" }
            };

            // Guaranteed By
            _referenceData["GUARANTEEDBY"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "SBGFC", Description = "Small Business Guarantee Finance Corporation" },
                new ReferenceDataItem { Code = "GFSME", Description = "Guarantee Fund for Small and Medium Enterprises" },
                new ReferenceDataItem { Code = "PHILGUARANTEE", Description = "Philippine Guarantee Corporation" }
            };

            // Currency
            _referenceData["CURRENCY"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "PHP", Description = "Philippine Peso" },
                new ReferenceDataItem { Code = "USD", Description = "US Dollar" },
                new ReferenceDataItem { Code = "EUR", Description = "Euro" },
                new ReferenceDataItem { Code = "JPY", Description = "Japanese Yen" }
            };

            // Budget Unit
            _referenceData["BUDGETUNIT"] = new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "001", Description = "Budget Unit 001" },
                new ReferenceDataItem { Code = "002", Description = "Budget Unit 002" },
                new ReferenceDataItem { Code = "003", Description = "Budget Unit 003" }
            };
        }

        private void InitializeAccountTypes()
        {
            _accountTypes = new List<AccountType>
            {
                new AccountType { Code = "IND", Description = "Industrial", RequiresPurpose = false },
                new AccountType { Code = "AA", Description = "Agri-Agra", RequiresPurpose = true },
                new AccountType { Code = "AI", Description = "Agri-Industrial", RequiresPurpose = true },
                new AccountType { Code = "R", Description = "Real Estate", RequiresPurpose = true },
                new AccountType { Code = "RDC", Description = "Real Estate Commercial", RequiresPurpose = true },
                new AccountType { Code = "RDE", Description = "Real Estate Residential", RequiresPurpose = true },
                new AccountType { Code = "RDH", Description = "Real Estate Housing", RequiresPurpose = true }
            };
        }

        private void InitializeEconomicActivities()
        {
            _economicActivities = new List<EconomicActivity>
            {
                new EconomicActivity { Code = "IND001", Description = "Manufacturing - General" },
                new EconomicActivity { Code = "AGR001", Description = "Agriculture - Crop Production" },
                new EconomicActivity { Code = "REL001", Description = "Real Estate Development" },
                new EconomicActivity { Code = "COM001", Description = "Commerce and Trading" },
                new EconomicActivity { Code = "SER001", Description = "Services - General" },
                new EconomicActivity { Code = "EXP001", Description = "Export Business" },
                new EconomicActivity { Code = "MAN001", Description = "Manufacturing - Large Scale" },
                new EconomicActivity { Code = "DEV001", Description = "Development Projects" }
            };
        }

        private void InitializeCenters()
        {
            _centers = new List<Center>
            {
                new Center { Code = "01", Name = "Head Office", Location = "Manila" },
                new Center { Code = "02", Name = "Branch A", Location = "Cebu" },
                new Center { Code = "03", Name = "Branch B", Location = "Davao" }
            };
        }

        #endregion
    }

    #region Supporting Classes

    public class ReferenceDataItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class AccountType
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool RequiresPurpose { get; set; }
    }

    public class GLAccounts
    {
        public string PrincipalAccount { get; set; }
        public string InterestAccount { get; set; }
        public string PenaltyAccount { get; set; }
    }

    public class EconomicActivity
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class Center
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }

    #endregion
}
