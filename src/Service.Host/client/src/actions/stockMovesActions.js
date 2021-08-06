import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { stockMovesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.stockMoves.item,
    itemTypes.stockMoves.actionType,
    itemTypes.stockMoves.uri,
    actionTypes,
    config.appRoot
);
