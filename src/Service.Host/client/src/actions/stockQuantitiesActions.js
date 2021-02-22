import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { stockQuantitiesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.stockQuantities.item,
    itemTypes.stockQuantities.actionType,
    itemTypes.stockQuantities.uri,
    actionTypes,
    config.appRoot
);
