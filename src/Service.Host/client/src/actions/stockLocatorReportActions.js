import { ReportActions } from '@linn-it/linn-form-components-library';
import { stockLocatorReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.stockLocatorReport.item,
    reportTypes.stockLocatorReport.actionType,
    reportTypes.stockLocatorReport.uri,
    actionTypes,
    config.appRoot
);
