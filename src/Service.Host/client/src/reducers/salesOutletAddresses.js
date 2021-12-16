import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { salesOutletAddressesActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(
    itemTypes.salesOutletAddresses.actionType,
    actionTypes,
    defaultState
);
