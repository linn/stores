import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { stockLocatorReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.stockLocatorReport.actionType,
    actionTypes,
    defaultState
);
