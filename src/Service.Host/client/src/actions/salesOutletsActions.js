import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { salesOutletsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.salesOutlets.item,
    itemTypes.salesOutlets.actionType,
    itemTypes.salesOutlets.uri,
    actionTypes,
    config.appRoot
);
