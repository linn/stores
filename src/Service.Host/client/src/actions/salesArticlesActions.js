import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { salesArticlesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.salesArticles.item,
    itemTypes.salesArticles.actionType,
    itemTypes.salesArticles.uri,
    actionTypes,
    config.appRoot
);
