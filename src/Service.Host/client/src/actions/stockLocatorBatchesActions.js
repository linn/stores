import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { stockLocatorBatchesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.stockLocatorBatches.item,
    itemTypes.stockLocatorBatches.actionType,
    itemTypes.stockLocatorBatches.uri,
    actionTypes,
    config.appRoot
);
