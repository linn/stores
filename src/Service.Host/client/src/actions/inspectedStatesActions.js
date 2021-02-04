import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { inspectedStatesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.inspectedStates.item,
    itemTypes.inspectedStates.actionType,
    itemTypes.inspectedStates.uri,
    actionTypes,
    config.appRoot
);
