import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { movePalletsToUpperActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.movePalletsToUpper.actionType,
    actionTypes,
    defaultState,
    'Pallet moved successfully'
);
