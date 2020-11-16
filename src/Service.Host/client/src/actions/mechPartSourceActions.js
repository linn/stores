import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { mechPartSourceActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.mechPartSource.item,
    itemTypes.mechPartSource.actionType,
    itemTypes.mechPartSource.uri,
    actionTypes,
    config.appRoot
);
