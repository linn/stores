import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { warehouseTaskActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.warehouseTask.item,
    itemTypes.warehouseTask.actionType,
    itemTypes.warehouseTask.uri,
    actionTypes,
    config.appRoot
);
