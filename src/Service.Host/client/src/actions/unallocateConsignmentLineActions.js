import { ProcessActions } from '@linn-it/linn-form-components-library';
import { unallocateConsignmentLineActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.unallocateConsignmentLine.item,
    itemTypes.unallocateConsignmentLine.actionType,
    itemTypes.unallocateConsignmentLine.uri,
    actionTypes,
    config.appRoot
);
