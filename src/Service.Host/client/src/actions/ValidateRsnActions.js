import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { validateRsnActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.validateRsnResult.item,
    itemTypes.validateRsnResult.actionType,
    itemTypes.validateRsnResult.uri,
    actionTypes,
    config.appRoot
);
