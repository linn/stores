import { ReportActions } from '@linn-it/linn-form-components-library';
import { qcPartsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.qcPartsReport.item,
    reportTypes.qcPartsReport.actionType,
    reportTypes.qcPartsReport.uri,
    actionTypes,
    config.appRoot
);
