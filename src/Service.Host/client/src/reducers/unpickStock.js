import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { unpickStockActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.unpickStock.actionType,
    actionTypes,
    defaultState,
    'STOCK UNPICKED'
);
