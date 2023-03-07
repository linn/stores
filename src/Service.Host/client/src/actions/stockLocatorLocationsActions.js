import { FetchApiActions, UpdateApiActions } from '@linn-it/linn-form-components-library';
import { stockLocatorLocationsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default {
    ...new FetchApiActions(
        itemTypes.stockLocatorLocations.item,
        itemTypes.stockLocatorLocations.actionType,
        itemTypes.stockLocatorLocations.uri,
        actionTypes,
        config.appRoot
    ),
    ...new UpdateApiActions(
        itemTypes.stockLocatorLocations.item,
        itemTypes.stockLocatorLocations.actionType,
        itemTypes.stockLocatorLocations.uri,
        actionTypes,
        config.appRoot
    )
};
