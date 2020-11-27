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
