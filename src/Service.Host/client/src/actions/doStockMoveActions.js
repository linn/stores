import { ProcessActions } from '@linn-it/linn-form-components-library';
import { doStockMoveActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.doStockMove.item,
    itemTypes.doStockMove.actionType,
    itemTypes.doStockMove.uri,
    actionTypes,
    config.appRoot
);
