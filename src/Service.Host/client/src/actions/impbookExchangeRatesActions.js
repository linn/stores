import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { impbookExchangeRatesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.impbookExchangeRates.item,
    itemTypes.impbookExchangeRates.actionType,
    itemTypes.impbookExchangeRates.uri,
    actionTypes,
    config.appRoot
);
