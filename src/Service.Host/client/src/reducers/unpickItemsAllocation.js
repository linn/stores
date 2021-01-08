import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { unpickItemsAllocationActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.unpickItemsAllocation.actionType,
    actionTypes,
    defaultState,
    'Unpicking completed successfully'
);
