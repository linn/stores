import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { auditLocationsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.auditLocations.item,
    itemTypes.auditLocations.actionType,
    itemTypes.auditLocations.uri,
    actionTypes,
    config.appRoot
);
