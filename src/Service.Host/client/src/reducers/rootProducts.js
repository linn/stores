import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { rootProductsActionTypes as actionTypes } from '../actions';
import * as itemTypes from '../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(itemTypes.rootProducts.actionType, actionTypes, defaultState);
