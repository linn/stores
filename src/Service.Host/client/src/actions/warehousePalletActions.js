import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { warehousePalletActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.warehousePallet.item,
    itemTypes.warehousePallet.actionType,
    itemTypes.warehousePallet.uri,
    actionTypes,
    config.appRoot
);
