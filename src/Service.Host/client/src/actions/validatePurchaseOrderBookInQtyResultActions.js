import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { validatePurchaseOrderBookInQtyResultActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.validatePurchaseOrderBookInQtyResult.item,
    itemTypes.validatePurchaseOrderBookInQtyResult.actionType,
    itemTypes.validatePurchaseOrderBookInQtyResult.uri,
    actionTypes,
    config.appRoot
);
