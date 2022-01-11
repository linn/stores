import { StateApiActions } from '@linn-it/linn-form-components-library';
import { partTemplateStateActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new StateApiActions(
    itemTypes.partTemplate.item,
    itemTypes.partTemplate.actionType,
    itemTypes.partTemplate.uri,
    actionTypes,
    config.appRoot,
    'application-state'
);
