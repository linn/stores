import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { salesAccountsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.salesAccounts.item,
    itemTypes.salesAccounts.actionType,
    itemTypes.salesAccounts.uri,
    actionTypes,
    config.appRoot
);
