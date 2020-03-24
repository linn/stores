import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { rootProductsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.rootProducts.item,
    itemTypes.rootProducts.actionType,
    itemTypes.rootProducts.uri,
    actionTypes,
    config.appRoot
);
