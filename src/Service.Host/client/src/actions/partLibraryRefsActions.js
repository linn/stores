import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partLibraryRefsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partLibraryRefs.item,
    itemTypes.partLibraryRefs.actionType,
    itemTypes.partLibraryRefs.uri,
    actionTypes,
    config.appRoot
);
