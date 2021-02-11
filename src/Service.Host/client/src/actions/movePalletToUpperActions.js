import { ProcessActions } from '@linn-it/linn-form-components-library';
import { movePalletToUpperActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.movePalletToUpper.item,
    itemTypes.movePalletToUpper.actionType,
    itemTypes.movePalletToUpper.uri,
    actionTypes,
    config.appRoot
);
