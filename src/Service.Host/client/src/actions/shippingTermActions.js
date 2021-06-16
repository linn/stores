import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { shippingTermActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.shippingTerm.item,
    itemTypes.shippingTerm.actionType,
    itemTypes.shippingTerm.uri,
    actionTypes,
    config.appRoot
);
