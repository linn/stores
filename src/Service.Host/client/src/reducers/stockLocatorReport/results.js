import { reportResultsFactory } from '@linn-it/linn-form-components-library';
import { stockLocatorReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { lading: false, data: null };

export default reportResultsFactory(
    reportTypes.stockLocatorReport.actionType,
    actionTypes,
    defaultState
);
