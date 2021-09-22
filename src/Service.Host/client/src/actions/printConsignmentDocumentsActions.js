import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printConsignmentDocumentsActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.printConsignmentDocuments.item,
    itemTypes.printConsignmentDocuments.actionType,
    itemTypes.printConsignmentDocuments.uri,
    actionTypes,
    config.appRoot
);
