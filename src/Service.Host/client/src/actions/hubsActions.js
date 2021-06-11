import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { hubsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.hubs.item,
    itemTypes.hubs.actionType,
    itemTypes.hubs.uri,
    actionTypes,
    config.appRoot
);
