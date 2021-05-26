import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { consignmentShipfileActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.consignmentShipfile.item,
    itemTypes.consignmentShipfile.actionType,
    itemTypes.consignmentShipfile.uri,
    actionTypes,
    config.appRoot
);
