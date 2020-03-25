import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { partActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.allocation.item,
    itemTypes.allocation.actionType,
    itemTypes.allocation.uri,
    actionTypes,
    config.appRoot
);
