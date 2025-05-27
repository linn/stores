import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { footprintRefOptionsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.footprintRefOptions.item,
    itemTypes.footprintRefOptions.actionType,
    itemTypes.footprintRefOptions.uri,
    actionTypes,
    config.appRoot
);
