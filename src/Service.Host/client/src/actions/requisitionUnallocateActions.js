import { ProcessActions } from '@linn-it/linn-form-components-library';
import { requisitionUnallocateActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.requisitionUnallocate.item,
    itemTypes.requisitionUnallocate.actionType,
    itemTypes.requisitionUnallocate.uri,
    actionTypes,
    config.appRoot
);
