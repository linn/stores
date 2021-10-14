import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { stockBatchesInRotationOrderActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.stockBatchesInRotationOrder.item,
    itemTypes.stockBatchesInRotationOrder.actionType,
    itemTypes.stockBatchesInRotationOrder.uri,
    actionTypes,
    config.appRoot
);
