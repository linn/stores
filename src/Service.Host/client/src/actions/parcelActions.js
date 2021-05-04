import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { parcelActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.parcel.item,
    itemTypes.parcel.actionType,
    itemTypes.parcel.uri,
    actionTypes,
    config.appRoot
);
