import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { stockLocatorsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.stockLocators.item,
    itemTypes.stockLocators.actionType,
    itemTypes.stockLocators.uri,
    actionTypes,
    config.appRoot
);
