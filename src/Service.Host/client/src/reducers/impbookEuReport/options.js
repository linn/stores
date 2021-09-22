import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { impbookEuReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.impbookEuReport.actionType,
    actionTypes,
    defaultState
);
