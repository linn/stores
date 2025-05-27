import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partSourcesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partSources.item,
    itemTypes.partSources.actionType,
    itemTypes.partSources.uri,
    actionTypes,
    config.appRoot
);
