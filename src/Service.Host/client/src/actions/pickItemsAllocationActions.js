import { ProcessActions } from '@linn-it/linn-form-components-library';
import { pickItemsAllocationActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.pickItemsAllocation.item,
    itemTypes.pickItemsAllocation.actionType,
    itemTypes.pickItemsAllocation.uri,
    actionTypes,
    config.appRoot
);
