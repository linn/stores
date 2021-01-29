import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { workstationTopUpStatusActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.workstationTopUpStatus.item,
    itemTypes.workstationTopUpStatus.actionType,
    itemTypes.workstationTopUpStatus.uri,
    actionTypes,
    config.appRoot
);
