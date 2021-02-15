import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { movePalletToUpperActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.movePalletToUpper.actionType,
    actionTypes,
    defaultState,
    'Pallet moved successfully'
);
