import { ProcessActions } from '@linn-it/linn-form-components-library';
import { unpickStockActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.unpickStock.item,
    processTypes.unpickStock.actionType,
    processTypes.unpickStock.uri,
    actionTypes,
    config.appRoot
);
