import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { doBookInActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.doBookIn.actionType,
    actionTypes,
    defaultState,
    'Booked In successfully'
);
