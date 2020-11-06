import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { parcelsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.parcels.item,
    itemTypes.parcel.actionType,
    itemTypes.parcel.uri,
    actionTypes,
    config.appRoot
);
