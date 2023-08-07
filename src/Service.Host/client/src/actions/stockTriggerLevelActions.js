import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { stockTriggerLevelActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.stockTriggerLevel.item,
    itemTypes.stockTriggerLevel.actionType,
    itemTypes.stockTriggerLevel.uri,
    actionTypes,
    config.appRoot
);
