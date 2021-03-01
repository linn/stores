import { ProcessActions } from '@linn-it/linn-form-components-library';
import { makeExportReturnActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.makeExportReturn.item,
    itemTypes.makeExportReturn.actionType,
    itemTypes.makeExportReturn.uri,
    actionTypes,
    config.appRoot
);
