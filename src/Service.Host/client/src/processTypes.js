import { ItemType } from '@linn-it/linn-form-components-library';

export const startAllocation = new ItemType(
    'startAllocation',
    'START_ALLOCATION',
    '/logistics/allocations'
);

export const finishAllocation = new ItemType(
    'finishAllocation',
    'FINISH_ALLOCATION',
    '/logistics/allocations/finish'
);

export const createAuditReqs = new ItemType(
    'createAuditReqs',
    'CREATE_AUDIT_REQS',
    '/inventory/storage-places/create-audit-reqs'
);

export const pickItemsAllocation = new ItemType(
    'pickItemsAllocation',
    'PICK_ITEMS_ALLOCATION',
    '/logistics/allocations/pick'
);

export const unpickItemsAllocation = new ItemType(
    'unpickItemsAllocation',
    'UNPICK_ITEMS_ALLOCATION',
    '/logistics/allocations/unpick'
);
