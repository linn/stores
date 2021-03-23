import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { stockLocatorPricesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.stockLocatorPrices.item,
    itemTypes.stockLocatorPrices.actionType,
    itemTypes.stockLocatorPrices.uri,
    actionTypes,
    config.appRoot
);
