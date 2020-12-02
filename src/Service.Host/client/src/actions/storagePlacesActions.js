import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { storagePlacesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.storagePlaces.item,
    itemTypes.storagePlaces.actionType,
    itemTypes.storagePlaces.uri,
    actionTypes,
    config.appRoot
);
