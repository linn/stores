import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { storesMoveLogReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.storesMoveLogReport.actionType,
    actionTypes,
    defaultState
);
