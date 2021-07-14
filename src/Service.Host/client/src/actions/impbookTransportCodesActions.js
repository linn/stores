import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { impbookTransportCodesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.impbookTransportCodes.item,
    itemTypes.impbookTransportCodes.actionType,
    itemTypes.impbookTransportCodes.uri,
    actionTypes,
    config.appRoot
);
