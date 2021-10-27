import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { rsnAccessoriesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.rsnAccessories.item,
    itemTypes.rsnAccessories.actionType,
    itemTypes.rsnAccessories.uri,
    actionTypes,
    config.appRoot
);
