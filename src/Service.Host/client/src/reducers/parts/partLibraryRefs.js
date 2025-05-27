import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { partLibraryRefsActionTypes as actionTypes } from '../../actions';
import * as itemTypes from '../../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(
    itemTypes.partLibraryRefs.actionType,
    actionTypes,
    defaultState
);
