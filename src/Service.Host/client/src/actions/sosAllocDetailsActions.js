import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { sosAllocDetailsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.sosAllocDetails.item,
    itemTypes.sosAllocDetails.actionType,
    itemTypes.sosAllocDetails.uri,
    actionTypes,
    config.appRoot
);
