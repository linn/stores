import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { storageLocationsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.storageLocations.item,
    itemTypes.storageLocations.actionType,
    itemTypes.storageLocations.uri,
    actionTypes,
    config.appRoot
);
