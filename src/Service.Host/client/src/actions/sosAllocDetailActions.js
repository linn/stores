import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { sosAllocDetailActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.sosAllocDetail.item,
    itemTypes.sosAllocDetail.actionType,
    itemTypes.sosAllocDetail.uri,
    actionTypes,
    config.appRoot
);
