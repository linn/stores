import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { unallocateReqActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.unallocateReq.actionType,
    actionTypes,
    defaultState,
    'STOCK UNPICKED'
);
