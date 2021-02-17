import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { wandConsignmentsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.wandConsignments.item,
    itemTypes.wandConsignments.actionType,
    itemTypes.wandConsignments.uri,
    actionTypes,
    config.appRoot
);
