import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { transferableStockActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.transferableStock.item,
    itemTypes.transferableStock.actionType,
    itemTypes.transferableStock.uri,
    actionTypes,
    config.appRoot
);
