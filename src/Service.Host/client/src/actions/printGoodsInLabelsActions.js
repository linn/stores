import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printGoodsInLabelsActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.printGoodsInLabels.item,
    itemTypes.printGoodsInLabels.actionType,
    itemTypes.printGoodsInLabels.uri,
    actionTypes,
    config.appRoot
);
