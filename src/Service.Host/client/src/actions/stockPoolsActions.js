import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { stockPoolsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.stockPools.item,
    itemTypes.stockPools.actionType,
    itemTypes.stockPools.uri,
    actionTypes,
    config.appRoot
);
