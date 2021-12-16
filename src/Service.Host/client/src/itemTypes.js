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

export const partCategories = new ItemType(
    'partCategories',
    'PART_CATEGORIES',
    '/inventory/part-categories'
);

export const partTemplate = new ItemType(
    'partTemplate',
    'PART_TEMPLATE',
    '/inventory/part-templates'
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
    '/parts/can-be-made-live'
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
    '/inventory/storage-place'
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

export const mechPartSource = new ItemType('mechPartSource', 'MECH_PART_SOURCE', '/parts/sources');

export const manufacturers = new ItemType(
    'manufacturers',
    'MANUFACTURERS',
    '/inventory/manufacturers'
);

export const employees = new ItemType('employees', 'EMPLOYEES', '/inventory/employees');

export const partDataSheetValues = new ItemType(
    'partDataSheetValues',
    'PART_DATA_SHEET_VALUES',
    '/parts/data-sheet-values'
);

export const tqmsCategories = new ItemType(
    'tqmsCategories',
    'TQMS_CATEGORIES',
    '/parts/tqms-categories'
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

export const stockLocatorLocations = new ItemType(
    'stockLocatorLocations',
    'STOCK_LOCATOR_LOCATIONS',
    '/inventory/stock-locators-by-location'
);

export const nominalAccounts = new ItemType(
    'nominalAccounts',
    'NOMINAL_ACCOUNTS',
    '/inventory/nominal-accounts'
);
export const wandConsignments = new ItemType(
    'wandConsignments',
    'WAND_CONSIGNMENTS',
    '/logistics/wand/consignments'
);

export const wandItems = new ItemType('wandItems', 'WAND_ITEMS', '/logistics/wand/items');

export const salesOutlets = new ItemType(
    'salesOutlets',
    'SALES_OUTLETS',
    '/inventory/sales-outlets'
);

export const salesAccounts = new ItemType(
    'salesAccounts',
    'SALES_ACCOUNTS',
    '/inventory/sales-accounts'
);

export const exportRsns = new ItemType('exportRsns', 'EXPORT_RSNS', '/inventory/exports/rsns');

export const stockQuantities = new ItemType(
    'stockQuantities',
    'STOCK_QUANTITIES',
    '/inventory/stock-quantities'
);

export const transferableStock = new ItemType(
    'transferableStock',
    'TRANSFERABLE_STOCK',
    '/logistics/tpk/items'
);

export const exportReturn = new ItemType(
    'exportReturn',
    'EXPORT_RETURN',
    '/inventory/exports/returns'
);

export const availableStock = new ItemType(
    'availableStock',
    'AVAILABLE_STOCK',
    '/inventory/available-stock'
);

export const stockLocatorPrices = new ItemType(
    'stockLocatorPrices',
    'STOCK_LOCATOR_PRICES',
    '/inventory/stock-locators/prices'
);

export const reqMoves = new ItemType('reqMoves', 'REQ_MOVES', '/logistics/requisitions');

export const partStorageTypes = new ItemType(
    'partStorageTypes',
    'PART_STORAGE_TYPES',
    '/inventory/part-storage-types'
);

export const interCompanyInvoices = new ItemType(
    'interCompanyInvoices',
    'INTER_COMPANY_INVOICES',
    '/inventory/exports/inter-company-invoices'
);

export const tqmsJobRefs = new ItemType('tqmsJobRefs', 'TQMS_JOBREFS', '/inventory/tqms-jobrefs');

export const consignmentShipfile = new ItemType(
    'consignmentShipfile',
    'CONSIGNMENT_SHIPFILE',
    '/logistics/shipfiles'
);

export const consignmentShipfiles = new ItemType(
    'consignmentShipfiles',
    'CONSIGNMENT_SHIPFILES',
    '/logistics/shipfiles'
);

export const parcel = new ItemType('parcel', 'PARCEL', '/logistics/parcels');

export const parcels = new ItemType('parcels', 'PARCELS', '/logistics/parcels');

export const consignment = new ItemType('consignment', 'CONSIGNMENT', '/logistics/consignments');
export const consignments = new ItemType('consignments', 'CONSIGNMENTS', '/logistics/consignments');

export const hub = new ItemType('hub', 'HUB', '/logistics/hubs');
export const hubs = new ItemType('hubs', 'HUBS', '/logistics/hubs');

export const carrier = new ItemType('carrier', 'CARRIER', '/logistics/carriers');
export const carriers = new ItemType('carriers', 'CARRIERS', '/logistics/carriers');

export const shippingTerm = new ItemType(
    'shippingTerm',
    'SHIPPING_TERM',
    '/logistics/shipping-terms'
);

export const shippingTerms = new ItemType(
    'shippingTerms',
    'SHIPPING_TERMS',
    '/logistics/shipping-terms'
);

export const demLocations = new ItemType(
    'demLocations',
    'DEM_LOCATIONS',
    '/logistics/goods-in/dem-locations'
);

export const loanDetails = new ItemType('loanDetails', 'LOAN_DETAILS', '/logistics/loan-details');

export const validatePurchaseOrderResult = new ItemType(
    'validatePurchaseOrderResult',
    'VALIDATE_PURCHASE_ORDER_RESULT',
    '/logistics/purchase-orders/validate'
);

export const salesArticles = new ItemType(
    'salesArticles',
    'SALES_ARTICLES',
    '/inventory/sales-articles'
);

export const importBook = new ItemType('importBook', 'IMPORT_BOOK', '/logistics/import-books');

export const importBooks = new ItemType('importBooks', 'IMPORT_BOOKS', '/logistics/import-books');

export const impbookExchangeRates = new ItemType(
    'impbookExchangeRates',
    'IMPORT_BOOK_EXCHANGE_RATES',
    '/logistics/import-books/exchange-rates'
);

export const impbookTransportCodes = new ItemType(
    'impbookTransportCodes',
    'IMPORT_BOOK_TRANSPORT_CODES',
    '/logistics/import-books/transport-codes'
);

export const impbookTransactionCodes = new ItemType(
    'impbookTransactionCodes',
    'IMPORT_BOOK_TRANSACTION_CODES',
    '/logistics/import-books/transaction-codes'
);

export const impbookCpcNumbers = new ItemType(
    'impbookCpcNumbers',
    'IMPORT_BOOK_CPC_NUMBERS',
    '/logistics/import-books/cpc-numbers'
);

export const impbookDeliveryTerms = new ItemType(
    'impbookDeliveryTerms',
    'IMPORT_BOOK_DELIVERY_TERMS',
    '/logistics/import-books/delivery-terms'
);

export const ports = new ItemType('ports', 'PORTS', '/logistics/import-books/ports');

export const cartonTypes = new ItemType('cartonTypes', 'CARTON_TYPES', '/logistics/carton-types');

export const validatePurchaseOrderBookInQtyResult = new ItemType(
    'validatePurchaseOrderBookInQtyResult',
    'VALIDATE_PURCHASE_ORDER_BOOKIN_QTY_RESULT',
    '/logistics/purchase-orders/validate-qty'
);

export const req = new ItemType('req', 'REQ', '/logistics/requisitions');

export const debitNote = new ItemType(
    'debitNote',
    'DEBIT_NOTE',
    '/inventory/purchasing/debit-notes'
);

export const debitNotes = new ItemType(
    'debitNotes',
    'DEBIT_NOTES',
    '/inventory/purchasing/debit-notes'
);

export const stockMoves = new ItemType(
    'stockMoves',
    'STOCK-MOVES',
    '/inventory/stock-locators/stock-moves'
);

export const currencies = new ItemType('currencies', 'CURRENCIES', '/logistics/currencies');

export const exchangeRates = new ItemType(
    'exchangeRates',
    'EXCHANGE_RATES',
    '/logistics/import-books/exchange-rates'
);

export const validateStorageTypeResult = new ItemType(
    'validateStorageTypeResult',
    'VALIDATE_STORAGE_TYPE_RESULT',
    '/logistics/goods-in/validate-storage-type'
);

export const consignmentPackingList = new ItemType(
    'consignmentPackingList',
    'CONSIGNMENT_PACKING_LIST',
    '/logistics/consignments'
);

export const parcelsByNumber = new ItemType('parcels', 'PARCELS', '/logistics/parcels-by-number');

export const rsns = new ItemType('rsns', 'RSNS', '/logistics/rsns');

export const loans = new ItemType('loans', 'LOANS', '/logistics/loans');

export const purchaseOrders = new ItemType(
    'purchaseOrders',
    'PURCHASE_ORDERS',
    '/logistics/purchase-orders'
);

export const stockBatchesInRotationOrder = new ItemType(
    'stockBatchesInRotationOrder',
    'STOCK_BATCHES_IN_ROTATION_ORDER',
    '/inventory/stock-locators/rotations'
);

export const postDuty = new ItemType('postDuty', 'POST_DUTY', '/logistics/import-books/post-duty');

export const rsnAccessories = new ItemType(
    'rsnAccessories',
    'RSN_ACCESSORIES',
    '/logistics/goods-in/rsn-accessories'
);

export const rsnConditions = new ItemType(
    'rsnConditions',
    'RSN_CONDITIONS',
    '/logistics/goods-in/rsn-conditions'
);

export const validateRsnResult = new ItemType(
    'validateRsnResult',
    'VALIDATE_RSN_RESULT',
    '/logistics/rsn/validate'
);

export const addresses = new ItemType(
    'addresses',
    'ADDRESSES',
    '/production/maintenance/labels/addresses'
);

export const salesOutletAddresses = new ItemType(
    'salesOutletAddresses',
    'SALES_OUTLET_ADDRESSES',
    '/inventory/sales-outlets/addresses'
);
