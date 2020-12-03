import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { auditLocationActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.auditLocation.item,
    itemTypes.auditLocation.actionType,
    itemTypes.auditLocation.uri,
    actionTypes,
    config.appRoot
);
