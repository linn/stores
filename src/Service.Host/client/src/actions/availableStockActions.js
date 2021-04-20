import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { availableStockActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.availableStock.item,
    itemTypes.availableStock.actionType,
    itemTypes.availableStock.uri,
    actionTypes,
    config.appRoot
);
