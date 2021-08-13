import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { printCartonLabelActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.printCartonLabel.actionType,
    actionTypes,
    defaultState,
    'Label printed successfully'
);
