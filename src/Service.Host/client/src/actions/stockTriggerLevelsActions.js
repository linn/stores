import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { stockTriggerLevelsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.stockTriggerLevels.item,
    itemTypes.stockTriggerLevels.actionType,
    itemTypes.stockTriggerLevels.uri,
    actionTypes,
    config.appRoot
);
