import { itemStoreFactory } from '@linn-it/linn-form-components-library';
import { workstationTopUpStatusActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    item: null,
    editStatus: 'view'
};

export default itemStoreFactory(itemTypes.workstationTopUpStatus.actionType, actionTypes, defaultState);
