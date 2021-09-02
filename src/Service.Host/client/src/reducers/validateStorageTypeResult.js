import { itemStoreFactory } from '@linn-it/linn-form-components-library';
import { validateStorageTypeActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    item: null,
    editStatus: 'view'
};

export default itemStoreFactory(
    itemTypes.validateStorageTypeResult.actionType,
    actionTypes,
    defaultState
);
