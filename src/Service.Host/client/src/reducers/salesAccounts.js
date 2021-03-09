import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { salesAccountsActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(
    itemTypes.salesAccounts.actionType,
    actionTypes,
    defaultState
);
