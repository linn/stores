import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { finishAllocationActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(processTypes.finishAllocation.actionType,
    actionTypes,
    defaultState,
    'Allocation completed successfully');
