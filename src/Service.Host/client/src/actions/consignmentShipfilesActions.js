import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { consignmentShipfilesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.consignmentShipfiles.item,
    itemTypes.consignmentShipfiles.actionType,
    itemTypes.consignmentShipfiles.uri,
    actionTypes,
    config.appRoot
);
