import { ReportActions } from '@linn-it/linn-form-components-library';
import { tqmsSummaryByCategoryReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.tqmsSummaryByCategoryReport.item,
    reportTypes.tqmsSummaryByCategoryReport.actionType,
    reportTypes.tqmsSummaryByCategoryReport.uri,
    actionTypes,
    config.appRoot
);
