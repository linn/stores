import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { deptStockPartsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.deptStockParts.item,
    itemTypes.deptStockParts.actionType,
    itemTypes.deptStockParts.uri,
    actionTypes,
    config.appRoot
);
