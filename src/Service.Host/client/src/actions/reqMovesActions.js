import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { reqMovesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.reqMoves.item,
    itemTypes.reqMoves.actionType,
    itemTypes.reqMoves.uri,
    actionTypes,
    config.appRoot,
    'application/vnd.linn.req-moves-summary+json;version=1'
);
