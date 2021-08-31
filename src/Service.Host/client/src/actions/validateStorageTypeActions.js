import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { validateStorageTypeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.validateStorageTypeResult.item,
    itemTypes.validateStorageTypeResult.actionType,
    itemTypes.validateStorageTypeResult.uri,
    actionTypes,
    config.appRoot
);
