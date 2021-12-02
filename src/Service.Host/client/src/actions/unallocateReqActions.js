import { ProcessActions } from '@linn-it/linn-form-components-library';
import { unallocateReqActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.unallocateReq.item,
    processTypes.unallocateReq.actionType,
    processTypes.unallocateReq.uri,
    actionTypes,
    config.appRoot
);
