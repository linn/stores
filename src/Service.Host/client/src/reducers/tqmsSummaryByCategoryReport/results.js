import { reportsResultsFactory } from '@linn-it/linn-form-components-library';
import { tqmsSummaryByCategoryReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { lading: false, data: null };

export default reportsResultsFactory(
    reportTypes.tqmsSummaryByCategoryReport.actionType,
    actionTypes,
    defaultState
);
