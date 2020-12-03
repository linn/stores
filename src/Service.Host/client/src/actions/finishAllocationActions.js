import { ProcessActions } from '@linn-it/linn-form-components-library';
import { finishAllocationActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.finishAllocation.item,
    itemTypes.finishAllocation.actionType,
    itemTypes.finishAllocation.uri,
    actionTypes,
    config.appRoot
);
