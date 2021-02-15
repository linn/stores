import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { wandItemsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.wandItems.item,
    itemTypes.wandItems.actionType,
    itemTypes.wandItems.uri,
    actionTypes,
    config.appRoot
);
