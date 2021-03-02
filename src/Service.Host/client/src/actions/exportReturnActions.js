import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { exportReturnActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.exportReturn.item,
    itemTypes.exportReturn.actionType,
    itemTypes.exportReturn.uri,
    actionTypes,
    config.appRoot
);
