import { ItemType } from '@linn-it/linn-form-components-library';

// eslint-disable-next-line import/prefer-default-export
export const wwdReport = new ItemType(
    'wwdReport',
    'WWD_REPORT',
    '/inventory/reports/what-will-decrement/report'
);

export const storagePlaceAuditReport = new ItemType(
    'storagePlaceAuditReport',
    'STORAGE_PLACE_AUDIT_REPORT',
    '/inventory/reports/storage-place-audit/report'
);

export const despatchPickingSummaryReport = new ItemType(
    'despatchPickingSummaryReport',
    'DESPATCH_PICKING_SUMMARY_REPORT',
    '/logistics/allocations/despatch-picking-summary'
);

export const despatchPalletQueueReport = new ItemType(
    'despatchPalletQueueReport',
    'DESPATCH_PALLET_QUEUE_REPORT',
    '/logistics/allocations/despatch-pallet-queue'
);

export const tqmsSummaryByCategoryReport = new ItemType(
    'tqmsSummaryByCategoryReport',
    'TQMS_SUMMARY_BY_CATEGORY_REPORT',
    '/inventory/tqms-category-summary/report'
);

export const impbookIprReport = new ItemType(
    'impbookIprReport',
    'IMPBOOK_IPR_REPORT',
    '/logistics/import-books/ipr/report'
);

export const impbookEuReport = new ItemType(
    'impbookEuReport',
    'IMPBOOK_EU_REPORT',
    '/logistics/import-books/eu/report'
);

export const qcPartsReport = new ItemType(
    'qcPartsReport',
    'QC_PARTS_REPORT',
    '/inventory/reports/qc-parts'
);

export const euCreditInvoicesReport = new ItemType(
    'euCreditInvoicesReport',
    'EU_CREDIT_INVOICES_REPORT',
    '/logistics/reports/eu-credit-invoices/report'
);
