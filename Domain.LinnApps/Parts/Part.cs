namespace Domain.LinnApps.Parts
{
    using System;

    public class Part
    {
        public int BridgeId { get; set; }

        ////PART_NUMBER NOT NULL VARCHAR2(14)
        public string PartNumber { get; set; }

        ////DESCRIPTION NOT NULL VARCHAR2(200)
        public string Description { get; set; }

        ////LINN_PRODUCED VARCHAR2(1)
        ////QC_ON_RECEIPT VARCHAR2(1)
        ////CREATED_BY NOT NULL NUMBER(6)
    
        ////ROOT_PRODUCT VARCHAR2(10)
        public string RootProduct { get; set; }

        ////DATE_CREATED DATE
        ////DATE_LIVE DATE
        ////LIVE_BY NUMBER(6)
        ////BOM_ID NUMBER(6)
        ////OPTION_SET VARCHAR2(14)
        ////STOCK_CONTROLLED NOT NULL VARCHAR2(1)
        public string StockControlled { get; set; }

        public string SafetyCriticalPart { get; set; }
        
        ////DATE_PURCH_PHASE_OUT DATE
        
        ////PRODUCT_ANALYSIS_CODE VARCHAR2(10)
        public ProductAnalysisCode ProductAnalysisCode { get; set; }

        ////PARETO_CLASS VARCHAR2(2)
        public ParetoClass ParetoClass { get; set; }

        ////DECREMENT_RULE VARCHAR2(10)
        ////ASSEMBLY_TECHNOLOGY VARCHAR2(4)
        ////BOM_TYPE VARCHAR2(1)
        ////OUR_UNIT_OF_MEASURE VARCHAR2(14)
        ////COSTING_PRICE NUMBER(14,4)
        ////MATERIAL_PRICE NUMBER(16,6)
        ////LABOUR_PRICE NUMBER(16,6)
        ////EXPECTED_UNIT_PRICE NUMBER(16,6)
        ////SERNOS_SEQUENCE VARCHAR2(10)
        ////PLANNER_STORY VARCHAR2(200)
        ////PREFERRED_SUPPLIER NUMBER(6)
        ////ACCOUNTING_COMPANY VARCHAR2(10)
        public string AccountingCompany { get; set; }

        ////SAFETY_WEEKS NUMBER(6)
        ////NOMACC_NOMACC_ID NUMBER(6)
        ////UNIT_OF_MEASURE VARCHAR2(14)
        ////RM_FG VARCHAR2(1)
        ////PARETO_CODE VARCHAR2(2)
        ////MIN_RAIL NUMBER(10,2)
        ////MAX_RAIL NUMBER(10,2)
        ////RAIL_METHOD VARCHAR2(10)
        ////NON_FC_REQT NUMBER(14,5)
        ////SPARES_REQT NUMBER(14,5)
        ////ONE_OFF_REQT NUMBER(14,5)
        ////OUR_INSP_WEEKS NUMBER(4)
        ////DRAWING_REFERENCE VARCHAR2(100)
        ////CURRENCY VARCHAR2(4)
        ////DATE_DESIGN_OBSOLETE DATE
        ////ABSOLUTE_MINIMUM_QTY NUMBER(14,5)
        ////IGNORE_WORKSTN_STOCK VARCHAR2(1)
        ////CURRENCY_UNIT_PRICE NUMBER(16,6)
        ////BASE_UNIT_PRICE NUMBER(16,6)
        ////IMDS_ID_NUMBER NUMBER(10)
        ////QC_INFORMATION VARCHAR2(90)
        ////IMDS_WEIGHT_G NUMBER(12,4)
        ////BOM_VERIFY_FREQ_WEEKS NUMBER(4)
        ////ORDER_HOLD VARCHAR2(1)
        ////MR_ACTION_DATE DATE
        ////MR_ACTION_CODE VARCHAR2(10)
        ////MR_ACTION_BY NUMBER(6)
        ////SECOND_STAGE_BOARD VARCHAR2(1)
        ////PURCH_PHASED_OUT_BY NUMBER(6)
        ////REASON_PURCH_PHASED_OUT VARCHAR2(250)
        ////MECHANICAL_OR_ELECTRONIC VARCHAR2(2)
        ////SCRAP_OR_CONVERT VARCHAR2(20)
        ////PURCH_PHASE_OUT_TYPE VARCHAR2(20)
        ////SS_DESCRIPTION VARCHAR2(200)
        
        ////EMC_CRITICAL_PART VARCHAR2(1)
        public string EmcCriticalPart { get; set; }

        ////SINGLE_SOURCE_PART VARCHAR2(1)
        public string SingleSourcePart { get; set; }

        ////PERFORMANCE_CRITICAL_PART VARCHAR2(1)
        public string PerformanceCriticalPart { get; set; }
    
        ////SAFETY_DATA_DIRECTORY VARCHAR2(500)
        public string SafetyDataDirectory { get; set; }
        
        ////CCC_CRITICAL_PART VARCHAR2(1)
        public string CccCriticalPart { get; set; }

        ////PSU_PART VARCHAR2(1)
        public string PsuPart { get; set; }

        ////PART_CATEGORY VARCHAR2(2)
        
        ////SAFETY_CERTIFICATE_EXPIRY_DATE DATE
        public DateTime SafetyCertificateExpirationDate { get; set; }

        ////PLANNED_SURPLUS VARCHAR2(1)
        ////STOCK_NOTES VARCHAR2(500)
        ////TQMS_CATEGORY_OVERRIDE VARCHAR2(20)
        ////LIBRARY_REF VARCHAR2(30)
        ////FOOTPRINT_REF VARCHAR2(30)
        ////CAD_DATA_TYPE VARCHAR2(10)
    }
}