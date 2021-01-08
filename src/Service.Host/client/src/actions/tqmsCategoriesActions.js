import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { tqmsCategoriesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.tqmsCategories.item,
    itemTypes.tqmsCategories.actionType,
    itemTypes.tqmsCategories.uri,
    actionTypes,
    config.appRoot
);
