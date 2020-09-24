import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partTemplatesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partTemplates.item,
    itemTypes.partTemplates.actionType,
    itemTypes.partTemplates.uri,
    actionTypes,
    config.appRoot
);
