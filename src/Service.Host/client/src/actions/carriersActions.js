import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { carriersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.carriers.item,
    itemTypes.carriers.actionType,
    itemTypes.carriers.uri,
    actionTypes,
    config.appRoot
);
