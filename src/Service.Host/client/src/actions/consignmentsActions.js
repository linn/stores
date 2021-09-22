import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { consignmentsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.consignments.item,
    itemTypes.consignments.actionType,
    itemTypes.consignments.uri,
    actionTypes,
    config.appRoot
);
