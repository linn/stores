import { ItemType } from '@linn-it/linn-form-components-library';

export const part = new ItemType('part', 'PART', '/parts');

export const parts = new ItemType('parts', 'PARTS', '/parts');

export const sernosSequences = new ItemType(
    'sernosSequences',
    'SERNOS_SEQUENCES',
    '/inventory/sernos-sequences'
);

export const suppliers = new ItemType('suppliers', 'SUPPLIERS', '/inventory/suppliers');

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

export const parcel = new ItemType('parcel', 'PARCEL', '/parcels');

export const parcels = new ItemType('parcels', 'PARCELS', '/parcels');

export const employees = new ItemType('employees', 'EMPLOYEES', '/employees');
