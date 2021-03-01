import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { makeExportReturnActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.makeExportReturn.actionType,
    actionTypes,
    defaultState,
    'Successfully made export return'
);
