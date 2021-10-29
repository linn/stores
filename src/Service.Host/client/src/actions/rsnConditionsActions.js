import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { rsnConditionsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.rsnConditions.item,
    itemTypes.rsnConditions.actionType,
    itemTypes.rsnConditions.uri,
    actionTypes,
    config.appRoot
);
