import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { tqmsJobRefsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.tqmsJobRefs.item,
    itemTypes.tqmsJobRefs.actionType,
    itemTypes.tqmsJobRefs.uri,
    actionTypes,
    config.appRoot
);
