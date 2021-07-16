import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { portsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.ports.item,
    itemTypes.ports.actionType,
    itemTypes.ports.uri,
    actionTypes,
    config.appRoot
);
