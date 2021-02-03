import { ReportActions } from '@linn-it/linn-form-components-library';
import { despatchPalletQueueReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.despatchPalletQueueReport.item,
    reportTypes.despatchPalletQueueReport.actionType,
    reportTypes.despatchPalletQueueReport.uri,
    actionTypes,
    config.appRoot
);
