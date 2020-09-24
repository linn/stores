import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { partTemplatesActionTypes as actionTypes } from '../../actions';
import * as itemTypes from '../../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(
    itemTypes.partTemplates.actionType,
    actionTypes,
    defaultState
);
