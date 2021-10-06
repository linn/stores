import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { rsnsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.rsns.item,
    itemTypes.rsns.actionType,
    itemTypes.rsns.uri,
    actionTypes,
    config.appRoot
);
