import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { createAuditReqsActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = {
    loading: false,
    messageText: '',
    messageVisible: null
};

export default processStoreFactory(
    processTypes.createAuditReqs.actionType,
    actionTypes,
    defaultState,
    'Audit Reqs created successfully'
);
