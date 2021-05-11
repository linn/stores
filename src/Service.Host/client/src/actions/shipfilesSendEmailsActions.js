import { ProcessActions } from '@linn-it/linn-form-components-library';
import { shipfilesSendEmailsActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.shipfileSendEmails.item,
    processTypes.shipfileSendEmails.actionType,
    processTypes.shipfileSendEmails.uri,
    actionTypes,
    config.appRoot
);
