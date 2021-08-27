import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { reqActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.req.item,
    itemTypes.req.actionType,
    itemTypes.req.uri,
    actionTypes,
    config.appRoot
);
