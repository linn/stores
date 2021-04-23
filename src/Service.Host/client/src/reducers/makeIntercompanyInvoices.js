import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { makeIntercompanyInvoicesActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = {
    loading: false,
    messageText: '',
    messageVisible: null
};

export default processStoreFactory(
    processTypes.makeIntercompanyInvoices.actionType,
    actionTypes,
    defaultState,
    'Intercompany Invoices created successfully'
);
