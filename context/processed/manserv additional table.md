# Manserv Additional Database Tables

This document describes the database table structures used in the Manserv loan account management system.

## Table of Contents
1. [RLSACCT.DBF - Account Types](#rlsacctdbf---account-types)
2. [RLSECON.DBF - Economic Activity Codes](#rlsecondbf---economic-activity-codes)
3. [GLMACCT.DBF - General Ledger Master Accounts](#glmacctdbf---general-ledger-master-accounts)
4. [RLSCUST.DBF - Customer Information](#rlscustdbf---customer-information)
5. [MNSHTRAN.DBF - Transaction History](#mnshtrandbf---transaction-history)
6. [MNSHBRR.DBF - Borrower Rating](#mnshbrrdbf---borrower-rating)
7. [RLSCTR.DBF - Center/Budget Unit](#rlsctrdbf---centerbudget-unit)

---

## RLSACCT.DBF - Account Types

This table contains account type definitions and their associated GL account mappings.

| Field | Field Name | Type | Width | Dec | Index | Collate | Nulls |
|-------|------------|------|-------|-----|-------|---------|-------|
| 1 | ACT_CODE | Character | 3 | | Asc | Machine | No |
| 2 | ACT_DESC | Character | 40 | | | | No |
| 3 | ACT_SNAME | Character | 20 | | | | No |
| 4 | ACT_LC | Character | 1 | | | | No |
| 5 | ACT_LCSUB | Character | 4 | | | | No |
| 6 | ACT_ADV | Character | 10 | | | | No |
| 7 | ACT_LTADV | Character | 10 | | | | No |
| 8 | ACT_LTEXP | Character | 10 | | | | No |
| 9 | ACT_PRSUB | Character | 4 | | | | No |
| 10 | ACT_UPSUB | Character | 4 | | | | No |
| 11 | ACT_PDASUB | Character | 4 | | | | No |
| 12 | ACT_DAACT | Character | 10 | | | | No |
| 13 | ACT_RESACT | Character | 10 | | | | No |
| 14 | ACT_CRCUR | Character | 6 | | | | No |
| 15 | ACT_CRPDO | Character | 6 | | | | No |
| 16 | ACT_CRLIT | Character | 6 | | | | No |
| 17 | ACT_RFSCR | Character | 6 | | | | No |
| 18 | ACT_RFSPD | Character | 6 | | | | No |
| 19 | ACT_RFSLT | Character | 6 | | | | No |
| 20 | ACT_RUPCR | Character | 6 | | | | No |
| 21 | ACT_RUPPD | Character | 6 | | | | No |
| 22 | ACT_RUPLT | Character | 6 | | | | No |
| 23 | ACT_RCURP | Character | 6 | | | | No |
| 24 | ACT_RCURNP | Character | 6 | | | | No |
| 25 | ACT_RPDNP | Character | 6 | | | | No |
| 26 | ACT_RLITG | Character | 6 | | | | No |
| 27 | ACT_DCSUB | Character | 4 | | | | No |
| 28 | ACT_R12SUB | Character | 4 | | | | No |
| 29 | ACT_CFSUB | Character | 4 | | | | No |
| 30 | ACT_BAHF | Character | 6 | | | | No |
| 31 | ACT_EAHF | Character | 6 | | | | No |
| 32 | ACT_BFISH | Character | 6 | | | | No |
| 33 | ACT_EFISH | Character | 6 | | | | No |
| 34 | ACT_BMQ | Character | 6 | | | | No |
| 35 | ACT_EMQ | Character | 6 | | | | No |
| 36 | ACT_BMANU | Character | 6 | | | | No |
| 37 | ACT_EMANU | Character | 6 | | | | No |
| 38 | ACT_BEGWS | Character | 6 | | | | No |
| 39 | ACT_EEGWS | Character | 6 | | | | No |
| 40 | ACT_BCONST | Character | 6 | | | | No |
| 41 | ACT_ECONST | Character | 6 | | | | No |
| 42 | ACT_BWLS | Character | 6 | | | | No |
| 43 | ACT_EWLS | Character | 6 | | | | No |
| 44 | ACT_BHRM | Character | 6 | | | | No |
| 45 | ACT_EHRM | Character | 6 | | | | No |
| 46 | ACT_BTSC | Character | 6 | | | | No |
| 47 | ACT_ETSC | Character | 6 | | | | No |
| 48 | ACT_BFINAN | Character | 6 | | | | No |
| 49 | ACT_EFINAN | Character | 6 | | | | No |
| 50 | ACT_BRERBA | Character | 6 | | | | No |
| 51 | ACT_ERERBA | Character | 6 | | | | No |
| 52 | ACT_BPADCS | Character | 6 | | | | No |
| 53 | ACT_EPADCS | Character | 6 | | | | No |
| 54 | ACT_BEDUC | Character | 6 | | | | No |
| 55 | ACT_EEDUC | Character | 6 | | | | No |
| 56 | ACT_BHSW | Character | 6 | | | | No |
| 57 | ACT_EHSW | Character | 6 | | | | No |
| 58 | ACT_BOCSPS | Character | 6 | | | | No |
| 59 | ACT_EOCSPS | Character | 6 | | | | No |
| 60 | ACT_BUPAPH | Character | 6 | | | | No |
| 61 | ACT_EUPAPH | Character | 6 | | | | No |
| 62 | ACT_BETOB | Character | 6 | | | | No |
| 63 | ACT_EETOB | Character | 6 | | | | No |

### Key Fields
- **ACT_CODE**: Primary key - Account type code (3 characters)
- **ACT_DESC**: Account type description (40 characters)
- **ACT_SNAME**: Short name for account type (20 characters)
- Fields 30-63: Economic sector-specific GL account mappings (B = Beginning, E = Ending)
  - AHF: Agriculture, Hunting, and Forestry
  - FISH: Fishing
  - MQ: Mining and Quarrying
  - MANU: Manufacturing
  - EGWS: Electricity, Gas, and Water Supply
  - CONST: Construction
  - WLS: Wholesale and Retail Trade
  - HRM: Hotels, Restaurants, and Motels
  - TSC: Transport, Storage, and Communication
  - FINAN: Financial Intermediation
  - RERBA: Real Estate, Renting, and Business Activities
  - PADCS: Public Administration, Defense, and Compulsory Social Security
  - EDUC: Education
  - HSW: Health and Social Work
  - OCSPS: Other Community, Social, and Personal Services
  - UPAPH: Activities of Private Households
  - ETOB: Extra-Territorial Organizations and Bodies

---

## RLSECON.DBF - Economic Activity Codes

This table contains economic activity classifications for loan purposes.

| Field | Field Name | Type | Width | Dec | Index | Collate | Nulls |
|-------|------------|------|-------|-----|-------|---------|-------|
| 1 | ECO_OLD | Character | 6 | | | | No |
| 2 | ECO_CODE | Character | 6 | | Asc | Machine | No |
| 3 | ECO_DESC | Character | 40 | | | | No |
| 4 | ECO_GRP | Character | 6 | | | | No |
| 5 | ECO_PURP | Character | 1 | | | | No |

### Key Fields
- **ECO_CODE**: Primary key - Economic activity code (6 characters)
- **ECO_DESC**: Description of economic activity (40 characters)
- **ECO_GRP**: Economic activity group code (6 characters)
- **ECO_PURP**: Purpose indicator (1 character)
- **ECO_OLD**: Legacy/old economic activity code (6 characters)

---

## GLMACCT.DBF - General Ledger Master Accounts

This table contains the chart of accounts for the general ledger system.

| Field | Field Name | Type | Width | Dec | Index | Collate | Nulls |
|-------|------------|------|-------|-----|-------|---------|-------|
| 1 | CACCTNO | Character | 8 | | | | No |
| 2 | ACCTDESC | Character | 40 | | | | No |
| 3 | GLTYPE | Character | 1 | | | | No |
| 4 | GLBAL | Character | 1 | | | | No |
| 5 | CACCSL | Character | 1 | | | | No |
| 6 | MASTKEY | Character | 18 | | | | No |
| 7 | ALIAS | Character | 9 | | | | No |
| 8 | WARDACCT | Character | 18 | | | | No |

### Key Fields
- **CACCTNO**: GL account number (8 characters)
- **ACCTDESC**: Account description (40 characters)
- **GLTYPE**: GL account type (1 character)
- **GLBAL**: Balance indicator (1 character)
- **ALIAS**: Account alias (9 characters)
- **MASTKEY**: Master key reference (18 characters)
- **WARDACCT**: Ward/subsidiary account (18 characters)

---

## RLSCUST.DBF - Customer Information

This table stores comprehensive customer/borrower information.

| Field | Field Name | Type | Width | Dec | Index | Collate | Nulls |
|-------|------------|------|-------|-----|-------|---------|-------|
| 1 | CUS_BNO | Character | 10 | | Asc | Machine | No |
| 2 | CUS_NAME | Character | 30 | | | | No |
| 3 | CUS_DATE | Date | 8 | | | | No |
| 4 | CUS_BADDR1 | Character | 40 | | | | No |
| 5 | CUS_BADDR2 | Character | 40 | | | | No |
| 6 | CUS_PADDR1 | Character | 40 | | | | No |
| 7 | CUS_PADDR2 | Character | 40 | | | | No |
| 8 | CUS_MAIL | Numeric | 1 | | | | No |
| 9 | CUS_LOC | Character | 4 | | | | No |
| 10 | CUS_ACLASS | Character | 4 | | | | No |
| 11 | CUS_APRAMT | Numeric | 15 | 2 | | | No |
| 12 | CUS_APRDAT | Date | 8 | | | | No |
| 13 | CUS_SIZE | Character | 1 | | | | No |
| 14 | CUS_TIN | Character | 17 | | | | No |
| 15 | CUS_SEX | Numeric | 1 | | | | No |
| 16 | CUS_OWNR | Character | 3 | | | | No |
| 17 | CUS_NATION | Character | 1 | | | | No |
| 18 | CUS_AFFIL | Character | 30 | | | | No |
| 19 | CUS_DOSRI | Character | 3 | | | | No |
| 20 | CUS_APRNO | Numeric | 3 | | | | No |
| 21 | TOTASSET | Numeric | 15 | 2 | | | No |
| 22 | TOTLIABS | Numeric | 15 | 2 | | | No |
| 23 | STCKHLEQ | Numeric | 15 | 2 | | | No |
| 24 | GROSSREV | Numeric | 15 | 2 | | | No |
| 25 | TOTEXPNS | Numeric | 15 | 2 | | | No |
| 26 | INTEXPNS | Numeric | 15 | 2 | | | No |
| 27 | NETINCOM | Numeric | 15 | 2 | | | No |
| 28 | FINSDTE | Numeric | 8 | | | | No |
| 29 | CUS_RDO | Character | 3 | | | | No |
| 30 | CUS_VAT | Character | 1 | | | | No |
| 31 | CUS_BMBE | Character | 1 | | | | No |
| 32 | CUS_SULONG | Character | 1 | | | | No |
| 33 | CUS_ATAG | Character | 4 | | | | No |
| 34 | CUS_OFW | Character | 1 | | | | No |
| 35 | CUS_OFWN | Numeric | 3 | | | | No |
| 36 | LONGNAME | Character | 100 | | | | No |
| 37 | CRIBIDNO | Character | 10 | | | | No |
| 38 | CUS_BRR | Character | 3 | | | | No |
| 39 | CUS_BRRWDT | Date | 8 | | | | No |
| 40 | CUS_BORR | Character | 3 | | | | No |
| 41 | CUS_GRPNM | Character | 10 | | | | No |

### Key Fields
- **CUS_BNO**: Primary key - Customer/Borrower number (10 characters)
- **CUS_NAME**: Customer name (30 characters)
- **LONGNAME**: Long name (100 characters)
- **CRIBIDNO**: CRIB ID number (10 characters)
- **CUS_TIN**: Tax Identification Number (17 characters)
- **CUS_DOSRI**: DOSRI classification (3 characters)
- **CUS_SIZE**: Size of firm (1 character)
- **CUS_OFW**: OFW indicator (1 character)
- **CUS_OFWN**: Number of OFWs (numeric, 3 digits)

### Financial Information Fields
- **TOTASSET**: Total assets (15 digits, 2 decimals)
- **TOTLIABS**: Total liabilities (15 digits, 2 decimals)
- **STCKHLEQ**: Stockholders' equity (15 digits, 2 decimals)
- **GROSSREV**: Gross revenue (15 digits, 2 decimals)
- **TOTEXPNS**: Total expenses (15 digits, 2 decimals)
- **INTEXPNS**: Interest expenses (15 digits, 2 decimals)
- **NETINCOM**: Net income (15 digits, 2 decimals)

---

## MNSHTRAN.DBF - Transaction History

This table stores detailed transaction history for loan accounts.

| Field | Field Name | Type | Width | Dec | Index | Collate | Nulls |
|-------|------------|------|-------|-----|-------|---------|-------|
| 1 | HIS_CRNO | Character | 17 | | Asc | Machine | No |
| 2 | HIS_TDATE | Date | 8 | | | | No |
| 3 | HIS_VDATE | Date | 8 | | | | No |
| 4 | HIS_CODE | Character | 4 | | | | No |
| 5 | HIS_SEQNO | Character | 3 | | | | No |
| 6 | HIS_AMT | Character | 15 | | | | No |
| 7 | HIS_OSP | Numeric | 15 | 2 | | | No |
| 8 | HIS_OSPD | Numeric | 15 | 2 | | | No |
| 9 | HIS_OSPO | Numeric | 15 | 2 | | | No |
| 10 | HIS_UPRIN | Numeric | 15 | 2 | | | No |
| 11 | HIS_UPRIND | Numeric | 15 | 2 | | | No |
| 12 | HIS_UPRINO | Numeric | 15 | 2 | | | No |
| 13 | HIS_RESV | Numeric | 15 | 2 | | | No |
| 14 | HIS_RESVD | Numeric | 15 | 2 | | | No |
| 15 | HIS_RESVO | Numeric | 15 | 2 | | | No |
| 16 | HIS_SETD | Date | 8 | | | | No |
| 17 | HIS_USERID | Character | 10 | | | | No |
| 18 | HIS_AUTHID | Character | 10 | | | | No |
| 19 | HIS_PARTIC | Character | 20 | | | | No |
| 20 | HIS_DESCDR | Character | 8 | | | | No |
| 21 | HIS_DESCCR | Character | 8 | | | | No |
| 22 | HIS_GLIND | Character | 9 | | | | No |
| 23 | HIS_DCGF | Numeric | 15 | 2 | | | No |
| 24 | HIS_DCGFD | Numeric | 15 | 2 | | | No |
| 25 | HIS_DCGFO | Numeric | 15 | 2 | | | No |
| 26 | HIS_GFAMT | Numeric | 12 | 2 | | | No |
| 27 | HIS_GFAMTD | Numeric | 15 | 2 | | | No |
| 28 | HIS_GFAMTO | Numeric | 15 | 2 | | | No |
| 29 | HIS_DCLE | Numeric | 15 | 2 | | | No |
| 30 | HIS_DCLED | Numeric | 15 | 2 | | | No |
| 31 | HIS_DCLEO | Numeric | 15 | 2 | | | No |
| 32 | HIS_LTR | Numeric | 15 | 2 | | | No |
| 33 | HIS_LTRD | Numeric | 15 | 2 | | | No |
| 34 | HIS_LTRO | Numeric | 15 | 2 | | | No |
| 35 | HIS_DCADV | Numeric | 15 | 2 | | | No |
| 36 | HIS_DCADVD | Numeric | 15 | 2 | | | No |
| 37 | HIS_DCADVO | Numeric | 15 | 2 | | | No |
| 38 | HIS_ADVR | Numeric | 15 | 2 | | | No |
| 39 | HIS_ADVRD | Numeric | 15 | 2 | | | No |
| 40 | HIS_ADVRO | Numeric | 15 | 2 | | | No |
| 41 | HIS_DCPDRI | Numeric | 15 | 2 | | | No |
| 42 | HIS_DCPDRD | Numeric | 15 | 2 | | | No |
| 43 | HIS_DCPDRO | Numeric | 15 | 2 | | | No |
| 44 | HIS_DCPDPR | Numeric | 15 | 2 | | | No |
| 45 | HIS_DCPDPD | Numeric | 15 | 2 | | | No |
| 46 | HIS_DCPDPO | Numeric | 15 | 2 | | | No |
| 47 | HIS_AIAMT | Numeric | 15 | 2 | | | No |
| 48 | HIS_AIAMTD | Numeric | 15 | 2 | | | No |
| 49 | HIS_AIAMTO | Numeric | 15 | 2 | | | No |
| 50 | HIS_RI2AMT | Numeric | 15 | 2 | | | No |
| 51 | HIS_RI2AMD | Numeric | 15 | 2 | | | No |
| 52 | HIS_RI2AMO | Numeric | 15 | 2 | | | No |
| 53 | HIS_RI1AMT | Numeric | 15 | 2 | | | No |
| 54 | HIS_RI1AMD | Numeric | 15 | 2 | | | No |
| 55 | HIS_RI1AMO | Numeric | 15 | 2 | | | No |
| 56 | HIS_PRINAM | Numeric | 15 | 2 | | | No |
| 57 | HIS_PRINAD | Numeric | 15 | 2 | | | No |
| 58 | HIS_PRINAO | Numeric | 15 | 2 | | | No |
| 59 | HIS_URIAMT | Numeric | 15 | 2 | | | No |
| 60 | HIS_URIAMD | Numeric | 15 | 2 | | | No |
| 61 | HIS_URIAMO | Numeric | 15 | 2 | | | No |
| 62 | HIS_AIR | Numeric | 15 | 2 | | | No |
| 63 | HIS_AIRD | Numeric | 15 | 2 | | | No |
| 64 | HIS_AIRO | Numeric | 15 | 2 | | | No |
| 65 | HIS_O2D | Numeric | 14 | 10 | | | No |
| 66 | HIS_D2P | Numeric | 14 | 10 | | | No |
| 67 | HIS_RVDR | Character | 9 | | | | No |
| 68 | HIS_RVCR | Character | 9 | | | | No |
| 69 | HIS_PRMODE | Character | 1 | | | | No |
| 70 | HIS_RAIR | Numeric | 15 | 2 | | | No |
| 71 | HIS_RAIRD | Numeric | 15 | 2 | | | No |
| 72 | HIS_RAIRO | Numeric | 15 | 2 | | | No |

### Key Fields
- **HIS_CRNO**: Primary key - Credit/Reference number (17 characters)
- **HIS_TDATE**: Transaction date
- **HIS_VDATE**: Value date
- **HIS_CODE**: Transaction code (4 characters)
- **HIS_SEQNO**: Sequence number (3 characters)
- **HIS_USERID**: User ID who created the transaction (10 characters)
- **HIS_AUTHID**: Authorizer ID (10 characters)

### Balance Fields (Suffix meanings)
- Base field: Peso amount
- Field + D: Dollar amount
- Field + O: Other currency amount

Examples:
- **HIS_OSP**: Outstanding principal (Peso)
- **HIS_OSPD**: Outstanding principal (Dollar)
- **HIS_OSPO**: Outstanding principal (Other currency)

---

## MNSHBRR.DBF - Borrower Rating

This table stores borrower risk ratings and their validity periods.

| Field | Field Name | Type | Width | Dec | Index | Collate | Nulls |
|-------|------------|------|-------|-----|-------|---------|-------|
| 1 | BRR_BNO | Character | 17 | | Asc | Machine | No |
| 2 | BRR_CODE | Character | 3 | | | | No |
| 3 | BRR_STRDT | Date | 8 | | | | No |
| 4 | BRR_ENDDT | Date | 8 | | | | No |
| 5 | BRR_TRAN | Date | 8 | | | | No |
| 6 | BRR_SG | Character | 3 | | | | No |

### Key Fields
- **BRR_BNO**: Primary key - Borrower number (17 characters)
- **BRR_CODE**: Borrower risk rating code (3 characters)
- **BRR_STRDT**: Start date of rating validity
- **BRR_ENDDT**: End date of rating validity
- **BRR_TRAN**: Transaction date
- **BRR_SG**: Sub-group code (3 characters)

---

## RLSCTR.DBF - Center/Budget Unit

This table contains organizational center and budget unit information.

| Field | Field Name | Type | Width | Dec | Index | Collate | Nulls |
|-------|------------|------|-------|-----|-------|---------|-------|
| 1 | CTR_CENTER | Character | 2 | | Asc | Machine | No |
| 2 | CTR_BUDGET | Character | 3 | | | | No |
| 3 | CTR_DESC | Character | 20 | | | | No |
| 4 | CTR_DIV | Character | 1 | | | | No |

### Key Fields
- **CTR_CENTER**: Primary key - Center code (2 characters)
- **CTR_BUDGET**: Budget unit code (3 characters)
- **CTR_DESC**: Center/budget unit description (20 characters)
- **CTR_DIV**: Division code (1 character)

---

## Notes

1. All tables use DBF (dBase) format
2. Character fields are fixed-width
3. Numeric fields with decimals are used for financial amounts
4. Date fields are 8 characters in DBF date format
5. Index columns indicate primary keys or indexed fields for faster lookups
6. "Machine" collation indicates system-level sorting
7. "No" in Nulls column means the field is mandatory (NOT NULL)
