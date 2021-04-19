import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { interCompanyInvoicesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.interCompanyInvoices.item,
    itemTypes.interCompanyInvoices.actionType,
    itemTypes.interCompanyInvoices.uri,
    actionTypes,
    config.appRoot
);
