import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { impbookTransactionCodesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.impbookTransactionCodes.item,
    itemTypes.impbookTransactionCodes.actionType,
    itemTypes.impbookTransactionCodes.uri,
    actionTypes,
    config.appRoot
);
