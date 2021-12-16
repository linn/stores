import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { salesOutletAddressesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.salesOutletAddresses.item,
    itemTypes.salesOutletAddresses.actionType,
    itemTypes.salesOutletAddresses.uri,
    actionTypes,
    config.appRoot
);
