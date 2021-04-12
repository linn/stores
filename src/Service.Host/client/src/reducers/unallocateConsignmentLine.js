import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { unallocateConsignmentLineActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.unallocateConsignmentLine.actionType,
    actionTypes,
    defaultState,
    'Consignment line unallocated successfully'
);
