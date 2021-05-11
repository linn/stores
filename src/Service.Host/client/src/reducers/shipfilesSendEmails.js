import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { shipfilesSendEmailsActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.shipfileSendEmails.actionType,
    actionTypes,
    defaultState,
    'SHIPFILES PROCESSED'
);
