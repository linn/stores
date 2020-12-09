import { ProcessActions } from '@linn-it/linn-form-components-library';
import { unpickItemsAllocationActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.unpickItemsAllocation.item,
    itemTypes.unpickItemsAllocation.actionType,
    itemTypes.unpickItemsAllocation.uri,
    actionTypes,
    config.appRoot
);
