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

