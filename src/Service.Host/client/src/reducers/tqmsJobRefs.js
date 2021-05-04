import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { tqmsJobRefsActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(itemTypes.tqmsJobRefs.actionType, actionTypes, defaultState);
