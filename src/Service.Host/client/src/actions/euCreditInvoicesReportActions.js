import { ReportActions } from '@linn-it/linn-form-components-library';
import { euCreditInvoicesReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.euCreditInvoicesReport.item,
    reportTypes.euCreditInvoicesReport.actionType,
    reportTypes.euCreditInvoicesReport.uri,
    actionTypes,
    config.appRoot
);
