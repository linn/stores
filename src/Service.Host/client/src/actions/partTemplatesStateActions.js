import { StateApiActions } from '@linn-it/linn-form-components-library';
import { partTemplatesStateActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new StateApiActions(
    itemTypes.partTemplates.item,
    itemTypes.partTemplates.actionType,
    itemTypes.partTemplates.uri,
    actionTypes,
    config.appRoot,
    'application-state'
);
