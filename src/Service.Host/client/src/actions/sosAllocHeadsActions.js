import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { sosAllocHeadsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.sosAllocHeads.item,
    itemTypes.sosAllocHeads.actionType,
    itemTypes.sosAllocHeads.uri,
    actionTypes,
    config.appRoot
);
