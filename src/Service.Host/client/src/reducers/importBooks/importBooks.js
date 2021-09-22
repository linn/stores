import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { importBooksActionTypes as actionTypes } from '../../actions';
import * as itemTypes from '../../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(itemTypes.importBooks.actionType, actionTypes, defaultState);
