import { ReportActions } from '@linn-it/linn-form-components-library';
import { impbookIprReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.impbookIprReport.item,
    reportTypes.impbookIprReport.actionType,
    reportTypes.impbookIprReport.uri,
    actionTypes,
    config.appRoot
);
