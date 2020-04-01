import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { decrementRulesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.decrementRules.item,
    itemTypes.decrementRules.actionType,
    itemTypes.decrementRules.uri,
    actionTypes,
    config.appRoot
);
