import { ProcessActions } from '@linn-it/linn-form-components-library';
import { shipfilesSendEmailsActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.shipfilesSendEmails.item,
    processTypes.shipfilesSendEmails.actionType,
    processTypes.shipfilesSendEmails.uri,
    actionTypes,
    config.appRoot
);
