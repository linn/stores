import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { doWandItemActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.doWandItem.actionType,
    actionTypes,
    defaultState,
    'Item wanded successfully'
);
