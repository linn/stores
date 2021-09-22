import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printConsignmentLabelActionTypes as actionTypes } from './index';
import * as itemTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    itemTypes.printConsignmentLabel.item,
    itemTypes.printConsignmentLabel.actionType,
    itemTypes.printConsignmentLabel.uri,
    actionTypes,
    config.appRoot
);
