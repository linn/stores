import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { loanDetailsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.loanDetails.item,
    itemTypes.loanDetails.actionType,
    itemTypes.loanDetails.uri,
    actionTypes,
    config.appRoot
);
