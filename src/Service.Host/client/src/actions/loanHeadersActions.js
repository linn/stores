import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { loanHeadersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.loanHeaders.item,
    itemTypes.loanHeaders.actionType,
    itemTypes.loanHeaders.uri,
    actionTypes,
    config.appRoot
);
