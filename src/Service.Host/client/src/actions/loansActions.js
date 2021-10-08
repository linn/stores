import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { loansActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.loans.item,
    itemTypes.loans.actionType,
    itemTypes.loans.uri,
    actionTypes,
    config.appRoot
);
