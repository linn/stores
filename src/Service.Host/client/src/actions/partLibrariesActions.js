import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partLibrariesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partLibraries.item,
    itemTypes.partLibraries.actionType,
    itemTypes.partLibraries.uri,
    actionTypes,
    config.appRoot
);
