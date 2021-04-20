import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { unallocateConsignmentActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.unallocateConsignment.actionType,
    actionTypes,
    defaultState,
    'Consignment unallocated successfully'
);
