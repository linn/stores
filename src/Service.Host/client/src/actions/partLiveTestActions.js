import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { partLiveTestActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.partLiveTest.item,
    itemTypes.partLiveTest.actionType,
    itemTypes.partLiveTest.uri,
    actionTypes,
    config.appRoot
);
