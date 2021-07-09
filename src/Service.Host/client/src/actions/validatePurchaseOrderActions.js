import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { validatePurchaseOrderActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.validatePurchaseOrderResult.item,
    itemTypes.validatePurchaseOrderResult.actionType,
    itemTypes.validatePurchaseOrderResult.uri,
    actionTypes,
    config.appRoot
);
