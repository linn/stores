import { ProcessActions } from '@linn-it/linn-form-components-library';
import { tpkTransferStock as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.transferStock.item,
    processTypes.transferStock.actionType,
    processTypes.transferStock.uri,
    actionTypes,
    config.appRoot
);
