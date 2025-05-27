import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { storesTransactionDefinitionsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.storesTransactionDefinitions.item,
    itemTypes.storesTransactionDefinitions.actionType,
    itemTypes.storesTransactionDefinitions.uri,
    actionTypes,
    config.appRoot
);
