/*
================================================================================
MANSERV Loan Account Management System
Stub Service: ReferenceDataServiceStub
================================================================================
Purpose: Mock implementation of reference data service for demo purposes
Note: This is a stub - replace with actual service when Unit 4 is available
Author: System Generated
Date: December 5, 2025
================================================================================
*/

using System.Collections.Generic;

namespace ManservLoanSystem.ExternalServices
{
    /// <summary>
    /// Reference data item
    /// </summary>
    public class ReferenceDataItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string GroupNumber { get; set; }
    }

    /// <summary>
    /// Stub implementation of reference data service
    /// Returns hardcoded dropdown values for demo
    /// </summary>
    public class ReferenceDataServiceStub
    {
        /// <summary>
        /// Get reference data by group number
        /// </summary>
        public List<ReferenceDataItem> GetReferenceData(string groupNumber)
        {
            var data = new List<ReferenceDataItem>();

            switch (groupNumber)
            {
                case "0100": // Corporation
                    data.Add(new ReferenceDataItem { Code = "RTL", Description = "Retail", GroupNumber = "0100" });
                    data.Add(new ReferenceDataItem { Code = "FCDU", Description = "Foreign Currency", GroupNumber = "0100" });
                    data.Add(new ReferenceDataItem { Code = "FCDW", Description = "Foreign Currency Wholesale", GroupNumber = "0100" });
                    data.Add(new ReferenceDataItem { Code = "WBG", Description = "Wholesale", GroupNumber = "0100" });
                    break;

                case "0110": // Book Code
                    data.Add(new ReferenceDataItem { Code = "11", Description = "PESO REGULAR ACCOUNTS", GroupNumber = "0110" });
                    data.Add(new ReferenceDataItem { Code = "12", Description = "FOREIGN REGULAR ACCOUNTS", GroupNumber = "0110" });
                    data.Add(new ReferenceDataItem { Code = "20", Description = "FCDU ACCOUNTS", GroupNumber = "0110" });
                    data.Add(new ReferenceDataItem { Code = "30", Description = "FOREIGN OFFICE ACCOUNTS", GroupNumber = "0110" });
                    break;

                case "0024": // Fund Source
                    data.Add(new ReferenceDataItem { Code = "BSP", Description = "BSP/CB", GroupNumber = "0024" });
                    data.Add(new ReferenceDataItem { Code = "DBP", Description = "DBP", GroupNumber = "0024" });
                    data.Add(new ReferenceDataItem { Code = "LBP", Description = "LBP", GroupNumber = "0024" });
                    data.Add(new ReferenceDataItem { Code = "WB", Description = "World Bank", GroupNumber = "0024" });
                    data.Add(new ReferenceDataItem { Code = "ACPC", Description = "ACPC", GroupNumber = "0024" });
                    break;

                case "0025": // Lending Program
                    data.Add(new ReferenceDataItem { Code = "DBP", Description = "DBP Internal Financing", GroupNumber = "0025" });
                    data.Add(new ReferenceDataItem { Code = "ALF", Description = "ALF", GroupNumber = "0025" });
                    data.Add(new ReferenceDataItem { Code = "CLF", Description = "CLF", GroupNumber = "0025" });
                    break;

                case "0014": // Area
                    data.Add(new ReferenceDataItem { Code = "PA", Description = "Performing", GroupNumber = "0014" });
                    data.Add(new ReferenceDataItem { Code = "NPA", Description = "Non-Performing", GroupNumber = "0014" });
                    break;

                case "0022": // Maturity Code
                    data.Add(new ReferenceDataItem { Code = "A", Description = "Demand", GroupNumber = "0022" });
                    data.Add(new ReferenceDataItem { Code = "B", Description = "Short-term", GroupNumber = "0022" });
                    data.Add(new ReferenceDataItem { Code = "C", Description = "Intermediate", GroupNumber = "0022" });
                    data.Add(new ReferenceDataItem { Code = "D", Description = "Medium-term", GroupNumber = "0022" });
                    data.Add(new ReferenceDataItem { Code = "E", Description = "Long-term", GroupNumber = "0022" });
                    break;

                case "0090": // Currency
                    data.Add(new ReferenceDataItem { Code = "PHP", Description = "Philippine Peso", GroupNumber = "0090" });
                    data.Add(new ReferenceDataItem { Code = "USD", Description = "US Dollar", GroupNumber = "0090" });
                    data.Add(new ReferenceDataItem { Code = "JPY", Description = "Japanese Yen", GroupNumber = "0090" });
                    data.Add(new ReferenceDataItem { Code = "EUR", Description = "Euro", GroupNumber = "0090" });
                    break;

                case "0019": // Guaranteed By
                    data.Add(new ReferenceDataItem { Code = "SBGFC", Description = "SBGFC", GroupNumber = "0019" });
                    data.Add(new ReferenceDataItem { Code = "GFSME", Description = "GFSME", GroupNumber = "0019" });
                    data.Add(new ReferenceDataItem { Code = "PHILGUARANTEE", Description = "PHILGUARANTEE", GroupNumber = "0019" });
                    break;
            }

            return data;
        }

        /// <summary>
        /// Get account types
        /// </summary>
        public List<ReferenceDataItem> GetAccountTypes()
        {
            return new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "IND", Description = "Industrial" },
                new ReferenceDataItem { Code = "AA", Description = "Agricultural - Agri" },
                new ReferenceDataItem { Code = "AI", Description = "Agricultural - Industrial" },
                new ReferenceDataItem { Code = "R", Description = "Real Estate" },
                new ReferenceDataItem { Code = "REL", Description = "Real Estate Loan" },
                new ReferenceDataItem { Code = "RDC", Description = "Real Estate - Commercial" },
                new ReferenceDataItem { Code = "RDE", Description = "Real Estate - Residential" },
                new ReferenceDataItem { Code = "RDH", Description = "Real Estate - Housing" }
            };
        }

        /// <summary>
        /// Get economic activities
        /// </summary>
        public List<ReferenceDataItem> GetEconomicActivities()
        {
            return new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "IND001", Description = "Manufacturing" },
                new ReferenceDataItem { Code = "AGR001", Description = "Agriculture" },
                new ReferenceDataItem { Code = "REL001", Description = "Real Estate" },
                new ReferenceDataItem { Code = "COM001", Description = "Commerce/Trading" },
                new ReferenceDataItem { Code = "SER001", Description = "Services" },
                new ReferenceDataItem { Code = "EXP001", Description = "Export" },
                new ReferenceDataItem { Code = "DEV001", Description = "Development" },
                new ReferenceDataItem { Code = "MAN001", Description = "Manufacturing - Heavy" }
            };
        }

        /// <summary>
        /// Get centers
        /// </summary>
        public List<ReferenceDataItem> GetCenters()
        {
            return new List<ReferenceDataItem>
            {
                new ReferenceDataItem { Code = "01", Description = "Center 01 - Manila" },
                new ReferenceDataItem { Code = "02", Description = "Center 02 - Cebu" },
                new ReferenceDataItem { Code = "03", Description = "Center 03 - Davao" }
            };
        }
    }
}
