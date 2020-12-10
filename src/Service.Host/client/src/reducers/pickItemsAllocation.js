import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { pickItemsAllocationActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.pickItemsAllocation.actionType,
    actionTypes,
    defaultState,
    'Picking completed successfully'
);
