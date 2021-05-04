import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { tqmsSummaryByCategoryReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.tqmsSummaryByCategoryReport.actionType,
    actionTypes,
    defaultState
);
