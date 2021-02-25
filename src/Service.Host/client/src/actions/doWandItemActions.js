import { ProcessActions } from '@linn-it/linn-form-components-library';
import { doWandItemActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.doWandItem.item,
    itemTypes.doWandItem.actionType,
    itemTypes.doWandItem.uri,
    actionTypes,
    config.appRoot
);
