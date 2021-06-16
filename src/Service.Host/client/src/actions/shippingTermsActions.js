import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { shippingTermsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.shippingTerms.item,
    itemTypes.shippingTerms.actionType,
    itemTypes.shippingTerms.uri,
    actionTypes,
    config.appRoot
);
