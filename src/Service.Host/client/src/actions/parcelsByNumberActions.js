import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { parcelsByNumberActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.parcelsByNumber.item,
    itemTypes.parcelsByNumber.actionType,
    itemTypes.parcelsByNumber.uri,
    actionTypes,
    config.appRoot
);
