import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partStorageTypesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partStorageTypes.item,
    itemTypes.partStorageTypes.actionType,
    itemTypes.partStorageTypes.uri,
    actionTypes,
    config.appRoot
);
