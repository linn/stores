import { ProcessActions } from '@linn-it/linn-form-components-library';
import { makeIntercompanyInvoicesActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.makeIntercompanyInvoices.item,
    processTypes.makeIntercompanyInvoices.actionType,
    processTypes.makeIntercompanyInvoices.uri,
    actionTypes,
    config.appRoot
);
