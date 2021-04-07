import { ProcessActions } from '@linn-it/linn-form-components-library';
import { unallocateConsignmentActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.unallocateConsignment.item,
    itemTypes.unallocateConsignment.actionType,
    itemTypes.unallocateConsignment.uri,
    actionTypes,
    config.appRoot
);
