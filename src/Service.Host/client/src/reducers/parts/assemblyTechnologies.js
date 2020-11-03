import { collectionStoreFactory } from '@linn-it/linn-form-components-library';
import { assemblyTechnologiesActionTypes as actionTypes } from '../../actions';
import * as itemTypes from '../../itemTypes';

const defaultState = {
    loading: false,
    items: []
};

export default collectionStoreFactory(
    itemTypes.assemblyTechnologies.actionType,
    actionTypes,
    defaultState
);
