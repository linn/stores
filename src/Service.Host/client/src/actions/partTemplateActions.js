import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { partTemplateActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.partTemplate.item,
    itemTypes.partTemplate.actionType,
    itemTypes.partTemplate.uri,
    actionTypes,
    config.appRoot
);
