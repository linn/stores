import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { impbookCpcNumbersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.impbookCpcNumbers.item,
    itemTypes.impbookCpcNumbers.actionType,
    itemTypes.impbookCpcNumbers.uri,
    actionTypes,
    config.appRoot
);
