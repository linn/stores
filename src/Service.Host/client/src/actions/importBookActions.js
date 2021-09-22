import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { importBookActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.importBook.item,
    itemTypes.importBook.actionType,
    itemTypes.importBook.uri,
    actionTypes,
    config.appRoot
);
