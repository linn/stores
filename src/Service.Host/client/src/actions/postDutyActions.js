import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { postDutyActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.postDuty.item,
    itemTypes.postDuty.actionType,
    itemTypes.postDuty.uri,
    actionTypes,
    config.appRoot
);
