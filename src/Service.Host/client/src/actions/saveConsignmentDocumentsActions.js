import { ProcessActions } from '@linn-it/linn-form-components-library';
import { saveConsignmentDocumentsActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.saveConsignmentDocuments.item,
    itemTypes.saveConsignmentDocuments.actionType,
    itemTypes.saveConsignmentDocuments.uri,
    actionTypes,
    config.appRoot
);
