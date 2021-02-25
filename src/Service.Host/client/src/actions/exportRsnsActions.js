import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { exportRsnsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.exportRsns.item,
    itemTypes.exportRsns.actionType,
    itemTypes.exportRsns.uri,
    actionTypes,
    config.appRoot
);
