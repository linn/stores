import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { impbookDeliveryTermsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.impbookDeliveryTerms.item,
    itemTypes.impbookDeliveryTerms.actionType,
    itemTypes.impbookDeliveryTerms.uri,
    actionTypes,
    config.appRoot
);
