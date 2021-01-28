import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { stockLocatorActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.stockLocator.item,
    itemTypes.stockLocator.actionType,
    itemTypes.stockLocator.uri,
    actionTypes,
    config.appRoot
);
