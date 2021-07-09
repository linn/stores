import { ProcessActions } from '@linn-it/linn-form-components-library';
import { doBookInActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.doBookIn.item,
    itemTypes.doBookIn.actionType,
    itemTypes.doBookIn.uri,
    actionTypes,
    config.appRoot
);
