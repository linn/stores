import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { partStorageTypesActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(
    itemTypes.partStorageTypes.actionType,
    actionTypes,
    defaultState
);
