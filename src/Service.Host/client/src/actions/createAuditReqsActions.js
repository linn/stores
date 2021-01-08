import { ProcessActions } from '@linn-it/linn-form-components-library';
import { createAuditReqsActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.createAuditReqs.item,
    processTypes.createAuditReqs.actionType,
    processTypes.createAuditReqs.uri,
    actionTypes,
    config.appRoot
);
