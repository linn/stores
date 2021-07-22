import { ReportActions } from '@linn-it/linn-form-components-library';
import { impbookEuReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.impbookEuReport.item,
    reportTypes.impbookEuReport.actionType,
    reportTypes.impbookEuReport.uri,
    actionTypes,
    config.appRoot
);
