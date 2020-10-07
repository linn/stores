import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { despatchLocationsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.despatchLocations.item,
    itemTypes.despatchLocations.actionType,
    itemTypes.despatchLocations.uri,
    actionTypes,
    config.appRoot
);
