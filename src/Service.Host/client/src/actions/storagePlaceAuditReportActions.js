import { ReportActions } from '@linn-it/linn-form-components-library';
import { storagePlaceAuditReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.storagePlaceAuditReport.item,
    reportTypes.storagePlaceAuditReport.actionType,
    reportTypes.storagePlaceAuditReport.uri,
    actionTypes,
    config.appRoot
);
