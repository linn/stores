import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printRsnActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.printRsn.item,
    itemTypes.printRsn.actionType,
    itemTypes.printRsn.uri,
    actionTypes,
    config.appRoot
);
