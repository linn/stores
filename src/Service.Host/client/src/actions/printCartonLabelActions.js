import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printCartonLabelActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.printCartonLabel.item,
    itemTypes.printCartonLabel.actionType,
    itemTypes.printCartonLabel.uri,
    actionTypes,
    config.appRoot
);
