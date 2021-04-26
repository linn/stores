import { ProcessActions } from '@linn-it/linn-form-components-library';
import { tpkTransferStockActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.tpkTransferStock.item,
    processTypes.tpkTransferStock.actionType,
    processTypes.tpkTransferStock.uri,
    actionTypes,
    config.appRoot
);
