import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { suppliersApprovedCarrierActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.suppliersApprovedCarrier.item,
    itemTypes.suppliersApprovedCarrier.actionType,
    itemTypes.suppliersApprovedCarrier.uri,
    actionTypes,
    config.appRoot
);
