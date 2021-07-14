import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { importBooksActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.importBooks.item,
    itemTypes.importBooks.actionType,
    itemTypes.importBooks.uri,
    actionTypes,
    config.appRoot
);
