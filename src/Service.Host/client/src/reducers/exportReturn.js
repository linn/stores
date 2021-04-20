import { itemStoreFactory } from '@linn-it/linn-form-components-library';
import { exportReturnActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    item: null,
    editStatus: 'view'
};

export default itemStoreFactory(itemTypes.exportReturn.actionType, actionTypes, defaultState);
