import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { demLocationsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.demLocations.item,
    itemTypes.demLocations.actionType,
    itemTypes.demLocations.uri,
    actionTypes,
    config.appRoot
);
