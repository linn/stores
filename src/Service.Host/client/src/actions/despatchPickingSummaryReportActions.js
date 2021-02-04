import { ReportActions } from '@linn-it/linn-form-components-library';
import { despatchPickingSummaryReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.despatchPickingSummaryReport.item,
    reportTypes.despatchPickingSummaryReport.actionType,
    reportTypes.despatchPickingSummaryReport.uri,
    actionTypes,
    config.appRoot
);
