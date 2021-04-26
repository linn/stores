import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { tpkTransferStockActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.tpkTransferStock.actionType,
    actionTypes,
    defaultState,
    'STOCK TRANSFERRED'
);
