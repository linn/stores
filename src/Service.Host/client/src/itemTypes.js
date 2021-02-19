import { ItemType } from '@linn-it/linn-form-components-library';

export const part = new ItemType('part', 'PART', '/parts');

export const parts = new ItemType('parts', 'PARTS', '/parts');

export const sernosSequences = new ItemType(
    'sernosSequences',
    'SERNOS_SEQUENCES',
    '/inventory/sernos-sequences'
);

export const suppliers = new ItemType('suppliers', 'SUPPLIERS', '/inventory/suppliers');
export const suppliersApprovedCarrier = new ItemType(
    'suppliersApprovedCarrier',
    'SUPPLIERS_APPROVED_CARRER',
    '/inventory/suppliers-approved-carrier'
);

export const carriers = new ItemType('carriers', 'CARRIERS', '/inventory/carriers');

export const partCategories = new ItemType(
    'partCategories',
    'PART_CATEGORIES',
    '/inventory/part-categories'
);

export const partTemplates = new ItemType(
    'partTemplates',
    'PART_TEMPLATES',
    '/inventory/part-templates'
);

export const departments = new ItemType('departments', 'DEPARTMENTS', '/inventory/departments');

export const accountingCompanies = new ItemType(
    'accountingCompanies',
    'ACCOUNTING_COMPANIES',
    '/inventory/accounting-companies'
);

export const rootProducts = new ItemType(
    'rootProducts',
    'ROOT_PRODUCTS',
    '/inventory/root-products'
);

export const unitsOfMeasure = new ItemType(
    'unitsOfMeasure',
    'UNITS_OF_MEASURE',
    '/inventory/units-of-measure'
);

export const allocation = new ItemType('allocation', 'ALLOCATION', '/logistics/allocations');

export const productAnalysisCodes = new ItemType(
    'productAnalysisCodes',
    'PRODUCT_ANALYSIS_CODES',
    '/inventory/product-analysis-codes'
);

export const nominal = new ItemType('nominal', 'NOMINAL', '/inventory/nominal-for-department');

export const assemblyTechnologies = new ItemType(
    'assemblyTechnologies',
    'ASSEMBLY_TECHNOLOGIES',
    '/inventory/assembly-technologies'
);

export const decrementRules = new ItemType(
    'decrementRules',
    'DECREMENT_RULES',
    '/inventory/decrement-rules'
);

export const stockPool = new ItemType('stockPool', 'STOCK_POOL', '/inventory/stock-pools');

export const stockPools = new ItemType('stockPools', 'STOCK_POOLS', '/inventory/stock-pools');

export const despatchLocation = new ItemType(
    'despatchLocation',
    'DESPATCH_LOCATION',
    '/logistics/despatch-locations'
);

export const despatchLocations = new ItemType(
    'despatchLocations',
    'DESPATCH_LOCATIONS',
    '/logistics/despatch-locations'
);

export const countries = new ItemType('countries', 'COUNTRIES', '/logistics/countries');

export const partLiveTest = new ItemType(
    'partLiveTest',
    'PART_LIVE_TEST',
    '/inventory/parts/can-be-made-live'
);

export const sosAllocHeads = new ItemType(
    'sosAllocHeads',
    'SOS_ALLOC_HEADS',
    '/logistics/sos-alloc-heads'
);

export const sosAllocDetail = new ItemType(
    'sosAllocDetail',
    'SOS_ALLOC_DETAIL',
    '/logistics/sos-alloc-details'
);
export const sosAllocDetails = new ItemType(
    'sosAllocDetails',
    'SOS_ALLOC_DETAILS',
    '/logistics/sos-alloc-details'
);
export const storagePlace = new ItemType(
    'storagePlace',
    'STORAGE_PLACE',
    'inventory/storage-places'
);

export const storagePlaces = new ItemType(
    'storagePlaces',
    'STORAGE_PLACES',
    '/inventory/storage-places'
);

export const auditLocation = new ItemType(
    'auditLocation',
    'AUDIT_LOCATION',
    '/inventory/audit-locations'
);

export const auditLocations = new ItemType(
    'auditLocations',
    'AUDIT_LOCATIONS',
    '/inventory/audit-locations'
);

export const mechPartSource = new ItemType(
    'mechPartSource',
    'MECH_PART_SOURCE',
    '/inventory/parts/sources'
);

export const manufacturers = new ItemType(
    'manufacturers',
    'MANUFACTURERS',
    '/inventory/manufacturers'
);

export const employees = new ItemType('employees', 'EMPLOYEES', '/inventory/employees');

export const partDataSheetValues = new ItemType(
    'partDataSheetValues',
    'PART_DATA_SHEET_VALUES',
    '/inventory/parts/data-sheet-values'
);

export const tqmsCategories = new ItemType(
    'tqmsCategories',
    'TQMS_CATEGORIES',
    '/inventory/parts/tqms-categories'
);

export const workstationTopUpStatus = new ItemType(
    'workstationTopUpStatus',
    'WORKSTATION_TOP_UP_STATUS',
    '/logistics/workstations/top-up'
);

export const deptStockParts = new ItemType(
    'deptStockParts',
    'DEPT_STOCK_PARTS',
    '/parts/dept-stock-parts'
);

export const stockLocator = new ItemType(
    'stockLocator',
    'STOCK_LOCATOR',
    '/inventory/stock-locators'
);

export const stockLocators = new ItemType(
    'stockLocators',
    'STOCK_LOCATORS',
    '/inventory/stock-locators'
);

export const stockLocatorBatches = new ItemType(
    'stockLocatorBatches',
    'STOCK_LOCATOR_BATCHES',
    '/inventory/stock-locators/batches'
);

export const storageLocations = new ItemType(
    'storageLocations',
    'STORAGE_LOCATIONS',
    '/inventory/storage-locations'
);

export const inspectedStates = new ItemType(
    'inspectedStates',
    'INSPECTED_STATES',
    '/inventory/stock-locators/states'
);

export const wandConsignments = new ItemType(
    'wandConsignments',
    'WAND_CONSIGNMENTS',
    '/logistics/wand/consignments'
);

export const wandItems = new ItemType('wandItems', 'WAND_ITEMS', '/logistics/wand/items');

export const parcel = new ItemType('parcel', 'PARCEL', '/logistics/parcels');

export const parcels = new ItemType('parcels', 'PARCELS', '/logistics/parcels');
