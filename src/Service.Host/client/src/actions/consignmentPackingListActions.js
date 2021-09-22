import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { consignmentPackingListActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.consignmentPackingList.item,
    itemTypes.consignmentPackingList.actionType,
    itemTypes.consignmentPackingList.uri,
    actionTypes,
    config.appRoot
);
