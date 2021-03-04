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

export const movePalletToUpper = new ItemType(
    'movePalletToUpper',
    'MOVE_PALLET_TO_UPPER',
    '/logistics/wcs/move-to-upper'
);

export const movePalletsToUpper = new ItemType(
    'movePalletsToUpper',
    'MOVE_PALLETS_TO_UPPER',
    '/logistics/wcs/move-all-to-upper'
);

export const doWandItem = new ItemType('doWandItem', 'DO_WAND_ITEM', '/logistics/wand/items');

export const requisitionUnallocate = new ItemType(
    'requisitionUnallocate',
    'REQUISTION_UNALLOCATE',
    '/logistics/requisitions/actions/un-allocate'
);
