import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { cartonTypesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.cartonTypes.item,
    itemTypes.cartonTypes.actionType,
    itemTypes.cartonTypes.uri,
    actionTypes,
    config.appRoot
);
