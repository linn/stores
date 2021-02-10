import { ProcessActions } from '@linn-it/linn-form-components-library';
import { movePalletsToUpperActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.movePalletsToUpper.item,
    itemTypes.movePalletsToUpper.actionType,
    itemTypes.movePalletsToUpper.uri,
    actionTypes,
    config.appRoot
);
